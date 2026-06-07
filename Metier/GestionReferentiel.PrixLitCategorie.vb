Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

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
                    cmd.Parameters.AddWithValue("@search", "%" & searchText.Trim() & "%")

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
                    cmd.Parameters.AddWithValue("@libelle_categorie", c.LibelleCategorie.Trim())
                    cmd.Parameters.AddWithValue("@description_categorie", If(String.IsNullOrWhiteSpace(c.DescriptionCategorie), DBNull.Value, c.DescriptionCategorie.Trim()))
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
                    cmd.Parameters.AddWithValue("@libelle_categorie", c.LibelleCategorie.Trim())
                    cmd.Parameters.AddWithValue("@description_categorie", If(String.IsNullOrWhiteSpace(c.DescriptionCategorie), DBNull.Value, c.DescriptionCategorie.Trim()))
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


End Module
