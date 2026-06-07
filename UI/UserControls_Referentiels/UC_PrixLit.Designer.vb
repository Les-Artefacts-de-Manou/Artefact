<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_PrixLit
    Inherits System.Windows.Forms.UserControl

    'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        pnlMain = New Panel()
        tlpCenter = New TableLayoutPanel()
        grpPrixLit = New GroupBox()
        dgvPrixLit = New DataGridView()
        tlpPrixLitDetails = New TableLayoutPanel()
        lblNomPrixLit = New Label()
        txtNomPrixLit = New TextBox()
        lblDescriptionPrixLit = New Label()
        txtDescriptionPrixLit = New TextBox()
        lblNotesPrixLit = New Label()
        pnlRichTextContainer = New Panel()
        rtbNotesPrixLit = New RichTextBox()
        ucRichTextToolbar = New UC_RichTextToolbar()
        chkPrixLitActif = New CheckBox()
        lblCodePrixLit = New Label()
        txtCodePrixLit = New TextBox()
        txtIdPrixLit = New TextBox()
        pnlActionsPrixLit = New Panel()
        btnNewPrixLit = New Button()
        btnEditPrixLit = New Button()
        btnSavePrixLit = New Button()
        btnCancelPrixLit = New Button()
        btnDeletePrixLit = New Button()
        pnlTopSearchPrixLit = New Panel()
        lblSearchPrixLit = New Label()
        txtSearchPrixLit = New TextBox()
        btnSearchPrixLit = New Button()
        btnClearSearchPrixLit = New Button()
        chkPrixLitActifs = New CheckBox()
        chkRechercherDansNotes = New CheckBox()
        lblCountPrixLit = New Label()
        grpCategories = New GroupBox()
        dgvCategories = New DataGridView()
        tlpCategorieDetails = New TableLayoutPanel()
        lblLibelleCategorie = New Label()
        txtLibelleCategorie = New TextBox()
        lblDescriptionCategorie = New Label()
        txtDescriptionCategorie = New TextBox()
        lblOrdreCategorie = New Label()
        nudOrdreCategorie = New NumericUpDown()
        chkCategorieActive = New CheckBox()
        lblCodeCategorie = New Label()
        txtCodeCategorie = New TextBox()
        txtIdCategorie = New TextBox()
        pnlActionsCategorie = New Panel()
        btnNewCategorie = New Button()
        btnEditCategorie = New Button()
        btnSaveCategorie = New Button()
        btnCancelCategorie = New Button()
        btnDeleteCategorie = New Button()
        pnlTopCategories = New Panel()
        lblCountCategories = New Label()
        grpAnnees = New GroupBox()
        dgvAnnees = New DataGridView()
        tlpAnneeDetails = New TableLayoutPanel()
        lblAnnee = New Label()
        nudAnnee = New NumericUpDown()
        lblCodeAnnee = New Label()
        txtCodeAnnee = New TextBox()
        txtIdAnnee = New TextBox()
        pnlActionsAnnee = New Panel()
        btnNewAnnee = New Button()
        btnEditAnnee = New Button()
        btnSaveAnnee = New Button()
        btnCancelAnnee = New Button()
        btnDeleteAnnee = New Button()
        pnlTopAnnees = New Panel()
        lblCountAnnees = New Label()
        pnlMain.SuspendLayout()
        tlpCenter.SuspendLayout()
        grpPrixLit.SuspendLayout()
        CType(dgvPrixLit, ComponentModel.ISupportInitialize).BeginInit()
        tlpPrixLitDetails.SuspendLayout()
        pnlRichTextContainer.SuspendLayout()
        pnlActionsPrixLit.SuspendLayout()
        pnlTopSearchPrixLit.SuspendLayout()
        grpCategories.SuspendLayout()
        CType(dgvCategories, ComponentModel.ISupportInitialize).BeginInit()
        tlpCategorieDetails.SuspendLayout()
        CType(nudOrdreCategorie, ComponentModel.ISupportInitialize).BeginInit()
        pnlActionsCategorie.SuspendLayout()
        pnlTopCategories.SuspendLayout()
        grpAnnees.SuspendLayout()
        CType(dgvAnnees, ComponentModel.ISupportInitialize).BeginInit()
        tlpAnneeDetails.SuspendLayout()
        CType(nudAnnee, ComponentModel.ISupportInitialize).BeginInit()
        pnlActionsAnnee.SuspendLayout()
        pnlTopAnnees.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.OldLace
        pnlMain.Controls.Add(tlpCenter)
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(8)
        pnlMain.Size = New Size(1700, 800)
        pnlMain.TabIndex = 0
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 3
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 35F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpCenter.Controls.Add(grpPrixLit, 0, 0)
        tlpCenter.Controls.Add(grpCategories, 1, 0)
        tlpCenter.Controls.Add(grpAnnees, 2, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 8)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(1684, 784)
        tlpCenter.TabIndex = 0
        ' 
        ' grpPrixLit
        ' 
        grpPrixLit.Controls.Add(dgvPrixLit)
        grpPrixLit.Controls.Add(tlpPrixLitDetails)
        grpPrixLit.Controls.Add(pnlActionsPrixLit)
        grpPrixLit.Controls.Add(pnlTopSearchPrixLit)
        grpPrixLit.Dock = DockStyle.Fill
        grpPrixLit.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpPrixLit.Location = New Point(3, 3)
        grpPrixLit.Name = "grpPrixLit"
        grpPrixLit.Size = New Size(667, 778)
        grpPrixLit.TabIndex = 0
        grpPrixLit.TabStop = False
        grpPrixLit.Text = "PRIX LITTÉRAIRES"
        ' 
        ' dgvPrixLit
        ' 
        dgvPrixLit.AllowUserToAddRows = False
        dgvPrixLit.AllowUserToDeleteRows = False
        dgvPrixLit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPrixLit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvPrixLit.DefaultCellStyle = DataGridViewCellStyle4
        dgvPrixLit.Dock = DockStyle.Fill
        dgvPrixLit.Location = New Point(3, 89)
        dgvPrixLit.MultiSelect = False
        dgvPrixLit.Name = "dgvPrixLit"
        dgvPrixLit.ReadOnly = True
        dgvPrixLit.RowHeadersVisible = False
        dgvPrixLit.RowTemplate.Height = 28
        dgvPrixLit.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPrixLit.Size = New Size(661, 248)
        dgvPrixLit.TabIndex = 1
        ' 
        ' tlpPrixLitDetails
        ' 
        tlpPrixLitDetails.ColumnCount = 2
        tlpPrixLitDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100F))
        tlpPrixLitDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpPrixLitDetails.Controls.Add(lblNomPrixLit, 0, 0)
        tlpPrixLitDetails.Controls.Add(txtNomPrixLit, 1, 0)
        tlpPrixLitDetails.Controls.Add(lblDescriptionPrixLit, 0, 1)
        tlpPrixLitDetails.Controls.Add(txtDescriptionPrixLit, 1, 1)
        tlpPrixLitDetails.Controls.Add(lblNotesPrixLit, 0, 2)
        tlpPrixLitDetails.Controls.Add(pnlRichTextContainer, 1, 2)
        tlpPrixLitDetails.Controls.Add(chkPrixLitActif, 1, 3)
        tlpPrixLitDetails.Controls.Add(lblCodePrixLit, 0, 4)
        tlpPrixLitDetails.Controls.Add(txtCodePrixLit, 1, 4)
        tlpPrixLitDetails.Controls.Add(txtIdPrixLit, 1, 5)
        tlpPrixLitDetails.Dock = DockStyle.Bottom
        tlpPrixLitDetails.Font = New Font("Segoe UI", 9F)
        tlpPrixLitDetails.Location = New Point(3, 337)
        tlpPrixLitDetails.Name = "tlpPrixLitDetails"
        tlpPrixLitDetails.Padding = New Padding(3)
        tlpPrixLitDetails.RowCount = 7
        tlpPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 80F))
        tlpPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpPrixLitDetails.Size = New Size(661, 391)
        tlpPrixLitDetails.TabIndex = 2
        ' 
        ' lblNomPrixLit
        ' 
        lblNomPrixLit.Anchor = AnchorStyles.Left
        lblNomPrixLit.AutoSize = True
        lblNomPrixLit.Location = New Point(6, 8)
        lblNomPrixLit.Name = "lblNomPrixLit"
        lblNomPrixLit.Size = New Size(34, 15)
        lblNomPrixLit.TabIndex = 0
        lblNomPrixLit.Text = "Nom"
        ' 
        ' txtNomPrixLit
        ' 
        txtNomPrixLit.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtNomPrixLit.Location = New Point(106, 6)
        txtNomPrixLit.MaxLength = 200
        txtNomPrixLit.Name = "txtNomPrixLit"
        txtNomPrixLit.Size = New Size(549, 23)
        txtNomPrixLit.TabIndex = 0
        ' 
        ' lblDescriptionPrixLit
        ' 
        lblDescriptionPrixLit.Anchor = AnchorStyles.Left
        lblDescriptionPrixLit.AutoSize = True
        lblDescriptionPrixLit.Location = New Point(6, 61)
        lblDescriptionPrixLit.Name = "lblDescriptionPrixLit"
        lblDescriptionPrixLit.Size = New Size(67, 15)
        lblDescriptionPrixLit.TabIndex = 2
        lblDescriptionPrixLit.Text = "Description"
        ' 
        ' txtDescriptionPrixLit
        ' 
        txtDescriptionPrixLit.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtDescriptionPrixLit.Location = New Point(106, 35)
        txtDescriptionPrixLit.MaxLength = 200
        txtDescriptionPrixLit.Multiline = True
        txtDescriptionPrixLit.Name = "txtDescriptionPrixLit"
        txtDescriptionPrixLit.Size = New Size(549, 68)
        txtDescriptionPrixLit.TabIndex = 1
        ' 
        ' lblNotesPrixLit
        ' 
        lblNotesPrixLit.Anchor = AnchorStyles.Left
        lblNotesPrixLit.AutoSize = True
        lblNotesPrixLit.Location = New Point(6, 210)
        lblNotesPrixLit.Name = "lblNotesPrixLit"
        lblNotesPrixLit.Size = New Size(38, 15)
        lblNotesPrixLit.TabIndex = 4
        lblNotesPrixLit.Text = "Notes"
        ' 
        ' pnlRichTextContainer
        ' 
        pnlRichTextContainer.Controls.Add(rtbNotesPrixLit)
        pnlRichTextContainer.Controls.Add(ucRichTextToolbar)
        pnlRichTextContainer.Dock = DockStyle.Fill
        pnlRichTextContainer.Location = New Point(106, 112)
        pnlRichTextContainer.Name = "pnlRichTextContainer"
        pnlRichTextContainer.Size = New Size(549, 211)
        pnlRichTextContainer.TabIndex = 2
        ' 
        ' rtbNotesPrixLit
        ' 
        rtbNotesPrixLit.Dock = DockStyle.Fill
        rtbNotesPrixLit.Location = New Point(0, 30)
        rtbNotesPrixLit.Name = "rtbNotesPrixLit"
        rtbNotesPrixLit.Size = New Size(549, 181)
        rtbNotesPrixLit.TabIndex = 1
        rtbNotesPrixLit.Text = ""
        ' 
        ' ucRichTextToolbar
        ' 
        ucRichTextToolbar.Dock = DockStyle.Top
        ucRichTextToolbar.Location = New Point(0, 0)
        ucRichTextToolbar.Name = "ucRichTextToolbar"
        ucRichTextToolbar.Size = New Size(549, 30)
        ucRichTextToolbar.TabIndex = 0
        ucRichTextToolbar.TargetRichTextBox = Nothing
        ' 
        ' chkPrixLitActif
        ' 
        chkPrixLitActif.Anchor = AnchorStyles.Left
        chkPrixLitActif.AutoSize = True
        chkPrixLitActif.Checked = True
        chkPrixLitActif.CheckState = CheckState.Checked
        chkPrixLitActif.Location = New Point(106, 329)
        chkPrixLitActif.Name = "chkPrixLitActif"
        chkPrixLitActif.Size = New Size(51, 19)
        chkPrixLitActif.TabIndex = 3
        chkPrixLitActif.Text = "Actif"
        chkPrixLitActif.UseVisualStyleBackColor = True
        ' 
        ' lblCodePrixLit
        ' 
        lblCodePrixLit.Anchor = AnchorStyles.Left
        lblCodePrixLit.AutoSize = True
        lblCodePrixLit.Location = New Point(6, 357)
        lblCodePrixLit.Name = "lblCodePrixLit"
        lblCodePrixLit.Size = New Size(70, 15)
        lblCodePrixLit.TabIndex = 8
        lblCodePrixLit.Text = "Code (auto)"
        ' 
        ' txtCodePrixLit
        ' 
        txtCodePrixLit.Anchor = AnchorStyles.Left
        txtCodePrixLit.BackColor = SystemColors.Control
        txtCodePrixLit.Location = New Point(106, 355)
        txtCodePrixLit.Name = "txtCodePrixLit"
        txtCodePrixLit.ReadOnly = True
        txtCodePrixLit.Size = New Size(110, 23)
        txtCodePrixLit.TabIndex = 7
        txtCodePrixLit.TabStop = False
        ' 
        ' txtIdPrixLit
        ' 
        txtIdPrixLit.Location = New Point(106, 381)
        txtIdPrixLit.Name = "txtIdPrixLit"
        txtIdPrixLit.Size = New Size(1, 23)
        txtIdPrixLit.TabIndex = 6
        txtIdPrixLit.TabStop = False
        txtIdPrixLit.Visible = False
        ' 
        ' pnlActionsPrixLit
        ' 
        pnlActionsPrixLit.Controls.Add(btnNewPrixLit)
        pnlActionsPrixLit.Controls.Add(btnEditPrixLit)
        pnlActionsPrixLit.Controls.Add(btnSavePrixLit)
        pnlActionsPrixLit.Controls.Add(btnCancelPrixLit)
        pnlActionsPrixLit.Controls.Add(btnDeletePrixLit)
        pnlActionsPrixLit.Dock = DockStyle.Bottom
        pnlActionsPrixLit.Location = New Point(3, 728)
        pnlActionsPrixLit.Name = "pnlActionsPrixLit"
        pnlActionsPrixLit.Size = New Size(661, 47)
        pnlActionsPrixLit.TabIndex = 3
        ' 
        ' btnNewPrixLit
        ' 
        btnNewPrixLit.Font = New Font("Segoe UI", 9F)
        btnNewPrixLit.Location = New Point(8, 9)
        btnNewPrixLit.Name = "btnNewPrixLit"
        btnNewPrixLit.Size = New Size(78, 28)
        btnNewPrixLit.TabIndex = 0
        btnNewPrixLit.Text = "➕ Nouveau"
        btnNewPrixLit.UseVisualStyleBackColor = True
        ' 
        ' btnEditPrixLit
        ' 
        btnEditPrixLit.Font = New Font("Segoe UI", 9F)
        btnEditPrixLit.Location = New Point(79, 9)
        btnEditPrixLit.Name = "btnEditPrixLit"
        btnEditPrixLit.Size = New Size(75, 28)
        btnEditPrixLit.TabIndex = 1
        btnEditPrixLit.Text = "✏ Modifier"
        btnEditPrixLit.UseVisualStyleBackColor = True
        ' 
        ' btnSavePrixLit
        ' 
        btnSavePrixLit.Font = New Font("Segoe UI", 9F)
        btnSavePrixLit.Location = New Point(147, 9)
        btnSavePrixLit.Name = "btnSavePrixLit"
        btnSavePrixLit.Size = New Size(86, 28)
        btnSavePrixLit.TabIndex = 2
        btnSavePrixLit.Text = "💾 Enregistrer"
        btnSavePrixLit.UseVisualStyleBackColor = True
        ' 
        ' btnCancelPrixLit
        ' 
        btnCancelPrixLit.Font = New Font("Segoe UI", 9F)
        btnCancelPrixLit.Location = New Point(226, 9)
        btnCancelPrixLit.Name = "btnCancelPrixLit"
        btnCancelPrixLit.Size = New Size(70, 28)
        btnCancelPrixLit.TabIndex = 3
        btnCancelPrixLit.Text = "↩ Annuler"
        btnCancelPrixLit.UseVisualStyleBackColor = True
        ' 
        ' btnDeletePrixLit
        ' 
        btnDeletePrixLit.Font = New Font("Segoe UI", 9F)
        btnDeletePrixLit.Location = New Point(289, 9)
        btnDeletePrixLit.Name = "btnDeletePrixLit"
        btnDeletePrixLit.Size = New Size(25, 28)
        btnDeletePrixLit.TabIndex = 4
        btnDeletePrixLit.Text = "🗑"
        btnDeletePrixLit.UseVisualStyleBackColor = True
        ' 
        ' pnlTopSearchPrixLit
        ' 
        pnlTopSearchPrixLit.BackColor = Color.WhiteSmoke
        pnlTopSearchPrixLit.Controls.Add(lblSearchPrixLit)
        pnlTopSearchPrixLit.Controls.Add(txtSearchPrixLit)
        pnlTopSearchPrixLit.Controls.Add(btnSearchPrixLit)
        pnlTopSearchPrixLit.Controls.Add(btnClearSearchPrixLit)
        pnlTopSearchPrixLit.Controls.Add(chkPrixLitActifs)
        pnlTopSearchPrixLit.Controls.Add(chkRechercherDansNotes)
        pnlTopSearchPrixLit.Controls.Add(lblCountPrixLit)
        pnlTopSearchPrixLit.Dock = DockStyle.Top
        pnlTopSearchPrixLit.Location = New Point(3, 19)
        pnlTopSearchPrixLit.Name = "pnlTopSearchPrixLit"
        pnlTopSearchPrixLit.Size = New Size(661, 70)
        pnlTopSearchPrixLit.TabIndex = 0
        ' 
        ' lblSearchPrixLit
        ' 
        lblSearchPrixLit.AutoSize = True
        lblSearchPrixLit.Font = New Font("Segoe UI", 9F)
        lblSearchPrixLit.Location = New Point(8, 15)
        lblSearchPrixLit.Name = "lblSearchPrixLit"
        lblSearchPrixLit.Size = New Size(62, 15)
        lblSearchPrixLit.TabIndex = 0
        lblSearchPrixLit.Text = "Recherche"
        ' 
        ' txtSearchPrixLit
        ' 
        txtSearchPrixLit.Font = New Font("Segoe UI", 9F)
        txtSearchPrixLit.Location = New Point(75, 12)
        txtSearchPrixLit.Name = "txtSearchPrixLit"
        txtSearchPrixLit.Size = New Size(180, 23)
        txtSearchPrixLit.TabIndex = 1
        ' 
        ' btnSearchPrixLit
        ' 
        btnSearchPrixLit.Font = New Font("Segoe UI", 9F)
        btnSearchPrixLit.Location = New Point(260, 11)
        btnSearchPrixLit.Name = "btnSearchPrixLit"
        btnSearchPrixLit.Size = New Size(30, 25)
        btnSearchPrixLit.TabIndex = 2
        btnSearchPrixLit.Text = "🔍"
        btnSearchPrixLit.UseVisualStyleBackColor = True
        ' 
        ' btnClearSearchPrixLit
        ' 
        btnClearSearchPrixLit.Font = New Font("Segoe UI", 9F)
        btnClearSearchPrixLit.Location = New Point(295, 11)
        btnClearSearchPrixLit.Name = "btnClearSearchPrixLit"
        btnClearSearchPrixLit.Size = New Size(30, 25)
        btnClearSearchPrixLit.TabIndex = 3
        btnClearSearchPrixLit.Text = "✖"
        btnClearSearchPrixLit.UseVisualStyleBackColor = True
        ' 
        ' chkPrixLitActifs
        ' 
        chkPrixLitActifs.AutoSize = True
        chkPrixLitActifs.Checked = True
        chkPrixLitActifs.CheckState = CheckState.Checked
        chkPrixLitActifs.Font = New Font("Segoe UI", 9F)
        chkPrixLitActifs.Location = New Point(335, 14)
        chkPrixLitActifs.Name = "chkPrixLitActifs"
        chkPrixLitActifs.Size = New Size(56, 19)
        chkPrixLitActifs.TabIndex = 4
        chkPrixLitActifs.Text = "Actifs"
        chkPrixLitActifs.UseVisualStyleBackColor = True
        ' 
        ' chkRechercherDansNotes
        ' 
        chkRechercherDansNotes.AutoSize = True
        chkRechercherDansNotes.Font = New Font("Segoe UI", 9F)
        chkRechercherDansNotes.Location = New Point(8, 42)
        chkRechercherDansNotes.Name = "chkRechercherDansNotes"
        chkRechercherDansNotes.Size = New Size(162, 19)
        chkRechercherDansNotes.TabIndex = 5
        chkRechercherDansNotes.Text = "Rechercher dans les notes"
        chkRechercherDansNotes.UseVisualStyleBackColor = True
        ' 
        ' lblCountPrixLit
        ' 
        lblCountPrixLit.Anchor = AnchorStyles.Right
        lblCountPrixLit.AutoSize = True
        lblCountPrixLit.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCountPrixLit.Location = New Point(590, 25)
        lblCountPrixLit.Name = "lblCountPrixLit"
        lblCountPrixLit.Size = New Size(39, 15)
        lblCountPrixLit.TabIndex = 5
        lblCountPrixLit.Text = "0 prix"
        lblCountPrixLit.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpCategories
        ' 
        grpCategories.Controls.Add(dgvCategories)
        grpCategories.Controls.Add(tlpCategorieDetails)
        grpCategories.Controls.Add(pnlActionsCategorie)
        grpCategories.Controls.Add(pnlTopCategories)
        grpCategories.Dock = DockStyle.Fill
        grpCategories.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpCategories.Location = New Point(676, 3)
        grpCategories.Name = "grpCategories"
        grpCategories.Size = New Size(583, 778)
        grpCategories.TabIndex = 1
        grpCategories.TabStop = False
        grpCategories.Text = "CATÉGORIES"
        ' 
        ' dgvCategories
        ' 
        dgvCategories.AllowUserToAddRows = False
        dgvCategories.AllowUserToDeleteRows = False
        dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        dgvCategories.DefaultCellStyle = DataGridViewCellStyle5
        dgvCategories.Dock = DockStyle.Fill
        dgvCategories.Location = New Point(3, 89)
        dgvCategories.MultiSelect = False
        dgvCategories.Name = "dgvCategories"
        dgvCategories.ReadOnly = True
        dgvCategories.RowHeadersVisible = False
        dgvCategories.RowTemplate.Height = 28
        dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCategories.Size = New Size(577, 438)
        dgvCategories.TabIndex = 1
        ' 
        ' tlpCategorieDetails
        ' 
        tlpCategorieDetails.ColumnCount = 2
        tlpCategorieDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80F))
        tlpCategorieDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCategorieDetails.Controls.Add(lblLibelleCategorie, 0, 0)
        tlpCategorieDetails.Controls.Add(txtLibelleCategorie, 1, 0)
        tlpCategorieDetails.Controls.Add(lblDescriptionCategorie, 0, 1)
        tlpCategorieDetails.Controls.Add(txtDescriptionCategorie, 1, 1)
        tlpCategorieDetails.Controls.Add(lblOrdreCategorie, 0, 2)
        tlpCategorieDetails.Controls.Add(nudOrdreCategorie, 1, 2)
        tlpCategorieDetails.Controls.Add(chkCategorieActive, 1, 3)
        tlpCategorieDetails.Controls.Add(lblCodeCategorie, 0, 4)
        tlpCategorieDetails.Controls.Add(txtCodeCategorie, 1, 4)
        tlpCategorieDetails.Controls.Add(txtIdCategorie, 1, 5)
        tlpCategorieDetails.Dock = DockStyle.Bottom
        tlpCategorieDetails.Font = New Font("Segoe UI", 9F)
        tlpCategorieDetails.Location = New Point(3, 527)
        tlpCategorieDetails.Name = "tlpCategorieDetails"
        tlpCategorieDetails.Padding = New Padding(3)
        tlpCategorieDetails.RowCount = 7
        tlpCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 60F))
        tlpCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 1F))
        tlpCategorieDetails.Size = New Size(577, 201)
        tlpCategorieDetails.TabIndex = 2
        ' 
        ' lblLibelleCategorie
        ' 
        lblLibelleCategorie.Anchor = AnchorStyles.Left
        lblLibelleCategorie.AutoSize = True
        lblLibelleCategorie.Location = New Point(6, 8)
        lblLibelleCategorie.Name = "lblLibelleCategorie"
        lblLibelleCategorie.Size = New Size(41, 15)
        lblLibelleCategorie.TabIndex = 0
        lblLibelleCategorie.Text = "Libellé"
        ' 
        ' txtLibelleCategorie
        ' 
        txtLibelleCategorie.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtLibelleCategorie.Location = New Point(86, 6)
        txtLibelleCategorie.MaxLength = 200
        txtLibelleCategorie.Name = "txtLibelleCategorie"
        txtLibelleCategorie.Size = New Size(485, 23)
        txtLibelleCategorie.TabIndex = 0
        ' 
        ' lblDescriptionCategorie
        ' 
        lblDescriptionCategorie.Anchor = AnchorStyles.Left
        lblDescriptionCategorie.AutoSize = True
        lblDescriptionCategorie.Location = New Point(6, 51)
        lblDescriptionCategorie.Name = "lblDescriptionCategorie"
        lblDescriptionCategorie.Size = New Size(67, 15)
        lblDescriptionCategorie.TabIndex = 2
        lblDescriptionCategorie.Text = "Description"
        ' 
        ' txtDescriptionCategorie
        ' 
        txtDescriptionCategorie.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtDescriptionCategorie.Location = New Point(86, 35)
        txtDescriptionCategorie.MaxLength = 200
        txtDescriptionCategorie.Multiline = True
        txtDescriptionCategorie.Name = "txtDescriptionCategorie"
        txtDescriptionCategorie.Size = New Size(485, 47)
        txtDescriptionCategorie.TabIndex = 1
        ' 
        ' lblOrdreCategorie
        ' 
        lblOrdreCategorie.Anchor = AnchorStyles.Left
        lblOrdreCategorie.AutoSize = True
        lblOrdreCategorie.Location = New Point(6, 94)
        lblOrdreCategorie.Name = "lblOrdreCategorie"
        lblOrdreCategorie.Size = New Size(37, 15)
        lblOrdreCategorie.TabIndex = 4
        lblOrdreCategorie.Text = "Ordre"
        ' 
        ' nudOrdreCategorie
        ' 
        nudOrdreCategorie.Anchor = AnchorStyles.Left
        nudOrdreCategorie.Location = New Point(86, 92)
        nudOrdreCategorie.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        nudOrdreCategorie.Minimum = New Decimal(New Integer() {10000, 0, 0, Integer.MinValue})
        nudOrdreCategorie.Name = "nudOrdreCategorie"
        nudOrdreCategorie.Size = New Size(80, 23)
        nudOrdreCategorie.TabIndex = 2
        ' 
        ' chkCategorieActive
        ' 
        chkCategorieActive.Anchor = AnchorStyles.Left
        chkCategorieActive.AutoSize = True
        chkCategorieActive.Checked = True
        chkCategorieActive.CheckState = CheckState.Checked
        chkCategorieActive.Location = New Point(86, 118)
        chkCategorieActive.Name = "chkCategorieActive"
        chkCategorieActive.Size = New Size(59, 19)
        chkCategorieActive.TabIndex = 3
        chkCategorieActive.Text = "Active"
        chkCategorieActive.UseVisualStyleBackColor = True
        ' 
        ' lblCodeCategorie
        ' 
        lblCodeCategorie.Anchor = AnchorStyles.Left
        lblCodeCategorie.AutoSize = True
        lblCodeCategorie.Location = New Point(6, 146)
        lblCodeCategorie.Name = "lblCodeCategorie"
        lblCodeCategorie.Size = New Size(70, 15)
        lblCodeCategorie.TabIndex = 8
        lblCodeCategorie.Text = "Code (auto)"
        ' 
        ' txtCodeCategorie
        ' 
        txtCodeCategorie.Anchor = AnchorStyles.Left
        txtCodeCategorie.BackColor = SystemColors.Control
        txtCodeCategorie.Location = New Point(86, 144)
        txtCodeCategorie.Name = "txtCodeCategorie"
        txtCodeCategorie.ReadOnly = True
        txtCodeCategorie.Size = New Size(110, 23)
        txtCodeCategorie.TabIndex = 7
        txtCodeCategorie.TabStop = False
        ' 
        ' txtIdCategorie
        ' 
        txtIdCategorie.Location = New Point(86, 170)
        txtIdCategorie.Name = "txtIdCategorie"
        txtIdCategorie.Size = New Size(1, 23)
        txtIdCategorie.TabIndex = 6
        txtIdCategorie.TabStop = False
        txtIdCategorie.Visible = False
        ' 
        ' pnlActionsCategorie
        ' 
        pnlActionsCategorie.Controls.Add(btnNewCategorie)
        pnlActionsCategorie.Controls.Add(btnEditCategorie)
        pnlActionsCategorie.Controls.Add(btnSaveCategorie)
        pnlActionsCategorie.Controls.Add(btnCancelCategorie)
        pnlActionsCategorie.Controls.Add(btnDeleteCategorie)
        pnlActionsCategorie.Dock = DockStyle.Bottom
        pnlActionsCategorie.Location = New Point(3, 728)
        pnlActionsCategorie.Name = "pnlActionsCategorie"
        pnlActionsCategorie.Size = New Size(577, 47)
        pnlActionsCategorie.TabIndex = 3
        ' 
        ' btnNewCategorie
        ' 
        btnNewCategorie.Font = New Font("Segoe UI", 9F)
        btnNewCategorie.Location = New Point(9, 9)
        btnNewCategorie.Name = "btnNewCategorie"
        btnNewCategorie.Size = New Size(78, 28)
        btnNewCategorie.TabIndex = 0
        btnNewCategorie.Text = "➕ Nouveau"
        btnNewCategorie.UseVisualStyleBackColor = True
        ' 
        ' btnEditCategorie
        ' 
        btnEditCategorie.Font = New Font("Segoe UI", 9F)
        btnEditCategorie.Location = New Point(85, 9)
        btnEditCategorie.Name = "btnEditCategorie"
        btnEditCategorie.Size = New Size(75, 28)
        btnEditCategorie.TabIndex = 1
        btnEditCategorie.Text = "✏ Modifier"
        btnEditCategorie.UseVisualStyleBackColor = True
        ' 
        ' btnSaveCategorie
        ' 
        btnSaveCategorie.Font = New Font("Segoe UI", 9F)
        btnSaveCategorie.Location = New Point(158, 9)
        btnSaveCategorie.Name = "btnSaveCategorie"
        btnSaveCategorie.Size = New Size(86, 28)
        btnSaveCategorie.TabIndex = 2
        btnSaveCategorie.Text = "💾 Enregistrer"
        btnSaveCategorie.UseVisualStyleBackColor = True
        ' 
        ' btnCancelCategorie
        ' 
        btnCancelCategorie.Font = New Font("Segoe UI", 9F)
        btnCancelCategorie.Location = New Point(242, 9)
        btnCancelCategorie.Name = "btnCancelCategorie"
        btnCancelCategorie.Size = New Size(70, 28)
        btnCancelCategorie.TabIndex = 3
        btnCancelCategorie.Text = "↩ Annuler"
        btnCancelCategorie.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteCategorie
        ' 
        btnDeleteCategorie.Font = New Font("Segoe UI", 9F)
        btnDeleteCategorie.Location = New Point(310, 9)
        btnDeleteCategorie.Name = "btnDeleteCategorie"
        btnDeleteCategorie.Size = New Size(25, 28)
        btnDeleteCategorie.TabIndex = 4
        btnDeleteCategorie.Text = "🗑 Supprimer"
        btnDeleteCategorie.UseVisualStyleBackColor = True
        ' 
        ' pnlTopCategories
        ' 
        pnlTopCategories.BackColor = Color.WhiteSmoke
        pnlTopCategories.Controls.Add(lblCountCategories)
        pnlTopCategories.Dock = DockStyle.Top
        pnlTopCategories.Location = New Point(3, 19)
        pnlTopCategories.Name = "pnlTopCategories"
        pnlTopCategories.Size = New Size(577, 70)
        pnlTopCategories.TabIndex = 0
        ' 
        ' lblCountCategories
        ' 
        lblCountCategories.AutoSize = True
        lblCountCategories.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCountCategories.Location = New Point(475, 25)
        lblCountCategories.Name = "lblCountCategories"
        lblCountCategories.Size = New Size(70, 15)
        lblCountCategories.TabIndex = 0
        lblCountCategories.Text = "0 catégorie"
        lblCountCategories.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' grpAnnees
        ' 
        grpAnnees.Controls.Add(dgvAnnees)
        grpAnnees.Controls.Add(tlpAnneeDetails)
        grpAnnees.Controls.Add(pnlActionsAnnee)
        grpAnnees.Controls.Add(pnlTopAnnees)
        grpAnnees.Dock = DockStyle.Fill
        grpAnnees.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        grpAnnees.Location = New Point(1265, 3)
        grpAnnees.Name = "grpAnnees"
        grpAnnees.Size = New Size(416, 778)
        grpAnnees.TabIndex = 2
        grpAnnees.TabStop = False
        grpAnnees.Text = "ANNÉES"
        ' 
        ' dgvAnnees
        ' 
        dgvAnnees.AllowUserToAddRows = False
        dgvAnnees.AllowUserToDeleteRows = False
        dgvAnnees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAnnees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = SystemColors.Window
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle6.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        dgvAnnees.DefaultCellStyle = DataGridViewCellStyle6
        dgvAnnees.Dock = DockStyle.Fill
        dgvAnnees.Location = New Point(3, 89)
        dgvAnnees.MultiSelect = False
        dgvAnnees.Name = "dgvAnnees"
        dgvAnnees.ReadOnly = True
        dgvAnnees.RowHeadersVisible = False
        dgvAnnees.RowTemplate.Height = 28
        dgvAnnees.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAnnees.Size = New Size(410, 567)
        dgvAnnees.TabIndex = 1
        ' 
        ' tlpAnneeDetails
        ' 
        tlpAnneeDetails.ColumnCount = 2
        tlpAnneeDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80F))
        tlpAnneeDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpAnneeDetails.Controls.Add(lblAnnee, 0, 0)
        tlpAnneeDetails.Controls.Add(nudAnnee, 1, 0)
        tlpAnneeDetails.Controls.Add(lblCodeAnnee, 0, 1)
        tlpAnneeDetails.Controls.Add(txtCodeAnnee, 1, 1)
        tlpAnneeDetails.Controls.Add(txtIdAnnee, 1, 2)
        tlpAnneeDetails.Dock = DockStyle.Bottom
        tlpAnneeDetails.Font = New Font("Segoe UI", 9F)
        tlpAnneeDetails.Location = New Point(3, 656)
        tlpAnneeDetails.Name = "tlpAnneeDetails"
        tlpAnneeDetails.Padding = New Padding(3)
        tlpAnneeDetails.RowCount = 4
        tlpAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpAnneeDetails.Size = New Size(410, 72)
        tlpAnneeDetails.TabIndex = 2
        ' 
        ' lblAnnee
        ' 
        lblAnnee.Anchor = AnchorStyles.Left
        lblAnnee.AutoSize = True
        lblAnnee.Location = New Point(6, 8)
        lblAnnee.Name = "lblAnnee"
        lblAnnee.Size = New Size(41, 15)
        lblAnnee.TabIndex = 0
        lblAnnee.Text = "Année"
        ' 
        ' nudAnnee
        ' 
        nudAnnee.Anchor = AnchorStyles.Left
        nudAnnee.Location = New Point(86, 6)
        nudAnnee.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        nudAnnee.Minimum = New Decimal(New Integer() {1900, 0, 0, 0})
        nudAnnee.Name = "nudAnnee"
        nudAnnee.Size = New Size(100, 23)
        nudAnnee.TabIndex = 0
        nudAnnee.Value = New Decimal(New Integer() {2024, 0, 0, 0})
        ' 
        ' lblCodeAnnee
        ' 
        lblCodeAnnee.Anchor = AnchorStyles.Left
        lblCodeAnnee.AutoSize = True
        lblCodeAnnee.Location = New Point(6, 34)
        lblCodeAnnee.Name = "lblCodeAnnee"
        lblCodeAnnee.Size = New Size(70, 15)
        lblCodeAnnee.TabIndex = 4
        lblCodeAnnee.Text = "Code (auto)"
        ' 
        ' txtCodeAnnee
        ' 
        txtCodeAnnee.Anchor = AnchorStyles.Left
        txtCodeAnnee.BackColor = SystemColors.Control
        txtCodeAnnee.Location = New Point(86, 32)
        txtCodeAnnee.Name = "txtCodeAnnee"
        txtCodeAnnee.ReadOnly = True
        txtCodeAnnee.Size = New Size(110, 23)
        txtCodeAnnee.TabIndex = 3
        txtCodeAnnee.TabStop = False
        ' 
        ' txtIdAnnee
        ' 
        txtIdAnnee.Location = New Point(86, 58)
        txtIdAnnee.Name = "txtIdAnnee"
        txtIdAnnee.Size = New Size(1, 23)
        txtIdAnnee.TabIndex = 2
        txtIdAnnee.TabStop = False
        txtIdAnnee.Visible = False
        ' 
        ' pnlActionsAnnee
        ' 
        pnlActionsAnnee.Controls.Add(btnNewAnnee)
        pnlActionsAnnee.Controls.Add(btnEditAnnee)
        pnlActionsAnnee.Controls.Add(btnSaveAnnee)
        pnlActionsAnnee.Controls.Add(btnCancelAnnee)
        pnlActionsAnnee.Controls.Add(btnDeleteAnnee)
        pnlActionsAnnee.Dock = DockStyle.Bottom
        pnlActionsAnnee.Location = New Point(3, 728)
        pnlActionsAnnee.Name = "pnlActionsAnnee"
        pnlActionsAnnee.Size = New Size(410, 47)
        pnlActionsAnnee.TabIndex = 3
        ' 
        ' btnNewAnnee
        ' 
        btnNewAnnee.Font = New Font("Segoe UI", 9F)
        btnNewAnnee.Location = New Point(3, 9)
        btnNewAnnee.Name = "btnNewAnnee"
        btnNewAnnee.Size = New Size(78, 28)
        btnNewAnnee.TabIndex = 0
        btnNewAnnee.Text = "➕ Nouveau"
        btnNewAnnee.UseVisualStyleBackColor = True
        ' 
        ' btnEditAnnee
        ' 
        btnEditAnnee.Font = New Font("Segoe UI", 9F)
        btnEditAnnee.Location = New Point(76, 9)
        btnEditAnnee.Name = "btnEditAnnee"
        btnEditAnnee.Size = New Size(75, 28)
        btnEditAnnee.TabIndex = 1
        btnEditAnnee.Text = "✏ Modifier"
        btnEditAnnee.UseVisualStyleBackColor = True
        ' 
        ' btnSaveAnnee
        ' 
        btnSaveAnnee.Font = New Font("Segoe UI", 9F)
        btnSaveAnnee.Location = New Point(146, 9)
        btnSaveAnnee.Name = "btnSaveAnnee"
        btnSaveAnnee.Size = New Size(86, 28)
        btnSaveAnnee.TabIndex = 2
        btnSaveAnnee.Text = "💾 Enregistrer"
        btnSaveAnnee.UseVisualStyleBackColor = True
        ' 
        ' btnCancelAnnee
        ' 
        btnCancelAnnee.Font = New Font("Segoe UI", 9F)
        btnCancelAnnee.Location = New Point(227, 9)
        btnCancelAnnee.Name = "btnCancelAnnee"
        btnCancelAnnee.Size = New Size(70, 28)
        btnCancelAnnee.TabIndex = 3
        btnCancelAnnee.Text = "↩ Annuler"
        btnCancelAnnee.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteAnnee
        ' 
        btnDeleteAnnee.Font = New Font("Segoe UI", 9F)
        btnDeleteAnnee.Location = New Point(292, 9)
        btnDeleteAnnee.Name = "btnDeleteAnnee"
        btnDeleteAnnee.Size = New Size(25, 28)
        btnDeleteAnnee.TabIndex = 4
        btnDeleteAnnee.Text = "🗑 Supprimer"
        btnDeleteAnnee.UseVisualStyleBackColor = True
        ' 
        ' pnlTopAnnees
        ' 
        pnlTopAnnees.BackColor = Color.WhiteSmoke
        pnlTopAnnees.Controls.Add(lblCountAnnees)
        pnlTopAnnees.Dock = DockStyle.Top
        pnlTopAnnees.Location = New Point(3, 19)
        pnlTopAnnees.Name = "pnlTopAnnees"
        pnlTopAnnees.Size = New Size(410, 70)
        pnlTopAnnees.TabIndex = 0
        ' 
        ' lblCountAnnees
        ' 
        lblCountAnnees.Anchor = AnchorStyles.Right
        lblCountAnnees.AutoSize = True
        lblCountAnnees.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCountAnnees.Location = New Point(330, 25)
        lblCountAnnees.Name = "lblCountAnnees"
        lblCountAnnees.Size = New Size(51, 15)
        lblCountAnnees.TabIndex = 0
        lblCountAnnees.Text = "0 année"
        lblCountAnnees.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UC_PrixLit
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlMain)
        Name = "UC_PrixLit"
        Size = New Size(1700, 800)
        pnlMain.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        grpPrixLit.ResumeLayout(False)
        CType(dgvPrixLit, ComponentModel.ISupportInitialize).EndInit()
        tlpPrixLitDetails.ResumeLayout(False)
        tlpPrixLitDetails.PerformLayout()
        pnlRichTextContainer.ResumeLayout(False)
        pnlActionsPrixLit.ResumeLayout(False)
        pnlTopSearchPrixLit.ResumeLayout(False)
        pnlTopSearchPrixLit.PerformLayout()
        grpCategories.ResumeLayout(False)
        CType(dgvCategories, ComponentModel.ISupportInitialize).EndInit()
        tlpCategorieDetails.ResumeLayout(False)
        tlpCategorieDetails.PerformLayout()
        CType(nudOrdreCategorie, ComponentModel.ISupportInitialize).EndInit()
        pnlActionsCategorie.ResumeLayout(False)
        pnlTopCategories.ResumeLayout(False)
        pnlTopCategories.PerformLayout()
        grpAnnees.ResumeLayout(False)
        CType(dgvAnnees, ComponentModel.ISupportInitialize).EndInit()
        tlpAnneeDetails.ResumeLayout(False)
        tlpAnneeDetails.PerformLayout()
        CType(nudAnnee, ComponentModel.ISupportInitialize).EndInit()
        pnlActionsAnnee.ResumeLayout(False)
        pnlTopAnnees.ResumeLayout(False)
        pnlTopAnnees.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents tlpCenter As TableLayoutPanel

    ' Volet PrixLit (gauche - 40%)
    Friend WithEvents grpPrixLit As GroupBox
    Friend WithEvents dgvPrixLit As DataGridView
    Friend WithEvents pnlTopSearchPrixLit As Panel
    Friend WithEvents lblSearchPrixLit As Label
    Friend WithEvents txtSearchPrixLit As TextBox
    Friend WithEvents btnSearchPrixLit As Button
    Friend WithEvents btnClearSearchPrixLit As Button
    Friend WithEvents chkPrixLitActifs As CheckBox
    Friend WithEvents chkRechercherDansNotes As CheckBox
    Friend WithEvents lblCountPrixLit As Label
    Friend WithEvents tlpPrixLitDetails As TableLayoutPanel
    Friend WithEvents lblNomPrixLit As Label
    Friend WithEvents txtNomPrixLit As TextBox
    Friend WithEvents lblDescriptionPrixLit As Label
    Friend WithEvents txtDescriptionPrixLit As TextBox
    Friend WithEvents lblNotesPrixLit As Label
    Friend WithEvents pnlRichTextContainer As Panel
    Friend WithEvents rtbNotesPrixLit As RichTextBox
    Friend WithEvents ucRichTextToolbar As UC_RichTextToolbar
    Friend WithEvents chkPrixLitActif As CheckBox
    Friend WithEvents lblCodePrixLit As Label
    Friend WithEvents txtCodePrixLit As TextBox
    Friend WithEvents txtIdPrixLit As TextBox
    Friend WithEvents pnlActionsPrixLit As Panel
    Friend WithEvents btnNewPrixLit As Button
    Friend WithEvents btnEditPrixLit As Button
    Friend WithEvents btnSavePrixLit As Button
    Friend WithEvents btnCancelPrixLit As Button
    Friend WithEvents btnDeletePrixLit As Button

    ' Volet Catégories (centre - 30%)
    Friend WithEvents grpCategories As GroupBox
    Friend WithEvents dgvCategories As DataGridView
    Friend WithEvents pnlTopCategories As Panel
    Friend WithEvents lblCountCategories As Label
    Friend WithEvents tlpCategorieDetails As TableLayoutPanel
    Friend WithEvents lblLibelleCategorie As Label
    Friend WithEvents txtLibelleCategorie As TextBox
    Friend WithEvents lblDescriptionCategorie As Label
    Friend WithEvents txtDescriptionCategorie As TextBox
    Friend WithEvents lblOrdreCategorie As Label
    Friend WithEvents nudOrdreCategorie As NumericUpDown
    Friend WithEvents chkCategorieActive As CheckBox
    Friend WithEvents lblCodeCategorie As Label
    Friend WithEvents txtCodeCategorie As TextBox
    Friend WithEvents txtIdCategorie As TextBox
    Friend WithEvents pnlActionsCategorie As Panel
    Friend WithEvents btnNewCategorie As Button
    Friend WithEvents btnEditCategorie As Button
    Friend WithEvents btnSaveCategorie As Button
    Friend WithEvents btnCancelCategorie As Button
    Friend WithEvents btnDeleteCategorie As Button

    ' Volet Années (droite - 30%)
    Friend WithEvents grpAnnees As GroupBox
    Friend WithEvents dgvAnnees As DataGridView
    Friend WithEvents pnlTopAnnees As Panel
    Friend WithEvents lblCountAnnees As Label
    Friend WithEvents tlpAnneeDetails As TableLayoutPanel
    Friend WithEvents lblAnnee As Label
    Friend WithEvents nudAnnee As NumericUpDown
    Friend WithEvents lblCodeAnnee As Label
    Friend WithEvents txtCodeAnnee As TextBox
    Friend WithEvents txtIdAnnee As TextBox
    Friend WithEvents pnlActionsAnnee As Panel
    Friend WithEvents btnNewAnnee As Button
    Friend WithEvents btnEditAnnee As Button
    Friend WithEvents btnSaveAnnee As Button
    Friend WithEvents btnCancelAnnee As Button
    Friend WithEvents btnDeleteAnnee As Button

End Class
