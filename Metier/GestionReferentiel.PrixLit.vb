Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "PRIXLIT - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_GetAll
    '
    '   • Retourne tous les prix littéraires
    '   • Peut filtrer sur les actifs
    '------------------------------------------------------------
    Public Function PrixLit_GetAll(actifsOnly As Boolean) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String = If(actifsOnly,
                                   QueryModule.PrixLit_SelectAll_ActifsOnly,
                                   QueryModule.PrixLit_SelectAll)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)
                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_GetAll.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V2.0 - 25/03/2026
    ' PrixLit_GetBySearch
    '
    '   • Recherche unifiée utilisant la VIEW v_prixlit_recherche
    '   • Cherche dans : nom, description, catégories, années
    '   • Peut inclure ou non les notes
    '   • Peut filtrer sur les actifs
    '------------------------------------------------------------
    Public Function PrixLit_GetBySearch(searchText As String,
                                        actifsOnly As Boolean,
                                        Optional includeNotes As Boolean = False) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                ' Construction de la requête sur la VIEW
                Dim sql As String = "
                    SELECT id_prixlit, nom_prixlit, description_prixlit, 
                           is_Actif, code_prixlit, created_at, updated_at
                    FROM v_prixlit_recherche
                    WHERE (
                        nom_prixlit LIKE @search 
                        OR description_prixlit LIKE @search
                        OR categories LIKE @search
                        OR annees LIKE @search"

                If includeNotes Then
                    sql &= " OR notes_prixlit_txt LIKE @search"
                End If

                sql &= ")"

                If actifsOnly Then
                    sql &= " AND is_Actif = 1"
                End If

                sql &= " ORDER BY nom_prixlit"

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
            GestionLog.EcrireLog("Database: erreur PrixLit_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_Insert
    '
    '   • Insère un prix littéraire
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function PrixLit_Insert(p As PrixLit) As ULong

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_prixLit", p.NomPrixLit.Trim())
                    cmd.Parameters.AddWithValue("@description_prixLit", If(String.IsNullOrWhiteSpace(p.DescriptionPrixLit), DBNull.Value, p.DescriptionPrixLit.Trim()))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_rtf", If(String.IsNullOrWhiteSpace(p.NotesPrixLitRtf), DBNull.Value, p.NotesPrixLitRtf))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_txt", If(String.IsNullOrWhiteSpace(p.NotesPrixLitTxt), DBNull.Value, p.NotesPrixLitTxt))
                    cmd.Parameters.AddWithValue("@is_actif", If(p.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using

                Using cmdId As New MySqlConnector.MySqlCommand("SELECT LAST_INSERT_ID();", conn)

                    Dim obj = cmdId.ExecuteScalar()

                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID du prix littéraire après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)

                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_Update
    '
    '   • Met à jour un prix littéraire existant
    '------------------------------------------------------------
    Public Sub PrixLit_Update(p As PrixLit)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_Update, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", p.IdPrixLit)
                    cmd.Parameters.AddWithValue("@nom_prixLit", p.NomPrixLit.Trim())
                    cmd.Parameters.AddWithValue("@description_prixLit", If(String.IsNullOrWhiteSpace(p.DescriptionPrixLit), DBNull.Value, p.DescriptionPrixLit.Trim()))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_rtf", If(String.IsNullOrWhiteSpace(p.NotesPrixLitRtf), DBNull.Value, p.NotesPrixLitRtf))
                    cmd.Parameters.AddWithValue("@Notes_PrixLit_txt", If(String.IsNullOrWhiteSpace(p.NotesPrixLitTxt), DBNull.Value, p.NotesPrixLitTxt))
                    cmd.Parameters.AddWithValue("@is_actif", If(p.IsActif, 1, 0))

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_Delete
    '
    '   • Supprime un prix littéraire
    '------------------------------------------------------------
    Public Sub PrixLit_Delete(idPrixLit As ULong)

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)
                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_CountCategories
    '
    '   • Compte les catégories liées à un prix littéraire
    '------------------------------------------------------------
    Public Function PrixLit_CountCategories(idPrixLit As ULong) As Integer

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.PrixLit_CountCategories, conn)

                    cmd.Parameters.AddWithValue("@id_prixLit", idPrixLit)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then Return 0

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur PrixLit_CountCategories.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

#End Region



End Module
