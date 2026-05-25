'------------------------------------------------------------
' 📌 GestionContacts.vb
' Version : V1.1
' Date    : 12/03/2026
'
' Rôle :
' Formulaire WinForms de gestion du référentiel Contacts.
'
' Règles Artefact :
' - Pas de SQL ici (QueryModule).
' - Pas d'accès DB direct ici (GestionReferentiel).
' - Validation UI : errProvider + StatusStrip.
' - code_contact affiché en lecture seule.
' - id_contact conservé pour le pilotage CRUD mais non visible.
'
' Évolution :
' - V1.0 : CRUD de base, recherche, ouverture email.
' - V1.1 : Réorganisation complète sur le modèle de GestionPays
'          (régions, ordre logique, en-têtes homogènes, nettoyage mineur).
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionContacts

#Region "Déclarations"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' Variables privées
    '
    '   • _mode      : mode courant du formulaire
    '   • _currentId : identifiant du contact courant
    '------------------------------------------------------------
    Private _mode As ModeEdition = ModeEdition.Consultation
    Private _currentId As ULong = 0

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 07/03/2026
    ' GestionContacts_Load
    '
    '   • Initialise complètement le formulaire au chargement
    '   • Charge la grille et les détails associés
    '   • Positionne l'UI en mode Consultation
    '   • Configure les info-bulles et le style des champs email
    '------------------------------------------------------------
    Private Sub GestionContacts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            InitToolTips()
            ClearErrors()

            LoadGrid()

            If dgvContacts.Rows.Count = 0 Then
                ClearContactDetails()
                _currentId = 0
            End If

            SetMode(ModeEdition.Consultation)
            UpdateEmailFieldStyle()
            SetStatus("Contacts chargés.")

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur chargement GestionContacts.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors du chargement des contacts." & Environment.NewLine & ex.Message,
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
        ttMain.SetToolTip(btnNew, "Créer un nouveau contact")
        ttMain.SetToolTip(btnEdit, "Passer en mode modification")
        ttMain.SetToolTip(btnSave, "Enregistrer les modifications")
        ttMain.SetToolTip(btnCancel, "Annuler les modifications")
        ttMain.SetToolTip(btnDelete, "Supprimer le contact sélectionné")
        ttMain.SetToolTip(txtEmailPerso, "Double-cliquer pour envoyer un email")
        ttMain.SetToolTip(txtAdresseLiseuse, "Double-cliquer pour envoyer un email")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 07/03/2026
    ' SetMode
    '
    '   • Définit le mode courant du formulaire
    '   • Active/désactive les boutons selon le contexte
    '   • Passe les champs en lecture seule ou en saisie
    '   • Met à jour le style des champs email
    '------------------------------------------------------------

    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Dim hasSelection As Boolean =
            dgvContacts IsNot Nothing AndAlso
            dgvContacts.CurrentRow IsNot Nothing AndAlso
            dgvContacts.Rows.Count > 0

        Select Case mode

            Case ModeEdition.Consultation

                btnNew.Enabled = True
                btnEdit.Enabled = hasSelection
                btnDelete.Enabled = hasSelection

                btnSave.Enabled = False
                btnCancel.Enabled = False

                txtNomContact.ReadOnly = True
                txtEmailPerso.ReadOnly = True
                txtAdresseLiseuse.ReadOnly = True
                txtTypeLiseuse.ReadOnly = True

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNew.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False

                btnSave.Enabled = True
                btnCancel.Enabled = True

                txtNomContact.ReadOnly = False
                txtEmailPerso.ReadOnly = False
                txtAdresseLiseuse.ReadOnly = False
                txtTypeLiseuse.ReadOnly = False

        End Select

        UpdateEmailFieldStyle()

    End Sub


#End Region

#Region "Interface utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 07/03/2026
    ' SetStatus
    '
    '   • Affiche un message dans la barre de statut
    '   • Fournit un retour utilisateur non bloquant
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)

        If stsLabelStatus Is Nothing Then Return
        stsLabelStatus.Text = message

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ClearErrors
    '
    '   • Efface les erreurs visuelles portées par ErrorProvider
    '   • À appeler avant toute validation ou changement de contexte
    '------------------------------------------------------------
    Private Sub ClearErrors()

        If errProvider Is Nothing Then Return
        errProvider.Clear()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' UpdateEmailFieldStyle
    '
    '   • Applique le style visuel des champs email
    '   • Conserve un fond standard lorsque les champs sont en lecture seule
    '------------------------------------------------------------
    Private Sub UpdateEmailFieldStyle()

        txtEmailPerso.ForeColor = Color.CornflowerBlue
        txtAdresseLiseuse.ForeColor = Color.CornflowerBlue

        If txtEmailPerso.ReadOnly Then
            txtEmailPerso.BackColor = SystemColors.Window
        End If

        If txtAdresseLiseuse.ReadOnly Then
            txtAdresseLiseuse.BackColor = SystemColors.Window
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' txtEmailPerso_DoubleClick
    '
    '   • Ouvre le client mail pour l'adresse personnelle
    '   • Ne modifie aucune donnée du formulaire
    '------------------------------------------------------------
    Private Sub txtEmailPerso_DoubleClick(sender As Object, e As EventArgs) Handles txtEmailPerso.DoubleClick

        Try

            UtilsForm.OpenEmailClient(txtEmailPerso.Text)

        Catch ex As Exception

            GestionLog.EcrireLog(
                $"UI: erreur ouverture client mail pour {txtEmailPerso.Text}.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Impossible d'ouvrir le client mail.",
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' txtEmailPerso_TextChanged
    '
    '   • Met à jour le curseur et le comportement visuel du champ email personnel
    '------------------------------------------------------------
    Private Sub txtEmailPerso_TextChanged(sender As Object, e As EventArgs) Handles txtEmailPerso.TextChanged

        UtilsForm.UpdateEmailTextBoxCursor(txtEmailPerso)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' txtAdresseLiseuse_DoubleClick
    '
    '   • Ouvre le client mail pour l'adresse liseuse
    '   • Ne modifie aucune donnée du formulaire
    '------------------------------------------------------------
    Private Sub txtAdresseLiseuse_DoubleClick(sender As Object, e As EventArgs) Handles txtAdresseLiseuse.DoubleClick

        Try

            UtilsForm.OpenEmailClient(txtAdresseLiseuse.Text)

        Catch ex As Exception

            GestionLog.EcrireLog(
                $"UI: erreur ouverture client mail pour {txtAdresseLiseuse.Text}.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Impossible d'ouvrir le client mail.",
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' txtAdresseLiseuse_TextChanged
    '
    '   • Met à jour le curseur et le comportement visuel du champ adresse liseuse
    '------------------------------------------------------------
    Private Sub txtAdresseLiseuse_TextChanged(sender As Object, e As EventArgs) Handles txtAdresseLiseuse.TextChanged

        UtilsForm.UpdateEmailTextBoxCursor(txtAdresseLiseuse)

    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' btnNew_Click
    '
    '   • Passe le formulaire en mode Nouveau
    '   • Vide les champs de détail
    '   • Prépare la saisie d'un nouveau contact
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try

            ClearErrors()
            ClearContactDetails()

            _currentId = 0

            SetMode(ModeEdition.Nouveau)
            SetStatus("Nouveau contact.")

            txtNomContact.Focus()

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur passage en mode Nouveau pour contact.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors de l'initialisation d'un nouveau contact." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' btnEdit_Click
    '
    '   • Passe le formulaire en mode Modification
    '   • Vérifie qu'un contact valide est sélectionné
    '   • Conserve l'identifiant courant pour la mise à jour
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try

            ClearErrors()

            If dgvContacts.CurrentRow Is Nothing Then
                SetStatus("Aucun contact sélectionné.")
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(txtIdContact.Text) Then
                SetStatus("Sélection invalide.")
                Exit Sub
            End If

            _currentId = Convert.ToUInt64(txtIdContact.Text)

            SetMode(ModeEdition.Modification)
            SetStatus("Modification du contact.")

            txtNomContact.Focus()

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur passage en mode Modification contact.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors du passage en mode modification." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 09/03/2026
    ' btnSave_Click
    '
    '   • Enregistre le contact en création ou en modification
    '   • Valide les données avant appel au référentiel
    '   • Recharge la grille et repositionne la sélection après sauvegarde
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            ClearErrors()

            If Not ValidateForm() Then Exit Sub

            Dim contact As Contact = BuildContactFromFields()

            If _mode = ModeEdition.Nouveau Then

                If GestionReferentiel.Contact_ExistsByNom(contact.NomContact) Then
                    errProvider.SetError(txtNomContact, "Ce nom de contact existe déjà.")
                    txtNomContact.Focus()
                    Exit Sub
                End If

                GestionReferentiel.Contact_Insert(contact)

                GestionLog.EcrireLog(
                    $"UI: création contact '{contact.NomContact}'.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.UI
                )

                SetStatus("Contact créé.")

            ElseIf _mode = ModeEdition.Modification Then

                If GestionReferentiel.Contact_ExistsByNomExceptId(contact.NomContact, contact.IdContact) Then
                    errProvider.SetError(txtNomContact, "Ce nom de contact existe déjà.")
                    txtNomContact.Focus()
                    Exit Sub
                End If

                GestionReferentiel.Contact_Update(contact)

                GestionLog.EcrireLog(
                    $"UI: modification contact id={contact.IdContact}.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.UI
                )

                SetStatus("Contact modifié.")

            Else
                Exit Sub
            End If

            LoadGrid()
            ReselectContactByNom(contact.NomContact)
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur enregistrement contact.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors de l'enregistrement du contact." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' btnCancel_Click
    '
    '   • Annule l'édition en cours
    '   • Revient au mode Consultation
    '   • Recharge le détail sélectionné si une ligne existe encore
    '------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try

            ClearErrors()
            ClearContactDetails()

            SetMode(ModeEdition.Consultation)

            If dgvContacts.Rows.Count > 0 Then
                BindSelectedContactToDetails()
                SetStatus("Modification annulée.")
            Else
                _currentId = 0
                SetStatus("Annulation.")
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur annulation édition contact.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors de l'annulation." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' btnDelete_Click
    '
    '   • Supprime le contact sélectionné après contrôles
    '   • Vérifie d'abord son utilisation éventuelle dans livres_contacts
    '   • Recharge ensuite la grille et les détails affichés
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try

            ClearErrors()

            If dgvContacts.CurrentRow Is Nothing Then
                SetStatus("Aucun contact sélectionné.")
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(txtIdContact.Text) Then
                SetStatus("Sélection invalide.")
                Exit Sub
            End If

            Dim idContact As ULong = Convert.ToUInt64(txtIdContact.Text)
            Dim usageCount As Integer = GestionReferentiel.Contact_CountUsage(idContact)

            If usageCount > 0 Then

                MessageBox.Show(
                    "Ce contact est utilisé dans des envois de livres et ne peut pas être supprimé.",
                    "Suppression impossible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                SetStatus("Suppression impossible : contact utilisé.")
                Exit Sub

            End If

            Dim rep = MessageBox.Show(
                "Confirmer la suppression de ce contact ?",
                "Suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If rep <> DialogResult.Yes Then Exit Sub

            GestionReferentiel.Contact_Delete(idContact)

            SetStatus("Contact supprimé.")

            LoadGrid()
            ClearContactDetails()

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur suppression contact.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            MessageBox.Show(
                "Erreur lors de la suppression." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 12/03/2026
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
    ' 📌 V1.0 - 07/03/2026
    ' LoadGrid
    '
    '   • Charge la grille des contacts depuis le référentiel
    '   • Applique le format standard des grilles référentielles
    '   • Masque les colonnes techniques et sélectionne la première ligne
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try

            Dim dt As DataTable = GestionReferentiel.Contact_GetAll()

            dgvContacts.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvContacts)

            If dgvContacts.Columns.Contains("id_contact") Then
                dgvContacts.Columns("id_contact").Visible = False
            End If

            If dgvContacts.Columns.Contains("created_at") Then
                dgvContacts.Columns("created_at").Visible = False
            End If

            If dgvContacts.Columns.Contains("updated_at") Then
                dgvContacts.Columns("updated_at").Visible = False
            End If

            If dgvContacts.Rows.Count > 0 Then

                dgvContacts.ClearSelection()
                dgvContacts.Rows(0).Selected = True
                dgvContacts.CurrentCell = dgvContacts.Rows(0).Cells(dgvContacts.Columns("nom_contact").Index)

                BindSelectedContactToDetails()

            Else

                ClearContactDetails()
                _currentId = 0

            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur chargement grille contacts.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 07/03/2026
    ' ClearContactDetails
    '
    '   • Réinitialise tous les champs de détail du contact
    '   • Ne modifie pas la grille ni le mode du formulaire
    '------------------------------------------------------------
    Private Sub ClearContactDetails()

        txtIdContact.Clear()
        txtCodeContact.Clear()

        txtNomContact.Clear()
        txtEmailPerso.Clear()
        txtAdresseLiseuse.Clear()
        txtTypeLiseuse.Clear()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 07/03/2026
    ' BindSelectedContactToDetails
    '
    '   • Charge dans le panneau détail le contact sélectionné dans la grille
    '   • Met également à jour _currentId si l'identifiant est valide
    '------------------------------------------------------------
    Private Sub BindSelectedContactToDetails()

        Try

            If dgvContacts.SelectedRows.Count = 0 Then Exit Sub

            Dim row As DataGridViewRow = dgvContacts.SelectedRows(0)

            txtIdContact.Text = row.Cells("id_contact").Value?.ToString()
            txtCodeContact.Text = row.Cells("code_contact").Value?.ToString()

            txtNomContact.Text = row.Cells("nom_contact").Value?.ToString()
            txtEmailPerso.Text = row.Cells("email_perso").Value?.ToString()
            txtAdresseLiseuse.Text = row.Cells("adresse_liseuse").Value?.ToString()
            txtTypeLiseuse.Text = row.Cells("type_liseuse").Value?.ToString()

            If Not String.IsNullOrWhiteSpace(txtIdContact.Text) Then
                _currentId = Convert.ToUInt64(txtIdContact.Text)
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur binding contact sélectionné.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            Throw

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 07/03/2026
    ' dgvContacts_SelectionChanged
    '
    '   • Synchronise le panneau détail avec la ligne sélectionnée
    '   • Inactif pendant les modes Nouveau et Modification
    '------------------------------------------------------------
    Private Sub dgvContacts_SelectionChanged(sender As Object, e As EventArgs) Handles dgvContacts.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub

        BindSelectedContactToDetails()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 09/03/2026
    ' ReselectContactByNom
    '
    '   • Recherche dans la grille le contact correspondant au nom fourni
    '   • Repositionne la sélection après un enregistrement
    '   • Bascule sur la première ligne si le contact n'est plus retrouvé
    '------------------------------------------------------------
    Private Sub ReselectContactByNom(nomContact As String)

        Try

            If String.IsNullOrWhiteSpace(nomContact) Then Exit Sub
            If dgvContacts.Rows.Count = 0 Then Exit Sub

            For Each row As DataGridViewRow In dgvContacts.Rows

                If row.IsNewRow Then Continue For

                Dim valeur As String = If(row.Cells("nom_contact").Value, "").ToString().Trim()

                If String.Equals(valeur, nomContact.Trim(), StringComparison.OrdinalIgnoreCase) Then

                    dgvContacts.ClearSelection()
                    row.Selected = True

                    If dgvContacts.Columns.Contains("nom_contact") Then
                        dgvContacts.CurrentCell = row.Cells("nom_contact")
                    End If

                    BindSelectedContactToDetails()
                    Exit Sub

                End If

            Next

            If dgvContacts.Rows.Count > 0 Then
                dgvContacts.ClearSelection()
                dgvContacts.Rows(0).Selected = True
                BindSelectedContactToDetails()
            Else
                ClearContactDetails()
                _currentId = 0
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                $"UI: erreur repositionnement contact '{nomContact}'.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' BuildContactFromFields
    '
    '   • Construit un objet Contact à partir des champs de saisie
    '   • Centralise la conversion des valeurs UI vers le modèle
    '------------------------------------------------------------
    Private Function BuildContactFromFields() As Contact

        Return New Contact With {
            .IdContact = _currentId,
            .NomContact = txtNomContact.Text.Trim(),
            .EmailPerso = txtEmailPerso.Text.Trim(),
            .AdresseLiseuse = txtAdresseLiseuse.Text.Trim(),
            .TypeLiseuse = txtTypeLiseuse.Text.Trim()
        }

    End Function

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ValidateForm
    '
    '   • Vérifie les règles minimales de validité du formulaire
    '   • Pose les erreurs sur les contrôles concernés
    '   • Retourne True si la saisie est exploitable pour un enregistrement
    '------------------------------------------------------------
    Private Function ValidateForm() As Boolean

        If String.IsNullOrWhiteSpace(txtNomContact.Text) Then
            errProvider.SetError(txtNomContact, "Le nom du contact est obligatoire.")
            txtNomContact.Focus()
            Return False
        End If

        If txtEmailPerso.Text.Trim() <> "" AndAlso Not UtilsForm.IsValidEmail(txtEmailPerso.Text.Trim()) Then
            errProvider.SetError(txtEmailPerso, "Adresse email invalide.")
            txtEmailPerso.Focus()
            Return False
        End If

        If txtAdresseLiseuse.Text.Trim() <> "" AndAlso Not UtilsForm.IsValidEmail(txtAdresseLiseuse.Text.Trim()) Then
            errProvider.SetError(txtAdresseLiseuse, "Adresse liseuse invalide.")
            txtAdresseLiseuse.Focus()
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.0 - 10/03/2026
    ' txtSearch_KeyDown
    '
    '   • Permet de lancer la recherche avec la touche Entrée
    '------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ApplySearch()
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 10/03/2026
    ' btnSearch_Click
    '
    '   • Lance explicitement la recherche à partir du texte saisi
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 10/03/2026
    ' btnClearSearch_Click
    '
    '   • Efface le texte de recherche puis recharge la liste complète
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()
        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 10/03/2026
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
                dt = GestionReferentiel.Contact_GetAll()
                SetStatus("Liste complète affichée.")
            Else
                dt = GestionReferentiel.Contact_GetBySearch(searchText)
                SetStatus($"Filtre appliqué : '{searchText}'")
            End If

            dgvContacts.DataSource = dt
            UtilsForm.FormatReferentielGrid(dgvContacts)

            If dgvContacts.Columns.Contains("id_contact") Then
                dgvContacts.Columns("id_contact").Visible = False
            End If

            If dgvContacts.Columns.Contains("created_at") Then
                dgvContacts.Columns("created_at").Visible = False
            End If

            If dgvContacts.Columns.Contains("updated_at") Then
                dgvContacts.Columns("updated_at").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} contact(s)"

            If UtilsForm.SelectFirstRow(dgvContacts, "nom_contact") Then
                BindSelectedContactToDetails()
            Else
                ClearContactDetails()
                _currentId = 0
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
                "UI: erreur ApplySearch (Contact).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )

            SetStatus("Erreur lors de la recherche.")

        End Try

    End Sub

#End Region

End Class