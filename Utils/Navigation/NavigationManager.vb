Imports System.Windows.Forms

''' <summary>
''' Gestionnaire de navigation entre les UserControls du portail
''' Coordonne le chargement/déchargement des UC et la gestion du contexte
''' </summary>
Public Class NavigationManager
    Private ReadOnly _containerPanel As Panel
    Private ReadOnly _context As UserControlContext
    Private _currentControl As UserControl

    ''' <summary>
    ''' Dictionnaire des UserControls disponibles (nom -> instance)
    ''' </summary>
    Private ReadOnly _userControls As New Dictionary(Of String, UserControl)()

    ''' <summary>
    ''' Constructeur
    ''' </summary>
    Public Sub New(containerPanel As Panel, context As UserControlContext)
        _containerPanel = containerPanel
        _context = context

        ' S'abonner aux demandes de navigation du contexte
        AddHandler _context.NavigationRequested, AddressOf OnNavigationRequested
    End Sub

    ''' <summary>
    ''' Enregistre un UserControl disponible pour la navigation
    ''' </summary>
    Public Sub RegisterUserControl(name As String, userControl As UserControl)
        If Not _userControls.ContainsKey(name) Then
            _userControls.Add(name, userControl)
        End If
    End Sub

    ''' <summary>
    ''' Navigue vers un UserControl enregistré
    ''' </summary>
    Public Sub NavigateTo(name As String)
        If Not _userControls.ContainsKey(name) Then
            MessageBox.Show($"UserControl '{name}' non trouvé.", "Navigation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Désactiver l'UC courant
        If _currentControl IsNot Nothing Then
            If TypeOf _currentControl Is IContextAwareUserControl Then
                DirectCast(_currentControl, IContextAwareUserControl).OnDeactivated()
            End If
        End If

        ' Charger le nouveau UC
        Dim newControl = _userControls(name)
        LoadUserControl(newControl)

        ' Activer le nouveau UC
        If TypeOf newControl Is IContextAwareUserControl Then
            Dim contextAware = DirectCast(newControl, IContextAwareUserControl)
            contextAware.SetContext(_context)
            contextAware.OnActivated()
        End If

        _currentControl = newControl
    End Sub

    ''' <summary>
    ''' Navigue vers un type d'UserControl (création à la volée)
    ''' </summary>
    Public Sub NavigateTo(Of T As {UserControl, New})()
        Dim name = GetType(T).Name

        ' Créer l'instance si elle n'existe pas
        If Not _userControls.ContainsKey(name) Then
            Dim newInstance As T = New T()
            RegisterUserControl(name, newInstance)
        End If

        NavigateTo(name)
    End Sub

    ''' <summary>
    ''' Charge physiquement un UserControl dans le panel conteneur
    ''' </summary>
    Private Sub LoadUserControl(userControl As UserControl)
        _containerPanel.SuspendLayout()
        _containerPanel.Controls.Clear()

        userControl.Dock = DockStyle.Fill
        _containerPanel.Controls.Add(userControl)

        _containerPanel.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Gestionnaire de l'événement NavigationRequested du contexte
    ''' </summary>
    Private Sub OnNavigationRequested(sender As Object, e As NavigationEventArgs)
        NavigateTo(e.TargetName)
    End Sub

    ''' <summary>
    ''' Retourne le UserControl actuellement affiché
    ''' </summary>
    Public ReadOnly Property CurrentControl As UserControl
        Get
            Return _currentControl
        End Get
    End Property
End Class
