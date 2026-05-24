'------------------------------------------------------------
' 📌 DatabaseManager.vb - Classe
' Version : V2.1
' Date    : 25/02/2026
' Auteur  : Joëlle
'
' Rôle :
' Gestion centralisée de la connexion MariaDB (Artefact).
' La connexion est construite exclusivement à partir
' de la configuration locale (artefact.local.json).
'
'------------------------------------------------------------

Option Strict On
Option Infer On
Option Explicit On

Imports System.Collections.Generic
Imports MySqlConnector

Public Class DatabaseManager

#Region "Variables / Constantes"

    Private Shared _connectionString As String = String.Empty
    Private Shared _isInitialized As Boolean = False
    Private Shared ReadOnly AllowedConnectionOptionKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        "connectiontimeout",
        "defaultcommandtimeout",
        "keepalive",
        "allowuservariables",
        "allowloadlocalinfile",
        "treattinyasboolean",
        "usecompression",
        "connectionreset",
        "maximumpoolsize",
        "minimumpoolsize"
    }

#End Region

#Region "Constructeur"

    Private Sub New()
        ' Empêche l’instanciation
    End Sub

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V2.1 - 25/02/2026
    ' InitializeMariaDb
    '
    ' Construit la connection string à partir
    ' de la configuration locale JSON.
    ' Logs PROD : niveau + catégorie + exception si erreur.
    '------------------------------------------------------------
    Public Shared Sub InitializeMariaDb()

        If _isInitialized Then Exit Sub

        Try
            Dim cfg = ConfigLocalManager.LireConfigDb()
            _connectionString = BuildConnectionString(cfg)

            If String.IsNullOrWhiteSpace(_connectionString) Then
                Throw New Exception("ConnectionString vide.")
            End If

            _isInitialized = True
            GestionLog.EcrireLog("✅ MariaDB: initialisation OK.")

        Catch ex As Exception
            _isInitialized = False
            _connectionString = String.Empty
            GestionLog.EcrireLog("MariaDB: échec initialisation.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Sub

#End Region

#Region "Connexion"

    '------------------------------------------------------------
    ' 📌 V2.1 - 25/02/2026
    ' GetConnexionMariaDB
    '
    ' Retourne une connexion MySqlConnection ouverte.
    ' Logs PROD : niveau + catégorie + exception si erreur.
    '------------------------------------------------------------
    Public Shared Function GetConnexionMariaDB() As MySqlConnection

        If Not _isInitialized Then
            InitializeMariaDb()
        End If

        Dim conn As New MySqlConnection(_connectionString)

        Try
            conn.Open()
            Return conn

        Catch ex As Exception
            conn.Dispose()
            GestionLog.EcrireLog("MariaDB: ouverture connexion impossible.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database, ex)
            Throw
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V2.2 - 25/02/2026
    ' BuildConnectionString
    '
    ' Construit une connection string sécurisée à partir d'une config.
    ' Ne jamais logguer le résultat.
    '------------------------------------------------------------
    Private Shared Function BuildConnectionString(cfg As LocalDbConfig) As String

        If cfg Is Nothing Then Throw New ArgumentNullException(NameOf(cfg))

        Dim password As String = ResolvePassword(cfg)

        Dim csb As New MySqlConnectionStringBuilder With {
        .Server = cfg.Host,
        .Port = CUInt(If(cfg.Port > 0, cfg.Port, 3306)),
        .Database = cfg.Database,
        .UserID = cfg.UserName,
        .Password = password,
        .SslMode = MySqlSslMode.None,
        .Pooling = True,
        .ConnectionProtocol = MySqlConnectionProtocol.Tcp
    }

        ApplyWhitelistedConnectionOptions(csb, cfg.OptionsConn)

        Return csb.ConnectionString

    End Function

    Private Shared Function ResolvePassword(cfg As LocalDbConfig) As String

        If cfg Is Nothing Then Throw New ArgumentNullException(NameOf(cfg))

        Try
            If Not String.IsNullOrWhiteSpace(cfg.PasswordEncB64) Then
                Return CryptoManagerDPAPI.DecryptStringFromBase64(cfg.PasswordEncB64)
            End If

            If String.IsNullOrWhiteSpace(cfg.PasswordLegacyRaw) Then
                Throw New Exception("Aucun mot de passe valide trouvé (PasswordEncB64/Password legacy).")
            End If

            Dim legacyPassword As String = cfg.PasswordLegacyRaw.Trim()
            Dim plainPassword As String
            Dim encryptedToPersist As String

            If LooksLikeBase64(legacyPassword) Then
                Try
                    plainPassword = CryptoManagerDPAPI.DecryptStringFromBase64(legacyPassword)
                    encryptedToPersist = legacyPassword
                Catch ex As Exception
                    GestionLog.EcrireLog(
                        "DPAPI: Password legacy ressemble à du Base64 mais déchiffrement KO, fallback texte brut.",
                        GestionLog.LogLevel.Complet,
                        GestionLog.LogCategory.Process,
                        ex
                    )
                    plainPassword = legacyPassword
                    encryptedToPersist = CryptoManagerDPAPI.EncryptStringToBase64(plainPassword)
                End Try
            Else
                plainPassword = legacyPassword
                encryptedToPersist = CryptoManagerDPAPI.EncryptStringToBase64(plainPassword)
            End If

            TryPersistLegacyPasswordMigration(cfg, encryptedToPersist)
            Return plainPassword

        Catch ex As Exception
            GestionLog.EcrireLog(
                "DPAPI: déchiffrement/migration mot de passe impossible.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Process,
                ex
            )
            Throw
        End Try

    End Function

    Private Shared Sub TryPersistLegacyPasswordMigration(cfg As LocalDbConfig, encryptedPassword As String)

        If cfg Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(encryptedPassword) Then Exit Sub

        cfg.PasswordEncB64 = encryptedPassword
        cfg.PasswordLegacyRaw = Nothing

        Try
            ConfigLocalManager.SauvegarderConfigDb(cfg)
            GestionLog.EcrireLog(
                "Config DB: migration mot de passe legacy effectuée.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Process
            )
        Catch ex As Exception
            GestionLog.EcrireLog(
                "Config DB: migration mot de passe legacy non persistée (non bloquant).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Process,
                ex
            )
        End Try

    End Sub

    Private Shared Sub ApplyWhitelistedConnectionOptions(csb As MySqlConnectionStringBuilder, optionsConn As String)

        If csb Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(optionsConn) Then Exit Sub

        Dim rawOptions As String() = optionsConn.Split(";"c, StringSplitOptions.RemoveEmptyEntries)

        For Each rawOption As String In rawOptions

            Dim pair As String() = rawOption.Split(New Char() {"="c}, 2, StringSplitOptions.None)
            If pair.Length <> 2 Then
                GestionLog.EcrireLog(
                    $"MariaDB: option de connexion ignorée (format invalide): '{rawOption.Trim()}'.",
                    GestionLog.LogLevel.Complet,
                    GestionLog.LogCategory.Database
                )
                Continue For
            End If

            Dim key As String = pair(0).Trim()
            Dim value As String = pair(1).Trim()
            If String.IsNullOrWhiteSpace(key) OrElse String.IsNullOrWhiteSpace(value) Then
                GestionLog.EcrireLog(
                    $"MariaDB: option de connexion ignorée (clé/valeur vide): '{rawOption.Trim()}'.",
                    GestionLog.LogLevel.Complet,
                    GestionLog.LogCategory.Database
                )
                Continue For
            End If
            Dim normalizedKey As String = NormalizeOptionKey(key)

            If Not AllowedConnectionOptionKeys.Contains(normalizedKey) Then
                GestionLog.EcrireLog(
                    $"MariaDB: option de connexion ignorée (hors whitelist): '{key}'.",
                    GestionLog.LogLevel.Complet,
                    GestionLog.LogCategory.Database
                )
                Continue For
            End If

            ApplySingleWhitelistedOption(csb, normalizedKey, key, value)
        Next

    End Sub

    Private Shared Sub ApplySingleWhitelistedOption(csb As MySqlConnectionStringBuilder,
                                                    normalizedKey As String,
                                                    originalKey As String,
                                                    rawValue As String)

        Try
            Select Case normalizedKey
                Case "connectiontimeout"
                    Dim parsed As UInteger
                    If UInteger.TryParse(rawValue, parsed) Then
                        csb.ConnectionTimeout = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu entier non signé).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "defaultcommandtimeout"
                    Dim parsed As UInteger
                    If UInteger.TryParse(rawValue, parsed) Then
                        csb.DefaultCommandTimeout = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu entier non signé).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "keepalive"
                    Dim parsed As UInteger
                    If UInteger.TryParse(rawValue, parsed) Then
                        csb.Keepalive = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu entier non signé).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "allowuservariables"
                    Dim parsed As Boolean
                    If TryParseFlexibleBoolean(rawValue, parsed) Then
                        csb.AllowUserVariables = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu booléen).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "allowloadlocalinfile"
                    Dim parsed As Boolean
                    If TryParseFlexibleBoolean(rawValue, parsed) Then
                        csb.AllowLoadLocalInfile = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu booléen).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "treattinyasboolean"
                    Dim parsed As Boolean
                    If TryParseFlexibleBoolean(rawValue, parsed) Then
                        csb.TreatTinyAsBoolean = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu booléen).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "usecompression"
                    Dim parsed As Boolean
                    If TryParseFlexibleBoolean(rawValue, parsed) Then
                        csb.UseCompression = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu booléen).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "connectionreset"
                    Dim parsed As Boolean
                    If TryParseFlexibleBoolean(rawValue, parsed) Then
                        csb.ConnectionReset = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu booléen).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "maximumpoolsize"
                    Dim parsed As UInteger
                    If UInteger.TryParse(rawValue, parsed) Then
                        csb.MaximumPoolSize = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu entier non signé).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If

                Case "minimumpoolsize"
                    Dim parsed As UInteger
                    If UInteger.TryParse(rawValue, parsed) Then
                        csb.MinimumPoolSize = parsed
                    Else
                        GestionLog.EcrireLog($"MariaDB: option invalide '{originalKey}' (attendu entier non signé).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Database)
                    End If
            End Select

        Catch ex As Exception
            GestionLog.EcrireLog(
                $"MariaDB: option de connexion ignorée (valeur invalide): '{originalKey}'.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Database,
                ex
            )
        End Try

    End Sub

    Private Shared Function NormalizeOptionKey(key As String) As String
        If String.IsNullOrWhiteSpace(key) Then Return ""
        Return key.Trim().ToLowerInvariant().Replace("_", "").Replace(" ", "")
    End Function

    Private Shared Function TryParseFlexibleBoolean(value As String, ByRef result As Boolean) As Boolean

        If Boolean.TryParse(value, result) Then
            Return True
        End If

        Select Case value.Trim().ToLowerInvariant()
            Case "1", "yes", "y", "on"
                result = True
                Return True
            Case "0", "no", "n", "off"
                result = False
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Shared Function LooksLikeBase64(value As String) As Boolean

        If String.IsNullOrWhiteSpace(value) Then Return False

        Dim t As String = value.Trim()
        If (t.Length Mod 4) <> 0 Then Return False

        Dim firstPad As Integer = t.IndexOf("="c)
        If firstPad >= 0 Then
            Dim padChunk As String = t.Substring(firstPad)
            If padChunk.Length > 2 Then Return False
            If padChunk.Trim("="c).Length > 0 Then Return False
        End If

        For Each ch As Char In t
            If Char.IsLetterOrDigit(ch) OrElse ch = "+"c OrElse ch = "/"c OrElse ch = "="c Then
                Continue For
            End If
            Return False
        Next

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V2.2 - 25/02/2026
    ' TestConnexionMariaDb
    '
    ' Teste une connexion MariaDB à partir d'un LocalDbConfig.
    ' Lance une exception si la connexion échoue.
    ' Logs PROD : contexte (Complet) + exception (Succinct/Complet).
    ' Ne loggue jamais de secret.
    '------------------------------------------------------------
    Public Shared Sub TestConnexionMariaDb(cfg As LocalDbConfig)

        If cfg Is Nothing Then
            Throw New ArgumentNullException(NameOf(cfg))
        End If

        ' Contexte minimal utile (sans secrets)
        GestionLog.EcrireLog(
        $"MariaDB: TestConnexion begin (Host={cfg.Host}, Port={cfg.Port}, DB={cfg.Database}, User={cfg.UserName}).",
        GestionLog.LogLevel.Complet,
        GestionLog.LogCategory.Database
    )

        Try
            Dim cs As String = BuildConnectionString(cfg)
            ' ⚠️ IMPORTANT : ne jamais logguer cs, même en Complet.

            Using conn As New MySqlConnection(cs)
                conn.Open()
            End Using

            GestionLog.EcrireLog(
            "MariaDB: TestConnexion OK.",
            GestionLog.LogLevel.Rapide,
            GestionLog.LogCategory.Database
        )

        Catch ex As Exception
            ' Succinct + ex pour diagnostic
            GestionLog.EcrireLog(
            "MariaDB: TestConnexion KO.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Database,
            ex
        )

            ' Contexte complémentaire (sans secrets)
            GestionLog.EcrireLog(
            $"MariaDB: TestConnexion KO (Host={cfg.Host}, Port={cfg.Port}, DB={cfg.Database}, User={cfg.UserName}).",
            GestionLog.LogLevel.Complet,
            GestionLog.LogCategory.Database
        )

            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/02/2026
    ' GetSchemaVersion
    '
    ' Retourne la version du schéma de la base de données.
    '------------------------------------------------------------
    Public Shared Function GetSchemaVersion(conn As MySqlConnection) As Integer
        Using cmd As New MySqlCommand("SELECT schema_version FROM meta_schema WHERE id = 1;", conn)
            Dim obj = cmd.ExecuteScalar()
            If obj Is Nothing OrElse obj Is DBNull.Value Then
                Throw New Exception("La table meta_schema est absente ou invalide (id=1 introuvable).")
            End If
            Return Convert.ToInt32(obj)
        End Using
    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 25/02/2026
    ' EnsureSchemaCompatible
    '
    ' Vérifie que la version du schéma (table meta_schema) est
    ' compatible avec la version attendue par l'application.
    '
    ' expectedVersion : version attendue côté application.
    '
    ' Exception :
    ' - Si mismatch : Throw Exception("SchemaMismatch|DB=..|Expected=..")
    '------------------------------------------------------------
    Public Shared Sub EnsureSchemaCompatible(conn As MySqlConnection, expectedVersion As Integer)

        Dim v As Integer = GetSchemaVersion(conn)

        If v <> expectedVersion Then
            Throw New Exception($"SchemaMismatch|DB={v}|Expected={expectedVersion}")
        End If

    End Sub

#End Region



End Class
