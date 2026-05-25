'------------------------------------------------------------
' 📌 GestionPrixLit.vb
' Version : V1.2
' Date    : 26/03/2026
'
' Rôle :
' Formulaire WinForms de gestion du référentiel hiérarchique
' PrixLit parent /Categorie prix liés /annee.
'
' Règles Artefact :
' - Pas de SQL ici (QueryModule).
' - Pas d'accès DB direct ici (GestionReferentiel).
' - Validation UI : errProvider + StatusStrip.
' - Modèle à 3 niveaux : PrixLit parent /Categorie prix liés /année .
'
' Évolution :
' - V1.0 : création de la structure initiale sur le modèle GestionRecommandation
' - V1.1 : Correction du bug de synchronisation
' - V1.2 : Correction tabAnnee
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionPrixLit

#Region "Déclarations"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' Variables privées
    '
    '   • _mode : mode courant du formulaire
    '   • _dtPrixLit / _dtCategories / _dtAnnees : sources de données
    '------------------------------------------------------------
    Private _mode As ModeEdition = ModeEdition.Consultation

    Private _dtPrixLit As DataTable
    Private _dtCategories As DataTable
    Private _dtAnnees As DataTable

    Private _snapshotPrixLit As PrixLit = Nothing
    Private _snapshotPrixLitCategorie As PrixLitCategorie = Nothing
    Private _snapshotPrixLitAnnee As PrixLitAnnee = Nothing

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' PrixLitContext
    '
    '   • Détermine le contexte courant selon l'onglet actif
    '------------------------------------------------------------
    Private Enum PrixLitContext
        PrixLit
        Categories
        Annees
    End Enum

#End Region


#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.1- 24/03/2026
    ' GestionPrixLit_Load
    '
    '   • Initialise complètement le formulaire
    '   •	Charge tous les niveaux hiérarchiques
    '   •	Configure les RichTextBox
    '------------------------------------------------------------
    Private Sub GestionPrixLit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            InitTooltips()
            ClearErrors()

            ' Notes enrichies
            RichTextNotesHelper.ConfigurerRichTextBoxNotes(rtbNotesPrixLit)

            ChargerFiltrePrixLit()
            ChargerComboPrixLitParentCategorie()
            ChargerPrixLit()
            ChargerCategories()
            ChargerComboPrixLitCategorieAnnee()
            ChargerAnnees()

            SetMode(ModeEdition.Consultation)
            SetStatus("Prix littéraires chargés.")

            nudAnneePrixLit.Value = Date.Today.Year


        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur chargement GestionPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors du chargement.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' InitTooltips
    '
    '   • Configure les info-bulles
    '------------------------------------------------------------
    Private Sub InitTooltips()

        If ttMain Is Nothing Then Return

        'Boutons
        ttMain.SetToolTip(btnNew, "Créer un nouvel élément")
        ttMain.SetToolTip(btnEdit, "Modifier l'élément sélectionné")
        ttMain.SetToolTip(btnSave, "Enregistrer")
        ttMain.SetToolTip(btnCancel, "Annuler")
        ttMain.SetToolTip(btnDelete, "Supprimer")
        ttMain.SetToolTip(btnSearch, "Rechercher")
        ttMain.SetToolTip(btnClearSearch, "Effacer la recherche")

        ' PrixLit
        ttMain.SetToolTip(txtNomPrixLit, "Nom du prix littéraire")
        ttMain.SetToolTip(txtDescriptionPrixLit, "Description")
        ttMain.SetToolTip(rtbNotesPrixLit, "Notes enrichies")

        ' Catégorie
        ttMain.SetToolTip(txtLibelleCategorie, "Libellé de la catégorie")
        ttMain.SetToolTip(txtDescriptionCategorie, "Description")
        ttMain.SetToolTip(nudOrdreAffichageCategorie, "Ordre d'affichage")

        ' Année
        ttMain.SetToolTip(nudAnneePrixLit, "Année du prix")
        ttMain.SetToolTip(txtCategorieParentAnnee, "Catégorie actuellement sélectionnée")
        ttMain.SetToolTip(cboPrixLitCategorieAnnee, "Catégorie de l'année")

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' GetCurrentContext
    '
    '   • Retourne le contexte courant selon l'onglet actif
    '------------------------------------------------------------
    Private Function GetCurrentContext() As PrixLitContext

        If tabMain.SelectedTab Is tabPrixLit Then
            Return PrixLitContext.PrixLit
        End If

        If tabMain.SelectedTab Is tabPrixLitCategorie Then
            Return PrixLitContext.Categories
        End If

        Return PrixLitContext.Annees

    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 24/03/2026
    ' ChargerFiltrePrixLit
    '
    '   • Alimente le combo filtre UI des prix littéraires
    '   • Ajoute une entrée sentinelle [Tous les prix]
    '------------------------------------------------------------
    Private Sub ChargerFiltrePrixLit()

        Dim dt As DataTable = GestionReferentiel.PrixLit_GetAll(False)

        Dim dtFiltre As DataTable = dt.Copy()

        Dim rowAll As DataRow = dtFiltre.NewRow()
        rowAll("id_prixLit") = 0UL
        rowAll("nom_prixLit") = "[Tous les prix]"
        dtFiltre.Rows.InsertAt(rowAll, 0)

        cboFiltrePrixLit.DataSource = dtFiltre
        cboFiltrePrixLit.DisplayMember = "nom_prixLit"
        cboFiltrePrixLit.ValueMember = "id_prixLit"
        cboFiltrePrixLit.SelectedIndex = 0

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ChargerComboPrixLitParentCategorie
    '
    '   • Alimente le combo métier des catégories
    '   • Ne contient aucune valeur sentinelle
    '------------------------------------------------------------
    Private Sub ChargerComboPrixLitParentCategorie()

        Dim dt As DataTable = GestionReferentiel.PrixLit_GetAll(False)

        cboPrixLitParentCategorie.DataSource = dt
        cboPrixLitParentCategorie.DisplayMember = "nom_prixLit"
        cboPrixLitParentCategorie.ValueMember = "id_prixLit"
        cboPrixLitParentCategorie.SelectedIndex = -1

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ChargerComboPrixLitCategorieAnnee
    '
    '   • Alimente le combo métier des catégories pour l'onglet Année
    '   • Dépend du prix courant (filtre UI ou sélection courante)
    '------------------------------------------------------------
    Private Sub ChargerComboPrixLitCategorieAnnee()

        Dim idPrixLit As ULong = 0UL

        If cboFiltrePrixLit.SelectedValue IsNot Nothing AndAlso IsNumeric(cboFiltrePrixLit.SelectedValue) Then
            idPrixLit = Convert.ToUInt64(cboFiltrePrixLit.SelectedValue)
        End If

        If idPrixLit = 0UL AndAlso dgvPrixLit.CurrentRow IsNot Nothing Then
            idPrixLit = Convert.ToUInt64(dgvPrixLit.CurrentRow.Cells("id_prixLit").Value)
        End If

        If idPrixLit = 0UL Then
            cboPrixLitCategorieAnnee.DataSource = Nothing
            txtPrixLitParentAnnee.Clear()
            Exit Sub
        End If

        Dim dt As DataTable = GestionReferentiel.PrixLitCategorie_GetByPrixLit(idPrixLit)

        cboPrixLitCategorieAnnee.DataSource = dt
        cboPrixLitCategorieAnnee.DisplayMember = "libelle_categorie"
        cboPrixLitCategorieAnnee.ValueMember = "id_prixlit_categorie"
        cboPrixLitCategorieAnnee.SelectedIndex = -1

        If dgvPrixLit.CurrentRow IsNot Nothing Then
            txtPrixLitParentAnnee.Text = dgvPrixLit.CurrentRow.Cells("nom_prixLit").Value.ToString()
        End If

    End Sub

#End Region


#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' SetMode
    '
    '   • Définit le mode du formulaire
    '   • Adapte l’UI selon :
    '       - le mode (Consult / New / Edit)
    '       - le contexte courant (Prix / Catégorie / Année)
    '------------------------------------------------------------
    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Dim context As PrixLitContext = GetCurrentContext()

        Select Case mode

            Case ModeEdition.Consultation

                EnableControls(False)
                EnableButtonsConsultation()

                Select Case context
                    Case PrixLitContext.PrixLit
                        SetStatus("Consultation des prix littéraires")
                    Case PrixLitContext.Categories
                        SetStatus("Consultation des catégories")
                    Case PrixLitContext.Annees
                        SetStatus("Consultation des années")
                        txtCategorieParentAnnee.Visible = True
                        lblCategorieParentAnnee.Visible = True

                        cboPrixLitCategorieAnnee.Visible = False
                End Select


            Case ModeEdition.Nouveau

                EnableControls(True)
                EnableButtonsEdition()

                ClearCurrentDetails()

                Select Case context
                    Case PrixLitContext.PrixLit
                        SetStatus("Création d’un prix littéraire")
                    Case PrixLitContext.Categories
                        SetStatus("Création d’une catégorie")
                    Case PrixLitContext.Annees
                        SetStatus("Création d’une année")
                        txtCategorieParentAnnee.Visible = False
                        lblCategorieParentAnnee.Visible = False

                        cboPrixLitCategorieAnnee.Visible = True
                End Select

            Case ModeEdition.Modification

                EnableControls(True)
                EnableButtonsEdition()

                Select Case context
                    Case PrixLitContext.PrixLit
                        SetStatus("Modification d’un prix littéraire")
                    Case PrixLitContext.Categories
                        SetStatus("Modification d’une catégorie")
                    Case PrixLitContext.Annees
                        SetStatus("Modification d’une année")
                        txtCategorieParentAnnee.Visible = False
                        lblCategorieParentAnnee.Visible = False

                        cboPrixLitCategorieAnnee.Visible = True
                End Select

        End Select

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' EnableControls
    '
    '   • Active / désactive les champs selon le contexte
    '------------------------------------------------------------
    Private Sub EnableControls(enable As Boolean)

        Dim context As PrixLitContext = GetCurrentContext()

        Select Case context

            Case PrixLitContext.PrixLit

                txtNomPrixLit.ReadOnly = Not enable
                txtDescriptionPrixLit.ReadOnly = Not enable
                chkPrixLitActif.Enabled = enable
                rtbNotesPrixLit.ReadOnly = Not enable

            Case PrixLitContext.Categories

                cboPrixLitParentCategorie.Enabled = enable
                txtLibelleCategorie.ReadOnly = Not enable
                txtDescriptionCategorie.ReadOnly = Not enable
                nudOrdreAffichageCategorie.Enabled = enable
                chkPrixLitCategorieActif.Enabled = enable

            Case PrixLitContext.Annees

                cboPrixLitCategorieAnnee.Enabled = enable
                nudAnneePrixLit.Enabled = enable

        End Select

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' EnableButtonsConsultation
    '
    '   • Etat des boutons en mode consultation
    '------------------------------------------------------------
    Private Sub EnableButtonsConsultation()

        btnNew.Enabled = True
        btnEdit.Enabled = True
        btnDelete.Enabled = True

        btnSave.Enabled = False
        btnCancel.Enabled = False

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' EnableButtonsEdition
    '
    '   • Etat des boutons en mode ajout / modification
    '------------------------------------------------------------
    Private Sub EnableButtonsEdition()

        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnSave.Enabled = True
        btnCancel.Enabled = True

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearCurrentDetails
    '
    '   • Vide les champs selon le contexte actif
    '------------------------------------------------------------
    Private Sub ClearCurrentDetails()

        Dim context As PrixLitContext = GetCurrentContext()

        Select Case context

            Case PrixLitContext.PrixLit
                ClearPrixLitDetails()

            Case PrixLitContext.Categories
                ClearPrixLitCategorieDetails()

            Case PrixLitContext.Annees
                ClearPrixLitAnneeDetails()

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
    ' 📌 V1.0 - 24/03/2026
    ' SetCurrentCellFirstVisible
    '
    '   • Positionne la cellule courante sur la première cellule visible
    '   • Evite l'erreur CurrentCell sur colonne masquée
    '------------------------------------------------------------
    Private Sub SetCurrentCellFirstVisible(dgv As DataGridView)

        If dgv Is Nothing Then Exit Sub
        If dgv.Rows.Count = 0 Then Exit Sub

        Dim row As DataGridViewRow = dgv.Rows(0)

        For Each cell As DataGridViewCell In row.Cells
            If cell.Visible Then
                dgv.CurrentCell = cell
                Exit Sub
            End If
        Next

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearPrixLitDetails
    '
    '   • Vide les champs PrixLit
    '------------------------------------------------------------
    Private Sub ClearPrixLitDetails()

        txtidPrixLit.Clear()
        txtCodePrixLit.Clear()
        txtNomPrixLit.Clear()
        txtDescriptionPrixLit.Clear()

        chkPrixLitActif.Checked = False
        rtbNotesPrixLit.Clear()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ClearPrixLitCategorieDetails
    '
    '   • Vide les champs PrixLitCategorie
    '------------------------------------------------------------
    Private Sub ClearPrixLitCategorieDetails()

        txtidprixlitcategorie.Clear()
        txtCodePrixLitCategorie.Clear()

        cboPrixLitParentCategorie.SelectedIndex = -1

        txtLibelleCategorie.Clear()
        txtDescriptionCategorie.Clear()

        nudOrdreAffichageCategorie.Value = nudOrdreAffichageCategorie.Minimum
        chkPrixLitCategorieActif.Checked = False

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1- 26/03/2026
    ' ClearPrixLitAnneeDetails
    '
    '   • Vide les champs PrixLitAnnee
    '------------------------------------------------------------
    Private Sub ClearPrixLitAnneeDetails()

        txtIdPrixLitAnnee.Clear()
        txtCodePrixLitAnnee.Clear()

        cboPrixLitCategorieAnnee.SelectedIndex = -1
        txtCategorieParentAnnee.Clear()
        nudAnneePrixLit.Value = nudAnneePrixLit.Minimum

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BindSelectedPrixLitToDetails
    '
    '  Transfère les données de la ligne sélectionnée vers les champs détails
    '------------------------------------------------------------
    Private Sub BindSelectedPrixLitToDetails()

        If dgvPrixLit.CurrentRow Is Nothing Then Exit Sub

        Dim row = dgvPrixLit.CurrentRow

        txtidPrixLit.Text = row.Cells("id_prixLit").Value.ToString()
        txtCodePrixLit.Text = row.Cells("code_prixLit").Value.ToString()
        txtNomPrixLit.Text = row.Cells("nom_prixLit").Value.ToString()
        txtDescriptionPrixLit.Text = row.Cells("description_prixLit").Value.ToString()

        chkPrixLitActif.Checked = Convert.ToBoolean(row.Cells("is_actif").Value)

        RichTextNotesHelper.ChargerContenuNotes(
            rtbNotesPrixLit,
            row.Cells("Notes_PrixLit_rtf").Value.ToString(),
            row.Cells("Notes_PrixLit_txt").Value.ToString()
        )

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BindSelectedPrixLitCategorieToDetails

    '  Transfère les données de la ligne sélectionnée vers les champs détails
    '------------------------------------------------------------
    Private Sub BindSelectedPrixLitCategorieToDetails()

        If dgvPrixLitCategorie.CurrentRow Is Nothing Then Exit Sub

        Dim row = dgvPrixLitCategorie.CurrentRow

        txtidprixlitcategorie.Text = row.Cells("id_prixlit_categorie").Value.ToString()
        txtCodePrixLitCategorie.Text = row.Cells("code_prixlit_categorie").Value.ToString()

        cboPrixLitParentCategorie.SelectedValue = Convert.ToUInt64(row.Cells("id_prixLit").Value)

        txtLibelleCategorie.Text = row.Cells("libelle_categorie").Value.ToString()
        txtDescriptionCategorie.Text = row.Cells("description_categorie").Value.ToString()

        nudOrdreAffichageCategorie.Value = Convert.ToDecimal(row.Cells("ordre_affichage").Value)
        chkPrixLitCategorieActif.Checked = Convert.ToBoolean(row.Cells("is_actif").Value)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 26/03/2026
    ' BindSelectedPrixLitAnneeToDetails
    '
    '  Transfère les données de la ligne sélectionnée vers les champs détails
    '------------------------------------------------------------
    Private Sub BindSelectedPrixLitAnneeToDetails()

        If dgvPrixLitAnnee.CurrentRow Is Nothing Then Exit Sub

        Dim row = dgvPrixLitAnnee.CurrentRow

        txtIdPrixLitAnnee.Text = row.Cells("id_prixLit_Annee").Value.ToString()
        txtCodePrixLitAnnee.Text = row.Cells("code_prixLit_Annee").Value.ToString()
        txtCategorieParentAnnee.Text = row.Cells("libelle_categorie").Value.ToString()
        cboPrixLitCategorieAnnee.SelectedValue = Convert.ToUInt64(row.Cells("id_prixlit_categorie").Value)
        nudAnneePrixLit.Value = Convert.ToDecimal(row.Cells("annee").Value)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' BindCurrentCategorieContextToAnneeDetails
    '
    '   • Affiche le contexte parent de l'onglet Année
    '   • Utilisé lorsque la grille Année est vide
    '------------------------------------------------------------
    Private Sub BindCurrentCategorieContextToAnneeDetails()

        If dgvPrixLitCategorie.CurrentRow Is Nothing Then
            txtPrixLitParentAnnee.Clear()
            txtCategorieParentAnnee.Clear()
            cboPrixLitCategorieAnnee.SelectedIndex = -1
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgvPrixLitCategorie.CurrentRow

        If dgvPrixLit.CurrentRow IsNot Nothing Then
            txtPrixLitParentAnnee.Text = dgvPrixLit.CurrentRow.Cells("nom_prixLit").Value.ToString()
        Else
            txtPrixLitParentAnnee.Clear()
        End If

        txtCategorieParentAnnee.Text = row.Cells("libelle_categorie").Value.ToString()

        If row.Cells("id_prixlit_categorie").Value IsNot Nothing AndAlso row.Cells("id_prixlit_categorie").Value IsNot DBNull.Value Then
            cboPrixLitCategorieAnnee.SelectedValue = Convert.ToUInt64(row.Cells("id_prixlit_categorie").Value)
        Else
            cboPrixLitCategorieAnnee.SelectedIndex = -1
        End If

    End Sub

#End Region


#Region "Actions utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.1 - 26/03/2026
    ' btnNew_Click
    '
    '   • Passe en mode Nouveau selon l'onglet actif
    '   • Prépare les champs détail du contexte courant
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try

            ClearErrors()

            Select Case GetCurrentContext()

                Case PrixLitContext.PrixLit

                    _snapshotPrixLit = Nothing
                    SetMode(ModeEdition.Nouveau)

                    ClearPrixLitDetails()
                    chkPrixLitActif.Checked = True
                    txtNomPrixLit.Focus()


                Case PrixLitContext.Categories

                    If dgvPrixLit.CurrentRow Is Nothing Then
                        SetStatus("Sélectionnez d'abord un prix littéraire.")
                        Exit Sub
                    End If

                    _snapshotPrixLitCategorie = Nothing
                    SetMode(ModeEdition.Nouveau)

                    ClearPrixLitCategorieDetails()

                    cboPrixLitParentCategorie.SelectedValue = Convert.ToUInt64(dgvPrixLit.CurrentRow.Cells("id_prixLit").Value)
                    chkPrixLitCategorieActif.Checked = True
                    txtLibelleCategorie.Focus()


                Case PrixLitContext.Annees

                    If dgvPrixLitCategorie.CurrentRow Is Nothing Then
                        SetStatus("Sélectionnez d'abord une catégorie.")
                        Exit Sub
                    End If

                    _snapshotPrixLitAnnee = Nothing
                    SetMode(ModeEdition.Nouveau)

                    ClearPrixLitAnneeDetails()

                    ClearPrixLitAnneeDetails()
                    BindCurrentCategorieContextToAnneeDetails()

                    If dgvPrixLitCategorie.CurrentRow IsNot Nothing Then
                        cboPrixLitCategorieAnnee.SelectedValue = Convert.ToUInt64(dgvPrixLitCategorie.CurrentRow.Cells("id_prixlit_categorie").Value)
                    End If

                    nudAnneePrixLit.Value = Date.Today.Year
                    nudAnneePrixLit.Focus()

            End Select

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnNew_Click GestionPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la préparation du nouvel élément.")
        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnEdit_Click
    '
    '   • Passe en mode Modification selon l'onglet actif
    '   • Crée un snapshot de l'objet courant
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try

            ClearErrors()

            Select Case GetCurrentContext()

                Case PrixLitContext.PrixLit

                    If dgvPrixLit.CurrentRow Is Nothing Then
                        SetStatus("Aucun prix littéraire sélectionné.")
                        Exit Sub
                    End If

                    _snapshotPrixLit = BuildPrixLitFromForm()
                    SetMode(ModeEdition.Modification)
                    txtNomPrixLit.Focus()


                Case PrixLitContext.Categories

                    If dgvPrixLitCategorie.CurrentRow Is Nothing Then
                        SetStatus("Aucune catégorie sélectionnée.")
                        Exit Sub
                    End If

                    _snapshotPrixLitCategorie = BuildPrixLitCategorieFromForm()
                    SetMode(ModeEdition.Modification)
                    txtLibelleCategorie.Focus()


                Case PrixLitContext.Annees

                    If dgvPrixLitAnnee.CurrentRow Is Nothing Then
                        SetStatus("Aucune année sélectionnée.")
                        Exit Sub
                    End If

                    _snapshotPrixLitAnnee = BuildPrixLitAnneeFromForm()
                    SetMode(ModeEdition.Modification)
                    nudAnneePrixLit.Focus()

            End Select

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnEdit_Click GestionPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du passage en modification.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnSave_Click
    '
    '   • Enregistre l'élément courant selon le contexte
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            ClearErrors()

            If Not ValidateCurrentForm() Then
                SetStatus("Le formulaire contient des erreurs.")
                Exit Sub
            End If

            Select Case GetCurrentContext()

            '====================================================
            ' PRIX LITTERAIRE
            '====================================================
                Case PrixLitContext.PrixLit

                    Dim p As PrixLit = BuildPrixLitFromForm()

                    Dim idSaved As ULong

                    If _mode = ModeEdition.Nouveau Then
                        idSaved = GestionReferentiel.PrixLit_Insert(p)
                        SetStatus("Prix littéraire créé.")
                    Else
                        GestionReferentiel.PrixLit_Update(p)
                        idSaved = p.IdPrixLit
                        SetStatus("Prix littéraire modifié.")
                    End If

                    ChargerPrixLit()
                    ChargerFiltrePrixLit()
                    ChargerComboPrixLitParentCategorie()

                    If idSaved > 0 Then
                        DgvSelectRowById(dgvPrixLit, "id_prixLit", idSaved)
                        cboFiltrePrixLit.SelectedValue = idSaved
                    End If

                    ChargerCategories()
                    ChargerComboPrixLitCategorieAnnee()
                    ChargerAnnees()
                    SetMode(ModeEdition.Consultation)


            '====================================================
            ' CATEGORIE
            '====================================================
                Case PrixLitContext.Categories

                    Dim c As PrixLitCategorie = BuildPrixLitCategorieFromForm()

                    Dim idSaved As ULong

                    If _mode = ModeEdition.Nouveau Then
                        idSaved = GestionReferentiel.PrixLitCategorie_Insert(c)
                        SetStatus("Catégorie créée.")
                    Else
                        GestionReferentiel.PrixLitCategorie_Update(c)
                        idSaved = c.IdPrixLitCategorie
                        SetStatus("Catégorie modifiée.")
                    End If

                    ChargerCategories()
                    ChargerComboPrixLitCategorieAnnee()

                    If idSaved > 0 Then
                        DgvSelectRowById(dgvPrixLitCategorie, "id_prixlit_categorie", idSaved)
                        cboPrixLitCategorieAnnee.SelectedValue = idSaved
                    End If

                    ChargerAnnees()
                    SetMode(ModeEdition.Consultation)


            '====================================================
            ' ANNEE
            '====================================================
                Case PrixLitContext.Annees

                    Dim a As PrixLitAnnee = BuildPrixLitAnneeFromForm()

                    Dim idSaved As ULong

                    If _mode = ModeEdition.Nouveau Then
                        idSaved = GestionReferentiel.PrixLitAnnee_Insert(a)
                        SetStatus("Année ajoutée.")
                    Else
                        GestionReferentiel.PrixLitAnnee_Update(a)
                        idSaved = a.IdPrixLitAnnee
                        SetStatus("Année modifiée.")
                    End If

                    ChargerAnnees()

                    If idSaved > 0 Then
                        DgvSelectRowById(dgvPrixLitAnnee, "id_prixLit_Annee", idSaved)
                    End If

                    SetMode(ModeEdition.Consultation)

            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur btnSave_Click GestionPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'enregistrement.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' btnCancel_Click
    '
    '   • Annule l'ajout ou la modification en cours
    '   • Restaure le snapshot si nécessaire
    '------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try

            ClearErrors()

            Select Case _mode

                Case ModeEdition.Nouveau

                    SetMode(ModeEdition.Consultation)

                    Select Case GetCurrentContext()

                        Case PrixLitContext.PrixLit
                            If dgvPrixLit.CurrentRow IsNot Nothing Then
                                BindSelectedPrixLitToDetails()
                            Else
                                ClearPrixLitDetails()
                            End If

                        Case PrixLitContext.Categories
                            If dgvPrixLitCategorie.CurrentRow IsNot Nothing Then
                                BindSelectedPrixLitCategorieToDetails()
                            Else
                                ClearPrixLitCategorieDetails()
                            End If

                        Case PrixLitContext.Annees
                            If dgvPrixLitAnnee.CurrentRow IsNot Nothing Then
                                BindSelectedPrixLitAnneeToDetails()
                            Else
                                ClearPrixLitAnneeDetails()
                            End If

                    End Select

                Case ModeEdition.Modification

                    Select Case GetCurrentContext()

                        Case PrixLitContext.PrixLit
                            If _snapshotPrixLit IsNot Nothing Then
                                RestorePrixLitToForm(_snapshotPrixLit)
                            End If

                        Case PrixLitContext.Categories
                            If _snapshotPrixLitCategorie IsNot Nothing Then
                                RestorePrixLitCategorieToForm(_snapshotPrixLitCategorie)
                            End If

                        Case PrixLitContext.Annees
                            If _snapshotPrixLitAnnee IsNot Nothing Then
                                RestorePrixLitAnneeToForm(_snapshotPrixLitAnnee)
                            End If

                    End Select

                    SetMode(ModeEdition.Consultation)

            End Select

            SetStatus("Modification annulée.")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur btnCancel_Click GestionPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de l'annulation.")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnDelete_Click
    '
    '   • Supprime l'élément courant selon le contexte actif
    '   • Contrôle d'abord les dépendances
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try

            ClearErrors()

            Select Case GetCurrentContext()

            '====================================================
            ' PRIX LITTERAIRE
            '====================================================
                Case PrixLitContext.PrixLit

                    If dgvPrixLit.CurrentRow Is Nothing Then
                        SetStatus("Aucun prix littéraire sélectionné.")
                        Exit Sub
                    End If

                    Dim idPrixLit As ULong = Convert.ToUInt64(txtidPrixLit.Text)
                    Dim nbCategories As Integer = GestionReferentiel.PrixLit_CountCategories(idPrixLit)

                    If nbCategories > 0 Then
                        MessageBox.Show(
                        "Impossible de supprimer ce prix littéraire car il possède encore " & nbCategories.ToString() & " catégorie(s).",
                        "Suppression impossible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )
                        SetStatus("Suppression bloquée : catégories liées.")
                        Exit Sub
                    End If

                    If MessageBox.Show(
                    "Supprimer ce prix littéraire ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) = DialogResult.Yes Then

                        GestionReferentiel.PrixLit_Delete(idPrixLit)

                        ChargerPrixLit()
                        ChargerFiltrePrixLit()
                        ChargerComboPrixLitParentCategorie()
                        ChargerComboPrixLitCategorieAnnee()

                        SetStatus("Prix littéraire supprimé.")
                    End If


            '====================================================
            ' CATEGORIE
            '====================================================
                Case PrixLitContext.Categories

                    If dgvPrixLitCategorie.CurrentRow Is Nothing Then
                        SetStatus("Aucune catégorie sélectionnée.")
                        Exit Sub
                    End If

                    Dim idCategorie As ULong = Convert.ToUInt64(txtidprixlitcategorie.Text)
                    Dim nbAnnees As Integer = GestionReferentiel.PrixLitCategorie_CountAnnees(idCategorie)

                    If nbAnnees > 0 Then
                        MessageBox.Show(
                        "Impossible de supprimer cette catégorie car elle possède encore " & nbAnnees.ToString() & " année(s).",
                        "Suppression impossible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )
                        SetStatus("Suppression bloquée : années liées.")
                        Exit Sub
                    End If

                    If MessageBox.Show(
                    "Supprimer cette catégorie ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) = DialogResult.Yes Then

                        GestionReferentiel.PrixLitCategorie_Delete(idCategorie)

                        ChargerCategories()
                        ChargerComboPrixLitCategorieAnnee()
                        ChargerAnnees()

                        SetStatus("Catégorie supprimée.")
                    End If


            '====================================================
            ' ANNEE
            '====================================================
                Case PrixLitContext.Annees

                    If dgvPrixLitAnnee.CurrentRow Is Nothing Then
                        SetStatus("Aucune année sélectionnée.")
                        Exit Sub
                    End If

                    Dim idAnnee As ULong = Convert.ToUInt64(txtIdPrixLitAnnee.Text)
                    Dim nbLivres As Integer = GestionReferentiel.PrixLitAnnee_CountLivres(idAnnee)

                    If nbLivres > 0 Then
                        MessageBox.Show(
                        "Impossible de supprimer cette année car elle est encore liée à " & nbLivres.ToString() & " livre(s).",
                        "Suppression impossible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )
                        SetStatus("Suppression bloquée : livres liés.")
                        Exit Sub
                    End If

                    If MessageBox.Show(
                    "Supprimer cette année ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) = DialogResult.Yes Then

                        GestionReferentiel.PrixLitAnnee_Delete(idAnnee)

                        ChargerAnnees()

                        SetStatus("Année supprimée.")
                    End If

            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur btnDelete_Click GestionPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub

#End Region


#Region "Actions utilisateur (CRUD)"
    Private Sub btnBold_Click(sender As Object, e As EventArgs) Handles btnBold.Click
        RichTextNotesHelper.BasculerGras(rtbNotesPrixLit)
    End Sub

    Private Sub btnItalic_Click(sender As Object, e As EventArgs) Handles btnItalic.Click
        RichTextNotesHelper.BasculerItalique(rtbNotesPrixLit)
    End Sub

    Private Sub btnUnderline_Click(sender As Object, e As EventArgs) Handles btnUnderline.Click
        RichTextNotesHelper.BasculerSouligne(rtbNotesPrixLit)
    End Sub

    Private Sub btnBullet_Click(sender As Object, e As EventArgs) Handles btnBullet.Click
        RichTextNotesHelper.BasculerListe(rtbNotesPrixLit)
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs) Handles btnTab.Click
        RichTextNotesHelper.InsererTabulation(rtbNotesPrixLit)
    End Sub

#End Region


#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ChargerPrixLit
    '
    '   • Charge les prix littéraires
    '------------------------------------------------------------
    Private Sub ChargerPrixLit()

        Try

            _dtPrixLit = GestionReferentiel.PrixLit_GetAll(False)
            dgvPrixLit.DataSource = _dtPrixLit

            UtilsForm.FormatReferentielGrid(dgvPrixLit)

            If dgvPrixLit.Columns.Contains("id_prixLit") Then dgvPrixLit.Columns("id_prixLit").Visible = False
            If dgvPrixLit.Columns.Contains("created_at") Then dgvPrixLit.Columns("created_at").Visible = False
            If dgvPrixLit.Columns.Contains("updated_at") Then dgvPrixLit.Columns("updated_at").Visible = False
            If dgvPrixLit.Columns.Contains("Notes_PrixLit_txt") Then dgvPrixLit.Columns("Notes_PrixLit_txt").Visible = False
            If dgvPrixLit.Columns.Contains("Notes_PrixLit_rtf") Then dgvPrixLit.Columns("Notes_PrixLit_rtf").Visible = False

            If dgvPrixLit.Columns.Contains("code_prixLit") Then
                dgvPrixLit.Columns("code_prixLit").HeaderText = "Code"
                dgvPrixLit.Columns("code_prixLit").Width = 80
            End If

            If dgvPrixLit.Columns.Contains("nom_prixLit") Then
                dgvPrixLit.Columns("nom_prixLit").HeaderText = "Nom"
                dgvPrixLit.Columns("nom_prixLit").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            If dgvPrixLit.Columns.Contains("description_prixLit") Then
                dgvPrixLit.Columns("description_prixLit").HeaderText = "Description"
                dgvPrixLit.Columns("description_prixLit").Width = 220
            End If

            If dgvPrixLit.Rows.Count > 0 Then
                SetCurrentCellFirstVisible(dgvPrixLit)
                BindSelectedPrixLitToDetails()
            Else
                ClearPrixLitDetails()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ChargerPrixLit.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            Throw
        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' ChargerCategories
    '
    '   • Charge les catégories du prix sélectionné
    '------------------------------------------------------------
    Private Sub ChargerCategories()

        If dgvPrixLit.CurrentRow Is Nothing Then
            dgvPrixLitCategorie.DataSource = Nothing
            ClearPrixLitCategorieDetails()
            Return
        End If

        Try

            Dim idPrix As ULong = Convert.ToUInt64(dgvPrixLit.CurrentRow.Cells("id_prixLit").Value)

            _dtCategories = GestionReferentiel.PrixLitCategorie_GetByPrixLit(idPrix)
            dgvPrixLitCategorie.DataSource = _dtCategories

            UtilsForm.FormatReferentielGrid(dgvPrixLitCategorie)

            If dgvPrixLitCategorie.Columns.Contains("id_prixlit_categorie") Then dgvPrixLitCategorie.Columns("id_prixlit_categorie").Visible = False
            If dgvPrixLitCategorie.Columns.Contains("id_prixLit") Then dgvPrixLitCategorie.Columns("id_prixLit").Visible = False
            If dgvPrixLitCategorie.Columns.Contains("created_at") Then dgvPrixLitCategorie.Columns("created_at").Visible = False
            If dgvPrixLitCategorie.Columns.Contains("updated_at") Then dgvPrixLitCategorie.Columns("updated_at").Visible = False

            If dgvPrixLitCategorie.Columns.Contains("code_prixlit_categorie") Then
                dgvPrixLitCategorie.Columns("code_prixlit_categorie").HeaderText = "Code"
                dgvPrixLitCategorie.Columns("code_prixlit_categorie").Width = 80
            End If

            If dgvPrixLitCategorie.Columns.Contains("libelle_categorie") Then
                dgvPrixLitCategorie.Columns("libelle_categorie").HeaderText = "Libellé"
                dgvPrixLitCategorie.Columns("libelle_categorie").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            If dgvPrixLitCategorie.Columns.Contains("description_categorie") Then
                dgvPrixLitCategorie.Columns("description_categorie").HeaderText = "Description"
                dgvPrixLitCategorie.Columns("description_categorie").Width = 220
            End If

            If dgvPrixLitCategorie.Rows.Count > 0 Then
                SetCurrentCellFirstVisible(dgvPrixLitCategorie)
                BindSelectedPrixLitCategorieToDetails()
            Else
                ClearPrixLitCategorieDetails()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ChargerCategories.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            Throw
        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.1 - 26/03/2026
    ' ChargerAnnees
    '
    '   • Charge les années de la catégorie sélectionnée
    '------------------------------------------------------------
    Private Sub ChargerAnnees()

        If dgvPrixLitCategorie.CurrentRow Is Nothing Then
            dgvPrixLitAnnee.DataSource = Nothing
            ClearPrixLitAnneeDetails()
            Return
        End If

        Try

            Dim idCat As ULong = Convert.ToUInt64(dgvPrixLitCategorie.CurrentRow.Cells("id_prixlit_categorie").Value)

            _dtAnnees = GestionReferentiel.PrixLitAnnee_GetByCategorie(idCat)
            dgvPrixLitAnnee.DataSource = _dtAnnees

            UtilsForm.FormatReferentielGrid(dgvPrixLitAnnee)

            If dgvPrixLitAnnee.Columns.Contains("id_prixLit_Annee") Then dgvPrixLitAnnee.Columns("id_prixLit_Annee").Visible = False
            If dgvPrixLitAnnee.Columns.Contains("id_prixlit_categorie") Then dgvPrixLitAnnee.Columns("id_prixlit_categorie").Visible = False
            If dgvPrixLitAnnee.Columns.Contains("id_prixLit") Then dgvPrixLitAnnee.Columns("id_prixLit").Visible = False
            If dgvPrixLitAnnee.Columns.Contains("created_at") Then dgvPrixLitAnnee.Columns("created_at").Visible = False
            If dgvPrixLitAnnee.Columns.Contains("updated_at") Then dgvPrixLitAnnee.Columns("updated_at").Visible = False

            If dgvPrixLitAnnee.Columns.Contains("code_prixLit_Annee") Then
                dgvPrixLitAnnee.Columns("code_prixLit_Annee").HeaderText = "Code"
                dgvPrixLitAnnee.Columns("code_prixLit_Annee").Width = 80
            End If

            If dgvPrixLitAnnee.Columns.Contains("libelle_categorie") Then
                dgvPrixLitAnnee.Columns("libelle_categorie").HeaderText = "Catégorie"
                dgvPrixLitAnnee.Columns("libelle_categorie").Width = 180
            End If

            If dgvPrixLitAnnee.Columns.Contains("annee") Then
                dgvPrixLitAnnee.Columns("annee").HeaderText = "Année"
                dgvPrixLitAnnee.Columns("annee").Width = 70
            End If

            If dgvPrixLitAnnee.Columns.Contains("nom_prixLit") Then
                dgvPrixLitAnnee.Columns("nom_prixLit").HeaderText = "Prix"
                dgvPrixLitAnnee.Columns("nom_prixLit").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            If dgvPrixLitAnnee.Rows.Count > 0 Then
                SetCurrentCellFirstVisible(dgvPrixLitAnnee)
                BindSelectedPrixLitAnneeToDetails()
            Else
                ClearPrixLitAnneeDetails()
                BindCurrentCategorieContextToAnneeDetails()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ChargerAnnees.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' dgvPrixLit_SelectionChanged
    '
    '   • Synchronise la sélection PrixLit avec le détail
    '   • Recharge les catégories et années liées
    '------------------------------------------------------------
    Private Sub dgvPrixLit_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPrixLit.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub
        If dgvPrixLit.CurrentRow Is Nothing Then Exit Sub

        BindSelectedPrixLitToDetails()
        ChargerCategories()
        ChargerComboPrixLitCategorieAnnee()
        ChargerAnnees()

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.1 - 26/03/2026
    ' dgvPrixLitCategorie_SelectionChanged
    '
    '   • Synchronise la sélection Catégorie avec le détail
    '   • Recharge les années liées
    '------------------------------------------------------------
    Private Sub dgvPrixLitCategorie_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPrixLitCategorie.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub
        If dgvPrixLitCategorie.CurrentRow Is Nothing Then Exit Sub

        BindSelectedPrixLitCategorieToDetails()
        ChargerComboPrixLitCategorieAnnee()
        ChargerAnnees()

        If dgvPrixLitAnnee.Rows.Count = 0 Then
            BindCurrentCategorieContextToAnneeDetails()
        End If

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' dgvPrixLitAnnee_SelectionChanged
    '
    '   • Synchronise la sélection Année avec le détail
    '------------------------------------------------------------
    Private Sub dgvPrixLitAnnee_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPrixLitAnnee.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub
        If dgvPrixLitAnnee.CurrentRow Is Nothing Then Exit Sub

        BindSelectedPrixLitAnneeToDetails()

    End Sub

#End Region


#Region " Snapshot & Restauration"

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BuildPrixLitFromForm
    '
    '   • Construit un objet PrixLit depuis les champs de la form
    '------------------------------------------------------------
    Private Function BuildPrixLitFromForm() As PrixLit

        Return New PrixLit With {
        .IdPrixLit = If(String.IsNullOrWhiteSpace(txtidPrixLit.Text), 0UL, Convert.ToUInt64(txtidPrixLit.Text)),
        .CodePrixLit = txtCodePrixLit.Text.Trim(),
        .NomPrixLit = txtNomPrixLit.Text.Trim(),
        .DescriptionPrixLit = txtDescriptionPrixLit.Text.Trim(),
        .NotesPrixLitRtf = RichTextNotesHelper.GetNotesRtf(rtbNotesPrixLit),
        .NotesPrixLitTxt = RichTextNotesHelper.GetNotesTxt(rtbNotesPrixLit),
        .IsActif = chkPrixLitActif.Checked
    }
    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BuildPrixLitCategorieFromForm
    '
    '   • Construit un objet PrixLitCategorie depuis les champs de la form
    '------------------------------------------------------------
    Private Function BuildPrixLitCategorieFromForm() As PrixLitCategorie

        Return New PrixLitCategorie With {
        .IdPrixLitCategorie = If(String.IsNullOrWhiteSpace(txtidprixlitcategorie.Text), 0UL, Convert.ToUInt64(txtidprixlitcategorie.Text)),
        .IdPrixLit = Convert.ToUInt64(cboPrixLitParentCategorie.SelectedValue),
        .CodePrixLitCategorie = txtCodePrixLitCategorie.Text.Trim(),
        .LibelleCategorie = txtLibelleCategorie.Text.Trim(),
        .DescriptionCategorie = txtDescriptionCategorie.Text.Trim(),
        .OrdreAffichage = Convert.ToInt32(nudOrdreAffichageCategorie.Value),
        .IsActif = chkPrixLitCategorieActif.Checked
    }

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' BuildPrixLitAnneeFromForm
    '
    '   • Construit un objet PrixLitAnnee depuis les champs de la form
    '------------------------------------------------------------
    Private Function BuildPrixLitAnneeFromForm() As PrixLitAnnee

        Return New PrixLitAnnee With {
        .IdPrixLitAnnee = If(String.IsNullOrWhiteSpace(txtIdPrixLitAnnee.Text), 0UL, Convert.ToUInt64(txtIdPrixLitAnnee.Text)),
        .IdPrixLitCategorie = Convert.ToUInt64(cboPrixLitCategorieAnnee.SelectedValue),
        .CodePrixLitAnnee = txtCodePrixLitAnnee.Text.Trim(),
        .Annee = Convert.ToInt32(nudAnneePrixLit.Value)
    }

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' RestorePrixLitToForm
    '
    '   • Restaure un snapshot PrixLit dans les champs
    '------------------------------------------------------------
    Private Sub RestorePrixLitToForm(p As PrixLit)

        txtidPrixLit.Text = If(p.IdPrixLit = 0UL, String.Empty, p.IdPrixLit.ToString())
        txtCodePrixLit.Text = p.CodePrixLit
        txtNomPrixLit.Text = p.NomPrixLit
        txtDescriptionPrixLit.Text = p.DescriptionPrixLit
        chkPrixLitActif.Checked = p.IsActif

        RichTextNotesHelper.ChargerContenuNotes(
        rtbNotesPrixLit,
        p.NotesPrixLitRtf,
        p.NotesPrixLitTxt
    )

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' RestorePrixLitCategorieToForm
    '
    '   • Restaure un snapshot PrixLitCategorie dans les champs
    '------------------------------------------------------------
    Private Sub RestorePrixLitCategorieToForm(c As PrixLitCategorie)

        txtidprixlitcategorie.Text = If(c.IdPrixLitCategorie = 0UL, String.Empty, c.IdPrixLitCategorie.ToString())
        txtCodePrixLitCategorie.Text = c.CodePrixLitCategorie
        cboPrixLitParentCategorie.SelectedValue = c.IdPrixLit
        txtLibelleCategorie.Text = c.LibelleCategorie
        txtDescriptionCategorie.Text = c.DescriptionCategorie
        nudOrdreAffichageCategorie.Value = c.OrdreAffichage
        chkPrixLitCategorieActif.Checked = c.IsActif

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 23/03/2026
    ' RestorePrixLitAnneeToForm
    '
    '   • Restaure un snapshot PrixLitAnnee dans les champs
    '------------------------------------------------------------
    Private Sub RestorePrixLitAnneeToForm(a As PrixLitAnnee)

        txtIdPrixLitAnnee.Text = If(a.IdPrixLitAnnee = 0UL, String.Empty, a.IdPrixLitAnnee.ToString())
        txtCodePrixLitAnnee.Text = a.CodePrixLitAnnee
        cboPrixLitCategorieAnnee.SelectedValue = a.IdPrixLitCategorie
        nudAnneePrixLit.Value = a.Annee

    End Sub

#End Region


#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ValidateCurrentForm
    '
    '   • Valide le formulaire selon le contexte actif
    '------------------------------------------------------------
    Private Function ValidateCurrentForm() As Boolean

        ClearErrors()

        Select Case GetCurrentContext()

            Case PrixLitContext.PrixLit
                Return ValidatePrixLitForm()

            Case PrixLitContext.Categories
                Return ValidatePrixLitCategorieForm()

            Case PrixLitContext.Annees
                Return ValidatePrixLitAnneeForm()

            Case Else
                Return False

        End Select

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ValidatePrixLitForm
    '
    '   • Valide les champs PrixLit
    '------------------------------------------------------------
    Private Function ValidatePrixLitForm() As Boolean

        Dim ok As Boolean = True

        If String.IsNullOrWhiteSpace(txtNomPrixLit.Text) Then
            errProvider.SetError(txtNomPrixLit, "Le nom du prix littéraire est obligatoire.")
            ok = False
        End If

        Return ok

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ValidatePrixLitCategorieForm
    '
    '   • Valide les champs Catégorie
    '------------------------------------------------------------
    Private Function ValidatePrixLitCategorieForm() As Boolean

        Dim ok As Boolean = True

        If cboPrixLitParentCategorie.SelectedValue Is Nothing OrElse Not IsNumeric(cboPrixLitParentCategorie.SelectedValue) Then
            errProvider.SetError(cboPrixLitParentCategorie, "Le prix littéraire parent est obligatoire.")
            ok = False
        End If

        If String.IsNullOrWhiteSpace(txtLibelleCategorie.Text) Then
            errProvider.SetError(txtLibelleCategorie, "Le libellé de la catégorie est obligatoire.")
            ok = False
        End If

        Return ok

    End Function


    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ValidatePrixLitAnneeForm
    '
    '   • Valide les champs Année
    '------------------------------------------------------------
    Private Function ValidatePrixLitAnneeForm() As Boolean

        Dim ok As Boolean = True

        If cboPrixLitCategorieAnnee.SelectedValue Is Nothing OrElse Not IsNumeric(cboPrixLitCategorieAnnee.SelectedValue) Then
            errProvider.SetError(cboPrixLitCategorieAnnee, "La catégorie est obligatoire.")
            ok = False
        End If

        If nudAnneePrixLit.Value < 1000 OrElse nudAnneePrixLit.Value > 3000 Then
            errProvider.SetError(nudAnneePrixLit, "L'année doit être comprise entre 1000 et 3000.")
            ok = False
        End If

        Return ok

    End Function

#End Region


#Region "Recherche / Filtres"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnSearch_Click
    '
    '   • Lance la recherche selon le contexte actif
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplySearch()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' btnClearSearch_Click
    '
    '   • Efface la recherche et recharge les données
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()

        Select Case GetCurrentContext()

            Case PrixLitContext.PrixLit
                ChargerPrixLit()
                ChargerCategories()
                ChargerComboPrixLitCategorieAnnee()
                ChargerAnnees()

            Case PrixLitContext.Categories
                ChargerCategories()
                ChargerComboPrixLitCategorieAnnee()
                ChargerAnnees()

            Case PrixLitContext.Annees
                ChargerAnnees()

        End Select

        SetStatus("Recherche effacée.")

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' ApplySearch
    '
    '   • Applique la recherche selon l'onglet actif
    '------------------------------------------------------------
    Private Sub ApplySearch()

        Dim searchText As String = txtSearch.Text.Trim()

        If String.IsNullOrWhiteSpace(searchText) Then
            btnClearSearch_Click(Nothing, EventArgs.Empty)
            Exit Sub
        End If

        Select Case GetCurrentContext()

        '====================================================
        ' PRIX LITTERAIRE
        '====================================================
            Case PrixLitContext.PrixLit

                Dim dt As DataTable = GestionReferentiel.PrixLit_GetBySearch(
                searchText,
                chkActifsOnly.Checked,
                chkSearchNotes.Checked
            )

                dgvPrixLit.DataSource = dt

                If dgvPrixLit.Rows.Count > 0 Then
                    SetCurrentCellFirstVisible(dgvPrixLit)
                    BindSelectedPrixLitToDetails()
                    ChargerCategories()
                    ChargerComboPrixLitCategorieAnnee()
                    ChargerAnnees()
                Else
                    ClearPrixLitDetails()
                    dgvPrixLitCategorie.DataSource = Nothing
                    dgvPrixLitAnnee.DataSource = Nothing
                    ClearPrixLitCategorieDetails()
                    ClearPrixLitAnneeDetails()
                End If

                SetStatus("Recherche PrixLit appliquée.")


        '====================================================
        ' CATEGORIES
        '====================================================
            Case PrixLitContext.Categories

                If dgvPrixLit.CurrentRow Is Nothing Then
                    SetStatus("Sélectionnez d'abord un prix littéraire.")
                    Exit Sub
                End If

                Dim idPrixLit As ULong = Convert.ToUInt64(dgvPrixLit.CurrentRow.Cells("id_prixLit").Value)

                Dim dt As DataTable = GestionReferentiel.PrixLitCategorie_GetBySearch(idPrixLit, searchText)

                dgvPrixLitCategorie.DataSource = dt

                If dgvPrixLitCategorie.Rows.Count > 0 Then
                    SetCurrentCellFirstVisible(dgvPrixLitCategorie)
                    BindSelectedPrixLitCategorieToDetails()
                    ChargerComboPrixLitCategorieAnnee()
                    ChargerAnnees()
                Else
                    ClearPrixLitCategorieDetails()
                    dgvPrixLitAnnee.DataSource = Nothing
                    ClearPrixLitAnneeDetails()
                End If

                SetStatus("Recherche Catégories appliquée.")


        '====================================================
        ' ANNEES
        '====================================================
            Case PrixLitContext.Annees

                If dgvPrixLit.CurrentRow Is Nothing Then
                    SetStatus("Sélectionnez d'abord un prix littéraire.")
                    Exit Sub
                End If

                Dim idPrixLit As ULong = Convert.ToUInt64(dgvPrixLit.CurrentRow.Cells("id_prixLit").Value)

                Dim dt As DataTable = GestionReferentiel.PrixLitAnnee_GetBySearch(idPrixLit, searchText)

                dgvPrixLitAnnee.DataSource = dt

                If dgvPrixLitAnnee.Rows.Count > 0 Then
                    SetCurrentCellFirstVisible(dgvPrixLitAnnee)
                    BindSelectedPrixLitAnneeToDetails()
                Else
                    ClearPrixLitAnneeDetails()
                End If

                SetStatus("Recherche Années appliquée.")

        End Select

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' UpdateSearchControlsState
    '
    '   • Adapte les contrôles de recherche selon l'onglet actif
    '------------------------------------------------------------
    Private Sub UpdateSearchControlsState()

        Select Case GetCurrentContext()

            Case PrixLitContext.PrixLit
                chkSearchNotes.Enabled = True

            Case Else
                chkSearchNotes.Checked = False
                chkSearchNotes.Enabled = False
        End Select

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' cboFiltrePrixLit_SelectedIndexChanged
    '
    '   • Synchronise le contexte courant selon le filtre sélectionné
    '------------------------------------------------------------
    Private Sub cboFiltrePrixLit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFiltrePrixLit.SelectedIndexChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub
        If cboFiltrePrixLit.SelectedValue Is Nothing Then Exit Sub
        If Not IsNumeric(cboFiltrePrixLit.SelectedValue) Then Exit Sub

        Dim idPrixLit As ULong = Convert.ToUInt64(cboFiltrePrixLit.SelectedValue)

        If idPrixLit = 0UL Then
            ChargerPrixLit()
        Else
            DgvSelectRowById(dgvPrixLit, "id_prixLit", idPrixLit)
            BindSelectedPrixLitToDetails()
            ChargerCategories()
            ChargerComboPrixLitCategorieAnnee()
            ChargerAnnees()
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' tabMain_SelectedIndexChanged
    '
    '   • Met à jour l'état de l'UI lors du changement d'onglet
    '------------------------------------------------------------
    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged
        UpdateSearchControlsState()
        SetMode(ModeEdition.Consultation)
    End Sub














#End Region



End Class