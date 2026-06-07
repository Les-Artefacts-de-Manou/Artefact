Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

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
                    cmd.Parameters.AddWithValue("@search", "%" & searchText.Trim() & "%")

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
