'------------------------------------------------------------
' 📌 GestionFormatFile.vb
' Version : V1.0
' Date    : 13/03/2026
'
' Rôle :
' Formulaire WinForms de gestion du référentiel FormatFile.
'
' Règles Artefact :
' - Pas de SQL ici (QueryModule).
' - Pas d'accès DB direct ici (GestionReferentiel).
' - Validation UI : errProvider + StatusStrip.
' - code_formatFile affiché en lecture seule.
' - id_formatFile conservé pour le pilotage CRUD mais non visible.
'
' Évolution :
' - V1.0 : CRUD de base, recherche, ouverture du site web.
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class GestionFormatFile
#Region "Déclarations"

    '------------------------------------------------------------
    ' 📌 V1.1 - 12/03/2026
    ' Variables privées
    '
    '   • _mode      : mode courant du formulaire
    '   • _currentId : identifiant de l'éditeur courant
    '------------------------------------------------------------
    Private _mode As ModeEdition = ModeEdition.Consultation
    Private _currentId As ULong = 0

#End Region

#Region "Initialisation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' GestionFormatFile_Load
    '
    ' Initialise la form :
    ' - charge la grille
    ' - sélectionne la première ligne si présente
    ' - vide les détails sinon
    ' - positionne l'UI en mode Consultation
    '------------------------------------------------------------
    Private Sub GestionFormatFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            "UI: erreur chargement GestionFormatFile.",
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

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' InitToolTips
    '
    ' Initialise les aides contextuelles.
    '------------------------------------------------------------
    Private Sub InitToolTips()

        ttMain.SetToolTip(txtNomFormat, "Nom du format de fichier.")
        ttMain.SetToolTip(txtExtension, "Extension associée au format (ex : epub, pdf, mobi).")
        ttMain.SetToolTip(txtMimeType, "Type MIME correspondant.")

        ttMain.SetToolTip(btnSearch, "Appliquer le filtre")
        ttMain.SetToolTip(btnClearSearch, "Effacer le filtre et réafficher la liste")
        ttMain.SetToolTip(btnNew, "Créer un nouvel éditeur")
        ttMain.SetToolTip(btnEdit, "Passer en mode modification")
        ttMain.SetToolTip(btnSave, "Enregistrer les modifications")
        ttMain.SetToolTip(btnCancel, "Annuler les modifications")
        ttMain.SetToolTip(btnDelete, "Supprimer l'éditeur sélectionné")
        ttMain.SetToolTip(btnClose, "Fermer la fenêtre")

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' ClearErrors
    '
    ' Réinitialise les erreurs d'interface.
    '------------------------------------------------------------
    Private Sub ClearErrors()

        errProvider.SetError(txtNomFormat, "")
        errProvider.SetError(txtExtension, "")
        errProvider.SetError(txtMimeType, "")

    End Sub

#End Region

#Region "Gestion des modes"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' SetMode
    '
    ' Gère l'état de l'interface selon le mode d'édition.
    '------------------------------------------------------------
    Private Sub SetMode(mode As ModeEdition)

        _mode = mode

        Dim hasSelection As Boolean =
            dgvFormatFile IsNot Nothing AndAlso
            dgvFormatFile.CurrentRow IsNot Nothing AndAlso
            dgvFormatFile.Rows.Count > 0

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

        End Select

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' HasSelectedFormatFile
    '
    ' Vérifie qu'une ligne est sélectionnée dans la grille.
    '------------------------------------------------------------
    Private Function HasSelectedFormatFile() As Boolean

        Return dgvFormatFile.SelectedRows.Count > 0

    End Function



#End Region

#Region "Interface utilisateur"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' SetStatus
    '
    ' Affiche un message dans la barre de statut.
    '------------------------------------------------------------
    Private Sub SetStatus(message As String)

        If stsLabelStatus Is Nothing Then Return
        stsLabelStatus.Text = message

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' BindSelectedToDetails
    '
    ' Charge le format de fichier sélectionné dans les champs détails.
    '------------------------------------------------------------
    Private Sub BindSelectedToDetails()

        If dgvFormatFile.CurrentRow Is Nothing Then
            ClearDetails()
            _currentId = 0
            SetMode(ModeEdition.Consultation)
            Return
        End If

        Dim row As DataGridViewRow = dgvFormatFile.CurrentRow

        txtIdFormatFile.Text = row.Cells("id_formatFile").Value?.ToString()
        txtCodeFormatFile.Text = row.Cells("code_formatFile").Value?.ToString()

        txtNomFormat.Text = row.Cells("nom_format").Value?.ToString()
        txtExtension.Text = row.Cells("extension").Value?.ToString()
        txtMimeType.Text = row.Cells("mime_type").Value?.ToString()

        nudOrdreAffichage.Value = DbToInt(row.Cells("ordre_affichage").Value)
        chkIsActif.Checked = DbToBool(row.Cells("is_actif").Value)

        If Not String.IsNullOrWhiteSpace(txtIdFormatFile.Text) Then
            _currentId = Convert.ToUInt64(txtIdFormatFile.Text)
        Else
            _currentId = 0
        End If

        SetMode(ModeEdition.Consultation)

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
    ' 📌 V1.0 - 13/03/2026
    ' ClearDetails
    '
    ' Réinitialise les champs de détail.
    '------------------------------------------------------------
    Private Sub ClearDetails()

        txtIdFormatFile.Clear()
        txtCodeFormatFile.Clear()

        txtNomFormat.Clear()
        txtExtension.Clear()
        txtMimeType.Clear()

        nudOrdreAffichage.Value = 1
        chkIsActif.Checked = True

    End Sub

#End Region

#Region "Actions utilisateur (CRUD)"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' btnNew_Click
    '
    ' Prépare la création d'un nouveau format.
    '------------------------------------------------------------
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try

            ' ClearErrors()
            ClearDetails()

            _currentId = 0

            SetMode(ModeEdition.Nouveau)
            SetStatus("Création d'un nouveau format.")

            txtNomFormat.Focus()

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur btnNew_Click (FormatFile).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de la création.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' btnEdit_Click
    '
    ' Passe en mode modification.
    '------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try

            If Not HasSelectedFormatFile() Then
                SetStatus("Aucun format sélectionné.")
                Return
            End If

            '   ClearErrors()

            SetMode(ModeEdition.Modification)
            SetStatus("Modification du format.")

            txtNomFormat.Focus()

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur btnEdit_Click (FormatFile).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors du passage en modification.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' btnCancel_Click
    '
    ' Annule l'édition en cours.
    '------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try

            ClearErrors()

            If dgvFormatFile.SelectedRows.Count > 0 Then
                BindSelectedToDetails()
            ElseIf dgvFormatFile.Rows.Count > 0 Then
                UtilsForm.SelectFirstRow(dgvFormatFile, "nom_format")
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

            SetMode(ModeEdition.Consultation)
            SetStatus("Edition annulée.")

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur btnCancel_Click (FormatFile).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de l'annulation.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' btnSave_Click
    '
    ' Enregistre un nouveau format ou une modification.
    '------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If _mode = ModeEdition.Consultation Then Return

        Try

            ClearErrors()

            Dim current As FormatFile = GetCurrentFormatFileFromFields()

            ' Validation minimale
            If String.IsNullOrWhiteSpace(current.NomFormat) Then
                errProvider.SetError(txtNomFormat, "Le nom du format est obligatoire.")
                txtNomFormat.Focus()
                Return
            End If

            If _mode = ModeEdition.Nouveau Then

                GestionReferentiel.FormatFile_Insert(
                current.NomFormat,
                current.Extension,
                current.MimeType,
                current.OrdreAffichage,
                current.IsActif
            )

                SetStatus("Format de fichier créé.")

            ElseIf _mode = ModeEdition.Modification Then

                GestionReferentiel.FormatFile_Update(
                current.IdFormatFile,
                current.NomFormat,
                current.Extension,
                current.MimeType,
                current.OrdreAffichage,
                current.IsActif
            )

                SetStatus("Format de fichier modifié.")

            End If

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur btnSave_Click (FormatFile).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de l'enregistrement.")

        End Try

    End Sub


    '------------------------------------------------------------
    ' 📌 V1.1 - 13/03/2026
    ' btnDelete_Click
    '
    ' Supprime un format de fichier avec contrôle FK.
    '------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If _mode <> ModeEdition.Consultation Then
            SetStatus("Suppression impossible pendant une édition.")
            Return
        End If

        If Not HasSelectedFormatFile() Then
            SetStatus("Aucun format sélectionné.")
            Return
        End If

        Dim row As DataGridViewRow = dgvFormatFile.SelectedRows(0)

        Dim id As ULong = 0UL
        If row.Cells("id_formatFile").Value IsNot Nothing AndAlso row.Cells("id_formatFile").Value IsNot DBNull.Value Then
            id = Convert.ToUInt64(row.Cells("id_formatFile").Value)
        End If
        If id = 0UL Then
            SetStatus("Identifiant format de fichier invalide.")
            Return
        End If

        Dim nom As String = row.Cells("nom_format").Value?.ToString()

        Try

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
            $"Supprimer le format '{nom}' ?",
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

            SetStatus("Format supprimé.")

            LoadGrid()
            SetMode(ModeEdition.Consultation)

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur btnDelete_Click (FormatFile).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de la suppression.")

        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' btnClose_Click
    '
    ' Ferme la fenêtre.
    '------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

#End Region

#Region "Synchronisation des données"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' LoadGrid
    '
    ' Charge la grille des formats de fichier,
    ' applique le format référentiel
    ' et sélectionne automatiquement la première ligne si présente.
    '------------------------------------------------------------
    Private Sub LoadGrid()

        Try

            Dim dt As DataTable = GestionReferentiel.FormatFile_GetAll()

            dgvFormatFile.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvFormatFile)

            ' Colonnes techniques
            If dgvFormatFile.Columns.Contains("id_formatFile") Then
                dgvFormatFile.Columns("id_formatFile").Visible = False
            End If

            If dgvFormatFile.Columns.Contains("created_at") Then
                dgvFormatFile.Columns("created_at").Visible = False
            End If

            If dgvFormatFile.Columns.Contains("updated_at") Then
                dgvFormatFile.Columns("updated_at").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} format(s)"

            If UtilsForm.SelectFirstRow(dgvFormatFile, "nom_format") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur LoadGrid (FormatFile).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors du chargement des formats de fichier.")

        End Try

    End Sub



#End Region

#Region "Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' GetCurrentFormatFileFromFields
    '
    ' Construit un objet métier à partir des champs de la form.
    '------------------------------------------------------------
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

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' ApplySearch
    '
    ' Applique le filtre de recherche sur les formats de fichier
    ' et sélectionne automatiquement la première ligne si présente.
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
                dt = GestionReferentiel.FormatFile_GetAll()
                SetStatus("Liste complète affichée.")
            Else
                dt = GestionReferentiel.FormatFile_GetBySearch(searchText)
                SetStatus($"Filtre appliqué : '{searchText}'")
            End If

            dgvFormatFile.DataSource = dt

            UtilsForm.FormatReferentielGrid(dgvFormatFile)

            If dgvFormatFile.Columns.Contains("id_formatFile") Then
                dgvFormatFile.Columns("id_formatFile").Visible = False
            End If

            If dgvFormatFile.Columns.Contains("created_at") Then
                dgvFormatFile.Columns("created_at").Visible = False
            End If

            If dgvFormatFile.Columns.Contains("updated_at") Then
                dgvFormatFile.Columns("updated_at").Visible = False
            End If

            lblCount.Text = $"{dt.Rows.Count} format(s)"

            If UtilsForm.SelectFirstRow(dgvFormatFile, "nom_format") Then
                BindSelectedToDetails()
            Else
                ClearDetails()
                _currentId = 0
            End If

        Catch ex As Exception

            GestionLog.EcrireLog(
            "UI: erreur ApplySearch (FormatFile).",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.UI,
            ex
        )

            SetStatus("Erreur lors de la recherche.")

        End Try

    End Sub
    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' btnSearch_Click
    '
    ' Lance la recherche.
    '------------------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' btnClearSearch_Click
    '
    ' Réinitialise la recherche.
    '------------------------------------------------------------
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        txtSearch.Clear()
        ApplySearch()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' txtSearch_KeyDown
    '
    ' Lance la recherche avec la touche Entrée.
    '------------------------------------------------------------
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ApplySearch()
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' dgvFormatFile_SelectionChanged
    '
    ' Recharge les détails lors du changement de sélection.
    '------------------------------------------------------------
    Private Sub dgvFormatFile_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFormatFile.SelectionChanged

        If _mode <> ModeEdition.Consultation Then Return

        BindSelectedToDetails()

    End Sub



#End Region





End Class