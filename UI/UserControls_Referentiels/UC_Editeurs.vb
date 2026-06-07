'====================================================================
' 📌 UC_Editeurs.vb
' Version : V1.0
' Date    : 24/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion du référentiel Editeurs.
' Transposé depuis GestionEditeurs.vb
'
' Règles :
' - Implémente IContextAwareUserControl
' - Utilise UserControlContext pour StatusStrip, ErrorProvider, ToolTip
' - Support RichTextBox enrichi pour les notes (RTF + TXT)
' - Logique métier identique à la Form d'origine
' - ComboBox Pays avec valeur nullable
' - Site web avec style URL et double-clic pour ouverture
'
' Évolution :
' - V1.0 : Transposition depuis GestionEditeurs.vb
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_Editeurs
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'utilisation
    Private _mode As ModeEdition = ModeEdition.Consultation

    ' Identifiant de l'éditeur courant
    Private _currentId As ULong = 0

    ' Contexte partagé
    Private _context As UserControlContext = Nothing

#End Region

#Region "Constructeur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' New
    '
    '   • Constructeur par défaut (requis pour NavigationManager)
    '------------------------------------------------------------
    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "IContextAwareUserControl Implementation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' SetContext (IContextAwareUserControl)
    '
    '   • Injecte le contexte partagé dans ce UserControl
    '------------------------------------------------------------
    Public Sub SetContext(context As UserControlContext) Implements IContextAwareUserControl.SetContext
        _context = context
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' OnActivated (IContextAwareUserControl)
    '
    '   • Appelé quand l'UC devient actif
    '   • Rafraîchit les données et met à jour le contexte
    '------------------------------------------------------------
    Public Sub OnActivated() Implements IContextAwareUserControl.OnActivated
        Try
            ' Rafraîchir les données
            LoadPaysCombo()
            LoadGrid()

            ' Mettre à jour le fil d'Ariane
            If _context IsNot Nothing Then
                _context.NavigateToLevel("Éditeurs")
                _context.SetStatus($"Éditeurs chargés : {dgvEditeurs.Rows.Count} élément(s).")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_Editeurs.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de l'activation.")
        End Try
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' OnDeactivated (IContextAwareUserControl)
    '
    '   • Appelé quand l'UC devient inactif
    '------------------------------------------------------------
    Public Sub OnDeactivated() Implements IContextAwareUserControl.OnDeactivated
        Try
            If _context IsNot Nothing Then
                _context.ClearAllErrors()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur désactivation UC_Editeurs.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' UC_Editeurs_Load
    '
    '   • Initialise le UserControl au chargement
    '------------------------------------------------------------
    Private Sub UC_Editeurs_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbNotes)

            ' Connecter le toolbar au RichTextBox
            ucToolbar.TargetRichTextBox = rtbNotes

            LoadPaysCombo()
            LoadGrid()

            If dgvEditeurs.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetStatus("Éditeurs chargés.")
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement UC_Editeurs.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors du chargement.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' InitToolTips
    '
    '   • Configure les info-bulles pour tous les contrôles
    '------------------------------------------------------------
    Private Sub InitToolTips()

        If _context?.ToolTip Is Nothing Then Return

        Dim tt = _context.ToolTip

        tt.SetToolTip(btnSearch, "Appliquer le filtre")
        tt.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        tt.SetToolTip(btnNew, "Créer un nouvel éditeur")
        tt.SetToolTip(btnEdit, "Passer en mode modification")
        tt.SetToolTip(btnSave, "Enregistrer les modifications")
        tt.SetToolTip(btnCancel, "Annuler les modifications")
        tt.SetToolTip(btnDelete, "Supprimer l'éditeur sélectionné")
        tt.SetToolTip(txtSiteWeb, "Double-cliquer pour ouvrir le site web")

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' LoadPaysCombo
    '
    '   • Charge le combo des pays à partir du référentiel
    '------------------------------------------------------------
    Private Sub LoadPaysCombo()

        Try
            Dim dt As DataTable = GestionReferentiel.Pays_GetAll()

            Dim row = dt.NewRow()
            row("id_pays") = DBNull.Value
            row("nom_pays") = ""

            dt.Rows.InsertAt(row, 0)

            cboPays.DataSource = dt
            cboPays.DisplayMember = "nom_pays"
            cboPays.ValueMember = "id_pays"

            cboPays.SelectedIndex = 0

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement combo pays (UC_Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' SetMode
    '
    '   • Configure l'UI selon le mode actuel (Consultation/Nouveau/Modification)
    '------------------------------------------------------------
    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Select Case mode

            Case ModeEdition.Consultation

                ' Champs lecture seule
                txtNomEditeur.ReadOnly = True
                txtSiteWeb.ReadOnly = True
                rtbNotes.ReadOnly = True
                ucToolbar.Enabled = False
                cboPays.Enabled = False

                ' Boutons
                btnNew.Enabled = True
                btnEdit.Enabled = HasSelectedEditeur()
                btnSave.Enabled = False
                btnCancel.Enabled = False
                btnDelete.Enabled = HasSelectedEditeur()

                ' Recherche active
                txtSearch.Enabled = True
                btnSearch.Enabled = True
                btnClearSearch.Enabled = True

                ' Grille active
                dgvEditeurs.Enabled = True

            Case ModeEdition.Nouveau, ModeEdition.Modification

                ' Champs éditables
                txtNomEditeur.ReadOnly = False
                txtSiteWeb.ReadOnly = False
                rtbNotes.ReadOnly = False
                ucToolbar.Enabled = True
                cboPays.Enabled = True

                ' Boutons
                btnNew.Enabled = False
                btnEdit.Enabled = False
                btnSave.Enabled = True
                btnCancel.Enabled = True
                btnDelete.Enabled = False

                ' Recherche désactivée
                txtSearch.Enabled = False
                btnSearch.Enabled = False
                btnClearSearch.Enabled = False

                ' Grille désactivée
                dgvEditeurs.Enabled = False

        End Select

        UpdateSiteWebStyle()

        ' Mettre à jour le fil d'Ariane avec le mode
        If _context IsNot Nothing Then
            Select Case mode
                Case ModeEdition.Consultation
                    _context.NavigateToLevel("Éditeurs")
                Case ModeEdition.Nouveau
                    _context.SetMode("Nouveau")
                Case ModeEdition.Modification
                    _context.SetMode("Modification")
            End Select
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' HasSelectedEditeur
    '
    '   • Retourne True si un éditeur est actuellement sélectionné
    '------------------------------------------------------------
    Private Function HasSelectedEditeur() As Boolean
        Return dgvEditeurs.CurrentRow IsNot Nothing
    End Function

#End Region

#Region "Interface utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' SetStatus
    '
    '   • Affiche un message dans la barre d'état via le contexte
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)
        _context?.SetStatus(message)
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ClearErrors
    '
    '   • Efface les erreurs de validation affichées dans l'UI
    '------------------------------------------------------------
    Private Sub ClearErrors()
        _context?.ClearAllErrors()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' UpdateSiteWebStyle
    '
    '   • Met le style du champ site web en style URL
    '------------------------------------------------------------
    Private Sub UpdateSiteWebStyle()
        UtilsForm.UpdateUrlTextBoxStyle(txtSiteWeb)
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' txtSiteWeb_DoubleClick
    '
    '   • Double-cliquer sur le champ site web ouvre le site web
    '------------------------------------------------------------
    Private Sub txtSiteWeb_DoubleClick(sender As Object, e As EventArgs) Handles txtSiteWeb.DoubleClick

        Try
            UtilsForm.OpenUrl(txtSiteWeb.Text)
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur ouverture site web éditeur.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Impossible d'ouvrir le site web.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' txtSiteWeb_TextChanged
    '
    '   • Met le style du champ site web en fonction du contenu
    '------------------------------------------------------------
    Private Sub txtSiteWeb_TextChanged(sender As Object, e As EventArgs) Handles txtSiteWeb.TextChanged
        UpdateSiteWebStyle()
    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnNew_Click
    '
    '   • Crée un nouvel éditeur
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearErrors()
        ClearDetails()
        _currentId = 0
        SetMode(ModeEdition.Nouveau)
        txtNomEditeur.Focus()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnEdit_Click
    '
    '   • Édite l'éditeur sélectionné
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ClearErrors()

        If Not HasSelectedEditeur() Then
            SetStatus("Aucun éditeur sélectionné.")
            Return
        End If

        SetMode(ModeEdition.Modification)
        txtNomEditeur.Focus()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnSave_Click
    '
    '   • Enregistre l'éditeur (création ou modification)
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateForm() Then Exit Sub

        Try
            Dim editeur As Editeur = BuildEditeurFromFields()

            If _mode = ModeEdition.Nouveau Then
                _currentId = GestionReferentiel.Editeurs_Insert(editeur)
                SetStatus("Éditeur créé.")
            ElseIf _mode = ModeEdition.Modification Then
                GestionReferentiel.Editeurs_Update(editeur)
                _currentId = editeur.IdEditeur
                SetStatus("Éditeur modifié.")
            End If

            LoadGrid()
            ReselectCurrentEditeur()

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnSave_Click (UC_Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de l'enregistrement.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnCancel_Click
    '
    '   • Annule les modifications en cours
    '------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try
            If dgvEditeurs.Rows.Count > 0 Then
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Edition annulée.")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnCancel_Click (UC_Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnDelete_Click
    '
    '   • Supprime l'éditeur sélectionné
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try
            If Not HasSelectedEditeur() Then
                SetStatus("Aucun éditeur sélectionné.")
                Return
            End If

            Dim id As ULong = UtilsUCReferentiels.SafeULong(txtIdEditeur.Text)
            Dim nomEditeur As String = txtNomEditeur.Text

            ' Vérifier dépendances
            Dim countLivres As Integer = GestionReferentiel.Editeurs_CountLivres(id)

            If countLivres > 0 Then
                MessageBox.Show(
                    $"Impossible de supprimer cet éditeur car {countLivres} livre(s) y sont rattachés.",
                    "Suppression impossible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                SetStatus("Suppression impossible (dépendances).")
                Return
            End If

            Dim rep = MessageBox.Show(
                $"Supprimer l'éditeur '{nomEditeur}' ?",
                "Confirmation suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            )

            If rep <> DialogResult.Yes Then
                SetStatus("Suppression annulée.")
                Return
            End If

            GestionReferentiel.Editeurs_Delete(id)

            GestionLog.EcrireLog(
                $"UI: suppression éditeur '{nomEditeur}'.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI
            )

            SetStatus("Éditeur supprimé.")

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnDelete_Click (UC_Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la suppression.")
        End Try

    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' LoadGrid
    '
    '   • Charge et affiche la liste complète des éditeurs
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try
            Dim dt = GestionReferentiel.Editeurs_GetAll()
            dgvEditeurs.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvEditeurs)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvEditeurs,
                "id_editeur", "code_editeur", "id_pays",
                "created_at", "updated_at",
                "notes_editeur_rtf", "notes_editeur_txt")

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvEditeurs, "éditeur")

            ' Sélection première ligne
            If dgvEditeurs.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvEditeurs, "nom_editeur")
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid (UC_Editeurs).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement de la grille.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' BindSelectedToDetails
    '
    '   • Affiche les détails de l'éditeur sélectionné dans la grille
    '------------------------------------------------------------
    Private Sub BindSelectedToDetails()

        Try
            If dgvEditeurs.CurrentRow Is Nothing Then
                ClearDetails()
                Return
            End If

            Dim row = dgvEditeurs.CurrentRow

            txtIdEditeur.Text = If(row.Cells("id_editeur")?.Value?.ToString(), "")
            txtCodeEditeur.Text = If(row.Cells("code_editeur")?.Value?.ToString(), "")
            txtNomEditeur.Text = If(row.Cells("nom_editeur")?.Value?.ToString(), "")
            txtSiteWeb.Text = If(row.Cells("site_web")?.Value?.ToString(), "")
            UpdateSiteWebStyle()

            ' Charger notes (RTF prioritaire, fallback TXT)
            Dim notesRtf As String = If(row.Cells("notes_editeur_rtf").Value, "").ToString()
            Dim notesTxt As String = If(row.Cells("notes_editeur_txt").Value, "").ToString()
            RichTextNotesHelper.ChargerContenuNotes(rtbNotes, notesRtf, notesTxt)

            ' Pays nullable
            If row.Cells("id_pays").Value Is Nothing OrElse row.Cells("id_pays").Value Is DBNull.Value Then
                cboPays.SelectedIndex = 0
            Else
                cboPays.SelectedValue = Convert.ToUInt64(row.Cells("id_pays").Value)
            End If

            If Not String.IsNullOrWhiteSpace(txtIdEditeur.Text) Then
                _currentId = Convert.ToUInt64(txtIdEditeur.Text)
            Else
                _currentId = 0
            End If

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur BindSelectedToDetails (UC_Editeurs).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ClearDetails
    '
    '   • Efface tous les champs du panneau détails
    '------------------------------------------------------------
    Private Sub ClearDetails()

        txtIdEditeur.Clear()
        txtCodeEditeur.Clear()
        txtNomEditeur.Clear()
        txtSiteWeb.Clear()
        UpdateSiteWebStyle()
        rtbNotes.Clear()

        If cboPays.Items.Count > 0 Then
            cboPays.SelectedIndex = 0
        End If

        _currentId = 0

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' dgvEditeurs_SelectionChanged
    '
    '   • Met à jour les détails lors du changement de sélection
    '------------------------------------------------------------
    Private Sub dgvEditeurs_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEditeurs.SelectionChanged
        If _mode = ModeEdition.Consultation Then
            BindSelectedToDetails()
            SetMode(ModeEdition.Consultation) ' Refresh buttons
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ReselectCurrentEditeur
    '
    '   • Re-sélectionne dans la grille l'éditeur correspondant à _currentId
    '   • Important après save pour éviter confusion visuelle
    '------------------------------------------------------------
    Private Sub ReselectCurrentEditeur()

        If _currentId = 0UL OrElse dgvEditeurs.Rows.Count = 0 Then
            Return
        End If

        UtilsForm.DgvSelectRowById(dgvEditeurs, "id_editeur", _currentId)
        BindSelectedToDetails()

    End Sub

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ValidateForm
    '
    '   • Valide tous les champs avant enregistrement
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        ClearErrors()

        Dim isValid As Boolean = True

        If String.IsNullOrWhiteSpace(txtNomEditeur.Text) Then
            _context?.ErrorProvider.SetError(txtNomEditeur, "Le nom de l'éditeur est obligatoire.")
            isValid = False
        End If

        If Not isValid Then
            SetStatus("Formulaire incomplet ou invalide.")
        End If

        Return isValid

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' BuildEditeurFromFields
    '
    '   • Construit un objet Editeur à partir des champs de la form
    '   • Gère le caractère nullable de id_pays
    '   • Sauvegarde RTF et TXT
    '------------------------------------------------------------
    Private Function BuildEditeurFromFields() As Editeur

        Dim idPaysValue As ULong? = Nothing

        If cboPays.SelectedValue IsNot Nothing AndAlso
           cboPays.SelectedValue IsNot DBNull.Value AndAlso
           cboPays.Text.Trim() <> "" Then

            idPaysValue = Convert.ToUInt64(cboPays.SelectedValue)

        End If

        Return New Editeur With {
            .IdEditeur = _currentId,
            .CodeEditeur = txtCodeEditeur.Text.Trim(),
            .NomEditeur = txtNomEditeur.Text.Trim(),
            .IdPays = idPaysValue,
            .SiteWeb = txtSiteWeb.Text.Trim(),
            .NotesEditeurRtf = RichTextNotesHelper.GetNotesRtf(rtbNotes),
            .NotesEditeurTxt = RichTextNotesHelper.GetNotesTxt(rtbNotes)
        }

    End Function

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ApplySearch
    '
    '   • Applique le filtre de recherche et recharge la grille
    '   • Tient compte du checkbox "Inclure les notes"
    '------------------------------------------------------------
    Private Sub ApplySearch()

        Try
            Dim searchText As String = txtSearch.Text.Trim()
            Dim includeNotes As Boolean = chkSearchNotes.Checked

            Dim dt As DataTable

            If String.IsNullOrWhiteSpace(searchText) Then
                dt = GestionReferentiel.Editeurs_GetAll()
            Else
                dt = GestionReferentiel.Editeurs_GetBySearch(searchText, includeNotes)
            End If

            dgvEditeurs.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvEditeurs)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvEditeurs,
                "id_editeur", "code_editeur", "id_pays",
                "created_at", "updated_at",
                "notes_editeur_rtf", "notes_editeur_txt")

            ' Sélection première ligne
            If dgvEditeurs.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvEditeurs, "nom_editeur")
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvEditeurs, "éditeur")

            SetStatus($"Recherche effectuée : {dgvEditeurs.Rows.Count} résultat(s).")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch (UC_Editeurs).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
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
