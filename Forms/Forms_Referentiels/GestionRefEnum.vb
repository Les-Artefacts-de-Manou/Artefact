'------------------------------------------------------------
' 📌 GestionRefEnum.vb
' Version : V1.1
' Date    : 12/03/2026
'
' Rôle :
' Formulaire WinForms de gestion du référentiel hiérarchique
' ref_enum_type (types parents) / ref_enum (valeurs enfants).
'
' Règles Artefact :
' - Pas de SQL ici (QueryModule).
' - Pas d'accès DB direct ici (GestionReferentiel).
' - Validation UI : errProvider + StatusStrip.
' - Modèle à 2 niveaux : type parent / valeurs liées.
'
' Évolution :
' - V1.0 : gestion initiale des types et valeurs.
' - V1.1 : réorganisation complète sur le modèle GestionPays
'          (régions, ordre logique, en-têtes homogènes, nettoyage mineur).
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionRefEnum

#Region "Déclarations"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' Variables privées
    '
    '   • _mode      : mode courant du formulaire
    '   • _dtTypes   : source de données des types parents
    '   • _dtValeurs : source de données des valeurs enfants
    '------------------------------------------------------------
    Private _mode As ModeEdition = ModeEdition.Consultation
    Private _dtTypes As DataTable
    Private _dtValeurs As DataTable

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' RefEnumContext
    '
    '   • Détermine le contexte courant selon l'onglet actif
    '   • Types   : table parent ref_enum_type
    '   • Valeurs : table enfant ref_enum
    '------------------------------------------------------------
    Private Enum RefEnumContext
        Types
        Valeurs
    End Enum

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' GestionRefEnum_Load
    '
    '   • Initialise complètement le formulaire au chargement
    '   • Charge les types puis les valeurs liées
    '   • Configure l'UI et les info-bulles
    '------------------------------------------------------------
    Private Sub GestionRefEnum_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            InitTooltips()
            ClearErrors()

            LoadTypes()
            LoadValeurs()

            SetMode(ModeEdition.Consultation)
            SetStatus("Types d'énumération chargés.")
            UpdateSearchControlsState()

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur chargement GestionRefEnum.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors du chargement.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' GetCurrentContext
    '
    '   • Retourne le contexte courant selon l'onglet actif
    '------------------------------------------------------------
    Private Function GetCurrentContext() As RefEnumContext

        If tabMain.SelectedTab Is tabTypes Then
            Return RefEnumContext.Types
        End If

        Return RefEnumContext.Valeurs

    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
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

        ttMain.SetToolTip(txtCodeType, "Code technique du type")
        ttMain.SetToolTip(txtLibelleType, "Libellé du type d'énumération")
        ttMain.SetToolTip(txtCodeValeur, "Code technique de la valeur")
        ttMain.SetToolTip(txtLibelleValeur, "Libellé de la valeur")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' SetMode
    '
    '   • Définit le mode courant du formulaire
    '   • Active/désactive les boutons selon l'onglet actif
    '   • Passe les contrôles concernés en lecture seule ou en saisie
    '------------------------------------------------------------
    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Dim hasSelection As Boolean

        If GetCurrentContext() = RefEnumContext.Types Then
            hasSelection =
                dgvEnumTypes IsNot Nothing AndAlso
                dgvEnumTypes.CurrentRow IsNot Nothing AndAlso
                dgvEnumTypes.Rows.Count > 0
        Else
            hasSelection =
                dgvEnumValeurs IsNot Nothing AndAlso
                dgvEnumValeurs.CurrentRow IsNot Nothing AndAlso
                dgvEnumValeurs.Rows.Count > 0
        End If

        Select Case mode

            Case ModeEdition.Consultation

                btnNew.Enabled = True
                btnEdit.Enabled = hasSelection
                btnDelete.Enabled = hasSelection

                btnSave.Enabled = False
                btnCancel.Enabled = False

                If tabMain.SelectedTab Is tabTypes Then
                    txtCodeType.ReadOnly = True
                    txtLibelleType.ReadOnly = True
                    nudOrdreType.ReadOnly = True
                    chkTypeActif.Enabled = False
                Else
                    txtCodeValeur.ReadOnly = True
                    txtLibelleValeur.ReadOnly = True
                    nudOrdreValeur.ReadOnly = True
                    chkValeurActive.Enabled = False
                    cboTypeEnum.Enabled = True
                End If

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNew.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False

                btnSave.Enabled = True
                btnCancel.Enabled = True

                If tabMain.SelectedTab Is tabTypes Then
                    txtCodeType.ReadOnly = False
                    txtLibelleType.ReadOnly = False
                    nudOrdreType.ReadOnly = False
                    chkTypeActif.Enabled = True
                Else
                    txtCodeValeur.ReadOnly = False
                    txtLibelleValeur.ReadOnly = False
                    nudOrdreValeur.ReadOnly = False
                    chkValeurActive.Enabled = True
                    cboTypeEnum.Enabled = True
                End If

        End Select

    End Sub

#End Region

#Region "Interface utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' SetStatus
    '
    '   • Affiche un message dans la barre de statut
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)

        stsLabelStatus.Text = message

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ClearErrors
    '
    '   • Efface les erreurs de validation affichées dans l’UI
    '------------------------------------------------------------
    Private Sub ClearErrors()

        errProvider.Clear()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
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
    ' 📌 V1.1 - 12/03/2026
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
    ' 📌 V1.1 - 12/03/2026
    ' BindSelectedTypeToDetails
    '
    '   • Charge le type parent sélectionné dans les champs détail
    '------------------------------------------------------------
    Private Sub BindSelectedTypeToDetails()

        If dgvEnumTypes.CurrentRow Is Nothing Then
            ClearTypeDetails()
            Return
        End If

        Dim row As DataGridViewRow = dgvEnumTypes.CurrentRow

        txtIdEnumType.Text = row.Cells("id_enum_type").Value?.ToString()
        txtCodeEnumType.Text = row.Cells("code_enum_type").Value?.ToString()

        txtCodeType.Text = row.Cells("code_type").Value?.ToString()
        txtLibelleType.Text = row.Cells("libelle_type").Value?.ToString()

        nudOrdreType.Value = DbToInt(row.Cells("ordre_affichage").Value)

        chkTypeActif.Checked = DbToBool(row.Cells("is_actif").Value)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' BindSelectedValeurToDetails
    '
    '   • Charge la valeur enfant sélectionnée dans les champs détail
    '------------------------------------------------------------
    Private Sub BindSelectedValeurToDetails()

        If dgvEnumValeurs.CurrentRow Is Nothing Then
            ClearValeurDetails()
            Return
        End If

        Dim row As DataGridViewRow = dgvEnumValeurs.CurrentRow

        txtIdEnum.Text = row.Cells("id_enum").Value?.ToString()
        txtCodeEnum.Text = row.Cells("code_enum").Value?.ToString()

        txtCodeValeur.Text = row.Cells("code_valeur").Value?.ToString()
        txtLibelleValeur.Text = row.Cells("libelle_valeur").Value?.ToString()

        nudOrdreValeur.Value = DbToInt(row.Cells("ordre_affichage").Value)

        chkValeurActive.Checked = DbToBool(row.Cells("is_actif").Value)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ClearTypeDetails
    '
    '   • Réinitialise les champs détail du type parent
    '------------------------------------------------------------
    Private Sub ClearTypeDetails()

        txtIdEnumType.Clear()
        txtCodeEnumType.Clear()

        txtCodeType.Clear()
        txtLibelleType.Clear()

        nudOrdreType.Value = 0

        chkTypeActif.Checked = True

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ClearValeurDetails
    '
    '   • Réinitialise les champs détail de la valeur enfant
    '------------------------------------------------------------
    Private Sub ClearValeurDetails()

        txtIdEnum.Clear()
        txtCodeEnum.Clear()

        txtCodeValeur.Clear()
        txtLibelleValeur.Clear()

        nudOrdreValeur.Value = 0

        chkValeurActive.Checked = True

    End Sub


#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnNew_Click
    '
    '   • Prépare la création d'un type ou d'une valeur selon le contexte
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        ClearErrors()

        Select Case GetCurrentContext()
            Case RefEnumContext.Types
                ClearTypeDetails()
            Case RefEnumContext.Valeurs
                ClearValeurDetails()
        End Select

        SetMode(ModeEdition.Nouveau)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnEdit_Click
    '
    '   • Passe l'élément sélectionné en mode modification
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        ClearErrors()

        Select Case GetCurrentContext()

            Case RefEnumContext.Types

                If dgvEnumTypes.CurrentRow Is Nothing Then
                    SetStatus("Aucun type sélectionné.")
                    Return
                End If

            Case RefEnumContext.Valeurs

                If dgvEnumValeurs.CurrentRow Is Nothing Then
                    SetStatus("Aucune valeur sélectionnée.")
                    Return
                End If

        End Select

        SetMode(ModeEdition.Modification)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnSave_Click
    '
    '   • Enregistre selon le contexte courant (type parent / valeur enfant)
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            Select Case GetCurrentContext()
                Case RefEnumContext.Types
                    SaveType()
                Case RefEnumContext.Valeurs
                    SaveValeur()
            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur Save RefEnum.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors de l'enregistrement.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnCancel_Click
    '
    '   • Annule l'édition en cours et revient en consultation
    '------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        ClearErrors()

        SetMode(ModeEdition.Consultation)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnDelete_Click
    '
    '   • Supprime selon le contexte courant (type parent / valeur enfant)
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try

            Select Case GetCurrentContext()

                Case RefEnumContext.Types
                    DeleteType()

                Case RefEnumContext.Valeurs
                    DeleteValeur()

            End Select

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur Delete RefEnum.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnClose_Click
    '
    '   • Ferme simplement le formulaire courant
    '------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' tabMain_SelectedIndexChanged
    '
    '   • Adapte l'UI au changement d'onglet
    '   • Réinitialise le mode en consultation
    '------------------------------------------------------------
    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged

        UpdateSearchControlsState()
        SetMode(ModeEdition.Consultation)
        If tabMain.SelectedTab Is tabTypes Then
            lblCount.Text = $"{_dtTypes.Rows.Count} Type(s)."
        Else
            lblCount.Text = $"{_dtValeurs.Rows.Count} Valeur(s)."
        End If

    End Sub


#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' LoadTypes
    '
    '   • Charge la liste des types parents
    '   • Met à jour la grille, le compteur et le combo des valeurs
    '------------------------------------------------------------
    Private Sub LoadTypes()

        _dtTypes = GestionReferentiel.RefEnumType_GetAll()

        dgvEnumTypes.DataSource = _dtTypes

        UtilsForm.FormatReferentielGrid(dgvEnumTypes)

        If dgvEnumTypes.Columns.Contains("id_enum_type") Then
            dgvEnumTypes.Columns("id_enum_type").Visible = False
        End If

        lblCount.Text = $"{_dtTypes.Rows.Count} Nbre types chargés."

        If UtilsForm.SelectFirstRow(dgvEnumTypes, "libelle_type") Then
            BindSelectedTypeToDetails()
        Else
            ClearTypeDetails()
            ClearValeurDetails()
        End If

        cboTypeEnum.DataSource = _dtTypes
        cboTypeEnum.DisplayMember = "libelle_type"
        cboTypeEnum.ValueMember = "id_enum_type"

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' LoadValeurs
    '
    '   • Charge les valeurs enfants du type sélectionné
    '   • Met à jour la grille et le compteur
    '------------------------------------------------------------
    Private Sub LoadValeurs()

        If cboTypeEnum.SelectedValue Is Nothing Then
            ClearValeurDetails()
            Exit Sub
        End If

        Dim idType As ULong = Convert.ToUInt64(cboTypeEnum.SelectedValue)

        _dtValeurs = GestionReferentiel.RefEnum_GetByType(idType, chkActifsOnly.Checked)

        dgvEnumValeurs.DataSource = _dtValeurs

        UtilsForm.FormatReferentielGrid(dgvEnumValeurs)

        If tabMain.SelectedTab Is tabTypes Then
            lblCount.Text = $"{_dtTypes.Rows.Count} Type(s)."
        Else
            lblCount.Text = $"{_dtValeurs.Rows.Count} Valeur(s)."
        End If

        If dgvEnumValeurs.Columns.Contains("id_enum") Then
            dgvEnumValeurs.Columns("id_enum").Visible = False
        End If

        If dgvEnumValeurs.Columns.Contains("id_enum_type") Then
            dgvEnumValeurs.Columns("id_enum_type").Visible = False
        End If

        If UtilsForm.SelectFirstRow(dgvEnumValeurs, "libelle_valeur") Then
            BindSelectedValeurToDetails()
        Else
            ClearValeurDetails()
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' dgvEnumTypes_SelectionChanged
    '
    '   • Synchronise le détail type et le combo des valeurs
    '------------------------------------------------------------
    Private Sub dgvEnumTypes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEnumTypes.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub

        Try

            BindSelectedTypeToDetails()

            ' Synchronise le combo de l'onglet valeurs
            Dim idType As ULong = DgvGetSelectedId(dgvEnumTypes, "id_enum_type")
            If idType <> 0UL Then
                cboTypeEnum.SelectedValue = idType
            End If

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur SelectionChanged Types.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' dgvEnumValeurs_SelectionChanged
    '
    '   • Synchronise le détail valeur avec la sélection courante
    '------------------------------------------------------------
    Private Sub dgvEnumValeurs_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEnumValeurs.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Exit Sub

        Try

            BindSelectedValeurToDetails()

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur SelectionChanged Valeurs.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' cboTypeEnum_SelectedIndexChanged
    '
    '   • Recharge les valeurs enfants quand le type change
    '------------------------------------------------------------
    Private Sub cboTypeEnum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTypeEnum.SelectedIndexChanged

        Try

            LoadValeurs()

            SetStatus("Valeurs chargées.")

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur chargement valeurs enum.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur chargement valeurs.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' chkActifsOnly_CheckedChanged
    '
    '   • Recharge les valeurs enfants quand le filtre actif change
    '------------------------------------------------------------
    Private Sub chkActifsOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkActifsOnly.CheckedChanged

        LoadValeurs()

    End Sub

#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ValidateTypeForm
    '
    '   • Vérifie les champs obligatoires du type parent
    '------------------------------------------------------------
    Private Function ValidateTypeForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtCodeType.Text) Then
            errProvider.SetError(txtCodeType, "Code obligatoire.")
            SetStatus("Code type manquant.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtLibelleType.Text) Then
            errProvider.SetError(txtLibelleType, "Libellé obligatoire.")
            SetStatus("Libellé type manquant.")
            Return False
        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' ValidateValeurForm
    '
    '   • Vérifie les champs obligatoires de la valeur enfant
    '------------------------------------------------------------
    Private Function ValidateValeurForm() As Boolean

        ClearErrors()

        If String.IsNullOrWhiteSpace(txtCodeValeur.Text) Then
            errProvider.SetError(txtCodeValeur, "Code obligatoire.")
            SetStatus("Code valeur manquant.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtLibelleValeur.Text) Then
            errProvider.SetError(txtLibelleValeur, "Libellé obligatoire.")
            SetStatus("Libellé valeur manquant.")
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Recherche"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnSearch_Click
    '
    '   • Lance la recherche dans les valeurs du type sélectionné
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Try

            If cboTypeEnum.SelectedValue Is Nothing Then Exit Sub

            Dim idType As ULong = Convert.ToUInt64(cboTypeEnum.SelectedValue)

            Dim txt = txtSearch.Text.Trim()

            If txt = "" Then
                LoadValeurs()
                Exit Sub
            End If

            _dtValeurs = GestionReferentiel.RefEnum_GetByTypeAndSearch(idType, txt, chkActifsOnly.Checked)

            dgvEnumValeurs.DataSource = _dtValeurs

            UtilsForm.FormatReferentielGrid(dgvEnumValeurs)

            lblCount.Text = $"{_dtValeurs.Rows.Count} Valeur(s)."

            dgvEnumValeurs.Columns("id_enum").Visible = False
            dgvEnumValeurs.Columns("id_enum_type").Visible = False

            SetStatus("Recherche effectuée.")

        Catch ex As Exception

            GestionLog.EcrireLog("UI: erreur recherche RefEnum.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            SetStatus("Erreur recherche.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' btnClearSearch_Click
    '
    '   • Efface la recherche et recharge la liste complète des valeurs
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()

        LoadValeurs()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' UpdateSearchControlsState
    '
    '   • Active ou désactive les contrôles de recherche
    '   • La recherche ne s'applique qu'aux valeurs enfants
    '------------------------------------------------------------
    Private Sub UpdateSearchControlsState()

        Dim isValeursTab As Boolean = (tabMain.SelectedTab Is tabValeurs)

        txtSearch.Enabled = isValeursTab
        btnSearch.Enabled = isValeursTab
        btnClearSearch.Enabled = isValeursTab

        If Not isValeursTab Then
            txtSearch.Clear()
        End If

    End Sub
#End Region

#Region "Méthodes métier internes"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' SaveType
    '
    '   • Sauvegarde un type parent ref_enum_type
    '------------------------------------------------------------
    Private Sub SaveType()

        If Not ValidateTypeForm() Then Exit Sub

        Dim t As New RefEnumType

        t.CodeType = txtCodeType.Text.Trim()
        t.LibelleType = txtLibelleType.Text.Trim()
        t.OrdreAffichage = CInt(nudOrdreType.Value)
        t.IsActif = chkTypeActif.Checked

        If _mode = ModeEdition.Nouveau Then
            Dim id = GestionReferentiel.RefEnumType_Insert(t)
            SetStatus("Type ajouté.")
        ElseIf _mode = ModeEdition.Modification Then
            t.IdEnumType = Convert.ToUInt64(txtIdEnumType.Text)
            GestionReferentiel.RefEnumType_Update(t)
            SetStatus("Type modifié.")
        End If

        LoadTypes()

        SetMode(ModeEdition.Consultation)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' SaveValeur
    '
    '   • Sauvegarde une valeur enfant ref_enum
    '------------------------------------------------------------
    Private Sub SaveValeur()

        Dim v As New RefEnumValeur

        If Not ValidateValeurForm() Then Exit Sub

        v.CodeValeur = txtCodeValeur.Text.Trim()
        v.LibelleValeur = txtLibelleValeur.Text.Trim()
        v.OrdreAffichage = CInt(nudOrdreValeur.Value)
        v.IsActif = chkValeurActive.Checked

        v.IdEnumType = Convert.ToUInt64(cboTypeEnum.SelectedValue)

        If _mode = ModeEdition.Nouveau Then
            Dim id = GestionReferentiel.RefEnum_Insert(v)
            SetStatus("Valeur ajoutée.")
        ElseIf _mode = ModeEdition.Modification Then
            v.IdEnum = Convert.ToUInt64(txtIdEnum.Text)
            GestionReferentiel.RefEnum_Update(v)
            SetStatus("Valeur modifiée.")
        End If

        LoadValeurs()

        SetMode(ModeEdition.Consultation)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' DeleteType
    '
    '   • Supprime un type parent
    '   • Gère les valeurs liées selon les règles métier
    '------------------------------------------------------------
    Private Sub DeleteType()

        Try
            Dim id As ULong = UtilsForm.DgvGetSelectedId(dgvEnumTypes, "id_enum_type")

            If id = 0 Then
                stsLabelStatus.Text = "Aucun type sélectionné."
                Exit Sub
            End If

            Dim nbValeurs As Integer = GestionReferentiel.RefEnum_CountByType(id)

            '--------------------------------------------------
            ' Cas 1 : aucune valeur liée
            '--------------------------------------------------
            If nbValeurs = 0 Then

                Dim rep = MessageBox.Show(
                "Supprimer ce type ?",
                "Artefact",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

                If rep <> DialogResult.Yes Then
                    stsLabelStatus.Text = "Suppression annulée."
                    Exit Sub
                End If

                GestionReferentiel.RefEnumType_Delete(id)

                GestionLog.EcrireLog(
                $"UI: suppression type enum id={id}.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI
            )

                LoadTypes()
                ClearTypeDetails()
                SetMode(UtilsForm.ModeEdition.Consultation)

                stsLabelStatus.Text = "Type supprimé."

                Exit Sub
            End If

            '--------------------------------------------------
            ' Cas 2 : valeurs liées présentes
            '--------------------------------------------------
            Dim choix = MessageBox.Show(
            $"Ce type contient {nbValeurs} valeur(s) associée(s)." & Environment.NewLine & Environment.NewLine &
            "Oui  = aller sur les valeurs liées" & Environment.NewLine &
            "Non  = supprimer le type et toutes ses valeurs" & Environment.NewLine &
            "Annuler = ne rien faire",
            "Suppression impossible en l'état",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning
        )

            Select Case choix

                Case DialogResult.Yes
                    AllerSurValeursLiees(id)
                    stsLabelStatus.Text = "Valeurs liées affichées. Supprimez-les ou modifiez-les avant de supprimer le type."
                    Exit Sub

                Case DialogResult.No
                    Dim repSuppression = MessageBox.Show(
                    "Confirmer la suppression du type ET de toutes ses valeurs associées ?",
                    "Artefact",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                )

                    If repSuppression <> DialogResult.Yes Then
                        SetStatus("Suppression complète annulée.")
                        Exit Sub
                    End If

                    GestionReferentiel.RefEnumType_DeleteWithValues(id)

                    GestionLog.EcrireLog(
                    $"UI: suppression complète type enum id={id} + valeurs liées.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.UI
                )

                    LoadTypes()
                    LoadValeurs()

                    ClearTypeDetails()
                    ClearValeurDetails()

                    SetMode(UtilsForm.ModeEdition.Consultation)
                    'SetValeurMode(UtilsForm.ModeEdition.Consultation)

                    SetStatus("Type et valeurs associées supprimés.")
                    Exit Sub

                Case Else
                    stsLabelStatus.Text = "Suppression annulée."
                    Exit Sub

            End Select

        Catch ex As Exception
            SetStatus("Erreur lors de la suppression du type.")

            GestionLog.EcrireLog(
            "UI: erreur suppression type enum.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            MessageBox.Show(
            "Erreur lors de la suppression du type." & Environment.NewLine & ex.Message,
            "Artefact",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' DeleteValeur
    '
    '   • Supprime une valeur enfant avec contrôle des usages
    '------------------------------------------------------------
    Private Sub DeleteValeur()

        If dgvEnumValeurs.CurrentRow Is Nothing Then
            SetStatus("Aucune valeur sélectionnée.")
            Exit Sub
        End If

        Dim idEnum As ULong = Convert.ToUInt64(txtIdEnum.Text)

        '--------------------------------------------
        ' Comptage des usages
        '--------------------------------------------

        Dim nbAuteursPays = GestionReferentiel.RefEnum_CountUsageInAuteursPaysTypeRelation(idEnum)
        Dim nbLivresAuteurs = GestionReferentiel.RefEnum_CountUsageInLivresAuteursRole(idEnum)
        Dim nbLivresFichiersScope = GestionReferentiel.RefEnum_CountUsageInLivresFichiersScope(idEnum)
        Dim nbLivresFichiersType = GestionReferentiel.RefEnum_CountUsageInLivresFichiersType(idEnum)
        Dim nbStagingSource = GestionReferentiel.RefEnum_CountUsageInLivresStagingSourceImport(idEnum)
        Dim nbStagingStatut = GestionReferentiel.RefEnum_CountUsageInLivresStagingStatut(idEnum)
        Dim nbStagingAuteurs = GestionReferentiel.RefEnum_CountUsageInLivresStagingAuteursRole(idEnum)

        Dim nbStatutLecture = GestionReferentiel.RefEnum_CountUsageInLivresStatutLecture(idEnum)
        Dim nbSupportLecture = GestionReferentiel.RefEnum_CountUsageInLivresSupportLecture(idEnum)
        Dim nbTypeAcquisition = GestionReferentiel.RefEnum_CountUsageInLivresTypeAcquisition(idEnum)

        '--------------------------------------------
        ' Cas RESTRICT → suppression impossible
        '--------------------------------------------

        If nbAuteursPays + nbLivresAuteurs + nbLivresFichiersScope + nbLivresFichiersType +
       nbStagingSource + nbStagingStatut + nbStagingAuteurs > 0 Then

            Dim msg =
            "Suppression impossible : cette valeur est encore utilisée." &
            Environment.NewLine & Environment.NewLine &
            $"Auteurs/Pays : {nbAuteursPays}" & Environment.NewLine &
            $"Livres/Auteurs : {nbLivresAuteurs}" & Environment.NewLine &
            $"Fichiers Scope : {nbLivresFichiersScope}" & Environment.NewLine &
            $"Fichiers Type : {nbLivresFichiersType}" & Environment.NewLine &
            $"Staging Source : {nbStagingSource}" & Environment.NewLine &
            $"Staging Statut : {nbStagingStatut}" & Environment.NewLine &
            $"Staging Auteurs : {nbStagingAuteurs}" & Environment.NewLine & Environment.NewLine &
            "Supprimez ou modifiez ces relations avant de supprimer cette valeur."

            MessageBox.Show(msg, "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            SetStatus("Suppression impossible : valeur utilisée.")
            Exit Sub

        End If

        '--------------------------------------------
        ' Cas SET NULL → avertissement
        '--------------------------------------------

        Dim totalSetNull = nbStatutLecture + nbSupportLecture + nbTypeAcquisition

        Dim rep As DialogResult

        If totalSetNull > 0 Then

            Dim msg =
            "Cette valeur est utilisée et sera mise à NULL si vous la supprimez." &
            Environment.NewLine & Environment.NewLine &
            $"Statut lecture : {nbStatutLecture}" & Environment.NewLine &
            $"Support lecture : {nbSupportLecture}" & Environment.NewLine &
            $"Type acquisition : {nbTypeAcquisition}" & Environment.NewLine & Environment.NewLine &
            "Confirmer la suppression ?"

            rep = MessageBox.Show(msg, "Confirmation suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        Else

            rep = MessageBox.Show(
            "Supprimer cette valeur ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        End If

        If rep <> DialogResult.Yes Then Exit Sub

        '--------------------------------------------
        ' Suppression
        '--------------------------------------------

        GestionReferentiel.RefEnum_Delete(idEnum)

        LoadValeurs()

        SetStatus("Valeur supprimée.")

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' AllerSurValeursLiees
    '
    '   • Positionne l'UI sur les valeurs du type parent sélectionné
    '------------------------------------------------------------
    Private Sub AllerSurValeursLiees(idEnumType As ULong)

        Try

            tabMain.SelectedTab = tabValeurs

            cboTypeEnum.SelectedValue = idEnumType

            LoadValeurs()

            If dgvEnumValeurs.Rows.Count > 0 Then
                dgvEnumValeurs.Rows(0).Selected = True
                BindSelectedValeurToDetails()
            Else
                ClearValeurDetails()
            End If

            SetStatus("Valeurs liées affichées.")

        Catch ex As Exception

            GestionLog.EcrireLog(
            $"UI: erreur positionnement sur les valeurs liées du type {idEnumType}.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de l'affichage des valeurs liées.")

            MessageBox.Show(
            "Erreur lors de l'affichage des valeurs liées." & Environment.NewLine & ex.Message,
            "Artefact",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

        End Try

    End Sub






#End Region

End Class