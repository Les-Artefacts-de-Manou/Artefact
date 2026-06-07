'====================================================================
' 📌 UC_PrixLit.vb
' Version : V1.0
' Date    : 25/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl pour gérer les Prix Littéraires avec 3 niveaux hiérarchiques :
' - PrixLit (master) : nom, description, notes RTF enrichies
' - PrixLit_Catégorie (niveau 2) : libellé, description, ordre
' - PrixLit_Annee (niveau 3) : année
'
' Synchronisation : sélection PrixLit → catégories ; sélection Catégorie → années
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_PrixLit
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Contexte partagé
    Private _context As UserControlContext = Nothing

    ' Identifiants courants pour les 3 niveaux
    Private _currentIdPrixLit As ULong = 0
    Private _currentIdCategorie As ULong = 0
    Private _currentIdAnnee As ULong = 0

    ' Modes édition pour chaque niveau
    Private _modePrixLit As ModeEdition = ModeEdition.Consultation
    Private _modeCategorie As ModeEdition = ModeEdition.Consultation
    Private _modeAnnee As ModeEdition = ModeEdition.Consultation

#End Region

#Region "Interface IContextAwareUserControl"

    Public Sub SetContext(context As UserControlContext) Implements IContextAwareUserControl.SetContext
        _context = context
    End Sub

    Public Sub OnActivated() Implements IContextAwareUserControl.OnActivated
        Try
            If _context IsNot Nothing Then
                _context.NavigateToLevel("Prix littéraires")
                _context.SetMode("Gestion hiérarchique à 3 niveaux")
                SetStatus("Prix littéraires, catégories et années chargés.")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_PrixLit.",
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
                "UI: erreur désactivation UC_PrixLit.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

#End Region

#Region "Initialisation"

    Private Sub UC_PrixLit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ' Configurer le RichTextBox pour les notes
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbNotesPrixLit)
            ucRichTextToolbar.TargetRichTextBox = rtbNotesPrixLit

            ' Charger les prix littéraires
            LoadPrixLit()

            ' Modes initiaux
            SetModePrixLit(ModeEdition.Consultation)
            SetModeCategorie(ModeEdition.Consultation)
            SetModeAnnee(ModeEdition.Consultation)

            SetStatus("Prêt.")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur load UC_PrixLit.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors du chargement.")
        End Try

    End Sub

#End Region

#Region "NIVEAU 1 : PrixLit - Load & Sélection"

    Private Sub LoadPrixLit()

        Try
            Dim actifsOnly As Boolean = chkPrixLitActifs.Checked
            Dim dt As DataTable = GestionReferentiel.PrixLit_GetAll(actifsOnly)

            dgvPrixLit.DataSource = dt

            ' Configuration grid PrixLit
            ConfigurerGridPrixLit()

            lblCountPrixLit.Text = $"{dt.Rows.Count} prix"

            ' Vider catégories et années tant qu'aucun prix n'est sélectionné
            _currentIdPrixLit = 0
            _currentIdCategorie = 0
            _currentIdAnnee = 0

            dgvCategories.DataSource = Nothing
            dgvAnnees.DataSource = Nothing
            lblCountCategories.Text = "0 catégorie"
            lblCountAnnees.Text = "0 année"

            ' Vider les détails des catégories et années
            ClearCategorieDetails()
            ClearAnneeDetails()

            SetStatus($"{dt.Rows.Count} prix littéraire(s) chargé(s).")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur LoadPrixLit.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors du chargement des prix littéraires.")
        End Try

    End Sub

    Private Sub dgvPrixLit_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPrixLit.SelectionChanged

        If _modePrixLit <> ModeEdition.Consultation Then Return
        If dgvPrixLit.SelectedRows.Count = 0 Then Return

        Try
            Dim row As DataGridViewRow = dgvPrixLit.SelectedRows(0)
            _currentIdPrixLit = Convert.ToUInt64(row.Cells("id_prixLit").Value)

            BindSelectedPrixLitToDetails()
            LoadCategories()

            ' Mettre à jour l'état des boutons catégorie
            SetModeCategorie(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur sélection PrixLit.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub BindSelectedPrixLitToDetails()

        If dgvPrixLit.SelectedRows.Count = 0 Then Return

        Try
            Dim row As DataGridViewRow = dgvPrixLit.SelectedRows(0)

            txtIdPrixLit.Text = row.Cells("id_prixLit").Value.ToString()
            txtCodePrixLit.Text = UtilsUCReferentiels.GetStringValue(row, "code_prixLit")
            txtNomPrixLit.Text = UtilsUCReferentiels.GetStringValue(row, "nom_prixLit")
            txtDescriptionPrixLit.Text = UtilsUCReferentiels.GetStringValue(row, "description_prixLit")
            chkPrixLitActif.Checked = UtilsUCReferentiels.GetBoolValue(row, "is_actif")

            Dim notesRtf As String = UtilsUCReferentiels.GetStringValue(row, "Notes_PrixLit_rtf")
            Dim notesTxt As String = UtilsUCReferentiels.GetStringValue(row, "Notes_PrixLit_txt")
            RichTextNotesHelper.ChargerContenuNotes(rtbNotesPrixLit, notesRtf, notesTxt)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur bind détails PrixLit.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

#End Region

#Region "NIVEAU 2 : Catégories - Load & Sélection"

    Private Sub LoadCategories()

        Try
            If _currentIdPrixLit = 0 Then
                dgvCategories.DataSource = Nothing
                lblCountCategories.Text = "0 catégorie"
                _currentIdCategorie = 0
                dgvAnnees.DataSource = Nothing
                lblCountAnnees.Text = "0 année"
                _currentIdAnnee = 0
                ClearCategorieDetails()
                ClearAnneeDetails()
                Return
            End If

            Dim dt As DataTable = GestionReferentiel.PrixLitCategorie_GetByPrixLit(_currentIdPrixLit)

            dgvCategories.DataSource = dt

            ' Configuration grid Catégories
            ConfigurerGridCategories()

            lblCountCategories.Text = $"{dt.Rows.Count} catégorie(s)"

            ' Réinitialiser l'ID catégorie et vider années
            _currentIdCategorie = 0
            _currentIdAnnee = 0
            dgvAnnees.DataSource = Nothing
            lblCountAnnees.Text = "0 année"
            ClearAnneeDetails()

            ' Si des catégories existent, forcer la sélection de la première et charger ses années
            If dt.Rows.Count > 0 AndAlso _modeCategorie = ModeEdition.Consultation Then
                dgvCategories.ClearSelection()
                dgvCategories.Rows(0).Selected = True
                dgvCategories.CurrentCell = dgvCategories.Rows(0).Cells(0)

                ' Forcer le chargement des années pour la première catégorie
                _currentIdCategorie = Convert.ToUInt64(dt.Rows(0)("id_prixlit_categorie"))
                BindSelectedCategorieToDetails()
                LoadAnnees()
                SetModeAnnee(ModeEdition.Consultation)
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur LoadCategories.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub dgvCategories_SelectionChanged(sender As Object, e As EventArgs) Handles dgvCategories.SelectionChanged

        If _modeCategorie <> ModeEdition.Consultation Then Return
        If dgvCategories.SelectedRows.Count = 0 Then
            ' Aucune sélection : vider années
            _currentIdCategorie = 0
            _currentIdAnnee = 0
            dgvAnnees.DataSource = Nothing
            lblCountAnnees.Text = "0 année"
            ClearCategorieDetails()
            ClearAnneeDetails()
            SetModeAnnee(ModeEdition.Consultation)
            Return
        End If

        Try
            Dim row As DataGridViewRow = dgvCategories.SelectedRows(0)
            _currentIdCategorie = Convert.ToUInt64(row.Cells("id_prixlit_categorie").Value)

            BindSelectedCategorieToDetails()
            LoadAnnees()

            ' Mettre à jour l'état des boutons année
            SetModeAnnee(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur sélection catégorie.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub BindSelectedCategorieToDetails()

        If dgvCategories.SelectedRows.Count = 0 Then Return

        Try
            Dim row As DataGridViewRow = dgvCategories.SelectedRows(0)

            txtIdCategorie.Text = row.Cells("id_prixlit_categorie").Value.ToString()
            txtCodeCategorie.Text = UtilsUCReferentiels.GetStringValue(row, "code_prixlit_categorie")
            txtLibelleCategorie.Text = UtilsUCReferentiels.GetStringValue(row, "libelle_categorie")
            txtDescriptionCategorie.Text = UtilsUCReferentiels.GetStringValue(row, "description_categorie")
            nudOrdreCategorie.Value = UtilsUCReferentiels.GetIntValue(row, "ordre_affichage", 0)
            chkCategorieActive.Checked = UtilsUCReferentiels.GetBoolValue(row, "is_actif")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur bind détails catégorie.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

#End Region

#Region "NIVEAU 3 : Années - Load & Sélection"

    Private Sub LoadAnnees()

        Try
            If _currentIdCategorie = 0 Then
                dgvAnnees.DataSource = Nothing
                lblCountAnnees.Text = "0 année"
                _currentIdAnnee = 0
                ClearAnneeDetails()
                Return
            End If

            Dim dt As DataTable = GestionReferentiel.PrixLitAnnee_GetByCategorie(_currentIdCategorie)

            dgvAnnees.DataSource = dt

            ' Configuration grid Années
            ConfigurerGridAnnees()

            lblCountAnnees.Text = $"{dt.Rows.Count} année(s)"

            ' Réinitialiser l'ID année
            _currentIdAnnee = 0

            ' Si des années existent, forcer la sélection de la première
            If dt.Rows.Count > 0 AndAlso _modeAnnee = ModeEdition.Consultation Then
                dgvAnnees.ClearSelection()
                dgvAnnees.Rows(0).Selected = True
                dgvAnnees.CurrentCell = dgvAnnees.Rows(0).Cells(0)

                ' Forcer le chargement des détails de la première année
                _currentIdAnnee = Convert.ToUInt64(dt.Rows(0)("id_prixLit_Annee"))
                BindSelectedAnneeToDetails()
            Else
                ClearAnneeDetails()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur LoadAnnees.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub dgvAnnees_SelectionChanged(sender As Object, e As EventArgs) Handles dgvAnnees.SelectionChanged

        If _modeAnnee <> ModeEdition.Consultation Then Return
        If dgvAnnees.SelectedRows.Count = 0 Then
            ' Aucune sélection : vider détails
            _currentIdAnnee = 0
            ClearAnneeDetails()
            Return
        End If

        Try
            Dim row As DataGridViewRow = dgvAnnees.SelectedRows(0)
            _currentIdAnnee = Convert.ToUInt64(row.Cells("id_prixLit_Annee").Value)

            BindSelectedAnneeToDetails()

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur sélection année.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub BindSelectedAnneeToDetails()

        If dgvAnnees.SelectedRows.Count = 0 Then Return

        Try
            Dim row As DataGridViewRow = dgvAnnees.SelectedRows(0)

            txtIdAnnee.Text = row.Cells("id_prixLit_Annee").Value.ToString()
            txtCodeAnnee.Text = UtilsUCReferentiels.GetStringValue(row, "code_prixLit_Annee")
            nudAnnee.Value = UtilsUCReferentiels.GetIntValue(row, "annee", DateTime.Now.Year)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur bind détails année.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

#End Region

#Region "MODES : Gestion états boutons"

    Private Sub SetModePrixLit(mode As ModeEdition)

        _modePrixLit = mode

        ' ✅ Utilisation du helper pour configurer les boutons CRUD
        Dim hasSelection As Boolean = (dgvPrixLit.SelectedRows.Count > 0)
        UtilsUCReferentiels.ConfigurerBoutonsMode(
            mode,
            hasSelection,
            btnNewPrixLit, btnEditPrixLit, btnSavePrixLit, btnCancelPrixLit, btnDeletePrixLit
        )

        ' Gestion des champs spécifiques
        Select Case mode

            Case ModeEdition.Consultation
                dgvPrixLit.Enabled = True
                txtNomPrixLit.ReadOnly = True
                txtDescriptionPrixLit.ReadOnly = True
                rtbNotesPrixLit.ReadOnly = True
                chkPrixLitActif.Enabled = False
                ucRichTextToolbar.Enabled = False

            Case ModeEdition.Nouveau
                dgvPrixLit.Enabled = False
                txtNomPrixLit.ReadOnly = False
                txtDescriptionPrixLit.ReadOnly = False
                rtbNotesPrixLit.ReadOnly = False
                chkPrixLitActif.Enabled = True
                ucRichTextToolbar.Enabled = True

            Case ModeEdition.Modification
                dgvPrixLit.Enabled = False
                txtNomPrixLit.ReadOnly = False
                txtDescriptionPrixLit.ReadOnly = False
                rtbNotesPrixLit.ReadOnly = False
                chkPrixLitActif.Enabled = True
                ucRichTextToolbar.Enabled = True

        End Select

    End Sub

    Private Sub SetModeCategorie(mode As ModeEdition)

        _modeCategorie = mode

        ' ✅ Utilisation du helper pour configurer les boutons CRUD
        ' Note: btnNew est spécial (dépend de _currentIdPrixLit), on le gère manuellement
        Select Case mode

            Case ModeEdition.Consultation
                dgvCategories.Enabled = True
                txtLibelleCategorie.ReadOnly = True
                txtDescriptionCategorie.ReadOnly = True
                nudOrdreCategorie.Enabled = False
                chkCategorieActive.Enabled = False

                btnNewCategorie.Enabled = (_currentIdPrixLit > 0)
                btnEditCategorie.Enabled = (dgvCategories.SelectedRows.Count > 0)
                btnSaveCategorie.Enabled = False
                btnCancelCategorie.Enabled = False
                btnDeleteCategorie.Enabled = (dgvCategories.SelectedRows.Count > 0)

            Case ModeEdition.Nouveau
                dgvCategories.Enabled = False
                txtLibelleCategorie.ReadOnly = False
                txtDescriptionCategorie.ReadOnly = False
                nudOrdreCategorie.Enabled = True
                chkCategorieActive.Enabled = True

                btnNewCategorie.Enabled = False
                btnEditCategorie.Enabled = False
                btnSaveCategorie.Enabled = True
                btnCancelCategorie.Enabled = True
                btnDeleteCategorie.Enabled = False

            Case ModeEdition.Modification
                dgvCategories.Enabled = False
                txtLibelleCategorie.ReadOnly = False
                txtDescriptionCategorie.ReadOnly = False
                nudOrdreCategorie.Enabled = True
                chkCategorieActive.Enabled = True

                btnNewCategorie.Enabled = False
                btnEditCategorie.Enabled = False
                btnSaveCategorie.Enabled = True
                btnCancelCategorie.Enabled = True
                btnDeleteCategorie.Enabled = False

        End Select

    End Sub

    Private Sub SetModeAnnee(mode As ModeEdition)

        _modeAnnee = mode

        ' ✅ Utilisation du helper pour configurer les boutons CRUD
        ' Note: btnNew est spécial (dépend de _currentIdCategorie), on le gère manuellement
        Select Case mode

            Case ModeEdition.Consultation
                dgvAnnees.Enabled = True
                nudAnnee.Enabled = False

                btnNewAnnee.Enabled = (_currentIdCategorie > 0)
                btnEditAnnee.Enabled = (dgvAnnees.SelectedRows.Count > 0)
                btnSaveAnnee.Enabled = False
                btnCancelAnnee.Enabled = False
                btnDeleteAnnee.Enabled = (dgvAnnees.SelectedRows.Count > 0)

            Case ModeEdition.Nouveau
                dgvAnnees.Enabled = False
                nudAnnee.Enabled = True

                btnNewAnnee.Enabled = False
                btnEditAnnee.Enabled = False
                btnSaveAnnee.Enabled = True
                btnCancelAnnee.Enabled = True
                btnDeleteAnnee.Enabled = False

            Case ModeEdition.Modification
                dgvAnnees.Enabled = False
                nudAnnee.Enabled = True

                btnNewAnnee.Enabled = False
                btnEditAnnee.Enabled = False
                btnSaveAnnee.Enabled = True
                btnCancelAnnee.Enabled = True
                btnDeleteAnnee.Enabled = False

        End Select

    End Sub

#End Region

#Region "RECHERCHE : PrixLit"

    Private Sub btnSearchPrixLit_Click(sender As Object, e As EventArgs) Handles btnSearchPrixLit.Click

        Try
            Dim searchText As String = txtSearchPrixLit.Text.Trim()

            If String.IsNullOrEmpty(searchText) Then
                LoadPrixLit()
                Return
            End If

            Dim actifsOnly As Boolean = chkPrixLitActifs.Checked
            Dim includeNotes As Boolean = chkRechercherDansNotes.Checked

            ' Recherche unifiée via la VIEW : nom, description, catégories, années, et optionnellement notes
            Dim dt As DataTable = GestionReferentiel.PrixLit_GetBySearch(searchText, actifsOnly, includeNotes)

            dgvPrixLit.DataSource = dt
            ConfigurerGridPrixLit()

            lblCountPrixLit.Text = $"{dt.Rows.Count} prix"

            Dim noteMsg As String = If(includeNotes, " (notes incluses)", "")
            SetStatus($"{dt.Rows.Count} prix trouvé(s) pour '{searchText}'{noteMsg}.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur recherche PrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur recherche.")
        End Try

    End Sub

    Private Sub btnClearSearchPrixLit_Click(sender As Object, e As EventArgs) Handles btnClearSearchPrixLit.Click
        txtSearchPrixLit.Clear()
        LoadPrixLit()
    End Sub

    Private Sub chkPrixLitActifs_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrixLitActifs.CheckedChanged
        LoadPrixLit()
    End Sub

#End Region

#Region "ACTIONS CRUD : PrixLit"

    Private Sub btnNewPrixLit_Click(sender As Object, e As EventArgs) Handles btnNewPrixLit.Click

        Try
            _currentIdPrixLit = 0
            txtIdPrixLit.Clear()
            txtCodePrixLit.Clear()
            txtNomPrixLit.Clear()
            txtDescriptionPrixLit.Clear()
            rtbNotesPrixLit.Clear()
            chkPrixLitActif.Checked = True

            SetModePrixLit(ModeEdition.Nouveau)
            txtNomPrixLit.Focus()
            SetStatus("Nouveau prix littéraire.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur nouveau PrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnEditPrixLit_Click(sender As Object, e As EventArgs) Handles btnEditPrixLit.Click

        Try
            If dgvPrixLit.SelectedRows.Count = 0 Then
                MessageBox.Show("Veuillez sélectionner un prix littéraire.", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            SetModePrixLit(ModeEdition.Modification)
            txtNomPrixLit.Focus()
            SetStatus("Modification du prix littéraire.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur édition PrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnSavePrixLit_Click(sender As Object, e As EventArgs) Handles btnSavePrixLit.Click

        Try
            ' Validation
            If String.IsNullOrWhiteSpace(txtNomPrixLit.Text) Then
                MessageBox.Show("Le nom est obligatoire.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNomPrixLit.Focus()
                Return
            End If

            Dim prix As New PrixLit() With {
                .NomPrixLit = txtNomPrixLit.Text.Trim(),
                .DescriptionPrixLit = txtDescriptionPrixLit.Text.Trim(),
                .IsActif = chkPrixLitActif.Checked
            }

            ' Notes RTF + TXT
            prix.NotesPrixLitRtf = RichTextNotesHelper.GetNotesRtf(rtbNotesPrixLit)
            prix.NotesPrixLitTxt = RichTextNotesHelper.GetNotesTxt(rtbNotesPrixLit)

            If _modePrixLit = ModeEdition.Nouveau Then
                ' Insertion
                Dim newId As ULong = GestionReferentiel.PrixLit_Insert(prix)
                _currentIdPrixLit = newId
                SetStatus($"Prix littéraire '{prix.NomPrixLit}' créé.")
            Else
                ' Mise à jour
                prix.IdPrixLit = _currentIdPrixLit
                GestionReferentiel.PrixLit_Update(prix)
                SetStatus($"Prix littéraire '{prix.NomPrixLit}' mis à jour.")
            End If

            LoadPrixLit()
            SetModePrixLit(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur save PrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            MessageBox.Show($"Erreur enregistrement : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnCancelPrixLit_Click(sender As Object, e As EventArgs) Handles btnCancelPrixLit.Click

        Try
            If _modePrixLit = ModeEdition.Nouveau Then
                txtIdPrixLit.Clear()
                txtCodePrixLit.Clear()
                txtNomPrixLit.Clear()
                txtDescriptionPrixLit.Clear()
                rtbNotesPrixLit.Clear()
                chkPrixLitActif.Checked = True
            Else
                BindSelectedPrixLitToDetails()
            End If

            SetModePrixLit(ModeEdition.Consultation)
            SetStatus("Annulation.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur cancel PrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnDeletePrixLit_Click(sender As Object, e As EventArgs) Handles btnDeletePrixLit.Click

        Try
            If dgvPrixLit.SelectedRows.Count = 0 Then Return

            Dim row As DataGridViewRow = dgvPrixLit.SelectedRows(0)
            Dim idPrixLit As ULong = Convert.ToUInt64(row.Cells("id_prixLit").Value)
            Dim nomPrixLit As String = UtilsUCReferentiels.GetStringValue(row, "nom_prixLit")

            ' Vérifier si catégories liées
            Dim nbCategories As Integer = GestionReferentiel.PrixLit_CountCategories(idPrixLit)
            If nbCategories > 0 Then
                MessageBox.Show($"Impossible de supprimer '{nomPrixLit}' : {nbCategories} catégorie(s) liée(s).", "Suppression refusée", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim rep As DialogResult = MessageBox.Show(
                $"Supprimer le prix littéraire '{nomPrixLit}' ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If rep = DialogResult.Yes Then
                GestionReferentiel.PrixLit_Delete(idPrixLit)
                SetStatus($"Prix littéraire '{nomPrixLit}' supprimé.")
                LoadPrixLit()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur delete PrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            MessageBox.Show($"Erreur suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

#Region "ACTIONS CRUD : Catégories"

    Private Sub btnNewCategorie_Click(sender As Object, e As EventArgs) Handles btnNewCategorie.Click

        Try
            If _currentIdPrixLit = 0 Then
                MessageBox.Show("Veuillez sélectionner un prix littéraire.", "Nouvelle catégorie", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            _currentIdCategorie = 0
            txtIdCategorie.Clear()
            txtCodeCategorie.Clear()
            txtLibelleCategorie.Clear()
            txtDescriptionCategorie.Clear()
            nudOrdreCategorie.Value = 0
            chkCategorieActive.Checked = True

            SetModeCategorie(ModeEdition.Nouveau)
            txtLibelleCategorie.Focus()
            SetStatus("Nouvelle catégorie.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur nouveau catégorie.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnEditCategorie_Click(sender As Object, e As EventArgs) Handles btnEditCategorie.Click

        Try
            If dgvCategories.SelectedRows.Count = 0 Then
                MessageBox.Show("Veuillez sélectionner une catégorie.", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            SetModeCategorie(ModeEdition.Modification)
            txtLibelleCategorie.Focus()
            SetStatus("Modification de la catégorie.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur édition catégorie.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnSaveCategorie_Click(sender As Object, e As EventArgs) Handles btnSaveCategorie.Click

        Try
            ' Validation
            If String.IsNullOrWhiteSpace(txtLibelleCategorie.Text) Then
                MessageBox.Show("Le libellé est obligatoire.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLibelleCategorie.Focus()
                Return
            End If

            Dim categorie As New PrixLitCategorie() With {
                .IdPrixLit = _currentIdPrixLit,
                .LibelleCategorie = txtLibelleCategorie.Text.Trim(),
                .DescriptionCategorie = txtDescriptionCategorie.Text.Trim(),
                .OrdreAffichage = Convert.ToInt32(nudOrdreCategorie.Value),
                .IsActif = chkCategorieActive.Checked
            }

            If _modeCategorie = ModeEdition.Nouveau Then
                ' Insertion
                Dim newId As ULong = GestionReferentiel.PrixLitCategorie_Insert(categorie)
                _currentIdCategorie = newId
                SetStatus($"Catégorie '{categorie.LibelleCategorie}' créée.")
            Else
                ' Mise à jour
                categorie.IdPrixLitCategorie = _currentIdCategorie
                GestionReferentiel.PrixLitCategorie_Update(categorie)
                SetStatus($"Catégorie '{categorie.LibelleCategorie}' mise à jour.")
            End If

            LoadCategories()
            SetModeCategorie(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur save catégorie.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            MessageBox.Show($"Erreur enregistrement : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnCancelCategorie_Click(sender As Object, e As EventArgs) Handles btnCancelCategorie.Click

        Try
            If _modeCategorie = ModeEdition.Nouveau Then
                txtIdCategorie.Clear()
                txtCodeCategorie.Clear()
                txtLibelleCategorie.Clear()
                txtDescriptionCategorie.Clear()
                nudOrdreCategorie.Value = 0
                chkCategorieActive.Checked = True
            Else
                BindSelectedCategorieToDetails()
            End If

            SetModeCategorie(ModeEdition.Consultation)
            SetStatus("Annulation.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur cancel catégorie.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnDeleteCategorie_Click(sender As Object, e As EventArgs) Handles btnDeleteCategorie.Click

        Try
            If dgvCategories.SelectedRows.Count = 0 Then Return

            Dim row As DataGridViewRow = dgvCategories.SelectedRows(0)
            Dim idCategorie As ULong = Convert.ToUInt64(row.Cells("id_prixlit_categorie").Value)
            Dim libelle As String = UtilsUCReferentiels.GetStringValue(row, "libelle_categorie")

            ' Vérifier si années liées
            Dim nbAnnees As Integer = GestionReferentiel.PrixLitCategorie_CountAnnees(idCategorie)
            If nbAnnees > 0 Then
                MessageBox.Show($"Impossible de supprimer '{libelle}' : {nbAnnees} année(s) liée(s).", "Suppression refusée", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim rep As DialogResult = MessageBox.Show(
                $"Supprimer la catégorie '{libelle}' ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If rep = DialogResult.Yes Then
                GestionReferentiel.PrixLitCategorie_Delete(idCategorie)
                SetStatus($"Catégorie '{libelle}' supprimée.")
                LoadCategories()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur delete catégorie.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            MessageBox.Show($"Erreur suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

#Region "ACTIONS CRUD : Années"

    Private Sub btnNewAnnee_Click(sender As Object, e As EventArgs) Handles btnNewAnnee.Click

        Try
            If _currentIdCategorie = 0 Then
                MessageBox.Show("Veuillez sélectionner une catégorie.", "Nouvelle année", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            _currentIdAnnee = 0
            txtIdAnnee.Clear()
            txtCodeAnnee.Clear()
            nudAnnee.Value = DateTime.Now.Year

            SetModeAnnee(ModeEdition.Nouveau)
            nudAnnee.Focus()
            SetStatus("Nouvelle année.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur nouveau année.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnEditAnnee_Click(sender As Object, e As EventArgs) Handles btnEditAnnee.Click

        Try
            If dgvAnnees.SelectedRows.Count = 0 Then
                MessageBox.Show("Veuillez sélectionner une année.", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            SetModeAnnee(ModeEdition.Modification)
            nudAnnee.Focus()
            SetStatus("Modification de l'année.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur édition année.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnSaveAnnee_Click(sender As Object, e As EventArgs) Handles btnSaveAnnee.Click

        Try
            Dim anneeObj As New PrixLitAnnee() With {
                .IdPrixLitCategorie = _currentIdCategorie,
                .Annee = Convert.ToInt32(nudAnnee.Value)
            }

            If _modeAnnee = ModeEdition.Nouveau Then
                ' Insertion
                Dim newId As ULong = GestionReferentiel.PrixLitAnnee_Insert(anneeObj)
                _currentIdAnnee = newId
                SetStatus($"Année '{anneeObj.Annee}' créée.")
            Else
                ' Mise à jour
                anneeObj.IdPrixLitAnnee = _currentIdAnnee
                GestionReferentiel.PrixLitAnnee_Update(anneeObj)
                SetStatus($"Année '{anneeObj.Annee}' mise à jour.")
            End If

            LoadAnnees()
            SetModeAnnee(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur save année.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            MessageBox.Show($"Erreur enregistrement : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnCancelAnnee_Click(sender As Object, e As EventArgs) Handles btnCancelAnnee.Click

        Try
            If _modeAnnee = ModeEdition.Nouveau Then
                txtIdAnnee.Clear()
                txtCodeAnnee.Clear()
                nudAnnee.Value = DateTime.Now.Year
            Else
                BindSelectedAnneeToDetails()
            End If

            SetModeAnnee(ModeEdition.Consultation)
            SetStatus("Annulation.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur cancel année.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub btnDeleteAnnee_Click(sender As Object, e As EventArgs) Handles btnDeleteAnnee.Click

        Try
            If dgvAnnees.SelectedRows.Count = 0 Then Return

            Dim row As DataGridViewRow = dgvAnnees.SelectedRows(0)
            Dim idAnnee As ULong = Convert.ToUInt64(row.Cells("id_prixLit_Annee").Value)
            Dim annee As String = UtilsUCReferentiels.GetStringValue(row, "annee")

            ' Vérifier si livres liés
            Dim nbLivres As Integer = GestionReferentiel.PrixLitAnnee_CountLivres(idAnnee)
            If nbLivres > 0 Then
                MessageBox.Show($"Impossible de supprimer l'année '{annee}' : {nbLivres} livre(s) lié(s).", "Suppression refusée", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim rep As DialogResult = MessageBox.Show(
                $"Supprimer l'année '{annee}' ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If rep = DialogResult.Yes Then
                GestionReferentiel.PrixLitAnnee_Delete(idAnnee)
                SetStatus($"Année '{annee}' supprimée.")
                LoadAnnees()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur delete année.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            MessageBox.Show($"Erreur suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

#Region "Configuration des Grids"

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' ConfigurerGridPrixLit
    '
    '   • Configure les colonnes du grid PrixLit
    '   • Headers en gras, multiligne sur description
    '------------------------------------------------------------
    Private Sub ConfigurerGridPrixLit()

        If dgvPrixLit.DataSource Is Nothing Then Return

        ' Masquer colonnes techniques et code
        UtilsUCReferentiels.MasquerColonnesTechniques(dgvPrixLit, {"id_prixLit", "code_prixLit", "Notes_PrixLit_txt", "Notes_PrixLit_rtf", "created_at", "updated_at"})

        ' Configurer colonnes visibles
        UtilsUCReferentiels.SetColonneVisible(dgvPrixLit, "nom_prixLit", True, "Nom Prix", 280)
        UtilsUCReferentiels.SetColonneVisible(dgvPrixLit, "description_prixLit", True, "Description", 250)
        UtilsUCReferentiels.SetColonneVisible(dgvPrixLit, "is_actif", True, "Actif", 50)

        ' Headers en gras
        dgvPrixLit.ColumnHeadersDefaultCellStyle.Font = New Font(dgvPrixLit.Font, FontStyle.Bold)

        ' Lignes en Normal (pas de gras)
        dgvPrixLit.DefaultCellStyle.Font = New Font(dgvPrixLit.Font, FontStyle.Regular)

        ' Multiligne pour description
        If dgvPrixLit.Columns.Contains("description_prixLit") Then
            dgvPrixLit.Columns("description_prixLit").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        End If

        ' Hauteur de ligne auto
        dgvPrixLit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' ConfigurerGridCategories
    '
    '   • Configure les colonnes du grid Catégories
    '   • Headers en gras, multiligne sur description, affiche IDs en abregé
    '------------------------------------------------------------
    Private Sub ConfigurerGridCategories()

        If dgvCategories.DataSource Is Nothing Then Return

        ' Masquer colonnes techniques et code
        UtilsUCReferentiels.MasquerColonnesTechniques(dgvCategories, {"id_prixlit_categorie", "code_prixlit_categorie", "created_at", "updated_at"})

        ' Configurer colonnes visibles avec ID PrixLit
        UtilsUCReferentiels.SetColonneVisible(dgvCategories, "id_prixLit", True, "ID Prix", 60)
        UtilsUCReferentiels.SetColonneVisible(dgvCategories, "libelle_categorie", True, "Catégorie", 180)
        UtilsUCReferentiels.SetColonneVisible(dgvCategories, "description_categorie", True, "Description", 200)
        UtilsUCReferentiels.SetColonneVisible(dgvCategories, "ordre_affichage", True, "Ordre", 50)
        UtilsUCReferentiels.SetColonneVisible(dgvCategories, "is_actif", True, "Actif", 50)

        ' Headers en gras
        dgvCategories.ColumnHeadersDefaultCellStyle.Font = New Font(dgvCategories.Font, FontStyle.Bold)

        ' Lignes en Normal (pas de gras)
        dgvCategories.DefaultCellStyle.Font = New Font(dgvCategories.Font, FontStyle.Regular)

        ' Multiligne pour description
        If dgvCategories.Columns.Contains("description_categorie") Then
            dgvCategories.Columns("description_categorie").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        End If

        ' Hauteur de ligne auto
        dgvCategories.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 25/03/2026
    ' ConfigurerGridAnnees
    '
    '   • Configure les colonnes du grid Années
    '   • Headers en gras, affiche IDs en abregé, pas de libelle_categorie
    '------------------------------------------------------------
    Private Sub ConfigurerGridAnnees()

        If dgvAnnees.DataSource Is Nothing Then Return

        ' Masquer colonnes techniques, code et libelle_categorie
        UtilsUCReferentiels.MasquerColonnesTechniques(dgvAnnees, {"id_prixLit_Annee", "code_prixLit_Annee", "libelle_categorie", "created_at", "updated_at"})

        ' Configurer colonnes visibles avec IDs
        UtilsUCReferentiels.SetColonneVisible(dgvAnnees, "id_prixLit", True, "ID Prix", 60)
        UtilsUCReferentiels.SetColonneVisible(dgvAnnees, "id_prixlit_categorie", True, "ID Cat.", 60)
        UtilsUCReferentiels.SetColonneVisible(dgvAnnees, "annee", True, "Année", 80)

        ' Headers en gras
        dgvAnnees.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAnnees.Font, FontStyle.Bold)

        ' Lignes en Normal (pas de gras)
        dgvAnnees.DefaultCellStyle.Font = New Font(dgvAnnees.Font, FontStyle.Regular)

        ' Hauteur de ligne auto
        dgvAnnees.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

    End Sub

#End Region

#Region "Helpers"

    Private Sub SetStatus(message As String)
        _context?.SetStatus(message)
    End Sub

    Private Sub ClearCategorieDetails()
        txtIdCategorie.Clear()
        txtCodeCategorie.Clear()
        txtLibelleCategorie.Clear()
        txtDescriptionCategorie.Clear()
        nudOrdreCategorie.Value = 0
        chkCategorieActive.Checked = True
    End Sub

    Private Sub ClearAnneeDetails()
        txtIdAnnee.Clear()
        txtCodeAnnee.Clear()
        nudAnnee.Value = DateTime.Now.Year
    End Sub

#End Region

End Class
