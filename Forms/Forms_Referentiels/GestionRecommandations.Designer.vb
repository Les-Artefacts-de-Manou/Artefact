<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionRecommandations
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GestionRecommandations))
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        lblTitreForm = New Label()
        pnlForm = New Panel()
        tabMain = New TabControl()
        tabOrigines = New TabPage()
        tlpCenterOrigines = New TableLayoutPanel()
        dgvOriginesRecommandation = New DataGridView()
        grpDetailOrigines = New GroupBox()
        tlpDetailsOrigines = New TableLayoutPanel()
        txtidOrigineRecommandation = New TextBox()
        chkIsActifOrigine = New CheckBox()
        nudOrdreAffichageOrigine = New NumericUpDown()
        lblOrdreAffichageOrigine = New Label()
        txtCodeOrigineRecommandation = New TextBox()
        lblCodeOrigineRecommandation = New Label()
        lblLibelleOrigineRecommandation = New Label()
        txtLibelleOrigineRecommandation = New TextBox()
        tabRecommandations = New TabPage()
        tlpCenterRecommandations = New TableLayoutPanel()
        dgvRecommandations = New DataGridView()
        grpDetailRecommandations = New GroupBox()
        tlpDetailsRecommandations = New TableLayoutPanel()
        cboOrigineRecommandation = New ComboBox()
        tsNotes = New ToolStrip()
        btnBold = New ToolStripButton()
        btnItalic = New ToolStripButton()
        btnUnderline = New ToolStripButton()
        btnBullet = New ToolStripButton()
        btnTab = New ToolStripButton()
        txtSourceUrl = New TextBox()
        lblSourceUrl = New Label()
        txtSourceLogin = New TextBox()
        txtCodeRecommandation = New TextBox()
        Label2 = New Label()
        chkIsActifRecommandation = New CheckBox()
        txtidRecommandation = New TextBox()
        lblSourceNom = New Label()
        txtSourceNom = New TextBox()
        lblSourceLogin = New Label()
        rtbCommentaireRecommandation = New RichTextBox()
        chkDateRecommandationVide = New CheckBox()
        dtpDateRecommandation = New DateTimePicker()
        lblNotes = New Label()
        lblOrigineRecommandation = New Label()
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
        cboFiltreOrigineRecommandation = New ComboBox()
        lblType = New Label()
        btnClearSearch = New Button()
        btnSearch = New Button()
        txtSearch = New TextBox()
        lblSearch = New Label()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        stsStatus.SuspendLayout()
        pnlForm.SuspendLayout()
        tabMain.SuspendLayout()
        tabOrigines.SuspendLayout()
        tlpCenterOrigines.SuspendLayout()
        CType(dgvOriginesRecommandation, ComponentModel.ISupportInitialize).BeginInit()
        grpDetailOrigines.SuspendLayout()
        tlpDetailsOrigines.SuspendLayout()
        CType(nudOrdreAffichageOrigine, ComponentModel.ISupportInitialize).BeginInit()
        tabRecommandations.SuspendLayout()
        tlpCenterRecommandations.SuspendLayout()
        CType(dgvRecommandations, ComponentModel.ISupportInitialize).BeginInit()
        grpDetailRecommandations.SuspendLayout()
        tlpDetailsRecommandations.SuspendLayout()
        tsNotes.SuspendLayout()
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
        stsStatus.Size = New Size(1268, 22)
        stsStatus.TabIndex = 17
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
        lblTitreForm.Size = New Size(253, 27)
        lblTitreForm.TabIndex = 18
        lblTitreForm.Text = "Gestion des Recommandations"
        ' 
        ' pnlForm
        ' 
        pnlForm.BackColor = Color.SeaShell
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tabMain)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(8, 31)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(1268, 657)
        pnlForm.TabIndex = 19
        ' 
        ' tabMain
        ' 
        tabMain.Controls.Add(tabOrigines)
        tabMain.Controls.Add(tabRecommandations)
        tabMain.Dock = DockStyle.Fill
        tabMain.Location = New Point(8, 56)
        tabMain.Name = "tabMain"
        tabMain.SelectedIndex = 0
        tabMain.Size = New Size(1248, 541)
        tabMain.TabIndex = 6
        ' 
        ' tabOrigines
        ' 
        tabOrigines.Controls.Add(tlpCenterOrigines)
        tabOrigines.Location = New Point(4, 24)
        tabOrigines.Name = "tabOrigines"
        tabOrigines.Padding = New Padding(3)
        tabOrigines.Size = New Size(1240, 513)
        tabOrigines.TabIndex = 0
        tabOrigines.Text = "Origines"
        tabOrigines.UseVisualStyleBackColor = True
        ' 
        ' tlpCenterOrigines
        ' 
        tlpCenterOrigines.ColumnCount = 2
        tlpCenterOrigines.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 830F))
        tlpCenterOrigines.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenterOrigines.Controls.Add(dgvOriginesRecommandation, 0, 0)
        tlpCenterOrigines.Controls.Add(grpDetailOrigines, 1, 0)
        tlpCenterOrigines.Dock = DockStyle.Fill
        tlpCenterOrigines.Location = New Point(3, 3)
        tlpCenterOrigines.Name = "tlpCenterOrigines"
        tlpCenterOrigines.RowCount = 1
        tlpCenterOrigines.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenterOrigines.Size = New Size(1234, 507)
        tlpCenterOrigines.TabIndex = 0
        ' 
        ' dgvOriginesRecommandation
        ' 
        dgvOriginesRecommandation.AllowUserToAddRows = False
        dgvOriginesRecommandation.AllowUserToDeleteRows = False
        dgvOriginesRecommandation.AllowUserToResizeColumns = False
        dgvOriginesRecommandation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOriginesRecommandation.BorderStyle = BorderStyle.Fixed3D
        dgvOriginesRecommandation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOriginesRecommandation.Dock = DockStyle.Fill
        dgvOriginesRecommandation.Location = New Point(3, 3)
        dgvOriginesRecommandation.MultiSelect = False
        dgvOriginesRecommandation.Name = "dgvOriginesRecommandation"
        dgvOriginesRecommandation.ReadOnly = True
        dgvOriginesRecommandation.RowHeadersVisible = False
        dgvOriginesRecommandation.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOriginesRecommandation.Size = New Size(824, 501)
        dgvOriginesRecommandation.TabIndex = 1
        ' 
        ' grpDetailOrigines
        ' 
        grpDetailOrigines.Controls.Add(tlpDetailsOrigines)
        grpDetailOrigines.Dock = DockStyle.Fill
        grpDetailOrigines.Location = New Point(833, 3)
        grpDetailOrigines.Name = "grpDetailOrigines"
        grpDetailOrigines.Padding = New Padding(6)
        grpDetailOrigines.Size = New Size(398, 501)
        grpDetailOrigines.TabIndex = 2
        grpDetailOrigines.TabStop = False
        grpDetailOrigines.Text = "Détails"
        ' 
        ' tlpDetailsOrigines
        ' 
        tlpDetailsOrigines.AutoSize = True
        tlpDetailsOrigines.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpDetailsOrigines.ColumnCount = 2
        tlpDetailsOrigines.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tlpDetailsOrigines.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpDetailsOrigines.Controls.Add(txtidOrigineRecommandation, 1, 3)
        tlpDetailsOrigines.Controls.Add(chkIsActifOrigine, 0, 3)
        tlpDetailsOrigines.Controls.Add(nudOrdreAffichageOrigine, 1, 2)
        tlpDetailsOrigines.Controls.Add(lblOrdreAffichageOrigine, 0, 2)
        tlpDetailsOrigines.Controls.Add(txtCodeOrigineRecommandation, 1, 0)
        tlpDetailsOrigines.Controls.Add(lblCodeOrigineRecommandation, 0, 0)
        tlpDetailsOrigines.Controls.Add(lblLibelleOrigineRecommandation, 0, 1)
        tlpDetailsOrigines.Controls.Add(txtLibelleOrigineRecommandation, 1, 1)
        tlpDetailsOrigines.Dock = DockStyle.Fill
        tlpDetailsOrigines.Location = New Point(6, 22)
        tlpDetailsOrigines.Name = "tlpDetailsOrigines"
        tlpDetailsOrigines.Padding = New Padding(4)
        tlpDetailsOrigines.RowCount = 4
        tlpDetailsOrigines.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpDetailsOrigines.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpDetailsOrigines.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpDetailsOrigines.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpDetailsOrigines.Size = New Size(386, 473)
        tlpDetailsOrigines.TabIndex = 0
        ' 
        ' txtidOrigineRecommandation
        ' 
        txtidOrigineRecommandation.Location = New Point(127, 355)
        txtidOrigineRecommandation.Name = "txtidOrigineRecommandation"
        txtidOrigineRecommandation.Size = New Size(100, 23)
        txtidOrigineRecommandation.TabIndex = 25
        txtidOrigineRecommandation.TabStop = False
        txtidOrigineRecommandation.Visible = False
        ' 
        ' chkIsActifOrigine
        ' 
        chkIsActifOrigine.AutoSize = True
        chkIsActifOrigine.Checked = True
        chkIsActifOrigine.CheckState = CheckState.Checked
        chkIsActifOrigine.Location = New Point(7, 355)
        chkIsActifOrigine.Name = "chkIsActifOrigine"
        chkIsActifOrigine.Size = New Size(60, 19)
        chkIsActifOrigine.TabIndex = 24
        chkIsActifOrigine.Text = "is actif"
        chkIsActifOrigine.UseVisualStyleBackColor = True
        ' 
        ' nudOrdreAffichageOrigine
        ' 
        nudOrdreAffichageOrigine.Location = New Point(127, 239)
        nudOrdreAffichageOrigine.Name = "nudOrdreAffichageOrigine"
        nudOrdreAffichageOrigine.Size = New Size(82, 23)
        nudOrdreAffichageOrigine.TabIndex = 23
        ' 
        ' lblOrdreAffichageOrigine
        ' 
        lblOrdreAffichageOrigine.AutoSize = True
        lblOrdreAffichageOrigine.Location = New Point(7, 236)
        lblOrdreAffichageOrigine.Name = "lblOrdreAffichageOrigine"
        lblOrdreAffichageOrigine.Size = New Size(37, 15)
        lblOrdreAffichageOrigine.TabIndex = 22
        lblOrdreAffichageOrigine.Text = "Ordre"
        ' 
        ' txtCodeOrigineRecommandation
        ' 
        txtCodeOrigineRecommandation.Location = New Point(127, 7)
        txtCodeOrigineRecommandation.Name = "txtCodeOrigineRecommandation"
        txtCodeOrigineRecommandation.ReadOnly = True
        txtCodeOrigineRecommandation.Size = New Size(100, 23)
        txtCodeOrigineRecommandation.TabIndex = 16
        ' 
        ' lblCodeOrigineRecommandation
        ' 
        lblCodeOrigineRecommandation.AutoSize = True
        lblCodeOrigineRecommandation.Location = New Point(7, 4)
        lblCodeOrigineRecommandation.Name = "lblCodeOrigineRecommandation"
        lblCodeOrigineRecommandation.Size = New Size(80, 30)
        lblCodeOrigineRecommandation.TabIndex = 15
        lblCodeOrigineRecommandation.Text = "Code Origine Recom"
        ' 
        ' lblLibelleOrigineRecommandation
        ' 
        lblLibelleOrigineRecommandation.AutoSize = True
        lblLibelleOrigineRecommandation.Location = New Point(7, 120)
        lblLibelleOrigineRecommandation.Name = "lblLibelleOrigineRecommandation"
        lblLibelleOrigineRecommandation.Size = New Size(83, 15)
        lblLibelleOrigineRecommandation.TabIndex = 17
        lblLibelleOrigineRecommandation.Text = "Libellé Origine"
        ' 
        ' txtLibelleOrigineRecommandation
        ' 
        txtLibelleOrigineRecommandation.Location = New Point(127, 123)
        txtLibelleOrigineRecommandation.MaxLength = 100
        txtLibelleOrigineRecommandation.Name = "txtLibelleOrigineRecommandation"
        txtLibelleOrigineRecommandation.Size = New Size(152, 23)
        txtLibelleOrigineRecommandation.TabIndex = 18
        ' 
        ' tabRecommandations
        ' 
        tabRecommandations.Controls.Add(tlpCenterRecommandations)
        tabRecommandations.Location = New Point(4, 24)
        tabRecommandations.Name = "tabRecommandations"
        tabRecommandations.Padding = New Padding(3)
        tabRecommandations.Size = New Size(1240, 513)
        tabRecommandations.TabIndex = 1
        tabRecommandations.Text = "Recommandations"
        tabRecommandations.UseVisualStyleBackColor = True
        ' 
        ' tlpCenterRecommandations
        ' 
        tlpCenterRecommandations.ColumnCount = 2
        tlpCenterRecommandations.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 830F))
        tlpCenterRecommandations.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenterRecommandations.Controls.Add(dgvRecommandations, 0, 0)
        tlpCenterRecommandations.Controls.Add(grpDetailRecommandations, 1, 0)
        tlpCenterRecommandations.Dock = DockStyle.Fill
        tlpCenterRecommandations.Location = New Point(3, 3)
        tlpCenterRecommandations.Name = "tlpCenterRecommandations"
        tlpCenterRecommandations.RowCount = 1
        tlpCenterRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenterRecommandations.Size = New Size(1234, 507)
        tlpCenterRecommandations.TabIndex = 1
        ' 
        ' dgvRecommandations
        ' 
        dgvRecommandations.AllowUserToAddRows = False
        dgvRecommandations.AllowUserToDeleteRows = False
        dgvRecommandations.AllowUserToResizeColumns = False
        dgvRecommandations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvRecommandations.BorderStyle = BorderStyle.Fixed3D
        dgvRecommandations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRecommandations.Dock = DockStyle.Fill
        dgvRecommandations.Location = New Point(3, 3)
        dgvRecommandations.MultiSelect = False
        dgvRecommandations.Name = "dgvRecommandations"
        dgvRecommandations.ReadOnly = True
        dgvRecommandations.RowHeadersVisible = False
        dgvRecommandations.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRecommandations.Size = New Size(824, 501)
        dgvRecommandations.TabIndex = 1
        ' 
        ' grpDetailRecommandations
        ' 
        grpDetailRecommandations.Controls.Add(tlpDetailsRecommandations)
        grpDetailRecommandations.Dock = DockStyle.Fill
        grpDetailRecommandations.Location = New Point(833, 3)
        grpDetailRecommandations.Name = "grpDetailRecommandations"
        grpDetailRecommandations.Padding = New Padding(6)
        grpDetailRecommandations.Size = New Size(398, 501)
        grpDetailRecommandations.TabIndex = 2
        grpDetailRecommandations.TabStop = False
        grpDetailRecommandations.Text = "Détails"
        ' 
        ' tlpDetailsRecommandations
        ' 
        tlpDetailsRecommandations.AutoSize = True
        tlpDetailsRecommandations.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpDetailsRecommandations.ColumnCount = 2
        tlpDetailsRecommandations.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tlpDetailsRecommandations.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpDetailsRecommandations.Controls.Add(cboOrigineRecommandation, 1, 0)
        tlpDetailsRecommandations.Controls.Add(tsNotes, 1, 6)
        tlpDetailsRecommandations.Controls.Add(txtSourceUrl, 1, 4)
        tlpDetailsRecommandations.Controls.Add(lblSourceUrl, 0, 4)
        tlpDetailsRecommandations.Controls.Add(txtSourceLogin, 1, 3)
        tlpDetailsRecommandations.Controls.Add(txtCodeRecommandation, 1, 1)
        tlpDetailsRecommandations.Controls.Add(Label2, 0, 1)
        tlpDetailsRecommandations.Controls.Add(chkIsActifRecommandation, 0, 9)
        tlpDetailsRecommandations.Controls.Add(txtidRecommandation, 1, 9)
        tlpDetailsRecommandations.Controls.Add(lblSourceNom, 0, 2)
        tlpDetailsRecommandations.Controls.Add(txtSourceNom, 1, 2)
        tlpDetailsRecommandations.Controls.Add(lblSourceLogin, 0, 3)
        tlpDetailsRecommandations.Controls.Add(rtbCommentaireRecommandation, 0, 7)
        tlpDetailsRecommandations.Controls.Add(chkDateRecommandationVide, 0, 5)
        tlpDetailsRecommandations.Controls.Add(dtpDateRecommandation, 1, 5)
        tlpDetailsRecommandations.Controls.Add(lblNotes, 0, 6)
        tlpDetailsRecommandations.Controls.Add(lblOrigineRecommandation, 0, 0)
        tlpDetailsRecommandations.Dock = DockStyle.Fill
        tlpDetailsRecommandations.Location = New Point(6, 22)
        tlpDetailsRecommandations.Name = "tlpDetailsRecommandations"
        tlpDetailsRecommandations.Padding = New Padding(4)
        tlpDetailsRecommandations.RowCount = 10
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 8F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 8F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 16F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 16F))
        tlpDetailsRecommandations.RowStyles.Add(New RowStyle(SizeType.Percent, 7F))
        tlpDetailsRecommandations.Size = New Size(386, 473)
        tlpDetailsRecommandations.TabIndex = 0
        ' 
        ' cboOrigineRecommandation
        ' 
        cboOrigineRecommandation.DropDownStyle = ComboBoxStyle.DropDownList
        cboOrigineRecommandation.FormattingEnabled = True
        cboOrigineRecommandation.Location = New Point(127, 7)
        cboOrigineRecommandation.Name = "cboOrigineRecommandation"
        cboOrigineRecommandation.Size = New Size(205, 23)
        cboOrigineRecommandation.TabIndex = 27
        ' 
        ' tsNotes
        ' 
        tsNotes.Anchor = AnchorStyles.Bottom
        tsNotes.AutoSize = False
        tsNotes.Dock = DockStyle.None
        tsNotes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        tsNotes.Items.AddRange(New ToolStripItem() {btnBold, btnItalic, btnUnderline, btnBullet, btnTab})
        tsNotes.Location = New Point(143, 258)
        tsNotes.Name = "tsNotes"
        tsNotes.Size = New Size(220, 25)
        tsNotes.TabIndex = 36
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
        ' txtSourceUrl
        ' 
        txtSourceUrl.ForeColor = Color.CornflowerBlue
        txtSourceUrl.Location = New Point(127, 167)
        txtSourceUrl.MaxLength = 150
        txtSourceUrl.Name = "txtSourceUrl"
        txtSourceUrl.Size = New Size(252, 23)
        txtSourceUrl.TabIndex = 30
        ' 
        ' lblSourceUrl
        ' 
        lblSourceUrl.AutoSize = True
        lblSourceUrl.Location = New Point(7, 164)
        lblSourceUrl.Name = "lblSourceUrl"
        lblSourceUrl.Size = New Size(67, 15)
        lblSourceUrl.TabIndex = 30
        lblSourceUrl.Text = "Source URL"
        ' 
        ' txtSourceLogin
        ' 
        txtSourceLogin.Location = New Point(127, 126)
        txtSourceLogin.MaxLength = 150
        txtSourceLogin.Name = "txtSourceLogin"
        txtSourceLogin.Size = New Size(252, 23)
        txtSourceLogin.TabIndex = 29
        ' 
        ' txtCodeRecommandation
        ' 
        txtCodeRecommandation.Location = New Point(127, 48)
        txtCodeRecommandation.Name = "txtCodeRecommandation"
        txtCodeRecommandation.ReadOnly = True
        txtCodeRecommandation.Size = New Size(100, 23)
        txtCodeRecommandation.TabIndex = 16
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(7, 45)
        Label2.Name = "Label2"
        Label2.Size = New Size(102, 30)
        Label2.TabIndex = 15
        Label2.Text = "Code Recommandation"
        ' 
        ' chkIsActifRecommandation
        ' 
        chkIsActifRecommandation.AutoSize = True
        chkIsActifRecommandation.Checked = True
        chkIsActifRecommandation.CheckState = CheckState.Checked
        chkIsActifRecommandation.Location = New Point(7, 434)
        chkIsActifRecommandation.Name = "chkIsActifRecommandation"
        chkIsActifRecommandation.Size = New Size(60, 19)
        chkIsActifRecommandation.TabIndex = 33
        chkIsActifRecommandation.Text = "is actif"
        chkIsActifRecommandation.UseVisualStyleBackColor = True
        ' 
        ' txtidRecommandation
        ' 
        txtidRecommandation.Location = New Point(127, 434)
        txtidRecommandation.Name = "txtidRecommandation"
        txtidRecommandation.Size = New Size(100, 23)
        txtidRecommandation.TabIndex = 25
        txtidRecommandation.TabStop = False
        txtidRecommandation.Visible = False
        ' 
        ' lblSourceNom
        ' 
        lblSourceNom.AutoSize = True
        lblSourceNom.Location = New Point(7, 82)
        lblSourceNom.Name = "lblSourceNom"
        lblSourceNom.Size = New Size(73, 15)
        lblSourceNom.TabIndex = 26
        lblSourceNom.Text = "Source Nom"
        ' 
        ' txtSourceNom
        ' 
        txtSourceNom.Location = New Point(127, 85)
        txtSourceNom.MaxLength = 150
        txtSourceNom.Name = "txtSourceNom"
        txtSourceNom.Size = New Size(252, 23)
        txtSourceNom.TabIndex = 28
        ' 
        ' lblSourceLogin
        ' 
        lblSourceLogin.AutoSize = True
        lblSourceLogin.Location = New Point(7, 123)
        lblSourceLogin.Name = "lblSourceLogin"
        lblSourceLogin.Size = New Size(76, 15)
        lblSourceLogin.TabIndex = 28
        lblSourceLogin.Text = "Source Login"
        ' 
        ' rtbCommentaireRecommandation
        ' 
        tlpDetailsRecommandations.SetColumnSpan(rtbCommentaireRecommandation, 2)
        rtbCommentaireRecommandation.Dock = DockStyle.Fill
        rtbCommentaireRecommandation.Location = New Point(7, 286)
        rtbCommentaireRecommandation.Name = "rtbCommentaireRecommandation"
        tlpDetailsRecommandations.SetRowSpan(rtbCommentaireRecommandation, 2)
        rtbCommentaireRecommandation.Size = New Size(372, 142)
        rtbCommentaireRecommandation.TabIndex = 32
        rtbCommentaireRecommandation.Text = ""
        ' 
        ' chkDateRecommandationVide
        ' 
        chkDateRecommandationVide.AutoSize = True
        chkDateRecommandationVide.Location = New Point(7, 208)
        chkDateRecommandationVide.Name = "chkDateRecommandationVide"
        chkDateRecommandationVide.Size = New Size(114, 19)
        chkDateRecommandationVide.TabIndex = 33
        chkDateRecommandationVide.Text = "Date Recommandation"
        chkDateRecommandationVide.TextAlign = ContentAlignment.TopCenter
        chkDateRecommandationVide.UseVisualStyleBackColor = True
        ' 
        ' dtpDateRecommandation
        ' 
        dtpDateRecommandation.Location = New Point(127, 208)
        dtpDateRecommandation.Name = "dtpDateRecommandation"
        dtpDateRecommandation.Size = New Size(200, 23)
        dtpDateRecommandation.TabIndex = 31
        ' 
        ' lblNotes
        ' 
        lblNotes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblNotes.AutoSize = True
        lblNotes.Location = New Point(7, 268)
        lblNotes.Name = "lblNotes"
        lblNotes.Size = New Size(38, 15)
        lblNotes.TabIndex = 35
        lblNotes.Text = "Notes"
        ' 
        ' lblOrigineRecommandation
        ' 
        lblOrigineRecommandation.AutoSize = True
        lblOrigineRecommandation.Location = New Point(7, 4)
        lblOrigineRecommandation.Name = "lblOrigineRecommandation"
        lblOrigineRecommandation.Size = New Size(46, 15)
        lblOrigineRecommandation.TabIndex = 37
        lblOrigineRecommandation.Text = "Origine"
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
        pnlActions.Size = New Size(1248, 48)
        pnlActions.TabIndex = 5
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
        pnlTop.Controls.Add(cboFiltreOrigineRecommandation)
        pnlTop.Controls.Add(lblType)
        pnlTop.Controls.Add(btnClearSearch)
        pnlTop.Controls.Add(btnSearch)
        pnlTop.Controls.Add(txtSearch)
        pnlTop.Controls.Add(lblSearch)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(8, 8)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(1248, 48)
        pnlTop.TabIndex = 4
        ' 
        ' chkSearchNotes
        ' 
        chkSearchNotes.AutoSize = True
        chkSearchNotes.Location = New Point(707, 15)
        chkSearchNotes.Name = "chkSearchNotes"
        chkSearchNotes.Size = New Size(111, 19)
        chkSearchNotes.TabIndex = 7
        chkSearchNotes.Text = "Inclure les notes"
        chkSearchNotes.UseVisualStyleBackColor = True
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(971, 15)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(66, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 Recom(s)"
        ' 
        ' chkActifsOnly
        ' 
        chkActifsOnly.AutoSize = True
        chkActifsOnly.Checked = True
        chkActifsOnly.CheckState = CheckState.Checked
        chkActifsOnly.Location = New Point(269, 14)
        chkActifsOnly.Name = "chkActifsOnly"
        chkActifsOnly.Size = New Size(114, 19)
        chkActifsOnly.TabIndex = 6
        chkActifsOnly.Text = "Actifs seulement"
        chkActifsOnly.UseVisualStyleBackColor = True
        ' 
        ' cboFiltreOrigineRecommandation
        ' 
        cboFiltreOrigineRecommandation.DropDownStyle = ComboBoxStyle.DropDownList
        cboFiltreOrigineRecommandation.FormattingEnabled = True
        cboFiltreOrigineRecommandation.Location = New Point(49, 12)
        cboFiltreOrigineRecommandation.Name = "cboFiltreOrigineRecommandation"
        cboFiltreOrigineRecommandation.Size = New Size(205, 23)
        cboFiltreOrigineRecommandation.TabIndex = 5
        ' 
        ' lblType
        ' 
        lblType.AutoSize = True
        lblType.Location = New Point(11, 16)
        lblType.Name = "lblType"
        lblType.Size = New Size(32, 15)
        lblType.TabIndex = 4
        lblType.Text = "Type"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(893, 10)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 23)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(823, 10)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(64, 23)
        btnSearch.TabIndex = 1
        btnSearch.Text = "Filtrer"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(459, 12)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(242, 23)
        txtSearch.TabIndex = 0
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(387, 17)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(66, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Rechercher"
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' GestionRecommandations
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1284, 711)
        Controls.Add(pnlForm)
        Controls.Add(lblTitreForm)
        Controls.Add(stsStatus)
        MinimumSize = New Size(900, 750)
        Name = "GestionRecommandations"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion des Recommandations"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        tabMain.ResumeLayout(False)
        tabOrigines.ResumeLayout(False)
        tlpCenterOrigines.ResumeLayout(False)
        CType(dgvOriginesRecommandation, ComponentModel.ISupportInitialize).EndInit()
        grpDetailOrigines.ResumeLayout(False)
        grpDetailOrigines.PerformLayout()
        tlpDetailsOrigines.ResumeLayout(False)
        tlpDetailsOrigines.PerformLayout()
        CType(nudOrdreAffichageOrigine, ComponentModel.ISupportInitialize).EndInit()
        tabRecommandations.ResumeLayout(False)
        tlpCenterRecommandations.ResumeLayout(False)
        CType(dgvRecommandations, ComponentModel.ISupportInitialize).EndInit()
        grpDetailRecommandations.ResumeLayout(False)
        grpDetailRecommandations.PerformLayout()
        tlpDetailsRecommandations.ResumeLayout(False)
        tlpDetailsRecommandations.PerformLayout()
        tsNotes.ResumeLayout(False)
        tsNotes.PerformLayout()
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
    Friend WithEvents lblCount As Label
    Friend WithEvents chkActifsOnly As CheckBox
    Friend WithEvents cboFiltreOrigineRecommandation As ComboBox
    Friend WithEvents btnClearSearch As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents lblType As Label
    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabOrigines As TabPage
    Friend WithEvents tabRecommandations As TabPage
    Friend WithEvents tlpCenterOrigines As TableLayoutPanel
    Friend WithEvents dgvOriginesRecommandation As DataGridView
    Friend WithEvents grpDetailOrigines As GroupBox
    Friend WithEvents tlpDetailsOrigines As TableLayoutPanel
    Friend WithEvents lblCodeOrigineRecommandation As Label
    Friend WithEvents txtCodeOrigineRecommandation As TextBox
    Friend WithEvents lblLibelleOrigineRecommandation As Label
    Friend WithEvents txtLibelleOrigineRecommandation As TextBox
    Friend WithEvents lblOrdreAffichageOrigine As Label
    Friend WithEvents nudOrdreAffichageOrigine As NumericUpDown
    Friend WithEvents chkIsActifOrigine As CheckBox
    Friend WithEvents txtidOrigineRecommandation As TextBox
    Friend WithEvents tlpCenterRecommandations As TableLayoutPanel
    Friend WithEvents dgvRecommandations As DataGridView
    Friend WithEvents grpDetailRecommandations As GroupBox
    Friend WithEvents tlpDetailsRecommandations As TableLayoutPanel
    Friend WithEvents txtidRecommandation As TextBox
    Friend WithEvents chkIsActifRecommandation As CheckBox
    Friend WithEvents txtCodeRecommandation As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblSourceNom As Label
    Friend WithEvents txtSourceNom As TextBox
    Friend WithEvents txtSourceLogin As TextBox
    Friend WithEvents lblSourceLogin As Label
    Friend WithEvents txtSourceUrl As TextBox
    Friend WithEvents lblSourceUrl As Label
    Friend WithEvents dtpDateRecommandation As DateTimePicker
    Friend WithEvents chkDateRecommandationVide As CheckBox
    Friend WithEvents rtbCommentaireRecommandation As RichTextBox
    Friend WithEvents lblNotes As Label
    Friend WithEvents chkSearchNotes As CheckBox
    Friend WithEvents tsNotes As ToolStrip
    Friend WithEvents btnBold As ToolStripButton
    Friend WithEvents btnItalic As ToolStripButton
    Friend WithEvents btnUnderline As ToolStripButton
    Friend WithEvents btnBullet As ToolStripButton
    Friend WithEvents btnTab As ToolStripButton
    Friend WithEvents lblOrigineRecommandation As Label
    Friend WithEvents cboOrigineRecommandation As ComboBox
End Class
