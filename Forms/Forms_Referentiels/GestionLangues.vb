'====================================================================
' 📌 GestionLangues.vb
' Version : V1.1
' Date    : 11/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Form de gestion du référentiel Langues.
'
' Évolution :
' - V1.0 : Squelette, gestion modes, utilitaires UI (Status + ErrorProvider + ToolTips).
' - V1.1 : Mise en pages
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionLangues

#Region "Déclarations"

    'Modes d'utilisation de la form
    Private _mode As ModeEdition = UtilsForm.ModeEdition.Consultation

    ' Sauvegarde des valeurs affichées (pour Annuler en mode Modification)
    Private _snapshot As Langue = Nothing

    ' Identifiant de la ligne courante (pour optimisation lors du rechargement de la grille)
    Private _currentId As ULong = 0

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' _Load

    ' Initialise la form :
    '   •	Initialisation complète du formulaire au chargement
    '   •	Charge la grille avec sélection automatique de la première ligne
    '   •	Positionne l'UI en mode Consultation par défaut
    '------------------------------------------------------------
    Private Sub GestionLangues_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            "UI: erreur chargement GestionLangues.",
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
    ' 📌 V1.0 - 02/03/2026
    ' InitToolTips
    '
    '   •	Configure les info-bulles pour tous les boutons et contrôles
    '   •	Améliore l'UX sans logique métier
    '------------------------------------------------------------
    Private Sub InitToolTips()

        If ttMain Is Nothing Then Return

        ttMain.SetToolTip(btnSearch, "Appliquer le filtre")
        ttMain.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        ttMain.SetToolTip(btnNew, "Créer une nouvelle langue")
        ttMain.SetToolTip(btnEdit, "Passer en mode modification")
        ttMain.SetToolTip(btnSave, "Enregistrer les modifications")
        ttMain.SetToolTip(btnCancel, "Annuler les modifications")
        ttMain.SetToolTip(btnDelete, "Supprimer la langue sélectionnée")
        ttMain.SetToolTip(btnClose, "Fermer")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' SetMode
    '
    '   •	Définit le mode d'édition du formulaire (Consultation/Nouveau/Modification)
    '   •	Active/désactive automatiquement les boutons, champs et navigation
    '   •	Met à jour le message de statut approprié
    '------------------------------------------------------------
    Private Sub SetMode(newMode As ModeEdition)

        _mode = newMode

        Dim isEdit As Boolean = (newMode = UtilsForm.ModeEdition.Nouveau OrElse newMode = UtilsForm.ModeEdition.Modification)
        Dim hasSelection As Boolean = HasSelectedLangue()

        ' --- Grille & recherche
        dgvLangues.Enabled = Not isEdit
        pnlTop.Enabled = Not isEdit

        ' --- Boutons
        btnNew.Enabled = Not isEdit
        btnEdit.Enabled = (Not isEdit) AndAlso hasSelection
        btnDelete.Enabled = (Not isEdit) AndAlso hasSelection

        btnSave.Enabled = isEdit
        btnCancel.Enabled = isEdit
        btnClose.Enabled = True

        ' --- Champs détails
        SetDetailsEnabled(isEdit)

        ' --- Statut
        Select Case newMode
            Case UtilsForm.ModeEdition.Consultation
                SetStatus("Prêt.")
            Case UtilsForm.ModeEdition.Nouveau
                SetStatus("Création d'une nouvelle langue...")
            Case UtilsForm.ModeEdition.Modification
                SetStatus("Modification en cours...")
        End Select

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' SetDetailsEnabled
    '
    '   •	Active/désactive les champs de saisie du panneau détails
    '   •	Note : code_langue reste toujours en lecture seule (généré automatiquement)
    '------------------------------------------------------------
    Private Sub SetDetailsEnabled(enabled As Boolean)

        txtNomLangue.Enabled = enabled
        txtAbrevLangue.Enabled = enabled
        txtIso639_1.Enabled = enabled
        txtIso639_2.Enabled = enabled

        ' Champ généré : toujours non éditable
        txtCodeLangue.ReadOnly = True
        txtCodeLangue.TabStop = False

        ' Champ technique : caché
        txtIdLangue.Visible = False
        txtIdLangue.TabStop = False

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' HasSelectedLangue
    '
    '   •	Vérifie si une langue est actuellement sélectionnée dans la grille
    '   •	Retourne True si une ligne est sélectionnée avec un ID valide
    '------------------------------------------------------------
    Private Function HasSelectedLangue() As Boolean

        Return (DgvGetSelectedId(dgvLangues, "id_langue") <> 0UL)

    End Function

#End Region

#Region "Interface utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' SetStatus
    '
    '   •	Affiche un message dans la barre d'état (StatusStrip)
    '   •	Utilisé pour feedback utilisateur sans popup
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)

        If stsLabelStatus Is Nothing Then Return
        stsLabelStatus.Text = message

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' ClearErrors
    '
    '   •	Efface tous les indicateurs d'erreur visuels (ErrorProvider)
    '   •	À appeler avant chaque nouvelle validation
    '------------------------------------------------------------
    Private Sub ClearErrors()

        If errProvider Is Nothing Then Return
        errProvider.Clear()

    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' btnNew_Click
    '
    '   •	Crée une nouvelle langue
    '   •	Vide les champs détails, passe en mode Nouveau
    '   •	Focus sur le premier champ de saisie
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearErrors()
        _snapshot = Nothing

        ClearDetails()
        SetMode(ModeEdition.Nouveau)

        txtNomLangue.Focus()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' btnEdit_Click
    '
    '   •	Édite la langue sélectionnée
    '   •	Vérifie qu'une langue est sélectionnée
    '   •	Crée un snapshot pour annulation possible
    '   •	Passe en mode Modification
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
    ' 📌 V1.0 - 02/03/2026
    ' btnSave_Click
    '
    '   •	Enregistre la langue (création ou modification)
    '   •	Valide les champs
    '   •	Appelle GestionReferentiel.Langues_Insert ou Langues_Update
    '   •	Recharge la grille et repositionne sur la ligne concernée
    '   •	Retour en mode Consultation
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
            DgvSelectRowById(dgvLangues, "id_langue", newId)
            BindSelectedToDetails()

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnSave_Click.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'enregistrement.")
        End Try
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' btnCancel_Click
    '
    '   •	Annule les modifications en cours
    '   •	Mode Nouveau : retour à la sélection courante ou détails vides
    '   •	Mode Modification : restauration du snapshot
    '   •	Retour en mode Consultation
    '------------------------------------------------------------

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearErrors()

        If _mode = ModeEdition.Nouveau Then
            ' Retour à la sélection courante (si elle existe), sinon vide
            BindSelectedToDetails()
        ElseIf _mode = ModeEdition.Modification Then
            RestoreSnapshotToFields()
            _snapshot = Nothing
        End If

        SetMode(ModeEdition.Consultation)
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.3 - 03/03/2026
    ' btnDelete_Click
    ' 
    '   •	Supprime la langue sélectionnée
    '   •	Vérifie la sélection
    '   •	Compte les usages (auteurs, livres)
    '   •	Affiche message de confirmation adapté selon les dépendances
    '   •	Suppression avec mise à NULL automatique des références
    '   •	Recharge la grille
    '------------------------------------------------------------
    ' TODO: tester suppression langue utilisée dans auteurs et livres
    ' vérifier que SET NULL fonctionne correctement.
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
            "UI: erreur btnDelete_Click (Langues_Delete).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' btnClose_Click
    '
    '   •	Ferme le formulaire
    '------------------------------------------------------------

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.3- 09/03/2026
    ' LoadGrid
    '
    '   •	Charge et affiche la liste complète des langues
    '   •	Récupère les données via GestionReferentiel.Langues_GetAll
    '   •	Applique le formatage standard (FormatReferentielGrid)
    '   •	Masque les colonnes techniques (id, timestamps)
    '   •	Sélectionne automatiquement la première ligne si présente
    '   •	Met à jour le compteur d'affichage
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try
            Dim dt As DataTable = GestionReferentiel.Langues_GetAll()

            dgvLangues.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvLangues)

            ' Colonnes techniques
            If dgvLangues.Columns.Contains("id_langue") Then
                dgvLangues.Columns("id_langue").Visible = False
            End If

            If dgvLangues.Columns.Contains("created_at") Then
                dgvLangues.Columns("created_at").Visible = False
            End If

            If dgvLangues.Columns.Contains("updated_at") Then
                dgvLangues.Columns("updated_at").Visible = False
            End If

            ' Mise à jour compteur
            lblCount.Text = $"{dt.Rows.Count} langue(s)"

            ' Sélection automatique première ligne si existante
            If UtilsForm.SelectFirstRow(dgvLangues, "nom_langue") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement des langues.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' BindSelectedToDetails
    '   
    '   •	Transfère les données de la ligne sélectionnée vers les champs détails
    '   •	Si aucune sélection : vide les détails et passe en mode Consultation
    '   •	Met à jour _currentId
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
    ' 📌 V1.0 - 02/03/2026
    ' ClearDetails
    '
    '   •	Efface tous les champs du panneau détails
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
    ' 📌 V1.0 - 02/03/2026
    ' dgvLangues_SelectionChanged
    '
    '   •	Événement déclenché lors du changement de sélection dans la grille
    '   •	Synchronise automatiquement les détails si en mode Consultation
    '------------------------------------------------------------
    Private Sub dgvLangues_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLangues.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Return
        BindSelectedToDetails()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 02/03/2026
    ' SnapshotFromFields
    '
    '   •	Crée une copie temporaire des valeurs actuelles des champs
    '   •	Utilisé avant modification pour permettre l'annulation
    '   •	Stocke dans _snapshot
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
    ' 📌 V1.1 - 02/03/2026
    ' SnapshotToFields
    '
    '   •	Restaure les champs à partir du snapshot sauvegardé
    '   •	Utilisé lors de l'annulation d'une modification
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

    '------------------------------------------------------------
    ' 📌 V1.1 - 02/03/2026
    ' SafeULong
    '   
    '   •	Convertit une chaîne en ULong de manière sécurisée
    '   •	Retourne 0 si la conversion échoue ou si la chaîne est vide
    '------------------------------------------------------------
    Private Function SafeULong(value As String) As ULong
        Dim t = If(value, "").Trim()
        If t = "" Then Return 0UL
        Dim n As ULong
        If ULong.TryParse(t, n) Then Return n
        Return 0UL
    End Function

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.2 - 02/03/2026
    ' ValidateForm
    '
    '   •	Valide tous les champs avant enregistrement
    '   •	Règles : nom obligatoire, abréviation obligatoire, ISO 639-1 max 2 car., ISO 639-2 max 3 car.
    '   •	Retourne True si tous les champs sont valides
    '   •	False sinon (affiche erreur visuelle et message)
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtNomLangue.Text) Then
            errProvider.SetError(txtNomLangue, "Le nom est obligatoire.")
            SetStatus("Nom obligatoire.")
            txtNomLangue.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtAbrevLangue.Text) Then
            errProvider.SetError(txtAbrevLangue, "L'abréviation est obligatoire.")
            SetStatus("Abréviation obligatoire.")
            txtAbrevLangue.Focus()
            Return False
        End If

        If txtIso639_1.Text.Length > 2 Then
            errProvider.SetError(txtIso639_1, "ISO 639-1 doit contenir 2 caractères.")
            SetStatus("ISO 639-1 invalide.")
            txtIso639_1.Focus()
            Return False
        End If

        If txtIso639_2.Text.Length > 3 Then
            errProvider.SetError(txtIso639_2, "ISO 639-2 doit contenir 3 caractères.")
            SetStatus("ISO 639-2 invalide.")
            txtIso639_2.Focus()
            Return False
        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.3 - 03/03/2026
    ' GetCurrentLangueFromFields
    '
    '   •	Construit un objet Langue à partir des valeurs actuelles des champs
    '   •	Utilisé pour confirmations et opérations CRUD
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
    ' 📌 V1.2 - 09/03/2026
    ' btnSearch_Click
    '   
    '   •	Lance la recherche selon le texte saisi
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' btnClearSearch_Click
    '
    '   •	Efface le texte de recherche et réaffiche la liste complète
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()
        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' ApplySearch
    '
    '   •	Applique le filtre de recherche et recharge la grille
    '   •	Texte vide : affiche toutes les langues
    '   •	Texte présent : appelle GestionReferentiel.Langues_GetBySearch
    '   •	Sélectionne automatiquement la première ligne des résultats
    '   •	Met à jour le compteur
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

            If dgvLangues.Columns.Contains("id_langue") Then
                dgvLangues.Columns("id_langue").Visible = False
            End If

            If dgvLangues.Columns.Contains("created_at") Then
                dgvLangues.Columns("created_at").Visible = False
            End If

            If dgvLangues.Columns.Contains("updated_at") Then
                dgvLangues.Columns("updated_at").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} langue(s)"

            If UtilsForm.SelectFirstRow(dgvLangues, "nom_langue") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' txtSearch_KeyDown
    '
    '   •	Détecte la touche Entrée pour lancer la recherche
    '   •	Améliore l'UX en évitant de cliquer sur le bouton
    '------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown

        If e.KeyCode = Keys.Enter Then
            ApplySearch()
            e.SuppressKeyPress = True
        End If

    End Sub

#End Region

End Class