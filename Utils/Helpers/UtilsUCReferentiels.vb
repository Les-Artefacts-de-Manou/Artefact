'------------------------------------------------------------
' 📌 V1.0 - 23/03/2026
' UtilsUCReferentiels.vb
'
'   • Utilitaires partagés pour les UserControls de référentiels
'   • Conversions DB, gestion grilles, compteurs
'------------------------------------------------------------

Imports System.Windows.Forms

Friend Module UtilsUCReferentiels

#Region "Conversions DB"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' DbToBool
    '
    '   • Convertit une valeur DB (0/1 ou DBNull) en Boolean
    '------------------------------------------------------------
    Public Function DbToBool(value As Object) As Boolean
        If value Is Nothing OrElse IsDBNull(value) Then
            Return False
        End If

        If TypeOf value Is Boolean Then
            Return CBool(value)
        End If

        If TypeOf value Is Integer OrElse TypeOf value Is Long OrElse TypeOf value Is ULong Then
            Return Convert.ToInt64(value) <> 0
        End If

        If TypeOf value Is String Then
            Dim str = value.ToString().Trim().ToLowerInvariant()
            Return str = "1" OrElse str = "true" OrElse str = "yes"
        End If

        Return False
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' DbToInt
    '
    '   • Convertit une valeur DB en Integer, retourne 0 si invalide
    '------------------------------------------------------------
    Public Function DbToInt(value As Object) As Integer
        If value Is Nothing OrElse IsDBNull(value) Then
            Return 0
        End If

        Try
            Return Convert.ToInt32(value)
        Catch
            Return 0
        End Try
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SafeULong
    '
    '   • Convertit une chaîne en ULong, retourne 0 si invalide
    '------------------------------------------------------------
    Public Function SafeULong(value As String) As ULong
        If String.IsNullOrWhiteSpace(value) Then
            Return 0UL
        End If

        Dim result As ULong
        If ULong.TryParse(value.Trim(), result) Then
            Return result
        End If

        Return 0UL
    End Function

#End Region

#Region "Gestion des grilles"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' HideTechnicalColumns
    '
    '   • Masque les colonnes techniques spécifiées dans une DataGridView
    '   • Supporte un nombre variable de noms de colonnes
    '------------------------------------------------------------
    Public Sub HideTechnicalColumns(dgv As DataGridView, ParamArray columnNames As String())
        If dgv Is Nothing Then Return

        For Each columnName In columnNames
            If dgv.Columns.Contains(columnName) Then
                dgv.Columns(columnName).Visible = False
            End If
        Next
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' MasquerColonnesTechniques (alias pour HideTechnicalColumns)
    '
    '   • Masque les colonnes techniques spécifiées dans une DataGridView
    '------------------------------------------------------------
    Public Sub MasquerColonnesTechniques(dgv As DataGridView, ParamArray columnNames As String())
        HideTechnicalColumns(dgv, columnNames)
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' SetColonneVisible
    '
    '   • Configure une colonne : visibilité, titre, largeur
    '------------------------------------------------------------
    Public Sub SetColonneVisible(dgv As DataGridView, columnName As String, visible As Boolean, headerText As String, Optional width As Integer = -1)
        If dgv Is Nothing Then Return
        If Not dgv.Columns.Contains(columnName) Then Return

        Dim col As DataGridViewColumn = dgv.Columns(columnName)
        col.Visible = visible
        col.HeaderText = headerText

        If width > 0 Then
            col.Width = width
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' GetStringValue
    '
    '   • Récupère une valeur string d'une cellule DataGridViewRow
    '------------------------------------------------------------
    Public Function GetStringValue(row As DataGridViewRow, columnName As String) As String
        If row Is Nothing OrElse Not row.DataGridView.Columns.Contains(columnName) Then Return ""

        Dim val As Object = row.Cells(columnName).Value

        If val Is Nothing OrElse IsDBNull(val) Then Return ""

        Return val.ToString()
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' GetBoolValue
    '
    '   • Récupère une valeur booléenne d'une cellule DataGridViewRow
    '------------------------------------------------------------
    Public Function GetBoolValue(row As DataGridViewRow, columnName As String) As Boolean
        If row Is Nothing OrElse Not row.DataGridView.Columns.Contains(columnName) Then Return False

        Dim val As Object = row.Cells(columnName).Value

        Return DbToBool(val)
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' GetIntValue
    '
    '   • Récupère une valeur entière d'une cellule DataGridViewRow
    '------------------------------------------------------------
    Public Function GetIntValue(row As DataGridViewRow, columnName As String, defaultValue As Integer) As Integer
        If row Is Nothing OrElse Not row.DataGridView.Columns.Contains(columnName) Then Return defaultValue

        Dim val As Object = row.Cells(columnName).Value

        If val Is Nothing OrElse IsDBNull(val) Then Return defaultValue

        Try
            Return Convert.ToInt32(val)
        Catch
            Return defaultValue
        End Try
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' UpdateCountLabel
    '
    '   • Met à jour un Label avec le nombre de lignes dans une DataGridView
    '   • Ex: "5 langue(s)", "12 contact(s)"
    '------------------------------------------------------------
    Public Sub UpdateCountLabel(lblCount As Label, dgv As DataGridView, unitName As String)
        If lblCount Is Nothing OrElse dgv Is Nothing Then Return

        lblCount.Text = $"{dgv.Rows.Count} {unitName}(s)"
    End Sub

#End Region

#Region "🆕 Factorisation UI - Ajouté le 25/03/2026"

    '------------------------------------------------------------
    ' 📌 V1.1 - 25/03/2026
    ' ConfigurerStyleGrid
    '
    ' Configure le style standard pour toutes les DataGridView des UC :
    '   • Headers en gras
    '   • Lignes en police normale
    '   • Sélection de ligne complète, pas de multi-sélection
    '   • Lecture seule
    '   • Pas d'ajout/suppression de lignes
    '------------------------------------------------------------
    Public Sub ConfigurerStyleGrid(dgv As DataGridView)
        If dgv Is Nothing Then Return

        ' Style des headers (gras)
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font(dgv.Font, FontStyle.Bold)

        ' Style des cellules (normal)
        dgv.DefaultCellStyle.Font = New Font(dgv.Font, FontStyle.Regular)

        ' Sélection
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False

        ' Lecture seule
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToOrderColumns = False
        dgv.AllowUserToResizeRows = False

        ' Comportement
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgv.RowHeadersVisible = False
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 25/03/2026
    ' ValidateRequiredField
    '
    ' Valide qu'un TextBox n'est pas vide.
    ' Si vide :
    '   • Définit une erreur sur l'ErrorProvider
    '   • Met le focus sur le TextBox
    '   • Retourne False
    ' Si OK :
    '   • Efface l'erreur
    '   • Retourne True
    '------------------------------------------------------------
    Public Function ValidateRequiredField(
        txt As TextBox,
        fieldName As String,
        errorProvider As ErrorProvider
    ) As Boolean

        If txt Is Nothing Then Return False

        If String.IsNullOrWhiteSpace(txt.Text) Then
            If errorProvider IsNot Nothing Then
                errorProvider.SetError(txt, $"{fieldName} est obligatoire.")
            End If
            txt.Focus()
            Return False
        End If

        If errorProvider IsNot Nothing Then
            errorProvider.SetError(txt, String.Empty)
        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 25/03/2026
    ' ConfigurerBoutonsMode
    '
    ' Configure l'état des boutons CRUD selon le mode d'édition.
    ' Simplifie la gestion répétitive des boutons dans SetMode().
    '
    ' Paramètres :
    '   • mode : ModeEdition (Consultation, Nouveau, Modification)
    '   • hasSelection : True si une ligne est sélectionnée dans la grid
    '   • btnNew, btnEdit, btnSave, btnCancel, btnDelete : les boutons CRUD
    '------------------------------------------------------------
    Public Sub ConfigurerBoutonsMode(
        mode As UtilsForm.ModeEdition,
        hasSelection As Boolean,
        btnNew As Button,
        btnEdit As Button,
        btnSave As Button,
        btnCancel As Button,
        btnDelete As Button
    )

        Select Case mode

            Case UtilsForm.ModeEdition.Consultation

                If btnNew IsNot Nothing Then btnNew.Enabled = True
                If btnEdit IsNot Nothing Then btnEdit.Enabled = hasSelection
                If btnSave IsNot Nothing Then btnSave.Enabled = False
                If btnCancel IsNot Nothing Then btnCancel.Enabled = False
                If btnDelete IsNot Nothing Then btnDelete.Enabled = hasSelection

            Case UtilsForm.ModeEdition.Nouveau, UtilsForm.ModeEdition.Modification

                If btnNew IsNot Nothing Then btnNew.Enabled = False
                If btnEdit IsNot Nothing Then btnEdit.Enabled = False
                If btnSave IsNot Nothing Then btnSave.Enabled = True
                If btnCancel IsNot Nothing Then btnCancel.Enabled = True
                If btnDelete IsNot Nothing Then btnDelete.Enabled = False

        End Select

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 25/03/2026
    ' ConfigurerRecherche
    '
    ' Configure l'état des contrôles de recherche selon le mode d'édition.
    ' En mode édition (Nouveau/Modification), désactive la recherche.
    ' En mode consultation, réactive la recherche.
    '
    ' Paramètres :
    '   • mode : ModeEdition
    '   • txtSearch : TextBox de recherche
    '   • btnSearch : Bouton de recherche
    '   • btnClearSearch : Bouton d'effacement de recherche
    '   • chkSearch : CheckBox optionnel (recherche dans notes, actifs, etc.)
    '------------------------------------------------------------
    Public Sub ConfigurerRecherche(
        mode As UtilsForm.ModeEdition,
        Optional txtSearch As TextBox = Nothing,
        Optional btnSearch As Button = Nothing,
        Optional btnClearSearch As Button = Nothing,
        Optional chkSearch As CheckBox = Nothing
    )

        Dim enabled As Boolean = (mode = UtilsForm.ModeEdition.Consultation)

        If txtSearch IsNot Nothing Then txtSearch.Enabled = enabled
        If btnSearch IsNot Nothing Then btnSearch.Enabled = enabled
        If btnClearSearch IsNot Nothing Then btnClearSearch.Enabled = enabled
        If chkSearch IsNot Nothing Then chkSearch.Enabled = enabled

    End Sub

#End Region

End Module
