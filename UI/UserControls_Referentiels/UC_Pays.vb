'====================================================================
' 📌 UC_Pays.vb
' Version : V1.0
' Date    : 23/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion du référentiel Pays.
' Transposé depuis GestionPays.vb
'
' Règles :
' - Implémente IContextAwareUserControl
' - Utilise UserControlContext pour StatusStrip, ErrorProvider, ToolTip
' - Logique métier identique à la Form d'origine
'
' Évolution :
' - V1.0 : Transposition depuis GestionPays.vb
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_Pays
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'utilisation
    Private _mode As ModeEdition = ModeEdition.Consultation

    ' Sauvegarde des valeurs affichées (pour Annuler en mode Modification)
    Private _snapshot As Pays = Nothing

    ' Identifiant de la ligne courante
    Private _currentId As ULong = 0

    ' Contexte partagé (remplace les injections séparées)
    Private _context As UserControlContext = Nothing

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
                _context.NavigateToLevel("Pays")
                _context.SetStatus($"Pays chargés : {dgvPays.Rows.Count} élément(s).")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_Pays.",
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
                "UI: erreur désactivation UC_Pays.",
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
    ' UC_Pays_Load
    '
    '   • Initialise le UserControl au chargement
    '------------------------------------------------------------
    Private Sub UC_Pays_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()

            LoadGrid()
            If dgvPays.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetStatus("Pays chargés.")
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement UC_Pays.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors du chargement des pays." & Environment.NewLine & ex.Message,
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
    '   • Configure les info-bulles pour tous les boutons et contrôles
    '------------------------------------------------------------
    Private Sub InitToolTips()

        If _context?.ToolTip Is Nothing Then Return

        _context.ToolTip.SetToolTip(btnSearch, "Appliquer le filtre")
        _context.ToolTip.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        _context.ToolTip.SetToolTip(btnNew, "Créer un nouveau pays")
        _context.ToolTip.SetToolTip(btnEdit, "Passer en mode modification")
        _context.ToolTip.SetToolTip(btnSave, "Enregistrer les modifications")
        _context.ToolTip.SetToolTip(btnCancel, "Annuler les modifications")
        _context.ToolTip.SetToolTip(btnDelete, "Supprimer le pays sélectionné")

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

        Dim isEdit As Boolean = (mode = ModeEdition.Nouveau OrElse mode = ModeEdition.Modification)
        Dim hasSelection As Boolean = HasSelectedPays()

        ' Grille / recherche verrouillées pendant édition
        dgvPays.Enabled = Not isEdit
        pnlTop.Enabled = Not isEdit

        ' Boutons
        btnNew.Enabled = Not isEdit
        btnEdit.Enabled = (Not isEdit) AndAlso hasSelection
        btnDelete.Enabled = (Not isEdit) AndAlso hasSelection

        btnSave.Enabled = isEdit
        btnCancel.Enabled = isEdit

        ' Champs
        SetDetailsEnabled(isEdit)

        ' Mettre à jour le fil d'Ariane avec le mode
        If _context IsNot Nothing Then
            Select Case mode
                Case ModeEdition.Consultation
                    _context.NavigateToLevel("Pays")
                Case ModeEdition.Nouveau
                    _context.SetMode("Nouveau")
                Case ModeEdition.Modification
                    _context.SetMode("Modification")
            End Select
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SetDetailsEnabled
    '
    '   • Active/désactive les champs de saisie du panneau détails
    '   • Note : code_pays reste toujours en lecture seule (généré automatiquement)
    '------------------------------------------------------------
    Private Sub SetDetailsEnabled(enabled As Boolean)

        txtNomPays.Enabled = enabled
        txtIso2.Enabled = enabled
        txtIso3.Enabled = enabled

        ' Champ généré
        txtCodePays.ReadOnly = True
        txtCodePays.TabStop = False

        ' Champ technique caché
        txtIdPays.Visible = False
        txtIdPays.TabStop = False

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' HasSelectedPays
    '
    '   • Retourne True si un pays est actuellement sélectionné
    '------------------------------------------------------------
    Private Function HasSelectedPays() As Boolean
        Return dgvPays.CurrentRow IsNot Nothing
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
        If _context IsNot Nothing Then
            _context.SetStatus(message)
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearErrors
    '
    '   • Efface tous les indicateurs d'erreur visuels via le contexte
    '------------------------------------------------------------
    Private Sub ClearErrors()
        If _context IsNot Nothing Then
            _context.ClearAllErrors()
        End If
    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnNew_Click
    '
    '   • Crée un nouveau pays
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearErrors()
        _snapshot = Nothing

        ClearDetails()
        SetMode(ModeEdition.Nouveau)

        txtNomPays.Focus()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnEdit_Click
    '
    '   • Modifie un pays existant
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        ClearErrors()

        If Not HasSelectedPays() Then
            SetStatus("Aucun pays sélectionné.")
            Return
        End If

        SnapshotFromFields()
        SetMode(ModeEdition.Modification)
        txtNomPays.Focus()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnSave_Click
    '
    '   • Enregistre le pays (création ou modification)
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Not ValidateForm() Then Exit Sub

        Try
            Dim p As New Pays With {
                .IdPays = SafeULong(txtIdPays.Text),
                .NomPays = txtNomPays.Text.Trim(),
                .Iso2 = txtIso2.Text.Trim(),
                .Iso3 = txtIso3.Text.Trim()
            }

            Dim targetId As ULong = 0UL

            If _mode = ModeEdition.Nouveau Then
                targetId = GestionReferentiel.Pays_Insert(p)
                SetStatus("Pays créé avec succès.")
            ElseIf _mode = ModeEdition.Modification Then
                GestionReferentiel.Pays_Update(p)
                targetId = p.IdPays
                SetStatus("Pays modifié avec succès.")
            End If

            LoadGrid()
            UtilsForm.DgvSelectRowById(dgvPays, "id_pays", targetId)
            BindSelectedToDetails()

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnSave_Click (UC_Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
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
    '   • Supprime le pays sélectionné
    '   • Vérifie les dépendances (auteurs, éditeurs, auteurs_pays)
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If _mode <> ModeEdition.Consultation Then
            SetStatus("Suppression impossible pendant une édition.")
            Return
        End If

        If Not HasSelectedPays() Then
            SetStatus("Aucun pays sélectionné.")
            Return
        End If

        Dim id As ULong = SafeULong(txtIdPays.Text)
        Dim current As Pays = GetCurrentPaysFromFields()

        Dim nbAuteurs As Integer = GestionReferentiel.Pays_CountUsageInAuteurs(id)
        Dim nbAuteursPays As Integer = GestionReferentiel.Pays_CountUsageInAuteursPays(id)
        Dim nbEditeurs As Integer = GestionReferentiel.Pays_CountUsageInEditeurs(id)

        ' Cas bloquant : FK RESTRICT
        If nbAuteursPays > 0 Then

            Dim msgBlocage As String =
            $"Suppression impossible : le pays '{current.NomPays}' ({current.Iso2}) est encore utilisé dans les relations auteurs/pays." &
            Environment.NewLine & Environment.NewLine &
            $"- Auteurs/Pays : {nbAuteursPays}" &
            Environment.NewLine & Environment.NewLine &
            "Supprimez ou modifiez d'abord ces relations avant de supprimer ce pays."

            MessageBox.Show(
                msgBlocage,
                "Suppression impossible",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            SetStatus("Suppression impossible : pays utilisé dans auteurs_pays.")
            Return

        End If

        Dim msg As String

        If nbAuteurs = 0 AndAlso nbEditeurs = 0 Then

            msg = $"Supprimer le pays '{current.NomPays}' ({current.Iso2}) ?"

        Else

            msg =
            $"Attention : le pays '{current.NomPays}' ({current.Iso2}) est encore utilisé." & Environment.NewLine & Environment.NewLine &
            $"- Auteurs  : {nbAuteurs}" & Environment.NewLine &
            $"- Éditeurs : {nbEditeurs}" & Environment.NewLine & Environment.NewLine &
            "Si vous le supprimez, ces références seront vidées automatiquement (mise à NULL)." & Environment.NewLine & Environment.NewLine &
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
            GestionReferentiel.Pays_Delete(id)

            SetStatus("Pays supprimé.")

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnDelete_Click (UC_Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la suppression.")
        End Try

    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' LoadGrid
    '
    '   • Charge et affiche la liste complète des pays
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try
            Dim dt = GestionReferentiel.Pays_GetAll()
            dgvPays.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvPays)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvPays, "id_pays", "code_pays", "date_creation", "date_modification")

            ' Largeurs colonnes
            If dgvPays.Columns.Contains("nom_pays") Then dgvPays.Columns("nom_pays").Width = 200
            If dgvPays.Columns.Contains("iso2") Then dgvPays.Columns("iso2").Width = 80
            If dgvPays.Columns.Contains("iso3") Then dgvPays.Columns("iso3").Width = 80

            ' Sélection première ligne
            If dgvPays.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvPays, "nom_pays")
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvPays, "pays")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid (UC_Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement de la grille.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BindSelectedToDetails
    '
    '   • Affiche les détails du pays sélectionné dans la grille
    '------------------------------------------------------------
    Private Sub BindSelectedToDetails()

        Try
            If dgvPays.CurrentRow Is Nothing Then
                ClearDetails()
                Return
            End If

            Dim row = dgvPays.CurrentRow

            txtIdPays.Text = If(row.Cells("id_pays")?.Value?.ToString(), "")
            txtCodePays.Text = If(row.Cells("code_pays")?.Value?.ToString(), "")
            txtNomPays.Text = If(row.Cells("nom_pays")?.Value?.ToString(), "")
            txtIso2.Text = If(row.Cells("iso2")?.Value?.ToString(), "")
            txtIso3.Text = If(row.Cells("iso3")?.Value?.ToString(), "")

            _currentId = SafeULong(txtIdPays.Text)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur BindSelectedToDetails (UC_Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearDetails
    '
    '   • Vide tous les champs de détails
    '------------------------------------------------------------
    Private Sub ClearDetails()

        txtIdPays.Text = ""
        txtCodePays.Text = ""
        txtNomPays.Text = ""
        txtIso2.Text = ""
        txtIso3.Text = ""

        _currentId = 0

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' dgvPays_SelectionChanged
    '
    '   • Met à jour les détails lors du changement de sélection
    '------------------------------------------------------------
    Private Sub dgvPays_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPays.SelectionChanged
        If _mode = ModeEdition.Consultation Then
            BindSelectedToDetails()
            SetMode(ModeEdition.Consultation) ' Refresh buttons
        End If
    End Sub

#End Region

#Region "Snapshot / Restauration"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SnapshotFromFields
    '
    '   • Crée une copie des valeurs actuelles pour annulation
    '------------------------------------------------------------
    Private Sub SnapshotFromFields()

        _snapshot = New Pays With {
            .IdPays = SafeULong(txtIdPays.Text),
            .NomPays = txtNomPays.Text.Trim(),
            .Iso2 = txtIso2.Text.Trim(),
            .Iso3 = txtIso3.Text.Trim()
        }

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' RestoreSnapshotToFields
    '
    '   • Restaure les valeurs depuis le snapshot
    '------------------------------------------------------------
    Private Sub RestoreSnapshotToFields()

        If _snapshot Is Nothing Then Return

        txtIdPays.Text = _snapshot.IdPays.ToString()
        txtNomPays.Text = _snapshot.NomPays
        txtIso2.Text = _snapshot.Iso2
        txtIso3.Text = _snapshot.Iso3

    End Sub

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ValidateForm
    '
    '   • Valide les champs avant enregistrement
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        ClearErrors()

        Dim isValid As Boolean = True

        ' Nom pays obligatoire
        If String.IsNullOrWhiteSpace(txtNomPays.Text) Then
            _context?.ErrorProvider.SetError(txtNomPays, "Le nom du pays est obligatoire.")
            isValid = False
        End If

        ' ISO2 obligatoire
        If String.IsNullOrWhiteSpace(txtIso2.Text) Then
            _context?.ErrorProvider.SetError(txtIso2, "Le code ISO2 est obligatoire.")
            isValid = False
        ElseIf txtIso2.Text.Trim().Length <> 2 Then
            _context?.ErrorProvider.SetError(txtIso2, "Le code ISO2 doit comporter exactement 2 caractères.")
            isValid = False
        End If

        ' ISO3 obligatoire
        If String.IsNullOrWhiteSpace(txtIso3.Text) Then
            _context?.ErrorProvider.SetError(txtIso3, "Le code ISO3 est obligatoire.")
            isValid = False
        ElseIf txtIso3.Text.Trim().Length <> 3 Then
            _context?.ErrorProvider.SetError(txtIso3, "Le code ISO3 doit comporter exactement 3 caractères.")
            isValid = False
        End If

        If Not isValid Then
            SetStatus("Formulaire incomplet ou invalide.")
        End If

        Return isValid

    End Function

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ApplySearch
    '
    '   • Applique le filtre de recherche
    '------------------------------------------------------------
    Private Sub ApplySearch()

        Try
            Dim searchText As String = txtSearch.Text.Trim()

            Dim dt = If(String.IsNullOrWhiteSpace(searchText),
                        GestionReferentiel.Pays_GetAll(),
                        GestionReferentiel.Pays_GetBySearch(searchText))

            dgvPays.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvPays)

            ' Masquer colonnes techniques
            If dgvPays.Columns.Contains("id_pays") Then dgvPays.Columns("id_pays").Visible = False
            If dgvPays.Columns.Contains("code_pays") Then dgvPays.Columns("code_pays").Visible = False
            If dgvPays.Columns.Contains("date_creation") Then dgvPays.Columns("date_creation").Visible = False
            If dgvPays.Columns.Contains("date_modification") Then dgvPays.Columns("date_modification").Visible = False

            ' Largeurs colonnes
            If dgvPays.Columns.Contains("nom_pays") Then dgvPays.Columns("nom_pays").Width = 200
            If dgvPays.Columns.Contains("iso2") Then dgvPays.Columns("iso2").Width = 80
            If dgvPays.Columns.Contains("iso3") Then dgvPays.Columns("iso3").Width = 80

            ' Sélection première ligne
            If dgvPays.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvPays, "nom_pays")
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvPays, "pays")

            SetStatus($"Recherche effectuée : {dgvPays.Rows.Count} résultat(s).")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch (UC_Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnSearch_Click
    '
    '   • Lance la recherche
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplySearch()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnClearSearch_Click
    '
    '   • Efface le filtre et recharge tous les pays
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Text = ""
        LoadGrid()
        SetStatus("Filtre effacé.")
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' txtSearch_KeyDown
    '
    '   • Recherche rapide au clavier (Entrée)
    '------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ApplySearch()
        End If
    End Sub

#End Region

#Region "Helpers"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SafeULong
    '
    '   • Conversion sécurisée String -> ULong
    '------------------------------------------------------------
    Private Function SafeULong(value As String) As ULong
        Return UtilsUCReferentiels.SafeULong(value)
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' GetCurrentPaysFromFields
    '
    '   • Récupère les valeurs actuelles des champs
    '------------------------------------------------------------
    Private Function GetCurrentPaysFromFields() As Pays

        Return New Pays With {
            .IdPays = SafeULong(txtIdPays.Text),
            .NomPays = txtNomPays.Text.Trim(),
            .Iso2 = txtIso2.Text.Trim(),
            .Iso3 = txtIso3.Text.Trim()
        }

    End Function

#End Region

#Region "Utilitaires (remplacés par UtilsUCReferentiels)"

    ' SafeULong déplacé vers UtilsUCReferentiels pour factorisation

#End Region

End Class
