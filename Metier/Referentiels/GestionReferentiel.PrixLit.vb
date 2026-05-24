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

#Region "PRIXLIT - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_GetAll
    '
    '   • Retourne tous les prix littéraires
    '   • Peut filtrer sur les actifs
    '------------------------------------------------------------
    Public Function PrixLit_GetAll(actifsOnly As Boolean) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String = If(actifsOnly,
                                   QueryModule.PrixLit_SelectAll_ActifsOnly,
                                   QueryModule.PrixLit_SelectAll)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_GetAll.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_GetBySearch
    '
    '   • Recherche globale des prix littéraires
    '   • Peut inclure ou non les notes
    '   • Peut filtrer sur les actifs
    '------------------------------------------------------------
    Public Function PrixLit_GetBySearch(searchText As String,
                                        actifsOnly As Boolean,
                                        Optional includeNotes As Boolean = False) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String

                If actifsOnly Then
                    sql = If(includeNotes,
                         QueryModule.PrixLit_SelectBySearchIncludingNotes_ActifsOnly,
                         QueryModule.PrixLit_SelectBySearch_ActifsOnly)
                Else
                    sql = If(includeNotes,
                         QueryModule.PrixLit_SelectBySearchIncludingNotes,
                         QueryModule.PrixLit_SelectBySearch)
                End If

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@search", InputHelpers.BuildContainsSearchValue(searchText))

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_Insert
    '
    '   • Insère un prix littéraire
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function PrixLit_Insert(p As PrixLit) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_prixLit", InputHelpers.NormalizeText(p.NomPrixLit))
                    cmd.Parameters.AddWithValue("@description_prixLit", DbValueOrNull(p.DescriptionPrixLit))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_rtf", DbValueOrNull(p.NotesPrixLitRtf))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_txt", DbValueOrNull(p.NotesPrixLitTxt))
                    cmd.Parameters.AddWithValue("@is_actif", If(p.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand("SELECT LAST_INSERT_ID();", conn)

                    Dim obj = cmdId.ExecuteScalar()

                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID du prix littéraire après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)

                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_Update
    '
    '   • Met à jour un prix littéraire existant
    '------------------------------------------------------------
    Public Sub PrixLit_Update(p As PrixLit)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_Update, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", p.IdPrixLit)
                    cmd.Parameters.AddWithValue("@nom_prixLit", InputHelpers.NormalizeText(p.NomPrixLit))
                    cmd.Parameters.AddWithValue("@description_prixLit", DbValueOrNull(p.DescriptionPrixLit))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_rtf", DbValueOrNull(p.NotesPrixLitRtf))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_txt", DbValueOrNull(p.NotesPrixLitTxt))
                    cmd.Parameters.AddWithValue("@is_actif", If(p.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_Delete
    '
    '   • Supprime un prix littéraire
    '------------------------------------------------------------
    Public Sub PrixLit_Delete(idPrixLit As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_CountCategories
    '
    '   • Compte les catégories liées à un prix littéraire
    '------------------------------------------------------------
    Public Function PrixLit_CountCategories(idPrixLit As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_CountCategories, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then Return 0

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_CountCategories.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

#End Region


#Region "PRIXLIT_CATEGORIE - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitCategorie_GetByPrixLit
    '
    '   • Retourne les catégories d'un prix littéraire
    '------------------------------------------------------------
    Public Function PrixLitCategorie_GetByPrixLit(idPrixLit As ULong) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitCategorie_SelectByPrixLit, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitCategorie_GetByPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitCategorie_GetBySearch
    '
    '   • Recherche les catégories d'un prix littéraire
    '------------------------------------------------------------
    Public Function PrixLitCategorie_GetBySearch(idPrixLit As ULong, searchText As String) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitCategorie_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)
                    cmd.Parameters.AddWithValue("@search", InputHelpers.BuildContainsSearchValue(searchText))

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitCategorie_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitCategorie_Insert
    '
    '   • Insère une catégorie pour un prix littéraire
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function PrixLitCategorie_Insert(c As PrixLitCategorie) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitCategorie_Insert, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", c.IdPrixLit)
                    cmd.Parameters.AddWithValue("@libelle_categorie", InputHelpers.NormalizeText(c.LibelleCategorie))
                    cmd.Parameters.AddWithValue("@description_categorie", DbValueOrNull(c.DescriptionCategorie))
                    cmd.Parameters.AddWithValue("@ordre_affichage", c.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@is_actif", If(c.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand("SELECT LAST_INSERT_ID();", conn)

                    Dim obj = cmdId.ExecuteScalar()

                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID de la catégorie de prix littéraire après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)

                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitCategorie_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitCategorie_Update
    '
    '   • Met à jour une catégorie existante
    '------------------------------------------------------------
    Public Sub PrixLitCategorie_Update(c As PrixLitCategorie)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitCategorie_Update, conn)

                    cmd.Parameters.AddWithValue("@id_prixlit_categorie", c.IdPrixLitCategorie)
                    cmd.Parameters.AddWithValue("@libelle_categorie", InputHelpers.NormalizeText(c.LibelleCategorie))
                    cmd.Parameters.AddWithValue("@description_categorie", DbValueOrNull(c.DescriptionCategorie))
                    cmd.Parameters.AddWithValue("@ordre_affichage", c.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@is_actif", If(c.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitCategorie_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitCategorie_Delete
    '
    '   • Supprime une catégorie
    '------------------------------------------------------------
    Public Sub PrixLitCategorie_Delete(idPrixLitCategorie As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitCategorie_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_prixlit_categorie", idPrixLitCategorie)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitCategorie_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitCategorie_CountAnnees
    '
    '   • Compte les années liées à une catégorie
    '------------------------------------------------------------
    Public Function PrixLitCategorie_CountAnnees(idPrixLitCategorie As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitCategorie_CountAnnees, conn)

                    cmd.Parameters.AddWithValue("@id_prixlit_categorie", idPrixLitCategorie)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then Return 0

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitCategorie_CountAnnees.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

#End Region

#Region "PRIXLIT_ANNEE - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitAnnee_GetByCategorie
    '
    '   • Retourne les années d'une catégorie
    '------------------------------------------------------------
    Public Function PrixLitAnnee_GetByCategorie(idPrixLitCategorie As ULong) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitAnnee_SelectByCategorie, conn)

                    cmd.Parameters.AddWithValue("@id_prixlit_categorie", idPrixLitCategorie)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitAnnee_GetByCategorie.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitAnnee_GetByPrixLit
    '
    '   • Retourne toutes les années d’un prix
    '------------------------------------------------------------
    Public Function PrixLitAnnee_GetByPrixLit(idPrixLit As ULong) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitAnnee_SelectByPrixLit, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitAnnee_GetByPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitAnnee_GetBySearch
    '
    '   • Recherche dans les années d’un prix
    '------------------------------------------------------------
    Public Function PrixLitAnnee_GetBySearch(idPrixLit As ULong,
                                             searchText As String) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitAnnee_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)
                    cmd.Parameters.AddWithValue("@search", InputHelpers.BuildContainsSearchValue(searchText))

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitAnnee_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitAnnee_Insert
    '
    '   • Insère une année
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function PrixLitAnnee_Insert(a As PrixLitAnnee) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitAnnee_Insert, conn)

                    cmd.Parameters.AddWithValue("@id_prixlit_categorie", a.IdPrixLitCategorie)
                    cmd.Parameters.AddWithValue("@annee", a.Annee)

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand("SELECT LAST_INSERT_ID();", conn)

                    Dim obj = cmdId.ExecuteScalar()

                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID de l'année de prix littéraire après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)

                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitAnnee_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitAnnee_Update
    '
    '   • Met à jour une année existante
    '------------------------------------------------------------
    Public Sub PrixLitAnnee_Update(a As PrixLitAnnee)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitAnnee_Update, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit_Annee", a.IdPrixLitAnnee)
                    cmd.Parameters.AddWithValue("@id_prixlit_categorie", a.IdPrixLitCategorie)
                    cmd.Parameters.AddWithValue("@annee", a.Annee)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitAnnee_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitAnnee_Delete
    '
    '   • Supprime une année
    '------------------------------------------------------------
    Public Sub PrixLitAnnee_Delete(idPrixLitAnnee As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitAnnee_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit_Annee", idPrixLitAnnee)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitAnnee_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLitAnnee_CountLivres
    '
    '   • Compte les livres liés à une année
    '------------------------------------------------------------
    Public Function PrixLitAnnee_CountLivres(idPrixLitAnnee As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLitAnnee_CountLivres, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit_Annee", idPrixLitAnnee)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then Return 0

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLitAnnee_CountLivres.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

#End Region

End Module
