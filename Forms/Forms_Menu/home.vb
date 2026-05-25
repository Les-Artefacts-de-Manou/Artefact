'------------------------------------------------------------
' 📌 Home.vb - Form
' Version : V1.1
' Date    : 26/02/2026
' Auteur  : Joëlle
'
' Rôle :
' Formulaire principal d’Artefact.
' Point d’entrée unique de l’application.
'
' Responsabilités :
' - Initialisation des services principaux (DatabaseManager, ConfigManager, etc.)
' - Vérification de la connexion MariaDB au démarrage
' - Navigation vers les modules métier
' - Affichage des statuts système via StatusStrip

'------------------------------------------------------------

Public Class Home

#Region "Process: Startup & Connexion DB"

    '------------------------------------------------------------
    ' 📌 V1.1 - 26/02/2026
    ' LockUI
    '
    ' Verrouille l'interface tant que le démarrage (DB/config) n'est pas OK.
    '------------------------------------------------------------
    Private Sub LockUI()

        Me.Enabled = False

        ' Status UI (si présent)
        Try
            stsLabelStatus.Text = "Démarrage : connexion en cours..."
        Catch
            ' Ignore si pas de StatusStrip/label câblé
        End Try

        GestionLog.EcrireLog("Home: UI verrouillée (startup).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)


    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 26/02/2026
    ' UnlockUI
    '
    ' Déverrouille l'interface après un démarrage validé.
    '------------------------------------------------------------
    Private Sub UnlockUI()

        Me.Enabled = True

        Try
            stsLabelStatus.Text = "✅ Connexion OK."
        Catch
            ' Ignore si pas de StatusStrip/label câblé
        End Try

        GestionLog.EcrireLog("Home: UI déverrouillée (startup OK).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.4 - 26/02/2026
    ' RunStartupFlow
    '
    ' Démarrage applicatif :
    ' - Verrouille l'UI
    ' - Teste startup
    ' - Si KO : ouvre GestionConnexionMariaDb
    ' - Si annulation : message critique + fermeture
    ' - Re-teste jusqu'à OK
    '------------------------------------------------------------
    Private Sub RunStartupFlow()

        LockUI()
        GestionLog.EcrireLog("Startup: début.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)

        Try
            Dim result = AppStartupManager.RunStartup()

            If result.Status = AppStartupManager.StartupStatus.Ok Then
                GestionLog.EcrireLog("Startup: OK.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)
                UnlockUI()
                Exit Sub
            End If

            GestionLog.EcrireLog($"Startup: KO - Status={result.Status}.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Startup)
            GestionLog.EcrireLog("Home: ouverture GestionConnexionMariaDb.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)

            Do
                Dim dlgResult As DialogResult

                Using frm As New GestionConnexionMariaDb()
                    dlgResult = frm.ShowDialog()
                End Using

                If dlgResult <> DialogResult.OK Then
                    GestionLog.EcrireLog("Startup: abandon utilisateur -> fermeture application.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)

                    ' Point critique : l'appli ne peut pas fonctionner sans DB
                    MessageBox.Show(
                    "Connexion non établie. L'application va se fermer.",
                    "Artefact",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )

                    Application.Exit()
                    Exit Sub
                End If

                GestionLog.EcrireLog("Startup: re-test après gestion connexion.", GestionLog.LogLevel.Complet, GestionLog.LogCategory.Startup)

                result = AppStartupManager.RunStartup()

                If result.Status = AppStartupManager.StartupStatus.Ok Then
                    GestionLog.EcrireLog("Startup: OK après correction.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)
                    UnlockUI()
                    Exit Do
                End If

                GestionLog.EcrireLog($"Startup: KO après correction - Status={result.Status}.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Startup)

            Loop

        Catch ex As Exception
            ' Catch global : on loggue et on ferme (point critique)
            GestionLog.EcrireLog("Startup: erreur non gérée dans RunStartupFlow.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Startup, ex)

            MessageBox.Show(
            "Erreur critique au démarrage. L'application va se fermer." & Environment.NewLine & ex.Message,
            "Artefact",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

            Application.Exit()

        Finally
            ' Si on a quitté sur exception sans déverrouiller, on évite une home bloquée
            ' (Application.Exit va fermer, mais c'est propre)
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 26/02/2026
    ' Home_Load
    '
    ' Point d’entrée du formulaire : lance le flux de démarrage.
    '------------------------------------------------------------
    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GestionLog.EcrireLog("Home: Load.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        RunStartupFlow()

    End Sub

#End Region

#Region "Navigation: Référentiels"
    '------------------------------------------------------------
    ' 📌 V1.0 - 01/03/2026
    ' btnOpenGestionLangues_Click

    'Ouvre le formulaire de gestion des langues.
    '------------------------------------------------------------ 
    Private Sub btnOpenGestionLangues_Click(sender As Object, e As EventArgs) Handles btnOpenGestionLangues.Click

        Try
            ShowFormIfNotOpen(Of GestionLangues)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionLangues affiché.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionLangues.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnOpenGestionPays_Click(sender As Object, e As EventArgs) Handles btnOpenGestionPays.Click

        Try
            ShowFormIfNotOpen(Of GestionPays)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionPays affiché.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionPays.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try
    End Sub

    Private Sub btnOpenGestionRefEnum_Click(sender As Object, e As EventArgs) Handles btnOpenGestionRefEnum.Click

        Try
            ShowFormIfNotOpen(Of GestionRefEnum)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionRefEnum affiché.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionRefEnum.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnOpenGestionContactsClick(sender As Object, e As EventArgs) Handles btnOpenGestionContacts.Click

        Try
            ShowFormIfNotOpen(Of GestionContacts)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionContacts affiché.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionContacts.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnOpenGestionEditeurs_Click(sender As Object, e As EventArgs) Handles btnOpenGestionEditeurs.Click

        Try
            ShowFormIfNotOpen(Of GestionEditeurs)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionEditeurs affiché", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionEditeurs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnOpenGestionFormatFile_Click(sender As Object, e As EventArgs) Handles btnOpenGestionFormatFile.Click

        Try
            ShowFormIfNotOpen(Of GestionFormatFile)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionFormatFile affiché", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionFormatFile.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnOpenGestionImpression_Click(sender As Object, e As EventArgs) Handles btnOpenGestionImpression.Click

        Try
            ShowFormIfNotOpen(Of GestionImpression)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionImpression affiché", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionImpression.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnOpenGestionRecommandation_Click(sender As Object, e As EventArgs) Handles btnOpenGestionRecommandation.Click

        Try
            ShowFormIfNotOpen(Of GestionRecommandations)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionRecommandations affiché", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionRecommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnOpenGestionPrixLit_Click(sender As Object, e As EventArgs) Handles btnOpenGestionPrixLit.Click

        Try
            ShowFormIfNotOpen(Of GestionPrixLit)()
            GestionLog.EcrireLog("📖 [Form] Formulaire GestionPrixLit affiché", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load GestionPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

#End Region

End Class
