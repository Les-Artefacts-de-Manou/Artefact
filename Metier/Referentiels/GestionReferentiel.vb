'------------------------------------------------------------
' 📌 GestionReferentiel.vb - Module
' Version : V1.3
' Date    : 17/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Module métier des formulaires de référentiels.
' Centralise les opérations de lecture, recherche, insertion,
' modification, contrôle d’usage et suppression.
'
' Règles Artefact :
' - Pas d'UI ici.
' - Requêtes SQL centralisées dans QueryModule.
' - Exécution DB via DatabaseManager.
'
' Évolution :
' - V1.0 : Implémentation initiale des référentiels.
' - V1.1 : Ajout progressif des nouveaux référentiels.
' - V1.2 : Remise en ordre des procédures/fonctions par région
'          et homogénéisation des en-têtes.
' - V1.3 : Ajout CRUD ref_origine_recommandations.
'------------------------------------------------------------

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

                    cmd.Parameters.AddWithValue("@s", InputHelpers.BuildContainsSearchValue(search))

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

                    cmd.Parameters.AddWithValue("@nom", InputHelpers.NormalizeText(langue.NomLangue))
                    cmd.Parameters.AddWithValue("@abrev", DbValueOrNullUpper(langue.AbrevLangue))
                    cmd.Parameters.AddWithValue("@iso1", DbValueOrNullLower(langue.Iso639_1))
                    cmd.Parameters.AddWithValue("@iso2", DbValueOrNullLower(langue.Iso639_2))

                    cmd.ExecuteNonQuery()
                End Using

                ' 2) Récupération de l'ID inséré
                ' NOTE :
                ' - LAST_INSERT_ID() n'est pas toujours fiable avec des SEQUENCE/DEFAULT selon setup.
                ' - Ici on re-sélectionne via contraintes uniques (nom + abre sont uniques).
                Using cmdId As New MySqlCommand(QueryModule.Langues_SelectIdByNomAbrev, conn)

                    cmdId.Parameters.AddWithValue("@nom", InputHelpers.NormalizeText(langue.NomLangue))
                    cmdId.Parameters.AddWithValue("@abrev", InputHelpers.NormalizeText(langue.AbrevLangue))

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
                    cmd.Parameters.AddWithValue("@nom", InputHelpers.NormalizeText(langue.NomLangue))
                    cmd.Parameters.AddWithValue("@abrev", InputHelpers.NormalizeText(langue.AbrevLangue))
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
        Return InputHelpers.ToDbNullUpper(value)
    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' DbValueOrNullLower
    '
    '   • Retourne DBNull.Value si la chaîne est vide
    '   • Sinon retourne la chaîne en minuscules
    '------------------------------------------------------------
    Private Function DbValueOrNullLower(value As String) As Object
        Return InputHelpers.ToDbNullLower(value)
    End Function

    Private Function DbValueOrNull(value As String) As Object
        Return InputHelpers.ToDbNullIfWhiteSpace(value)
    End Function

#End Region


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

                    cmd.Parameters.AddWithValue("@s", InputHelpers.BuildContainsSearchValue(search))

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

                    cmd.Parameters.AddWithValue("@nom", InputHelpers.NormalizeText(p.NomPays))
                    cmd.Parameters.AddWithValue("@iso2", DbValueOrNullUpper(p.Iso2))
                    cmd.Parameters.AddWithValue("@iso3", DbValueOrNullUpper(p.Iso3))

                    cmd.ExecuteNonQuery()
                End Using

                Using cmdId As New MySqlConnector.MySqlCommand(QueryModule.Pays_SelectIdByNom, conn)
                    cmdId.Parameters.AddWithValue("@nom", InputHelpers.NormalizeText(p.NomPays))

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
                    cmd.Parameters.AddWithValue("@nom", InputHelpers.NormalizeText(p.NomPays))
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
