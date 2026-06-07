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



End Module
