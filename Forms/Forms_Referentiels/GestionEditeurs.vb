'------------------------------------------------------------
' 📌 GestionEditeurs.vb
' Version : V1.1
' Date    : 12/03/2026
'
' Rôle :
' Formulaire WinForms de gestion du référentiel Editeurs.
'
' Règles Artefact :
' - Pas de SQL ici (QueryModule).
' - Pas d'accès DB direct ici (GestionReferentiel).
' - Validation UI : errProvider + StatusStrip.
' - code_editeur affiché en lecture seule.
' - id_editeur conservé pour le pilotage CRUD mais non visible.
'
' Évolution :
' - V1.0 : CRUD de base, recherche, ouverture du site web.
' - V1.1 : Réorganisation complète sur le modèle de GestionPays
'          (régions, ordre logique, en-têtes homogènes, nettoyage mineur).
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionEditeurs

#Region "Déclarations"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' Variables privées
    '
    '   • _mode      : mode courant du formulaire
    '   • _currentId : identifiant de l'éditeur courant
    '------------------------------------------------------------
    Private _mode As ModeEdition = ModeEdition.Consultation
    Private _currentId As ULong = 0

#End Region

#Region "Initialisation"


    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' GestionEditeurs_Load
    '
    '   • Initialise complètement le formulaire au chargement
    '   • Charge le combo des pays puis la grille des éditeurs
    '   • Positionne l’interface en mode Consultation
    '   • Configure les info-bulles et le style du site web
    '------------------------------------------------------------
    Private Sub GestionEditeurs_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            InitToolTips()
            ClearErrors()
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbNotesEditeur)

            LoadPaysCombo()
            LoadGrid()



            If dgvEditeurs.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetMode(ModeEdition.Consultation)
            UpdateSiteWebStyle()
            SetStatus("Editeurs chargés.")

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur chargement GestionEditeurs.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors du chargement des éditeurs." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' InitToolTips
    '
    '   • Configure les info-bulles des contrôles principaux
    '   • Centralise la configuration UX légère du formulaire
    '------------------------------------------------------------
    Private Sub InitToolTips()

        If ttMain Is Nothing Then Return

        ttMain.SetToolTip(btnSearch, "Appliquer le filtre")
        ttMain.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        ttMain.SetToolTip(btnNew, "Créer un nouvel éditeur")
        ttMain.SetToolTip(btnEdit, "Passer en mode modification")
        ttMain.SetToolTip(btnSave, "Enregistrer les modifications")
        ttMain.SetToolTip(btnCancel, "Annuler les modifications")
        ttMain.SetToolTip(btnDelete, "Supprimer l'éditeur sélectionné")
        ttMain.SetToolTip(btnClose, "Fermer la fenêtre")
        ttMain.SetToolTip(txtSiteWeb, "Double-cliquer pour ouvrir le site web")

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
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
                "UI: erreur chargement combo pays (Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            Throw

        End Try

    End Sub

#End Region


#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' SetMode
    '
    '   • Définit le mode courant du formulaire
    '   • Active/désactive les boutons selon le contexte
    '   • Passe les champs en lecture seule ou en saisie
    '   • Met à jour le style du champ site web
    '------------------------------------------------------------
    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Dim hasSelection As Boolean =
            dgvEditeurs IsNot Nothing AndAlso
            dgvEditeurs.CurrentRow IsNot Nothing AndAlso
            dgvEditeurs.Rows.Count > 0

        Select Case mode

            Case ModeEdition.Consultation

                btnNew.Enabled = True
                btnEdit.Enabled = hasSelection
                btnDelete.Enabled = hasSelection

                btnSave.Enabled = False
                btnCancel.Enabled = False

                txtNomEditeur.ReadOnly = True
                txtSiteWeb.ReadOnly = True
                rtbNotesEditeur.ReadOnly = True
                tsNotes.Enabled = False
                cboPays.Enabled = False

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNew.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False

                btnSave.Enabled = True
                btnCancel.Enabled = True

                txtNomEditeur.ReadOnly = False
                txtSiteWeb.ReadOnly = False
                rtbNotesEditeur.ReadOnly = False
                tsNotes.Enabled = True
                cboPays.Enabled = True

        End Select

        UpdateSiteWebStyle()

    End Sub

#End Region


#Region "Interface utilisateur"

    '---------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' SetStatus
    '
    '   • Affiche un message dans la barre d'état
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)

        stsLabelStatus.Text = message

    End Sub

    '---------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ClearErrors
    '
    '   • Efface les erreurs de validation affichées dans l’UI
    '------------------------------------------------------------
    Private Sub ClearErrors()

        If errProvider IsNot Nothing Then
            errProvider.Clear()
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' UpdateSiteWebStyle
    '
    '   • Met le style du champ site web en style URL
    ' ------------------------------------------------------------
    Private Sub UpdateSiteWebStyle()

        UtilsForm.UpdateUrlTextBoxStyle(txtSiteWeb)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
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
    ' 📌 V1.1 - 12/03/2026
    ' txtSiteWeb_TextChanged
    '
    '   • Met le style du champ site web en fonction du contenu
    '------------------------------------------------------------
    Private Sub txtSiteWeb_TextChanged(sender As Object, e As EventArgs) Handles txtSiteWeb.TextChanged

        UpdateSiteWebStyle()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' btnBold_Click, btnItalic_Click, btnUnderline_Click, btnBullet_Click, btnTab_Click
    '
    '   • Actions utilisateur pour le richTextBox de notes
    '------------------------------------------------------------
    Private Sub btnBold_Click(sender As Object, e As EventArgs) Handles btnBold.Click
        RichTextNotesHelper.BasculerGras(rtbNotesEditeur)
    End Sub

    Private Sub btnItalic_Click(sender As Object, e As EventArgs) Handles btnItalic.Click
        RichTextNotesHelper.BasculerItalique(rtbNotesEditeur)
    End Sub

    Private Sub btnUnderline_Click(sender As Object, e As EventArgs) Handles btnUnderline.Click
        RichTextNotesHelper.BasculerSouligne(rtbNotesEditeur)
    End Sub

    Private Sub btnBullet_Click(sender As Object, e As EventArgs) Handles btnBullet.Click
        RichTextNotesHelper.BasculerListe(rtbNotesEditeur)
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs) Handles btnTab.Click
        RichTextNotesHelper.InsererTabulation(rtbNotesEditeur)
    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try

            ClearDetails()

            _currentId = 0

            SetMode(ModeEdition.Nouveau)

            txtNomEditeur.Focus()

            SetStatus("Création d'un nouvel éditeur.")

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur btnNew_Click (Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors de la création.")

        End Try

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try

            If dgvEditeurs.CurrentRow Is Nothing Then
                SetStatus("Aucun éditeur sélectionné.")
                Return
            End If


            SetMode(ModeEdition.Modification)

            txtNomEditeur.Focus()

            SetStatus("Modification de l'éditeur.")

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur btnEdit_Click (Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors du passage en modification.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' btnSave_Click
    '
    '   • Enregistre l'éditeur
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            If Not ValidateForm() Then
                Return
            End If

            Dim editeur As Editeur = BuildEditeurFromFields()

            If _mode = ModeEdition.Nouveau Then

                _currentId = GestionReferentiel.Editeurs_Insert(editeur)
                SetStatus("Editeur créé.")

            ElseIf _mode = ModeEdition.Modification Then

                GestionReferentiel.Editeurs_Update(editeur)
                _currentId = editeur.IdEditeur
                SetStatus("Editeur modifié.")

            End If

            LoadGrid()
            ReselectCurrentEditeur()

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur btnSave_Click (Editeurs).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de l'enregistrement.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 19/03/2026
    ' ReselectCurrentEditeur
    '
    '   • Re-sélectionne dans la grille l'éditeur correspondant à _currentId
    '   • Recharge ensuite le panneau détail
    '------------------------------------------------------------
    Private Sub ReselectCurrentEditeur()

        If _currentId = 0UL OrElse dgvEditeurs.Rows.Count = 0 Then
            Return
        End If

        For Each row As DataGridViewRow In dgvEditeurs.Rows

            If row.IsNewRow Then Continue For

            If row.Cells("id_editeur").Value IsNot Nothing AndAlso
           row.Cells("id_editeur").Value IsNot DBNull.Value AndAlso
           Convert.ToUInt64(row.Cells("id_editeur").Value) = _currentId Then

                dgvEditeurs.ClearSelection()
                row.Selected = True
                dgvEditeurs.CurrentCell = row.Cells("nom_editeur")
                BindSelectedToDetails()
                Exit For

            End If

        Next

    End Sub
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
                "UI: erreur btnCancel_Click (Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors de l'annulation.")

        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If _mode <> ModeEdition.Consultation Then
            SetStatus("Suppression impossible pendant une édition.")
            Return
        End If

        If dgvEditeurs.SelectedRows.Count = 0 Then
            SetStatus("Aucun éditeur sélectionné.")
            Return
        End If

        Dim row As DataGridViewRow = dgvEditeurs.SelectedRows(0)

        Dim id As ULong = 0UL
        If row.Cells("id_editeur").Value IsNot Nothing AndAlso row.Cells("id_editeur").Value IsNot DBNull.Value Then
            id = Convert.ToUInt64(row.Cells("id_editeur").Value)
        End If

        If id = 0UL Then
            SetStatus("Identifiant éditeur invalide.")
            Return
        End If

        Dim editeurNom As String = If(row.Cells("nom_editeur").Value, "").ToString().Trim()

        Try

            Dim nbLivres As Integer = GestionReferentiel.Editeurs_CountLivres(id)

            Dim msg As String

            If nbLivres = 0 Then

                msg = $"Supprimer l'éditeur '{editeurNom}' ?"

            Else

                msg =
                $"Cet éditeur est utilisé dans {nbLivres} livre(s)." & Environment.NewLine & Environment.NewLine &
                "La suppression laissera ces livres sans éditeur." & Environment.NewLine & Environment.NewLine &
                "Confirmer la suppression ?"

            End If

            Dim rep = MessageBox.Show(
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

            GestionReferentiel.Editeurs_Delete(id)

            SetStatus("Editeur supprimé.")

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur btnDelete_Click (Editeurs).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnClose_Click
    '
    '   • Ferme simplement le formulaire courant
    '------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' LoadGrid
    '
    '   • Charge la grille des contacts depuis le référentiel
    '   • Applique le format standard des grilles référentielles
    '   • Masque les colonnes techniques et sélectionne la première ligne
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try

            Dim dt As DataTable = GestionReferentiel.Editeurs_GetAll()

            dgvEditeurs.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvEditeurs)

            If dgvEditeurs.Columns.Contains("id_editeur") Then
                dgvEditeurs.Columns("id_editeur").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("id_pays") Then
                dgvEditeurs.Columns("id_pays").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("created_at") Then
                dgvEditeurs.Columns("created_at").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("updated_at") Then
                dgvEditeurs.Columns("updated_at").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("notes_editeur_rtf") Then
                dgvEditeurs.Columns("notes_editeur_rtf").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("notes_editeur_txt") Then
                dgvEditeurs.Columns("notes_editeur_txt").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} éditeur(s)"

            If UtilsForm.SelectFirstRow(dgvEditeurs, "nom_editeur") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur LoadGrid (Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors du chargement des éditeurs.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ClearDetails
    '
    '   • Efface les données dans le détailsl et remet les champs à leur état initial
    '------------------------------------------------------------
    Private Sub ClearDetails()

        txtIdEditeur.Clear()
        txtCodeEditeur.Clear()
        txtNomEditeur.Clear()
        txtSiteWeb.Clear()
        UpdateSiteWebStyle()
        rtbNotesEditeur.Clear()

        If cboPays.Items.Count > 0 Then
            cboPays.SelectedIndex = 0
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' BuildEditeurFromFields
    '
    '   • Construit un objet Editeur à partir des champs de la form
    '   • Gère le caractère nullable de id_pays
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
            .NotesEditeurRtf = RichTextNotesHelper.GetNotesRtf(rtbNotesEditeur),
            .NotesEditeurTxt = RichTextNotesHelper.GetNotesTxt(rtbNotesEditeur)
        }

    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' BindSelectedToDetails
    '
    '   • Charge dans le panneau détail l'éditeur sélectionné dans la grille
    '   • Met également à jour _currentId si l'identifiant est valide
    '------------------------------------------------------------

    Private Sub BindSelectedToDetails()

        If dgvEditeurs.CurrentRow Is Nothing Then
            ClearDetails()
            _currentId = 0
            SetMode(ModeEdition.Consultation)
            Return
        End If

        Dim row As DataGridViewRow = dgvEditeurs.CurrentRow

        txtIdEditeur.Text = row.Cells("id_editeur").Value?.ToString()
        txtCodeEditeur.Text = row.Cells("code_editeur").Value?.ToString()
        txtNomEditeur.Text = row.Cells("nom_editeur").Value?.ToString()
        txtSiteWeb.Text = row.Cells("site_web").Value?.ToString()
        UpdateSiteWebStyle()

        Dim notesRtf As String = If(row.Cells("notes_editeur_rtf").Value, "").ToString()
        Dim notesTxt As String = If(row.Cells("notes_editeur_txt").Value, "").ToString()

        RichTextNotesHelper.ChargerContenuNotes(rtbNotesEditeur, notesRtf, notesTxt)

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

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' dgvEditeurs_SelectionChanged
    '   
    '   • Gestion de la selection dans la grille
    '------------------------------------------------------------
    Private Sub dgvEditeurs_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEditeurs.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Return

        BindSelectedToDetails()

    End Sub

#End Region

#Region "Validation"


    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ValidateForm
    '
    '   • Vérifie les champs obligatoires avant enregistrement
    '   • Alimente errProvider et la barre de statut
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtNomEditeur.Text) Then
            errProvider.SetError(txtNomEditeur, "Le nom de l'éditeur est obligatoire.")
            SetStatus("Nom éditeur manquant.")
            Return False
        End If

        Return True

    End Function


#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ApplySearch
    '
    '   • Applique le filtre de recherche sur la grille
    '   • Recharge soit la liste complète, soit la liste filtrée
    '   • Met à jour le compteur et le panneau détail
    '------------------------------------------------------------
    Private Sub ApplySearch()

        If _mode <> ModeEdition.Consultation Then
            SetStatus("Recherche indisponible pendant une édition.")
            Return
        End If

        Try

            Dim searchText As String = txtSearch.Text.Trim()
            Dim dt As DataTable

            If searchText = "" Then
                dt = GestionReferentiel.Editeurs_GetAll()
                SetStatus("Liste complète affichée.")
            Else
                dt = GestionReferentiel.Editeurs_GetBySearch(searchText, chkSearchNotes.Checked)

                If chkSearchNotes.Checked Then
                    SetStatus($"Filtre appliqué (avec notes) : '{searchText}'")
                Else
                    SetStatus($"Filtre appliqué : '{searchText}'")
                End If
            End If

            dgvEditeurs.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvEditeurs)

            If dgvEditeurs.Columns.Contains("id_editeur") Then
                dgvEditeurs.Columns("id_editeur").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("id_pays") Then
                dgvEditeurs.Columns("id_pays").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("created_at") Then
                dgvEditeurs.Columns("created_at").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("updated_at") Then
                dgvEditeurs.Columns("updated_at").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("notes_editeur_rtf") Then
                dgvEditeurs.Columns("notes_editeur_rtf").Visible = False
            End If

            If dgvEditeurs.Columns.Contains("notes_editeur_txt") Then
                dgvEditeurs.Columns("notes_editeur_txt").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} éditeur(s)"

            If UtilsForm.SelectFirstRow(dgvEditeurs, "nom_editeur") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur ApplySearch (Editeurs).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors de la recherche.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnSearch_Click
    '
    '   • Lance la recherche
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnClearSearch_Click
    '
    '   • Efface la recherche et recharge la liste complète
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()
        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' txtSearch_KeyDown
    '
    '   •	Détecte la touche Entrée pour lancer la recherche
    '   •	Améliore l'UX en évitant de cliquer sur le bouton
    '------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ApplySearch()
        End If

    End Sub

#End Region

End Class