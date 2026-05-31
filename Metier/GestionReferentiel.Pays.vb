Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "PAYS - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_GetAll
    '
    '   • Retourne la liste complète des pays
    '------------------------------------------------------------
    Public Function Pays_GetAll() As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_SelectAll, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_GetAll.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_GetBySearch
    '
    '   • Recherche les pays par code, nom ou nationalité
    '------------------------------------------------------------
    Public Function Pays_GetBySearch(search As String) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_Insert
    '
    '   • Insère un nouveau pays
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function Pays_Insert(p As Pays) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom", p.NomPays.Trim())
                    cmd.Parameters.AddWithValue("@iso2", DbValueOrNullUpper(p.Iso2))
                    cmd.Parameters.AddWithValue("@iso3", DbValueOrNullUpper(p.Iso3))

                    cmd.ExecuteNonQuery()
                End Using

                Using cmdId As New MySqlConnector.MySqlCommand(QueryModule.Pays_SelectIdByNom, conn)
                    cmdId.Parameters.AddWithValue("@nom", p.NomPays.Trim())

                    Dim obj = cmdId.ExecuteScalar()
                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID du pays après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_Update
    '
    '   • Met à jour un pays existant via son identifiant
    '------------------------------------------------------------
    Public Sub Pays_Update(p As Pays)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_Update, conn)

                    cmd.Parameters.AddWithValue("@id", p.IdPays)
                    cmd.Parameters.AddWithValue("@nom", p.NomPays.Trim())
                    cmd.Parameters.AddWithValue("@iso2", DbValueOrNullUpper(p.Iso2))
                    cmd.Parameters.AddWithValue("@iso3", DbValueOrNullUpper(p.Iso3))

                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_CountUsageInAuteurs
    '
    '   • Compte les usages d'un pays dans la table auteurs
    '------------------------------------------------------------
    Public Function Pays_CountUsageInAuteurs(idPays As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_CountUsageInAuteurs, conn)

                    cmd.Parameters.AddWithValue("@id", idPays)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_CountUsageInAuteurs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_CountUsageInAuteursPays
    '
    '   • Compte les usages d'un pays dans la table de relation auteurs_pays
    '------------------------------------------------------------
    Public Function Pays_CountUsageInAuteursPays(idPays As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_CountUsageInAuteursPays, conn)

                    cmd.Parameters.AddWithValue("@id", idPays)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_CountUsageInAuteursPays.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_CountUsageInEditeurs
    '
    '   • Compte les usages d'un pays dans la table editeurs
    '------------------------------------------------------------
    Public Function Pays_CountUsageInEditeurs(idPays As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_CountUsageInEditeurs, conn)

                    cmd.Parameters.AddWithValue("@id", idPays)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_CountUsageInEditeurs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Pays_Delete
    '
    '   • Supprime un pays via id_pays
    '------------------------------------------------------------
    Public Sub Pays_Delete(idPays As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Pays_Delete, conn)

                    cmd.Parameters.AddWithValue("@id", idPays)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Pays_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

#End Region



End Module
