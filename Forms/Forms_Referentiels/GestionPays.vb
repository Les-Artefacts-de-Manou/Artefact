'------------------------------------------------------------
' 📌 GestionPays.vb
' Version : V1.0
' Date    : 03/03/2026
'
' Rôle :
' Form de gestion du référentiel Pays.
'
' Règles Artefact :
' - Pas de SQL ici (QueryModule).
' - Pas d'accès DB direct ici (GestionReferentiel).
' - Validation UI : errProvider + StatusStrip.
' - Option B : modification uniquement via btnEdit.
'
' Évolution :
' - V1.0 : Squelette, modes, utilitaires UI.
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionPays

#Region "Déclarations"

    ' Mode d'édition actuel : Consultation, Nouveau ou Modification.
    Private _mode As ModeEdition = ModeEdition.Consultation

    ' Sauvegarde des valeurs affichées (pour Annuler en mode Modification)
    Private _snapshot As Pays = Nothing

    ' Identifiant de la ligne courante (pour optimisation lors du rechargement de la grille)
    Private _currentId As ULong = 0

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.1 - 09/03/2026
    ' GestionPays_Load
    '
    '   •	Initialisation complète du formulaire au chargement
    '   •	Charge la grille avec sélection automatique de la première ligne
    '   •	Positionne l'UI en mode Consultation par défaut
    '------------------------------------------------------------
    Private Sub GestionPays_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()

            LoadGrid()

            If dgvPays.Rows.Count = 0 Then
                ClearDetails()
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Pays chargés.")


        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur GestionPays_Load.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur au chargement de la fenêtre.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 03/03/2026
    ' InitToolTips
    '
    '   •	Configure les info-bulles pour tous les boutons et contrôles
    '   •	Améliore l'UX sans logique métier
    '------------------------------------------------------------
    Private Sub InitToolTips()

        If ttMain Is Nothing Then Return

        ttMain.SetToolTip(btnSearch, "Appliquer le filtre")
        ttMain.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        ttMain.SetToolTip(btnNew, "Créer un nouveau pays")
        ttMain.SetToolTip(btnEdit, "Passer en mode modification")
        ttMain.SetToolTip(btnSave, "Enregistrer les modifications")
        ttMain.SetToolTip(btnCancel, "Annuler les modifications")
        ttMain.SetToolTip(btnDelete, "Supprimer le pays sélectionné")
        ttMain.SetToolTip(btnClose, "Fermer")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 03/03/2026
    ' SetMode
    '
    '   •	Définit le mode d'édition du formulaire (Consultation/Nouveau/Modification)
    '   •	Active/désactive automatiquement les boutons, champs et navigation
    '   •	Met à jour le message de statut approprié
    '------------------------------------------------------------
    Private Sub SetMode(newMode As ModeEdition)

        _mode = newMode

        Dim isEdit As Boolean = (newMode = ModeEdition.Nouveau OrElse newMode = ModeEdition.Modification)
        Dim hasSelection As Boolean = (DgvGetSelectedId(dgvPays, "id_pays") <> 0UL)

        ' Grille / recherche verrouillées pendant édition
        dgvPays.Enabled = Not isEdit
        pnlTop.Enabled = Not isEdit

        ' Boutons
        btnNew.Enabled = Not isEdit
        btnEdit.Enabled = (Not isEdit) AndAlso hasSelection
        btnDelete.Enabled = (Not isEdit) AndAlso hasSelection

        btnSave.Enabled = isEdit
        btnCancel.Enabled = isEdit
        btnClose.Enabled = True

        ' Champs
        SetDetailsEnabled(isEdit)

        ' Status
        Select Case newMode
            Case ModeEdition.Consultation
                SetStatus("Prêt.")
            Case ModeEdition.Nouveau
                SetStatus("Création d'un nouveau pays...")
            Case ModeEdition.Modification
                SetStatus("Modification en cours...")
        End Select

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 03/03/2026
    ' SetDetailsEnabled
    '
    '   •	Active/désactive les champs de saisie du panneau détails
    '   •	Note : code_pays reste toujours en lecture seule (généré automatiquement)
    '   •	id_pays est un champ technique caché, jamais éditable
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
    ' 📌 V1.0 - 02/03/2026
    ' HasSelectedPays
    '
    '   •	Vérifie si une langue est actuellement sélectionnée dans la grille
    '   •	Retourne True si une ligne est sélectionnée avec un ID valide
    '------------------------------------------------------------
    ' TODO : A implémenter le process vérification sélection pays pour btnEdit et btnDelete (actuellement géré par l'état des boutons dans SetMode)
    '------------------------------------------------------------
    Private Function HasSelectedPays() As Boolean

        Return (DgvGetSelectedId(dgvPays, "id_pays") <> 0UL)

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
    '   •	Crée une nouveau pays
    '   •	Vide les champs détails, passe en mode Nouveau
    '   •	Focus sur le premier champ de saisie
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        ClearErrors()
        _snapshot = Nothing

        ClearDetails()
        SetMode(ModeEdition.Nouveau)
        txtNomPays.Focus()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 02/03/2026
    ' btnEdit_Click
    '
    '   •	Modifie un pays
    '   •	Vérifie qu'un pays est sélectionnée
    '   •	Crée un snapshot pour annulation possible
    '   •	Passe en mode Modification
    '------------------------------------------------------------
    ' TODO : A implémenter le process modification pays

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        ClearErrors()

        If DgvGetSelectedId(dgvPays, "id_pays") = 0UL Then
            SetStatus("Aucun pays sélectionné.")
            Return
        End If

        SnapshotFromFields()

        SetMode(ModeEdition.Modification)
        txtNomPays.Focus()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 03/03/2026
    ' btnSave_Click
    '
    '   •	Enregistre le pays (création ou modification)
    '   •	Valide les champs
    '   •	Appelle GestionReferentiel.Pays_Insert ou Pays_Update
    '   •	Recharge la grille et repositionne sur la ligne concernée
    '   •	Retour en mode Consultation
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
            DgvSelectRowById(dgvPays, "id_pays", targetId)
            BindSelectedToDetails()

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnSave_Click (Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
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
    ' 📌 V1.1 - 09/03/2026
    ' btnDelete_Click
    '
    '   •	Supprime la langue sélectionnée
    '   •	Vérifie la sélection
    '   •	Compte les usages (auteurs, livres)
    '   •	Affiche message de confirmation adapté selon les dépendances
    '   •	Suppression avec mise à NULL automatique des références
    '   •	Recharge la grille
    '------------------------------------------------------------
    ' TODO: tester suppression pays avec auteurs_pays (FK RESTRICT)
    ' lorsque les tables auteurs et auteurs_pays seront peuplées.
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If _mode <> ModeEdition.Consultation Then
            SetStatus("Suppression impossible pendant une édition.")
            Return
        End If

        Dim id As ULong = DgvGetSelectedId(dgvPays, "id_pays")
        If id = 0UL Then
            SetStatus("Aucun pays sélectionné.")
            Return
        End If

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
            GestionLog.EcrireLog("UI: erreur btnDelete_Click (Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la suppression.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 09/03/2026
    ' btnClose_Click
    '
    ' Fermeture du formulaire.
    '------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.3 - 09/03/2026
    ' LoadGrid
    '
    '   •	Charge et affiche la liste complète des pays    
    '   •	Récupère les données via GestionReferentiel.Pays_GetAll
    '   •	Applique le formatage standard (FormatReferentielGrid)
    '   •	Masque les colonnes techniques (id, timestamps)
    '   •	Sélectionne automatiquement la première ligne si présente
    '   •	Met à jour le compteur d'affichage
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try
            Dim dt As DataTable = GestionReferentiel.Pays_GetAll()

            dgvPays.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvPays)

            ' Masquer colonnes techniques
            If dgvPays.Columns.Contains("id_pays") Then
                dgvPays.Columns("id_pays").Visible = False
            End If

            If dgvPays.Columns.Contains("created_at") Then
                dgvPays.Columns("created_at").Visible = False
            End If

            If dgvPays.Columns.Contains("updated_at") Then
                dgvPays.Columns("updated_at").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} pays"

            If UtilsForm.SelectFirstRow(dgvPays, "nom_pays") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid (Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement des pays.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' BindSelectedRowToDetails
    '
    '   •	Transfère les données de la ligne sélectionnée vers les champs détails
    '   •	Si aucune sélection : vide les détails et passe en mode Consultation
    '   •	Met à jour _currentId
    '------------------------------------------------------------
    Private Sub BindSelectedToDetails()

        If dgvPays.CurrentRow Is Nothing Then
            ClearDetails()
            _currentId = 0
            SetMode(ModeEdition.Consultation)
            Return
        End If

        Dim row As DataGridViewRow = dgvPays.CurrentRow

        txtIdPays.Text = row.Cells("id_pays").Value?.ToString()
        txtNomPays.Text = row.Cells("nom_pays").Value?.ToString()
        txtIso2.Text = row.Cells("iso2").Value?.ToString()
        txtIso3.Text = row.Cells("iso3").Value?.ToString()
        txtCodePays.Text = row.Cells("code_pays").Value?.ToString()

        If Not String.IsNullOrWhiteSpace(txtIdPays.Text) Then
            _currentId = Convert.ToUInt64(txtIdPays.Text)
        Else
            _currentId = 0
        End If
        SetMode(ModeEdition.Consultation)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' ClearDetails
    '
    '   • Efface tous les champs du panneau détails
    Private Sub ClearDetails()

        txtIdPays.Clear()
        txtNomPays.Clear()
        txtIso2.Clear()
        txtIso3.Clear()
        txtCodePays.Clear()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' dgvPays_SelectionChanged
    '
    '   •	Événement déclenché lors du changement de sélection dans la grille
    '   •	Synchronise automatiquement les détails si en mode Consultation
    Private Sub dgvPays_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPays.SelectionChanged
        If _mode <> ModeEdition.Consultation Then Return
        BindSelectedToDetails()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' SnapshotFromFields
    '
    '   •	Crée une copie temporaire des valeurs actuelles des champs
    '   •	Utilisé avant modification pour permettre l'annulation
    '   •	Stocke dans _snapshot
    Private Sub SnapshotFromFields()

        _snapshot = New Pays With {
            .IdPays = SafeULong(txtIdPays.Text),
            .NomPays = txtNomPays.Text,
            .Iso2 = txtIso2.Text,
            .Iso3 = txtIso3.Text,
            .CodePays = txtCodePays.Text
        }

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
    ' RestoreSnapshotToFields
    '
    '   •	Restaure les champs à partir du snapshot sauvegardé
    '   •	Utilisé lors de l'annulation d'une modification
    Private Sub RestoreSnapshotToFields()

        If _snapshot Is Nothing Then Return

        txtIdPays.Text = _snapshot.IdPays.ToString()
        txtNomPays.Text = _snapshot.NomPays
        txtIso2.Text = _snapshot.Iso2
        txtIso3.Text = _snapshot.Iso3
        txtCodePays.Text = _snapshot.CodePays

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 09/03/2026
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
    ' 📌 V1.2 - 03/03/2026
    ' ValidateForm
    '
    '   •	Valide tous les champs avant enregistrement
    '   •	Règles : nom obligatoire, abréviation obligatoire, ISO 639-1 max 2 car., ISO 639-2 max 3 car.
    '   •	Retourne True si tous les champs sont valides
    '   •	False sinon (affiche erreur visuelle et message)    
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtNomPays.Text) Then
            errProvider.SetError(txtNomPays, "Le nom du pays est obligatoire.")
            SetStatus("Nom pays obligatoire.")
            txtNomPays.Focus()
            Return False
        End If

        Dim iso2 As String = txtIso2.Text.Trim()
        If iso2 <> "" AndAlso iso2.Length <> 2 Then
            errProvider.SetError(txtIso2, "ISO2 doit contenir exactement 2 caractères.")
            SetStatus("ISO2 invalide.")
            txtIso2.Focus()
            Return False
        End If

        Dim iso3 As String = txtIso3.Text.Trim()
        If iso3 <> "" AndAlso iso3.Length <> 3 Then
            errProvider.SetError(txtIso3, "ISO3 doit contenir exactement 3 caractères.")
            SetStatus("ISO3 invalide.")
            txtIso3.Focus()
            Return False
        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.3 - 03/03/2026
    ' GetCurrentPaysFromFields
    '
    '   •	Construit un objet Pays à partir des valeurs actuelles des champs
    '   •	Utilisé pour confirmations et opérations CRUD
    '------------------------------------------------------------
    Private Function GetCurrentPaysFromFields() As Pays

        Return New Pays With {
            .IdPays = SafeULong(txtIdPays.Text),
            .NomPays = txtNomPays.Text.Trim(),
            .Iso2 = txtIso2.Text.Trim(),
            .Iso3 = txtIso3.Text.Trim(),
            .CodePays = txtCodePays.Text.Trim()
        }

    End Function

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.5 - 09/03/2026
    ' btnSearch_Click
    '
    '   •	Lance la recherche selon le texte saisi
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplySearch()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.5 - 09/03/2026
    ' btnClearSearch_Click
    '
    '   •	Efface le texte de recherche et réaffiche la liste complète
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Clear()
        ApplySearch()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.5 - 09/03/2026
    ' ApplySearch
    '
    '   •	Applique le filtre de recherche et recharge la grille
    '   •	Texte vide : affiche tous les pays
    '   •	Texte présent : appelle GestionReferentiel.Pays_GetBySearch
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
                dt = GestionReferentiel.Pays_GetAll()
                SetStatus("Liste complète affichée.")
            Else
                dt = GestionReferentiel.Pays_GetBySearch(searchText)
                SetStatus($"Filtre appliqué : '{searchText}'")
            End If

            dgvPays.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvPays)

            If dgvPays.Columns.Contains("id_pays") Then
                dgvPays.Columns("id_pays").Visible = False
            End If

            If dgvPays.Columns.Contains("created_at") Then
                dgvPays.Columns("created_at").Visible = False
            End If

            If dgvPays.Columns.Contains("updated_at") Then
                dgvPays.Columns("updated_at").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} pays"

            If UtilsForm.SelectFirstRow(dgvPays, "nom_pays") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch (Pays).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.5 - 09/03/2026
    ' txtSearch_KeyDown
    '
    '   •	Détecte la touche Entrée pour lancer la recherche
    '      •	Améliore l'UX en évitant de cliquer sur le bouton
    ''------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplySearch()
            e.SuppressKeyPress = True
        End If
    End Sub

#End Region


End Class