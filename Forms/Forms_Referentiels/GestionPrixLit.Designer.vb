<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionPrixLit
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
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

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GestionPrixLit))
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        lblTitreForm = New Label()
        pnlForm = New Panel()
        tabMain = New TabControl()
        tabPrixLit = New TabPage()
        tlpCenterPrixLit = New TableLayoutPanel()
        dgvPrixLit = New DataGridView()
        grpPrixLitDetails = New GroupBox()
        tblPrixLitDetails = New TableLayoutPanel()
        rtbNotesPrixLit = New RichTextBox()
        tsNotes = New ToolStrip()
        btnBold = New ToolStripButton()
        btnItalic = New ToolStripButton()
        btnUnderline = New ToolStripButton()
        btnBullet = New ToolStripButton()
        btnTab = New ToolStripButton()
        lblNotes = New Label()
        txtDescriptionPrixLit = New TextBox()
        lblCodePrixLit = New Label()
        txtCodePrixLit = New TextBox()
        lblNomPrixLit = New Label()
        txtNomPrixLit = New TextBox()
        lblDescriptionPrixLit = New Label()
        chkPrixLitActif = New CheckBox()
        txtidPrixLit = New TextBox()
        tabPrixLitCategorie = New TabPage()
        tlpCenterCategorie = New TableLayoutPanel()
        dgvPrixLitCategorie = New DataGridView()
        grpPrixLitCategorieDetails = New GroupBox()
        tblPrixLitCategorieDetails = New TableLayoutPanel()
        cboPrixLitParentCategorie = New ComboBox()
        txtidprixlitcategorie = New TextBox()
        chkPrixLitCategorieActif = New CheckBox()
        txtDescriptionCategorie = New TextBox()
        lblDescriptionCategorie = New Label()
        txtLibelleCategorie = New TextBox()
        txtCodePrixLitCategorie = New TextBox()
        lblCodeCat = New Label()
        lblPrixLitParentCategorie = New Label()
        lblLibelleCategorie = New Label()
        lblOrdreAffichageCategorie = New Label()
        nudOrdreAffichageCategorie = New NumericUpDown()
        tabPrixLitAnnee = New TabPage()
        tblCenterPrixLitAnnee = New TableLayoutPanel()
        dgvPrixLitAnnee = New DataGridView()
        grpPrixLitAnneeDetails = New GroupBox()
        tblPrixLitAnneeDetails = New TableLayoutPanel()
        lblCodePrixLitAnnee = New Label()
        txtCodePrixLitAnnee = New TextBox()
        txtIdPrixLitAnnee = New TextBox()
        nudAnneePrixLit = New NumericUpDown()
        lblAnneePrixLit = New Label()
        lblPrixLitParentAnnee = New Label()
        txtPrixLitParentAnnee = New TextBox()
        lblCategoriePrixLitAnnee = New Label()
        cboPrixLitCategorieAnnee = New ComboBox()
        txtCategorieParentAnnee = New TextBox()
        lblCategorieParentAnnee = New Label()
        pnlActions = New Panel()
        btnEdit = New Button()
        btnClose = New Button()
        btnDelete = New Button()
        btnCancel = New Button()
        btnSave = New Button()
        btnNew = New Button()
        pnlTop = New Panel()
        chkSearchNotes = New CheckBox()
        lblCount = New Label()
        chkActifsOnly = New CheckBox()
        cboFiltrePrixLit = New ComboBox()
        lblFiltrePrixLit = New Label()
        btnClearSearch = New Button()
        btnSearch = New Button()
        txtSearch = New TextBox()
        lblSearch = New Label()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        stsStatus.SuspendLayout()
        pnlForm.SuspendLayout()
        tabMain.SuspendLayout()
        tabPrixLit.SuspendLayout()
        tlpCenterPrixLit.SuspendLayout()
        CType(dgvPrixLit, ComponentModel.ISupportInitialize).BeginInit()
        grpPrixLitDetails.SuspendLayout()
        tblPrixLitDetails.SuspendLayout()
        tsNotes.SuspendLayout()
        tabPrixLitCategorie.SuspendLayout()
        tlpCenterCategorie.SuspendLayout()
        CType(dgvPrixLitCategorie, ComponentModel.ISupportInitialize).BeginInit()
        grpPrixLitCategorieDetails.SuspendLayout()
        tblPrixLitCategorieDetails.SuspendLayout()
        CType(nudOrdreAffichageCategorie, ComponentModel.ISupportInitialize).BeginInit()
        tabPrixLitAnnee.SuspendLayout()
        tblCenterPrixLitAnnee.SuspendLayout()
        CType(dgvPrixLitAnnee, ComponentModel.ISupportInitialize).BeginInit()
        grpPrixLitAnneeDetails.SuspendLayout()
        tblPrixLitAnneeDetails.SuspendLayout()
        CType(nudAnneePrixLit, ComponentModel.ISupportInitialize).BeginInit()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(8, 688)
        stsStatus.Name = "stsStatus"
        stsStatus.Size = New Size(1157, 22)
        stsStatus.TabIndex = 18
        stsStatus.Text = "StatusStrip1"
        ' 
        ' stsLabelStatus
        ' 
        stsLabelStatus.Font = New Font("Calibri", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        stsLabelStatus.ForeColor = Color.Tomato
        stsLabelStatus.Name = "stsLabelStatus"
        stsLabelStatus.Size = New Size(52, 17)
        stsLabelStatus.Text = "Artefact"
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.Dock = DockStyle.Top
        lblTitreForm.Font = New Font("Calibri", 14F, FontStyle.Bold)
        lblTitreForm.Location = New Point(8, 4)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Padding = New Padding(0, 0, 0, 4)
        lblTitreForm.Size = New Size(220, 27)
        lblTitreForm.TabIndex = 19
        lblTitreForm.Text = "Gestion des Prix littéraires"
        ' 
        ' pnlForm
        ' 
        pnlForm.BackColor = Color.FloralWhite
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tabMain)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(8, 31)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(1157, 657)
        pnlForm.TabIndex = 20
        ' 
        ' tabMain
        ' 
        tabMain.Controls.Add(tabPrixLit)
        tabMain.Controls.Add(tabPrixLitCategorie)
        tabMain.Controls.Add(tabPrixLitAnnee)
        tabMain.Dock = DockStyle.Fill
        tabMain.Location = New Point(8, 74)
        tabMain.Name = "tabMain"
        tabMain.SelectedIndex = 0
        tabMain.Size = New Size(1137, 523)
        tabMain.TabIndex = 7
        ' 
        ' tabPrixLit
        ' 
        tabPrixLit.Controls.Add(tlpCenterPrixLit)
        tabPrixLit.Location = New Point(4, 24)
        tabPrixLit.Name = "tabPrixLit"
        tabPrixLit.Padding = New Padding(3)
        tabPrixLit.Size = New Size(1129, 495)
        tabPrixLit.TabIndex = 0
        tabPrixLit.Text = "Prix littéraire"
        tabPrixLit.UseVisualStyleBackColor = True
        ' 
        ' tlpCenterPrixLit
        ' 
        tlpCenterPrixLit.ColumnCount = 2
        tlpCenterPrixLit.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 54.5454559F))
        tlpCenterPrixLit.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45.4545441F))
        tlpCenterPrixLit.Controls.Add(dgvPrixLit, 0, 0)
        tlpCenterPrixLit.Controls.Add(grpPrixLitDetails, 1, 0)
        tlpCenterPrixLit.Dock = DockStyle.Fill
        tlpCenterPrixLit.Location = New Point(3, 3)
        tlpCenterPrixLit.Name = "tlpCenterPrixLit"
        tlpCenterPrixLit.RowCount = 1
        tlpCenterPrixLit.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenterPrixLit.Size = New Size(1123, 489)
        tlpCenterPrixLit.TabIndex = 0
        ' 
        ' dgvPrixLit
        ' 
        dgvPrixLit.AllowUserToAddRows = False
        dgvPrixLit.AllowUserToDeleteRows = False
        dgvPrixLit.AllowUserToResizeColumns = False
        dgvPrixLit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPrixLit.BorderStyle = BorderStyle.Fixed3D
        dgvPrixLit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPrixLit.Dock = DockStyle.Fill
        dgvPrixLit.Location = New Point(3, 3)
        dgvPrixLit.MultiSelect = False
        dgvPrixLit.Name = "dgvPrixLit"
        dgvPrixLit.ReadOnly = True
        dgvPrixLit.RowHeadersVisible = False
        dgvPrixLit.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPrixLit.Size = New Size(606, 483)
        dgvPrixLit.TabIndex = 2
        ' 
        ' grpPrixLitDetails
        ' 
        grpPrixLitDetails.Controls.Add(tblPrixLitDetails)
        grpPrixLitDetails.Dock = DockStyle.Fill
        grpPrixLitDetails.Location = New Point(615, 3)
        grpPrixLitDetails.Name = "grpPrixLitDetails"
        grpPrixLitDetails.Padding = New Padding(6)
        grpPrixLitDetails.Size = New Size(505, 483)
        grpPrixLitDetails.TabIndex = 3
        grpPrixLitDetails.TabStop = False
        grpPrixLitDetails.Text = "Détails"
        ' 
        ' tblPrixLitDetails
        ' 
        tblPrixLitDetails.AutoSize = True
        tblPrixLitDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tblPrixLitDetails.ColumnCount = 2
        tblPrixLitDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tblPrixLitDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblPrixLitDetails.Controls.Add(rtbNotesPrixLit, 0, 4)
        tblPrixLitDetails.Controls.Add(tsNotes, 1, 3)
        tblPrixLitDetails.Controls.Add(lblNotes, 0, 3)
        tblPrixLitDetails.Controls.Add(txtDescriptionPrixLit, 1, 2)
        tblPrixLitDetails.Controls.Add(lblCodePrixLit, 0, 0)
        tblPrixLitDetails.Controls.Add(txtCodePrixLit, 1, 0)
        tblPrixLitDetails.Controls.Add(lblNomPrixLit, 0, 1)
        tblPrixLitDetails.Controls.Add(txtNomPrixLit, 1, 1)
        tblPrixLitDetails.Controls.Add(lblDescriptionPrixLit, 0, 2)
        tblPrixLitDetails.Controls.Add(chkPrixLitActif, 0, 5)
        tblPrixLitDetails.Controls.Add(txtidPrixLit, 1, 5)
        tblPrixLitDetails.Dock = DockStyle.Fill
        tblPrixLitDetails.Location = New Point(6, 22)
        tblPrixLitDetails.Name = "tblPrixLitDetails"
        tblPrixLitDetails.RowCount = 6
        tblPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tblPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 12F))
        tblPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 12F))
        tblPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tblPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 48F))
        tblPrixLitDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tblPrixLitDetails.Size = New Size(493, 455)
        tblPrixLitDetails.TabIndex = 0
        ' 
        ' rtbNotesPrixLit
        ' 
        tblPrixLitDetails.SetColumnSpan(rtbNotesPrixLit, 2)
        rtbNotesPrixLit.Dock = DockStyle.Fill
        rtbNotesPrixLit.Location = New Point(3, 196)
        rtbNotesPrixLit.Name = "rtbNotesPrixLit"
        rtbNotesPrixLit.Size = New Size(487, 212)
        rtbNotesPrixLit.TabIndex = 40
        rtbNotesPrixLit.Text = ""
        ' 
        ' tsNotes
        ' 
        tsNotes.Anchor = AnchorStyles.Bottom
        tsNotes.AutoSize = False
        tsNotes.Dock = DockStyle.None
        tsNotes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        tsNotes.Items.AddRange(New ToolStripItem() {btnBold, btnItalic, btnUnderline, btnBullet, btnTab})
        tsNotes.Location = New Point(196, 168)
        tsNotes.Name = "tsNotes"
        tsNotes.Size = New Size(220, 25)
        tsNotes.TabIndex = 37
        tsNotes.Text = "ToolStrip1"
        ' 
        ' btnBold
        ' 
        btnBold.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnBold.Image = CType(resources.GetObject("btnBold.Image"), Image)
        btnBold.ImageTransparentColor = Color.Magenta
        btnBold.Name = "btnBold"
        btnBold.Size = New Size(23, 22)
        btnBold.Text = "B"
        btnBold.ToolTipText = "Gras"
        ' 
        ' btnItalic
        ' 
        btnItalic.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnItalic.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        btnItalic.Image = CType(resources.GetObject("btnItalic.Image"), Image)
        btnItalic.ImageTransparentColor = Color.Magenta
        btnItalic.Name = "btnItalic"
        btnItalic.Size = New Size(23, 22)
        btnItalic.Text = "I"
        btnItalic.ToolTipText = "Italic"
        ' 
        ' btnUnderline
        ' 
        btnUnderline.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnUnderline.Font = New Font("Segoe UI", 9F, FontStyle.Underline)
        btnUnderline.Image = CType(resources.GetObject("btnUnderline.Image"), Image)
        btnUnderline.ImageTransparentColor = Color.Magenta
        btnUnderline.Name = "btnUnderline"
        btnUnderline.Size = New Size(23, 22)
        btnUnderline.Text = "U"
        btnUnderline.ToolTipText = "Souligné"
        ' 
        ' btnBullet
        ' 
        btnBullet.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnBullet.Image = CType(resources.GetObject("btnBullet.Image"), Image)
        btnBullet.ImageTransparentColor = Color.Magenta
        btnBullet.Name = "btnBullet"
        btnBullet.Size = New Size(23, 22)
        btnBullet.Text = "*"
        btnBullet.ToolTipText = "Bullet"
        ' 
        ' btnTab
        ' 
        btnTab.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTab.Image = CType(resources.GetObject("btnTab.Image"), Image)
        btnTab.ImageTransparentColor = Color.Magenta
        btnTab.Name = "btnTab"
        btnTab.Size = New Size(29, 22)
        btnTab.Text = "-->"
        btnTab.ToolTipText = "Tab"
        ' 
        ' lblNotes
        ' 
        lblNotes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblNotes.AutoSize = True
        lblNotes.Location = New Point(3, 178)
        lblNotes.Name = "lblNotes"
        lblNotes.Size = New Size(38, 15)
        lblNotes.TabIndex = 36
        lblNotes.Text = "Notes"
        ' 
        ' txtDescriptionPrixLit
        ' 
        txtDescriptionPrixLit.Location = New Point(123, 102)
        txtDescriptionPrixLit.MaxLength = 200
        txtDescriptionPrixLit.Multiline = True
        txtDescriptionPrixLit.Name = "txtDescriptionPrixLit"
        txtDescriptionPrixLit.Size = New Size(367, 23)
        txtDescriptionPrixLit.TabIndex = 22
        ' 
        ' lblCodePrixLit
        ' 
        lblCodePrixLit.AutoSize = True
        lblCodePrixLit.Location = New Point(3, 0)
        lblCodePrixLit.Name = "lblCodePrixLit"
        lblCodePrixLit.Size = New Size(103, 15)
        lblCodePrixLit.TabIndex = 18
        lblCodePrixLit.Text = "Code Prix littéraire"
        ' 
        ' txtCodePrixLit
        ' 
        txtCodePrixLit.Location = New Point(123, 3)
        txtCodePrixLit.Name = "txtCodePrixLit"
        txtCodePrixLit.ReadOnly = True
        txtCodePrixLit.Size = New Size(100, 23)
        txtCodePrixLit.TabIndex = 17
        ' 
        ' lblNomPrixLit
        ' 
        lblNomPrixLit.AutoSize = True
        lblNomPrixLit.Location = New Point(3, 45)
        lblNomPrixLit.Name = "lblNomPrixLit"
        lblNomPrixLit.Size = New Size(76, 30)
        lblNomPrixLit.TabIndex = 19
        lblNomPrixLit.Text = "Nom du prix littéraire"
        ' 
        ' txtNomPrixLit
        ' 
        txtNomPrixLit.Location = New Point(123, 48)
        txtNomPrixLit.MaxLength = 200
        txtNomPrixLit.Multiline = True
        txtNomPrixLit.Name = "txtNomPrixLit"
        txtNomPrixLit.Size = New Size(367, 23)
        txtNomPrixLit.TabIndex = 20
        ' 
        ' lblDescriptionPrixLit
        ' 
        lblDescriptionPrixLit.AutoSize = True
        lblDescriptionPrixLit.Location = New Point(3, 99)
        lblDescriptionPrixLit.Name = "lblDescriptionPrixLit"
        lblDescriptionPrixLit.Size = New Size(70, 15)
        lblDescriptionPrixLit.TabIndex = 21
        lblDescriptionPrixLit.Text = "Description "
        ' 
        ' chkPrixLitActif
        ' 
        chkPrixLitActif.AutoSize = True
        chkPrixLitActif.Checked = True
        chkPrixLitActif.CheckState = CheckState.Checked
        chkPrixLitActif.Location = New Point(3, 414)
        chkPrixLitActif.Name = "chkPrixLitActif"
        chkPrixLitActif.Size = New Size(60, 19)
        chkPrixLitActif.TabIndex = 38
        chkPrixLitActif.Text = "is actif"
        chkPrixLitActif.UseVisualStyleBackColor = True
        ' 
        ' txtidPrixLit
        ' 
        txtidPrixLit.Location = New Point(123, 414)
        txtidPrixLit.Name = "txtidPrixLit"
        txtidPrixLit.Size = New Size(100, 23)
        txtidPrixLit.TabIndex = 39
        txtidPrixLit.TabStop = False
        txtidPrixLit.Visible = False
        ' 
        ' tabPrixLitCategorie
        ' 
        tabPrixLitCategorie.Controls.Add(tlpCenterCategorie)
        tabPrixLitCategorie.Location = New Point(4, 24)
        tabPrixLitCategorie.Name = "tabPrixLitCategorie"
        tabPrixLitCategorie.Padding = New Padding(3)
        tabPrixLitCategorie.Size = New Size(1129, 495)
        tabPrixLitCategorie.TabIndex = 1
        tabPrixLitCategorie.Text = "Categorie de prix"
        tabPrixLitCategorie.UseVisualStyleBackColor = True
        ' 
        ' tlpCenterCategorie
        ' 
        tlpCenterCategorie.ColumnCount = 2
        tlpCenterCategorie.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 54.5454559F))
        tlpCenterCategorie.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45.4545441F))
        tlpCenterCategorie.Controls.Add(dgvPrixLitCategorie, 0, 0)
        tlpCenterCategorie.Controls.Add(grpPrixLitCategorieDetails, 1, 0)
        tlpCenterCategorie.Dock = DockStyle.Fill
        tlpCenterCategorie.Location = New Point(3, 3)
        tlpCenterCategorie.Name = "tlpCenterCategorie"
        tlpCenterCategorie.RowCount = 1
        tlpCenterCategorie.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenterCategorie.Size = New Size(1123, 489)
        tlpCenterCategorie.TabIndex = 1
        ' 
        ' dgvPrixLitCategorie
        ' 
        dgvPrixLitCategorie.AllowUserToAddRows = False
        dgvPrixLitCategorie.AllowUserToDeleteRows = False
        dgvPrixLitCategorie.AllowUserToResizeColumns = False
        dgvPrixLitCategorie.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPrixLitCategorie.BorderStyle = BorderStyle.Fixed3D
        dgvPrixLitCategorie.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPrixLitCategorie.Dock = DockStyle.Fill
        dgvPrixLitCategorie.Location = New Point(3, 3)
        dgvPrixLitCategorie.MultiSelect = False
        dgvPrixLitCategorie.Name = "dgvPrixLitCategorie"
        dgvPrixLitCategorie.ReadOnly = True
        dgvPrixLitCategorie.RowHeadersVisible = False
        dgvPrixLitCategorie.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPrixLitCategorie.Size = New Size(606, 483)
        dgvPrixLitCategorie.TabIndex = 2
        ' 
        ' grpPrixLitCategorieDetails
        ' 
        grpPrixLitCategorieDetails.Controls.Add(tblPrixLitCategorieDetails)
        grpPrixLitCategorieDetails.Dock = DockStyle.Fill
        grpPrixLitCategorieDetails.Location = New Point(615, 3)
        grpPrixLitCategorieDetails.Name = "grpPrixLitCategorieDetails"
        grpPrixLitCategorieDetails.Padding = New Padding(6)
        grpPrixLitCategorieDetails.Size = New Size(505, 483)
        grpPrixLitCategorieDetails.TabIndex = 3
        grpPrixLitCategorieDetails.TabStop = False
        grpPrixLitCategorieDetails.Text = "Détails"
        ' 
        ' tblPrixLitCategorieDetails
        ' 
        tblPrixLitCategorieDetails.AutoSize = True
        tblPrixLitCategorieDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tblPrixLitCategorieDetails.ColumnCount = 2
        tblPrixLitCategorieDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tblPrixLitCategorieDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblPrixLitCategorieDetails.Controls.Add(cboPrixLitParentCategorie, 1, 0)
        tblPrixLitCategorieDetails.Controls.Add(txtidprixlitcategorie, 1, 5)
        tblPrixLitCategorieDetails.Controls.Add(chkPrixLitCategorieActif, 0, 5)
        tblPrixLitCategorieDetails.Controls.Add(txtDescriptionCategorie, 1, 3)
        tblPrixLitCategorieDetails.Controls.Add(lblDescriptionCategorie, 0, 3)
        tblPrixLitCategorieDetails.Controls.Add(txtLibelleCategorie, 1, 2)
        tblPrixLitCategorieDetails.Controls.Add(txtCodePrixLitCategorie, 1, 1)
        tblPrixLitCategorieDetails.Controls.Add(lblCodeCat, 0, 1)
        tblPrixLitCategorieDetails.Controls.Add(lblPrixLitParentCategorie, 0, 0)
        tblPrixLitCategorieDetails.Controls.Add(lblLibelleCategorie, 0, 2)
        tblPrixLitCategorieDetails.Controls.Add(lblOrdreAffichageCategorie, 0, 4)
        tblPrixLitCategorieDetails.Controls.Add(nudOrdreAffichageCategorie, 1, 4)
        tblPrixLitCategorieDetails.Dock = DockStyle.Fill
        tblPrixLitCategorieDetails.Location = New Point(6, 22)
        tblPrixLitCategorieDetails.Name = "tblPrixLitCategorieDetails"
        tblPrixLitCategorieDetails.RowCount = 6
        tblPrixLitCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 18F))
        tblPrixLitCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 12F))
        tblPrixLitCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 21F))
        tblPrixLitCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 23F))
        tblPrixLitCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 14F))
        tblPrixLitCategorieDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 12F))
        tblPrixLitCategorieDetails.Size = New Size(493, 455)
        tblPrixLitCategorieDetails.TabIndex = 1
        ' 
        ' cboPrixLitParentCategorie
        ' 
        cboPrixLitParentCategorie.DropDownStyle = ComboBoxStyle.DropDownList
        cboPrixLitParentCategorie.FormattingEnabled = True
        cboPrixLitParentCategorie.Location = New Point(123, 3)
        cboPrixLitParentCategorie.Name = "cboPrixLitParentCategorie"
        cboPrixLitParentCategorie.Size = New Size(289, 23)
        cboPrixLitParentCategorie.TabIndex = 41
        ' 
        ' txtidprixlitcategorie
        ' 
        txtidprixlitcategorie.Location = New Point(123, 400)
        txtidprixlitcategorie.Name = "txtidprixlitcategorie"
        txtidprixlitcategorie.Size = New Size(100, 23)
        txtidprixlitcategorie.TabIndex = 40
        txtidprixlitcategorie.TabStop = False
        txtidprixlitcategorie.Visible = False
        ' 
        ' chkPrixLitCategorieActif
        ' 
        chkPrixLitCategorieActif.AutoSize = True
        chkPrixLitCategorieActif.Checked = True
        chkPrixLitCategorieActif.CheckState = CheckState.Checked
        chkPrixLitCategorieActif.Location = New Point(3, 400)
        chkPrixLitCategorieActif.Name = "chkPrixLitCategorieActif"
        chkPrixLitCategorieActif.Size = New Size(60, 19)
        chkPrixLitCategorieActif.TabIndex = 34
        chkPrixLitCategorieActif.Text = "is actif"
        chkPrixLitCategorieActif.UseVisualStyleBackColor = True
        ' 
        ' txtDescriptionCategorie
        ' 
        txtDescriptionCategorie.Location = New Point(123, 233)
        txtDescriptionCategorie.MaxLength = 200
        txtDescriptionCategorie.Multiline = True
        txtDescriptionCategorie.Name = "txtDescriptionCategorie"
        txtDescriptionCategorie.Size = New Size(367, 23)
        txtDescriptionCategorie.TabIndex = 27
        ' 
        ' lblDescriptionCategorie
        ' 
        lblDescriptionCategorie.AutoSize = True
        lblDescriptionCategorie.Location = New Point(3, 230)
        lblDescriptionCategorie.Name = "lblDescriptionCategorie"
        lblDescriptionCategorie.Size = New Size(70, 30)
        lblDescriptionCategorie.TabIndex = 26
        lblDescriptionCategorie.Text = "Description Catégorie"
        ' 
        ' txtLibelleCategorie
        ' 
        txtLibelleCategorie.Location = New Point(123, 138)
        txtLibelleCategorie.MaxLength = 200
        txtLibelleCategorie.Multiline = True
        txtLibelleCategorie.Name = "txtLibelleCategorie"
        txtLibelleCategorie.Size = New Size(367, 23)
        txtLibelleCategorie.TabIndex = 25
        ' 
        ' txtCodePrixLitCategorie
        ' 
        txtCodePrixLitCategorie.Location = New Point(123, 84)
        txtCodePrixLitCategorie.Name = "txtCodePrixLitCategorie"
        txtCodePrixLitCategorie.ReadOnly = True
        txtCodePrixLitCategorie.Size = New Size(100, 23)
        txtCodePrixLitCategorie.TabIndex = 23
        ' 
        ' lblCodeCat
        ' 
        lblCodeCat.AutoSize = True
        lblCodeCat.Location = New Point(3, 81)
        lblCodeCat.Name = "lblCodeCat"
        lblCodeCat.Size = New Size(111, 15)
        lblCodeCat.TabIndex = 22
        lblCodeCat.Text = "Code Categorie Prix"
        ' 
        ' lblPrixLitParentCategorie
        ' 
        lblPrixLitParentCategorie.AutoSize = True
        lblPrixLitParentCategorie.Location = New Point(3, 0)
        lblPrixLitParentCategorie.Name = "lblPrixLitParentCategorie"
        lblPrixLitParentCategorie.Size = New Size(75, 15)
        lblPrixLitParentCategorie.TabIndex = 0
        lblPrixLitParentCategorie.Text = "Prix Littéraire"
        ' 
        ' lblLibelleCategorie
        ' 
        lblLibelleCategorie.AutoSize = True
        lblLibelleCategorie.Location = New Point(3, 135)
        lblLibelleCategorie.Name = "lblLibelleCategorie"
        lblLibelleCategorie.Size = New Size(95, 15)
        lblLibelleCategorie.TabIndex = 24
        lblLibelleCategorie.Text = "Libellé Catégorie"
        ' 
        ' lblOrdreAffichageCategorie
        ' 
        lblOrdreAffichageCategorie.AutoSize = True
        lblOrdreAffichageCategorie.Location = New Point(3, 334)
        lblOrdreAffichageCategorie.Name = "lblOrdreAffichageCategorie"
        lblOrdreAffichageCategorie.Size = New Size(99, 15)
        lblOrdreAffichageCategorie.TabIndex = 28
        lblOrdreAffichageCategorie.Text = "Ordre d'affichage"
        ' 
        ' nudOrdreAffichageCategorie
        ' 
        nudOrdreAffichageCategorie.Location = New Point(123, 337)
        nudOrdreAffichageCategorie.Name = "nudOrdreAffichageCategorie"
        nudOrdreAffichageCategorie.Size = New Size(120, 23)
        nudOrdreAffichageCategorie.TabIndex = 29
        ' 
        ' tabPrixLitAnnee
        ' 
        tabPrixLitAnnee.Controls.Add(tblCenterPrixLitAnnee)
        tabPrixLitAnnee.Location = New Point(4, 24)
        tabPrixLitAnnee.Name = "tabPrixLitAnnee"
        tabPrixLitAnnee.Size = New Size(1129, 495)
        tabPrixLitAnnee.TabIndex = 2
        tabPrixLitAnnee.Text = "Année"
        tabPrixLitAnnee.UseVisualStyleBackColor = True
        ' 
        ' tblCenterPrixLitAnnee
        ' 
        tblCenterPrixLitAnnee.ColumnCount = 2
        tblCenterPrixLitAnnee.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 54.5454559F))
        tblCenterPrixLitAnnee.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45.4545441F))
        tblCenterPrixLitAnnee.Controls.Add(dgvPrixLitAnnee, 0, 0)
        tblCenterPrixLitAnnee.Controls.Add(grpPrixLitAnneeDetails, 1, 0)
        tblCenterPrixLitAnnee.Dock = DockStyle.Fill
        tblCenterPrixLitAnnee.Location = New Point(0, 0)
        tblCenterPrixLitAnnee.Name = "tblCenterPrixLitAnnee"
        tblCenterPrixLitAnnee.RowCount = 1
        tblCenterPrixLitAnnee.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblCenterPrixLitAnnee.Size = New Size(1129, 495)
        tblCenterPrixLitAnnee.TabIndex = 2
        ' 
        ' dgvPrixLitAnnee
        ' 
        dgvPrixLitAnnee.AllowUserToAddRows = False
        dgvPrixLitAnnee.AllowUserToDeleteRows = False
        dgvPrixLitAnnee.AllowUserToResizeColumns = False
        dgvPrixLitAnnee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPrixLitAnnee.BorderStyle = BorderStyle.Fixed3D
        dgvPrixLitAnnee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPrixLitAnnee.Dock = DockStyle.Fill
        dgvPrixLitAnnee.Location = New Point(3, 3)
        dgvPrixLitAnnee.MultiSelect = False
        dgvPrixLitAnnee.Name = "dgvPrixLitAnnee"
        dgvPrixLitAnnee.ReadOnly = True
        dgvPrixLitAnnee.RowHeadersVisible = False
        dgvPrixLitAnnee.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPrixLitAnnee.Size = New Size(609, 489)
        dgvPrixLitAnnee.TabIndex = 2
        ' 
        ' grpPrixLitAnneeDetails
        ' 
        grpPrixLitAnneeDetails.Controls.Add(tblPrixLitAnneeDetails)
        grpPrixLitAnneeDetails.Dock = DockStyle.Fill
        grpPrixLitAnneeDetails.Location = New Point(618, 3)
        grpPrixLitAnneeDetails.Name = "grpPrixLitAnneeDetails"
        grpPrixLitAnneeDetails.Padding = New Padding(6)
        grpPrixLitAnneeDetails.Size = New Size(508, 489)
        grpPrixLitAnneeDetails.TabIndex = 3
        grpPrixLitAnneeDetails.TabStop = False
        grpPrixLitAnneeDetails.Text = "Détails"
        ' 
        ' tblPrixLitAnneeDetails
        ' 
        tblPrixLitAnneeDetails.AutoSize = True
        tblPrixLitAnneeDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tblPrixLitAnneeDetails.ColumnCount = 2
        tblPrixLitAnneeDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tblPrixLitAnneeDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblPrixLitAnneeDetails.Controls.Add(lblCodePrixLitAnnee, 0, 3)
        tblPrixLitAnneeDetails.Controls.Add(txtCodePrixLitAnnee, 1, 3)
        tblPrixLitAnneeDetails.Controls.Add(txtIdPrixLitAnnee, 1, 5)
        tblPrixLitAnneeDetails.Controls.Add(nudAnneePrixLit, 1, 4)
        tblPrixLitAnneeDetails.Controls.Add(lblAnneePrixLit, 0, 4)
        tblPrixLitAnneeDetails.Controls.Add(lblPrixLitParentAnnee, 0, 0)
        tblPrixLitAnneeDetails.Controls.Add(txtPrixLitParentAnnee, 1, 0)
        tblPrixLitAnneeDetails.Controls.Add(lblCategoriePrixLitAnnee, 0, 1)
        tblPrixLitAnneeDetails.Controls.Add(cboPrixLitCategorieAnnee, 1, 1)
        tblPrixLitAnneeDetails.Controls.Add(txtCategorieParentAnnee, 1, 2)
        tblPrixLitAnneeDetails.Controls.Add(lblCategorieParentAnnee, 0, 2)
        tblPrixLitAnneeDetails.Dock = DockStyle.Fill
        tblPrixLitAnneeDetails.Location = New Point(6, 22)
        tblPrixLitAnneeDetails.Name = "tblPrixLitAnneeDetails"
        tblPrixLitAnneeDetails.RowCount = 6
        tblPrixLitAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPrixLitAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 12F))
        tblPrixLitAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 12F))
        tblPrixLitAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPrixLitAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPrixLitAnneeDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16F))
        tblPrixLitAnneeDetails.Size = New Size(496, 461)
        tblPrixLitAnneeDetails.TabIndex = 2
        ' 
        ' lblCodePrixLitAnnee
        ' 
        lblCodePrixLitAnnee.AutoSize = True
        lblCodePrixLitAnnee.Location = New Point(3, 202)
        lblCodePrixLitAnnee.Name = "lblCodePrixLitAnnee"
        lblCodePrixLitAnnee.Size = New Size(35, 15)
        lblCodePrixLitAnnee.TabIndex = 16
        lblCodePrixLitAnnee.Text = "Code"
        ' 
        ' txtCodePrixLitAnnee
        ' 
        txtCodePrixLitAnnee.Location = New Point(123, 205)
        txtCodePrixLitAnnee.Name = "txtCodePrixLitAnnee"
        txtCodePrixLitAnnee.ReadOnly = True
        txtCodePrixLitAnnee.Size = New Size(100, 23)
        txtCodePrixLitAnnee.TabIndex = 17
        ' 
        ' txtIdPrixLitAnnee
        ' 
        txtIdPrixLitAnnee.Location = New Point(123, 389)
        txtIdPrixLitAnnee.Name = "txtIdPrixLitAnnee"
        txtIdPrixLitAnnee.Size = New Size(100, 23)
        txtIdPrixLitAnnee.TabIndex = 26
        txtIdPrixLitAnnee.TabStop = False
        txtIdPrixLitAnnee.Visible = False
        ' 
        ' nudAnneePrixLit
        ' 
        nudAnneePrixLit.Location = New Point(123, 297)
        nudAnneePrixLit.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        nudAnneePrixLit.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudAnneePrixLit.Name = "nudAnneePrixLit"
        nudAnneePrixLit.Size = New Size(120, 23)
        nudAnneePrixLit.TabIndex = 27
        nudAnneePrixLit.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        ' 
        ' lblAnneePrixLit
        ' 
        lblAnneePrixLit.AutoSize = True
        lblAnneePrixLit.Location = New Point(3, 294)
        lblAnneePrixLit.Name = "lblAnneePrixLit"
        lblAnneePrixLit.Size = New Size(41, 15)
        lblAnneePrixLit.TabIndex = 28
        lblAnneePrixLit.Text = "Année"
        ' 
        ' lblPrixLitParentAnnee
        ' 
        lblPrixLitParentAnnee.AutoSize = True
        lblPrixLitParentAnnee.Location = New Point(3, 0)
        lblPrixLitParentAnnee.Name = "lblPrixLitParentAnnee"
        lblPrixLitParentAnnee.Size = New Size(75, 15)
        lblPrixLitParentAnnee.TabIndex = 29
        lblPrixLitParentAnnee.Text = "Prix Littéraire"
        ' 
        ' txtPrixLitParentAnnee
        ' 
        txtPrixLitParentAnnee.Location = New Point(123, 3)
        txtPrixLitParentAnnee.Name = "txtPrixLitParentAnnee"
        txtPrixLitParentAnnee.ReadOnly = True
        txtPrixLitParentAnnee.Size = New Size(370, 23)
        txtPrixLitParentAnnee.TabIndex = 30
        ' 
        ' lblCategoriePrixLitAnnee
        ' 
        lblCategoriePrixLitAnnee.AutoSize = True
        lblCategoriePrixLitAnnee.Location = New Point(3, 92)
        lblCategoriePrixLitAnnee.Name = "lblCategoriePrixLitAnnee"
        lblCategoriePrixLitAnnee.Size = New Size(58, 15)
        lblCategoriePrixLitAnnee.TabIndex = 32
        lblCategoriePrixLitAnnee.Text = "Categorie"
        ' 
        ' cboPrixLitCategorieAnnee
        ' 
        cboPrixLitCategorieAnnee.FormattingEnabled = True
        cboPrixLitCategorieAnnee.Location = New Point(123, 95)
        cboPrixLitCategorieAnnee.Name = "cboPrixLitCategorieAnnee"
        cboPrixLitCategorieAnnee.Size = New Size(370, 23)
        cboPrixLitCategorieAnnee.TabIndex = 31
        ' 
        ' txtCategorieParentAnnee
        ' 
        txtCategorieParentAnnee.Location = New Point(123, 150)
        txtCategorieParentAnnee.Name = "txtCategorieParentAnnee"
        txtCategorieParentAnnee.ReadOnly = True
        txtCategorieParentAnnee.Size = New Size(370, 23)
        txtCategorieParentAnnee.TabIndex = 33
        ' 
        ' lblCategorieParentAnnee
        ' 
        lblCategorieParentAnnee.AutoSize = True
        lblCategorieParentAnnee.Location = New Point(3, 147)
        lblCategorieParentAnnee.Name = "lblCategorieParentAnnee"
        lblCategorieParentAnnee.Size = New Size(95, 15)
        lblCategorieParentAnnee.TabIndex = 34
        lblCategorieParentAnnee.Text = "Catégorie Parent"
        ' 
        ' pnlActions
        ' 
        pnlActions.Controls.Add(btnEdit)
        pnlActions.Controls.Add(btnClose)
        pnlActions.Controls.Add(btnDelete)
        pnlActions.Controls.Add(btnCancel)
        pnlActions.Controls.Add(btnSave)
        pnlActions.Controls.Add(btnNew)
        pnlActions.Dock = DockStyle.Bottom
        pnlActions.Location = New Point(8, 597)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(8)
        pnlActions.Size = New Size(1137, 48)
        pnlActions.TabIndex = 6
        ' 
        ' btnEdit
        ' 
        btnEdit.Location = New Point(282, 14)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(75, 23)
        btnEdit.TabIndex = 35
        btnEdit.Text = "Modifier"
        btnEdit.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(694, 14)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(75, 23)
        btnClose.TabIndex = 34
        btnClose.Text = "Fermer"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Location = New Point(591, 14)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(75, 23)
        btnDelete.TabIndex = 33
        btnDelete.Text = "Supprimer"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Enabled = False
        btnCancel.Location = New Point(488, 14)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(75, 23)
        btnCancel.TabIndex = 32
        btnCancel.Text = "Annuler"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Location = New Point(385, 14)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(75, 23)
        btnSave.TabIndex = 31
        btnSave.Text = "Enregistrer"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnNew
        ' 
        btnNew.Location = New Point(179, 14)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(75, 23)
        btnNew.TabIndex = 30
        btnNew.Text = "Nouveau"
        btnNew.UseVisualStyleBackColor = True
        ' 
        ' pnlTop
        ' 
        pnlTop.Controls.Add(chkSearchNotes)
        pnlTop.Controls.Add(lblCount)
        pnlTop.Controls.Add(chkActifsOnly)
        pnlTop.Controls.Add(cboFiltrePrixLit)
        pnlTop.Controls.Add(lblFiltrePrixLit)
        pnlTop.Controls.Add(btnClearSearch)
        pnlTop.Controls.Add(btnSearch)
        pnlTop.Controls.Add(txtSearch)
        pnlTop.Controls.Add(lblSearch)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(8, 8)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(1137, 66)
        pnlTop.TabIndex = 5
        ' 
        ' chkSearchNotes
        ' 
        chkSearchNotes.AutoSize = True
        chkSearchNotes.Location = New Point(764, 18)
        chkSearchNotes.Name = "chkSearchNotes"
        chkSearchNotes.Size = New Size(111, 19)
        chkSearchNotes.TabIndex = 7
        chkSearchNotes.Text = "Inclure les notes"
        chkSearchNotes.UseVisualStyleBackColor = True
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(1026, 18)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(86, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 Prix littéraires"
        ' 
        ' chkActifsOnly
        ' 
        chkActifsOnly.AutoSize = True
        chkActifsOnly.Checked = True
        chkActifsOnly.CheckState = CheckState.Checked
        chkActifsOnly.Location = New Point(317, 17)
        chkActifsOnly.Name = "chkActifsOnly"
        chkActifsOnly.Size = New Size(114, 19)
        chkActifsOnly.TabIndex = 6
        chkActifsOnly.Text = "Actifs seulement"
        chkActifsOnly.UseVisualStyleBackColor = True
        ' 
        ' cboFiltrePrixLit
        ' 
        cboFiltrePrixLit.DropDownStyle = ComboBoxStyle.DropDownList
        cboFiltrePrixLit.FormattingEnabled = True
        cboFiltrePrixLit.Location = New Point(106, 11)
        cboFiltrePrixLit.Name = "cboFiltrePrixLit"
        cboFiltrePrixLit.Size = New Size(205, 23)
        cboFiltrePrixLit.TabIndex = 5
        ' 
        ' lblFiltrePrixLit
        ' 
        lblFiltrePrixLit.AutoSize = True
        lblFiltrePrixLit.Location = New Point(25, 18)
        lblFiltrePrixLit.Name = "lblFiltrePrixLit"
        lblFiltrePrixLit.Size = New Size(72, 15)
        lblFiltrePrixLit.TabIndex = 4
        lblFiltrePrixLit.Text = "Prix littéraire"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(964, 15)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 23)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(894, 15)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(64, 23)
        btnSearch.TabIndex = 1
        btnSearch.Text = "Filtrer"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(509, 16)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(242, 23)
        txtSearch.TabIndex = 0
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(437, 17)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(66, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Rechercher"
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' GestionPrixLit
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1173, 711)
        Controls.Add(pnlForm)
        Controls.Add(lblTitreForm)
        Controls.Add(stsStatus)
        Name = "GestionPrixLit"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion de Prix Littéraires"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        tabMain.ResumeLayout(False)
        tabPrixLit.ResumeLayout(False)
        tlpCenterPrixLit.ResumeLayout(False)
        CType(dgvPrixLit, ComponentModel.ISupportInitialize).EndInit()
        grpPrixLitDetails.ResumeLayout(False)
        grpPrixLitDetails.PerformLayout()
        tblPrixLitDetails.ResumeLayout(False)
        tblPrixLitDetails.PerformLayout()
        tsNotes.ResumeLayout(False)
        tsNotes.PerformLayout()
        tabPrixLitCategorie.ResumeLayout(False)
        tlpCenterCategorie.ResumeLayout(False)
        CType(dgvPrixLitCategorie, ComponentModel.ISupportInitialize).EndInit()
        grpPrixLitCategorieDetails.ResumeLayout(False)
        grpPrixLitCategorieDetails.PerformLayout()
        tblPrixLitCategorieDetails.ResumeLayout(False)
        tblPrixLitCategorieDetails.PerformLayout()
        CType(nudOrdreAffichageCategorie, ComponentModel.ISupportInitialize).EndInit()
        tabPrixLitAnnee.ResumeLayout(False)
        tblCenterPrixLitAnnee.ResumeLayout(False)
        CType(dgvPrixLitAnnee, ComponentModel.ISupportInitialize).EndInit()
        grpPrixLitAnneeDetails.ResumeLayout(False)
        grpPrixLitAnneeDetails.PerformLayout()
        tblPrixLitAnneeDetails.ResumeLayout(False)
        tblPrixLitAnneeDetails.PerformLayout()
        CType(nudAnneePrixLit, ComponentModel.ISupportInitialize).EndInit()
        pnlActions.ResumeLayout(False)
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents pnlForm As Panel
    Friend WithEvents pnlTop As Panel
    Friend WithEvents chkSearchNotes As CheckBox
    Friend WithEvents lblCount As Label
    Friend WithEvents chkActifsOnly As CheckBox
    Friend WithEvents cboFiltrePrixLit As ComboBox
    Friend WithEvents lblFiltrePrixLit As Label
    Friend WithEvents btnClearSearch As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabPrixLit As TabPage
    Friend WithEvents tabPrixLitCategorie As TabPage
    Friend WithEvents tabPrixLitAnnee As TabPage
    Friend WithEvents tlpCenterPrixLit As TableLayoutPanel
    Friend WithEvents dgvPrixLit As DataGridView
    Friend WithEvents grpPrixLitDetails As GroupBox
    Friend WithEvents tlpCenterCategorie As TableLayoutPanel
    Friend WithEvents dgvPrixLitCategorie As DataGridView
    Friend WithEvents grpPrixLitCategorieDetails As GroupBox
    Friend WithEvents tblCenterPrixLitAnnee As TableLayoutPanel
    Friend WithEvents dgvPrixLitAnnee As DataGridView
    Friend WithEvents grpPrixLitAnneeDetails As GroupBox
    Friend WithEvents tblPrixLitDetails As TableLayoutPanel
    Friend WithEvents tblPrixLitCategorieDetails As TableLayoutPanel
    Friend WithEvents tblPrixLitAnneeDetails As TableLayoutPanel
    Friend WithEvents txtCodePrixLit As TextBox
    Friend WithEvents lblCodePrixLit As Label
    Friend WithEvents lblNomPrixLit As Label
    Friend WithEvents txtNomPrixLit As TextBox
    Friend WithEvents lblDescriptionPrixLit As Label
    Friend WithEvents txtDescriptionPrixLit As TextBox
    Friend WithEvents lblNotes As Label
    Friend WithEvents tsNotes As ToolStrip
    Friend WithEvents btnBold As ToolStripButton
    Friend WithEvents btnItalic As ToolStripButton
    Friend WithEvents btnUnderline As ToolStripButton
    Friend WithEvents btnBullet As ToolStripButton
    Friend WithEvents btnTab As ToolStripButton
    Friend WithEvents chkPrixLitActif As CheckBox
    Friend WithEvents txtidPrixLit As TextBox
    Friend WithEvents rtbNotesPrixLit As RichTextBox
    Friend WithEvents lblPrixLitParentCategorie As Label
    Friend WithEvents lblCodeCat As Label
    Friend WithEvents txtLibelleCategorie As TextBox
    Friend WithEvents txtCodePrixLitCategorie As TextBox
    Friend WithEvents lblLibelleCategorie As Label
    Friend WithEvents txtDescriptionCategorie As TextBox
    Friend WithEvents lblDescriptionCategorie As Label
    Friend WithEvents lblOrdreAffichageCategorie As Label
    Friend WithEvents nudOrdreAffichageCategorie As NumericUpDown
    Friend WithEvents txtidprixlitcategorie As TextBox
    Friend WithEvents chkPrixLitCategorieActif As CheckBox
    Friend WithEvents cboPrixLitParentCategorie As ComboBox
    Friend WithEvents lblCodePrixLitAnnee As Label
    Friend WithEvents txtCodePrixLitAnnee As TextBox
    Friend WithEvents txtIdPrixLitAnnee As TextBox
    Friend WithEvents nudAnneePrixLit As NumericUpDown
    Friend WithEvents lblAnneePrixLit As Label
    Friend WithEvents lblPrixLitParentAnnee As Label
    Friend WithEvents txtPrixLitParentAnnee As TextBox
    Friend WithEvents cboPrixLitCategorieAnnee As ComboBox
    Friend WithEvents lblCategoriePrixLitAnnee As Label
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents txtCategorieParentAnnee As TextBox
    Friend WithEvents lblCategorieParentAnnee As Label
End Class
