'===================================================================================================
'📌 UtilsForm V1.4 - Module
' Version : V1.0
' Date    : 21/02/2026
' Auteur  : Joëlle

'Le Module UtilsForm est une classe utilitaire dédiée à la gestion des formulaires Windows Forms. 
'===================================================================================================

Imports System.Windows.Forms

Module UtilsForm

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' ShowFormIfNotOpen
    '
    '•	Affiche un formulaire générique de type T uniquement s'il n'est pas déjà ouvert.
    '•	Si le formulaire est déjà présent dans Application.OpenForms, il est mis au premier plan.
    '------------------------------------------------------------
    Public Sub ShowFormIfNotOpen(Of T As {Form, New})()
        For Each f As Form In Application.OpenForms
            If TypeOf f Is T Then
                f.BringToFront()
                f.Focus()
                Return
            End If
        Next

        Dim newForm As New T()
        newForm.Show()
    End Sub


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
    Public Function DgvGetSelectedId(dgv As DataGridView, idColumnName As String) As ULong

        If dgv Is Nothing OrElse dgv.CurrentRow Is Nothing Then Return 0UL
        If Not dgv.Columns.Contains(idColumnName) Then Return 0UL

        Dim obj = dgv.CurrentRow.Cells(idColumnName).Value
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

End Module
