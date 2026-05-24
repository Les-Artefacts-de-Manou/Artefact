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

#Region "FORMATFILE - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_GetAll
    '
    ' Retourne tous les formats de fichier.
    '------------------------------------------------------------
    Public Function FormatFile_GetAll() As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_GetAll.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_GetBySearch
    '
    ' Recherche sur nom_format, extension ou mime_type.
    '------------------------------------------------------------
    Public Function FormatFile_GetBySearch(searchText As String) As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_SelectBySearch, conn)

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
            "Database: erreur FormatFile_GetBySearch.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Insert
    '
    ' Ajoute un nouveau format de fichier.
    '------------------------------------------------------------
    Public Sub FormatFile_Insert(
    nomFormat As String,
    extension As String,
    mimeType As String,
    ordreAffichage As Integer,
    isActif As Boolean)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_format", nomFormat)
                    cmd.Parameters.AddWithValue("@extension", extension)
                    cmd.Parameters.AddWithValue("@mime_type", mimeType)
                    cmd.Parameters.AddWithValue("@ordre_affichage", ordreAffichage)
                    cmd.Parameters.AddWithValue("@is_actif", isActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_Insert.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Update
    '
    ' Met à jour un format de fichier existant.
    '------------------------------------------------------------
    Public Sub FormatFile_Update(
    idFormatFile As ULong,
    nomFormat As String,
    extension As String,
    mimeType As String,
    ordreAffichage As Integer,
    isActif As Boolean)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_Update, conn)

                    cmd.Parameters.AddWithValue("@id_formatFile", idFormatFile)
                    cmd.Parameters.AddWithValue("@nom_format", nomFormat)
                    cmd.Parameters.AddWithValue("@extension", extension)
                    cmd.Parameters.AddWithValue("@mime_type", mimeType)
                    cmd.Parameters.AddWithValue("@ordre_affichage", ordreAffichage)
                    cmd.Parameters.AddWithValue("@is_actif", isActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_Update.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Delete
    '
    ' Supprime un format de fichier.
    '------------------------------------------------------------
    Public Sub FormatFile_Delete(idFormatFile As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_formatFile", idFormatFile)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_Delete.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_CountLivresFichiers
    '
    ' Compte les références dans livres_fichiers.
    '------------------------------------------------------------
    Public Function FormatFile_CountLivresFichiers(idFormatFile As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_CountLivresFichiers, conn)

                    cmd.Parameters.AddWithValue("@id_formatFile", idFormatFile)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_CountLivresFichiers.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

#End Region


#Region "IMPRESSION - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_GetAll
    '
    ' Retourne la liste complète des impressions.
    '------------------------------------------------------------
    Public Function Impression_GetAll() As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_GetAll.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.1 - 14/03/2026
    ' Impression_GetBySearch
    '
    ' Recherche d'impressions par texte.
    ' Peut inclure ou non les notes.
    '------------------------------------------------------------
    Public Function Impression_GetBySearch(searchText As String,
                                       Optional includeNotes As Boolean = False) As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String =
                If(includeNotes,
                   QueryModule.Impression_SelectBySearchIncludingNotes,
                   QueryModule.Impression_SelectBySearch)

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
            "Database: erreur Impression_GetBySearch.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Insert
    '
    ' Ajoute une nouvelle impression.
    '------------------------------------------------------------
    Public Sub Impression_Insert(impression As Impression)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_impression", impression.NomImpression)
                    cmd.Parameters.AddWithValue("@description_impression", impression.DescriptionImpression)
                    cmd.Parameters.AddWithValue("@note_rtf", impression.NoteRtf)
                    cmd.Parameters.AddWithValue("@note_txt", impression.NoteTxt)
                    cmd.Parameters.AddWithValue("@envie_Cal", impression.EnvieCal)
                    cmd.Parameters.AddWithValue("@is_actif", impression.IsActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_Insert.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Update
    '
    ' Met à jour une impression existante.
    '------------------------------------------------------------
    Public Sub Impression_Update(impression As Impression)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_Update, conn)

                    cmd.Parameters.AddWithValue("@id_impression", impression.IdImpression)
                    cmd.Parameters.AddWithValue("@nom_impression", impression.NomImpression)
                    cmd.Parameters.AddWithValue("@description_impression", impression.DescriptionImpression)
                    cmd.Parameters.AddWithValue("@note_rtf", impression.NoteRtf)
                    cmd.Parameters.AddWithValue("@note_txt", impression.NoteTxt)
                    cmd.Parameters.AddWithValue("@envie_Cal", impression.EnvieCal)
                    cmd.Parameters.AddWithValue("@is_actif", impression.IsActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_Update.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Delete
    '
    ' Supprime une impression via id_impression.
    '------------------------------------------------------------
    Public Sub Impression_Delete(idImpression As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_impression", idImpression)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_Delete.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_CountLivres
    '
    ' Compte les livres liés à cette impression.
    '------------------------------------------------------------
    Public Function Impression_CountLivres(idImpression As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_CountLivres, conn)

                    cmd.Parameters.AddWithValue("@id_impression", idImpression)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_CountLivres.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_CountLivresStaging
    '
    ' Compte les livres_staging liés à cette impression.
    '------------------------------------------------------------
    Public Function Impression_CountLivresStaging(idImpression As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_CountLivresStaging, conn)

                    cmd.Parameters.AddWithValue("@id_impression", idImpression)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_CountLivresStaging.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

#End Region

End Module
