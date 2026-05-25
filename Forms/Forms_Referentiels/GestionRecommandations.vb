'------------------------------------------------------------
' 📌 GestionRecommandations.vb
' Version : V1.1
' Date    : 19/03/2026
'
' Rôle :
' Formulaire WinForms de gestion du référentiel hiérarchique
' ref_origine_recommandation (origines parents) / recommandations (enfants).
'
' Règles Artefact :
' - Pas de SQL ici (QueryModule).
' - Pas d'accès DB direct ici (GestionReferentiel).
' - Validation UI : errProvider + StatusStrip.
' - Modèle à 2 niveaux : origine parent / recommandations liées.
'
' Évolution :
' - V1.0 : création de la structure initiale sur le modèle GestionRefEnum.
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionRecommandations

#Region "Déclarations"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Variables privées
    '
    '   • _mode               : mode courant du formulaire
    '   • _dtOrigines         : source de données des origines parents
    '   • _dtRecommandations  : source de données des recommandations enfants
    '------------------------------------------------------------
    Private _mode As ModeEdition = ModeEdition.Consultation
    Private _dtOrigines As DataTable
    Private _dtRecommandations As DataTable

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RecommandationContext
    '
    '   • Détermine le contexte courant selon l'onglet actif
    '   • Origines        : table parent ref_origine_recommandation
    '   • Recommandations : table enfant recommandations
    '------------------------------------------------------------
    Private Enum RecommandationContext
        Origines
        Recommandations
    End Enum

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' GestionRecommandations_Load
    '
    '   • Initialise complètement le formulaire au chargement
    '   • Charge les origines puis les recommandations liées
    '   • Configure l'UI et les info-bulles
    '------------------------------------------------------------
    Private Sub GestionRecommandations_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            InitTooltips()
            ClearErrors()
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbCommentaireRecommandation)

            LoadOrigines()
            LoadRecommandations()

            SetMode(ModeEdition.Consultation)
            SetStatus("Origines de recommandation chargées.")
            UpdateSearchControlsState()

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur chargement GestionRecommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors du chargement.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' GetCurrentContext
    '
    '   • Retourne le contexte courant selon l'onglet actif
    '------------------------------------------------------------
    Private Function GetCurrentContext() As RecommandationContext

        If tabMain.SelectedTab Is tabOrigines Then
            Return RecommandationContext.Origines
        End If

        Return RecommandationContext.Recommandations

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' InitTooltips
    '
    '   • Configure les info-bulles des contrôles principaux
    '------------------------------------------------------------
    Private Sub InitTooltips()

        If ttMain Is Nothing Then Return

        ttMain.SetToolTip(btnNew, "Créer un nouvel élément")
        ttMain.SetToolTip(btnEdit, "Passer en mode modification")
        ttMain.SetToolTip(btnSave, "Enregistrer les modifications")
        ttMain.SetToolTip(btnCancel, "Annuler les modifications")
        ttMain.SetToolTip(btnDelete, "Supprimer l'élément sélectionné")
        ttMain.SetToolTip(btnClose, "Fermer la fenêtre")
        ttMain.SetToolTip(btnSearch, "Appliquer le filtre")
        ttMain.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")

        ttMain.SetToolTip(txtCodeOrigineRecommandation, "Code technique de l'origine")
        ttMain.SetToolTip(txtLibelleOrigineRecommandation, "Libellé de l'origine de recommandation")
        ttMain.SetToolTip(txtCodeRecommandation, "Code technique de la recommandation")
        ttMain.SetToolTip(txtSourceNom, "Nom libre de la source")
        ttMain.SetToolTip(txtSourceLogin, "Login ou pseudo de la source")
        ttMain.SetToolTip(txtSourceUrl, "URL de la source")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V11 - 21/03/2026
    ' SetMode
    '
    '   • Définit le mode courant du formulaire
    '   • Active/désactive les boutons selon l'onglet actif
    '   • Passe les contrôles concernés en lecture seule ou en saisie
    '------------------------------------------------------------
    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Dim hasSelection As Boolean

        If GetCurrentContext() = RecommandationContext.Origines Then
            hasSelection =
                dgvOriginesRecommandation IsNot Nothing AndAlso
                dgvOriginesRecommandation.CurrentRow IsNot Nothing AndAlso
                dgvOriginesRecommandation.Rows.Count > 0
        Else
            hasSelection =
                dgvRecommandations IsNot Nothing AndAlso
                dgvRecommandations.CurrentRow IsNot Nothing AndAlso
                dgvRecommandations.Rows.Count > 0
        End If

        Select Case mode

            Case ModeEdition.Consultation

                btnNew.Enabled = True
                btnEdit.Enabled = hasSelection
                btnDelete.Enabled = hasSelection

                btnSave.Enabled = False
                btnCancel.Enabled = False

                If tabMain.SelectedTab Is tabOrigines Then
                    txtCodeOrigineRecommandation.ReadOnly = True
                    txtLibelleOrigineRecommandation.ReadOnly = True
                    nudOrdreAffichageOrigine.ReadOnly = True
                    chkIsActifOrigine.Enabled = False
                Else
                    txtCodeRecommandation.ReadOnly = True
                    txtSourceNom.ReadOnly = True
                    txtSourceLogin.ReadOnly = True
                    txtSourceUrl.ReadOnly = True
                    dtpDateRecommandation.Enabled = False
                    chkDateRecommandationVide.Enabled = False
                    rtbCommentaireRecommandation.ReadOnly = True
                    tsNotes.Enabled = False
                    chkIsActifRecommandation.Enabled = False
                    cboOrigineRecommandation.Enabled = False
                End If

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNew.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False

                btnSave.Enabled = True
                btnCancel.Enabled = True

                If tabMain.SelectedTab Is tabOrigines Then
                    txtCodeOrigineRecommandation.ReadOnly = True
                    txtLibelleOrigineRecommandation.ReadOnly = False
                    nudOrdreAffichageOrigine.ReadOnly = False
                    chkIsActifOrigine.Enabled = True
                Else
                    txtCodeRecommandation.ReadOnly = True
                    txtSourceNom.ReadOnly = False
                    txtSourceLogin.ReadOnly = False
                    txtSourceUrl.ReadOnly = False
                    dtpDateRecommandation.Enabled = True
                    chkDateRecommandationVide.Enabled = True
                    rtbCommentaireRecommandation.ReadOnly = False
                    tsNotes.Enabled = True
                    chkIsActifRecommandation.Enabled = True
                    cboOrigineRecommandation.Enabled = True
                End If

        End Select

    End Sub

#End Region

#Region "Interface utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' SetStatus
    '
    '   • Affiche un message dans la barre de statut
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)

        stsLabelStatus.Text = message

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' ClearErrors
    '
    '   • Efface les erreurs de validation affichées dans l’UI
    '------------------------------------------------------------
    Private Sub ClearErrors()

        errProvider.Clear()

    End Sub



    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' DbToBool
    '
    '   • Convertit une valeur DB vers Boolean
    '------------------------------------------------------------
    Private Function DbToBool(value As Object) As Boolean

        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        If TypeOf value Is Boolean Then Return CBool(value)

        Dim s As String = value.ToString().Trim()
        If s = "1" Then Return True
        If s = "0" Then Return False

        Dim b As Boolean
        If Boolean.TryParse(s, b) Then Return b

        Return False

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' DbToInt
    '
    '   • Convertit une valeur DB vers Integer
    '------------------------------------------------------------
    Private Function DbToInt(value As Object) As Integer

        If value Is Nothing OrElse value Is DBNull.Value Then Return 0
        If value Is DBNull.Value Then Return 0

        Dim n As Integer
        If Integer.TryParse(value.ToString(), n) Then Return n
        Return 0

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' BindSelectedOrigineToDetails
    '
    '   • Charge l'origine parent sélectionnée dans les champs détail
    '------------------------------------------------------------
    Private Sub BindSelectedOrigineToDetails()

        If dgvOriginesRecommandation.CurrentRow Is Nothing Then
            ClearOrigineDetails()
            Return
        End If

        Dim row As DataGridViewRow = dgvOriginesRecommandation.CurrentRow

        txtidOrigineRecommandation.Text = row.Cells("id_origine_recommandation").Value?.ToString()
        txtCodeOrigineRecommandation.Text = row.Cells("code_origine_recommandation").Value?.ToString()

        txtLibelleOrigineRecommandation.Text = row.Cells("libelle_origine_recommandation").Value?.ToString()

        nudOrdreAffichageOrigine.Value = DbToInt(row.Cells("ordre_affichage").Value)

        chkIsActifOrigine.Checked = DbToBool(row.Cells("is_actif").Value)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' BindSelectedRecommandationToDetails
    '
    '   • Charge la recommandation enfant sélectionnée dans les champs détail
    '------------------------------------------------------------
    Private Sub BindSelectedRecommandationToDetails()

        If dgvRecommandations.CurrentRow Is Nothing Then
            ClearRecommandationDetails()
            Return
        End If

        Dim row As DataGridViewRow = dgvRecommandations.CurrentRow

        txtidRecommandation.Text = row.Cells("id_recommandation").Value?.ToString()
        txtCodeRecommandation.Text = row.Cells("code_recommandation").Value?.ToString()

        txtSourceNom.Text = row.Cells("source_nom").Value?.ToString()
        txtSourceLogin.Text = row.Cells("source_login").Value?.ToString()
        txtSourceUrl.Text = row.Cells("source_url").Value?.ToString()

        If row.Cells("date_recommandation").Value Is Nothing OrElse row.Cells("date_recommandation").Value Is DBNull.Value Then
            chkDateRecommandationVide.Checked = True
            dtpDateRecommandation.Value = Date.Today
        Else
            chkDateRecommandationVide.Checked = False
            dtpDateRecommandation.Value = CDate(row.Cells("date_recommandation").Value)
        End If

        Dim commentaireRtf As String = If(row.Cells("commentaire_rtf").Value, "").ToString()
        Dim commentaireTxt As String = If(row.Cells("commentaire_txt").Value, "").ToString()

        RichTextNotesHelper.ChargerContenuNotes(rtbCommentaireRecommandation, commentaireRtf, commentaireTxt)

        If row.Cells("id_origine_recommandation").Value Is Nothing OrElse
   row.Cells("id_origine_recommandation").Value Is DBNull.Value Then
            cboOrigineRecommandation.SelectedIndex = -1
        Else
            cboOrigineRecommandation.SelectedValue = Convert.ToUInt64(row.Cells("id_origine_recommandation").Value)
        End If

        chkIsActifRecommandation.Checked = DbToBool(row.Cells("is_actif").Value)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' ClearOrigineDetails
    '
    '   • Réinitialise les champs détail de l'origine parent
    '------------------------------------------------------------
    Private Sub ClearOrigineDetails()

        txtidOrigineRecommandation.Clear()
        txtCodeOrigineRecommandation.Clear()

        txtLibelleOrigineRecommandation.Clear()

        nudOrdreAffichageOrigine.Value = 0

        chkIsActifOrigine.Checked = True

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' ClearRecommandationDetails
    '
    '   • Réinitialise les champs détail de la recommandation enfant
    '------------------------------------------------------------
    Private Sub ClearRecommandationDetails()

        txtidRecommandation.Clear()
        txtCodeRecommandation.Clear()

        txtSourceNom.Clear()
        txtSourceLogin.Clear()
        txtSourceUrl.Clear()

        chkDateRecommandationVide.Checked = True
        dtpDateRecommandation.Value = Date.Today

        rtbCommentaireRecommandation.Clear()

        chkIsActifRecommandation.Checked = True

        If cboOrigineRecommandation.Items.Count > 0 Then
            cboOrigineRecommandation.SelectedIndex = -1
        End If

    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' btnNew_Click
    '
    '   • Prépare la création selon le contexte
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        ClearErrors()

        Select Case GetCurrentContext()
            Case RecommandationContext.Origines
                ClearOrigineDetails()
            Case RecommandationContext.Recommandations
                ClearRecommandationDetails()
                If cboFiltreOrigineRecommandation.SelectedValue IsNot Nothing AndAlso
   cboFiltreOrigineRecommandation.SelectedValue IsNot DBNull.Value Then

                    Dim idFiltre As ULong = Convert.ToUInt64(cboFiltreOrigineRecommandation.SelectedValue)

                    If idFiltre > 0UL Then
                        cboOrigineRecommandation.SelectedValue = idFiltre
                    End If
                End If
        End Select

        SetMode(ModeEdition.Nouveau)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' btnEdit_Click
    '
    '   • Passe en mode modification
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        ClearErrors()

        Select Case GetCurrentContext()

            Case RecommandationContext.Origines

                If dgvOriginesRecommandation.CurrentRow Is Nothing Then
                    SetStatus("Aucune origine sélectionnée.")
                    Return
                End If

            Case RecommandationContext.Recommandations

                If dgvRecommandations.CurrentRow Is Nothing Then
                    SetStatus("Aucune recommandation sélectionnée.")
                    Return
                End If

        End Select

        SetMode(ModeEdition.Modification)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' btnSave_Click
    '
    '   • Enregistre selon le contexte
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            Select Case GetCurrentContext()
                Case RecommandationContext.Origines
                    SaveOrigine()
                Case RecommandationContext.Recommandations
                    SaveRecommandation()
            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur Save Recommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors de l'enregistrement.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' btnCancel_Click
    '
    '   • Annule l'édition
    '------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        ClearErrors()

        SetMode(ModeEdition.Consultation)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' btnDelete_Click
    '
    '   • Supprime selon le contexte
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try

            Select Case GetCurrentContext()

                Case RecommandationContext.Origines
                    DeleteOrigine()

                Case RecommandationContext.Recommandations
                    DeleteRecommandation()

            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur Delete Recommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' btnClose_Click
    '
    '   • Ferme le formulaire
    '------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' tabMain_SelectedIndexChanged
    '
    '   • Adaptation UI selon l'onglet
    '------------------------------------------------------------
    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged

        UpdateSearchControlsState()
        SetMode(ModeEdition.Consultation)

        If tabMain.SelectedTab Is tabOrigines Then
            lblCount.Text = $"{_dtOrigines.Rows.Count} Origine(s)."
        Else
            lblCount.Text = $"{_dtRecommandations.Rows.Count} Recommandation(s)."
        End If

    End Sub

    Private Sub btnBold_Click(sender As Object, e As EventArgs) Handles btnBold.Click
        RichTextNotesHelper.BasculerGras(rtbCommentaireRecommandation)
    End Sub

    Private Sub btnItalic_Click(sender As Object, e As EventArgs) Handles btnItalic.Click
        RichTextNotesHelper.BasculerItalique(rtbCommentaireRecommandation)
    End Sub

    Private Sub btnUnderline_Click(sender As Object, e As EventArgs) Handles btnUnderline.Click
        RichTextNotesHelper.BasculerSouligne(rtbCommentaireRecommandation)
    End Sub

    Private Sub btnBullet_Click(sender As Object, e As EventArgs) Handles btnBullet.Click
        RichTextNotesHelper.BasculerListe(rtbCommentaireRecommandation)
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs) Handles btnTab.Click
        RichTextNotesHelper.InsererTabulation(rtbCommentaireRecommandation)
    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' LoadOrigines
    '
    '   • Charge les origines parents
    '   • Met à jour la grille, le compteur et le combo
    '------------------------------------------------------------
    Private Sub LoadOrigines()

        _dtOrigines = GestionReferentiel.OrigineRecommandation_GetAll()

        dgvOriginesRecommandation.DataSource = _dtOrigines

        UtilsForm.FormatReferentielGrid(dgvOriginesRecommandation)

        If dgvOriginesRecommandation.Columns.Contains("id_origine_recommandation") Then
            dgvOriginesRecommandation.Columns("id_origine_recommandation").Visible = False
        End If

        lblCount.Text = $"{_dtOrigines.Rows.Count} Origine(s)."

        If UtilsForm.SelectFirstRow(dgvOriginesRecommandation, "libelle_origine_recommandation") Then
            BindSelectedOrigineToDetails()
        Else
            ClearOrigineDetails()
            ClearRecommandationDetails()
        End If

        ' Combo filtre
        Dim dtFiltre As DataTable = _dtOrigines.Copy()
        Dim rowAll As DataRow = dtFiltre.NewRow()
        rowAll("id_origine_recommandation") = 0
        rowAll("libelle_origine_recommandation") = "[Toutes les origines]"
        dtFiltre.Rows.InsertAt(rowAll, 0)

        cboFiltreOrigineRecommandation.DataSource = dtFiltre
        cboFiltreOrigineRecommandation.DisplayMember = "libelle_origine_recommandation"
        cboFiltreOrigineRecommandation.ValueMember = "id_origine_recommandation"

        ' Combo détail
        Dim dtDetail As DataTable = _dtOrigines.Copy()

        cboOrigineRecommandation.DataSource = dtDetail
        cboOrigineRecommandation.DisplayMember = "libelle_origine_recommandation"
        cboOrigineRecommandation.ValueMember = "id_origine_recommandation"

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 19/03/2026
    ' LoadRecommandations
    '
    '   • Charge les recommandations selon origine sélectionnée
    '------------------------------------------------------------
    Private Sub LoadRecommandations()

        If cboFiltreOrigineRecommandation.SelectedValue Is Nothing Then
            ClearRecommandationDetails()
            Exit Sub
        End If

        Dim idOrigine As ULong = Convert.ToUInt64(cboFiltreOrigineRecommandation.SelectedValue)

        If idOrigine = 0UL Then
            _dtRecommandations = GestionReferentiel.Recommandation_GetAll(chkActifsOnly.Checked)
        Else
            _dtRecommandations = GestionReferentiel.Recommandation_GetByOrigine(idOrigine, chkActifsOnly.Checked)
        End If

        '_dtRecommandations = GestionReferentiel.Recommandation_GetByOrigine(idOrigine, chkActifsOnly.Checked)

        dgvRecommandations.DataSource = _dtRecommandations

        UtilsForm.FormatReferentielGrid(dgvRecommandations)

        If dgvRecommandations.Columns.Contains("id_recommandation") Then
            dgvRecommandations.Columns("id_recommandation").Visible = False
        End If

        If dgvRecommandations.Columns.Contains("id_origine_recommandation") Then
            dgvRecommandations.Columns("id_origine_recommandation").Visible = False
        End If

        If tabMain.SelectedTab Is tabOrigines Then
            lblCount.Text = $"{_dtOrigines.Rows.Count} Origine(s)."
        Else
            lblCount.Text = $"{_dtRecommandations.Rows.Count} Recommandation(s)."
        End If

        If dgvRecommandations.Columns.Contains("commentaire_rtf") Then
            dgvRecommandations.Columns("commentaire_rtf").Visible = False
        End If

        If dgvRecommandations.Columns.Contains("commentaire_txt") Then
            dgvRecommandations.Columns("commentaire_txt").Visible = False
        End If

        If UtilsForm.SelectFirstRow(dgvRecommandations, "code_recommandation") Then
            BindSelectedRecommandationToDetails()
        Else
            ClearRecommandationDetails()
        End If

        If tabMain.SelectedTab Is tabOrigines Then
            lblCount.Text = $"{_dtOrigines.Rows.Count} Origine(s)."
        Else
            lblCount.Text = $"{_dtRecommandations.Rows.Count} Recommandation(s)."
        End If

        If UtilsForm.SelectFirstRow(dgvRecommandations, "code_recommandation") Then
            BindSelectedRecommandationToDetails()
        Else
            ClearRecommandationDetails()
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' dgvOriginesRecommandation_SelectionChanged
    '
    '   • Synchronise détail origine + combo recommandations
    '------------------------------------------------------------
    Private Sub dgvOriginesRecommandation_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOriginesRecommandation.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub

        Try

            BindSelectedOrigineToDetails()

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur SelectionChanged Origines.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' dgvRecommandations_SelectionChanged
    '
    '   • Synchronise détail recommandation
    '------------------------------------------------------------
    Private Sub dgvRecommandations_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRecommandations.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub

        Try

            BindSelectedRecommandationToDetails()

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur SelectionChanged Recommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' cboOrigineRecommandation_SelectedIndexChanged
    '
    '   • Recharge les recommandations quand origine change
    '------------------------------------------------------------
    Private Sub cboOrigineRecommandation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFiltreOrigineRecommandation.SelectedIndexChanged

        Try

            LoadRecommandations()

            SetStatus("Recommandations chargées.")

        Catch ex As Exception

            EcrireLog("UI: erreur chargement recommandations.", LogLevel.Succinct, LogCategory.UI, ex)

            SetStatus("Erreur chargement recommandations.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' chkActifsOnly_CheckedChanged
    '
    '   • Recharge les recommandations selon filtre actif
    '------------------------------------------------------------
    Private Sub chkActifsOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkActifsOnly.CheckedChanged

        LoadRecommandations()

    End Sub

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' ValidateOrigineForm
    '
    '   • Vérifie les champs obligatoires origine
    '------------------------------------------------------------
    Private Function ValidateOrigineForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtLibelleOrigineRecommandation.Text) Then
            errProvider.SetError(txtLibelleOrigineRecommandation, "Libellé obligatoire.")
            SetStatus("Libellé origine manquant.")
            Return False
        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' ValidateRecommandationForm
    '
    '   • Vérifie les champs obligatoires recommandation
    '------------------------------------------------------------
    Private Function ValidateRecommandationForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtSourceNom.Text) Then
            errProvider.SetError(txtSourceNom, "Nom source obligatoire.")
            SetStatus("Nom source manquant.")
            Return False
        End If

        If cboOrigineRecommandation.SelectedValue Is Nothing OrElse
   cboOrigineRecommandation.SelectedValue Is DBNull.Value OrElse
   Convert.ToUInt64(cboOrigineRecommandation.SelectedValue) = 0UL Then

            errProvider.SetError(cboOrigineRecommandation, "Origine obligatoire.")
            SetStatus("Origine obligatoire.")
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Méthodes métier internes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' SaveOrigine
    '
    '   • Sauvegarde une origine parent
    '------------------------------------------------------------
    Private Sub SaveOrigine()

        If Not ValidateOrigineForm() Then Exit Sub

        Dim o As New RefOrigineRecommandation

        o.LibelleOrigineRecommandation = txtLibelleOrigineRecommandation.Text.Trim()
        o.OrdreAffichage = CInt(nudOrdreAffichageOrigine.Value)
        o.IsActif = chkIsActifOrigine.Checked

        If _mode = ModeEdition.Nouveau Then
            GestionReferentiel.OrigineRecommandation_Insert(o)
            SetStatus("Origine ajoutée.")
        ElseIf _mode = ModeEdition.Modification Then
            o.IdOrigineRecommandation = Convert.ToUInt64(txtidOrigineRecommandation.Text)
            GestionReferentiel.OrigineRecommandation_Update(o)
            SetStatus("Origine modifiée.")
        End If

        LoadOrigines()
        SetMode(ModeEdition.Consultation)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' SaveRecommandation
    '
    '   • Sauvegarde une recommandation enfant
    '------------------------------------------------------------
    Private Sub SaveRecommandation()

        If Not ValidateRecommandationForm() Then Exit Sub

        Dim r As New Recommandation

        r.SourceNom = txtSourceNom.Text.Trim()
        r.SourceLogin = txtSourceLogin.Text.Trim()
        r.SourceURL = txtSourceUrl.Text.Trim()

        If chkDateRecommandationVide.Checked Then
            r.DateRecommandation = Nothing
        Else
            r.DateRecommandation = dtpDateRecommandation.Value
        End If

        r.CommentaireRtf = RichTextNotesHelper.GetNotesRtf(rtbCommentaireRecommandation)
        r.CommentaireTxt = RichTextNotesHelper.GetNotesTxt(rtbCommentaireRecommandation)
        r.IsActif = chkIsActifRecommandation.Checked

        r.IdOrigineRecommandation = Convert.ToUInt64(cboOrigineRecommandation.SelectedValue)

        If _mode = ModeEdition.Nouveau Then
            GestionReferentiel.Recommandation_Insert(r)
            SetStatus("Recommandation ajoutée.")
        ElseIf _mode = ModeEdition.Modification Then
            r.IdRecommandation = Convert.ToUInt64(txtidRecommandation.Text)
            GestionReferentiel.Recommandation_Update(r)
            SetStatus("Recommandation modifiée.")
        End If

        LoadRecommandations()
        SetMode(ModeEdition.Consultation)

    End Sub

#End Region

#Region "Méthodes métier internes - DELETE"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' DeleteOrigine
    '
    '   • Supprime une origine
    '   • Gère les recommandations liées
    '------------------------------------------------------------
    Private Sub DeleteOrigine()

        Try
            Dim id As ULong = UtilsForm.DgvGetSelectedId(dgvOriginesRecommandation, "id_origine_recommandation")

            If id = 0 Then
                SetStatus("Aucune origine sélectionnée.")
                Exit Sub
            End If

            Dim nbRecommandations As Integer = GestionReferentiel.Recommandation_CountByOrigine(id)

            '--------------------------------------------
            ' Cas 1 : aucune recommandation liée
            '--------------------------------------------
            If nbRecommandations = 0 Then

                Dim rep = MessageBox.Show(
                    "Supprimer cette origine ?",
                    "Artefact",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                )

                If rep <> DialogResult.Yes Then
                    SetStatus("Suppression annulée.")
                    Exit Sub
                End If

                GestionReferentiel.OrigineRecommandation_Delete(id)

                LoadOrigines()
                ClearOrigineDetails()

                SetMode(ModeEdition.Consultation)

                SetStatus("Origine supprimée.")
                Exit Sub

            End If

            '--------------------------------------------
            ' Cas 2 : recommandations liées
            '--------------------------------------------
            Dim choix = MessageBox.Show(
                $"Cette origine contient {nbRecommandations} recommandation(s)." & Environment.NewLine & Environment.NewLine &
                "Oui  = aller voir les recommandations liées" & Environment.NewLine &
                "Non  = supprimer l'origine et ses recommandations" & Environment.NewLine &
                "Annuler = ne rien faire",
                "Suppression avec dépendances",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            )

            Select Case choix

                Case DialogResult.Yes
                    AllerSurRecommandationsLiees(id)
                    SetStatus("Recommandations liées affichées.")
                    Exit Sub

                Case DialogResult.No

                    Dim repSuppression = MessageBox.Show(
                        "Confirmer la suppression de l'origine ET de toutes ses recommandations ?",
                        "Artefact",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    )

                    If repSuppression <> DialogResult.Yes Then
                        SetStatus("Suppression annulée.")
                        Exit Sub
                    End If

                    GestionReferentiel.OrigineRecommandation_DeleteWithRecommandations(id)

                    LoadOrigines()
                    LoadRecommandations()

                    ClearOrigineDetails()
                    ClearRecommandationDetails()

                    SetMode(ModeEdition.Consultation)

                    SetStatus("Origine et recommandations supprimées.")
                    Exit Sub

                Case Else
                    SetStatus("Suppression annulée.")
                    Exit Sub

            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur suppression origine.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' DeleteRecommandation
    '
    '   • Supprime une recommandation
    '   • Vérifie les usages (livres + staging)
    '------------------------------------------------------------
    Private Sub DeleteRecommandation()

        If dgvRecommandations.CurrentRow Is Nothing Then
            SetStatus("Aucune recommandation sélectionnée.")
            Exit Sub
        End If

        Dim id As ULong = Convert.ToUInt64(txtidRecommandation.Text)

        '--------------------------------------------
        ' Comptage usages
        '--------------------------------------------
        Dim nbLivres = GestionReferentiel.Recommandation_CountUsageInLivres(id)
        Dim nbStaging = GestionReferentiel.Recommandation_CountUsageInStaging(id)

        Dim totalUsage = nbLivres + nbStaging

        '--------------------------------------------
        ' Cas utilisé → blocage
        '--------------------------------------------
        If totalUsage > 0 Then

            Dim msg =
                "Suppression impossible : cette recommandation est utilisée." &
                Environment.NewLine & Environment.NewLine &
                $"Livres : {nbLivres}" & Environment.NewLine &
                $"Staging : {nbStaging}" & Environment.NewLine & Environment.NewLine &
                "Supprimez ou modifiez ces liens avant suppression."

            MessageBox.Show(msg, "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            SetStatus("Suppression impossible : recommandation utilisée.")
            Exit Sub

        End If

        '--------------------------------------------
        ' Confirmation simple
        '--------------------------------------------
        Dim rep = MessageBox.Show(
            "Supprimer cette recommandation ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If rep <> DialogResult.Yes Then Exit Sub

        '--------------------------------------------
        ' Suppression
        '--------------------------------------------
        GestionReferentiel.Recommandation_Delete(id)

        LoadRecommandations()

        SetStatus("Recommandation supprimée.")

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' AllerSurRecommandationsLiees
    '
    '   • Positionne l'UI sur les recommandations liées
    '------------------------------------------------------------
    Private Sub AllerSurRecommandationsLiees(idOrigine As ULong)

        Try

            tabMain.SelectedTab = tabRecommandations

            cboFiltreOrigineRecommandation.SelectedValue = idOrigine

            LoadRecommandations()

            If dgvRecommandations.Rows.Count > 0 Then
                dgvRecommandations.Rows(0).Selected = True
                BindSelectedRecommandationToDetails()
            Else
                ClearRecommandationDetails()
            End If

            SetStatus("Recommandations liées affichées.")

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur AllerSurRecommandationsLiees.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors de l'affichage.")

        End Try

    End Sub

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' ApplySearch
    '
    '   • Applique la recherche sur les recommandations
    '   • Peut inclure ou non le champ commentaire
    '------------------------------------------------------------
    Private Sub ApplySearch()

        If _mode <> ModeEdition.Consultation Then
            SetStatus("Recherche indisponible pendant une édition.")
            Return
        End If

        Try

            If cboFiltreOrigineRecommandation.SelectedValue Is Nothing Then
                SetStatus("Aucune origine sélectionnée.")
                Exit Sub
            End If

            Dim idOrigine As ULong = Convert.ToUInt64(cboFiltreOrigineRecommandation.SelectedValue)
            Dim searchText As String = txtSearch.Text.Trim()

            Dim dt As DataTable

            If searchText = "" Then

                If idOrigine = 0UL Then
                    dt = GestionReferentiel.Recommandation_GetAll(chkActifsOnly.Checked)
                Else
                    dt = GestionReferentiel.Recommandation_GetByOrigine(idOrigine, chkActifsOnly.Checked)
                End If

                SetStatus("Liste complète affichée.")

            Else

                If idOrigine = 0UL Then
                    dt = GestionReferentiel.Recommandation_GetBySearch(
                    searchText,
                    chkActifsOnly.Checked,
                    chkSearchNotes.Checked
                )
                Else
                    dt = GestionReferentiel.Recommandation_GetByOrigineAndSearch(
                    idOrigine,
                    searchText,
                    chkActifsOnly.Checked,
                    chkSearchNotes.Checked
                )
                End If

                If chkSearchNotes.Checked Then
                    SetStatus($"Filtre appliqué (avec notes) : '{searchText}'")
                Else
                    SetStatus($"Filtre appliqué : '{searchText}'")
                End If

            End If

            _dtRecommandations = dt
            dgvRecommandations.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvRecommandations)

            If dgvRecommandations.Columns.Contains("id_recommandation") Then
                dgvRecommandations.Columns("id_recommandation").Visible = False
            End If

            If dgvRecommandations.Columns.Contains("id_origine_recommandation") Then
                dgvRecommandations.Columns("id_origine_recommandation").Visible = False
            End If

            If dgvRecommandations.Columns.Contains("commentaire_rtf") Then
                dgvRecommandations.Columns("commentaire_rtf").Visible = False
            End If

            If dgvRecommandations.Columns.Contains("commentaire_txt") Then
                dgvRecommandations.Columns("commentaire_txt").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} Recommandation(s)."

            If UtilsForm.SelectFirstRow(dgvRecommandations, "code_recommandation") Then
                BindSelectedRecommandationToDetails()
            Else
                ClearRecommandationDetails()
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur ApplySearch (Recommandations).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de la recherche.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 19/03/2026
    ' btnSearch_Click
    '
    '   • Lance la recherche selon le contexte courant
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Try

            Select Case GetCurrentContext()

                Case RecommandationContext.Origines
                    SetStatus("Recherche non utilisée sur les origines.")

                Case RecommandationContext.Recommandations
                    ApplySearch()

            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur recherche GestionRecommandations.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur recherche.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 19/03/2026
    ' btnClearSearch_Click
    '
    '   • Efface la recherche et recharge la liste complète
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()
        chkSearchNotes.Checked = False
        LoadRecommandations()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 21/03/2026
    ' UpdateSearchControlsState
    '
    '   • Active ou désactive les contrôles de recherche
    '   • La recherche ne s'applique qu'aux recommandations
    '------------------------------------------------------------
    Private Sub UpdateSearchControlsState()

        Dim isRecommandationsTab As Boolean = (tabMain.SelectedTab Is tabRecommandations)

        txtSearch.Enabled = isRecommandationsTab
        btnSearch.Enabled = isRecommandationsTab
        btnClearSearch.Enabled = isRecommandationsTab
        chkActifsOnly.Enabled = isRecommandationsTab
        chkSearchNotes.Enabled = isRecommandationsTab
        cboFiltreOrigineRecommandation.Enabled = isRecommandationsTab

        If Not isRecommandationsTab Then
            txtSearch.Clear()
            chkSearchNotes.Checked = False
        End If

    End Sub

#End Region

End Class