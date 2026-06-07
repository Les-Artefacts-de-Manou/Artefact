Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "REF_ENUM (VALEURS) - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_GetByType
    '
    '   • Retourne les valeurs enfants d'un type parent
    '   • Peut filtrer sur les valeurs actives uniquement
    '------------------------------------------------------------
    Public Function RefEnum_GetByType(idEnumType As ULong, actifsOnly As Boolean) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String = If(actifsOnly,
                                   QueryModule.RefEnum_SelectByType_ActifsOnly,
                                   QueryModule.RefEnum_SelectByType)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@id_enum_type", idEnumType)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_GetByType.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_GetByTypeAndSearch
    '
    '   • Recherche les valeurs d'un type parent par code ou libellé
    '------------------------------------------------------------
    Public Function RefEnum_GetByTypeAndSearch(idEnumType As ULong, search As String, actifsOnly As Boolean) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String = If(actifsOnly,
                                   QueryModule.RefEnum_SelectByTypeAndSearch_ActifsOnly,
                                   QueryModule.RefEnum_SelectByTypeAndSearch)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@id_enum_type", idEnumType)
                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_GetByTypeAndSearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_Insert
    '
    '   • Insère une nouvelle valeur enfant dans ref_enum
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function RefEnum_Insert(v As RefEnumValeur) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_Insert, conn)

                    cmd.Parameters.AddWithValue("@id_enum_type", v.IdEnumType)
                    cmd.Parameters.AddWithValue("@code_valeur", UCase(v.CodeValeur.Trim()))
                    cmd.Parameters.AddWithValue("@libelle_valeur", v.LibelleValeur.Trim())
                    cmd.Parameters.AddWithValue("@ordre", v.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@actif", If(v.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_SelectIdByTypeAndCodeValeur, conn)
                    cmdId.Parameters.AddWithValue("@id_enum_type", v.IdEnumType)
                    cmdId.Parameters.AddWithValue("@code_valeur", UCase(v.CodeValeur.Trim()))

                    Dim obj = cmdId.ExecuteScalar()
                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID de la valeur d'énumération après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_Update
    '
    '   • Met à jour une valeur enfant existante
    '------------------------------------------------------------
    Public Sub RefEnum_Update(v As RefEnumValeur)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_Update, conn)

                    cmd.Parameters.AddWithValue("@id", v.IdEnum)
                    cmd.Parameters.AddWithValue("@code_valeur", UCase(v.CodeValeur.Trim()))
                    cmd.Parameters.AddWithValue("@libelle_valeur", v.LibelleValeur.Trim())
                    cmd.Parameters.AddWithValue("@ordre", v.OrdreAffichage)
                    cmd.Parameters.AddWithValue("@actif", If(v.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountByType
    '
    '   • Compte le nombre de valeurs enfants rattachées à un type parent
    '------------------------------------------------------------
    Public Function RefEnum_CountByType(idEnumType As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountByType, conn)
                    cmd.Parameters.AddWithValue("@id_enum_type", idEnumType)
                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountByType.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInAuteursPaysTypeRelation
    '
    '   • Compte les usages d'une valeur enum dans auteurs_pays.type_relation
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInAuteursPaysTypeRelation(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInAuteursPaysTypeRelation, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInAuteursPaysTypeRelation.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresAuteursRole
    '
    '   • Compte les usages d'une valeur enum dans livres_auteurs.role
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresAuteursRole(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresAuteursRole, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresAuteursRole.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresFichiersScope
    '
    '   • Compte les usages d'une valeur enum dans livres_fichiers.scope_fichier
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresFichiersScope(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresFichiersScope, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresFichiersScope.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresFichiersType
    '
    '   • Compte les usages d'une valeur enum dans livres_fichiers.type_fichier
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresFichiersType(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresFichiersType, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresFichiersType.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresStagingSourceImport
    '
    '   • Compte les usages d'une valeur enum dans livres_staging.source_import
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresStagingSourceImport(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresStagingSourceImport, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresStagingSourceImport.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresStagingStatut
    '
    '   • Compte les usages d'une valeur enum dans livres_staging.statut_traitement
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresStagingStatut(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresStagingStatut, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresStagingStatut.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresStagingAuteursRole
    '
    '   • Compte les usages d'une valeur enum dans livres_staging_auteurs.role
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresStagingAuteursRole(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresStagingAuteursRole, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresStagingAuteursRole.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresStatutLecture
    '
    '   • Compte les usages d'une valeur enum dans livres.statut_lecture
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresStatutLecture(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresStatutLecture, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresStatutLecture.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresSupportLecture
    '
    '   • Compte les usages d'une valeur enum dans livres.support_lecture
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresSupportLecture(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresSupportLecture, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresSupportLecture.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_CountUsageInLivresTypeAcquisition
    '
    '   • Compte les usages d'une valeur enum dans livres.type_acquisition
    '------------------------------------------------------------
    Public Function RefEnum_CountUsageInLivresTypeAcquisition(idEnum As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_CountUsageInLivresTypeAcquisition, conn)

                    cmd.Parameters.AddWithValue("@id_enum", idEnum)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_CountUsageInLivresTypeAcquisition.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_Delete
    '
    '   • Supprime une valeur enfant via id_enum
    '------------------------------------------------------------
    Public Sub RefEnum_Delete(idEnum As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_Delete, conn)

                    cmd.Parameters.AddWithValue("@id", idEnum)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur RefEnum_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnum_DeleteByType
    '
    '   • Supprime toutes les valeurs enfants d'un type parent
    '------------------------------------------------------------
    Public Sub RefEnum_DeleteByType(idEnumType As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_Delete(), conn)

                    cmd.Parameters.AddWithValue("@id_enum_type", idEnumType)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog(
                $"REF: erreur lors de la suppression des valeurs liées au type {idEnumType}.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' RefEnumType_DeleteWithValues
    '
    '   • Supprime un type parent et toutes ses valeurs enfants liées
    '------------------------------------------------------------
    Public Sub RefEnumType_DeleteWithValues(idEnumType As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using trans = conn.BeginTransaction()

                    Try

                        ' suppression des valeurs
                        Using cmdValeurs As New MySqlConnector.MySqlCommand(QueryModule.RefEnum_DeleteByType(), conn, trans)
                            cmdValeurs.Parameters.AddWithValue("@id_enum_type", idEnumType)
                            cmdValeurs.ExecuteNonQuery()
                        End Using

                        ' suppression du type
                        Using cmdType As New MySqlConnector.MySqlCommand(QueryModule.RefEnumType_Delete(), conn, trans)
                            cmdType.Parameters.AddWithValue("@id_enum_type", idEnumType)
                            cmdType.ExecuteNonQuery()
                        End Using

                        trans.Commit()

                    Catch exTrans As Exception

                        trans.Rollback()

                        GestionLog.EcrireLog(
                            $"REF: rollback suppression type enum {idEnumType}",
                            GestionLog.LogLevel.Succinct,
                            GestionLog.LogCategory.Database,
                            exTrans
                        )

                        Throw
                    End Try
                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
                $"REF: erreur suppression complète type enum {idEnumType}",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )
            Throw

        End Try

    End Sub

#End Region



End Module
