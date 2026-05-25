'------------------------------------------------------------
' 📌 GestionImpression.vb
' Version : V1.0
' Date    : 14/03/2026
'
' Rôle :
' Formulaire de gestion du référentiel Impression.
'------------------------------------------------------------
Public Class GestionImpression

#Region "Déclarations"


    Private _mode As ModeEdition = ModeEdition.Consultation
    Private _snapshot As Impression = Nothing
    Private _currentId As ULong = 0

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.1 - 20/03/2026
    ' GestionImpression_Load
    '
    ' Initialise complètement le formulaire :
    ' - charge la grille
    ' - sélectionne automatiquement la première ligne
    ' - positionne l'UI en mode Consultation
    '------------------------------------------------------------
    Private Sub GestionImpression_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            InitToolTips()
            ClearErrors()
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbNote)

            LoadGrid()

            If dgvImpression.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Impressions chargées.")

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur chargement GestionImpression.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors du chargement des impressions." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' InitToolTips
    '
    ' Initialise les info-bulles du formulaire.
    '------------------------------------------------------------
    Private Sub InitToolTips()

        ttMain.SetToolTip(txtNomImpression, "Nom de l'impression.")
        ttMain.SetToolTip(txtDescriptionImpression, "Description complémentaire.")
        ttMain.SetToolTip(txtEnvieCal, "Champ libre de catégorisation ou envie Cal.")
        ttMain.SetToolTip(rtbNote, "Note libre associée à l'impression.")

        ttMain.SetToolTip(btnNew, "Créer une nouvelle impression")
        ttMain.SetToolTip(btnEdit, "Modifier l'impression sélectionnée")
        ttMain.SetToolTip(btnDelete, "Supprimer l'impression sélectionnée")
        ttMain.SetToolTip(btnSave, "Enregistrer")
        ttMain.SetToolTip(btnCancel, "Annuler l'édition")
        ttMain.SetToolTip(btnClose, "Fermer la fenêtre")

        ttMain.SetToolTip(txtSearch, "Texte de recherche")
        ttMain.SetToolTip(btnSearch, "Lancer la recherche")
        ttMain.SetToolTip(btnClearSearch, "Effacer la recherche")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' SetMode
    '
    ' Définit le mode d'édition du formulaire.
    '------------------------------------------------------------
    Private Sub SetMode(newMode As ModeEdition)

        _mode = newMode

        Select Case newMode

            Case ModeEdition.Consultation

                btnNew.Enabled = True
                btnEdit.Enabled = True
                btnDelete.Enabled = True
                btnSave.Enabled = False
                btnCancel.Enabled = False

                txtNomImpression.ReadOnly = True
                txtDescriptionImpression.ReadOnly = True
                txtEnvieCal.ReadOnly = True
                rtbNote.ReadOnly = True
                tsNotes.Enabled = False
                chkIsActif.Enabled = False

                dgvImpression.Enabled = True
                txtSearch.Enabled = True
                btnSearch.Enabled = True
                btnClearSearch.Enabled = True


            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNew.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False
                btnSave.Enabled = True
                btnCancel.Enabled = True

                txtNomImpression.ReadOnly = False
                txtDescriptionImpression.ReadOnly = False
                txtEnvieCal.ReadOnly = False
                rtbNote.ReadOnly = False
                tsNotes.Enabled = True
                chkIsActif.Enabled = True
                tsNotes.Enabled = True

                dgvImpression.Enabled = False
                txtSearch.Enabled = False
                btnSearch.Enabled = False
                btnClearSearch.Enabled = False

        End Select

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' HasSelectedImpression
    '
    ' Vérifie qu'une impression est actuellement sélectionnée.
    '------------------------------------------------------------
    Private Function HasSelectedImpression() As Boolean

        Return dgvImpression.CurrentRow IsNot Nothing AndAlso _currentId > 0UL

    End Function

#End Region

#Region "Interface utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' SetStatus
    '
    ' Affiche un message dans la barre d'état.
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)

        stsLabelStatus.Text = message

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' ClearErrors
    '
    ' Efface les erreurs visuelles de validation.
    '------------------------------------------------------------
    Private Sub ClearErrors()

        errProvider.SetError(txtNomImpression, "")
        errProvider.SetError(txtDescriptionImpression, "")
        errProvider.SetError(txtEnvieCal, "")
        errProvider.SetError(rtbNote, "")

    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnNew_Click
    '
    ' Création d'une nouvelle impression.
    '------------------------------------------------------------
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


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnEdit_Click
    '
    ' Modification de l'impression sélectionnée.
    '------------------------------------------------------------
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


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnSave_Click
    '
    ' Enregistre une création ou modification.
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Not ValidateForm() Then Return

        Try

            Dim current As Impression = GetCurrentImpressionFromFields()

            If _mode = ModeEdition.Nouveau Then

                GestionReferentiel.Impression_Insert(current)
                SetStatus("Impression créée.")

            Else

                current.IdImpression = _currentId
                GestionReferentiel.Impression_Update(current)

                SetStatus("Impression modifiée.")

            End If

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur btnSave_Click (Impression).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors de l'enregistrement.")

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnCancel_Click
    '
    ' Annule l'édition en cours.
    '------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        If _mode = ModeEdition.Nouveau Then

            If dgvImpression.CurrentRow IsNot Nothing Then
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

        ElseIf _mode = ModeEdition.Modification Then

            RestoreSnapshotToFields()

        End If

        SetMode(ModeEdition.Consultation)

        SetStatus("Modification annulée.")

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnDelete_Click
    '
    ' Supprime l'impression sélectionnée.
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If Not HasSelectedImpression() Then
            SetStatus("Aucune impression sélectionnée.")
            Return
        End If

        Try

            Dim nbLivres As Integer =
                GestionReferentiel.Impression_CountLivres(_currentId)

            Dim nbStaging As Integer =
                GestionReferentiel.Impression_CountLivresStaging(_currentId)

            Dim msg As String =
                $"Supprimer l'impression '{txtNomImpression.Text}' ?"

            If nbLivres > 0 OrElse nbStaging > 0 Then

                msg &= Environment.NewLine & Environment.NewLine &
                       $"Utilisée par {nbLivres} livre(s) et {nbStaging} livre(s) staging." &
                       Environment.NewLine &
                       "Les références seront mises à NULL."

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

            GestionReferentiel.Impression_Delete(_currentId)

            SetStatus("Impression supprimée.")

            LoadGrid()

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur suppression Impression.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnClose_Click
    '
    ' Ferme le formulaire.
    '------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

    Private Sub btnBold_Click(sender As Object, e As EventArgs) Handles btnBold.Click
        RichTextNotesHelper.BasculerGras(rtbNote)
    End Sub

    Private Sub btnItalic_Click(sender As Object, e As EventArgs) Handles btnItalic.Click
        RichTextNotesHelper.BasculerItalique(rtbNote)
    End Sub

    Private Sub btnUnderline_Click(sender As Object, e As EventArgs) Handles btnUnderline.Click
        RichTextNotesHelper.BasculerSouligne(rtbNote)
    End Sub

    Private Sub btnBullet_Click(sender As Object, e As EventArgs) Handles btnBullet.Click
        RichTextNotesHelper.BasculerListe(rtbNote)
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs) Handles btnTab.Click
        RichTextNotesHelper.InsererTabulation(rtbNote)
    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.1 - 20/03/2026
    ' LoadGrid
    '   
    ' Charge la grille des impressions.
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Dim dt As DataTable =
            GestionReferentiel.Impression_GetAll()

        dgvImpression.DataSource = dt

        UtilsForm.FormatReferentielGrid(dgvImpression)

        dgvImpression.Columns("id_impression").Visible = False
        dgvImpression.Columns("code_impression").Visible = False

        If dgvImpression.Columns.Contains("note_rtf") Then
            dgvImpression.Columns("note_rtf").Visible = False
        End If

        If dgvImpression.Columns.Contains("note_txt") Then
            dgvImpression.Columns("note_txt").Visible = False
        End If

        dgvImpression.Columns("created_at").Visible = False
        dgvImpression.Columns("updated_at").Visible = False

        lblCount.Text = $"{dt.Rows.Count} impression(s)"

        If UtilsForm.SelectFirstRow(dgvImpression, "nom_impression") Then
            BindSelectedToDetails()
        Else
            ClearDetails()
            _currentId = 0
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' BindSelectedToDetails
    '
    '   • Charge dans le panneau détail l'impression sélectionnée dans la grille
    '   • Met également à jour _currentId si l'identifiant est valide
    '------------------------------------------------------------
    Private Sub BindSelectedToDetails()

        If dgvImpression.CurrentRow Is Nothing Then
            ClearDetails()
            _currentId = 0
            Return
        End If

        Dim row = dgvImpression.CurrentRow

        txtIdImpression.Text = row.Cells("id_impression").Value?.ToString()
        txtCodeImpression.Text = row.Cells("code_impression").Value?.ToString()

        txtNomImpression.Text = row.Cells("nom_impression").Value?.ToString()
        txtDescriptionImpression.Text = row.Cells("description_impression").Value?.ToString()
        txtEnvieCal.Text = row.Cells("envie_Cal").Value?.ToString()

        Dim noteRtf As String = If(row.Cells("note_rtf").Value, "").ToString()
        Dim noteTxt As String = If(row.Cells("note_txt").Value, "").ToString()

        RichTextNotesHelper.ChargerContenuNotes(rtbNote, noteRtf, noteTxt)

        chkIsActif.Checked = Convert.ToBoolean(row.Cells("is_actif").Value)

        _currentId = SafeULong(txtIdImpression.Text)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' ClearDetails
    '
    ' Efface les données de l'impression.
    '------------------------------------------------------------
    Private Sub ClearDetails()

        txtIdImpression.Clear()
        txtCodeImpression.Clear()

        txtNomImpression.Clear()
        txtDescriptionImpression.Clear()
        txtEnvieCal.Clear()

        rtbNote.Clear()

        chkIsActif.Checked = True

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' dgvImpression_SelectionChanged
    '
    ' •	Événement déclenché lors du changement de sélection dans la grille
    ' •	Synchronise automatiquement les détails si en mode Consultation
    '------------------------------------------------------------
    Private Sub dgvImpression_SelectionChanged(sender As Object, e As EventArgs) Handles dgvImpression.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Return

        BindSelectedToDetails()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' SnapshotFromFields
    '
    '   •	Crée une copie temporaire des valeurs actuelles des champs
    '   •	Utilisé avant modification pour permettre l'annulation
    '------------------------------------------------------------
    Private Sub SnapshotFromFields()

        _snapshot = GetCurrentImpressionFromFields()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' RestoreSnapshotToFields
    '
    '   •	Restore les valeurs des champs depuis la copie temporaire
    '------------------------------------------------------------
    Private Sub RestoreSnapshotToFields()

        If _snapshot Is Nothing Then Return

        txtNomImpression.Text = _snapshot.NomImpression
        txtDescriptionImpression.Text = _snapshot.DescriptionImpression
        txtEnvieCal.Text = _snapshot.EnvieCal
        RichTextNotesHelper.ChargerContenuNotes(rtbNote, _snapshot.NoteRtf, _snapshot.NoteTxt)
        chkIsActif.Checked = _snapshot.IsActif

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' SafeULong
    '
    '   •	Convertit une chaîne en ULong 
    '   •	Retourne 0 si la conversion échoue ou si la chaîne est vide
    '------------------------------------------------------------
    Private Function SafeULong(value As String) As ULong

        If String.IsNullOrWhiteSpace(value) Then Return 0UL

        Dim result As ULong

        If ULong.TryParse(value, result) Then
            Return result
        End If

        Return 0UL

    End Function

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' ValidateForm
    '
    '   •	Vérifie les champs obligatoires avant enregistrement
    '   •	Alimente errProvider et la barre de statut
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtNomImpression.Text) Then

            errProvider.SetError(txtNomImpression, "Nom obligatoire.")
            SetStatus("Nom obligatoire.")
            txtNomImpression.Focus()

            Return False

        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' GetCurrentImpressionFromFields
    '
    '   •	Retourne une Impression avec les valeurs actuelles des champs
    '------------------------------------------------------------
    Private Function GetCurrentImpressionFromFields() As Impression

        Dim obj As New Impression

        With obj
            .NomImpression = txtNomImpression.Text.Trim()
            .DescriptionImpression = txtDescriptionImpression.Text.Trim()
            .EnvieCal = txtEnvieCal.Text.Trim()
            .NoteRtf = RichTextNotesHelper.GetNotesRtf(rtbNote)
            .NoteTxt = RichTextNotesHelper.GetNotesTxt(rtbNote)
            .IsActif = chkIsActif.Checked
        End With

        Return obj

    End Function

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnSearch_Click
    '
    '   •	Lance la recherche
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' btnClearSearch_Click
    '
    '   •	Efface la recherche et recharge la liste complète
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()
        LoadGrid()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' ApplySearch
    '
    '   •	Lance la recherche
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
                dt = GestionReferentiel.Impression_GetAll()
                SetStatus("Liste complète affichée.")
            Else
                dt = GestionReferentiel.Impression_GetBySearch(searchText, chkSearchNotes.Checked)
                If chkSearchNotes.Checked Then
                    SetStatus($"Filtre appliqué (avec notes) : '{searchText}'")
                Else
                    SetStatus($"Filtre appliqué : '{searchText}'")
                End If
            End If

            dgvImpression.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvImpression)

            dgvImpression.Columns("id_impression").Visible = False
            dgvImpression.Columns("code_impression").Visible = False
            dgvImpression.Columns("note_txt").Visible = False
            dgvImpression.Columns("created_at").Visible = False
            dgvImpression.Columns("updated_at").Visible = False

            lblCount.Text = $"{dt.Rows.Count} impression(s)"

            If UtilsForm.SelectFirstRow(dgvImpression, "nom_impression") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur ApplySearch (Impression).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors de la recherche.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' txtSearch_KeyDown
    '
    '   •	Détecte la touche Entrée pour lancer la recherche
    '------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown

        If e.KeyCode = Keys.Enter Then
            ApplySearch()
        End If

    End Sub

#End Region

End Class