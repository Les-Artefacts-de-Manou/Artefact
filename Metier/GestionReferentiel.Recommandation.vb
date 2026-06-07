Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "RECOMMANDATION - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_GetByOrigine
    '
    '   • Retourne les recommandations d'une origine
    '   • Peut filtrer sur les actives
    '------------------------------------------------------------
    Public Function Recommandation_GetByOrigine(idOrigine As ULong, actifsOnly As Boolean) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String = If(actifsOnly,
                    QueryModule.Recommandation_SelectByOrigine_ActifsOnly,
                    QueryModule.Recommandation_SelectByOrigine)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@id_origine_recommandation", idOrigine)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_GetByOrigine.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 19/03/2026
    ' Recommandation_GetByOrigineAndSearch
    '
    '   • Recherche les recommandations d'une origine parent
    '   • Peut inclure ou non le commentaire
    '------------------------------------------------------------
    Public Function Recommandation_GetByOrigineAndSearch(idOrigine As ULong,
                                                     search As String,
                                                     actifsOnly As Boolean,
                                                     Optional includeNotes As Boolean = False) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String

                If actifsOnly Then
                    sql = If(includeNotes,
                         QueryModule.Recommandation_SelectByOrigineAndSearchIncludingNotes_ActifsOnly,
                         QueryModule.Recommandation_SelectByOrigineAndSearch_ActifsOnly)
                Else
                    sql = If(includeNotes,
                         QueryModule.Recommandation_SelectByOrigineAndSearchIncludingNotes,
                         QueryModule.Recommandation_SelectByOrigineAndSearch)
                End If

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@id_origine_recommandation", idOrigine)
                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_GetByOrigineAndSearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_GetAll
    '
    '   • Retourne toutes les recommandations
    '   • Peut filtrer sur les actives
    '------------------------------------------------------------
    Public Function Recommandation_GetAll(actifsOnly As Boolean) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String = If(actifsOnly,
                                   QueryModule.Recommandation_SelectAll_ActifsOnly,
                                   QueryModule.Recommandation_SelectAll)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_GetAll.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_GetBySearch
    '
    '   • Recherche globale des recommandations
    '   • Peut inclure ou non le commentaire
    '------------------------------------------------------------
    Public Function Recommandation_GetBySearch(searchText As String,
                                           actifsOnly As Boolean,
                                           Optional includeNotes As Boolean = False) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String

                If actifsOnly Then
                    sql = If(includeNotes,
                         QueryModule.Recommandation_SelectBySearchIncludingNotes_ActifsOnly,
                         QueryModule.Recommandation_SelectBySearch_ActifsOnly)
                Else
                    sql = If(includeNotes,
                         QueryModule.Recommandation_SelectBySearchIncludingNotes,
                         QueryModule.Recommandation_SelectBySearch)
                End If

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@search", "%" & searchText.Trim() & "%")

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_Insert
    '
    '   • Insère une recommandation
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function Recommandation_Insert(r As Recommandation) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_Insert, conn)

                    cmd.Parameters.AddWithValue("@id_origine_recommandation", r.IdOrigineRecommandation)
                    cmd.Parameters.AddWithValue("@source_nom", r.SourceNom)
                    cmd.Parameters.AddWithValue("@source_login", r.SourceLogin)
                    cmd.Parameters.AddWithValue("@source_url", r.SourceURL)
                    cmd.Parameters.AddWithValue("@date_recommandation", If(r.DateRecommandation.HasValue, r.DateRecommandation.Value, DBNull.Value))
                    cmd.Parameters.AddWithValue("@commentaire_rtf", r.CommentaireRtf)
                    cmd.Parameters.AddWithValue("@commentaire_txt", r.CommentaireTxt)
                    cmd.Parameters.AddWithValue("@actif", If(r.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_SelectLastId, conn)
                    Dim obj = cmdId.ExecuteScalar()

                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID de la recommandation après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_Update
    '
    '   • Met à jour une recommandation existante
    '------------------------------------------------------------
    Public Sub Recommandation_Update(r As Recommandation)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_Update, conn)

                    cmd.Parameters.AddWithValue("@id", r.IdRecommandation)
                    cmd.Parameters.AddWithValue("@id_origine_recommandation", r.IdOrigineRecommandation)
                    cmd.Parameters.AddWithValue("@source_nom", r.SourceNom)
                    cmd.Parameters.AddWithValue("@source_login", r.SourceLogin)
                    cmd.Parameters.AddWithValue("@source_url", r.SourceURL)
                    cmd.Parameters.AddWithValue("@date_recommandation", If(r.DateRecommandation.HasValue, r.DateRecommandation.Value, DBNull.Value))
                    cmd.Parameters.AddWithValue("@commentaire_rtf", r.CommentaireRtf)
                    cmd.Parameters.AddWithValue("@commentaire_txt", r.CommentaireTxt)
                    cmd.Parameters.AddWithValue("@actif", If(r.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_Delete
    '
    '   • Supprime une recommandation
    '------------------------------------------------------------
    Public Sub Recommandation_Delete(idRecommandation As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_recommandation", idRecommandation)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_CountByOrigine
    '
    '   • Compte les recommandations d'une origine
    '------------------------------------------------------------
    Public Function Recommandation_CountByOrigine(idOrigine As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_CountByOrigine, conn)

                    cmd.Parameters.AddWithValue("@id_origine_recommandation", idOrigine)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then Return 0

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_CountByOrigine.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_CountUsageInLivres
    '------------------------------------------------------------
    Public Function Recommandation_CountUsageInLivres(idRecommandation As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_CountUsageInLivres, conn)

                    cmd.Parameters.AddWithValue("@id_recommandation", idRecommandation)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_CountUsageInLivres.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_CountUsageInStaging
    '------------------------------------------------------------
    Public Function Recommandation_CountUsageInStaging(idRecommandation As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_CountUsageInStaging, conn)

                    cmd.Parameters.AddWithValue("@id_recommandation", idRecommandation)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Recommandation_CountUsageInStaging.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' OrigineRecommandation_DeleteWithRecommandations
    '
    '   • Supprime une origine + ses recommandations
    '------------------------------------------------------------
    Public Sub OrigineRecommandation_DeleteWithRecommandations(idOrigine As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using trans = conn.BeginTransaction()

                    Try

                        ' Suppression des recommandations
                        Using cmdRec As New MySqlConnector.MySqlCommand(QueryModule.Recommandation_DeleteByOrigine, conn, trans)
                            cmdRec.Parameters.AddWithValue("@id_origine_recommandation", idOrigine)
                            cmdRec.ExecuteNonQuery()
                        End Using

                        ' Suppression de l'origine
                        Using cmdOrig As New MySqlConnector.MySqlCommand(QueryModule.RefOrigineRecommandation_Delete, conn, trans)
                            cmdOrig.Parameters.AddWithValue("@id_origine_recommandation", idOrigine)
                            cmdOrig.ExecuteNonQuery()
                        End Using

                        trans.Commit()

                    Catch exTrans As Exception

                        trans.Rollback()

                        GestionLog.EcrireLog("REF: rollback suppression origine + recommandations", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, exTrans)

                        Throw

                    End Try

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog("Database: erreur OrigineRecommandation_DeleteWithRecommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw

        End Try

    End Sub

#End Region



End Module
