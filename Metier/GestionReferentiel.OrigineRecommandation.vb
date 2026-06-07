Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "ORIGINE_RECOMMANDATION - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' OrigineRecommandation_GetAll
    '
    '   • Retourne la liste complète des origines de recommandation
    '------------------------------------------------------------
    Public Function OrigineRecommandation_GetAll() As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_SelectAll, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur OrigineRecommandation_GetAll.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' OrigineRecommandation_GetActifs
    '
    '   • Retourne uniquement les origines actives
    '------------------------------------------------------------
    Public Function OrigineRecommandation_GetActifs() As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_SelectActifs, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur OrigineRecommandation_GetActifs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' OrigineRecommandation_GetBySearch
    '
    '   • Recherche les origines par libellé
    '------------------------------------------------------------
    Public Function OrigineRecommandation_GetBySearch(search As String) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur OrigineRecommandation_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' OrigineRecommandation_Insert
    '
    '   • Insère une nouvelle origine de recommandation
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function OrigineRecommandation_Insert(o As RefOrigineRecommandation) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_Insert, conn)

                    cmd.Parameters.AddWithValue("@libelle_origine_recommandation", o.LibelleOrigineRecommandation.Trim())
                    cmd.Parameters.AddWithValue("@ordre", o.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@actif", If(o.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_SelectIdByLibelle, conn)
                    cmdId.Parameters.AddWithValue("@libelle_origine_recommandation", o.LibelleOrigineRecommandation.Trim())

                    Dim obj = cmdId.ExecuteScalar()
                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID de l'origine de recommandation après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur OrigineRecommandation_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' OrigineRecommandation_Update
    '
    '   • Met à jour une origine existante
    '------------------------------------------------------------
    Public Sub OrigineRecommandation_Update(o As RefOrigineRecommandation)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_Update, conn)

                    cmd.Parameters.AddWithValue("@id", o.IdOrigineRecommandation)
                    cmd.Parameters.AddWithValue("@libelle_origine_recommandation", o.LibelleOrigineRecommandation.Trim())
                    cmd.Parameters.AddWithValue("@ordre", o.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@actif", If(o.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur OrigineRecommandation_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' OrigineRecommandation_Delete
    '
    '   • Supprime une origine via id_origine_recommandation
    '------------------------------------------------------------
    Public Sub OrigineRecommandation_Delete(idOrigineRecommandation As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_origine_recommandation", idOrigineRecommandation)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur OrigineRecommandation_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

#End Region



End Module
