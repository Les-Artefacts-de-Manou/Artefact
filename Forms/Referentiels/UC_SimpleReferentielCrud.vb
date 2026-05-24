Option Strict On
Option Explicit On

Imports System.Data
Imports System.Linq

Public Enum CrudFieldKind
    [Text] = 0
    MultilineText = 1
    [Boolean] = 2
    [Integer] = 3
    [Date] = 4
End Enum

Public Class CrudFieldDefinition
    Public ReadOnly Property Name As String
    Public ReadOnly Property Label As String
    Public ReadOnly Property Kind As CrudFieldKind
    Public ReadOnly Property Required As Boolean
    Public ReadOnly Property [ReadOnly] As Boolean
    Public ReadOnly Property MaxLength As Integer
    Public ReadOnly Property CharacterCasing As CharacterCasing

    Public Sub New(name As String,
                   label As String,
                   Optional kind As CrudFieldKind = CrudFieldKind.Text,
                   Optional required As Boolean = False,
                   Optional [readOnly] As Boolean = False,
                   Optional maxLength As Integer = 0,
                   Optional characterCasing As CharacterCasing = CharacterCasing.Normal)

        Me.Name = name
        Me.Label = label
        Me.Kind = kind
        Me.Required = required
        Me.ReadOnly = [readOnly]
        Me.MaxLength = maxLength
        Me.CharacterCasing = characterCasing
    End Sub
End Class

Public MustInherit Class UC_SimpleReferentielCrud
    Inherits UserControl

    Private ReadOnly _context As IReferentielShellContext
    Private ReadOnly _moduleName As String
    Private ReadOnly _idColumnName As String
    Private ReadOnly _mainColumnName As String
    Private ReadOnly _fields As List(Of CrudFieldDefinition)

    Private ReadOnly _loadAllFunc As Func(Of DataTable)
    Private ReadOnly _searchFunc As Func(Of String, DataTable)
    Private ReadOnly _insertFunc As Func(Of Dictionary(Of String, Object), ULong)
    Private ReadOnly _updateFunc As Action(Of ULong, Dictionary(Of String, Object))
    Private ReadOnly _deleteFunc As Action(Of ULong)
    Private ReadOnly _buildDeleteMessageFunc As Func(Of ULong, Dictionary(Of String, Object), String)

    Private ReadOnly _controlByField As New Dictionary(Of String, Control)(StringComparer.OrdinalIgnoreCase)
    Private ReadOnly _snapshotValues As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

    Private ReadOnly _lblTitle As Label
    Private ReadOnly _lblCount As Label
    Private ReadOnly _lblMode As Label
    Private ReadOnly _txtSearch As TextBox
    Private ReadOnly _txtId As TextBox
    Private ReadOnly _dgv As DataGridView
    Private ReadOnly _grpDetails As GroupBox

    Private ReadOnly _btnSearch As Button
    Private ReadOnly _btnClearSearch As Button
    Private ReadOnly _btnNew As Button
    Private ReadOnly _btnEdit As Button
    Private ReadOnly _btnSave As Button
    Private ReadOnly _btnCancel As Button
    Private ReadOnly _btnDelete As Button
    Private ReadOnly _btnClose As Button

    Private _mode As ModeEdition = ModeEdition.Consultation

    Protected Sub New(moduleName As String,
                      title As String,
                      idColumnName As String,
                      mainColumnName As String,
                      context As IReferentielShellContext,
                      fields As IEnumerable(Of CrudFieldDefinition),
                      loadAllFunc As Func(Of DataTable),
                      searchFunc As Func(Of String, DataTable),
                      insertFunc As Func(Of Dictionary(Of String, Object), ULong),
                      updateFunc As Action(Of ULong, Dictionary(Of String, Object)),
                      deleteFunc As Action(Of ULong),
                      Optional buildDeleteMessageFunc As Func(Of ULong, Dictionary(Of String, Object), String) = Nothing)

        _moduleName = moduleName
        _context = context
        _idColumnName = idColumnName
        _mainColumnName = mainColumnName
        _fields = fields.ToList()

        _loadAllFunc = loadAllFunc
        _searchFunc = searchFunc
        _insertFunc = insertFunc
        _updateFunc = updateFunc
        _deleteFunc = deleteFunc
        _buildDeleteMessageFunc = buildDeleteMessageFunc

        Dim root As New Panel With {
            .Dock = DockStyle.Fill,
            .Padding = New Padding(8),
            .BackColor = Color.FloralWhite,
            .BorderStyle = BorderStyle.Fixed3D
        }

        _lblTitle = New Label With {
            .Dock = DockStyle.Top,
            .AutoSize = True,
            .Font = New Font("Calibri", 14.0F, FontStyle.Bold),
            .Padding = New Padding(0, 0, 0, 4),
            .Text = title
        }

        Dim topPanel As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 48,
            .Padding = New Padding(8)
        }

        _txtSearch = New TextBox With {
            .Location = New Point(88, 10),
            .Size = New Size(260, 23)
        }
        AddHandler _txtSearch.KeyDown, AddressOf txtSearch_KeyDown

        Dim lblSearch As New Label With {
            .Location = New Point(16, 13),
            .AutoSize = True,
            .Text = "Rechercher"
        }

        _btnSearch = New Button With {
            .Location = New Point(364, 10),
            .Size = New Size(90, 23),
            .Text = "Filtrer",
            .UseVisualStyleBackColor = True
        }
        AddHandler _btnSearch.Click, AddressOf btnSearch_Click

        _btnClearSearch = New Button With {
            .Location = New Point(472, 10),
            .Size = New Size(32, 23),
            .Text = "X",
            .UseVisualStyleBackColor = True
        }
        AddHandler _btnClearSearch.Click, AddressOf btnClearSearch_Click

        _lblCount = New Label With {
            .AutoSize = True,
            .Location = New Point(760, 13),
            .Text = "0 ligne(s)"
        }

        topPanel.Controls.Add(lblSearch)
        topPanel.Controls.Add(_txtSearch)
        topPanel.Controls.Add(_btnSearch)
        topPanel.Controls.Add(_btnClearSearch)
        topPanel.Controls.Add(_lblCount)

        Dim actionsPanel As New Panel With {
            .Dock = DockStyle.Bottom,
            .Height = 48,
            .Padding = New Padding(8)
        }

        _btnNew = New Button With {.Location = New Point(135, 14), .Size = New Size(75, 23), .Text = "Nouveau", .UseVisualStyleBackColor = True}
        _btnEdit = New Button With {.Location = New Point(238, 14), .Size = New Size(75, 23), .Text = "Modifier", .UseVisualStyleBackColor = True}
        _btnSave = New Button With {.Location = New Point(341, 14), .Size = New Size(75, 23), .Text = "Enregistrer", .UseVisualStyleBackColor = True, .Enabled = False}
        _btnCancel = New Button With {.Location = New Point(444, 14), .Size = New Size(75, 23), .Text = "Annuler", .UseVisualStyleBackColor = True, .Enabled = False}
        _btnDelete = New Button With {.Location = New Point(547, 14), .Size = New Size(75, 23), .Text = "Supprimer", .UseVisualStyleBackColor = True}
        _btnClose = New Button With {.Location = New Point(650, 14), .Size = New Size(110, 23), .Text = "Retour accueil", .UseVisualStyleBackColor = True}

        AddHandler _btnNew.Click, AddressOf btnNew_Click
        AddHandler _btnEdit.Click, AddressOf btnEdit_Click
        AddHandler _btnSave.Click, AddressOf btnSave_Click
        AddHandler _btnCancel.Click, AddressOf btnCancel_Click
        AddHandler _btnDelete.Click, AddressOf btnDelete_Click
        AddHandler _btnClose.Click, AddressOf btnClose_Click

        actionsPanel.Controls.Add(_btnNew)
        actionsPanel.Controls.Add(_btnEdit)
        actionsPanel.Controls.Add(_btnSave)
        actionsPanel.Controls.Add(_btnCancel)
        actionsPanel.Controls.Add(_btnDelete)
        actionsPanel.Controls.Add(_btnClose)

        Dim center As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 2
        }
        center.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520.0F))
        center.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))

        _dgv = New DataGridView With {
            .Dock = DockStyle.Fill,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False,
            .RowHeadersVisible = False
        }
        AddHandler _dgv.SelectionChanged, AddressOf dgv_SelectionChanged

        _grpDetails = New GroupBox With {
            .Text = "Détails",
            .Dock = DockStyle.Fill,
            .Padding = New Padding(6)
        }

        Dim detailsGrid As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .ColumnCount = 2,
            .Padding = New Padding(8, 16, 8, 8)
        }
        detailsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140.0F))
        detailsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))

        Dim rowIndex As Integer = 0
        For Each field In _fields
            detailsGrid.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Dim lbl As New Label With {
                .AutoSize = True,
                .Text = field.Label,
                .Margin = New Padding(3, 8, 3, 4)
            }
            detailsGrid.Controls.Add(lbl, 0, rowIndex)

            Dim ctrl As Control = BuildFieldControl(field)
            ctrl.Margin = New Padding(3, 4, 3, 8)
            ctrl.Dock = DockStyle.Fill
            detailsGrid.Controls.Add(ctrl, 1, rowIndex)
            _controlByField(field.Name) = ctrl
            rowIndex += 1
        Next

        detailsGrid.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        _lblMode = New Label With {
            .Text = "Consultation",
            .ForeColor = Color.Gray,
            .AutoSize = True,
            .Margin = New Padding(3, 8, 3, 4)
        }
        detailsGrid.Controls.Add(New Label With {.Text = "Mode", .AutoSize = True, .Margin = New Padding(3, 8, 3, 4)}, 0, rowIndex)
        detailsGrid.Controls.Add(_lblMode, 1, rowIndex)

        _txtId = New TextBox With {
            .Visible = False,
            .TabStop = False
        }
        detailsGrid.Controls.Add(_txtId, 1, rowIndex + 1)

        _grpDetails.Controls.Add(detailsGrid)

        center.Controls.Add(_dgv, 0, 0)
        center.Controls.Add(_grpDetails, 1, 0)

        root.Controls.Add(center)
        root.Controls.Add(actionsPanel)
        root.Controls.Add(topPanel)
        root.Controls.Add(_lblTitle)

        Controls.Add(root)
        Dock = DockStyle.Fill

        AddHandler Load, AddressOf UC_SimpleReferentielCrud_Load
    End Sub

    Private Function BuildFieldControl(field As CrudFieldDefinition) As Control
        Select Case field.Kind
            Case CrudFieldKind.MultilineText
                Return New TextBox With {
                    .Multiline = True,
                    .Height = 72,
                    .ScrollBars = ScrollBars.Vertical,
                    .ReadOnly = field.ReadOnly,
                    .MaxLength = If(field.MaxLength > 0, field.MaxLength, 32767)
                }

            Case CrudFieldKind.Boolean
                Return New CheckBox With {
                    .AutoSize = True
                }

            Case CrudFieldKind.Integer
                Return New NumericUpDown With {
                    .Maximum = Integer.MaxValue,
                    .Minimum = 0,
                    .ThousandsSeparator = True,
                    .ReadOnly = field.ReadOnly
                }

            Case CrudFieldKind.Date
                Return New DateTimePicker With {
                    .Format = DateTimePickerFormat.Short,
                    .ShowCheckBox = True
                }

            Case Else
                Return New TextBox With {
                    .ReadOnly = field.ReadOnly,
                    .MaxLength = If(field.MaxLength > 0, field.MaxLength, 32767),
                    .CharacterCasing = field.CharacterCasing
                }
        End Select
    End Function

    Private Sub UC_SimpleReferentielCrud_Load(sender As Object, e As EventArgs)
        Try
            InitToolTips()
            LoadGrid()
            SetMode(ModeEdition.Consultation)
            SetStatus($"{_moduleName} chargé.", FormStatusKind.Success)
        Catch ex As Exception
            GestionLog.EcrireLog($"UI: erreur chargement {_moduleName}.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus($"Erreur lors du chargement de {_moduleName}.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub InitToolTips()
        Dim tip = _context?.SharedToolTip
        If tip Is Nothing Then Return

        tip.SetToolTip(_btnSearch, "Appliquer le filtre")
        tip.SetToolTip(_btnClearSearch, "Effacer le filtre")
        tip.SetToolTip(_btnNew, "Créer un nouvel élément")
        tip.SetToolTip(_btnEdit, "Passer en mode modification")
        tip.SetToolTip(_btnSave, "Enregistrer")
        tip.SetToolTip(_btnCancel, "Annuler")
        tip.SetToolTip(_btnDelete, "Supprimer l'élément sélectionné")
        tip.SetToolTip(_btnClose, "Retour au portail")
    End Sub

    Private Sub SetStatus(message As String, Optional statusKind As FormStatusKind = FormStatusKind.Info)
        _context?.SetStatus(message, statusKind)
    End Sub

    Private Sub SetContext(mode As ModeEdition)
        _context?.SetContext(_moduleName, mode)
    End Sub

    Private Sub SetMode(newMode As ModeEdition)
        _mode = newMode

        Dim isEdit As Boolean = (newMode = ModeEdition.Nouveau OrElse newMode = ModeEdition.Modification)
        Dim hasSelection As Boolean = (GetSelectedId() <> 0UL)

        _dgv.Enabled = Not isEdit
        _txtSearch.Enabled = Not isEdit
        _btnSearch.Enabled = Not isEdit
        _btnClearSearch.Enabled = Not isEdit

        _btnNew.Enabled = Not isEdit
        _btnEdit.Enabled = (Not isEdit) AndAlso hasSelection
        _btnDelete.Enabled = (Not isEdit) AndAlso hasSelection

        _btnSave.Enabled = isEdit
        _btnCancel.Enabled = isEdit
        _btnClose.Enabled = True

        For Each field In _fields
            Dim ctrl = _controlByField(field.Name)
            Select Case True
                Case TypeOf ctrl Is TextBox
                    Dim txt = DirectCast(ctrl, TextBox)
                    txt.Enabled = isEdit
                    txt.ReadOnly = (Not isEdit) OrElse field.ReadOnly

                Case TypeOf ctrl Is NumericUpDown
                    Dim nud = DirectCast(ctrl, NumericUpDown)
                    nud.Enabled = isEdit AndAlso Not field.ReadOnly

                Case TypeOf ctrl Is CheckBox
                    Dim chk = DirectCast(ctrl, CheckBox)
                    chk.Enabled = isEdit AndAlso Not field.ReadOnly

                Case TypeOf ctrl Is DateTimePicker
                    Dim dtp = DirectCast(ctrl, DateTimePicker)
                    dtp.Enabled = isEdit AndAlso Not field.ReadOnly
            End Select
        Next

        _lblMode.Text = newMode.ToString()
        SetContext(newMode)

        Select Case newMode
            Case ModeEdition.Nouveau
                SetStatus("Création en cours...")
            Case ModeEdition.Modification
                SetStatus("Modification en cours...")
            Case Else
                SetStatus("Prêt.")
        End Select

        Dim hostForm As Form = TryCast(FindForm(), Form)
        If hostForm IsNot Nothing Then
            ApplyEditionVisualState(hostForm, isEdit, _btnSave, _btnCancel, _btnClose, _grpDetails)
        End If
    End Sub

    Private Sub LoadGrid(Optional searchText As String = "")
        Dim dt As DataTable
        If searchText.Trim() = "" Then
            dt = _loadAllFunc()
        ElseIf _searchFunc IsNot Nothing Then
            dt = _searchFunc(searchText.Trim())
        Else
            dt = _loadAllFunc()
        End If

        _dgv.DataSource = dt
        FormatReferentielGrid(_dgv)
        If _dgv.Columns.Contains("created_at") Then _dgv.Columns("created_at").Visible = False
        If _dgv.Columns.Contains("updated_at") Then _dgv.Columns("updated_at").Visible = False

        _lblCount.Text = $"{dt.Rows.Count} ligne(s)"

        If Not SelectFirstRowSafe() Then
            ClearDetails()
            _txtId.Text = ""
        End If
    End Sub

    Private Function SelectFirstRowSafe() As Boolean
        If _dgv Is Nothing OrElse _dgv.Rows.Count = 0 Then Return False

        If Not String.IsNullOrWhiteSpace(_mainColumnName) AndAlso _dgv.Columns.Contains(_mainColumnName) Then
            Return SelectFirstRow(_dgv, _mainColumnName)
        End If

        For Each col As DataGridViewColumn In _dgv.Columns
            If col.Visible Then
                Return SelectFirstRow(_dgv, col.Name)
            End If
        Next

        Return False
    End Function

    Private Sub BindSelectedToDetails()
        Dim row = _dgv.CurrentRow
        If row Is Nothing Then
            ClearDetails()
            _txtId.Text = ""
            Exit Sub
        End If

        If _dgv.Columns.Contains(_idColumnName) Then
            _txtId.Text = If(row.Cells(_idColumnName).Value, "").ToString()
        End If

        For Each field In _fields
            Dim value As Object = Nothing
            If _dgv.Columns.Contains(field.Name) Then
                value = row.Cells(field.Name).Value
            End If
            SetControlValue(field.Name, value)
        Next
    End Sub

    Private Sub SetControlValue(fieldName As String, value As Object)
        If Not _controlByField.ContainsKey(fieldName) Then Return
        Dim ctrl = _controlByField(fieldName)

        Dim isNull As Boolean = (value Is Nothing OrElse value Is DBNull.Value)

        Select Case True
            Case TypeOf ctrl Is TextBox
                DirectCast(ctrl, TextBox).Text = If(isNull, "", value.ToString())

            Case TypeOf ctrl Is NumericUpDown
                Dim nud = DirectCast(ctrl, NumericUpDown)
                If isNull Then
                    nud.Value = 0D
                Else
                    Dim n As Decimal
                    If Decimal.TryParse(value.ToString(), n) Then
                        n = Math.Min(n, nud.Maximum)
                        n = Math.Max(n, nud.Minimum)
                        nud.Value = n
                    Else
                        nud.Value = 0D
                    End If
                End If

            Case TypeOf ctrl Is CheckBox
                Dim chk = DirectCast(ctrl, CheckBox)
                chk.Checked = (Not isNull) AndAlso ToBoolean(value)

            Case TypeOf ctrl Is DateTimePicker
                Dim dtp = DirectCast(ctrl, DateTimePicker)
                If isNull Then
                    dtp.Checked = False
                Else
                    Dim d As DateTime
                    If DateTime.TryParse(value.ToString(), d) Then
                        dtp.Value = d
                        dtp.Checked = True
                    Else
                        dtp.Checked = False
                    End If
                End If
        End Select
    End Sub

    Private Function CollectFieldValues() As Dictionary(Of String, Object)
        Dim values As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

        For Each field In _fields
            Dim ctrl = _controlByField(field.Name)

            Select Case True
                Case TypeOf ctrl Is TextBox
                    values(field.Name) = DirectCast(ctrl, TextBox).Text.Trim()

                Case TypeOf ctrl Is NumericUpDown
                    values(field.Name) = Convert.ToInt32(DirectCast(ctrl, NumericUpDown).Value)

                Case TypeOf ctrl Is CheckBox
                    values(field.Name) = DirectCast(ctrl, CheckBox).Checked

                Case TypeOf ctrl Is DateTimePicker
                    Dim dtp = DirectCast(ctrl, DateTimePicker)
                    values(field.Name) = If(dtp.Checked, CType(dtp.Value.Date, Date?), Nothing)

                Case Else
                    values(field.Name) = Nothing
            End Select
        Next

        Return values
    End Function

    Private Function ValidateForm() As Boolean
        ClearErrors()

        For Each field In _fields
            If Not field.Required Then Continue For

            Dim ctrl = _controlByField(field.Name)
            If TypeOf ctrl Is CheckBox Then Continue For
            If TypeOf ctrl Is NumericUpDown Then
                Dim nud = DirectCast(ctrl, NumericUpDown)
                If nud.Value <= 0D Then
                    SetError(ctrl, $"{field.Label} doit être supérieur à 0.")
                    SetStatus($"{field.Label} invalide.", FormStatusKind.Warning)
                    ctrl.Focus()
                    Return False
                End If
                Continue For
            End If

            Dim value As String = ""
            If TypeOf ctrl Is TextBox Then
                value = DirectCast(ctrl, TextBox).Text.Trim()
            ElseIf TypeOf ctrl Is DateTimePicker Then
                Dim dtp = DirectCast(ctrl, DateTimePicker)
                value = If(dtp.Checked, dtp.Value.ToShortDateString(), "")
            End If

            If value = "" Then
                SetError(ctrl, $"{field.Label} est obligatoire.")
                SetStatus($"{field.Label} obligatoire.", FormStatusKind.Warning)
                ctrl.Focus()
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub SnapshotFromFields()
        _snapshotValues.Clear()
        Dim values = CollectFieldValues()
        For Each pair In values
            _snapshotValues(pair.Key) = pair.Value
        Next
    End Sub

    Private Sub RestoreSnapshotToFields()
        For Each field In _fields
            Dim value As Object = Nothing
            If _snapshotValues.ContainsKey(field.Name) Then
                value = _snapshotValues(field.Name)
            End If
            SetControlValue(field.Name, value)
        Next
    End Sub

    Private Sub ClearDetails()
        For Each field In _fields
            SetControlValue(field.Name, Nothing)
        Next
    End Sub

    Private Sub ClearErrors()
        _context?.SharedErrorProvider?.Clear()
    End Sub

    Private Sub SetError(ctrl As Control, message As String)
        Dim provider = _context?.SharedErrorProvider
        If provider Is Nothing OrElse ctrl Is Nothing Then Return
        provider.SetError(ctrl, message)
    End Sub

    Private Function GetSelectedId() As ULong
        Return DgvGetSelectedId(_dgv, _idColumnName)
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs)
        ClearErrors()
        ClearDetails()
        _snapshotValues.Clear()
        _txtId.Text = ""
        SetMode(ModeEdition.Nouveau)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs)
        ClearErrors()
        If GetSelectedId() = 0UL Then
            SetStatus("Aucune ligne sélectionnée.", FormStatusKind.Warning)
            Return
        End If

        SnapshotFromFields()
        SetMode(ModeEdition.Modification)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        If Not ValidateForm() Then Return

        Try
            Dim values = CollectFieldValues()
            Dim currentId = SafeULong(_txtId.Text)
            Dim savedId As ULong = currentId

            If _mode = ModeEdition.Nouveau Then
                savedId = _insertFunc(values)
                SetStatus("Création réussie.", FormStatusKind.Success)
            ElseIf _mode = ModeEdition.Modification Then
                _updateFunc(currentId, values)
                SetStatus("Modification réussie.", FormStatusKind.Success)
            End If

            LoadGrid(_txtSearch.Text)
            If savedId <> 0UL Then
                DgvSelectRowById(_dgv, _idColumnName, savedId)
            End If
            BindSelectedToDetails()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog($"UI: erreur enregistrement {_moduleName}.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'enregistrement.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        ClearErrors()

        If _mode = ModeEdition.Nouveau Then
            BindSelectedToDetails()
        ElseIf _mode = ModeEdition.Modification Then
            RestoreSnapshotToFields()
        End If

        SetMode(ModeEdition.Consultation)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        If _mode <> ModeEdition.Consultation Then
            SetStatus("Suppression impossible pendant une édition.", FormStatusKind.Warning)
            Return
        End If

        Dim id = GetSelectedId()
        If id = 0UL Then
            SetStatus("Aucune ligne sélectionnée.", FormStatusKind.Warning)
            Return
        End If

        ClearErrors()

        Dim values = CollectFieldValues()
        Dim message As String
        If _buildDeleteMessageFunc IsNot Nothing Then
            message = _buildDeleteMessageFunc(id, values)
        Else
            message = "Confirmer la suppression de l'élément sélectionné ?"
        End If

        Dim rep = MessageBox.Show(
            FindForm(),
            message,
            "Confirmation suppression",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2
        )

        If rep <> DialogResult.Yes Then
            SetStatus("Suppression annulée.")
            Return
        End If

        Try
            _deleteFunc(id)
            SetStatus("Suppression réussie.", FormStatusKind.Success)
            LoadGrid(_txtSearch.Text)
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog($"UI: erreur suppression {_moduleName}.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la suppression.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        If _context Is Nothing Then
            Dim form As Form = TryCast(FindForm(), Form)
            form?.Close()
            Return
        End If

        _context.NavigateHome()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        If _mode <> ModeEdition.Consultation Then
            SetStatus("Recherche indisponible pendant une édition.", FormStatusKind.Warning)
            Return
        End If

        Try
            LoadGrid(_txtSearch.Text)
            SetStatus("Filtre appliqué.")
        Catch ex As Exception
            GestionLog.EcrireLog($"UI: erreur recherche {_moduleName}.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs)
        _txtSearch.Clear()
        btnSearch_Click(sender, e)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            btnSearch_Click(sender, e)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub dgv_SelectionChanged(sender As Object, e As EventArgs)
        If _mode <> ModeEdition.Consultation Then Return
        BindSelectedToDetails()
    End Sub

    Protected Shared Function SafeULong(value As String) As ULong
        Dim n As ULong
        If ULong.TryParse(If(value, "").Trim(), n) Then Return n
        Return 0UL
    End Function

    Protected Shared Function ValueToString(values As IDictionary(Of String, Object), key As String) As String
        If values Is Nothing OrElse Not values.ContainsKey(key) OrElse values(key) Is Nothing Then Return ""
        Return values(key).ToString().Trim()
    End Function

    Protected Shared Function ValueToInt(values As IDictionary(Of String, Object), key As String) As Integer
        Dim n As Integer
        If values IsNot Nothing AndAlso values.ContainsKey(key) AndAlso values(key) IsNot Nothing AndAlso Integer.TryParse(values(key).ToString(), n) Then
            Return n
        End If
        Return 0
    End Function

    Protected Shared Function ValueToNullableULong(values As IDictionary(Of String, Object), key As String) As ULong?
        Dim n As ULong
        If values IsNot Nothing AndAlso values.ContainsKey(key) AndAlso values(key) IsNot Nothing Then
            Dim s = values(key).ToString().Trim()
            If s <> "" AndAlso ULong.TryParse(s, n) Then Return n
        End If
        Return Nothing
    End Function

    Protected Shared Function ValueToBool(values As IDictionary(Of String, Object), key As String, Optional defaultValue As Boolean = False) As Boolean
        If values Is Nothing OrElse Not values.ContainsKey(key) OrElse values(key) Is Nothing Then Return defaultValue
        Return ToBoolean(values(key))
    End Function

    Protected Shared Function ValueToNullableDate(values As IDictionary(Of String, Object), key As String) As Date?
        If values Is Nothing OrElse Not values.ContainsKey(key) OrElse values(key) Is Nothing Then Return Nothing

        Dim value = values(key)
        If TypeOf value Is DateTime Then
            Return DirectCast(value, DateTime)
        End If

        If TypeOf value Is Date Then
            Return DirectCast(value, Date)
        End If

        Dim d As DateTime
        If DateTime.TryParse(value.ToString(), d) Then Return d
        Return Nothing
    End Function

    Private Shared Function ToBoolean(value As Object) As Boolean
        If value Is Nothing OrElse value Is DBNull.Value Then Return False
        If TypeOf value Is Boolean Then Return CBool(value)

        Dim s As String = value.ToString().Trim()
        If s = "" Then Return False

        Dim n As Integer
        If Integer.TryParse(s, n) Then Return n <> 0

        Return s.Equals("true", StringComparison.OrdinalIgnoreCase) _
            OrElse s.Equals("yes", StringComparison.OrdinalIgnoreCase) _
            OrElse s.Equals("oui", StringComparison.OrdinalIgnoreCase)
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            RemoveHandler Load, AddressOf UC_SimpleReferentielCrud_Load
            RemoveHandler _dgv.SelectionChanged, AddressOf dgv_SelectionChanged
            RemoveHandler _txtSearch.KeyDown, AddressOf txtSearch_KeyDown
            RemoveHandler _btnSearch.Click, AddressOf btnSearch_Click
            RemoveHandler _btnClearSearch.Click, AddressOf btnClearSearch_Click
            RemoveHandler _btnNew.Click, AddressOf btnNew_Click
            RemoveHandler _btnEdit.Click, AddressOf btnEdit_Click
            RemoveHandler _btnSave.Click, AddressOf btnSave_Click
            RemoveHandler _btnCancel.Click, AddressOf btnCancel_Click
            RemoveHandler _btnDelete.Click, AddressOf btnDelete_Click
            RemoveHandler _btnClose.Click, AddressOf btnClose_Click
        End If

        MyBase.Dispose(disposing)
    End Sub
End Class
