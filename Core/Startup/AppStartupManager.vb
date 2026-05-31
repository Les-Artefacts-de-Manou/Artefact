'------------------------------------------------------------
' 📌 V1.2 - 23/02/2026
' AppStartupManager
'
' Orchestrateur de démarrage Artefact :
' - Lecture config locale (JSON)
' - Test de connexion MariaDB
' - Vérification version schéma Artefact (meta_schema)
'
' Règles :
' - Pas de MsgBox ici
' - Tout passe par GestionLog
' - Retourne uniquement un statut exploitable par l'UI
'------------------------------------------------------------

Public Class AppStartupManager

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/02/2026
    ' ExpectedSchemaVersion
    '
    ' Version attendue du schéma Artefact (PAS MariaDB).
    ' À incrémenter quand une migration structurelle est appliquée.
    '------------------------------------------------------------

    Private Const ExpectedSchemaVersion As Integer = 7
    '  (modification : 2026-03-20 16:45:07  - et update de la table "meta_schema" )

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/02/2026
    ' StartupStatus
    ' Statuts de démarrage consommés par l'UI (Home).
    '------------------------------------------------------------
    Public Enum StartupStatus
        Ok
        ConfigMissingOrInvalid
        ConnectionFailed
        SchemaMismatch
        UnexpectedError
    End Enum

    '------------------------------------------------------------
    ' 📌 V1.0 - 22/02/2026
    ' StartupResult
    ' Résultat minimal (détails -> GestionLog).
    '------------------------------------------------------------
    Public Class StartupResult
        Public Property Status As StartupStatus
    End Class

    '------------------------------------------------------------
    ' 📌 V1.3 - 26/02/2026
    ' RunStartup
    '
    ' Orchestration de démarrage :
    ' - Lecture config locale
    ' - Test connexion MariaDB
    ' - Vérification version schéma Artefact (meta_schema)
    '------------------------------------------------------------
    Public Shared Function RunStartup() As StartupResult

        Try
            ' 1) Lire config locale
            Dim cfg = ConfigLocalManager.LireConfigDb()

            If cfg Is Nothing Then
                GestionLog.EcrireLog(
                    "Config locale absente ou illisible.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.Process
                )
                Return New StartupResult With {.Status = StartupStatus.ConfigMissingOrInvalid}
            End If

            ' Contexte minimal (sans secrets)
            GestionLog.EcrireLog(
                $"Config OK (Host={cfg.Host}, Port={cfg.Port}, DB={cfg.Database}, User={cfg.UserName}).",
                GestionLog.LogLevel.Complet,
                GestionLog.LogCategory.Startup
            )

            ' 2) Tester connexion MariaDB
            Try
                DatabaseManager.TestConnexionMariaDb(cfg)

            Catch ex As Exception
                GestionLog.EcrireLog(
                    "Connexion MariaDB échouée.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.Database,
                    ex
                )

                GestionLog.EcrireLog(
                    $"Contexte connexion (Host={cfg.Host}, Port={cfg.Port}, DB={cfg.Database}, User={cfg.UserName}).",
                    GestionLog.LogLevel.Complet,
                    GestionLog.LogCategory.Database
                )

                Return New StartupResult With {.Status = StartupStatus.ConnectionFailed}
            End Try

            ' 3) Vérifier version schéma
            Try
                ' Une seule initialisation centrale
                DatabaseManager.InitializeMariaDb()

                Using conn = DatabaseManager.GetConnexionMariaDB()
                    DatabaseManager.EnsureSchemaCompatible(conn, ExpectedSchemaVersion)
                End Using

            Catch ex As Exception
                If ex.Message.StartsWith("SchemaMismatch|", StringComparison.Ordinal) Then

                    GestionLog.EcrireLog(
                        "Incompatibilité version schéma détectée: " & ex.Message,
                        GestionLog.LogLevel.Succinct,
                        GestionLog.LogCategory.Database
                    )

                    Return New StartupResult With {.Status = StartupStatus.SchemaMismatch}
                End If

                GestionLog.EcrireLog(
                    "Erreur lors de la vérification du schéma.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.Database,
                    ex
                )

                Return New StartupResult With {.Status = StartupStatus.UnexpectedError}
            End Try

            ' OK final
            GestionLog.EcrireLog(
                "Startup OK.",
                GestionLog.LogLevel.Rapide,
                GestionLog.LogCategory.Startup
            )

            Return New StartupResult With {.Status = StartupStatus.Ok}

        Catch ex As Exception
            GestionLog.EcrireLog(
                "Erreur inattendue dans RunStartup().",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Startup,
                ex
            )
            Return New StartupResult With {.Status = StartupStatus.UnexpectedError}
        End Try

    End Function

End Class
