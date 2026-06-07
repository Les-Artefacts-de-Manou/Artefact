'====================================================================
' 📌 PortailReferentiels.vb
' Version : V1.0
' Date    : 23/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Form conteneur pour tous les UserControls de référentiels.
' Fournit l'infrastructure commune :
' - StatusStrip mutualisé
' - ErrorProvider mutualisé
' - ToolTip mutualisé
' - Label de contexte (mode, environnement)
' - Panel d'hébergement pour les UC
'
' Évolution :
' - V1.0 : Création de l'infrastructure conteneur
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class PortailReferentiels

#Region "Déclarations"

    ' UserControl actuellement affiché
    Private _currentUC As UserControl = Nothing

    ' Contexte partagé et gestionnaire de navigation
    Private _context As UserControlContext = Nothing
    Private _navigationManager As NavigationManager = Nothing

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' PortailReferentiels_Load
    '
    '   • Initialise le portail au chargement
    '   • Crée le contexte partagé et le gestionnaire de navigation
    '   • Affiche l'UC_Accueil par défaut
    '------------------------------------------------------------
    Private Sub PortailReferentiels_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ' Créer le contexte partagé avec injection des services UI
            _context = New UserControlContext(Me.stsStatus, Me.errProvider, Me.ttMain)
            _context.ContextLabel = Me.lblContexte
            _context.CurrentUserName = Environment.UserName
            _context.ApplicationTitle = "Artefact - Portail Référentiels"

            ' Créer le gestionnaire de navigation
            _navigationManager = New NavigationManager(Me.pnlMain, _context)

            ' Enregistrer les UserControls disponibles
            _navigationManager.RegisterUserControl("Accueil", New UC_Accueil())
            _navigationManager.RegisterUserControl("Langues", New UC_Langues())
            _navigationManager.RegisterUserControl("Pays", New UC_Pays())
            _navigationManager.RegisterUserControl("Contacts", New UC_Contacts())
            _navigationManager.RegisterUserControl("Formats de fichier", New UC_FormatFile())
            _navigationManager.RegisterUserControl("Éditeurs", New UC_Editeurs())
            _navigationManager.RegisterUserControl("Impression", New UC_Impression())
            _navigationManager.RegisterUserControl("Ref Enum", New UC_RefEnum())
            _navigationManager.RegisterUserControl("Recommandations", New UC_Recommandations())
            _navigationManager.RegisterUserControl("Prix littéraires", New UC_PrixLit())
            ' TODO: enregistrer les autres UC au fur et à mesure de leur migration

            ' Charger l'accueil par défaut
            _navigationManager.NavigateTo("Accueil")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement PortailReferentiels.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors du chargement du portail." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

#End Region

#Region "Navigation UserControls"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ChargerUserControl
    '
    '   • Charge un UserControl dans le panel principal
    '   • Dispose le précédent UC si existant
    '   • Ajuste la taille du form à la taille préférée de l'UC
    '------------------------------------------------------------
    Private Sub ChargerUserControl(uc As UserControl)

        Try
            ' Dispose l'UC précédent
            If _currentUC IsNot Nothing Then
                pnlMain.Controls.Remove(_currentUC)
                _currentUC.Dispose()
                _currentUC = Nothing
            End If

            ' Charge le nouvel UC
            If uc IsNot Nothing Then
                uc.Dock = DockStyle.Fill
                pnlMain.Controls.Add(uc)
                _currentUC = uc

                ' Ajuster la taille du form à la taille de l'UC + marges (navigation + top + status)
                AjusterTailleForm(uc)
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement UserControl dans PortailReferentiels.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors du chargement.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' AjusterTailleForm
    '
    '   • Ajuste la taille du form pour s'adapter à l'UC chargé
    '   • Prend en compte la taille préférée de l'UC + marges du portail
    '------------------------------------------------------------
    Private Sub AjusterTailleForm(uc As UserControl)

        Try
            ' Récupérer la taille préférée de l'UC (ou sa taille définie)
            Dim ucWidth As Integer = If(uc.Width > 0, uc.Width, 1000)
            Dim ucHeight As Integer = If(uc.Height > 0, uc.Height, 700)

            ' Calculer la taille totale nécessaire pour le form
            ' Navigation (180) + UC + marge
            Dim formWidth As Integer = pnlNavigation.Width + ucWidth + 20

            ' Top (60) + UC + StatusBar (22) + marge
            Dim formHeight As Integer = pnlTop.Height + ucHeight + stsStatus.Height + 20

            ' Obtenir la taille de l'écran de travail (sans barre des tâches)
            Dim screenBounds As Rectangle = Screen.FromControl(Me).WorkingArea

            ' Limiter à la taille de l'écran (avec marge de sécurité)
            formWidth = Math.Min(formWidth, screenBounds.Width - 40)
            formHeight = Math.Min(formHeight, screenBounds.Height - 40)

            ' Respecter la taille minimale
            formWidth = Math.Max(formWidth, Me.MinimumSize.Width)
            formHeight = Math.Max(formHeight, Me.MinimumSize.Height)

            ' Appliquer la nouvelle taille
            Me.Size = New Size(formWidth, formHeight)

            ' Centrer le form après redimensionnement
            Me.CenterToScreen()

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur ajustement taille form.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' Boutons de navigation
    '------------------------------------------------------------
    Private Sub btnAccueil_Click(sender As Object, e As EventArgs) Handles btnAccueil.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Accueil")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Accueil.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnLangues_Click(sender As Object, e As EventArgs) Handles btnLangues.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Langues")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Langues.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnPays_Click(sender As Object, e As EventArgs) Handles btnPays.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Pays")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Pays.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnContacts_Click(sender As Object, e As EventArgs) Handles btnContacts.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Contacts")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Contacts.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnFormatFile_Click(sender As Object, e As EventArgs) Handles btnFormatFile.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Formats de fichier")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Formats de fichier.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnEditeurs_Click(sender As Object, e As EventArgs) Handles btnEditeurs.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Éditeurs")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Éditeurs.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnImpression_Click(sender As Object, e As EventArgs) Handles btnImpression.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Impression")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Impression.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnRefEnum_Click(sender As Object, e As EventArgs) Handles btnRefEnum.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Ref Enum")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Ref Enum.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnRecommandations_Click(sender As Object, e As EventArgs) Handles btnRecommandations.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Recommandations")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Recommandations.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

    Private Sub btnPrixLit_Click(sender As Object, e As EventArgs) Handles btnPrixLit.Click
        Try
            If _navigationManager IsNot Nothing Then
                _navigationManager.NavigateTo("Prix littéraires")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur navigation Prix littéraires.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de la navigation.")
        End Try
    End Sub

#End Region

#Region "Utilitaires UI"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SetStatus
    '
    '   • Met à jour le StatusStrip (obsolète, préférer _context.SetStatus)
    '------------------------------------------------------------
    Public Sub SetStatus(message As String)
        If stsLabelStatus IsNot Nothing Then
            stsLabelStatus.Text = message
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SetContexte
    '
    '   • Met à jour le label de contexte en haut (obsolète, préférer _context.SetContextDisplay)
    '------------------------------------------------------------
    Public Sub SetContexte(contexte As String)
        If lblContexte IsNot Nothing Then
            lblContexte.Text = contexte
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' Context (propriété publique)
    '
    '   • Permet l'accès au contexte partagé depuis l'extérieur si nécessaire
    '------------------------------------------------------------
    Public ReadOnly Property Context As UserControlContext
        Get
            Return _context
        End Get
    End Property

#End Region

#Region "Fermeture"

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

End Class
