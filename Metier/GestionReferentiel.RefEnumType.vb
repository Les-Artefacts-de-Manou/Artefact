Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "REF_ENUM_TYPE - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnumType_GetAll
    '
    '   • Retourne la liste complète des types d'énumération parents
    '------------------------------------------------------------
    Public Function RefEnumType_GetAll() As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_SelectAll, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnumType_GetAll.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnumType_GetActifs
    '
    '   • Retourne uniquement les types d'énumération actifs
    '------------------------------------------------------------
    Public Function RefEnumType_GetActifs() As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_SelectActifs, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnumType_GetActifs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnumType_GetBySearch
    '
    '   • Recherche les types d'énumération par code ou libellé
    '------------------------------------------------------------
    Public Function RefEnumType_GetBySearch(search As String) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnumType_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnumType_Insert
    '
    '   • Insère un nouveau type parent ref_enum_type
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function RefEnumType_Insert(t As RefEnumType) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_Insert, conn)

                    cmd.Parameters.AddWithValue("@code_type", UCase(t.CodeType.Trim()))
                    cmd.Parameters.AddWithValue("@libelle_type", t.LibelleType.Trim())
                    cmd.Parameters.AddWithValue("@ordre", t.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@actif", If(t.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_SelectIdByCodeType, conn)
                    cmdId.Parameters.AddWithValue("@code_type", UCase(t.CodeType.Trim()))

                    Dim obj = cmdId.ExecuteScalar()
                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID du type d'énumération après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnumType_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnumType_Update
    '
    '   • Met à jour un type parent existant
    '------------------------------------------------------------
    Public Sub RefEnumType_Update(t As RefEnumType)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_Update, conn)

                    cmd.Parameters.AddWithValue("@id", t.IdEnumType)
                    cmd.Parameters.AddWithValue("@code_type", UCase(t.CodeType.Trim()))
                    cmd.Parameters.AddWithValue("@libelle_type", t.LibelleType.Trim())
                    cmd.Parameters.AddWithValue("@ordre", t.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@actif", If(t.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnumType_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnumType_Delete
    '
    '   • Supprime un type parent via id_enum_type
    '------------------------------------------------------------
    Public Sub RefEnumType_Delete(idEnumType As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_enum_type", idEnumType)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnumType_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

#End Region



End Module
