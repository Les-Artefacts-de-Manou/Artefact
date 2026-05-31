'====================================================================
' 📌 UC_Langues.vb
' Version : V1.0
' Date    : 23/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion du référentiel Langues.
' Transposé depuis GestionLangues.vb
'
' Règles :
' - StatusStrip, ErrorProvider, ToolTip fournis par le conteneur
' - Logique métier identique à la Form d'origine
'
' Évolution :
' - V1.0 : Transposition depuis GestionLangues.vb
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_Langues
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'utilisation
    Private _mode As ModeEdition = ModeEdition.Consultation

    ' Sauvegarde des valeurs affichées (pour Annuler en mode Modification)
    Private _snapshot As Langue = Nothing

    ' Identifiant de la ligne courante
    Private _currentId As ULong = 0

    ' Contexte partagé (remplace les injections séparées)
    Private _context As UserControlContext = Nothing

    ' Composants fournis par le conteneur (OBSOLETE - utiliser _context à la place)
    Private _stsStatus As StatusStrip
    Private _errProvider As ErrorProvider
    Private _ttMain As ToolTip

#End Region

#Region "Constructeur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' New
    '
    '   • Constructeur par défaut (requis pour NavigationManager)
    '------------------------------------------------------------
    Public Sub New()
        InitializeComponent()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' New (OBSOLETE - gardé pour compatibilité temporaire)
    '
    '   • Constructeur avec injection des composants mutualisés
    '------------------------------------------------------------
    Public Sub New(statusStrip As StatusStrip, errorProvider As ErrorProvider, toolTip As ToolTip)

        InitializeComponent()

        _stsStatus = statusStrip
        _errProvider = errorProvider
        _ttMain = toolTip

    End Sub

#End Region

#Region "IContextAwareUserControl Implementation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SetContext (IContextAwareUserControl)
    '
    '   • Injecte le contexte partagé dans ce UserControl
    '   • Appelé par NavigationManager lors du chargement
    '------------------------------------------------------------
    Public Sub SetContext(context As UserControlContext) Implements IContextAwareUserControl.SetContext
        _context = context

        ' Réassigner les composants pour compatibilité avec le code existant
        _stsStatus = context.StatusStrip
        _errProvider = context.ErrorProvider
        _ttMain = context.ToolTip
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' OnActivated (IContextAwareUserControl)
    '
    '   • Appelé quand l'UC devient actif
    '   • Rafraîchit les données et met à jour le contexte
    '------------------------------------------------------------
    Public Sub OnActivated() Implements IContextAwareUserControl.OnActivated
        Try
            ' Rafraîchir les données
            LoadGrid()

            ' Mettre à jour le fil d'Ariane
            If _context IsNot Nothing Then
                _context.NavigateToLevel("Langues")
                _context.SetStatus($"Langues chargées : {dgvLangues.Rows.Count} élément(s).")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_Langues.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de l'activation.")
        End Try
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' OnDeactivated (IContextAwareUserControl)
    '
    '   • Appelé quand l'UC est désactivé
    '   • Nettoie les ressources temporaires
    '------------------------------------------------------------
    Public Sub OnDeactivated() Implements IContextAwareUserControl.OnDeactivated
        Try
            ' Annuler toute édition en cours
            If _mode <> ModeEdition.Consultation Then
                btnCancel.PerformClick()
            End If

            ' Effacer les erreurs
            If _context IsNot Nothing Then
                _context.ClearAllErrors()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur désactivation UC_Langues.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' UC_Langues_Load
    '
    '   • Initialise le UserControl au chargement
    '------------------------------------------------------------
    Private Sub UC_Langues_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()

            LoadGrid()
            If dgvLangues.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetStatus("Langues chargées.")
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement UC_Langues.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors du chargement des langues." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' InitToolTips
    '
    '   • Configure les info-bulles pour tous les contrôles
    '------------------------------------------------------------
    Private Sub InitToolTips()

        If _ttMain Is Nothing Then Return

        _ttMain.SetToolTip(btnSearch, "Appliquer le filtre")
        _ttMain.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        _ttMain.SetToolTip(btnNew, "Créer une nouvelle langue")
        _ttMain.SetToolTip(btnEdit, "Passer en mode modification")
        _ttMain.SetToolTip(btnSave, "Enregistrer les modifications")
        _ttMain.SetToolTip(btnCancel, "Annuler les modifications")
        _ttMain.SetToolTip(btnDelete, "Supprimer la langue sélectionnée")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SetMode
    '
    '   • Configure l'UI selon le mode actuel (Consultation/Nouveau/Modification)
    '------------------------------------------------------------
    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Select Case mode

            Case ModeEdition.Consultation
                lblMode.Text = "Consultation"
                lblMode.ForeColor = System.Drawing.Color.Gray

                ' Champs lecture seule
                txtNomLangue.ReadOnly = True
                txtAbrevLangue.ReadOnly = True
                txtIso639_1.ReadOnly = True
                txtIso639_2.ReadOnly = True

                ' Boutons
                btnNew.Enabled = True
                btnEdit.Enabled = HasSelectedLangue()
                btnSave.Enabled = False
                btnCancel.Enabled = False
                btnDelete.Enabled = HasSelectedLangue()

                ' Recherche active
                txtSearch.Enabled = True
                btnSearch.Enabled = True
                btnClearSearch.Enabled = True

                ' Grille active
                dgvLangues.Enabled = True

            Case ModeEdition.Nouveau
                lblMode.Text = "Nouveau"
                lblMode.ForeColor = System.Drawing.Color.Green

                ' Champs éditables
                txtNomLangue.ReadOnly = False
                txtAbrevLangue.ReadOnly = False
                txtIso639_1.ReadOnly = False
                txtIso639_2.ReadOnly = False

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
                dgvLangues.Enabled = False

            Case ModeEdition.Modification
                lblMode.Text = "Modification"
                lblMode.ForeColor = System.Drawing.Color.Orange

                ' Champs éditables
                txtNomLangue.ReadOnly = False
                txtAbrevLangue.ReadOnly = False
                txtIso639_1.ReadOnly = False
                txtIso639_2.ReadOnly = False

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
                dgvLangues.Enabled = False

        End Select

        ' Mettre à jour le fil d'Ariane avec le mode
        If _context IsNot Nothing Then
            Select Case mode
                Case ModeEdition.Consultation
                    _context.NavigateToLevel("Langues")
                Case ModeEdition.Nouveau
                    _context.SetMode("Nouveau")
                Case ModeEdition.Modification
                    _context.SetMode("Modification")
            End Select
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' HasSelectedLangue
    '
    '   • Retourne True si une langue est actuellement sélectionnée
    '------------------------------------------------------------
    Private Function HasSelectedLangue() As Boolean
        Return dgvLangues.CurrentRow IsNot Nothing
    End Function

#End Region

#Region "Utilitaires UI"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SetStatus
    '
    '   • Met à jour le StatusStrip du conteneur via le contexte
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)
        ' Utiliser le contexte s'il est disponible
        If _context IsNot Nothing Then
            _context.SetStatus(message)
        ElseIf _stsStatus IsNot Nothing AndAlso _stsStatus.Items.Count > 0 Then
            ' Fallback pour compatibilité temporaire
            _stsStatus.Items(0).Text = message
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearErrors
    '
    '   • Efface tous les indicateurs d'erreur visuels via le contexte
    '------------------------------------------------------------
    Private Sub ClearErrors()
        ' Utiliser le contexte s'il est disponible
        If _context IsNot Nothing Then
            _context.ClearAllErrors()
        ElseIf _errProvider IsNot Nothing Then
            ' Fallback pour compatibilité temporaire
            _errProvider.Clear()
        End If
    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnNew_Click
    '
    '   • Crée une nouvelle langue
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearErrors()
        _snapshot = Nothing

        ClearDetails()
        SetMode(ModeEdition.Nouveau)

        txtNomLangue.Focus()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnEdit_Click
    '
    '   • Édite la langue sélectionnée
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ClearErrors()

        If Not HasSelectedLangue() Then
            SetStatus("Aucune langue sélectionnée.")
            Return
        End If

        ' Snapshot avant modification
        SnapshotFromFields()

        SetMode(ModeEdition.Modification)
        txtNomLangue.Focus()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnSave_Click
    '
    '   • Enregistre la langue (création ou modification)
    '------------------------------------------------------------
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
                SetStatus("Langue créée avec succès.")
            ElseIf _mode = ModeEdition.Modification Then
                GestionReferentiel.Langues_Update(langue)
                newId = langue.IdLangue
                SetStatus("Langue modifiée avec succès.")
            End If

            ' Recharge grille
            LoadGrid()

            ' Repositionnement sur la ligne concernée
            UtilsForm.DgvSelectRowById(dgvLangues, "id_langue", newId)
            BindSelectedToDetails()

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnSave_Click (UC_Langues).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'enregistrement.")
        End Try
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnCancel_Click
    '
    '   • Annule les modifications en cours
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnDelete_Click
    '
    '   • Supprime la langue sélectionnée
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If _mode <> ModeEdition.Consultation Then
            SetStatus("Suppression impossible pendant une édition.")
            Return
        End If

        If Not HasSelectedLangue() Then
            SetStatus("Aucune langue sélectionnée.")
            Return
        End If

        ClearErrors()

        Dim current As Langue = GetCurrentLangueFromFields()

        If current.IdLangue = 0UL Then
            SetStatus("Identifiant langue invalide.")
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
            SetStatus("Langue supprimée.")
            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnDelete_Click (UC_Langues).",
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
    ' 📌 V1.0 - 23/03/2026
    ' LoadGrid
    '
    '   • Charge et affiche la liste complète des langues
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try
            Dim dt As DataTable = GestionReferentiel.Langues_GetAll()

            dgvLangues.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvLangues)

            ' Colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvLangues, "id_langue", "code_langue", "created_at", "updated_at")

            ' Mise à jour compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvLangues, "langue")

            ' Sélection automatique première ligne si existante
            If UtilsForm.SelectFirstRow(dgvLangues, "nom_langue") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid (UC_Langues).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement des langues.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BindSelectedToDetails
    '
    '   • Transfère les données de la ligne sélectionnée vers les champs détails
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearDetails
    '
    '   • Efface tous les champs du panneau détails
    '------------------------------------------------------------
    Private Sub ClearDetails()
        txtIdLangue.Clear()
        txtNomLangue.Clear()
        txtAbrevLangue.Clear()
        txtIso639_1.Clear()
        txtIso639_2.Clear()
        txtCodeLangue.Clear()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' dgvLangues_SelectionChanged
    '
    '   • Événement déclenché lors du changement de sélection dans la grille
    '------------------------------------------------------------
    Private Sub dgvLangues_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLangues.SelectionChanged
        If _mode <> ModeEdition.Consultation Then Return
        BindSelectedToDetails()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SnapshotFromFields
    '
    '   • Crée une copie temporaire des valeurs actuelles des champs
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' RestoreSnapshotToFields
    '
    '   • Restaure les champs à partir du snapshot sauvegardé
    '------------------------------------------------------------
    Private Sub RestoreSnapshotToFields()
        If _snapshot Is Nothing Then Return

        txtIdLangue.Text = _snapshot.IdLangue.ToString()
        txtNomLangue.Text = _snapshot.NomLangue
        txtAbrevLangue.Text = _snapshot.AbrevLangue
        txtIso639_1.Text = _snapshot.Iso639_1
        txtIso639_2.Text = _snapshot.Iso639_2
        txtCodeLangue.Text = _snapshot.CodeLangue
    End Sub

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ValidateForm
    '
    '   • Valide tous les champs avant enregistrement
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtNomLangue.Text) Then
            _errProvider.SetError(txtNomLangue, "Le nom est obligatoire.")
            SetStatus("Nom obligatoire.")
            txtNomLangue.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtAbrevLangue.Text) Then
            _errProvider.SetError(txtAbrevLangue, "L'abréviation est obligatoire.")
            SetStatus("Abréviation obligatoire.")
            txtAbrevLangue.Focus()
            Return False
        End If

        If txtIso639_1.Text.Length > 2 Then
            _errProvider.SetError(txtIso639_1, "ISO 639-1 doit contenir 2 caractères.")
            SetStatus("ISO 639-1 invalide.")
            txtIso639_1.Focus()
            Return False
        End If

        If txtIso639_2.Text.Length > 3 Then
            _errProvider.SetError(txtIso639_2, "ISO 639-2 doit contenir 3 caractères.")
            SetStatus("ISO 639-2 invalide.")
            txtIso639_2.Focus()
            Return False
        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' GetCurrentLangueFromFields
    '
    '   • Construit un objet Langue à partir des valeurs actuelles des champs
    '------------------------------------------------------------
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

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnSearch_Click
    '
    '   • Lance la recherche selon le texte saisi
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplySearch()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnClearSearch_Click
    '
    '   • Efface le texte de recherche et réaffiche la liste complète
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Clear()
        ApplySearch()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ApplySearch
    '
    '   • Applique le filtre de recherche et recharge la grille
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
                dt = GestionReferentiel.Langues_GetAll()
                SetStatus("Liste complète affichée.")
            Else
                dt = GestionReferentiel.Langues_GetBySearch(searchText)
                SetStatus($"Filtre appliqué : '{searchText}'")
            End If

            dgvLangues.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvLangues)

            UtilsUCReferentiels.HideTechnicalColumns(dgvLangues, "id_langue", "code_langue", "created_at", "updated_at")

            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvLangues, "langue")

            If UtilsForm.SelectFirstRow(dgvLangues, "nom_langue") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch (UC_Langues).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' txtSearch_KeyDown
    '
    '   • Détecte la touche Entrée pour lancer la recherche
    '------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplySearch()
            e.SuppressKeyPress = True
        End If
    End Sub

#End Region

#Region "Utilitaires (remplacés par UtilsUCReferentiels)"

    ' Les méthodes SafeULong ont été déplacées vers UtilsUCReferentiels
    ' pour factorisation entre tous les UC_*

    Private Function SafeULong(value As String) As ULong
        Return UtilsUCReferentiels.SafeULong(value)
    End Function

#End Region

End Class
