'====================================================================
' 📌 UC_Impression.vb
' Version : V1.0
' Date    : 24/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion du référentiel Impression.
' Transposé depuis GestionImpression.vb
'
' Règles :
' - Implémente IContextAwareUserControl
' - Utilise UserControlContext pour StatusStrip, ErrorProvider, ToolTip
' - Support RichTextBox enrichi pour les notes (RTF + TXT)
' - Logique métier identique à la Form d'origine
' - Checkbox is_actif
'
' Évolution :
' - V1.0 : Transposition depuis GestionImpression.vb
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_Impression
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'utilisation
    Private _mode As ModeEdition = ModeEdition.Consultation

    ' Snapshot pour annulation
    Private _snapshot As Impression = Nothing

    ' Identifiant de l'impression courante
    Private _currentId As ULong = 0

    ' Contexte partagé
    Private _context As UserControlContext = Nothing

#End Region

#Region "Constructeur"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "IContextAwareUserControl Implementation"

    Public Sub SetContext(context As UserControlContext) Implements IContextAwareUserControl.SetContext
        _context = context
    End Sub

    Public Sub OnActivated() Implements IContextAwareUserControl.OnActivated
        Try
            LoadGrid()

            If _context IsNot Nothing Then
                _context.NavigateToLevel("Impression")
                _context.SetStatus($"Impressions chargées : {dgvImpression.Rows.Count} élément(s).")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_Impression.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de l'activation.")
        End Try
    End Sub

    Public Sub OnDeactivated() Implements IContextAwareUserControl.OnDeactivated
        Try
            If _context IsNot Nothing Then
                _context.ClearAllErrors()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur désactivation UC_Impression.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

#End Region

#Region "Initialisation"

    Private Sub UC_Impression_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbNote)

            ' Connecter le toolbar au RichTextBox
            ucToolbar.TargetRichTextBox = rtbNote

            LoadGrid()

            If dgvImpression.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetStatus("Impressions chargées.")
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement UC_Impression.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors du chargement.")
        End Try

    End Sub

    Private Sub InitToolTips()

        If _context?.ToolTip Is Nothing Then Return

        Dim tt = _context.ToolTip

        tt.SetToolTip(btnSearch, "Appliquer le filtre")
        tt.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        tt.SetToolTip(btnNew, "Créer une nouvelle impression")
        tt.SetToolTip(btnEdit, "Passer en mode modification")
        tt.SetToolTip(btnSave, "Enregistrer les modifications")
        tt.SetToolTip(btnCancel, "Annuler les modifications")
        tt.SetToolTip(btnDelete, "Supprimer l'impression sélectionnée")
        tt.SetToolTip(txtNomImpression, "Nom de l'impression (sera normalisé en CODE_MAJUSCULE)")
        tt.SetToolTip(txtDescriptionImpression, "Description complémentaire")
        tt.SetToolTip(txtEnvieCal, "Champ libre de catégorisation ou envie Cal")
        tt.SetToolTip(rtbNote, "Note libre associée à l'impression")
        tt.SetToolTip(chkIsActif, "Actif / Inactif")

    End Sub

    ''' <summary>
    ''' Normalise le nom d'impression : majuscules, espaces -> underscores, pas de caractères spéciaux.
    ''' </summary>
    Private Sub txtNomImpression_TextChanged(sender As Object, e As EventArgs) Handles txtNomImpression.TextChanged
        If _mode = ModeEdition.Consultation Then Exit Sub

        Dim cursorPos As Integer = txtNomImpression.SelectionStart
        Dim originalText As String = txtNomImpression.Text
        Dim normalizedText As String = NormalizeImpressionName(originalText)

        If originalText <> normalizedText Then
            txtNomImpression.Text = normalizedText
            ' Restaurer la position du curseur
            txtNomImpression.SelectionStart = Math.Min(cursorPos, txtNomImpression.Text.Length)
        End If
    End Sub

    ''' <summary>
    ''' Normalise une chaîne pour un nom d'impression : majuscules, espaces -> _, supprime ' . ,
    ''' </summary>
    Private Function NormalizeImpressionName(input As String) As String
        If String.IsNullOrEmpty(input) Then Return input

        Dim result As String = input.ToUpperInvariant()
        result = result.Replace(" "c, "_"c)
        result = result.Replace("'"c, String.Empty)
        result = result.Replace("."c, String.Empty)
        result = result.Replace(","c, String.Empty)

        Return result
    End Function

#End Region

#Region "Gestion des modes"

    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        ' ✅ Utilisation des helpers pour simplifier la gestion des modes
        UtilsUCReferentiels.ConfigurerBoutonsMode(
            mode,
            HasSelectedImpression(),
            btnNew, btnEdit, btnSave, btnCancel, btnDelete
        )

        UtilsUCReferentiels.ConfigurerRecherche(
            mode,
            txtSearch, btnSearch, btnClearSearch, chkSearchNotes
        )

        ' Gestion des champs spécifiques
        Select Case mode

            Case ModeEdition.Consultation

                txtNomImpression.ReadOnly = True
                txtDescriptionImpression.ReadOnly = True
                txtEnvieCal.ReadOnly = True
                rtbNote.ReadOnly = True
                ucToolbar.Enabled = False
                chkIsActif.Enabled = False
                dgvImpression.Enabled = True

            Case ModeEdition.Nouveau, ModeEdition.Modification

                txtNomImpression.ReadOnly = False
                txtDescriptionImpression.ReadOnly = False
                txtEnvieCal.ReadOnly = False
                rtbNote.ReadOnly = False
                ucToolbar.Enabled = True
                chkIsActif.Enabled = True
                dgvImpression.Enabled = False

        End Select

        ' Mettre à jour le fil d'Ariane avec le mode
        If _context IsNot Nothing Then
            Select Case mode
                Case ModeEdition.Consultation
                    _context.NavigateToLevel("Impression")
                Case ModeEdition.Nouveau
                    _context.SetMode("Nouveau")
                Case ModeEdition.Modification
                    _context.SetMode("Modification")
            End Select
        End If

    End Sub

    Private Function HasSelectedImpression() As Boolean
        Return dgvImpression.CurrentRow IsNot Nothing AndAlso _currentId > 0UL
    End Function

#End Region

#Region "Interface utilisateur"

    Private Sub SetStatus(message As String)
        _context?.SetStatus(message)
    End Sub

    Private Sub ClearErrors()
        _context?.ClearAllErrors()
    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearErrors()
        ClearDetails()
        _snapshot = Nothing
        _currentId = 0
        chkIsActif.Checked = True
        SetMode(ModeEdition.Nouveau)
        txtNomImpression.Focus()
        SetStatus("Nouvelle impression.")
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not HasSelectedImpression() Then
            SetStatus("Aucune impression sélectionnée.")
            Return
        End If

        ClearErrors()
        SnapshotFromFields()
        SetMode(ModeEdition.Modification)
        txtNomImpression.Focus()
        SetStatus("Modification de l'impression.")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateForm() Then Exit Sub

        Try
            Dim current As Impression = GetCurrentImpressionFromFields()

            If _mode = ModeEdition.Nouveau Then
                GestionReferentiel.Impression_Insert(current)
                SetStatus("Impression créée.")
                LoadGrid()
                ' Sélectionner la nouvelle impression dans la grille (par nom)
                For i As Integer = 0 To dgvImpression.Rows.Count - 1
                    If dgvImpression.Rows(i).Cells("nom_impression").Value?.ToString() = current.NomImpression Then
                        dgvImpression.ClearSelection()
                        dgvImpression.Rows(i).Selected = True
                        dgvImpression.CurrentCell = dgvImpression.Rows(i).Cells(0)
                        BindSelectedToDetails()
                        Exit For
                    End If
                Next
            ElseIf _mode = ModeEdition.Modification Then
                current.IdImpression = _currentId
                GestionReferentiel.Impression_Update(current)
                SetStatus("Impression modifiée.")
                LoadGrid()
                ReselectCurrentImpression()
            End If

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnSave_Click (UC_Impression).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de l'enregistrement.")
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try
            If _mode = ModeEdition.Modification Then
                RestoreSnapshotToFields()
            ElseIf _mode = ModeEdition.Nouveau Then
                If dgvImpression.Rows.Count > 0 Then
                    BindSelectedToDetails()
                Else
                    ClearDetails()
                End If
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Edition annulée.")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnCancel_Click (UC_Impression).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try
            If Not HasSelectedImpression() Then
                SetStatus("Aucune impression sélectionnée.")
                Return
            End If

            Dim id As ULong = UtilsUCReferentiels.SafeULong(txtIdImpression.Text)
            Dim nomImpression As String = txtNomImpression.Text

            ' Vérifier dépendances
            Dim countLivres As Integer = GestionReferentiel.Impression_CountLivres(id)

            If countLivres > 0 Then
                MessageBox.Show(
                    $"Impossible de supprimer cette impression car {countLivres} livre(s) y sont rattachés.",
                    "Suppression impossible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                SetStatus("Suppression impossible (dépendances).")
                Return
            End If

            Dim rep = MessageBox.Show(
                $"Supprimer l'impression '{nomImpression}' ?",
                "Confirmation suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            )

            If rep <> DialogResult.Yes Then
                SetStatus("Suppression annulée.")
                Return
            End If

            GestionReferentiel.Impression_Delete(id)

            GestionLog.EcrireLog(
                $"UI: suppression impression '{nomImpression}'.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI
            )

            SetStatus("Impression supprimée.")

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnDelete_Click (UC_Impression).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la suppression.")
        End Try

    End Sub

#End Region

#Region "Synchronisation des données"

    Private Sub LoadGrid()

        Try
            Dim dt = GestionReferentiel.Impression_GetAll()
            dgvImpression.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvImpression)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvImpression,
                "id_impression", "code_impression",
                "created_at", "updated_at",
                "note_rtf", "note_txt")

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvImpression, "impression")

            ' Sélection première ligne
            If dgvImpression.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvImpression, "nom_impression")
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid (UC_Impression).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement de la grille.")
        End Try

    End Sub

    Private Sub BindSelectedToDetails()

        Try
            If dgvImpression.CurrentRow Is Nothing Then
                ClearDetails()
                Return
            End If

            Dim row = dgvImpression.CurrentRow

            txtIdImpression.Text = If(row.Cells("id_impression")?.Value?.ToString(), "")
            txtCodeImpression.Text = If(row.Cells("code_impression")?.Value?.ToString(), "")
            txtNomImpression.Text = If(row.Cells("nom_impression")?.Value?.ToString(), "")
            txtDescriptionImpression.Text = If(row.Cells("description_impression")?.Value?.ToString(), "")
            txtEnvieCal.Text = If(row.Cells("envie_Cal")?.Value?.ToString(), "")

            ' Charger notes (RTF prioritaire, fallback TXT)
            Dim noteRtf As String = If(row.Cells("note_rtf").Value, "").ToString()
            Dim noteTxt As String = If(row.Cells("note_txt").Value, "").ToString()
            RichTextNotesHelper.ChargerContenuNotes(rtbNote, noteRtf, noteTxt)

            chkIsActif.Checked = Convert.ToBoolean(row.Cells("is_actif").Value)

            _currentId = UtilsUCReferentiels.SafeULong(txtIdImpression.Text)

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur BindSelectedToDetails (UC_Impression).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub ClearDetails()

        txtIdImpression.Clear()
        txtCodeImpression.Clear()
        txtNomImpression.Clear()
        txtDescriptionImpression.Clear()
        txtEnvieCal.Clear()
        rtbNote.Clear()
        chkIsActif.Checked = True
        _currentId = 0

    End Sub

    Private Sub dgvImpression_SelectionChanged(sender As Object, e As EventArgs) Handles dgvImpression.SelectionChanged
        If _mode = ModeEdition.Consultation Then
            BindSelectedToDetails()
            SetMode(ModeEdition.Consultation)
        End If
    End Sub

    Private Sub ReselectCurrentImpression()

        If _currentId = 0UL OrElse dgvImpression.Rows.Count = 0 Then
            Return
        End If

        UtilsForm.DgvSelectRowById(dgvImpression, "id_impression", _currentId)
        BindSelectedToDetails()

    End Sub

    Private Sub SnapshotFromFields()
        _snapshot = GetCurrentImpressionFromFields()
    End Sub

    Private Sub RestoreSnapshotToFields()

        If _snapshot Is Nothing Then Return

        txtNomImpression.Text = _snapshot.NomImpression
        txtDescriptionImpression.Text = _snapshot.DescriptionImpression
        txtEnvieCal.Text = _snapshot.EnvieCal
        RichTextNotesHelper.ChargerContenuNotes(rtbNote, _snapshot.NoteRtf, _snapshot.NoteTxt)
        chkIsActif.Checked = _snapshot.IsActif

    End Sub

#End Region

#Region "Validation"

    Private Function ValidateForm() As Boolean

        ClearErrors()

        ' ✅ Utilisation du helper pour valider les champs obligatoires
        Dim isValid As Boolean = UtilsUCReferentiels.ValidateRequiredField(
            txtNomImpression,
            "Le nom de l'impression",
            _context?.ErrorProvider
        )

        If Not isValid Then
            SetStatus("Formulaire incomplet ou invalide.")
        End If

        Return isValid

    End Function

    Private Function GetCurrentImpressionFromFields() As Impression

        Return New Impression With {
            .IdImpression = _currentId,
            .CodeImpression = txtCodeImpression.Text.Trim(),
            .NomImpression = txtNomImpression.Text.Trim(),
            .DescriptionImpression = txtDescriptionImpression.Text.Trim(),
            .EnvieCal = txtEnvieCal.Text.Trim(),
            .NoteRtf = RichTextNotesHelper.GetNotesRtf(rtbNote),
            .NoteTxt = RichTextNotesHelper.GetNotesTxt(rtbNote),
            .IsActif = chkIsActif.Checked
        }

    End Function

#End Region

#Region "Recherche"

    Private Sub ApplySearch()

        Try
            Dim searchText As String = txtSearch.Text.Trim()
            Dim includeNotes As Boolean = chkSearchNotes.Checked

            Dim dt As DataTable

            If String.IsNullOrWhiteSpace(searchText) Then
                dt = GestionReferentiel.Impression_GetAll()
            Else
                dt = GestionReferentiel.Impression_GetBySearch(searchText, includeNotes)
            End If

            dgvImpression.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvImpression)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvImpression,
                "id_impression", "code_impression",
                "created_at", "updated_at",
                "note_rtf", "note_txt")

            ' Sélection première ligne
            If dgvImpression.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvImpression, "nom_impression")
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvImpression, "impression")

            SetStatus($"Recherche effectuée : {dgvImpression.Rows.Count} résultat(s).")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch (UC_Impression).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplySearch()
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Clear()
        ApplySearch()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ApplySearch()
        End If
    End Sub

#End Region

End Class
