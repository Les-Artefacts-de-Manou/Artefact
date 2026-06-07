'====================================================================
' 📌 UC_FormatFile.vb
' Version : V1.0
' Date    : 23/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl de gestion du référentiel FormatFile.
' Transposé depuis GestionFormatFile.vb
'
' Règles :
' - Implémente IContextAwareUserControl
' - Utilise UserControlContext pour StatusStrip, ErrorProvider, ToolTip
' - Logique métier identique à la Form d'origine
' - Gestion ordre_affichage (NumericUpDown) et is_actif (CheckBox)
' - Contrôle FK avant suppression
'
' Évolution :
' - V1.0 : Transposition depuis GestionFormatFile.vb
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_FormatFile
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Modes d'utilisation
    Private _mode As ModeEdition = ModeEdition.Consultation

    ' Identifiant du format courant
    Private _currentId As ULong = 0

    ' Contexte partagé
    Private _context As UserControlContext = Nothing

#End Region

#Region "Constructeur"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "IContextAwareUserControl Implementation"

    Public Sub SetContext(context As UserControlContext) Implements IContextAwareUserControl.SetContext
        _context = context
    End Sub

    Public Sub OnActivated() Implements IContextAwareUserControl.OnActivated
        Try
            LoadGrid()

            If _context IsNot Nothing Then
                _context.NavigateToLevel("Formats de fichier")
                _context.SetStatus($"Formats de fichier chargés : {dgvFormatFile.Rows.Count} élément(s).")
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_FormatFile.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            _context?.SetStatus("Erreur lors de l'activation.")
        End Try
    End Sub

    Public Sub OnDeactivated() Implements IContextAwareUserControl.OnDeactivated
        Try
            If _mode <> ModeEdition.Consultation Then
                btnCancel.PerformClick()
            End If

            If _context IsNot Nothing Then
                _context.ClearAllErrors()
            End If

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur désactivation UC_FormatFile.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

#End Region

#Region "Initialisation"

    Private Sub UC_FormatFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            InitToolTips()
            ClearErrors()

            LoadGrid()
            If dgvFormatFile.Rows.Count = 0 Then
                ClearDetails()
                _currentId = 0
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Formats de fichier chargés.")

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur chargement UC_FormatFile.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors du chargement des formats de fichier." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

    Private Sub InitToolTips()

        If _context?.ToolTip Is Nothing Then Return

        _context.ToolTip.SetToolTip(txtNomFormat, "Nom du format de fichier.")
        _context.ToolTip.SetToolTip(txtExtension, "Extension associée au format (ex : epub, pdf, mobi).")
        _context.ToolTip.SetToolTip(txtMimeType, "Type MIME correspondant.")
        _context.ToolTip.SetToolTip(nudOrdreAffichage, "Ordre d'affichage du format.")
        _context.ToolTip.SetToolTip(chkIsActif, "Format actif/inactif.")
        _context.ToolTip.SetToolTip(btnSearch, "Appliquer le filtre")
        _context.ToolTip.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        _context.ToolTip.SetToolTip(btnNew, "Créer un nouveau format")
        _context.ToolTip.SetToolTip(btnEdit, "Passer en mode modification")
        _context.ToolTip.SetToolTip(btnSave, "Enregistrer les modifications")
        _context.ToolTip.SetToolTip(btnCancel, "Annuler les modifications")
        _context.ToolTip.SetToolTip(btnDelete, "Supprimer le format sélectionné")

    End Sub

#End Region

#Region "Gestion des modes"

    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Dim hasSelection As Boolean = HasSelectedFormatFile()

        Select Case mode

            Case ModeEdition.Consultation

                btnNew.Enabled = True
                btnEdit.Enabled = hasSelection
                btnDelete.Enabled = hasSelection

                btnSave.Enabled = False
                btnCancel.Enabled = False

                txtNomFormat.ReadOnly = True
                txtExtension.ReadOnly = True
                txtMimeType.ReadOnly = True

                nudOrdreAffichage.Enabled = False
                chkIsActif.Enabled = False

                dgvFormatFile.Enabled = True
                pnlTop.Enabled = True

            Case ModeEdition.Nouveau, ModeEdition.Modification

                btnNew.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False

                btnSave.Enabled = True
                btnCancel.Enabled = True

                txtNomFormat.ReadOnly = False
                txtExtension.ReadOnly = False
                txtMimeType.ReadOnly = False

                nudOrdreAffichage.Enabled = True
                chkIsActif.Enabled = True

                dgvFormatFile.Enabled = False
                pnlTop.Enabled = False

        End Select

        If _context IsNot Nothing Then
            Select Case mode
                Case ModeEdition.Consultation
                    _context.NavigateToLevel("Formats de fichier")
                Case ModeEdition.Nouveau
                    _context.SetMode("Nouveau")
                Case ModeEdition.Modification
                    _context.SetMode("Modification")
            End Select
        End If

    End Sub

    Private Function HasSelectedFormatFile() As Boolean
        Return dgvFormatFile.CurrentRow IsNot Nothing AndAlso dgvFormatFile.Rows.Count > 0
    End Function

#End Region

#Region "Interface utilisateur"

    Private Sub SetStatus(message As String)
        If _context IsNot Nothing Then
            _context.SetStatus(message)
        End If
    End Sub

    Private Sub ClearErrors()
        If _context IsNot Nothing Then
            _context.ClearAllErrors()
        End If
    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try
            ClearErrors()
            ClearDetails()

            _currentId = 0

            SetMode(ModeEdition.Nouveau)
            SetStatus("Nouveau format.")

            txtNomFormat.Focus()

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur passage en mode Nouveau pour format fichier.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            MessageBox.Show(
                "Erreur lors de l'initialisation d'un nouveau format." & Environment.NewLine & ex.Message,
                "Artefact",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try
            ClearErrors()

            If Not HasSelectedFormatFile() Then
                SetStatus("Aucun format sélectionné.")
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(txtIdFormatFile.Text) Then
                SetStatus("Sélection invalide.")
                Exit Sub
            End If

            _currentId = SafeULong(txtIdFormatFile.Text)

            SetMode(ModeEdition.Modification)
            SetStatus("Modification du format.")

            txtNomFormat.Focus()

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur passage en mode Modification format fichier.",
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            ClearErrors()

            If Not ValidateForm() Then Exit Sub

            Dim formatFile As FormatFile = GetCurrentFormatFileFromFields()

            If _mode = ModeEdition.Nouveau Then

                GestionReferentiel.FormatFile_Insert(
                    formatFile.NomFormat,
                    formatFile.Extension,
                    formatFile.MimeType,
                    formatFile.OrdreAffichage,
                    formatFile.IsActif
                )

                GestionLog.EcrireLog(
                    $"UI: création format fichier '{formatFile.NomFormat}'.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.UI
                )

                SetStatus("Format créé.")

            ElseIf _mode = ModeEdition.Modification Then

                GestionReferentiel.FormatFile_Update(
                    formatFile.IdFormatFile,
                    formatFile.NomFormat,
                    formatFile.Extension,
                    formatFile.MimeType,
                    formatFile.OrdreAffichage,
                    formatFile.IsActif
                )

                GestionLog.EcrireLog(
                    $"UI: modification format fichier '{formatFile.NomFormat}'.",
                    GestionLog.LogLevel.Succinct,
                    GestionLog.LogCategory.UI
                )

                SetStatus("Format modifié.")

            End If

            LoadGrid()
            SelectFormatFileById(_currentId)

            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur enregistrement format fichier.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de l'enregistrement.")
        End Try

    End Sub

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
                "UI: erreur annulation format fichier.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try
            If _mode <> ModeEdition.Consultation Then
                SetStatus("Suppression impossible pendant une édition.")
                Return
            End If

            If Not HasSelectedFormatFile() Then
                SetStatus("Aucun format sélectionné.")
                Return
            End If

            Dim id As ULong = SafeULong(txtIdFormatFile.Text)
            Dim nomFormat As String = txtNomFormat.Text

            ' Vérification dépendances
            Dim nb As Integer = GestionReferentiel.FormatFile_CountLivresFichiers(id)

            If nb > 0 Then
                MessageBox.Show(
                    $"Ce format est utilisé par {nb} fichier(s) de livre et ne peut pas être supprimé.",
                    "Suppression impossible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                SetStatus("Suppression impossible : format utilisé.")
                Return
            End If

            Dim rep = MessageBox.Show(
                $"Supprimer le format '{nomFormat}' ?",
                "Confirmation suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            )

            If rep <> DialogResult.Yes Then
                SetStatus("Suppression annulée.")
                Return
            End If

            GestionReferentiel.FormatFile_Delete(id)

            GestionLog.EcrireLog(
                $"UI: suppression format fichier '{nomFormat}'.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI
            )

            SetStatus("Format supprimé.")

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur suppression format fichier.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
            SetStatus("Erreur lors de la suppression.")
        End Try

    End Sub

#End Region

#Region "Synchronisation des données"

    Private Sub LoadGrid()

        Try
            Dim dt = GestionReferentiel.FormatFile_GetAll()
            dgvFormatFile.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvFormatFile)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvFormatFile, "id_formatFile", "code_formatFile", "created_at", "updated_at")

            ' Sélection première ligne
            If dgvFormatFile.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvFormatFile, "nom_format")
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvFormatFile, "format")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur LoadGrid (UC_FormatFile).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors du chargement de la grille.")
        End Try

    End Sub

    Private Sub BindSelectedToDetails()

        Try
            If dgvFormatFile.CurrentRow Is Nothing Then
                ClearDetails()
                Return
            End If

            Dim row = dgvFormatFile.CurrentRow

            txtIdFormatFile.Text = If(row.Cells("id_formatFile")?.Value?.ToString(), "")
            txtCodeFormatFile.Text = If(row.Cells("code_formatFile")?.Value?.ToString(), "")
            txtNomFormat.Text = If(row.Cells("nom_format")?.Value?.ToString(), "")
            txtExtension.Text = If(row.Cells("extension")?.Value?.ToString(), "")
            txtMimeType.Text = If(row.Cells("mime_type")?.Value?.ToString(), "")

            nudOrdreAffichage.Value = DbToInt(row.Cells("ordre_affichage").Value)
            chkIsActif.Checked = DbToBool(row.Cells("is_actif").Value)

            _currentId = SafeULong(txtIdFormatFile.Text)

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur BindSelectedToDetails (UC_FormatFile).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try

    End Sub

    Private Sub ClearDetails()

        txtIdFormatFile.Text = ""
        txtCodeFormatFile.Text = ""
        txtNomFormat.Text = ""
        txtExtension.Text = ""
        txtMimeType.Text = ""

        nudOrdreAffichage.Value = 1
        chkIsActif.Checked = True

        _currentId = 0

    End Sub

    Private Sub dgvFormatFile_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFormatFile.SelectionChanged
        If _mode = ModeEdition.Consultation Then
            BindSelectedToDetails()
            SetMode(ModeEdition.Consultation)
        End If
    End Sub

    Private Sub SelectFormatFileById(id As ULong)
        Try
            If id > 0 Then
                UtilsForm.DgvSelectRowById(dgvFormatFile, "id_formatFile", id)
                BindSelectedToDetails()
            End If
        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur SelectFormatFileById (UC_FormatFile).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
        End Try
    End Sub

#End Region

#Region "Validation"

    Private Function ValidateForm() As Boolean

        ClearErrors()

        Dim isValid As Boolean = True

        If String.IsNullOrWhiteSpace(txtNomFormat.Text) Then
            _context?.ErrorProvider.SetError(txtNomFormat, "Le nom du format est obligatoire.")
            isValid = False
        End If

        If Not isValid Then
            SetStatus("Formulaire incomplet ou invalide.")
        End If

        Return isValid

    End Function

    Private Function GetCurrentFormatFileFromFields() As FormatFile

        Return New FormatFile With {
            .IdFormatFile = _currentId,
            .CodeFormatFile = txtCodeFormatFile.Text.Trim(),
            .NomFormat = txtNomFormat.Text.Trim(),
            .Extension = txtExtension.Text.Trim(),
            .MimeType = txtMimeType.Text.Trim(),
            .OrdreAffichage = Convert.ToInt32(nudOrdreAffichage.Value),
            .IsActif = chkIsActif.Checked
        }

    End Function

#End Region

#Region "Recherche"

    Private Sub ApplySearch()

        Try
            Dim searchText As String = txtSearch.Text.Trim()

            Dim dt = If(String.IsNullOrWhiteSpace(searchText),
                        GestionReferentiel.FormatFile_GetAll(),
                        GestionReferentiel.FormatFile_GetBySearch(searchText))

            dgvFormatFile.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvFormatFile)

            ' Masquer colonnes techniques
            UtilsUCReferentiels.HideTechnicalColumns(dgvFormatFile, "id_formatFile", "code_formatFile", "created_at", "updated_at")

            ' Sélection première ligne
            If dgvFormatFile.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvFormatFile, "nom_format")
                BindSelectedToDetails()
            Else
                ClearDetails()
            End If

            ' Compteur
            UtilsUCReferentiels.UpdateCountLabel(lblCount, dgvFormatFile, "format")

            SetStatus($"Recherche effectuée : {dgvFormatFile.Rows.Count} résultat(s).")

        Catch ex As Exception
            GestionLog.EcrireLog("UI: erreur ApplySearch (UC_FormatFile).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)
            SetStatus("Erreur lors de la recherche.")
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplySearch()
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Text = ""
        LoadGrid()
        SetStatus("Filtre effacé.")
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ApplySearch()
        End If
    End Sub

#End Region

#Region "Helpers"

    Private Function SafeULong(value As String) As ULong
        Return UtilsUCReferentiels.SafeULong(value)
    End Function

    Private Function DbToBool(value As Object) As Boolean
        Return UtilsUCReferentiels.DbToBool(value)
    End Function

    Private Function DbToInt(value As Object) As Integer
        Return UtilsUCReferentiels.DbToInt(value)
    End Function

#End Region

End Class
