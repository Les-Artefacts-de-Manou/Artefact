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

                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

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



End Module
