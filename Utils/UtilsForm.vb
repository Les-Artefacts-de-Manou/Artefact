'===================================================================================================
'📌 UtilsForm V1.4 - Module
' Version : V1.0
' Date    : 21/02/2026
' Auteur  : Joëlle

'Le Module UtilsForm est une classe utilitaire dédiée à la gestion des formulaires Windows Forms. 
'===================================================================================================
Imports System.Windows.Forms

Public Module UtilsForm

    Private ReadOnly StatusInfoColor As Color = Color.SteelBlue
    Private ReadOnly StatusSuccessColor As Color = Color.ForestGreen
    Private ReadOnly StatusWarningColor As Color = Color.DarkOrange
    Private ReadOnly StatusErrorColor As Color = Color.Firebrick

    Private ReadOnly DetailEditForeColor As Color = Color.FromArgb(48, 78, 120)
    Private ReadOnly EditableBackColor As Color = Color.White
    Private ReadOnly ReadOnlyBackColor As Color = Color.FromArgb(245, 245, 245)
    Private ReadOnly EditToolStripBackColor As Color = Color.FromArgb(248, 248, 248)

    Public Enum FormStatusKind
        Info = 0
        Success = 1
        Warning = 2
        [Error] = 3
    End Enum

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' ModeEdition - Enumération
    '
    '•	Enumération pour les modes d'édition des formulaires : Consultation, Nouveau, Modification.
    '------------------------------------------------------------
    Public Enum ModeEdition
        Consultation = 0
        Nouveau = 1
        Modification = 2
    End Enum

    '------------------------------------------------------------
    ' 📌 V1.0 - 03/03/2026
    ' DgvGetSelectedId

    ' Retourne l'ID (ULong) de la ligne courante,
    ' basé sur le nom de colonne (ex: "id_langue").
    ' Renvoie 0 si aucune sélection valide.
    '------------------------------------------------------------
    Public Function GetSelectedRowOrNothing(dgv As DataGridView) As DataGridViewRow

        If dgv Is Nothing Then Return Nothing

        If dgv.SelectedRows.Count > 0 Then
            Return dgv.SelectedRows(0)
        End If

        If dgv.CurrentRow Is Nothing Then Return Nothing

        Return dgv.CurrentRow

    End Function

    Public Function DgvGetSelectedId(dgv As DataGridView, idColumnName As String) As ULong

        Dim row As DataGridViewRow = GetSelectedRowOrNothing(dgv)
        If row Is Nothing Then Return 0UL
        If Not dgv.Columns.Contains(idColumnName) Then Return 0UL

        Dim obj As Object = row.Cells(idColumnName).Value
        If obj Is Nothing OrElse obj Is DBNull.Value Then Return 0UL

        Dim n As ULong
        If ULong.TryParse(obj.ToString(), n) Then Return n
        Return 0UL

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 03/03/2026
    ' DgvSelectRowById

    '  Repositionne la grille sur une ligne correspondant à un ID.
    '------------------------------------------------------------
    Public Sub DgvSelectRowById(dgv As DataGridView, idColumnName As String, idValue As ULong)

        If dgv Is Nothing OrElse idValue = 0UL Then Exit Sub
        If Not dgv.Columns.Contains(idColumnName) Then Exit Sub

        For Each row As DataGridViewRow In dgv.Rows
            Dim obj = row.Cells(idColumnName).Value
            If obj Is Nothing OrElse obj Is DBNull.Value Then Continue For

            Dim n As ULong
            If ULong.TryParse(obj.ToString(), n) AndAlso n = idValue Then
                row.Selected = True
                ' On met un CurrentCell non masqué si possible
                Dim colIndex As Integer = Math.Min(1, dgv.Columns.Count - 1)
                dgv.CurrentCell = row.Cells(colIndex)
                Exit Sub
            End If
        Next

    End Sub

    '------------------------------------------------------------
    ' 📌 UtilsForms.vb
    ' Version : V1.3
    ' Date    : 03/03/2026
    '
    ' Évolution :
    ' - V1.3 : Standardisation visuelle des DataGridView référentiels
    '------------------------------------------------------------

    Public Sub FormatReferentielGrid(dgv As DataGridView)
        If dgv Is Nothing Then Exit Sub

        ' --- Général
        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToResizeRows = False
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None

        ' --- Header
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font(dgv.Font, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.ColumnHeadersHeight = 32

        ' --- Lignes alternées
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)

        ' --- Sélection
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 255)
        dgv.DefaultCellStyle.SelectionForeColor = Color.Black

        ' --- Alignement cellules par défaut
        dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        ' --- Colonnes : header, largeur, alignement, mise en valeur
        For Each col As DataGridViewColumn In dgv.Columns

            ' 1. Nettoyage du HeaderText
            col.HeaderText = BuildGridHeaderText(col.Name)

            ' 2. Colonnes techniques cachées
            If col.Name.StartsWith("id_", StringComparison.OrdinalIgnoreCase) Then
                col.Visible = False
                Continue For
            End If

            ' 3. Largeur / style selon type de colonne
            If IsMainTextColumn(col.Name) Then

                Dim baseFont As Font = If(dgv.Font, Control.DefaultFont)

                col.MinimumWidth = 140

                If dgv.IsHandleCreated Then
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Else
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End If

                col.DefaultCellStyle.Font = New Font(baseFont, FontStyle.Bold)
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True

            ElseIf IsBusinessCodeColumn(col.Name) Then

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                col.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60)

            ElseIf IsShortCodeColumn(col.Name) Then

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ElseIf IsNumericInfoColumn(col.Name) Then

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ElseIf IsBooleanColumn(col.Name) Then

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ElseIf col.Name.StartsWith("code_", StringComparison.OrdinalIgnoreCase) Then

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Else

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

            End If

        Next

    End Sub

    Private Function BuildGridHeaderText(columnName As String) As String

        If String.IsNullOrWhiteSpace(columnName) Then Return ""

        Select Case columnName.ToLowerInvariant()

            Case "is_actif"
                Return "Actif"

            Case "ordre_affichage"
                Return "Ordre"

            Case "code_valeur", "code_type"
                Return "Code"

            Case "libelle_valeur", "libelle_type"
                Return "Libellé"

            Case Else
                Dim txt As String = columnName.Replace("_", " ").Trim()

                If txt = "" Then Return ""

                Return Char.ToUpper(txt(0)) & txt.Substring(1)

        End Select

    End Function

    Private Function IsBusinessCodeColumn(columnName As String) As Boolean

        If String.IsNullOrWhiteSpace(columnName) Then Return False

        Select Case columnName.ToLowerInvariant()
            Case "code_type", "code_valeur"
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Function IsShortCodeColumn(columnName As String) As Boolean

        If String.IsNullOrWhiteSpace(columnName) Then Return False

        Select Case columnName.ToLowerInvariant()
            Case "iso2", "iso3", "iso639_1", "iso639_2"
                Return True
            Case Else
                Return False
        End Select

    End Function


    Private Function IsNumericInfoColumn(columnName As String) As Boolean

        If String.IsNullOrWhiteSpace(columnName) Then Return False

        Select Case columnName.ToLowerInvariant()
            Case "ordre_affichage"
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Function IsBooleanColumn(columnName As String) As Boolean

        If String.IsNullOrWhiteSpace(columnName) Then Return False

        Select Case columnName.ToLowerInvariant()
            Case "is_actif"
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Function IsMainTextColumn(columnName As String) As Boolean

        If String.IsNullOrWhiteSpace(columnName) Then Return False

        Return columnName.StartsWith("nom_", StringComparison.OrdinalIgnoreCase) _
        OrElse columnName.StartsWith("libelle_", StringComparison.OrdinalIgnoreCase)

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' IsValidEmail
    '
    ' Vérifie si une chaîne correspond à un format email simple.
    '------------------------------------------------------------
    Public Function IsValidEmail(email As String) As Boolean

        If String.IsNullOrWhiteSpace(email) Then Return False

        Try
            Dim addr = New System.Net.Mail.MailAddress(email.Trim())
            Return addr.Address = email.Trim()
        Catch
            Return False
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' OpenEmailClient
    '
    ' Ouvre le client mail par défaut avec l'adresse fournie.
    '------------------------------------------------------------
    Public Sub OpenEmailClient(email As String)

        If Not IsValidEmail(email) Then Exit Sub

        Dim psi As New ProcessStartInfo With {
            .FileName = "mailto:" & email.Trim(),
            .UseShellExecute = True
        }

        Process.Start(psi)

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' UpdateEmailTextBoxCursor
    '
    ' Met à jour le curseur d'un TextBox selon que son contenu
    ' est une adresse email valide ou non.
    '------------------------------------------------------------
    Public Sub UpdateEmailTextBoxCursor(txt As TextBox)

        If txt Is Nothing Then Exit Sub

        txt.Cursor = If(IsValidEmail(txt.Text), Cursors.Hand, Cursors.Default)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 10/03/2026
    ' IsValidUrl
    '
    ' Vérifie si une chaîne correspond à une URL absolue valide.
    '------------------------------------------------------------
    Public Function IsValidUrl(url As String) As Boolean

        If String.IsNullOrWhiteSpace(url) Then Return False

        Dim uri As Uri = Nothing

        If Uri.TryCreate(url.Trim(), UriKind.Absolute, uri) Then
            Return uri.Scheme = Uri.UriSchemeHttp OrElse uri.Scheme = Uri.UriSchemeHttps
        End If

        Return False

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 10/03/2026
    ' OpenUrl
    '
    ' Ouvre l'URL dans le navigateur par défaut.
    '------------------------------------------------------------
    Public Sub OpenUrl(url As String)

        If Not IsValidUrl(url) Then Exit Sub

        Dim psi As New ProcessStartInfo With {
        .FileName = url.Trim(),
        .UseShellExecute = True
    }

        Process.Start(psi)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 10/03/2026
    ' UpdateUrlTextBoxStyle
    '
    ' Applique le style visuel d'un champ URL.
    '------------------------------------------------------------
    Public Sub UpdateUrlTextBoxStyle(txt As TextBox)

        If txt Is Nothing Then Exit Sub

        If IsValidUrl(txt.Text) Then
            txt.ForeColor = Color.CornflowerBlue
            txt.Cursor = Cursors.Hand
        Else
            txt.ForeColor = SystemColors.WindowText
            txt.Cursor = Cursors.Default
        End If

        If txt.ReadOnly Then
            txt.BackColor = SystemColors.Window
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' SelectFirstRow
    '
    ' Sélectionne la première ligne d'un DataGridView si elle existe
    ' et positionne la cellule courante sur une colonne visible donnée.
    '
    ' Retour :
    ' - True  : une ligne a été sélectionnée
    ' - False : aucune ligne disponible
    '------------------------------------------------------------
    Public Function SelectFirstRow(dgv As DataGridView, visibleColumnName As String) As Boolean

        If dgv Is Nothing Then Return False
        If dgv.Rows.Count = 0 Then Return False
        If String.IsNullOrWhiteSpace(visibleColumnName) Then Return False
        If Not dgv.Columns.Contains(visibleColumnName) Then Return False

        dgv.ClearSelection()
        dgv.Rows(0).Selected = True
        dgv.CurrentCell = dgv.Rows(0).Cells(visibleColumnName)

        Return True

    End Function

    Public Sub SetFormStatus(statusLabel As ToolStripStatusLabel, message As String, Optional statusKind As FormStatusKind? = Nothing)

        If statusLabel Is Nothing Then Exit Sub

        Dim safeMessage As String = If(message, "").Trim()
        If safeMessage = "" Then
            safeMessage = "Prêt."
        End If

        Dim resolvedKind As FormStatusKind = If(statusKind.HasValue, statusKind.Value, FormStatusKind.Info)

        statusLabel.Text = safeMessage
        statusLabel.ForeColor = GetStatusColor(resolvedKind)

    End Sub

    Public Sub ShowCriticalError(owner As IWin32Window,
                                 statusLabel As ToolStripStatusLabel,
                                 title As String,
                                 message As String,
                                 ex As Exception)

        SetFormStatus(statusLabel, message, FormStatusKind.Error)

        Dim popupMessage As String = If(message, "").Trim()

        If ex IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ex.Message) Then
            popupMessage &= Environment.NewLine & Environment.NewLine & ex.Message.Trim()
        End If

        MessageBox.Show(owner, popupMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub

    Public Sub ApplyEditionVisualState(hostForm As Form,
                                       isEditMode As Boolean,
                                       btnSave As Button,
                                       btnCancel As Button,
                                       btnClose As Button,
                                       ParamArray detailContainers() As Control)

        If hostForm Is Nothing Then Exit Sub

        hostForm.KeyPreview = True
        hostForm.AcceptButton = GetDefaultAcceptButton(isEditMode, btnSave)

        If isEditMode AndAlso btnCancel IsNot Nothing AndAlso btnCancel.Enabled Then
            hostForm.CancelButton = btnCancel
        ElseIf btnClose IsNot Nothing AndAlso btnClose.Enabled Then
            hostForm.CancelButton = btnClose
        Else
            hostForm.CancelButton = Nothing
        End If

        If detailContainers Is Nothing Then Exit Sub

        For Each detailContainer As Control In detailContainers
            ApplyDetailContainerStyle(detailContainer, isEditMode)
        Next

    End Sub

    Private Sub ApplyDetailContainerStyle(container As Control, isEditMode As Boolean)

        If container Is Nothing Then Exit Sub

        If TypeOf container Is GroupBox Then
            container.ForeColor = If(isEditMode, DetailEditForeColor, SystemColors.ControlText)
        End If

        For Each child As Control In container.Controls
            ApplyControlEditionStyle(child, isEditMode)
        Next

    End Sub

    Private Sub ApplyControlEditionStyle(control As Control, isEditMode As Boolean)

        If control Is Nothing Then Exit Sub

        Select Case True
            Case TypeOf control Is TextBox
                Dim txt As TextBox = DirectCast(control, TextBox)
                Dim isEditable As Boolean = txt.Visible AndAlso txt.Enabled AndAlso Not txt.ReadOnly

                txt.BackColor = If(isEditable, EditableBackColor, ReadOnlyBackColor)
                txt.TabStop = isEditable

            Case TypeOf control Is RichTextBox
                Dim rtb As RichTextBox = DirectCast(control, RichTextBox)
                Dim isEditable As Boolean = rtb.Visible AndAlso rtb.Enabled AndAlso Not rtb.ReadOnly

                rtb.BackColor = If(isEditable, EditableBackColor, ReadOnlyBackColor)
                rtb.TabStop = isEditable

            Case TypeOf control Is ComboBox
                Dim cbo As ComboBox = DirectCast(control, ComboBox)
                Dim isEditable As Boolean = cbo.Visible AndAlso cbo.Enabled

                cbo.BackColor = If(isEditable, EditableBackColor, ReadOnlyBackColor)
                cbo.TabStop = isEditable

            Case TypeOf control Is NumericUpDown
                Dim nud As NumericUpDown = DirectCast(control, NumericUpDown)
                Dim isEditable As Boolean = nud.Visible AndAlso nud.Enabled AndAlso Not nud.ReadOnly

                nud.BackColor = If(isEditable, EditableBackColor, ReadOnlyBackColor)
                nud.TabStop = isEditable

            Case TypeOf control Is DateTimePicker
                Dim dtp As DateTimePicker = DirectCast(control, DateTimePicker)
                dtp.TabStop = dtp.Visible AndAlso dtp.Enabled

            Case TypeOf control Is CheckBox OrElse TypeOf control Is RadioButton
                control.TabStop = control.Visible AndAlso control.Enabled

            Case TypeOf control Is ToolStrip
                control.BackColor = If(isEditMode, EditToolStripBackColor, SystemColors.Control)
        End Select

        For Each child As Control In control.Controls
            ApplyControlEditionStyle(child, isEditMode)
        Next

    End Sub

    Private Function GetDefaultAcceptButton(isEditMode As Boolean, btnSave As Button) As Button

        If Not isEditMode Then Return Nothing
        If btnSave Is Nothing OrElse Not btnSave.Enabled Then Return Nothing

        Return btnSave

    End Function

    Private Function GetStatusColor(statusKind As FormStatusKind) As Color

        Select Case statusKind
            Case FormStatusKind.Success
                Return StatusSuccessColor
            Case FormStatusKind.Warning
                Return StatusWarningColor
            Case FormStatusKind.Error
                Return StatusErrorColor
            Case Else
                Return StatusInfoColor
        End Select

    End Function

End Module
