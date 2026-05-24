Option Strict On
Option Explicit On

Public MustInherit Class UC_LegacyReferentielHost
    Inherits UserControl

    Private Shared ReadOnly TitleForeColor As Color = Color.Tomato

    Private ReadOnly _context As IReferentielShellContext
    Private ReadOnly _moduleName As String
    Private ReadOnly _titleText As String

    Private ReadOnly _lblTitle As Label
    Private ReadOnly _btnBack As Button
    Private ReadOnly _hostPanel As Panel

    Private _hostedControl As Control

    Protected Sub New(moduleName As String, titleText As String, context As IReferentielShellContext)
        _moduleName = moduleName
        _titleText = titleText
        _context = context

        Dim topPanel As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 46,
            .Padding = New Padding(8)
        }

        _lblTitle = New Label With {
            .Dock = DockStyle.Fill,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Calibri", 14.0F, FontStyle.Bold),
            .ForeColor = TitleForeColor,
            .Text = _titleText
        }

        _btnBack = New Button With {
            .Dock = DockStyle.Right,
            .Width = 120,
            .Text = "Retour accueil",
            .UseVisualStyleBackColor = True
        }
        AddHandler _btnBack.Click, AddressOf btnBack_Click

        topPanel.Controls.Add(_lblTitle)
        topPanel.Controls.Add(_btnBack)

        _hostPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .BorderStyle = BorderStyle.FixedSingle
        }

        Controls.Add(_hostPanel)
        Controls.Add(topPanel)

        AddHandler Me.Load, AddressOf UC_LegacyReferentielHost_Load
    End Sub

    Protected Overridable Function BuildHostedControl() As Control
        Dim lbl As New Label With {
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .ForeColor = Color.DimGray,
            .Font = New Font("Calibri", 12.0F, FontStyle.Italic),
            .Text = "Ce module est en cours de migration vers un UserControl natif."
        }

        Return lbl
    End Function

    Private Sub UC_LegacyReferentielHost_Load(sender As Object, e As EventArgs)
        EnsureHosted()
    End Sub

    Private Sub EnsureHosted()
        If _hostedControl IsNot Nothing Then Return

        Try
            _hostedControl = BuildHostedControl()
            If _hostedControl Is Nothing Then
                Throw New InvalidOperationException($"Le contrôle hébergé de '{_moduleName}' est Nothing. BuildHostedControl() doit retourner une instance Control valide.")
            End If

            _hostedControl.Dock = DockStyle.Fill

            _hostPanel.Controls.Clear()
            _hostPanel.Controls.Add(_hostedControl)

            SetContext(ModeEdition.Consultation)
            SetStatus($"{_moduleName} chargé.", FormStatusKind.Success)

            GestionLog.EcrireLog($"[UC] {_moduleName}: écran chargé (sans form legacy).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)

        Catch ex As Exception
            GestionLog.EcrireLog($"UI: erreur hosting {_moduleName}.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus($"Erreur lors du chargement de {_moduleName}.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        _context?.NavigateHome()
    End Sub

    Private Sub SetStatus(message As String, Optional kind As FormStatusKind = FormStatusKind.Info)
        _context?.SetStatus(message, kind)
    End Sub

    Private Sub SetContext(mode As ModeEdition)
        _context?.SetContext(_moduleName, mode)
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            RemoveHandler Me.Load, AddressOf UC_LegacyReferentielHost_Load
            RemoveHandler _btnBack.Click, AddressOf btnBack_Click

            If _hostedControl IsNot Nothing Then
                _hostedControl.Dispose()
                _hostedControl = Nothing
            End If
        End If

        MyBase.Dispose(disposing)
    End Sub
End Class
