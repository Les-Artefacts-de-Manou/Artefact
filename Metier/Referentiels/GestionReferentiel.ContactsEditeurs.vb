'------------------------------------------------------------
' 📌 GestionReferentiel.vb - Module
' Version : V1.3
' Date    : 17/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Module métier des formulaires de référentiels.
' Centralise les opérations de lecture, recherche, insertion,
' modification, contrôle d’usage et suppression.
'
' Règles Artefact :
' - Pas d'UI ici.
' - Requêtes SQL centralisées dans QueryModule.
' - Exécution DB via DatabaseManager.
'
' Évolution :
' - V1.0 : Implémentation initiale des référentiels.
' - V1.1 : Ajout progressif des nouveaux référentiels.
' - V1.2 : Remise en ordre des procédures/fonctions par région
'          et homogénéisation des en-têtes.
' - V1.3 : Ajout CRUD ref_origine_recommandations.
'------------------------------------------------------------

Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "CONTACTS - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_GetAll
    '
    '   • Retourne la liste complète des contacts
    '------------------------------------------------------------
    Public Function Contact_GetAll() As DataTable

        Dim dt As New DataTable()

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "REF: erreur chargement contacts.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

        Return dt

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_GetBySearch
    '
    '   • Recherche les contacts par nom ou informations associées
    '   • Peut inclure les notes selon le paramètre fourni
    '------------------------------------------------------------
    Public Function Contact_GetBySearch(search As String) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@s", InputHelpers.BuildContainsSearchValue(search))

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Contact_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_ExistsByNom
    '
    '   • Vérifie si un contact existe déjà pour un nom donné
    '------------------------------------------------------------
    Public Function Contact_ExistsByNom(nom As String) As Boolean

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_ExistsByNom, conn)

                    cmd.Parameters.AddWithValue("@nom_contact", nom)

                    Dim result = Convert.ToInt32(cmd.ExecuteScalar())

                    Return result > 0

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "REF: erreur vérification nom contact.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_ExistsByNomExceptId
    '
    '   • Vérifie l'existence d'un nom de contact en excluant un identifiant
    '------------------------------------------------------------
    Public Function Contact_ExistsByNomExceptId(nom As String, idContact As ULong) As Boolean

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_ExistsByNomExceptId, conn)

                    cmd.Parameters.AddWithValue("@nom_contact", nom)
                    cmd.Parameters.AddWithValue("@id_contact", idContact)

                    Dim result = Convert.ToInt32(cmd.ExecuteScalar())

                    Return result > 0

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "REF: erreur vérification unicité nom contact.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_Insert
    '
    '   • Insère un nouveau contact
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Sub Contact_Insert(c As Contact)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_contact", c.NomContact)
                    cmd.Parameters.AddWithValue("@email_perso", c.EmailPerso)
                    cmd.Parameters.AddWithValue("@adresse_liseuse", c.AdresseLiseuse)
                    cmd.Parameters.AddWithValue("@type_liseuse", c.TypeLiseuse)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur insertion contact {c.NomContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_Update
    '
    '   • Met à jour un contact existant
    '------------------------------------------------------------
    Public Sub Contact_Update(c As Contact)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_Update, conn)

                    cmd.Parameters.AddWithValue("@id_contact", c.IdContact)
                    cmd.Parameters.AddWithValue("@nom_contact", c.NomContact)
                    cmd.Parameters.AddWithValue("@email_perso", c.EmailPerso)
                    cmd.Parameters.AddWithValue("@adresse_liseuse", c.AdresseLiseuse)
                    cmd.Parameters.AddWithValue("@type_liseuse", c.TypeLiseuse)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur update contact id={c.IdContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_CountUsage
    '
    '   • Compte les usages d'un contact dans les tables métier concernées
    '------------------------------------------------------------
    Public Function Contact_CountUsage(idContact As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_CountUsageInLivresContacts, conn)

                    cmd.Parameters.AddWithValue("@id_contact", idContact)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur vérification usage contact id={idContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_Delete
    '
    '   • Supprime un contact via id_contact
    '------------------------------------------------------------
    Public Sub Contact_Delete(idContact As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_contact", idContact)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur suppression contact id={idContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

#End Region


#Region "EDITEURS - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_GetAll
    '
    '   • Retourne la liste complète des éditeurs
    '------------------------------------------------------------
    Public Function Editeurs_GetAll() As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
                "Database: erreur Editeurs_GetAll.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_GetBySearch
    '
    '   • Recherche les éditeurs par nom, site web ou notes
    '   • Peut inclure les notes selon le paramètre fourni
    '------------------------------------------------------------
    Public Function Editeurs_GetBySearch(searchText As String,
                                     Optional includeNotes As Boolean = False) As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String =
                If(includeNotes,
                   QueryModule.Editeurs_SelectBySearchIncludingNotes,
                   QueryModule.Editeurs_SelectBySearch)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@search", searchText)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Editeurs_GetBySearch.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_Insert
    '
    '   • Insère un nouvel éditeur
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function Editeurs_Insert(editeur As Editeur) As ULong

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_Insert & "
SELECT LAST_INSERT_ID();", conn)

                    cmd.Parameters.AddWithValue("@nom_editeur", editeur.NomEditeur)

                    If editeur.IdPays.HasValue Then
                        cmd.Parameters.AddWithValue("@id_pays", editeur.IdPays.Value)
                    Else
                        cmd.Parameters.AddWithValue("@id_pays", DBNull.Value)
                    End If

                    cmd.Parameters.AddWithValue("@site_web", editeur.SiteWeb)
                    cmd.Parameters.AddWithValue("@notes_editeur_rtf", editeur.NotesEditeurRtf)
                    cmd.Parameters.AddWithValue("@notes_editeur_txt", editeur.NotesEditeurTxt)

                    Dim result = cmd.ExecuteScalar()

                    Return Convert.ToUInt64(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Editeurs_Insert.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.3 - 19/03/2026
    ' Editeurs_Update
    '
    '   • Met à jour un éditeur existant
    '------------------------------------------------------------
    Public Sub Editeurs_Update(editeur As Editeur)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_Update, conn)

                    cmd.Parameters.AddWithValue("@id_editeur", editeur.IdEditeur)
                    cmd.Parameters.AddWithValue("@nom_editeur", editeur.NomEditeur)

                    If editeur.IdPays.HasValue Then
                        cmd.Parameters.AddWithValue("@id_pays", editeur.IdPays.Value)
                    Else
                        cmd.Parameters.AddWithValue("@id_pays", DBNull.Value)
                    End If

                    cmd.Parameters.AddWithValue("@site_web", editeur.SiteWeb)
                    cmd.Parameters.AddWithValue("@notes_editeur_rtf", editeur.NotesEditeurRtf)
                    cmd.Parameters.AddWithValue("@notes_editeur_txt", editeur.NotesEditeurTxt)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Editeurs_Update.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_CountLivres
    '
    '   • Compte le nombre de livres liés à un éditeur
    '------------------------------------------------------------
    Public Function Editeurs_CountLivres(idEditeur As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_CountLivres, conn)

                    cmd.Parameters.AddWithValue("@id_editeur", idEditeur)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
                "Database: erreur Editeurs_CountLivres.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_Delete
    '
    '   • Supprime un éditeur via id_editeur
    '   • La FK livres.id_editeur reste en SET NULL
    '------------------------------------------------------------
    Public Sub Editeurs_Delete(idEditeur As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_editeur", idEditeur)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
                "Database: erreur Editeurs_Delete.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )

            Throw

        End Try

    End Sub

#End Region

End Module
