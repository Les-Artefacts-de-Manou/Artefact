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

Module GestionReferentiel


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


#Region "CONTACTS - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_GetAll
    '
    '   • Retourne la liste complète des contacts
    '------------------------------------------------------------
    Public Function Contact_GetAll() As DataTable

        Dim dt As New DataTable()

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "REF: erreur chargement contacts.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

        Return dt

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_GetBySearch
    '
    '   • Recherche les contacts par nom ou informations associées
    '   • Peut inclure les notes selon le paramètre fourni
    '------------------------------------------------------------
    Public Function Contact_GetBySearch(search As String) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@s", "%" & search.Trim() & "%")

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            GestionLog.EcrireLog("Database: erreur Contact_GetBySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_ExistsByNom
    '
    '   • Vérifie si un contact existe déjà pour un nom donné
    '------------------------------------------------------------
    Public Function Contact_ExistsByNom(nom As String) As Boolean

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_ExistsByNom, conn)

                    cmd.Parameters.AddWithValue("@nom_contact", nom)

                    Dim result = Convert.ToInt32(cmd.ExecuteScalar())

                    Return result > 0

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "REF: erreur vérification nom contact.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_ExistsByNomExceptId
    '
    '   • Vérifie l'existence d'un nom de contact en excluant un identifiant
    '------------------------------------------------------------
    Public Function Contact_ExistsByNomExceptId(nom As String, idContact As ULong) As Boolean

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_ExistsByNomExceptId, conn)

                    cmd.Parameters.AddWithValue("@nom_contact", nom)
                    cmd.Parameters.AddWithValue("@id_contact", idContact)

                    Dim result = Convert.ToInt32(cmd.ExecuteScalar())

                    Return result > 0

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "REF: erreur vérification unicité nom contact.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_Insert
    '
    '   • Insère un nouveau contact
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Sub Contact_Insert(c As Contact)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_contact", c.NomContact)
                    cmd.Parameters.AddWithValue("@email_perso", c.EmailPerso)
                    cmd.Parameters.AddWithValue("@adresse_liseuse", c.AdresseLiseuse)
                    cmd.Parameters.AddWithValue("@type_liseuse", c.TypeLiseuse)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur insertion contact {c.NomContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_Update
    '
    '   • Met à jour un contact existant
    '------------------------------------------------------------
    Public Sub Contact_Update(c As Contact)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_Update, conn)

                    cmd.Parameters.AddWithValue("@id_contact", c.IdContact)
                    cmd.Parameters.AddWithValue("@nom_contact", c.NomContact)
                    cmd.Parameters.AddWithValue("@email_perso", c.EmailPerso)
                    cmd.Parameters.AddWithValue("@adresse_liseuse", c.AdresseLiseuse)
                    cmd.Parameters.AddWithValue("@type_liseuse", c.TypeLiseuse)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur update contact id={c.IdContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_CountUsage
    '
    '   • Compte les usages d'un contact dans les tables métier concernées
    '------------------------------------------------------------
    Public Function Contact_CountUsage(idContact As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_CountUsageInLivresContacts, conn)

                    cmd.Parameters.AddWithValue("@id_contact", idContact)

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur vérification usage contact id={idContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Contact_Delete
    '
    '   • Supprime un contact via id_contact
    '------------------------------------------------------------
    Public Sub Contact_Delete(idContact As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Contact_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_contact", idContact)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"REF: erreur suppression contact id={idContact}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

#End Region


#Region "EDITEURS - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_GetAll
    '
    '   • Retourne la liste complète des éditeurs
    '------------------------------------------------------------
    Public Function Editeurs_GetAll() As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
                "Database: erreur Editeurs_GetAll.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_GetBySearch
    '
    '   • Recherche les éditeurs par nom, site web ou notes
    '   • Peut inclure les notes selon le paramètre fourni
    '------------------------------------------------------------
    Public Function Editeurs_GetBySearch(searchText As String,
                                     Optional includeNotes As Boolean = False) As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String =
                If(includeNotes,
                   QueryModule.Editeurs_SelectBySearchIncludingNotes,
                   QueryModule.Editeurs_SelectBySearch)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@search", searchText)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Editeurs_GetBySearch.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_Insert
    '
    '   • Insère un nouvel éditeur
    '   • Retourne l'identifiant créé
    '------------------------------------------------------------
    Public Function Editeurs_Insert(editeur As Editeur) As ULong

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_Insert & "
SELECT LAST_INSERT_ID();", conn)

                    cmd.Parameters.AddWithValue("@nom_editeur", editeur.NomEditeur)

                    If editeur.IdPays.HasValue Then
                        cmd.Parameters.AddWithValue("@id_pays", editeur.IdPays.Value)
                    Else
                        cmd.Parameters.AddWithValue("@id_pays", DBNull.Value)
                    End If

                    cmd.Parameters.AddWithValue("@site_web", editeur.SiteWeb)
                    cmd.Parameters.AddWithValue("@notes_editeur_rtf", editeur.NotesEditeurRtf)
                    cmd.Parameters.AddWithValue("@notes_editeur_txt", editeur.NotesEditeurTxt)

                    Dim result = cmd.ExecuteScalar()

                    Return Convert.ToUInt64(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Editeurs_Insert.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.3 - 19/03/2026
    ' Editeurs_Update
    '
    '   • Met à jour un éditeur existant
    '------------------------------------------------------------
    Public Sub Editeurs_Update(editeur As Editeur)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_Update, conn)

                    cmd.Parameters.AddWithValue("@id_editeur", editeur.IdEditeur)
                    cmd.Parameters.AddWithValue("@nom_editeur", editeur.NomEditeur)

                    If editeur.IdPays.HasValue Then
                        cmd.Parameters.AddWithValue("@id_pays", editeur.IdPays.Value)
                    Else
                        cmd.Parameters.AddWithValue("@id_pays", DBNull.Value)
                    End If

                    cmd.Parameters.AddWithValue("@site_web", editeur.SiteWeb)
                    cmd.Parameters.AddWithValue("@notes_editeur_rtf", editeur.NotesEditeurRtf)
                    cmd.Parameters.AddWithValue("@notes_editeur_txt", editeur.NotesEditeurTxt)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Editeurs_Update.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_CountLivres
    '
    '   • Compte le nombre de livres liés à un éditeur
    '------------------------------------------------------------
    Public Function Editeurs_CountLivres(idEditeur As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_CountLivres, conn)

                    cmd.Parameters.AddWithValue("@id_editeur", idEditeur)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
                "Database: erreur Editeurs_CountLivres.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )

            Throw

        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
    ' Editeurs_Delete
    '
    '   • Supprime un éditeur via id_editeur
    '   • La FK livres.id_editeur reste en SET NULL
    '------------------------------------------------------------
    Public Sub Editeurs_Delete(idEditeur As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Editeurs_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_editeur", idEditeur)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
                "Database: erreur Editeurs_Delete.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )

            Throw

        End Try

    End Sub

#End Region


#Region "FORMATFILE - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_GetAll
    '
    ' Retourne tous les formats de fichier.
    '------------------------------------------------------------
    Public Function FormatFile_GetAll() As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_GetAll.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_GetBySearch
    '
    ' Recherche sur nom_format, extension ou mime_type.
    '------------------------------------------------------------
    Public Function FormatFile_GetBySearch(searchText As String) As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_SelectBySearch, conn)

                    cmd.Parameters.AddWithValue("@search", searchText)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_GetBySearch.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Insert
    '
    ' Ajoute un nouveau format de fichier.
    '------------------------------------------------------------
    Public Sub FormatFile_Insert(
    nomFormat As String,
    extension As String,
    mimeType As String,
    ordreAffichage As Integer,
    isActif As Boolean)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_format", nomFormat)
                    cmd.Parameters.AddWithValue("@extension", extension)
                    cmd.Parameters.AddWithValue("@mime_type", mimeType)
                    cmd.Parameters.AddWithValue("@ordre_affichage", ordreAffichage)
                    cmd.Parameters.AddWithValue("@is_actif", isActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_Insert.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Update
    '
    ' Met à jour un format de fichier existant.
    '------------------------------------------------------------
    Public Sub FormatFile_Update(
    idFormatFile As ULong,
    nomFormat As String,
    extension As String,
    mimeType As String,
    ordreAffichage As Integer,
    isActif As Boolean)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_Update, conn)

                    cmd.Parameters.AddWithValue("@id_formatFile", idFormatFile)
                    cmd.Parameters.AddWithValue("@nom_format", nomFormat)
                    cmd.Parameters.AddWithValue("@extension", extension)
                    cmd.Parameters.AddWithValue("@mime_type", mimeType)
                    cmd.Parameters.AddWithValue("@ordre_affichage", ordreAffichage)
                    cmd.Parameters.AddWithValue("@is_actif", isActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_Update.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Delete
    '
    ' Supprime un format de fichier.
    '------------------------------------------------------------
    Public Sub FormatFile_Delete(idFormatFile As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_formatFile", idFormatFile)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_Delete.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_CountLivresFichiers
    '
    ' Compte les références dans livres_fichiers.
    '------------------------------------------------------------
    Public Function FormatFile_CountLivresFichiers(idFormatFile As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.FormatFile_CountLivresFichiers, conn)

                    cmd.Parameters.AddWithValue("@id_formatFile", idFormatFile)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur FormatFile_CountLivresFichiers.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

#End Region


#Region "IMPRESSION - CRUD"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_GetAll
    '
    ' Retourne la liste complète des impressions.
    '------------------------------------------------------------
    Public Function Impression_GetAll() As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_SelectAll, conn)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_GetAll.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.1 - 14/03/2026
    ' Impression_GetBySearch
    '
    ' Recherche d'impressions par texte.
    ' Peut inclure ou non les notes.
    '------------------------------------------------------------
    Public Function Impression_GetBySearch(searchText As String,
                                       Optional includeNotes As Boolean = False) As DataTable

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String =
                If(includeNotes,
                   QueryModule.Impression_SelectBySearchIncludingNotes,
                   QueryModule.Impression_SelectBySearch)

                Using cmd As New MySqlConnector.MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@search", searchText)

                    Using da As New MySqlConnector.MySqlDataAdapter(cmd)

                        Dim dt As New DataTable
                        da.Fill(dt)

                        Return dt

                    End Using
                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_GetBySearch.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Insert
    '
    ' Ajoute une nouvelle impression.
    '------------------------------------------------------------
    Public Sub Impression_Insert(impression As Impression)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_Insert, conn)

                    cmd.Parameters.AddWithValue("@nom_impression", impression.NomImpression)
                    cmd.Parameters.AddWithValue("@description_impression", impression.DescriptionImpression)
                    cmd.Parameters.AddWithValue("@note_rtf", impression.NoteRtf)
                    cmd.Parameters.AddWithValue("@note_txt", impression.NoteTxt)
                    cmd.Parameters.AddWithValue("@envie_Cal", impression.EnvieCal)
                    cmd.Parameters.AddWithValue("@is_actif", impression.IsActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_Insert.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Update
    '
    ' Met à jour une impression existante.
    '------------------------------------------------------------
    Public Sub Impression_Update(impression As Impression)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_Update, conn)

                    cmd.Parameters.AddWithValue("@id_impression", impression.IdImpression)
                    cmd.Parameters.AddWithValue("@nom_impression", impression.NomImpression)
                    cmd.Parameters.AddWithValue("@description_impression", impression.DescriptionImpression)
                    cmd.Parameters.AddWithValue("@note_rtf", impression.NoteRtf)
                    cmd.Parameters.AddWithValue("@note_txt", impression.NoteTxt)
                    cmd.Parameters.AddWithValue("@envie_Cal", impression.EnvieCal)
                    cmd.Parameters.AddWithValue("@is_actif", impression.IsActif)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_Update.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Delete
    '
    ' Supprime une impression via id_impression.
    '------------------------------------------------------------
    Public Sub Impression_Delete(idImpression As ULong)

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_Delete, conn)

                    cmd.Parameters.AddWithValue("@id_impression", idImpression)

                    cmd.ExecuteNonQuery()

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_Delete.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_CountLivres
    '
    ' Compte les livres liés à cette impression.
    '------------------------------------------------------------
    Public Function Impression_CountLivres(idImpression As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_CountLivres, conn)

                    cmd.Parameters.AddWithValue("@id_impression", idImpression)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_CountLivres.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_CountLivresStaging
    '
    ' Compte les livres_staging liés à cette impression.
    '------------------------------------------------------------
    Public Function Impression_CountLivresStaging(idImpression As ULong) As Integer

        Try

            Using conn = DatabaseManager.GetConnexionMariaDB()
                Using cmd As New MySqlConnector.MySqlCommand(QueryModule.Impression_CountLivresStaging, conn)

                    cmd.Parameters.AddWithValue("@id_impression", idImpression)

                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return 0
                    End If

                    Return Convert.ToInt32(result)

                End Using
            End Using

        Catch ex As Exception

            GestionLog.EcrireLog(
            "Database: erreur Impression_CountLivresStaging.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            Throw

        End Try

    End Function

#End Region


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
    ' 📌 V1.0 - 22/03/2026
    ' PrixLit_GetBySearch
    '
    '   • Recherche globale des prix littéraires
    '   • Peut inclure ou non les notes
    '   • Peut filtrer sur les actifs
    '------------------------------------------------------------
    Public Function PrixLit_GetBySearch(searchText As String,
                                        actifsOnly As Boolean,
                                        Optional includeNotes As Boolean = False) As DataTable

        Try
            Using conn = DatabaseManager.GetConnexionMariaDB()

                Dim sql As String

                If actifsOnly Then
                    sql = If(includeNotes,
                         QueryModule.PrixLit_SelectBySearchIncludingNotes_ActifsOnly,
                         QueryModule.PrixLit_SelectBySearch_ActifsOnly)
                Else
                    sql = If(includeNotes,
                         QueryModule.PrixLit_SelectBySearchIncludingNotes,
                         QueryModule.PrixLit_SelectBySearch)
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
