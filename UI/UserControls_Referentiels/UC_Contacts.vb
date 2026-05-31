'====================================================================
' 📌 UC_Contacts.vb
' Version : V1.0
' Date    : 23/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion du référentiel Contacts.
' Transposé depuis GestionContacts.vb
'
' Règles :
' - Implémente IContextAwareUserControl
' - Utilise UserControlContext pour StatusStrip, ErrorProvider, ToolTip
' - Logique métier identique à la Form d'origine
' - Support double-clic sur emails pour ouverture client mail
'
' Évolution :
' - V1.0 : Transposition depuis GestionContacts.vb
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_Contacts
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'utilisation
    Private _mode As ModeEdition = ModeEdition.Consultation

    ' Identifiant du contact courant
    Private _currentId As ULong = 0

    ' Contexte partagé
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
                _context.NavigateToLevel("Contacts")
                _context.SetStatus($"Contacts chargés : {dgvContacts.Rows.Count} élément(s).")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_Contacts.",
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
                "UI: erreur désactivation UC_Contacts.",
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
    ' UC_Contacts_Load
    '
    '   • Initialise le UserControl au chargement
    '------------------------------------------------------------
    Private Sub UC_Contacts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
                "UI: erreur chargement UC_Contacts.",
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
    ' 📌 V1.0 - 23/03/2026
    ' InitToolTips
    '
    '   • Configure les info-bulles pour tous les boutons et contrôles
    '------------------------------------------------------------
    Private Sub InitToolTips()

        If _context?.ToolTip Is Nothing Then Return

        _context.ToolTip.SetToolTip(btnSearch, "Appliquer le filtre")
        _context.ToolTip.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        _context.ToolTip.SetToolTip(btnNew, "Créer un nouveau contact")
        _context.ToolTip.SetToolTip(btnEdit, "Passer en mode modification")
        _context.ToolTip.SetToolTip(btnSave, "Enregistrer les modifications")
        _context.ToolTip.SetToolTip(btnCancel, "Annuler les modifications")
        _context.ToolTip.SetToolTip(btnDelete, "Supprimer le contact sélectionné")
        _context.ToolTip.SetToolTip(txtEmailPerso, "Double-cliquer pour envoyer un email")
        _context.ToolTip.SetToolTip(txtAdresseLiseuse, "Double-cliquer pour envoyer un email")

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

        Dim hasSelection As Boolean = HasSelectedContact()

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

                ' Grille et recherche actives
                dgvContacts.Enabled = True
                pnlTop.Enabled = True

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

                ' Grille et recherche désactivées
                dgvContacts.Enabled = False
                pnlTop.Enabled = False

        End Select

        UpdateEmailFieldStyle()

        ' Mettre à jour le fil d'Ariane avec le mode
        If _context IsNot Nothing Then
            Select Case mode
                Case ModeEdition.Consultation
                    _context.NavigateToLevel("Contacts")
                Case ModeEdition.Nouveau
                    _context.SetMode("Nouveau")
                Case ModeEdition.Modification
                    _context.SetMode("Modification")
            End Select
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' HasSelectedContact
    '
    '   • Retourne True si un contact est actuellement sélectionné
    '------------------------------------------------------------
    Private Function HasSelectedContact() As Boolean
        Return dgvContacts.CurrentRow IsNot Nothing AndAlso dgvContacts.Rows.Count > 0
    End Function

#End Region

#Region "Interface utilisateur"

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

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' UpdateEmailFieldStyle
    '
    '   • Applique le style visuel des champs email
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
    ' 📌 V1.0 - 23/03/2026
    ' txtEmailPerso_DoubleClick
    '
    '   • Ouvre le client mail pour l'adresse personnelle
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
    ' 📌 V1.0 - 23/03/2026
    ' txtEmailPerso_TextChanged
    '
    '   • Met à jour le curseur et le comportement visuel du champ email personnel
    '------------------------------------------------------------
    Private Sub txtEmailPerso_TextChanged(sender As Object, e As EventArgs) Handles txtEmailPerso.TextChanged
        UtilsForm.UpdateEmailTextBoxCursor(txtEmailPerso)
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' txtAdresseLiseuse_DoubleClick
    '
    '   • Ouvre le client mail pour l'adresse liseuse
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
    ' 📌 V1.0 - 23/03/2026
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
    ' 📌 V1.0 - 23/03/2026
    ' btnNew_Click
    '
    '   • Crée un nouveau contact
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
    ' 📌 V1.0 - 23/03/2026
    ' btnEdit_Click
    '
    '   • Modifie un contact existant
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try
            ClearErrors()

            If Not HasSelectedContact() Then
                SetStatus("Aucun contact sélectionné.")
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(txtIdContact.Text) Then
                SetStatus("Sélection invalide.")
                Exit Sub
            End If

            _currentId = SafeULong(txtIdContact.Text)

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
    ' 📌 V1.0 - 23/03/2026
    ' btnSave_Click
    '
    '   • Enregistre le contact (création ou modification)
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            ClearErrors()

            If Not ValidateForm() Then Exit Sub

            Dim contact As Contact = BuildContactFromFields()

            If _mode = ModeEdition.Nouveau Then

                If GestionReferentiel.Contact_ExistsByNom(contact.NomContact) Then
                    _context?.ErrorProvider.SetError(txtNomContact, "Ce nom de contact existe déjà.")
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

                contact.IdContact = _currentId
                GestionReferentiel.Contact_Update(contact)

                GestionLog.EcrireLog(
                    $"UI: modification contact '{contact.NomContact}'.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.UI
                )

                SetStatus("Contact modifié.")

            End If

            LoadGrid()
            SelectContactById(_currentId)

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur enregistrement contact.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
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

        Try
            ClearErrors()

            If _mode = ModeEdition.Nouveau Then
                BindSelectedToDetails()
            ElseIf _mode = ModeEdition.Modification Then
                BindSelectedToDetails()
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Annulation.")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur annulation contact.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnDelete_Click
    '
    '   • Supprime le contact sélectionné
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try
            If _mode <> ModeEdition.Consultation Then
                SetStatus("Suppression impossible pendant une édition.")
                Return
            End If

            If Not HasSelectedContact() Then
                SetStatus("Aucun contact sélectionné.")
                Return
            End If

            Dim id As ULong = SafeULong(txtIdContact.Text)
            Dim nomContact As String = txtNomContact.Text

            Dim rep = MessageBox.Show(
                $"Supprimer le contact '{nomContact}' ?",
                "Confirmation suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            )

            If rep <> DialogResult.Yes Then
                SetStatus("Suppression annulée.")
                Return
            End If

            GestionReferentiel.Contact_Delete(id)

            GestionLog.EcrireLog(
                $"UI: suppression contact '{nomContact}'.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI
            )

            SetStatus("Contact supprimé.")

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur suppression contact.",
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
    '   • Charge et affiche la liste complète des contacts
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try
            Dim dt = GestionReferentiel.Contact_GetAll()
            dgvContacts.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvContacts)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvContacts, "id_contact", "code_contact", "date_creation", "date_modification", "created", "updated")

            ' Largeurs colonnes
            If dgvContacts.Columns.Contains("nom_contact") Then dgvContacts.Columns("nom_contact").Width = 200
            If dgvContacts.Columns.Contains("email_perso") Then dgvContacts.Columns("email_perso").Width = 280
            If dgvContacts.Columns.Contains("adresse_liseuse") Then dgvContacts.Columns("adresse_liseuse").Width = 280
            If dgvContacts.Columns.Contains("type_liseuse") Then dgvContacts.Columns("type_liseuse").Width = 150

            ' Sélection première ligne
            If dgvContacts.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvContacts, "nom_contact")
                BindSelectedToDetails()
            Else
                ClearContactDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvContacts, "contact")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid (UC_Contacts).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement de la grille.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BindSelectedToDetails
    '
    '   • Affiche les détails du contact sélectionné dans la grille
    '------------------------------------------------------------
    Private Sub BindSelectedToDetails()

        Try
            If dgvContacts.CurrentRow Is Nothing Then
                ClearContactDetails()
                Return
            End If

            Dim row = dgvContacts.CurrentRow

            txtIdContact.Text = If(row.Cells("id_contact")?.Value?.ToString(), "")
            txtCodeContact.Text = If(row.Cells("code_contact")?.Value?.ToString(), "")
            txtNomContact.Text = If(row.Cells("nom_contact")?.Value?.ToString(), "")
            txtEmailPerso.Text = If(row.Cells("email_perso")?.Value?.ToString(), "")
            txtAdresseLiseuse.Text = If(row.Cells("adresse_liseuse")?.Value?.ToString(), "")
            txtTypeLiseuse.Text = If(row.Cells("type_liseuse")?.Value?.ToString(), "")

            _currentId = SafeULong(txtIdContact.Text)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur BindSelectedToDetails (UC_Contacts).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearContactDetails
    '
    '   • Vide tous les champs de détails
    '------------------------------------------------------------
    Private Sub ClearContactDetails()

        txtIdContact.Text = ""
        txtCodeContact.Text = ""
        txtNomContact.Text = ""
        txtEmailPerso.Text = ""
        txtAdresseLiseuse.Text = ""
        txtTypeLiseuse.Text = ""

        _currentId = 0

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' dgvContacts_SelectionChanged
    '
    '   • Met à jour les détails lors du changement de sélection
    '------------------------------------------------------------
    Private Sub dgvContacts_SelectionChanged(sender As Object, e As EventArgs) Handles dgvContacts.SelectionChanged
        If _mode = ModeEdition.Consultation Then
            BindSelectedToDetails()
            SetMode(ModeEdition.Consultation) ' Refresh buttons
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SelectContactById
    '
    '   • Sélectionne le contact par son ID dans la grille
    '------------------------------------------------------------
    Private Sub SelectContactById(id As ULong)
        Try
            If id > 0 Then
                UtilsForm.DgvSelectRowById(dgvContacts, "id_contact", id)
                BindSelectedToDetails()
            End If
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur SelectContactById (UC_Contacts).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try
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

        ' Nom contact obligatoire
        If String.IsNullOrWhiteSpace(txtNomContact.Text) Then
            _context?.ErrorProvider.SetError(txtNomContact, "Le nom du contact est obligatoire.")
            isValid = False
        End If

        ' Email personnel optionnel mais si rempli, doit être valide
        If Not String.IsNullOrWhiteSpace(txtEmailPerso.Text) Then
            If Not UtilsForm.IsValidEmail(txtEmailPerso.Text) Then
                _context?.ErrorProvider.SetError(txtEmailPerso, "Format d'email invalide.")
                isValid = False
            End If
        End If

        ' Adresse liseuse optionnelle mais si remplie, doit être valide
        If Not String.IsNullOrWhiteSpace(txtAdresseLiseuse.Text) Then
            If Not UtilsForm.IsValidEmail(txtAdresseLiseuse.Text) Then
                _context?.ErrorProvider.SetError(txtAdresseLiseuse, "Format d'email invalide.")
                isValid = False
            End If
        End If

        If Not isValid Then
            SetStatus("Formulaire incomplet ou invalide.")
        End If

        Return isValid

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BuildContactFromFields
    '
    '   • Construit un objet Contact depuis les champs de saisie
    '------------------------------------------------------------
    Private Function BuildContactFromFields() As Contact

        Return New Contact With {
            .IdContact = SafeULong(txtIdContact.Text),
            .NomContact = txtNomContact.Text.Trim(),
            .EmailPerso = txtEmailPerso.Text.Trim(),
            .AdresseLiseuse = txtAdresseLiseuse.Text.Trim(),
            .TypeLiseuse = txtTypeLiseuse.Text.Trim()
        }

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
                        GestionReferentiel.Contact_GetAll(),
                        GestionReferentiel.Contact_GetBySearch(searchText))

            dgvContacts.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvContacts)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvContacts, "id_contact", "code_contact", "date_creation", "date_modification", "created", "updated")

            ' Largeurs colonnes
            If dgvContacts.Columns.Contains("nom_contact") Then dgvContacts.Columns("nom_contact").Width = 200
            If dgvContacts.Columns.Contains("email_perso") Then dgvContacts.Columns("email_perso").Width = 280
            If dgvContacts.Columns.Contains("adresse_liseuse") Then dgvContacts.Columns("adresse_liseuse").Width = 280
            If dgvContacts.Columns.Contains("type_liseuse") Then dgvContacts.Columns("type_liseuse").Width = 150

            ' Sélection première ligne
            If dgvContacts.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvContacts, "nom_contact")
                BindSelectedToDetails()
            Else
                ClearContactDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvContacts, "contact")

            SetStatus($"Recherche effectuée : {dgvContacts.Rows.Count} résultat(s).")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch (UC_Contacts).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
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
    '   • Efface le filtre et recharge tous les contacts
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

#End Region

End Class
