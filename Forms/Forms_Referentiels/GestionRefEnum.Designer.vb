<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionRefEnum
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
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        pnlForm = New Panel()
        tabMain = New TabControl()
        tabTypes = New TabPage()
        tlpTypesMain = New TableLayoutPanel()
        dgvEnumTypes = New DataGridView()
        grpEnumDetails = New GroupBox()
        tlpEnumValeur = New TableLayoutPanel()
        txtIdEnumType = New TextBox()
        chkTypeActif = New CheckBox()
        lblCodeEnumType = New Label()
        txtCodeEnumType = New TextBox()
        txtCodeType = New TextBox()
        lblCodeType = New Label()
        txtLibelleType = New TextBox()
        lblLibelleType = New Label()
        nudOrdreType = New NumericUpDown()
        lblOrdreType = New Label()
        tabValeurs = New TabPage()
        tlpCenterValeur = New TableLayoutPanel()
        dgvEnumValeurs = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        nudOrdreValeur = New NumericUpDown()
        lblOrdreValeur = New Label()
        txtLibelleValeur = New TextBox()
        lblLibelleValeur = New Label()
        txtCodeValeur = New TextBox()
        lblCodeValeur = New Label()
        txtCodeEnum = New TextBox()
        lblCodeEnum = New Label()
        txtIdEnum = New TextBox()
        chkValeurActive = New CheckBox()
        pnlActions = New Panel()
        btnEdit = New Button()
        btnClose = New Button()
        btnDelete = New Button()
        btnCancel = New Button()
        btnSave = New Button()
        btnNew = New Button()
        pnlTop = New Panel()
        lblCount = New Label()
        chkActifsOnly = New CheckBox()
        cboTypeEnum = New ComboBox()
        lblType = New Label()
        btnClearSearch = New Button()
        btnSearch = New Button()
        txtSearch = New TextBox()
        lblSearch = New Label()
        lblTitreForm = New Label()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        stsStatus.SuspendLayout()
        pnlForm.SuspendLayout()
        tabMain.SuspendLayout()
        tabTypes.SuspendLayout()
        tlpTypesMain.SuspendLayout()
        CType(dgvEnumTypes, ComponentModel.ISupportInitialize).BeginInit()
        grpEnumDetails.SuspendLayout()
        tlpEnumValeur.SuspendLayout()
        CType(nudOrdreType, ComponentModel.ISupportInitialize).BeginInit()
        tabValeurs.SuspendLayout()
        tlpCenterValeur.SuspendLayout()
        CType(dgvEnumValeurs, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
        CType(nudOrdreValeur, ComponentModel.ISupportInitialize).BeginInit()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(8, 538)
        stsStatus.Name = "stsStatus"
        stsStatus.Size = New Size(868, 22)
        stsStatus.TabIndex = 16
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
        ' pnlForm
        ' 
        pnlForm.BackColor = Color.Snow
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tabMain)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Controls.Add(lblTitreForm)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(8, 4)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(868, 534)
        pnlForm.TabIndex = 17
        ' 
        ' tabMain
        ' 
        tabMain.Controls.Add(tabTypes)
        tabMain.Controls.Add(tabValeurs)
        tabMain.Dock = DockStyle.Fill
        tabMain.Location = New Point(8, 83)
        tabMain.Name = "tabMain"
        tabMain.SelectedIndex = 0
        tabMain.Size = New Size(848, 391)
        tabMain.TabIndex = 5
        ' 
        ' tabTypes
        ' 
        tabTypes.Controls.Add(tlpTypesMain)
        tabTypes.Location = New Point(4, 24)
        tabTypes.Name = "tabTypes"
        tabTypes.Padding = New Padding(3)
        tabTypes.Size = New Size(840, 363)
        tabTypes.TabIndex = 0
        tabTypes.Text = "Gestion Types Enum"
        tabTypes.UseVisualStyleBackColor = True
        ' 
        ' tlpTypesMain
        ' 
        tlpTypesMain.ColumnCount = 2
        tlpTypesMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520F))
        tlpTypesMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpTypesMain.Controls.Add(dgvEnumTypes, 0, 0)
        tlpTypesMain.Controls.Add(grpEnumDetails, 1, 0)
        tlpTypesMain.Dock = DockStyle.Fill
        tlpTypesMain.Location = New Point(3, 3)
        tlpTypesMain.Name = "tlpTypesMain"
        tlpTypesMain.RowCount = 1
        tlpTypesMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpTypesMain.Size = New Size(834, 357)
        tlpTypesMain.TabIndex = 4
        ' 
        ' dgvEnumTypes
        ' 
        dgvEnumTypes.AllowUserToAddRows = False
        dgvEnumTypes.AllowUserToDeleteRows = False
        dgvEnumTypes.AllowUserToResizeColumns = False
        dgvEnumTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEnumTypes.BorderStyle = BorderStyle.Fixed3D
        dgvEnumTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvEnumTypes.Dock = DockStyle.Fill
        dgvEnumTypes.Location = New Point(3, 3)
        dgvEnumTypes.MultiSelect = False
        dgvEnumTypes.Name = "dgvEnumTypes"
        dgvEnumTypes.ReadOnly = True
        dgvEnumTypes.RowHeadersVisible = False
        dgvEnumTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEnumTypes.Size = New Size(514, 351)
        dgvEnumTypes.TabIndex = 0
        ' 
        ' grpEnumDetails
        ' 
        grpEnumDetails.Controls.Add(tlpEnumValeur)
        grpEnumDetails.Dock = DockStyle.Fill
        grpEnumDetails.Location = New Point(523, 3)
        grpEnumDetails.Name = "grpEnumDetails"
        grpEnumDetails.Padding = New Padding(6)
        grpEnumDetails.Size = New Size(308, 351)
        grpEnumDetails.TabIndex = 1
        grpEnumDetails.TabStop = False
        grpEnumDetails.Text = "Détails"
        ' 
        ' tlpEnumValeur
        ' 
        tlpEnumValeur.AutoSize = True
        tlpEnumValeur.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpEnumValeur.ColumnCount = 2
        tlpEnumValeur.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tlpEnumValeur.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpEnumValeur.Controls.Add(txtIdEnumType, 1, 4)
        tlpEnumValeur.Controls.Add(chkTypeActif, 0, 4)
        tlpEnumValeur.Controls.Add(lblCodeEnumType, 0, 0)
        tlpEnumValeur.Controls.Add(txtCodeEnumType, 1, 0)
        tlpEnumValeur.Controls.Add(txtCodeType, 1, 1)
        tlpEnumValeur.Controls.Add(lblCodeType, 0, 1)
        tlpEnumValeur.Controls.Add(txtLibelleType, 1, 2)
        tlpEnumValeur.Controls.Add(lblLibelleType, 0, 2)
        tlpEnumValeur.Controls.Add(nudOrdreType, 1, 3)
        tlpEnumValeur.Controls.Add(lblOrdreType, 0, 3)
        tlpEnumValeur.Dock = DockStyle.Fill
        tlpEnumValeur.Location = New Point(6, 22)
        tlpEnumValeur.Name = "tlpEnumValeur"
        tlpEnumValeur.Padding = New Padding(8, 30, 4, 8)
        tlpEnumValeur.RowCount = 5
        tlpEnumValeur.RowStyles.Add(New RowStyle(SizeType.Percent, 20.73171F))
        tlpEnumValeur.RowStyles.Add(New RowStyle(SizeType.Percent, 19.5121937F))
        tlpEnumValeur.RowStyles.Add(New RowStyle(SizeType.Percent, 20.7317066F))
        tlpEnumValeur.RowStyles.Add(New RowStyle(SizeType.Percent, 19.5121975F))
        tlpEnumValeur.RowStyles.Add(New RowStyle(SizeType.Percent, 19.5121937F))
        tlpEnumValeur.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpEnumValeur.Size = New Size(296, 323)
        tlpEnumValeur.TabIndex = 0
        ' 
        ' txtIdEnumType
        ' 
        txtIdEnumType.Location = New Point(131, 261)
        txtIdEnumType.Name = "txtIdEnumType"
        txtIdEnumType.Size = New Size(100, 23)
        txtIdEnumType.TabIndex = 11
        txtIdEnumType.TabStop = False
        txtIdEnumType.Visible = False
        ' 
        ' chkTypeActif
        ' 
        chkTypeActif.AutoSize = True
        chkTypeActif.Checked = True
        chkTypeActif.CheckState = CheckState.Checked
        chkTypeActif.Location = New Point(11, 261)
        chkTypeActif.Name = "chkTypeActif"
        chkTypeActif.Size = New Size(60, 19)
        chkTypeActif.TabIndex = 13
        chkTypeActif.Text = "is actif"
        chkTypeActif.UseVisualStyleBackColor = True
        ' 
        ' lblCodeEnumType
        ' 
        lblCodeEnumType.AutoSize = True
        lblCodeEnumType.Location = New Point(11, 30)
        lblCodeEnumType.Name = "lblCodeEnumType"
        lblCodeEnumType.Size = New Size(94, 15)
        lblCodeEnumType.TabIndex = 14
        lblCodeEnumType.Text = "Code EnumType"
        ' 
        ' txtCodeEnumType
        ' 
        txtCodeEnumType.Location = New Point(131, 33)
        txtCodeEnumType.Name = "txtCodeEnumType"
        txtCodeEnumType.ReadOnly = True
        txtCodeEnumType.Size = New Size(100, 23)
        txtCodeEnumType.TabIndex = 15
        ' 
        ' txtCodeType
        ' 
        txtCodeType.CharacterCasing = CharacterCasing.Upper
        txtCodeType.Location = New Point(131, 92)
        txtCodeType.MaxLength = 60
        txtCodeType.Name = "txtCodeType"
        txtCodeType.Size = New Size(151, 23)
        txtCodeType.TabIndex = 16
        ' 
        ' lblCodeType
        ' 
        lblCodeType.AutoSize = True
        lblCodeType.Location = New Point(11, 89)
        lblCodeType.Name = "lblCodeType"
        lblCodeType.Size = New Size(63, 15)
        lblCodeType.TabIndex = 17
        lblCodeType.Text = "Code Type"
        ' 
        ' txtLibelleType
        ' 
        txtLibelleType.Location = New Point(131, 147)
        txtLibelleType.MaxLength = 120
        txtLibelleType.Name = "txtLibelleType"
        txtLibelleType.Size = New Size(151, 23)
        txtLibelleType.TabIndex = 18
        ' 
        ' lblLibelleType
        ' 
        lblLibelleType.AutoSize = True
        lblLibelleType.Location = New Point(11, 144)
        lblLibelleType.Name = "lblLibelleType"
        lblLibelleType.Size = New Size(69, 15)
        lblLibelleType.TabIndex = 19
        lblLibelleType.Text = "Libellé Type"
        ' 
        ' nudOrdreType
        ' 
        nudOrdreType.Location = New Point(131, 206)
        nudOrdreType.Name = "nudOrdreType"
        nudOrdreType.Size = New Size(82, 23)
        nudOrdreType.TabIndex = 20
        ' 
        ' lblOrdreType
        ' 
        lblOrdreType.AutoSize = True
        lblOrdreType.Location = New Point(11, 203)
        lblOrdreType.Name = "lblOrdreType"
        lblOrdreType.Size = New Size(65, 15)
        lblOrdreType.TabIndex = 21
        lblOrdreType.Text = "Ordre Type"
        ' 
        ' tabValeurs
        ' 
        tabValeurs.Controls.Add(tlpCenterValeur)
        tabValeurs.Location = New Point(4, 24)
        tabValeurs.Name = "tabValeurs"
        tabValeurs.Padding = New Padding(3)
        tabValeurs.Size = New Size(840, 363)
        tabValeurs.TabIndex = 1
        tabValeurs.Text = "Gestion Valeurs Enum"
        tabValeurs.UseVisualStyleBackColor = True
        ' 
        ' tlpCenterValeur
        ' 
        tlpCenterValeur.ColumnCount = 2
        tlpCenterValeur.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520F))
        tlpCenterValeur.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenterValeur.Controls.Add(dgvEnumValeurs, 0, 0)
        tlpCenterValeur.Controls.Add(grpDetails, 1, 0)
        tlpCenterValeur.Dock = DockStyle.Fill
        tlpCenterValeur.Location = New Point(3, 3)
        tlpCenterValeur.Name = "tlpCenterValeur"
        tlpCenterValeur.RowCount = 1
        tlpCenterValeur.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenterValeur.Size = New Size(834, 357)
        tlpCenterValeur.TabIndex = 4
        ' 
        ' dgvEnumValeurs
        ' 
        dgvEnumValeurs.AllowUserToAddRows = False
        dgvEnumValeurs.AllowUserToDeleteRows = False
        dgvEnumValeurs.AllowUserToResizeColumns = False
        dgvEnumValeurs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEnumValeurs.BorderStyle = BorderStyle.Fixed3D
        dgvEnumValeurs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvEnumValeurs.Dock = DockStyle.Fill
        dgvEnumValeurs.Location = New Point(3, 3)
        dgvEnumValeurs.MultiSelect = False
        dgvEnumValeurs.Name = "dgvEnumValeurs"
        dgvEnumValeurs.ReadOnly = True
        dgvEnumValeurs.RowHeadersVisible = False
        dgvEnumValeurs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEnumValeurs.Size = New Size(514, 351)
        dgvEnumValeurs.TabIndex = 0
        ' 
        ' grpDetails
        ' 
        grpDetails.Controls.Add(tlpDetails)
        grpDetails.Dock = DockStyle.Fill
        grpDetails.Location = New Point(523, 3)
        grpDetails.Name = "grpDetails"
        grpDetails.Padding = New Padding(6)
        grpDetails.Size = New Size(308, 351)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Détails"
        ' 
        ' tlpDetails
        ' 
        tlpDetails.AutoSize = True
        tlpDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpDetails.ColumnCount = 2
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpDetails.Controls.Add(nudOrdreValeur, 1, 3)
        tlpDetails.Controls.Add(lblOrdreValeur, 0, 3)
        tlpDetails.Controls.Add(txtLibelleValeur, 1, 2)
        tlpDetails.Controls.Add(lblLibelleValeur, 0, 2)
        tlpDetails.Controls.Add(txtCodeValeur, 1, 1)
        tlpDetails.Controls.Add(lblCodeValeur, 0, 1)
        tlpDetails.Controls.Add(txtCodeEnum, 1, 0)
        tlpDetails.Controls.Add(lblCodeEnum, 0, 0)
        tlpDetails.Controls.Add(txtIdEnum, 1, 4)
        tlpDetails.Controls.Add(chkValeurActive, 0, 4)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(6, 22)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(8, 30, 4, 8)
        tlpDetails.RowCount = 5
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20.48193F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 19.2771072F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20.481926F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20.481926F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 19.2771111F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpDetails.Size = New Size(296, 323)
        tlpDetails.TabIndex = 0
        ' 
        ' nudOrdreValeur
        ' 
        nudOrdreValeur.Location = New Point(131, 203)
        nudOrdreValeur.Name = "nudOrdreValeur"
        nudOrdreValeur.Size = New Size(82, 23)
        nudOrdreValeur.TabIndex = 23
        ' 
        ' lblOrdreValeur
        ' 
        lblOrdreValeur.AutoSize = True
        lblOrdreValeur.Location = New Point(11, 200)
        lblOrdreValeur.Name = "lblOrdreValeur"
        lblOrdreValeur.Size = New Size(72, 15)
        lblOrdreValeur.TabIndex = 22
        lblOrdreValeur.Text = "Ordre Valeur"
        ' 
        ' txtLibelleValeur
        ' 
        txtLibelleValeur.Location = New Point(131, 145)
        txtLibelleValeur.MaxLength = 120
        txtLibelleValeur.Name = "txtLibelleValeur"
        txtLibelleValeur.Size = New Size(151, 23)
        txtLibelleValeur.TabIndex = 21
        ' 
        ' lblLibelleValeur
        ' 
        lblLibelleValeur.AutoSize = True
        lblLibelleValeur.Location = New Point(11, 142)
        lblLibelleValeur.Name = "lblLibelleValeur"
        lblLibelleValeur.Size = New Size(76, 15)
        lblLibelleValeur.TabIndex = 20
        lblLibelleValeur.Text = "Libellé Valeur"
        ' 
        ' txtCodeValeur
        ' 
        txtCodeValeur.CharacterCasing = CharacterCasing.Upper
        txtCodeValeur.Location = New Point(131, 91)
        txtCodeValeur.MaxLength = 60
        txtCodeValeur.Name = "txtCodeValeur"
        txtCodeValeur.Size = New Size(151, 23)
        txtCodeValeur.TabIndex = 19
        ' 
        ' lblCodeValeur
        ' 
        lblCodeValeur.AutoSize = True
        lblCodeValeur.Location = New Point(11, 88)
        lblCodeValeur.Name = "lblCodeValeur"
        lblCodeValeur.Size = New Size(70, 15)
        lblCodeValeur.TabIndex = 18
        lblCodeValeur.Text = "Code Valeur"
        ' 
        ' txtCodeEnum
        ' 
        txtCodeEnum.Location = New Point(131, 33)
        txtCodeEnum.Name = "txtCodeEnum"
        txtCodeEnum.ReadOnly = True
        txtCodeEnum.Size = New Size(100, 23)
        txtCodeEnum.TabIndex = 16
        ' 
        ' lblCodeEnum
        ' 
        lblCodeEnum.AutoSize = True
        lblCodeEnum.Location = New Point(11, 30)
        lblCodeEnum.Name = "lblCodeEnum"
        lblCodeEnum.Size = New Size(104, 15)
        lblCodeEnum.TabIndex = 15
        lblCodeEnum.Text = "Code Enum Valeur"
        ' 
        ' txtIdEnum
        ' 
        txtIdEnum.Location = New Point(131, 261)
        txtIdEnum.Name = "txtIdEnum"
        txtIdEnum.Size = New Size(100, 23)
        txtIdEnum.TabIndex = 11
        txtIdEnum.TabStop = False
        txtIdEnum.Visible = False
        ' 
        ' chkValeurActive
        ' 
        chkValeurActive.AutoSize = True
        chkValeurActive.Checked = True
        chkValeurActive.CheckState = CheckState.Checked
        chkValeurActive.Location = New Point(11, 261)
        chkValeurActive.Name = "chkValeurActive"
        chkValeurActive.Size = New Size(60, 19)
        chkValeurActive.TabIndex = 14
        chkValeurActive.Text = "is actif"
        chkValeurActive.UseVisualStyleBackColor = True
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
        pnlActions.Location = New Point(8, 474)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(8)
        pnlActions.Size = New Size(848, 48)
        pnlActions.TabIndex = 4
        ' 
        ' btnEdit
        ' 
        btnEdit.Location = New Point(238, 14)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(75, 23)
        btnEdit.TabIndex = 35
        btnEdit.Text = "Modifier"
        btnEdit.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(650, 14)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(75, 23)
        btnClose.TabIndex = 34
        btnClose.Text = "Fermer"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Location = New Point(547, 14)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(75, 23)
        btnDelete.TabIndex = 33
        btnDelete.Text = "Supprimer"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Enabled = False
        btnCancel.Location = New Point(444, 14)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(75, 23)
        btnCancel.TabIndex = 32
        btnCancel.Text = "Annuler"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Location = New Point(341, 14)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(75, 23)
        btnSave.TabIndex = 31
        btnSave.Text = "Enregistrer"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnNew
        ' 
        btnNew.Location = New Point(135, 14)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(75, 23)
        btnNew.TabIndex = 30
        btnNew.Text = "Nouveau"
        btnNew.UseVisualStyleBackColor = True
        ' 
        ' pnlTop
        ' 
        pnlTop.Controls.Add(lblCount)
        pnlTop.Controls.Add(chkActifsOnly)
        pnlTop.Controls.Add(cboTypeEnum)
        pnlTop.Controls.Add(lblType)
        pnlTop.Controls.Add(btnClearSearch)
        pnlTop.Controls.Add(btnSearch)
        pnlTop.Controls.Add(txtSearch)
        pnlTop.Controls.Add(lblSearch)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(8, 35)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(848, 48)
        pnlTop.TabIndex = 3
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(769, 15)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(60, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 Enum(s)"
        ' 
        ' chkActifsOnly
        ' 
        chkActifsOnly.AutoSize = True
        chkActifsOnly.Checked = True
        chkActifsOnly.CheckState = CheckState.Checked
        chkActifsOnly.Location = New Point(231, 14)
        chkActifsOnly.Name = "chkActifsOnly"
        chkActifsOnly.Size = New Size(114, 19)
        chkActifsOnly.TabIndex = 6
        chkActifsOnly.Text = "Actifs seulement"
        chkActifsOnly.UseVisualStyleBackColor = True
        ' 
        ' cboTypeEnum
        ' 
        cboTypeEnum.DropDownStyle = ComboBoxStyle.DropDownList
        cboTypeEnum.FormattingEnabled = True
        cboTypeEnum.Location = New Point(46, 10)
        cboTypeEnum.Name = "cboTypeEnum"
        cboTypeEnum.Size = New Size(173, 23)
        cboTypeEnum.TabIndex = 5
        ' 
        ' lblType
        ' 
        lblType.AutoSize = True
        lblType.Location = New Point(8, 13)
        lblType.Name = "lblType"
        lblType.Size = New Size(32, 15)
        lblType.TabIndex = 4
        lblType.Text = "Type"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(731, 10)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 23)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(661, 10)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(64, 23)
        btnSearch.TabIndex = 1
        btnSearch.Text = "Filtrer"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(413, 11)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(242, 23)
        txtSearch.TabIndex = 0
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(341, 15)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(66, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Rechercher"
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.Dock = DockStyle.Top
        lblTitreForm.Font = New Font("Calibri", 14F, FontStyle.Bold)
        lblTitreForm.Location = New Point(8, 8)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Padding = New Padding(0, 0, 0, 4)
        lblTitreForm.Size = New Size(151, 27)
        lblTitreForm.TabIndex = 2
        lblTitreForm.Text = "Gestion des Enum"
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' GestionRefEnum
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(884, 561)
        Controls.Add(pnlForm)
        Controls.Add(stsStatus)
        MinimumSize = New Size(900, 600)
        Name = "GestionRefEnum"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion de RefEnum"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        pnlForm.PerformLayout()
        tabMain.ResumeLayout(False)
        tabTypes.ResumeLayout(False)
        tlpTypesMain.ResumeLayout(False)
        CType(dgvEnumTypes, ComponentModel.ISupportInitialize).EndInit()
        grpEnumDetails.ResumeLayout(False)
        grpEnumDetails.PerformLayout()
        tlpEnumValeur.ResumeLayout(False)
        tlpEnumValeur.PerformLayout()
        CType(nudOrdreType, ComponentModel.ISupportInitialize).EndInit()
        tabValeurs.ResumeLayout(False)
        tlpCenterValeur.ResumeLayout(False)
        CType(dgvEnumValeurs, ComponentModel.ISupportInitialize).EndInit()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
        CType(nudOrdreValeur, ComponentModel.ISupportInitialize).EndInit()
        pnlActions.ResumeLayout(False)
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents pnlForm As Panel
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblCount As Label
    Friend WithEvents btnClearSearch As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabTypes As TabPage
    Friend WithEvents tabValeurs As TabPage
    Friend WithEvents tlpTypesMain As TableLayoutPanel
    Friend WithEvents dgvEnumTypes As DataGridView
    Friend WithEvents grpEnumDetails As GroupBox
    Friend WithEvents tlpEnumValeur As TableLayoutPanel
    Friend WithEvents txtIdEnumType As TextBox
    Friend WithEvents chkTypeActif As CheckBox
    Friend WithEvents lblCodeEnumType As Label
    Friend WithEvents txtCodeEnumType As TextBox
    Friend WithEvents txtCodeType As TextBox
    Friend WithEvents lblCodeType As Label
    Friend WithEvents txtLibelleType As TextBox
    Friend WithEvents lblLibelleType As Label
    Friend WithEvents nudOrdreType As NumericUpDown
    Friend WithEvents lblOrdreType As Label
    Friend WithEvents lblType As Label
    Friend WithEvents chkActifsOnly As CheckBox
    Friend WithEvents cboTypeEnum As ComboBox
    Friend WithEvents tlpCenterValeur As TableLayoutPanel
    Friend WithEvents dgvEnumValeurs As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents txtIdEnum As TextBox
    Friend WithEvents chkValeurActive As CheckBox
    Friend WithEvents lblCodeEnum As Label
    Friend WithEvents txtCodeEnum As TextBox
    Friend WithEvents lblCodeValeur As Label
    Friend WithEvents txtCodeValeur As TextBox
    Friend WithEvents lblLibelleValeur As Label
    Friend WithEvents txtLibelleValeur As TextBox
    Friend WithEvents lblOrdreValeur As Label
    Friend WithEvents nudOrdreValeur As NumericUpDown
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
End Class
