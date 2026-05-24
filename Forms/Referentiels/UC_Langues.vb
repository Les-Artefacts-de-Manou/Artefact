Option Strict On
Option Explicit On

Public Class UC_Langues

    Private _mode As ModeEdition = UtilsForm.ModeEdition.Consultation
    Private _snapshot As Langue = Nothing
    Private _currentId As ULong = 0
    Private _context As IReferentielShellContext

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(context As IReferentielShellContext)
        Me.New()
        AttachContext(context)
    End Sub

    Public Sub AttachContext(context As IReferentielShellContext)
        _context = context
        InitToolTips()
    End Sub

    Private Function GetErrorProvider() As ErrorProvider
        If _context Is Nothing Then Return Nothing
        Return _context.SharedErrorProvider
    End Function

    Private Sub SetError(ctrl As Control, message As String)
        Dim provider = GetErrorProvider()
        If provider Is Nothing OrElse ctrl Is Nothing Then Return
        provider.SetError(ctrl, message)
    End Sub

    Private Sub SetStatus(message As String, Optional statusKind As FormStatusKind = FormStatusKind.Info)
        If _context Is Nothing Then Return
        _context.SetStatus(message, statusKind)
    End Sub

    Private Sub SetContext(mode As ModeEdition)
        If _context Is Nothing Then Return
        _context.SetContext("Gestion des langues", mode)
    End Sub

    Private Sub ClearErrors()
        Dim provider = GetErrorProvider()
        If provider Is Nothing Then Return
        provider.Clear()
    End Sub

    Private Sub InitToolTips()
        If _context?.SharedToolTip Is Nothing Then Return

        Dim tip = _context.SharedToolTip
        tip.SetToolTip(btnSearch, "Appliquer le filtre")
        tip.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        tip.SetToolTip(btnNew, "Créer une nouvelle langue")
        tip.SetToolTip(btnEdit, "Passer en mode modification")
        tip.SetToolTip(btnSave, "Enregistrer les modifications")
        tip.SetToolTip(btnCancel, "Annuler les modifications")
        tip.SetToolTip(btnDelete, "Supprimer la langue sélectionnée")
        tip.SetToolTip(btnClose, "Retour au portail")
    End Sub

    Private Sub UC_Langues_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ClearErrors()
            LoadGrid()

            If dgvLangues.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Langues chargées.", FormStatusKind.Success)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur chargement UC_Langues.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement des langues.", FormStatusKind.Error)

            MessageBox.Show(
                FindForm(),
                "Erreur lors du chargement des langues." & Environment.NewLine & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try
    End Sub

    Private Sub SetMode(newMode As ModeEdition)
        _mode = newMode

        Dim isEdit As Boolean = (newMode = UtilsForm.ModeEdition.Nouveau OrElse newMode = UtilsForm.ModeEdition.Modification)
        Dim hasSelection As Boolean = HasSelectedLangue()

        dgvLangues.Enabled = Not isEdit
        pnlTop.Enabled = Not isEdit

        btnNew.Enabled = Not isEdit
        btnEdit.Enabled = (Not isEdit) AndAlso hasSelection
        btnDelete.Enabled = (Not isEdit) AndAlso hasSelection

        btnSave.Enabled = isEdit
        btnCancel.Enabled = isEdit
        btnClose.Enabled = True

        SetDetailsEnabled(isEdit)

        Select Case newMode
            Case UtilsForm.ModeEdition.Consultation
                SetStatus("Prêt.")
            Case UtilsForm.ModeEdition.Nouveau
                SetStatus("Création d'une nouvelle langue...")
            Case UtilsForm.ModeEdition.Modification
                SetStatus("Modification en cours...")
        End Select

        lblMode.Text = newMode.ToString()
        SetContext(newMode)

        Dim hostForm As Form = TryCast(Me.FindForm(), Form)
        If hostForm IsNot Nothing Then
            ApplyEditionVisualState(hostForm, isEdit, btnSave, btnCancel, btnClose, grpDetails)
        End If
    End Sub

    Private Sub SetDetailsEnabled(enabled As Boolean)
        txtNomLangue.Enabled = enabled
        txtAbrevLangue.Enabled = enabled
        txtIso639_1.Enabled = enabled
        txtIso639_2.Enabled = enabled

        txtCodeLangue.ReadOnly = True
        txtCodeLangue.TabStop = False

        txtIdLangue.Visible = False
        txtIdLangue.TabStop = False
    End Sub

    Private Function HasSelectedLangue() As Boolean
        Return (DgvGetSelectedId(dgvLangues, "id_langue") <> 0UL)
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearErrors()
        _snapshot = Nothing

        ClearDetails()
        SetMode(ModeEdition.Nouveau)
        txtNomLangue.Focus()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ClearErrors()

        If Not HasSelectedLangue() Then
            SetStatus("Aucune langue sélectionnée.", FormStatusKind.Warning)
            Return
        End If

        SnapshotFromFields()

        SetMode(ModeEdition.Modification)
        txtNomLangue.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateForm() Then Exit Sub

        Try
            Dim langue As New Langue With {
                .IdLangue = SafeULong(txtIdLangue.Text),
                .NomLangue = txtNomLangue.Text.Trim(),
                .AbrevLangue = txtAbrevLangue.Text.Trim(),
                .Iso639_1 = txtIso639_1.Text.Trim(),
                .Iso639_2 = txtIso639_2.Text.Trim()
            }

            Dim newId As ULong = 0UL

            If _mode = ModeEdition.Nouveau Then
                newId = GestionReferentiel.Langues_Insert(langue)
                SetStatus("Langue créée avec succès.", FormStatusKind.Success)

            ElseIf _mode = ModeEdition.Modification Then
                GestionReferentiel.Langues_Update(langue)
                newId = langue.IdLangue
                SetStatus("Langue modifiée avec succès.", FormStatusKind.Success)
            End If

            LoadGrid()
            DgvSelectRowById(dgvLangues, "id_langue", newId)
            BindSelectedToDetails()

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnSave_Click UC_Langues.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'enregistrement.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearErrors()

        If _mode = ModeEdition.Nouveau Then
            BindSelectedToDetails()
        ElseIf _mode = ModeEdition.Modification Then
            RestoreSnapshotToFields()
            _snapshot = Nothing
        End If

        SetMode(ModeEdition.Consultation)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _mode <> ModeEdition.Consultation Then
            SetStatus("Suppression impossible pendant une édition.", FormStatusKind.Warning)
            Return
        End If

        If Not HasSelectedLangue() Then
            SetStatus("Aucune langue sélectionnée.", FormStatusKind.Warning)
            Return
        End If

        ClearErrors()

        Dim current As Langue = GetCurrentLangueFromFields()

        If current.IdLangue = 0UL Then
            SetStatus("Identifiant langue invalide.", FormStatusKind.Warning)
            Return
        End If

        Dim labelNom As String = If(current.NomLangue, "").Trim()
        Dim labelAbrev As String = If(current.AbrevLangue, "").Trim()

        Dim nbAuteurs As Integer = GestionReferentiel.Langues_CountUsageInAuteurs(current.IdLangue)
        Dim nbLivres As Integer = GestionReferentiel.Langues_CountUsageInLivres(current.IdLangue)

        Dim msg As String

        If nbAuteurs = 0 AndAlso nbLivres = 0 Then
            If labelNom <> "" AndAlso labelAbrev <> "" Then
                msg = $"Supprimer la langue '{labelNom}' ({labelAbrev}) ?"
            ElseIf labelNom <> "" Then
                msg = $"Supprimer la langue '{labelNom}' ?"
            Else
                msg = "Supprimer la langue sélectionnée ?"
            End If
        Else
            Dim labelLangue As String

            If labelNom <> "" AndAlso labelAbrev <> "" Then
                labelLangue = $"la langue '{labelNom}' ({labelAbrev})"
            ElseIf labelNom <> "" Then
                labelLangue = $"la langue '{labelNom}'"
            Else
                labelLangue = "la langue sélectionnée"
            End If

            msg =
                $"Attention : {labelLangue} est encore utilisée." & Environment.NewLine & Environment.NewLine &
                $"- Auteurs : {nbAuteurs}" & Environment.NewLine &
                $"- Livres  : {nbLivres}" & Environment.NewLine & Environment.NewLine &
                "Si vous la supprimez, les références correspondantes seront vidées automatiquement (mise à NULL)." & Environment.NewLine & Environment.NewLine &
                "Confirmer la suppression ?"
        End If

        Dim rep = MessageBox.Show(
            FindForm(),
            msg,
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
            GestionReferentiel.Langues_Delete(current.IdLangue)
            SetStatus("Langue supprimée.", FormStatusKind.Success)

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnDelete_Click (Langues_Delete).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la suppression.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If _context Is Nothing Then
            Dim form As Form = TryCast(Me.FindForm(), Form)
            form?.Close()
            Return
        End If

        _context.NavigateHome()
    End Sub

    Private Sub LoadGrid()
        Try
            Dim dt As DataTable = GestionReferentiel.Langues_GetAll()

            dgvLangues.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvLangues)

            If dgvLangues.Columns.Contains("id_langue") Then dgvLangues.Columns("id_langue").Visible = False
            If dgvLangues.Columns.Contains("created_at") Then dgvLangues.Columns("created_at").Visible = False
            If dgvLangues.Columns.Contains("updated_at") Then dgvLangues.Columns("updated_at").Visible = False

            lblCount.Text = $"{dt.Rows.Count} langue(s)"

            If UtilsForm.SelectFirstRow(dgvLangues, "nom_langue") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid UC_Langues.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement des langues.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub BindSelectedToDetails()
        If dgvLangues.CurrentRow Is Nothing Then
            ClearDetails()
            _currentId = 0
            SetMode(ModeEdition.Consultation)
            Return
        End If

        Dim row = dgvLangues.CurrentRow

        txtIdLangue.Text = row.Cells("id_langue").Value?.ToString()
        txtNomLangue.Text = row.Cells("nom_langue").Value?.ToString()
        txtAbrevLangue.Text = row.Cells("abrev_langue").Value?.ToString()
        txtIso639_1.Text = row.Cells("iso639_1").Value?.ToString()
        txtIso639_2.Text = row.Cells("iso639_2").Value?.ToString()
        txtCodeLangue.Text = row.Cells("code_langue").Value?.ToString()

        If Not String.IsNullOrWhiteSpace(txtIdLangue.Text) Then
            _currentId = Convert.ToUInt64(txtIdLangue.Text)
        Else
            _currentId = 0
        End If

        SetMode(ModeEdition.Consultation)
    End Sub

    Private Sub ClearDetails()
        txtIdLangue.Clear()
        txtNomLangue.Clear()
        txtAbrevLangue.Clear()
        txtIso639_1.Clear()
        txtIso639_2.Clear()
        txtCodeLangue.Clear()
    End Sub

    Private Sub dgvLangues_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLangues.SelectionChanged
        If _mode <> ModeEdition.Consultation Then Return
        BindSelectedToDetails()
    End Sub

    Private Sub SnapshotFromFields()
        _snapshot = New Langue With {
            .IdLangue = SafeULong(txtIdLangue.Text),
            .NomLangue = txtNomLangue.Text,
            .AbrevLangue = txtAbrevLangue.Text,
            .Iso639_1 = txtIso639_1.Text,
            .Iso639_2 = txtIso639_2.Text,
            .CodeLangue = txtCodeLangue.Text
        }
    End Sub

    Private Sub RestoreSnapshotToFields()
        If _snapshot Is Nothing Then Return

        txtIdLangue.Text = _snapshot.IdLangue.ToString()
        txtNomLangue.Text = _snapshot.NomLangue
        txtAbrevLangue.Text = _snapshot.AbrevLangue
        txtIso639_1.Text = _snapshot.Iso639_1
        txtIso639_2.Text = _snapshot.Iso639_2
        txtCodeLangue.Text = _snapshot.CodeLangue
    End Sub

    Private Function SafeULong(value As String) As ULong
        Dim t = If(value, "").Trim()
        If t = "" Then Return 0UL
        Dim n As ULong
        If ULong.TryParse(t, n) Then Return n
        Return 0UL
    End Function

    Private Function ValidateForm() As Boolean
        ClearErrors()

        If String.IsNullOrWhiteSpace(txtNomLangue.Text) Then
            SetError(txtNomLangue, "Le nom est obligatoire.")
            SetStatus("Nom obligatoire.", FormStatusKind.Warning)
            txtNomLangue.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtAbrevLangue.Text) Then
            SetError(txtAbrevLangue, "L'abréviation est obligatoire.")
            SetStatus("Abréviation obligatoire.", FormStatusKind.Warning)
            txtAbrevLangue.Focus()
            Return False
        End If

        Dim iso639_1 As String = txtIso639_1.Text.Trim()
        Dim iso639_2 As String = txtIso639_2.Text.Trim()

        If iso639_1.Length <> 0 AndAlso iso639_1.Length <> 2 Then
            SetError(txtIso639_1, "ISO 639-1 doit contenir exactement 2 caractères.")
            SetStatus("ISO 639-1 invalide.", FormStatusKind.Warning)
            txtIso639_1.Focus()
            Return False
        End If

        If iso639_2.Length <> 0 AndAlso iso639_2.Length <> 3 Then
            SetError(txtIso639_2, "ISO 639-2 doit contenir exactement 3 caractères.")
            SetStatus("ISO 639-2 invalide.", FormStatusKind.Warning)
            txtIso639_2.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function GetCurrentLangueFromFields() As Langue
        Return New Langue With {
            .IdLangue = SafeULong(txtIdLangue.Text),
            .NomLangue = txtNomLangue.Text.Trim(),
            .AbrevLangue = txtAbrevLangue.Text.Trim(),
            .Iso639_1 = txtIso639_1.Text.Trim(),
            .Iso639_2 = txtIso639_2.Text.Trim(),
            .CodeLangue = txtCodeLangue.Text.Trim()
        }
    End Function

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplySearch()
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Clear()
        ApplySearch()
    End Sub

    Private Sub ApplySearch()
        If _mode <> ModeEdition.Consultation Then
            SetStatus("Recherche indisponible pendant une édition.", FormStatusKind.Warning)
            Return
        End If

        Try
            Dim searchText As String = txtSearch.Text.Trim()
            Dim dt As DataTable

            If searchText = "" Then
                dt = GestionReferentiel.Langues_GetAll()
                SetStatus("Liste complète affichée.")
            Else
                dt = GestionReferentiel.Langues_GetBySearch(searchText)
                SetStatus($"Filtre appliqué : '{searchText}'")
            End If

            dgvLangues.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvLangues)

            If dgvLangues.Columns.Contains("id_langue") Then dgvLangues.Columns("id_langue").Visible = False
            If dgvLangues.Columns.Contains("created_at") Then dgvLangues.Columns("created_at").Visible = False
            If dgvLangues.Columns.Contains("updated_at") Then dgvLangues.Columns("updated_at").Visible = False

            lblCount.Text = $"{dt.Rows.Count} langue(s)"

            If UtilsForm.SelectFirstRow(dgvLangues, "nom_langue") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch UC_Langues.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.", FormStatusKind.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplySearch()
            e.SuppressKeyPress = True
        End If
    End Sub

End Class
