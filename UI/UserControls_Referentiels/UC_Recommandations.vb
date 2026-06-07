'====================================================================
' 📌 UC_Recommandations.vb
' Version : V1.0
' Date    : 24/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion hiérarchique ref_origine_recommandation / recommandations.
' Approche Master-Detail unifiée : origines à gauche, détails origine + recommandations à droite.
' RichTextBox enrichi pour les notes de recommandations (commentaire_rtf/txt).
'
' Règles :
' - Implémente IContextAwareUserControl
' - Utilise UserControlContext pour StatusStrip, ErrorProvider, ToolTip
' - Modes duaux indépendants : _modeOrigine et _modeRecommandation
' - RichTextBox géré avec RichTextNotesHelper et UC_RichTextToolbar
' - Recherche dans notes optionnelle (checkbox chkSearchNotes)
'
' Évolution :
' - V1.0 : Migration depuis GestionRecommandations.vb avec approche Master-Detail
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_Recommandations
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'édition duaux (origine et recommandation indépendants)
    Private _modeOrigine As ModeEdition = ModeEdition.Consultation
    Private _modeRecommandation As ModeEdition = ModeEdition.Consultation

    ' Snapshots pour annulation
    Private _snapshotOrigine As RefOrigineRecommandation = Nothing
    Private _snapshotRecommandation As Recommandation = Nothing

    ' Identifiants courants
    Private _currentIdOrigine As ULong = 0
    Private _currentIdRecommandation As ULong = 0

    ' Contexte partagé
    Private _context As UserControlContext = Nothing

#End Region

#Region "Constructeur"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "IContextAwareUserControl"

    Public Sub SetContext(context As UserControlContext) Implements IContextAwareUserControl.SetContext
        _context = context
    End Sub

    Public Sub OnActivated() Implements IContextAwareUserControl.OnActivated
        Try
            If _context IsNot Nothing Then
                _context.NavigateToLevel("Recommandations")
                _context.SetMode("Référentiels hiérarchiques")
                SetStatus("Origines et recommandations chargées.")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_Recommandations.",
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
                "UI: erreur désactivation UC_Recommandations.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

#End Region

#Region "Initialisation"

    Private Sub UC_Recommandations_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()

            ' Configurer le RichTextBox pour les notes
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbCommentaire)

            ' Configurer le toolbar pour le RichTextBox
            ucRichTextToolbar.TargetRichTextBox = rtbCommentaire

            LoadOrigines()

            If dgvOrigines.Rows.Count = 0 Then
                ClearOrigineDetails()
                ClearRecommandationsArea()
            End If

            SetStatus("Origines de recommandation chargées.")
            ' SetModeOrigine et SetModeRecommandation sont appelés par LoadOrigines après binding

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur UC_Recommandations_Load.",
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

        ' Recherche origines
        tt.SetToolTip(btnSearchOrigines, "Rechercher une origine")
        tt.SetToolTip(btnClearSearchOrigines, "Effacer le filtre")

        ' CRUD Origine
        tt.SetToolTip(btnNewOrigine, "Créer une nouvelle origine")
        tt.SetToolTip(btnEditOrigine, "Modifier l'origine sélectionnée")
        tt.SetToolTip(btnSaveOrigine, "Enregistrer l'origine")
        tt.SetToolTip(btnCancelOrigine, "Annuler les modifications")
        tt.SetToolTip(btnDeleteOrigine, "Supprimer l'origine sélectionnée")

        ' Champs Origine
        tt.SetToolTip(txtLibelleOrigine, "Libellé de l'origine")
        tt.SetToolTip(nudOrdreOrigine, "Ordre d'affichage")
        tt.SetToolTip(chkOrigineActif, "Origine active")

        ' Recherche recommandations
        tt.SetToolTip(btnSearchRecommandations, "Rechercher une recommandation")
        tt.SetToolTip(btnClearSearchRecommandations, "Effacer le filtre")

        ' CRUD Recommandation
        tt.SetToolTip(btnNewRecommandation, "Créer une nouvelle recommandation")
        tt.SetToolTip(btnEditRecommandation, "Modifier la recommandation sélectionnée")
        tt.SetToolTip(btnSaveRecommandation, "Enregistrer la recommandation")
        tt.SetToolTip(btnCancelRecommandation, "Annuler les modifications")
        tt.SetToolTip(btnDeleteRecommandation, "Supprimer la recommandation sélectionnée")

        ' Champs Recommandation
        tt.SetToolTip(txtSourceNom, "Nom de la source")
        tt.SetToolTip(txtSourceLogin, "Login ou pseudo de la source")
        tt.SetToolTip(txtSourceUrl, "URL de la source")
        tt.SetToolTip(dtpDateRecommandation, "Date de la recommandation")
        tt.SetToolTip(chkRecommandationActive, "Recommandation active")
        tt.SetToolTip(rtbCommentaire, "Notes riches (RTF) de la recommandation")

    End Sub

#End Region

#Region "Gestion des modes"

    Private Sub SetModeOrigine(mode As ModeEdition)

        _modeOrigine = mode

        Dim hasSelection As Boolean =
            dgvOrigines IsNot Nothing AndAlso
            dgvOrigines.CurrentRow IsNot Nothing AndAlso
            dgvOrigines.Rows.Count > 0

        ' ✅ Utilisation du helper pour configurer les boutons CRUD
        UtilsUCReferentiels.ConfigurerBoutonsMode(
            mode,
            hasSelection,
            btnNewOrigine, btnEditOrigine, btnSaveOrigine, btnCancelOrigine, btnDeleteOrigine
        )

        ' Gestion des champs spécifiques
        Select Case mode

            Case ModeEdition.Consultation

                txtLibelleOrigine.ReadOnly = True
                nudOrdreOrigine.ReadOnly = True
                chkOrigineActif.Enabled = False

                ' Réactiver les contrôles recommandations
                EnableRecommandationsControls(True)

            Case ModeEdition.Nouveau, ModeEdition.Modification

                txtLibelleOrigine.ReadOnly = False
                nudOrdreOrigine.ReadOnly = False
                chkOrigineActif.Enabled = True

                ' Désactiver les contrôles recommandations pendant édition origine
                EnableRecommandationsControls(False)

        End Select

    End Sub

    Private Sub SetModeRecommandation(mode As ModeEdition)

        _modeRecommandation = mode

        Dim hasSelection As Boolean =
            dgvRecommandations IsNot Nothing AndAlso
            dgvRecommandations.CurrentRow IsNot Nothing AndAlso
            dgvRecommandations.Rows.Count > 0

        ' ✅ Utilisation du helper pour configurer les boutons CRUD
        ' Note: btnNew est spécial (dépend de _currentIdOrigine), on le gère manuellement
        Select Case mode

            Case ModeEdition.Consultation

                btnNewRecommandation.Enabled = (_currentIdOrigine > 0)
                btnEditRecommandation.Enabled = hasSelection
                btnDeleteRecommandation.Enabled = hasSelection

                btnSaveRecommandation.Enabled = False
                btnCancelRecommandation.Enabled = False

                txtSourceNom.ReadOnly = True
                txtSourceLogin.ReadOnly = True
                txtSourceUrl.ReadOnly = True
                dtpDateRecommandation.Enabled = False
                chkRecommandationActive.Enabled = False
                rtbCommentaire.ReadOnly = True

                ' Désactiver le toolbar en consultation
                ucRichTextToolbar.Enabled = False

                ' Réactiver les contrôles origine
                EnableOrigineControls(True)

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNewRecommandation.Enabled = False
                btnEditRecommandation.Enabled = False
                btnDeleteRecommandation.Enabled = False

                btnSaveRecommandation.Enabled = True
                btnCancelRecommandation.Enabled = True

                txtSourceNom.ReadOnly = False
                txtSourceLogin.ReadOnly = False
                txtSourceUrl.ReadOnly = False
                dtpDateRecommandation.Enabled = True
                chkRecommandationActive.Enabled = True
                rtbCommentaire.ReadOnly = False

                ' Activer le toolbar en édition
                ucRichTextToolbar.Enabled = True

                ' Désactiver les contrôles origine pendant édition recommandation
                EnableOrigineControls(False)

        End Select

    End Sub

    Private Sub EnableOrigineControls(enabled As Boolean)
        btnNewOrigine.Enabled = enabled
        btnEditOrigine.Enabled = enabled AndAlso dgvOrigines.CurrentRow IsNot Nothing
        btnDeleteOrigine.Enabled = enabled AndAlso dgvOrigines.CurrentRow IsNot Nothing
        btnSearchOrigines.Enabled = enabled
        btnClearSearchOrigines.Enabled = enabled
        chkOriginesActifs.Enabled = enabled
    End Sub

    Private Sub EnableRecommandationsControls(enabled As Boolean)
        btnNewRecommandation.Enabled = enabled AndAlso _currentIdOrigine > 0
        btnEditRecommandation.Enabled = enabled AndAlso dgvRecommandations.CurrentRow IsNot Nothing
        btnDeleteRecommandation.Enabled = enabled AndAlso dgvRecommandations.CurrentRow IsNot Nothing
        btnSearchRecommandations.Enabled = enabled
        btnClearSearchRecommandations.Enabled = enabled
        chkRecommandationsActifs.Enabled = enabled
        chkSearchNotes.Enabled = enabled
    End Sub

#End Region

#Region "Interface utilisateur"

    Private Sub SetStatus(message As String)
        _context?.SetStatus(message)
    End Sub

    Private Sub ClearErrors()
        _context?.ClearAllErrors()
    End Sub

#End Region

#Region "Chargement données - Origines"

    Private Sub LoadOrigines()

        Try
            Dim dt As DataTable = GestionReferentiel.OrigineRecommandation_GetAll()

            dgvOrigines.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvOrigines, {"id_origine_recommandation", "code_origine_recommandation", "created_at", "updated_at"})

            UtilsUCReferentiels.UpdateCountLabel(lblCountOrigines, dgvOrigines, "origine")

            If dgvOrigines.Rows.Count > 0 Then
                dgvOrigines.ClearSelection()
                dgvOrigines.Rows(0).Selected = True
                ' Définir CurrentCell sur une colonne visible pour éviter les problèmes
                If dgvOrigines.Columns.Contains("libelle_origine_recommandation") Then
                    dgvOrigines.CurrentCell = dgvOrigines.Rows(0).Cells("libelle_origine_recommandation")
                End If
                BindSelectedOrigineToDetails()
                ' Réactiver les boutons origine après binding
                SetModeOrigine(ModeEdition.Consultation)
                ' Réactiver les boutons recommandations après binding
                ' (LoadRecommandations a déjà été appelée par BindSelectedOrigineToDetails)
                SetModeRecommandation(ModeEdition.Consultation)
            Else
                ClearOrigineDetails()
                ClearRecommandationsArea()
                SetModeOrigine(ModeEdition.Consultation)
                SetModeRecommandation(ModeEdition.Consultation)
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur LoadOrigines.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur chargement origines.")
        End Try

    End Sub

    Private Sub BindSelectedOrigineToDetails()

        If dgvOrigines.CurrentRow Is Nothing Then
            ClearOrigineDetails()
            ClearRecommandationsArea()
            Return
        End If

        Dim row As DataGridViewRow = dgvOrigines.CurrentRow

        txtIdOrigine.Text = row.Cells("id_origine_recommandation").Value?.ToString()
        txtCodeOrigine.Text = row.Cells("code_origine_recommandation").Value?.ToString()

        txtLibelleOrigine.Text = row.Cells("libelle_origine_recommandation").Value?.ToString()
        nudOrdreOrigine.Value = UtilsUCReferentiels.DbToInt(row.Cells("ordre_affichage").Value)
        chkOrigineActif.Checked = UtilsUCReferentiels.DbToBool(row.Cells("is_actif").Value)

        _currentIdOrigine = UtilsUCReferentiels.SafeULong(txtIdOrigine.Text)

        ' Charger les recommandations liées
        LoadRecommandations()

        ' Réactiver les boutons recommandations après chargement
        SetModeRecommandation(ModeEdition.Consultation)

    End Sub

    Private Sub ClearOrigineDetails()
        txtIdOrigine.Clear()
        txtCodeOrigine.Clear()
        txtLibelleOrigine.Clear()
        nudOrdreOrigine.Value = 0
        chkOrigineActif.Checked = True
        _currentIdOrigine = 0
    End Sub

#End Region

#Region "Chargement données - Recommandations"

    Private Sub LoadRecommandations()

        If _currentIdOrigine = 0 Then
            ClearRecommandationsArea()
            Return
        End If

        Try
            Dim actifsOnly As Boolean = chkRecommandationsActifs.Checked
            Dim dt As DataTable = GestionReferentiel.Recommandation_GetByOrigine(_currentIdOrigine, actifsOnly)

            dgvRecommandations.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvRecommandations, {"id_recommandation", "code_recommandation", "id_origine_recommandation", "commentaire_rtf", "commentaire_txt", "created_at", "updated_at"})

            UtilsUCReferentiels.UpdateCountLabel(lblCountRecommandations, dgvRecommandations, "recommandation")

            If dgvRecommandations.Rows.Count > 0 Then
                dgvRecommandations.ClearSelection()
                dgvRecommandations.Rows(0).Selected = True
                ' Définir CurrentCell sur une colonne visible pour éviter les problèmes
                If dgvRecommandations.Columns.Contains("source_nom") Then
                    dgvRecommandations.CurrentCell = dgvRecommandations.Rows(0).Cells("source_nom")
                End If
                BindSelectedRecommandationToDetails()
            Else
                ClearRecommandationDetails()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur LoadRecommandations.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur chargement recommandations.")
        End Try

    End Sub

    Private Sub BindSelectedRecommandationToDetails()

        If dgvRecommandations.CurrentRow Is Nothing Then
            ClearRecommandationDetails()
            Return
        End If

        Dim row As DataGridViewRow = dgvRecommandations.CurrentRow

        txtIdRecommandation.Text = row.Cells("id_recommandation").Value?.ToString()
        txtCodeRecommandation.Text = row.Cells("code_recommandation").Value?.ToString()

        txtSourceNom.Text = row.Cells("source_nom").Value?.ToString()
        txtSourceLogin.Text = row.Cells("source_login").Value?.ToString()
        txtSourceUrl.Text = row.Cells("source_url").Value?.ToString()

        ' Date (peut être NULL)
        Dim dateVal = row.Cells("date_recommandation").Value
        If dateVal IsNot Nothing AndAlso dateVal IsNot DBNull.Value Then
            dtpDateRecommandation.Value = CType(dateVal, Date)
            dtpDateRecommandation.Checked = True
        Else
            dtpDateRecommandation.Checked = False
        End If

        chkRecommandationActive.Checked = UtilsUCReferentiels.DbToBool(row.Cells("is_actif").Value)

        ' Charger le RichTextBox (RTF prioritaire, fallback TXT)
        Dim rtfContent As String = row.Cells("commentaire_rtf").Value?.ToString()
        Dim txtContent As String = row.Cells("commentaire_txt").Value?.ToString()
        RichTextNotesHelper.ChargerContenuNotes(rtbCommentaire, rtfContent, txtContent)

        _currentIdRecommandation = UtilsUCReferentiels.SafeULong(txtIdRecommandation.Text)

    End Sub

    Private Sub ClearRecommandationDetails()
        txtIdRecommandation.Clear()
        txtCodeRecommandation.Clear()
        txtSourceNom.Clear()
        txtSourceLogin.Clear()
        txtSourceUrl.Clear()
        dtpDateRecommandation.Checked = False
        chkRecommandationActive.Checked = True
        rtbCommentaire.Clear()
        _currentIdRecommandation = 0
    End Sub

    Private Sub ClearRecommandationsArea()
        dgvRecommandations.DataSource = Nothing
        lblCountRecommandations.Text = "0 recommandation"
        ClearRecommandationDetails()
    End Sub

#End Region

#Region "Événements grilles"

    Private Sub dgvOrigines_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOrigines.SelectionChanged

        If _modeOrigine <> ModeEdition.Consultation Then Exit Sub

        Try
            BindSelectedOrigineToDetails()
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur dgvOrigines_SelectionChanged.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub dgvRecommandations_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRecommandations.SelectionChanged

        If _modeRecommandation <> ModeEdition.Consultation Then Exit Sub

        Try
            BindSelectedRecommandationToDetails()
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur dgvRecommandations_SelectionChanged.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

#End Region

#Region "Actions CRUD - Origine"

    Private Sub btnNewOrigine_Click(sender As Object, e As EventArgs) Handles btnNewOrigine.Click

        ClearErrors()
        ClearOrigineDetails()

        txtLibelleOrigine.Text = ""
        nudOrdreOrigine.Value = 0
        chkOrigineActif.Checked = True

        SetModeOrigine(ModeEdition.Nouveau)
        txtLibelleOrigine.Focus()

    End Sub

    Private Sub btnEditOrigine_Click(sender As Object, e As EventArgs) Handles btnEditOrigine.Click

        If dgvOrigines.CurrentRow Is Nothing Then Return

        ClearErrors()

        ' Snapshot pour Cancel
        _snapshotOrigine = New RefOrigineRecommandation With {
            .IdOrigineRecommandation = _currentIdOrigine,
            .LibelleOrigineRecommandation = txtLibelleOrigine.Text,
            .OrdreAffichage = CInt(nudOrdreOrigine.Value),
            .IsActif = chkOrigineActif.Checked
        }

        SetModeOrigine(ModeEdition.Modification)
        txtLibelleOrigine.Focus()

    End Sub

    Private Sub btnSaveOrigine_Click(sender As Object, e As EventArgs) Handles btnSaveOrigine.Click

        If Not ValidateOrigineForm() Then Return

        Try
            Dim o As New RefOrigineRecommandation With {
                .LibelleOrigineRecommandation = txtLibelleOrigine.Text.Trim(),
                .OrdreAffichage = CInt(nudOrdreOrigine.Value),
                .IsActif = chkOrigineActif.Checked
            }

            Dim wasNew As Boolean = (_modeOrigine = ModeEdition.Nouveau)

            If _modeOrigine = ModeEdition.Nouveau Then
                _currentIdOrigine = GestionReferentiel.OrigineRecommandation_Insert(o)
                SetStatus("Origine créée.")
            ElseIf _modeOrigine = ModeEdition.Modification Then
                o.IdOrigineRecommandation = _currentIdOrigine
                GestionReferentiel.OrigineRecommandation_Update(o)
                SetStatus("Origine modifiée.")
            End If

            ' Passer en consultation AVANT de recharger
            SetModeOrigine(ModeEdition.Consultation)

            ' Recharger la grid sans réinitialiser les modes
            Dim dt As DataTable = GestionReferentiel.OrigineRecommandation_GetAll()
            dgvOrigines.DataSource = dt
            UtilsUCReferentiels.HideTechnicalColumns(dgvOrigines, {"id_origine_recommandation", "code_origine_recommandation", "created_at", "updated_at"})
            UtilsUCReferentiels.UpdateCountLabel(lblCountOrigines, dgvOrigines, "origine")

            ' Resélectionner l'origine modifiée/créée
            For i As Integer = 0 To dgvOrigines.Rows.Count - 1
                If UtilsUCReferentiels.SafeULong(dgvOrigines.Rows(i).Cells("id_origine_recommandation").Value?.ToString()) = _currentIdOrigine Then
                    dgvOrigines.ClearSelection()
                    dgvOrigines.Rows(i).Selected = True
                    ' Pointer vers une colonne visible (libelle_origine_recommandation) au lieu de la colonne 0 cachée
                    If dgvOrigines.Columns.Contains("libelle_origine_recommandation") Then
                        dgvOrigines.CurrentCell = dgvOrigines.Rows(i).Cells("libelle_origine_recommandation")
                    End If

                    ' Bind les détails sans recharger les recommandations si c'était une nouvelle origine
                    Dim row As DataGridViewRow = dgvOrigines.CurrentRow
                    txtIdOrigine.Text = row.Cells("id_origine_recommandation").Value?.ToString()
                    txtCodeOrigine.Text = row.Cells("code_origine_recommandation").Value?.ToString()
                    txtLibelleOrigine.Text = row.Cells("libelle_origine_recommandation").Value?.ToString()
                    nudOrdreOrigine.Value = UtilsUCReferentiels.DbToInt(row.Cells("ordre_affichage").Value)
                    chkOrigineActif.Checked = UtilsUCReferentiels.DbToBool(row.Cells("is_actif").Value)
                    _currentIdOrigine = UtilsUCReferentiels.SafeULong(txtIdOrigine.Text)

                    ' Charger les recommandations
                    If wasNew Then
                        ClearRecommandationsArea() ' Nouvelle origine = pas encore de recommandations
                    Else
                        LoadRecommandations()
                    End If

                    ' Activer le bouton Nouveau Recommandation
                    SetModeRecommandation(ModeEdition.Consultation)

                    Exit For
                End If
            Next

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnSaveOrigine_Click.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors de l'enregistrement de l'origine." & vbCrLf & vbCrLf & "Détail : " & ex.Message,
                "Erreur",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

    Private Sub btnCancelOrigine_Click(sender As Object, e As EventArgs) Handles btnCancelOrigine.Click

        ClearErrors()

        If _modeOrigine = ModeEdition.Nouveau Then
            ClearOrigineDetails()
            If dgvOrigines.Rows.Count > 0 Then
                dgvOrigines.ClearSelection()
                dgvOrigines.Rows(0).Selected = True
                ' Définir CurrentCell sur une colonne visible pour éviter les problèmes
                If dgvOrigines.Columns.Contains("libelle_origine_recommandation") Then
                    dgvOrigines.CurrentCell = dgvOrigines.Rows(0).Cells("libelle_origine_recommandation")
                End If
                BindSelectedOrigineToDetails()
            End If
        ElseIf _modeOrigine = ModeEdition.Modification AndAlso _snapshotOrigine IsNot Nothing Then
            txtLibelleOrigine.Text = _snapshotOrigine.LibelleOrigineRecommandation
            nudOrdreOrigine.Value = _snapshotOrigine.OrdreAffichage
            chkOrigineActif.Checked = _snapshotOrigine.IsActif
        End If

        SetModeOrigine(ModeEdition.Consultation)
        SetStatus("Annulation.")

    End Sub

    Private Sub btnDeleteOrigine_Click(sender As Object, e As EventArgs) Handles btnDeleteOrigine.Click

        If dgvOrigines.CurrentRow Is Nothing Then Return

        ' Vérifier si des recommandations existent
        Dim countReco As Integer = GestionReferentiel.Recommandation_CountByOrigine(_currentIdOrigine)

        If countReco > 0 Then
            Dim result = MessageBox.Show(
                $"Cette origine a {countReco} recommandation(s) liée(s). Supprimer l'origine supprimera également toutes ses recommandations." & vbCrLf & vbCrLf & "Voulez-vous continuer ?",
                "Confirmation suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )

            If result <> DialogResult.Yes Then
                SetStatus("Suppression annulée.")
                Return
            End If
        Else
            Dim result = MessageBox.Show(
                "Supprimer cette origine ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If result <> DialogResult.Yes Then
                SetStatus("Suppression annulée.")
                Return
            End If
        End If

        Try
            ' Supprimer l'origine et ses recommandations liées en transaction atomique
            If countReco > 0 Then
                GestionReferentiel.OrigineRecommandation_DeleteWithRecommandations(_currentIdOrigine)
            Else
                GestionReferentiel.OrigineRecommandation_Delete(_currentIdOrigine)
            End If

            SetStatus("Origine supprimée.")

            ' Recharger la liste
            LoadOrigines()

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnDeleteOrigine_Click.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors de la suppression de l'origine." & vbCrLf & vbCrLf & "Détail : " & ex.Message,
                "Erreur",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

#End Region

#Region "Actions CRUD - Recommandation"

    Private Sub btnNewRecommandation_Click(sender As Object, e As EventArgs) Handles btnNewRecommandation.Click

        If _currentIdOrigine = 0 Then
            MessageBox.Show(
                "Veuillez sélectionner une origine de recommandation.",
                "Origine requise",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
            Return
        End If

        ClearErrors()
        ClearRecommandationDetails()

        txtSourceNom.Text = ""
        txtSourceLogin.Text = ""
        txtSourceUrl.Text = ""
        dtpDateRecommandation.Value = DateTime.Today
        dtpDateRecommandation.Checked = True
        chkRecommandationActive.Checked = True
        rtbCommentaire.Clear()

        SetModeRecommandation(ModeEdition.Nouveau)
        txtSourceNom.Focus()

    End Sub

    Private Sub btnEditRecommandation_Click(sender As Object, e As EventArgs) Handles btnEditRecommandation.Click

        If dgvRecommandations.CurrentRow Is Nothing Then Return

        ClearErrors()

        ' Snapshot pour Cancel
        _snapshotRecommandation = New Recommandation With {
            .IdRecommandation = _currentIdRecommandation,
            .IdOrigineRecommandation = _currentIdOrigine,
            .SourceNom = txtSourceNom.Text,
            .SourceLogin = txtSourceLogin.Text,
            .SourceURL = txtSourceUrl.Text,
            .DateRecommandation = If(dtpDateRecommandation.Checked, dtpDateRecommandation.Value, Nothing),
            .CommentaireRtf = RichTextNotesHelper.GetNotesRtf(rtbCommentaire),
            .CommentaireTxt = RichTextNotesHelper.GetNotesTxt(rtbCommentaire),
            .IsActif = chkRecommandationActive.Checked
        }

        SetModeRecommandation(ModeEdition.Modification)
        txtSourceNom.Focus()

    End Sub

    Private Sub btnSaveRecommandation_Click(sender As Object, e As EventArgs) Handles btnSaveRecommandation.Click

        If Not ValidateRecommandationForm() Then Return

        Try
            Dim r As New Recommandation With {
                .IdOrigineRecommandation = _currentIdOrigine,
                .SourceNom = txtSourceNom.Text.Trim(),
                .SourceLogin = txtSourceLogin.Text.Trim(),
                .SourceURL = txtSourceUrl.Text.Trim(),
                .DateRecommandation = If(dtpDateRecommandation.Checked, dtpDateRecommandation.Value, Nothing),
                .CommentaireRtf = RichTextNotesHelper.GetNotesRtf(rtbCommentaire),
                .CommentaireTxt = RichTextNotesHelper.GetNotesTxt(rtbCommentaire),
                .IsActif = chkRecommandationActive.Checked
            }

            If _modeRecommandation = ModeEdition.Nouveau Then
                _currentIdRecommandation = GestionReferentiel.Recommandation_Insert(r)
                SetStatus("Recommandation créée.")
            ElseIf _modeRecommandation = ModeEdition.Modification Then
                r.IdRecommandation = _currentIdRecommandation
                GestionReferentiel.Recommandation_Update(r)
                SetStatus("Recommandation modifiée.")
            End If

            ' Passer en consultation AVANT de recharger
            SetModeRecommandation(ModeEdition.Consultation)

            ' Recharger la grid des recommandations
            LoadRecommandations()

            ' Resélectionner la recommandation modifiée/créée
            For i As Integer = 0 To dgvRecommandations.Rows.Count - 1
                If UtilsUCReferentiels.SafeULong(dgvRecommandations.Rows(i).Cells("id_recommandation").Value?.ToString()) = _currentIdRecommandation Then
                    dgvRecommandations.ClearSelection()
                    dgvRecommandations.Rows(i).Selected = True
                    ' Pointer vers une colonne visible (source_nom) au lieu de la colonne 0 cachée
                    If dgvRecommandations.Columns.Contains("source_nom") Then
                        dgvRecommandations.CurrentCell = dgvRecommandations.Rows(i).Cells("source_nom")
                    End If
                    BindSelectedRecommandationToDetails()
                    Exit For
                End If
            Next

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnSaveRecommandation_Click.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors de l'enregistrement de la recommandation." & vbCrLf & vbCrLf & "Détail : " & ex.Message,
                "Erreur",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

    Private Sub btnCancelRecommandation_Click(sender As Object, e As EventArgs) Handles btnCancelRecommandation.Click

        ClearErrors()

        If _modeRecommandation = ModeEdition.Nouveau Then
            ClearRecommandationDetails()
            If dgvRecommandations.Rows.Count > 0 Then
                dgvRecommandations.ClearSelection()
                dgvRecommandations.Rows(0).Selected = True
                ' Définir CurrentCell sur une colonne visible pour éviter les problèmes
                If dgvRecommandations.Columns.Contains("source_nom") Then
                    dgvRecommandations.CurrentCell = dgvRecommandations.Rows(0).Cells("source_nom")
                End If
                BindSelectedRecommandationToDetails()
            End If
        ElseIf _modeRecommandation = ModeEdition.Modification AndAlso _snapshotRecommandation IsNot Nothing Then
            txtSourceNom.Text = _snapshotRecommandation.SourceNom
            txtSourceLogin.Text = _snapshotRecommandation.SourceLogin
            txtSourceUrl.Text = _snapshotRecommandation.SourceURL
            If _snapshotRecommandation.DateRecommandation.HasValue Then
                dtpDateRecommandation.Value = _snapshotRecommandation.DateRecommandation.Value
                dtpDateRecommandation.Checked = True
            Else
                dtpDateRecommandation.Checked = False
            End If
            RichTextNotesHelper.ChargerContenuNotes(rtbCommentaire, _snapshotRecommandation.CommentaireRtf, _snapshotRecommandation.CommentaireTxt)
            chkRecommandationActive.Checked = _snapshotRecommandation.IsActif
        End If

        SetModeRecommandation(ModeEdition.Consultation)
        SetStatus("Annulation.")

    End Sub

    Private Sub btnDeleteRecommandation_Click(sender As Object, e As EventArgs) Handles btnDeleteRecommandation.Click

        If dgvRecommandations.CurrentRow Is Nothing Then Return

        Dim result = MessageBox.Show(
            "Supprimer cette recommandation ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If result <> DialogResult.Yes Then
            SetStatus("Suppression annulée.")
            Return
        End If

        Try
            GestionReferentiel.Recommandation_Delete(_currentIdRecommandation)

            SetStatus("Recommandation supprimée.")

            ' Recharger la liste des recommandations
            LoadRecommandations()

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnDeleteRecommandation_Click.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors de la suppression de la recommandation.",
                "Erreur",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

#End Region

#Region "Recherche"

    Private Sub btnSearchOrigines_Click(sender As Object, e As EventArgs) Handles btnSearchOrigines.Click
        ApplySearchOrigines()
    End Sub

    Private Sub btnClearSearchOrigines_Click(sender As Object, e As EventArgs) Handles btnClearSearchOrigines.Click
        txtSearchOrigines.Clear()
        LoadOrigines()
        SetStatus("Filtre effacé.")
    End Sub

    Private Sub txtSearchOrigines_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchOrigines.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplySearchOrigines()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub ApplySearchOrigines()

        Dim search As String = txtSearchOrigines.Text.Trim()

        If String.IsNullOrWhiteSpace(search) Then
            LoadOrigines()
            Return
        End If

        Try
            Dim dt As DataTable = GestionReferentiel.OrigineRecommandation_GetBySearch(search)
            dgvOrigines.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvOrigines, {"id_origine_recommandation", "code_origine_recommandation", "created_at", "updated_at"})
            UtilsUCReferentiels.UpdateCountLabel(lblCountOrigines, dgvOrigines, "origine")

            If dgvOrigines.Rows.Count > 0 Then
                dgvOrigines.ClearSelection()
                dgvOrigines.Rows(0).Selected = True
                ' Définir CurrentCell sur une colonne visible pour éviter les problèmes
                If dgvOrigines.Columns.Contains("libelle_origine_recommandation") Then
                    dgvOrigines.CurrentCell = dgvOrigines.Rows(0).Cells("libelle_origine_recommandation")
                End If
                BindSelectedOrigineToDetails()
                ' Réactiver les boutons après binding
                SetModeOrigine(ModeEdition.Consultation)
                SetModeRecommandation(ModeEdition.Consultation)
            Else
                ClearOrigineDetails()
                ClearRecommandationsArea()
                SetModeOrigine(ModeEdition.Consultation)
                SetModeRecommandation(ModeEdition.Consultation)
            End If

            SetStatus($"Recherche : {dgvOrigines.Rows.Count} origine(s) trouvée(s).")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur ApplySearchOrigines.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    Private Sub btnSearchRecommandations_Click(sender As Object, e As EventArgs) Handles btnSearchRecommandations.Click
        ApplySearchRecommandations()
    End Sub

    Private Sub btnClearSearchRecommandations_Click(sender As Object, e As EventArgs) Handles btnClearSearchRecommandations.Click
        txtSearchRecommandations.Clear()
        LoadRecommandations()
        SetStatus("Filtre recommandations effacé.")
    End Sub

    Private Sub txtSearchRecommandations_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchRecommandations.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplySearchRecommandations()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub chkRecommandationsActifs_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecommandationsActifs.CheckedChanged
        LoadRecommandations()
    End Sub

    Private Sub ApplySearchRecommandations()

        If _currentIdOrigine = 0 Then
            SetStatus("Aucune origine sélectionnée.")
            Return
        End If

        Dim search As String = txtSearchRecommandations.Text.Trim()

        If String.IsNullOrWhiteSpace(search) Then
            LoadRecommandations()
            Return
        End If

        Try
            Dim actifsOnly As Boolean = chkRecommandationsActifs.Checked
            Dim includeNotes As Boolean = chkSearchNotes.Checked

            Dim dt As DataTable = GestionReferentiel.Recommandation_GetByOrigineAndSearch(_currentIdOrigine, search, actifsOnly, includeNotes)
            dgvRecommandations.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvRecommandations, {"id_recommandation", "code_recommandation", "id_origine_recommandation", "commentaire_rtf", "commentaire_txt", "created_at", "updated_at"})
            UtilsUCReferentiels.UpdateCountLabel(lblCountRecommandations, dgvRecommandations, "recommandation")

            If dgvRecommandations.Rows.Count > 0 Then
                dgvRecommandations.ClearSelection()
                dgvRecommandations.Rows(0).Selected = True
                ' Définir CurrentCell sur une colonne visible pour éviter les problèmes
                If dgvRecommandations.Columns.Contains("source_nom") Then
                    dgvRecommandations.CurrentCell = dgvRecommandations.Rows(0).Cells("source_nom")
                End If
                BindSelectedRecommandationToDetails()
            Else
                ClearRecommandationDetails()
            End If

            SetStatus($"Recherche recommandations : {dgvRecommandations.Rows.Count} recommandation(s) trouvée(s).")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur ApplySearchRecommandations.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la recherche recommandations.")
        End Try

    End Sub

#End Region

#Region "Validation"

    Private Function ValidateOrigineForm() As Boolean

        ClearErrors()

        ' ✅ Utilisation du helper pour valider les champs obligatoires
        Dim isValid As Boolean = UtilsUCReferentiels.ValidateRequiredField(
            txtLibelleOrigine,
            "Libellé origine",
            _context?.ErrorProvider
        )

        If Not isValid Then
            SetStatus("Libellé origine manquant.")
        End If

        Return isValid

    End Function

    Private Function ValidateRecommandationForm() As Boolean

        ClearErrors()

        If _context?.ErrorProvider Is Nothing Then Return True

        Dim ep = _context.ErrorProvider

        ' Au moins source nom ou URL
        If String.IsNullOrWhiteSpace(txtSourceNom.Text) AndAlso String.IsNullOrWhiteSpace(txtSourceUrl.Text) Then
            ep.SetError(txtSourceNom, "Au moins un nom de source ou une URL est requis.")
            SetStatus("Source nom ou URL manquant.")
            Return False
        End If

        Return True

    End Function

#End Region

End Class
