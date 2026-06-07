Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

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
