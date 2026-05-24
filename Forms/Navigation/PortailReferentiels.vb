Option Strict On
Option Explicit On

Public Class PortailReferentiels
    Implements IReferentielShellContext

    Private _activeControl As UserControl

#Region "Process: Startup & Connexion DB"

    Private Sub LockUI()
        Me.Enabled = False
        SetStatus("Démarrage : connexion en cours...")
        GestionLog.EcrireLog("Portail: UI verrouillée (startup).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)
    End Sub

    Private Sub UnlockUI()
        Me.Enabled = True
        SetStatus("✅ Connexion OK.", FormStatusKind.Success)
        GestionLog.EcrireLog("Portail: UI déverrouillée (startup OK).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.Startup)
    End Sub

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

            Do
                Dim dlgResult As DialogResult

                Using frm As New GestionConnexionMariaDb()
                    dlgResult = frm.ShowDialog(Me)
                End Using

                If dlgResult <> DialogResult.OK Then
                    MessageBox.Show(
                        Me,
                        "Connexion non établie. L'application va se fermer.",
                        "Artefact",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    )
                    Application.Exit()
                    Exit Sub
                End If

                result = AppStartupManager.RunStartup()

                If result.Status = AppStartupManager.StartupStatus.Ok Then
                    UnlockUI()
                    Exit Do
                End If
            Loop

        Catch ex As Exception
            GestionLog.EcrireLog("Startup: erreur non gérée dans RunStartupFlow.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.Startup, ex)
            MessageBox.Show(
                Me,
                "Erreur critique au démarrage. L'application va se fermer." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
            Application.Exit()
        End Try
    End Sub

    Private Sub PortailReferentiels_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GestionLog.EcrireLog("PortailReferentiels: Load.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        RunStartupFlow()
        ShowLanding()
    End Sub

#End Region

#Region "Navigation"

    Private Sub ShowLanding()
        ClearCurrentControl()
        lblContexte.Text = "Contexte : accueil"
        pnlContent.Controls.Clear()

        Dim lbl As New Label With {
            .Text = "Sélectionnez un référentiel dans le menu de gauche.",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .ForeColor = Color.DimGray,
            .Font = New Font("Calibri", 13.0F, FontStyle.Italic)
        }

        pnlContent.Controls.Add(lbl)
        SetStatus("Accueil du portail référentiels.")
    End Sub

    Private Sub ShowControl(control As UserControl)
        ClearCurrentControl()

        _activeControl = control
        control.Dock = DockStyle.Fill

        pnlContent.Controls.Clear()
        pnlContent.Controls.Add(control)
        control.BringToFront()
    End Sub

    Private Sub ClearCurrentControl()
        If _activeControl Is Nothing Then Return

        pnlContent.Controls.Remove(_activeControl)
        _activeControl.Dispose()
        _activeControl = Nothing
    End Sub

    Private Sub btnOpenGestionLangues_Click(sender As Object, e As EventArgs) Handles btnOpenGestionLangues.Click
        Try
            Dim uc As New UC_Langues(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_Langues affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_Langues.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des langues.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionPays_Click(sender As Object, e As EventArgs) Handles btnOpenGestionPays.Click
        Try
            Dim uc As New UC_Pays(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_Pays affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_Pays.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des pays.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionRefEnum_Click(sender As Object, e As EventArgs) Handles btnOpenGestionRefEnum.Click
        Try
            Dim uc As New UC_RefEnum(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_RefEnum affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_RefEnum.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des références énumération.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionContacts_Click(sender As Object, e As EventArgs) Handles btnOpenGestionContacts.Click
        Try
            Dim uc As New UC_Contacts(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_Contacts affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_Contacts.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des contacts.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionEditeurs_Click(sender As Object, e As EventArgs) Handles btnOpenGestionEditeurs.Click
        Try
            Dim uc As New UC_Editeurs(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_Editeurs affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_Editeurs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des éditeurs.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionFormatFile_Click(sender As Object, e As EventArgs) Handles btnOpenGestionFormatFile.Click
        Try
            Dim uc As New UC_FormatFile(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_FormatFile affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_FormatFile.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des formats file.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionImpression_Click(sender As Object, e As EventArgs) Handles btnOpenGestionImpression.Click
        Try
            Dim uc As New UC_Impression(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_Impression affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_Impression.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion impression.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionRecommandation_Click(sender As Object, e As EventArgs) Handles btnOpenGestionRecommandation.Click
        Try
            Dim uc As New UC_Recommandations(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_Recommandations affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_Recommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des recommandations.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnOpenGestionPrixLit_Click(sender As Object, e As EventArgs) Handles btnOpenGestionPrixLit.Click
        Try
            Dim uc As New UC_PrixLit(Me)
            ShowControl(uc)
            GestionLog.EcrireLog("[UC] UC_PrixLit affiché dans PortailReferentiels.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur Load UC_PrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'ouverture de la gestion des prix littéraires.", FormStatusKind.Error)
        End Try
    End Sub

#End Region

#Region "IReferentielShellContext"

    Public ReadOnly Property SharedToolTip As ToolTip Implements IReferentielShellContext.SharedToolTip
        Get
            Return ttMain
        End Get
    End Property

    Public ReadOnly Property SharedErrorProvider As ErrorProvider Implements IReferentielShellContext.SharedErrorProvider
        Get
            Return errProvider
        End Get
    End Property

    Public Sub SetStatus(message As String, Optional statusKind As FormStatusKind = FormStatusKind.Info) Implements IReferentielShellContext.SetStatus
        SetFormStatus(stsLabelStatus, message, statusKind)
    End Sub

    Public Sub SetContext(moduleName As String, mode As ModeEdition) Implements IReferentielShellContext.SetContext
        Dim modeLabel As String

        Select Case mode
            Case ModeEdition.Nouveau
                modeLabel = "Nouveau"
            Case ModeEdition.Modification
                modeLabel = "Édition"
            Case Else
                modeLabel = "Lecture"
        End Select

        lblContexte.Text = $"Contexte : {moduleName} | Mode : {modeLabel}"
    End Sub

    Public Sub NavigateHome() Implements IReferentielShellContext.NavigateHome
        ShowLanding()
    End Sub

#End Region

End Class
