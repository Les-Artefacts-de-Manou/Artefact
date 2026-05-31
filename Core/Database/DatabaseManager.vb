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

Imports MySqlConnector

Public Class DatabaseManager

#Region "Variables / Constantes"

    Private Shared _connectionString As String = String.Empty
    Private Shared _isInitialized As Boolean = False

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

        Dim password As String

        Try
            If String.IsNullOrWhiteSpace(cfg.PasswordEncB64) Then
                Throw New Exception("PasswordEncB64 vide.")
            End If

            password = CryptoManagerDPAPI.DecryptStringFromBase64(cfg.PasswordEncB64)

        Catch ex As Exception
            GestionLog.EcrireLog(
            "DPAPI: déchiffrement mot de passe impossible.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Process,
            ex
        )
            Throw
        End Try

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

        Return csb.ConnectionString

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
