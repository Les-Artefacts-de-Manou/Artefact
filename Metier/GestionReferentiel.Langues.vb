Imports System.Data
Imports MySqlConnector

Partial Module GestionReferentiel

#Region "LANGUES - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Langues_GetAll
    '
    '   • Retourne la liste complète des langues
    '   • Alimente les grilles des formulaires de référence
    '------------------------------------------------------------
    Public Function Langues_GetAll() As DataTable

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlCommand(QueryModule.Langues_SelectAll, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Langues_GetAll.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Langues_GetBySearch
    '
    '   • Recherche les langues par nom, abréviation ou code ISO
    '   • Applique le paramètre SQL @s au format '%texte%'
    '------------------------------------------------------------
    Public Function Langues_GetBySearch(search As String) As DataTable

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlCommand(QueryModule.Langues_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

                    Using da As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Langues_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Langues_Insert
    '
    '   • Insère une nouvelle langue
    '   • Retourne l'identifiant créé après réinterrogation
    '------------------------------------------------------------
    Public Function Langues_Insert(langue As Langue) As ULong

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()

                ' 1) Insert
                Using cmd As New MySqlCommand(QueryModule.Langues_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom", langue.NomLangue.Trim())
                    cmd.Parameters.AddWithValue("@abrev", DbValueOrNullUpper(langue.AbrevLangue.Trim()))
                    cmd.Parameters.AddWithValue("@iso1", DbValueOrNullLower(langue.Iso639_1))
                    cmd.Parameters.AddWithValue("@iso2", DbValueOrNullLower(langue.Iso639_2))

                    cmd.ExecuteNonQuery()
                End Using

                ' 2) Récupération de l'ID inséré
                ' NOTE :
                ' - LAST_INSERT_ID() n'est pas toujours fiable avec des SEQUENCE/DEFAULT selon setup.
                ' - Ici on re-sélectionne via contraintes uniques (nom + abre sont uniques).
                Using cmdId As New MySqlCommand(QueryModule.Langues_SelectIdByNomAbrev, conn)

                    cmdId.Parameters.AddWithValue("@nom", langue.NomLangue.Trim())
                    cmdId.Parameters.AddWithValue("@abrev", langue.AbrevLangue.Trim())

                    Dim obj = cmdId.ExecuteScalar()
                    If obj Is Nothing OrElse obj Is DBNull.Value Then
                        Throw New Exception("Impossible de récupérer l'ID de la langue après insertion.")
                    End If

                    Return Convert.ToUInt64(obj)
                End Using

            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Langues_Insert.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Langues_Update
    '
    '   • Met à jour une langue existante via son identifiant
    '------------------------------------------------------------
    Public Sub Langues_Update(langue As Langue)

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlCommand(QueryModule.Langues_Update, conn)

                    cmd.Parameters.AddWithValue("@id", langue.IdLangue)
                    cmd.Parameters.AddWithValue("@nom", langue.NomLangue.Trim())
                    cmd.Parameters.AddWithValue("@abrev", langue.AbrevLangue.Trim())
                    cmd.Parameters.AddWithValue("@iso1", DbValueOrNullLower(langue.Iso639_1))
                    cmd.Parameters.AddWithValue("@iso2", DbValueOrNullLower(langue.Iso639_2))

                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Langues_Update.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Langues_CountUsageInAuteurs
    '
    '   • Compte les usages d'une langue dans les auteurs
    '------------------------------------------------------------
    Public Function Langues_CountUsageInAuteurs(idLangue As ULong) As Integer

        Try
            Dim total As Integer = 0

            Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()

                Using cmdAuteurs As New MySqlCommand(QueryModule.Langues_CountUsageInAuteurs, conn)
                    cmdAuteurs.Parameters.AddWithValue("@id", idLangue)
                    total += Convert.ToInt32(cmdAuteurs.ExecuteScalar())
                End Using

            End Using

            Return total

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Langues_CountUsageInAuteurs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Langues_CountUsageInLivres
    '
    '   • Compte les usages d'une langue dans les livres
    '------------------------------------------------------------
    Public Function Langues_CountUsageInLivres(idLangue As ULong) As Integer

        Try
            Dim total As Integer = 0

            Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()

                Using cmdLivres As New MySqlCommand(QueryModule.Langues_CountUsageInLivres, conn)
                    cmdLivres.Parameters.AddWithValue("@id", idLangue)
                    total += Convert.ToInt32(cmdLivres.ExecuteScalar())
                End Using

            End Using

            Return total

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Langues_CountUsageInLivres.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Langues_Delete
    '
    '   • Supprime une langue via id_langue
    '------------------------------------------------------------
    Public Sub Langues_Delete(idLangue As ULong)

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlCommand(QueryModule.Langues_Delete, conn)
                    cmd.Parameters.AddWithValue("@id", idLangue)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Langues_Delete.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' DbValueOrNullUpper
    '
    '   • Retourne DBNull.Value si la chaîne est vide
    '   • Sinon retourne la chaîne en majuscules
    '------------------------------------------------------------
    Private Function DbValueOrNullUpper(value As String) As Object

        Dim t As String = If(value, "").Trim()
        If t = "" Then Return DBNull.Value
        Return t.ToUpperInvariant()

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' DbValueOrNullLower
    '
    '   • Retourne DBNull.Value si la chaîne est vide
    '   • Sinon retourne la chaîne en minuscules
    '------------------------------------------------------------
    Private Function DbValueOrNullLower(value As String) As Object
        Dim t As String = If(value, "").Trim()
        If t = "" Then Return DBNull.Value
        Return t.ToLowerInvariant()
    End Function

#End Region



End Module
