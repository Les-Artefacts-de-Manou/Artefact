Imports System.Windows.Forms

''' <summary>
''' Contexte partagé entre le conteneur (portail) et les UserControls
''' Fournit les services communs (StatusStrip, ErrorProvider, ToolTip)
''' </summary>
Public Class UserControlContext
    ' Services UI partagés
    Public Property StatusStrip As StatusStrip
    Public Property ErrorProvider As ErrorProvider
    Public Property ToolTip As ToolTip

    ' Propriétés de contexte applicatif
    Public Property CurrentUserName As String
    Public Property ApplicationTitle As String
    Public Property ContextLabel As Label  ' Le label de contexte dans le portail

    ' Fil d'Ariane (breadcrumb)
    Private _breadcrumb As New List(Of String) From {"Portail Référentiels"}
    Private _currentMode As String = ""

    ' Events pour communication bidirectionnelle
    Public Event ContextChanged As EventHandler
    Public Event NavigationRequested As EventHandler(Of NavigationEventArgs)

    ''' <summary>
    ''' Constructeur avec injection des services UI
    ''' </summary>
    Public Sub New(statusStrip As StatusStrip, errorProvider As ErrorProvider, toolTip As ToolTip)
        Me.StatusStrip = statusStrip
        Me.ErrorProvider = errorProvider
        Me.ToolTip = toolTip
    End Sub

    ''' <summary>
    ''' Met à jour le statut affiché dans la barre de status
    ''' </summary>
    Public Sub SetStatus(message As String)
        If StatusStrip IsNot Nothing AndAlso StatusStrip.Items.Count > 0 Then
            If TypeOf StatusStrip.Items(0) Is ToolStripStatusLabel Then
                Dim label = DirectCast(StatusStrip.Items(0), ToolStripStatusLabel)
                label.Text = message
            End If
        End If
    End Sub

    ''' <summary>
    ''' Met à jour le contexte affiché dans le label principal (texte direct)
    ''' </summary>
    Public Sub SetContextDisplay(text As String)
        If ContextLabel IsNot Nothing Then
            ContextLabel.Text = text
            RaiseEvent ContextChanged(Me, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Navigue vers un niveau du portail (ex: "Accueil", "Langues", "Pays")
    ''' </summary>
    Public Sub NavigateToLevel(levelName As String)
        ' Réinitialiser à Portail Référentiels + nouveau niveau
        _breadcrumb.Clear()
        _breadcrumb.Add("Portail Référentiels")
        _breadcrumb.Add(levelName)
        _currentMode = ""
        UpdateBreadcrumbDisplay()
    End Sub

    ''' <summary>
    ''' Définit le mode actuel (ex: "Nouveau", "Modification", "Consultation")
    ''' </summary>
    Public Sub SetMode(mode As String)
        _currentMode = mode
        UpdateBreadcrumbDisplay()
    End Sub

    ''' <summary>
    ''' Retour à l'accueil
    ''' </summary>
    Public Sub NavigateToHome()
        _breadcrumb.Clear()
        _breadcrumb.Add("Portail Référentiels")
        _breadcrumb.Add("Accueil")
        _currentMode = ""
        UpdateBreadcrumbDisplay()
    End Sub

    ''' <summary>
    ''' Met à jour l'affichage du fil d'Ariane dans lblContexte
    ''' </summary>
    Private Sub UpdateBreadcrumbDisplay()
        Dim breadcrumbText As String = String.Join(" > ", _breadcrumb)
        If Not String.IsNullOrWhiteSpace(_currentMode) Then
            breadcrumbText &= " > " & _currentMode
        End If
        SetContextDisplay(breadcrumbText)
    End Sub

    ''' <summary>
    ''' Efface toutes les erreurs de l'ErrorProvider
    ''' </summary>
    Public Sub ClearAllErrors()
        If ErrorProvider IsNot Nothing Then
            ErrorProvider.Clear()
        End If
    End Sub

    ''' <summary>
    ''' Demande une navigation vers un autre UC
    ''' </summary>
    Public Sub RequestNavigation(targetName As String, Optional data As Object = Nothing)
        RaiseEvent NavigationRequested(Me, New NavigationEventArgs(targetName, data))
    End Sub
End Class

''' <summary>
''' Arguments pour l'événement de navigation
''' </summary>
Public Class NavigationEventArgs
    Inherits EventArgs

    Public Property TargetName As String
    Public Property NavigationData As Object

    Public Sub New(targetName As String, Optional data As Object = Nothing)
        Me.TargetName = targetName
        Me.NavigationData = data
    End Sub
End Class
