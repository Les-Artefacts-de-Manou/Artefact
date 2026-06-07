Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

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
