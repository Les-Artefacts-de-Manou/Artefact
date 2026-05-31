'====================================================================
' 📌 UC_RefEnum.vb
' Version : V1.0
' Date    : 24/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion hiérarchique ref_enum_type / ref_enum.
' Approche Master-Detail unifiée : types à gauche, détails type + valeurs à droite.
'
' Règles :
' - Implémente IContextAwareUserControl
' - Utilise UserControlContext pour StatusStrip, ErrorProvider, ToolTip
' - Modes duaux indépendants : _modeType et _modeValeur
' - Suppression type avec valeurs : 3 choix (voir valeurs / supprimer tout / annuler)
' - Les valeurs sont toujours filtrées par le type sélectionné
'
' Évolution :
' - V1.0 : Migration depuis GestionRefEnum.vb avec approche Master-Detail
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_RefEnum
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'édition duaux (type et valeur indépendants)
    Private _modeType As ModeEdition = ModeEdition.Consultation
    Private _modeValeur As ModeEdition = ModeEdition.Consultation

    ' Snapshots pour annulation
    Private _snapshotType As RefEnumType = Nothing
    Private _snapshotValeur As RefEnumValeur = Nothing

    ' Identifiants courants
    Private _currentIdType As ULong = 0
    Private _currentIdValeur As ULong = 0

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
                _context.NavigateToLevel("Ref Enum")
                _context.SetMode("Référentiels hiérarchiques")
                SetStatus("Types et valeurs d'énumération chargés.")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_RefEnum.",
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
                "UI: erreur désactivation UC_RefEnum.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

#End Region

#Region "Initialisation"

    Private Sub UC_RefEnum_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()

            LoadTypes()

            If dgvTypes.Rows.Count = 0 Then
                ClearTypeDetails()
                ClearValeursArea()
            End If

            SetStatus("Types d'énumération chargés.")
            ' SetModeType et SetModeValeur sont appelés par LoadTypes après binding

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur UC_RefEnum_Load.",
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

        ' Recherche types
        tt.SetToolTip(btnSearchTypes, "Rechercher un type")
        tt.SetToolTip(btnClearSearchTypes, "Effacer le filtre")

        ' CRUD Type
        tt.SetToolTip(btnNewType, "Créer un nouveau type")
        tt.SetToolTip(btnEditType, "Modifier le type sélectionné")
        tt.SetToolTip(btnSaveType, "Enregistrer le type")
        tt.SetToolTip(btnCancelType, "Annuler les modifications")
        tt.SetToolTip(btnDeleteType, "Supprimer le type sélectionné")

        ' Champs Type
        tt.SetToolTip(txtCodeType, "Code technique du type (MAJUSCULES)")
        tt.SetToolTip(txtLibelleType, "Libellé humain du type")
        tt.SetToolTip(nudOrdreType, "Ordre d'affichage")
        tt.SetToolTip(chkTypeActif, "Type actif/inactif")

        ' Recherche valeurs
        tt.SetToolTip(btnSearchValeurs, "Rechercher une valeur")
        tt.SetToolTip(btnClearSearchValeurs, "Effacer le filtre")
        tt.SetToolTip(chkValeursActives, "Afficher uniquement les valeurs actives")

        ' CRUD Valeur
        tt.SetToolTip(btnNewValeur, "Créer une nouvelle valeur pour ce type")
        tt.SetToolTip(btnEditValeur, "Modifier la valeur sélectionnée")
        tt.SetToolTip(btnSaveValeur, "Enregistrer la valeur")
        tt.SetToolTip(btnCancelValeur, "Annuler les modifications")
        tt.SetToolTip(btnDeleteValeur, "Supprimer la valeur sélectionnée")

        ' Champs Valeur
        tt.SetToolTip(txtCodeValeur, "Code technique de la valeur (MAJUSCULES)")
        tt.SetToolTip(txtLibelleValeur, "Libellé humain de la valeur")
        tt.SetToolTip(nudOrdreValeur, "Ordre d'affichage")
        tt.SetToolTip(chkValeurActive, "Valeur active/inactive")

    End Sub

    ''' <summary>
    ''' Normalise le code type : majuscules, espaces -> underscores, pas de caractères spéciaux.
    ''' </summary>
    Private Sub txtCodeType_TextChanged(sender As Object, e As EventArgs) Handles txtCodeType.TextChanged
        If _modeType = ModeEdition.Consultation Then Exit Sub

        Dim cursorPos As Integer = txtCodeType.SelectionStart
        Dim originalText As String = txtCodeType.Text
        Dim normalizedText As String = NormalizeCodeName(originalText)

        If originalText <> normalizedText Then
            txtCodeType.Text = normalizedText
            ' Restaurer la position du curseur
            txtCodeType.SelectionStart = Math.Min(cursorPos, txtCodeType.Text.Length)
        End If
    End Sub

    ''' <summary>
    ''' Normalise le code valeur : majuscules, espaces -> underscores, pas de caractères spéciaux.
    ''' </summary>
    Private Sub txtCodeValeur_TextChanged(sender As Object, e As EventArgs) Handles txtCodeValeur.TextChanged
        If _modeValeur = ModeEdition.Consultation Then Exit Sub

        Dim cursorPos As Integer = txtCodeValeur.SelectionStart
        Dim originalText As String = txtCodeValeur.Text
        Dim normalizedText As String = NormalizeCodeName(originalText)

        If originalText <> normalizedText Then
            txtCodeValeur.Text = normalizedText
            ' Restaurer la position du curseur
            txtCodeValeur.SelectionStart = Math.Min(cursorPos, txtCodeValeur.Text.Length)
        End If
    End Sub

    ''' <summary>
    ''' Normalise une chaîne pour un code : majuscules, espaces -> _, supprime ' . ,
    ''' </summary>
    Private Function NormalizeCodeName(input As String) As String
        If String.IsNullOrEmpty(input) Then Return input

        Dim result As String = input.ToUpperInvariant()
        result = result.Replace(" "c, "_"c)
        result = result.Replace("'"c, String.Empty)
        result = result.Replace("."c, String.Empty)
        result = result.Replace(","c, String.Empty)

        Return result
    End Function

#End Region

#Region "Gestion des modes"

    Private Sub SetModeType(mode As ModeEdition)

        _modeType = mode

        Dim hasSelection As Boolean =
            dgvTypes IsNot Nothing AndAlso
            dgvTypes.CurrentRow IsNot Nothing AndAlso
            dgvTypes.Rows.Count > 0

        Select Case mode

            Case ModeEdition.Consultation

                btnNewType.Enabled = True
                btnEditType.Enabled = hasSelection
                btnDeleteType.Enabled = hasSelection

                btnSaveType.Enabled = False
                btnCancelType.Enabled = False

                txtCodeType.ReadOnly = True
                txtLibelleType.ReadOnly = True
                nudOrdreType.ReadOnly = True
                chkTypeActif.Enabled = False

                ' Réactiver les contrôles valeurs
                EnableValeursControls(True)

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNewType.Enabled = False
                btnEditType.Enabled = False
                btnDeleteType.Enabled = False

                btnSaveType.Enabled = True
                btnCancelType.Enabled = True

                txtCodeType.ReadOnly = (mode = ModeEdition.Modification)
                txtLibelleType.ReadOnly = False
                nudOrdreType.ReadOnly = False
                chkTypeActif.Enabled = True

                ' Désactiver les contrôles valeurs pendant édition type
                EnableValeursControls(False)

        End Select

    End Sub

    Private Sub SetModeValeur(mode As ModeEdition)

        _modeValeur = mode

        Dim hasSelection As Boolean =
            dgvValeurs IsNot Nothing AndAlso
            dgvValeurs.CurrentRow IsNot Nothing AndAlso
            dgvValeurs.Rows.Count > 0

        Select Case mode

            Case ModeEdition.Consultation

                btnNewValeur.Enabled = (_currentIdType > 0)
                btnEditValeur.Enabled = hasSelection
                btnDeleteValeur.Enabled = hasSelection

                btnSaveValeur.Enabled = False
                btnCancelValeur.Enabled = False

                txtCodeValeur.ReadOnly = True
                txtLibelleValeur.ReadOnly = True
                nudOrdreValeur.ReadOnly = True
                chkValeurActive.Enabled = False

                ' Réactiver les contrôles type
                EnableTypeControls(True)

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNewValeur.Enabled = False
                btnEditValeur.Enabled = False
                btnDeleteValeur.Enabled = False

                btnSaveValeur.Enabled = True
                btnCancelValeur.Enabled = True

                txtCodeValeur.ReadOnly = (mode = ModeEdition.Modification)
                txtLibelleValeur.ReadOnly = False
                nudOrdreValeur.ReadOnly = False
                chkValeurActive.Enabled = True

                ' Désactiver les contrôles type pendant édition valeur
                EnableTypeControls(False)

        End Select

    End Sub

    Private Sub EnableTypeControls(enabled As Boolean)
        btnNewType.Enabled = enabled
        btnEditType.Enabled = enabled AndAlso dgvTypes.CurrentRow IsNot Nothing
        btnDeleteType.Enabled = enabled AndAlso dgvTypes.CurrentRow IsNot Nothing
        btnSearchTypes.Enabled = enabled
        btnClearSearchTypes.Enabled = enabled
    End Sub

    Private Sub EnableValeursControls(enabled As Boolean)
        btnNewValeur.Enabled = enabled AndAlso _currentIdType > 0
        btnEditValeur.Enabled = enabled AndAlso dgvValeurs.CurrentRow IsNot Nothing
        btnDeleteValeur.Enabled = enabled AndAlso dgvValeurs.CurrentRow IsNot Nothing
        btnSearchValeurs.Enabled = enabled
        btnClearSearchValeurs.Enabled = enabled
        chkValeursActives.Enabled = enabled
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

#Region "Chargement données - Types"

    Private Sub LoadTypes()

        Try
            Dim dt As DataTable = GestionReferentiel.RefEnumType_GetAll()

            dgvTypes.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvTypes, {"id_enum_type", "code_enum_type", "created_at", "updated_at"})

            UtilsUCReferentiels.UpdateCountLabel(lblCountTypes, dgvTypes, "type")

            If dgvTypes.Rows.Count > 0 Then
                dgvTypes.ClearSelection()
                dgvTypes.Rows(0).Selected = True
                BindSelectedTypeToDetails()
                ' Réactiver les boutons type après binding
                SetModeType(ModeEdition.Consultation)
                ' Réactiver les boutons valeurs après binding
                ' (LoadValeurs a déjà été appelée par BindSelectedTypeToDetails)
                SetModeValeur(ModeEdition.Consultation)
            Else
                ClearTypeDetails()
                ClearValeursArea()
                SetModeType(ModeEdition.Consultation)
                SetModeValeur(ModeEdition.Consultation)
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur LoadTypes.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur chargement types.")
        End Try

    End Sub

    Private Sub BindSelectedTypeToDetails()

        If dgvTypes.CurrentRow Is Nothing Then
            ClearTypeDetails()
            ClearValeursArea()
            Return
        End If

        Dim row As DataGridViewRow = dgvTypes.CurrentRow

        txtIdEnumType.Text = row.Cells("id_enum_type").Value?.ToString()
        txtCodeEnumType.Text = row.Cells("code_enum_type").Value?.ToString()

        txtCodeType.Text = row.Cells("code_type").Value?.ToString()
        txtLibelleType.Text = row.Cells("libelle_type").Value?.ToString()
        nudOrdreType.Value = UtilsUCReferentiels.DbToInt(row.Cells("ordre_affichage").Value)
        chkTypeActif.Checked = UtilsUCReferentiels.DbToBool(row.Cells("is_actif").Value)

        _currentIdType = UtilsUCReferentiels.SafeULong(txtIdEnumType.Text)

        ' Charger les valeurs liées
        LoadValeurs()

        ' Réactiver les boutons valeurs après chargement
        ' Note : toujours appeler SetModeValeur pour synchroniser l'état des boutons
        ' après LoadValeurs, car la sélection des valeurs vient d'être mise à jour
        SetModeValeur(ModeEdition.Consultation)

    End Sub

    Private Sub ClearTypeDetails()
        txtIdEnumType.Clear()
        txtCodeEnumType.Clear()
        txtCodeType.Clear()
        txtLibelleType.Clear()
        nudOrdreType.Value = 0
        chkTypeActif.Checked = True
        _currentIdType = 0
    End Sub

#End Region

#Region "Chargement données - Valeurs"

    Private Sub LoadValeurs()

        If _currentIdType = 0 Then
            ClearValeursArea()
            Return
        End If

        Try
            Dim actifsOnly As Boolean = chkValeursActives.Checked
            Dim dt As DataTable = GestionReferentiel.RefEnum_GetByType(_currentIdType, actifsOnly)

            dgvValeurs.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvValeurs, {"id_enum", "code_enum", "id_enum_type", "created_at", "updated_at"})

            UtilsUCReferentiels.UpdateCountLabel(lblCountValeurs, dgvValeurs, "valeur")

            If dgvValeurs.Rows.Count > 0 Then
                dgvValeurs.ClearSelection()
                dgvValeurs.Rows(0).Selected = True
                BindSelectedValeurToDetails()
            Else
                ClearValeurDetails()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur LoadValeurs.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur chargement valeurs.")
        End Try

    End Sub

    Private Sub BindSelectedValeurToDetails()

        If dgvValeurs.CurrentRow Is Nothing Then
            ClearValeurDetails()
            Return
        End If

        Dim row As DataGridViewRow = dgvValeurs.CurrentRow

        txtIdEnum.Text = row.Cells("id_enum").Value?.ToString()
        txtCodeEnum.Text = row.Cells("code_enum").Value?.ToString()

        txtCodeValeur.Text = row.Cells("code_valeur").Value?.ToString()
        txtLibelleValeur.Text = row.Cells("libelle_valeur").Value?.ToString()
        nudOrdreValeur.Value = UtilsUCReferentiels.DbToInt(row.Cells("ordre_affichage").Value)
        chkValeurActive.Checked = UtilsUCReferentiels.DbToBool(row.Cells("is_actif").Value)

        _currentIdValeur = UtilsUCReferentiels.SafeULong(txtIdEnum.Text)

    End Sub

    Private Sub ClearValeurDetails()
        txtIdEnum.Clear()
        txtCodeEnum.Clear()
        txtCodeValeur.Clear()
        txtLibelleValeur.Clear()
        nudOrdreValeur.Value = 0
        chkValeurActive.Checked = True
        _currentIdValeur = 0
    End Sub

    Private Sub ClearValeursArea()
        dgvValeurs.DataSource = Nothing
        ClearValeurDetails()
        lblCountValeurs.Text = "0 valeur"
    End Sub

#End Region

#Region "Événements grilles"

    Private Sub dgvTypes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTypes.SelectionChanged

        If _modeType <> ModeEdition.Consultation Then Exit Sub

        Try
            BindSelectedTypeToDetails()
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur dgvTypes_SelectionChanged.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub dgvValeurs_SelectionChanged(sender As Object, e As EventArgs) Handles dgvValeurs.SelectionChanged

        If _modeValeur <> ModeEdition.Consultation Then Exit Sub

        Try
            BindSelectedValeurToDetails()
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur dgvValeurs_SelectionChanged.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

#End Region

#Region "Actions CRUD - Type"

    Private Sub btnNewType_Click(sender As Object, e As EventArgs) Handles btnNewType.Click

        ClearErrors()
        ClearTypeDetails()

        txtCodeType.Text = ""
        txtLibelleType.Text = ""
        nudOrdreType.Value = 0
        chkTypeActif.Checked = True

        SetModeType(ModeEdition.Nouveau)
        txtCodeType.Focus()

    End Sub

    Private Sub btnEditType_Click(sender As Object, e As EventArgs) Handles btnEditType.Click

        If dgvTypes.CurrentRow Is Nothing Then
            SetStatus("Aucun type sélectionné.")
            Return
        End If

        ClearErrors()

        _snapshotType = New RefEnumType With {
            .CodeType = txtCodeType.Text,
            .LibelleType = txtLibelleType.Text,
            .OrdreAffichage = CInt(nudOrdreType.Value),
            .IsActif = chkTypeActif.Checked
        }

        SetModeType(ModeEdition.Modification)
        txtLibelleType.Focus()

    End Sub

    Private Sub btnSaveType_Click(sender As Object, e As EventArgs) Handles btnSaveType.Click

        If Not ValidateTypeForm() Then Exit Sub

        Try
            Dim t As New RefEnumType With {
                .CodeType = txtCodeType.Text.Trim(),
                .LibelleType = txtLibelleType.Text.Trim(),
                .OrdreAffichage = CInt(nudOrdreType.Value),
                .IsActif = chkTypeActif.Checked
            }

            Dim wasNew As Boolean = (_modeType = ModeEdition.Nouveau)

            If _modeType = ModeEdition.Nouveau Then
                _currentIdType = GestionReferentiel.RefEnumType_Insert(t)
                SetStatus("Type créé.")
            ElseIf _modeType = ModeEdition.Modification Then
                t.IdEnumType = _currentIdType
                GestionReferentiel.RefEnumType_Update(t)
                SetStatus("Type modifié.")
            End If

            ' Passer en consultation AVANT de recharger
            SetModeType(ModeEdition.Consultation)

            ' Recharger la grid sans réinitialiser les modes
            Dim dt As DataTable = GestionReferentiel.RefEnumType_GetAll()
            dgvTypes.DataSource = dt
            UtilsUCReferentiels.HideTechnicalColumns(dgvTypes, {"id_enum_type", "code_enum_type", "created_at", "updated_at"})
            UtilsUCReferentiels.UpdateCountLabel(lblCountTypes, dgvTypes, "type")

            ' Resélectionner le type modifié/créé
            For i As Integer = 0 To dgvTypes.Rows.Count - 1
                If UtilsUCReferentiels.SafeULong(dgvTypes.Rows(i).Cells("id_enum_type").Value?.ToString()) = _currentIdType Then
                    dgvTypes.ClearSelection()
                    dgvTypes.Rows(i).Selected = True
                    dgvTypes.CurrentCell = dgvTypes.Rows(i).Cells(0)

                    ' Bind les détails sans recharger les valeurs si c'était un nouveau type
                    Dim row As DataGridViewRow = dgvTypes.CurrentRow
                    txtIdEnumType.Text = row.Cells("id_enum_type").Value?.ToString()
                    txtCodeEnumType.Text = row.Cells("code_enum_type").Value?.ToString()
                    txtCodeType.Text = row.Cells("code_type").Value?.ToString()
                    txtLibelleType.Text = row.Cells("libelle_type").Value?.ToString()
                    nudOrdreType.Value = UtilsUCReferentiels.DbToInt(row.Cells("ordre_affichage").Value)
                    chkTypeActif.Checked = UtilsUCReferentiels.DbToBool(row.Cells("is_actif").Value)
                    _currentIdType = UtilsUCReferentiels.SafeULong(txtIdEnumType.Text)

                    ' Charger les valeurs
                    If wasNew Then
                        ClearValeursArea() ' Nouveau type = pas encore de valeurs
                    Else
                        LoadValeurs()
                    End If

                    ' Activer le bouton Nouveau Valeur
                    SetModeValeur(ModeEdition.Consultation)

                    Exit For
                End If
            Next

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnSaveType_Click.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de l'enregistrement du type.")
        End Try

    End Sub

    Private Sub btnCancelType_Click(sender As Object, e As EventArgs) Handles btnCancelType.Click

        ClearErrors()

        If _modeType = ModeEdition.Modification AndAlso _snapshotType IsNot Nothing Then
            txtCodeType.Text = _snapshotType.CodeType
            txtLibelleType.Text = _snapshotType.LibelleType
            nudOrdreType.Value = _snapshotType.OrdreAffichage
            chkTypeActif.Checked = _snapshotType.IsActif
        End If

        SetModeType(ModeEdition.Consultation)
        SetStatus("Modification annulée.")

    End Sub

    Private Sub btnDeleteType_Click(sender As Object, e As EventArgs) Handles btnDeleteType.Click

        Try
            If dgvTypes.CurrentRow Is Nothing Then
                SetStatus("Aucun type sélectionné.")
                Exit Sub
            End If

            Dim id As ULong = UtilsUCReferentiels.SafeULong(txtIdEnumType.Text)

            If id = 0 Then
                SetStatus("Aucun type sélectionné.")
                Exit Sub
            End If

            Dim nbValeurs As Integer = GestionReferentiel.RefEnum_CountByType(id)

            '------------------------------------------------------------
            ' Cas 1 : aucune valeur liée
            '------------------------------------------------------------
            If nbValeurs = 0 Then

                Dim rep = MessageBox.Show(
                    "Supprimer ce type ?",
                    "Artefact",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                )

                If rep <> DialogResult.Yes Then
                    SetStatus("Suppression annulée.")
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
                ClearValeursArea()
                SetModeType(ModeEdition.Consultation)

                SetStatus("Type supprimé.")

                Exit Sub
            End If

            '------------------------------------------------------------
            ' Cas 2 : valeurs liées présentes
            '------------------------------------------------------------
            Dim dlg As New DialogChoix()
            dlg.Titre = "Suppression avec dépendances"
            dlg.Message = $"Ce type contient {nbValeurs} valeur(s) associée(s) (visibles dans la section ""Valeurs"" ci-dessous)." & Environment.NewLine & Environment.NewLine &
                          "Que voulez-vous faire ?"
            dlg.SetBoutons("Voir les valeurs", "Supprimer tout", "Annuler")

            Dim choix As DialogResult = dlg.ShowDialog()

            Select Case choix

                Case DialogResult.Yes
                    ' Focus sur la section valeurs
                    grpValeurs.Focus()
                    If dgvValeurs.Rows.Count > 0 Then
                        dgvValeurs.Focus()
                    End If
                    SetStatus($"{nbValeurs} valeur(s) liée(s) affichée(s). Supprimez-les ou modifiez-les avant de supprimer le type.")
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
                    ClearTypeDetails()
                    ClearValeursArea()

                    SetModeType(ModeEdition.Consultation)
                    SetModeValeur(ModeEdition.Consultation)

                    SetStatus("Type et valeurs associées supprimés.")
                    Exit Sub

                Case Else
                    SetStatus("Suppression annulée.")
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

#End Region

#Region "Actions CRUD - Valeur"

    Private Sub btnNewValeur_Click(sender As Object, e As EventArgs) Handles btnNewValeur.Click

        If _currentIdType = 0 Then
            SetStatus("Aucun type sélectionné.")
            Return
        End If

        ClearErrors()
        ClearValeurDetails()

        txtCodeValeur.Text = ""
        txtLibelleValeur.Text = ""
        nudOrdreValeur.Value = 0
        chkValeurActive.Checked = True

        SetModeValeur(ModeEdition.Nouveau)
        txtCodeValeur.Focus()

    End Sub

    Private Sub btnEditValeur_Click(sender As Object, e As EventArgs) Handles btnEditValeur.Click

        If dgvValeurs.CurrentRow Is Nothing Then
            SetStatus("Aucune valeur sélectionnée.")
            Return
        End If

        ClearErrors()

        _snapshotValeur = New RefEnumValeur With {
            .CodeValeur = txtCodeValeur.Text,
            .LibelleValeur = txtLibelleValeur.Text,
            .OrdreAffichage = CInt(nudOrdreValeur.Value),
            .IsActif = chkValeurActive.Checked
        }

        SetModeValeur(ModeEdition.Modification)
        txtLibelleValeur.Focus()

    End Sub

    Private Sub btnSaveValeur_Click(sender As Object, e As EventArgs) Handles btnSaveValeur.Click

        If Not ValidateValeurForm() Then Exit Sub

        Try
            Dim v As New RefEnumValeur With {
                .IdEnumType = _currentIdType,
                .CodeValeur = txtCodeValeur.Text.Trim(),
                .LibelleValeur = txtLibelleValeur.Text.Trim(),
                .OrdreAffichage = CInt(nudOrdreValeur.Value),
                .IsActif = chkValeurActive.Checked
            }

            If _modeValeur = ModeEdition.Nouveau Then
                _currentIdValeur = GestionReferentiel.RefEnum_Insert(v)
                SetStatus("Valeur créée.")
            ElseIf _modeValeur = ModeEdition.Modification Then
                v.IdEnum = _currentIdValeur
                GestionReferentiel.RefEnum_Update(v)
                SetStatus("Valeur modifiée.")
            End If

            ' Passer en consultation AVANT de recharger
            SetModeValeur(ModeEdition.Consultation)

            ' Recharger les valeurs
            Dim actifsOnly As Boolean = chkValeursActives.Checked
            Dim dt As DataTable = GestionReferentiel.RefEnum_GetByType(_currentIdType, actifsOnly)
            dgvValeurs.DataSource = dt
            UtilsUCReferentiels.HideTechnicalColumns(dgvValeurs, {"id_enum", "code_enum", "id_enum_type", "created_at", "updated_at"})
            UtilsUCReferentiels.UpdateCountLabel(lblCountValeurs, dgvValeurs, "valeur")

            ' Resélectionner la valeur modifiée/créée
            For i As Integer = 0 To dgvValeurs.Rows.Count - 1
                If UtilsUCReferentiels.SafeULong(dgvValeurs.Rows(i).Cells("id_enum").Value?.ToString()) = _currentIdValeur Then
                    dgvValeurs.ClearSelection()
                    dgvValeurs.Rows(i).Selected = True
                    dgvValeurs.CurrentCell = dgvValeurs.Rows(i).Cells(0)
                    BindSelectedValeurToDetails()
                    Exit For
                End If
            Next

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur btnSaveValeur_Click.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de l'enregistrement de la valeur.")
        End Try

    End Sub

    Private Sub btnCancelValeur_Click(sender As Object, e As EventArgs) Handles btnCancelValeur.Click

        ClearErrors()

        If _modeValeur = ModeEdition.Modification AndAlso _snapshotValeur IsNot Nothing Then
            txtCodeValeur.Text = _snapshotValeur.CodeValeur
            txtLibelleValeur.Text = _snapshotValeur.LibelleValeur
            nudOrdreValeur.Value = _snapshotValeur.OrdreAffichage
            chkValeurActive.Checked = _snapshotValeur.IsActif
        End If

        SetModeValeur(ModeEdition.Consultation)
        SetStatus("Modification annulée.")

    End Sub

    Private Sub btnDeleteValeur_Click(sender As Object, e As EventArgs) Handles btnDeleteValeur.Click

        If dgvValeurs.CurrentRow Is Nothing Then
            SetStatus("Aucune valeur sélectionnée.")
            Exit Sub
        End If

        Dim idEnum As ULong = UtilsUCReferentiels.SafeULong(txtIdEnum.Text)

        If idEnum = 0 Then
            SetStatus("Aucune valeur sélectionnée.")
            Exit Sub
        End If

        ' Ici : contrôle des usages (comme dans GestionRefEnum)
        ' Pour simplifier, on demande confirmation simple
        ' Dans un UC complet, il faudrait vérifier RefEnum_CountUsage*

        Dim rep = MessageBox.Show(
            "Supprimer cette valeur ?" & Environment.NewLine & Environment.NewLine &
            "Attention : si cette valeur est utilisée dans d'autres tables, " &
            "la suppression peut échouer ou entraîner des effets de bord.",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        )

        If rep <> DialogResult.Yes Then
            SetStatus("Suppression annulée.")
            Exit Sub
        End If

        Try
            GestionReferentiel.RefEnum_Delete(idEnum)

            GestionLog.EcrireLog(
                $"UI: suppression valeur enum id={idEnum}.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI
            )

            LoadValeurs()
            ClearValeurDetails()
            SetModeValeur(ModeEdition.Consultation)

            SetStatus("Valeur supprimée.")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur suppression valeur enum.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la suppression de la valeur.")
            MessageBox.Show(
                "Erreur lors de la suppression." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

#End Region

#Region "Recherche"

    Private Sub btnSearchTypes_Click(sender As Object, e As EventArgs) Handles btnSearchTypes.Click
        ApplySearchTypes()
    End Sub

    Private Sub btnClearSearchTypes_Click(sender As Object, e As EventArgs) Handles btnClearSearchTypes.Click
        txtSearchTypes.Clear()
        LoadTypes()
        SetStatus("Filtre effacé.")
    End Sub

    Private Sub txtSearchTypes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchTypes.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplySearchTypes()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub ApplySearchTypes()

        Dim search As String = txtSearchTypes.Text.Trim()

        If String.IsNullOrWhiteSpace(search) Then
            LoadTypes()
            Return
        End If

        Try
            Dim dt As DataTable = GestionReferentiel.RefEnumType_GetBySearch(search)
            dgvTypes.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvTypes, {"id_enum_type", "code_enum_type", "created_at", "updated_at"})
            UtilsUCReferentiels.UpdateCountLabel(lblCountTypes, dgvTypes, "type")

            If dgvTypes.Rows.Count > 0 Then
                dgvTypes.ClearSelection()
                dgvTypes.Rows(0).Selected = True
                BindSelectedTypeToDetails()
                ' Réactiver les boutons après binding
                SetModeType(ModeEdition.Consultation)
                SetModeValeur(ModeEdition.Consultation)
            Else
                ClearTypeDetails()
                ClearValeursArea()
                SetModeType(ModeEdition.Consultation)
                SetModeValeur(ModeEdition.Consultation)
            End If

            SetStatus($"Recherche : {dgvTypes.Rows.Count} type(s) trouvé(s).")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur ApplySearchTypes.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    Private Sub btnSearchValeurs_Click(sender As Object, e As EventArgs) Handles btnSearchValeurs.Click
        ApplySearchValeurs()
    End Sub

    Private Sub btnClearSearchValeurs_Click(sender As Object, e As EventArgs) Handles btnClearSearchValeurs.Click
        txtSearchValeurs.Clear()
        LoadValeurs()
        SetStatus("Filtre valeurs effacé.")
    End Sub

    Private Sub txtSearchValeurs_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchValeurs.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplySearchValeurs()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub chkValeursActives_CheckedChanged(sender As Object, e As EventArgs) Handles chkValeursActives.CheckedChanged
        LoadValeurs()
    End Sub

    Private Sub ApplySearchValeurs()

        If _currentIdType = 0 Then
            SetStatus("Aucun type sélectionné.")
            Return
        End If

        Dim search As String = txtSearchValeurs.Text.Trim()

        If String.IsNullOrWhiteSpace(search) Then
            LoadValeurs()
            Return
        End If

        Try
            Dim actifsOnly As Boolean = chkValeursActives.Checked
            Dim dt As DataTable = GestionReferentiel.RefEnum_GetByTypeAndSearch(_currentIdType, search, actifsOnly)
            dgvValeurs.DataSource = dt

            UtilsUCReferentiels.HideTechnicalColumns(dgvValeurs, {"id_enum", "code_enum", "id_enum_type", "created_at", "updated_at"})
            UtilsUCReferentiels.UpdateCountLabel(lblCountValeurs, dgvValeurs, "valeur")

            If dgvValeurs.Rows.Count > 0 Then
                dgvValeurs.ClearSelection()
                dgvValeurs.Rows(0).Selected = True
                BindSelectedValeurToDetails()
            Else
                ClearValeurDetails()
            End If

            SetStatus($"Recherche valeurs : {dgvValeurs.Rows.Count} valeur(s) trouvée(s).")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur ApplySearchValeurs.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la recherche valeurs.")
        End Try

    End Sub

#End Region

#Region "Validation"

    Private Function ValidateTypeForm() As Boolean

        ClearErrors()

        If _context?.ErrorProvider Is Nothing Then Return True

        Dim ep = _context.ErrorProvider

        If String.IsNullOrWhiteSpace(txtCodeType.Text) Then
            ep.SetError(txtCodeType, "Code type obligatoire.")
            SetStatus("Code type manquant.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtLibelleType.Text) Then
            ep.SetError(txtLibelleType, "Libellé type obligatoire.")
            SetStatus("Libellé type manquant.")
            Return False
        End If

        Return True

    End Function

    Private Function ValidateValeurForm() As Boolean

        ClearErrors()

        If _context?.ErrorProvider Is Nothing Then Return True

        Dim ep = _context.ErrorProvider

        If String.IsNullOrWhiteSpace(txtCodeValeur.Text) Then
            ep.SetError(txtCodeValeur, "Code valeur obligatoire.")
            SetStatus("Code valeur manquant.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtLibelleValeur.Text) Then
            ep.SetError(txtLibelleValeur, "Libellé valeur obligatoire.")
            SetStatus("Libellé valeur manquant.")
            Return False
        End If

        Return True

    End Function

#End Region

End Class
