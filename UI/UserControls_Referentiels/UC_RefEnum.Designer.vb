<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_RefEnum
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
        pnlMain = New Panel()
        tlpCenter = New TableLayoutPanel()
        pnlLeftTypes = New Panel()
        dgvTypes = New DataGridView()
        grpTypeDetails = New GroupBox()
        tlpTypeDetails = New TableLayoutPanel()
        lblCodeType = New Label()
        txtCodeType = New TextBox()
        lblLibelleType = New Label()
        txtLibelleType = New TextBox()
        lblOrdreType = New Label()
        nudOrdreType = New NumericUpDown()
        lblTypeActif = New Label()
        chkTypeActif = New CheckBox()
        txtIdEnumType = New TextBox()
        txtCodeEnumType = New TextBox()
        pnlActionsType = New Panel()
        btnNewType = New Button()
        btnEditType = New Button()
        btnSaveType = New Button()
        btnCancelType = New Button()
        btnDeleteType = New Button()
        pnlTopSearchTypes = New Panel()
        lblSearchTypes = New Label()
        txtSearchTypes = New TextBox()
        btnSearchTypes = New Button()
        btnClearSearchTypes = New Button()
        lblCountTypes = New Label()
        grpValeurs = New GroupBox()
        dgvValeurs = New DataGridView()
        tlpValeurDetails = New TableLayoutPanel()
        lblCodeValeur = New Label()
        txtCodeValeur = New TextBox()
        lblLibelleValeur = New Label()
        txtLibelleValeur = New TextBox()
        lblOrdreValeur = New Label()
        nudOrdreValeur = New NumericUpDown()
        lblValeurActive = New Label()
        chkValeurActive = New CheckBox()
        txtIdEnum = New TextBox()
        txtCodeEnum = New TextBox()
        pnlActionsValeur = New Panel()
        btnNewValeur = New Button()
        btnEditValeur = New Button()
        btnSaveValeur = New Button()
        btnCancelValeur = New Button()
        btnDeleteValeur = New Button()
        pnlTopSearchValeurs = New Panel()
        lblSearchValeurs = New Label()
        txtSearchValeurs = New TextBox()
        btnSearchValeurs = New Button()
        btnClearSearchValeurs = New Button()
        chkValeursActives = New CheckBox()
        lblCountValeurs = New Label()
        pnlMain.SuspendLayout()
        tlpCenter.SuspendLayout()
        pnlLeftTypes.SuspendLayout()
        CType(dgvTypes, ComponentModel.ISupportInitialize).BeginInit()
        grpTypeDetails.SuspendLayout()
        tlpTypeDetails.SuspendLayout()
        CType(nudOrdreType, ComponentModel.ISupportInitialize).BeginInit()
        pnlActionsType.SuspendLayout()
        pnlTopSearchTypes.SuspendLayout()
        grpValeurs.SuspendLayout()
        CType(dgvValeurs, ComponentModel.ISupportInitialize).BeginInit()
        tlpValeurDetails.SuspendLayout()
        CType(nudOrdreValeur, ComponentModel.ISupportInitialize).BeginInit()
        pnlActionsValeur.SuspendLayout()
        pnlTopSearchValeurs.SuspendLayout()
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
        pnlMain.Size = New Size(1069, 765)
        pnlMain.TabIndex = 0
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 500F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenter.Controls.Add(pnlLeftTypes, 0, 0)
        tlpCenter.Controls.Add(grpValeurs, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 8)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(1053, 749)
        tlpCenter.TabIndex = 0
        ' 
        ' pnlLeftTypes
        ' 
        pnlLeftTypes.Controls.Add(dgvTypes)
        pnlLeftTypes.Controls.Add(grpTypeDetails)
        pnlLeftTypes.Controls.Add(pnlTopSearchTypes)
        pnlLeftTypes.Dock = DockStyle.Fill
        pnlLeftTypes.Location = New Point(3, 3)
        pnlLeftTypes.Name = "pnlLeftTypes"
        pnlLeftTypes.Size = New Size(494, 743)
        pnlLeftTypes.TabIndex = 0
        ' 
        ' dgvTypes
        ' 
        dgvTypes.AllowUserToAddRows = False
        dgvTypes.AllowUserToDeleteRows = False
        dgvTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTypes.Dock = DockStyle.Fill
        dgvTypes.Location = New Point(0, 48)
        dgvTypes.MultiSelect = False
        dgvTypes.Name = "dgvTypes"
        dgvTypes.ReadOnly = True
        dgvTypes.RowHeadersVisible = False
        dgvTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTypes.Size = New Size(494, 505)
        dgvTypes.TabIndex = 1
        ' 
        ' grpTypeDetails
        ' 
        grpTypeDetails.Controls.Add(tlpTypeDetails)
        grpTypeDetails.Controls.Add(pnlActionsType)
        grpTypeDetails.Dock = DockStyle.Bottom
        grpTypeDetails.Font = New Font("Segoe UI", 9F)
        grpTypeDetails.Location = New Point(0, 553)
        grpTypeDetails.Name = "grpTypeDetails"
        grpTypeDetails.Size = New Size(494, 190)
        grpTypeDetails.TabIndex = 2
        grpTypeDetails.TabStop = False
        grpTypeDetails.Text = "TYPE SÉLECTIONNÉ"
        ' 
        ' tlpTypeDetails
        ' 
        tlpTypeDetails.ColumnCount = 2
        tlpTypeDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100F))
        tlpTypeDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpTypeDetails.Controls.Add(lblCodeType, 0, 0)
        tlpTypeDetails.Controls.Add(txtCodeType, 1, 0)
        tlpTypeDetails.Controls.Add(lblLibelleType, 0, 1)
        tlpTypeDetails.Controls.Add(txtLibelleType, 1, 1)
        tlpTypeDetails.Controls.Add(lblOrdreType, 0, 2)
        tlpTypeDetails.Controls.Add(nudOrdreType, 1, 2)
        tlpTypeDetails.Controls.Add(lblTypeActif, 0, 3)
        tlpTypeDetails.Controls.Add(chkTypeActif, 1, 3)
        tlpTypeDetails.Controls.Add(txtIdEnumType, 1, 4)
        tlpTypeDetails.Controls.Add(txtCodeEnumType, 1, 5)
        tlpTypeDetails.Dock = DockStyle.Fill
        tlpTypeDetails.Font = New Font("Segoe UI", 9F)
        tlpTypeDetails.Location = New Point(3, 19)
        tlpTypeDetails.Name = "tlpTypeDetails"
        tlpTypeDetails.Padding = New Padding(3)
        tlpTypeDetails.RowCount = 6
        tlpTypeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpTypeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpTypeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpTypeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpTypeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpTypeDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpTypeDetails.Size = New Size(488, 128)
        tlpTypeDetails.TabIndex = 0
        ' 
        ' lblCodeType
        ' 
        lblCodeType.Anchor = AnchorStyles.Left
        lblCodeType.AutoSize = True
        lblCodeType.Location = New Point(6, 8)
        lblCodeType.Name = "lblCodeType"
        lblCodeType.Size = New Size(35, 15)
        lblCodeType.TabIndex = 0
        lblCodeType.Text = "Code"
        ' 
        ' txtCodeType
        ' 
        txtCodeType.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtCodeType.CharacterCasing = CharacterCasing.Upper
        txtCodeType.Location = New Point(106, 6)
        txtCodeType.MaxLength = 60
        txtCodeType.Name = "txtCodeType"
        txtCodeType.Size = New Size(376, 23)
        txtCodeType.TabIndex = 0
        ' 
        ' lblLibelleType
        ' 
        lblLibelleType.Anchor = AnchorStyles.Left
        lblLibelleType.AutoSize = True
        lblLibelleType.Location = New Point(6, 34)
        lblLibelleType.Name = "lblLibelleType"
        lblLibelleType.Size = New Size(41, 15)
        lblLibelleType.TabIndex = 2
        lblLibelleType.Text = "Libellé"
        ' 
        ' txtLibelleType
        ' 
        txtLibelleType.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtLibelleType.Location = New Point(106, 32)
        txtLibelleType.MaxLength = 120
        txtLibelleType.Name = "txtLibelleType"
        txtLibelleType.Size = New Size(376, 23)
        txtLibelleType.TabIndex = 1
        ' 
        ' lblOrdreType
        ' 
        lblOrdreType.Anchor = AnchorStyles.Left
        lblOrdreType.AutoSize = True
        lblOrdreType.Location = New Point(6, 60)
        lblOrdreType.Name = "lblOrdreType"
        lblOrdreType.Size = New Size(37, 15)
        lblOrdreType.TabIndex = 4
        lblOrdreType.Text = "Ordre"
        ' 
        ' nudOrdreType
        ' 
        nudOrdreType.Anchor = AnchorStyles.Left
        nudOrdreType.Location = New Point(106, 58)
        nudOrdreType.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        nudOrdreType.Minimum = New Decimal(New Integer() {10000, 0, 0, Integer.MinValue})
        nudOrdreType.Name = "nudOrdreType"
        nudOrdreType.Size = New Size(80, 23)
        nudOrdreType.TabIndex = 2
        ' 
        ' lblTypeActif
        ' 
        lblTypeActif.Anchor = AnchorStyles.Left
        lblTypeActif.AutoSize = True
        lblTypeActif.Location = New Point(6, 86)
        lblTypeActif.Name = "lblTypeActif"
        lblTypeActif.Size = New Size(32, 15)
        lblTypeActif.TabIndex = 6
        lblTypeActif.Text = "Actif"
        ' 
        ' chkTypeActif
        ' 
        chkTypeActif.Anchor = AnchorStyles.Left
        chkTypeActif.AutoSize = True
        chkTypeActif.Checked = True
        chkTypeActif.CheckState = CheckState.Checked
        chkTypeActif.Location = New Point(106, 87)
        chkTypeActif.Name = "chkTypeActif"
        chkTypeActif.Size = New Size(15, 14)
        chkTypeActif.TabIndex = 3
        chkTypeActif.UseVisualStyleBackColor = True
        ' 
        ' txtIdEnumType
        ' 
        txtIdEnumType.Location = New Point(106, 110)
        txtIdEnumType.Name = "txtIdEnumType"
        txtIdEnumType.Size = New Size(1, 23)
        txtIdEnumType.TabIndex = 8
        txtIdEnumType.TabStop = False
        txtIdEnumType.Visible = False
        ' 
        ' txtCodeEnumType
        ' 
        txtCodeEnumType.Location = New Point(106, 115)
        txtCodeEnumType.Name = "txtCodeEnumType"
        txtCodeEnumType.Size = New Size(1, 23)
        txtCodeEnumType.TabIndex = 9
        txtCodeEnumType.TabStop = False
        txtCodeEnumType.Visible = False
        ' 
        ' pnlActionsType
        ' 
        pnlActionsType.Controls.Add(btnNewType)
        pnlActionsType.Controls.Add(btnEditType)
        pnlActionsType.Controls.Add(btnSaveType)
        pnlActionsType.Controls.Add(btnCancelType)
        pnlActionsType.Controls.Add(btnDeleteType)
        pnlActionsType.Dock = DockStyle.Bottom
        pnlActionsType.Location = New Point(3, 147)
        pnlActionsType.Name = "pnlActionsType"
        pnlActionsType.Size = New Size(488, 40)
        pnlActionsType.TabIndex = 1
        ' 
        ' btnNewType
        ' 
        btnNewType.Location = New Point(5, 8)
        btnNewType.Name = "btnNewType"
        btnNewType.Size = New Size(80, 28)
        btnNewType.TabIndex = 0
        btnNewType.Text = "➕ Nouveau"
        btnNewType.UseVisualStyleBackColor = True
        ' 
        ' btnEditType
        ' 
        btnEditType.Location = New Point(90, 8)
        btnEditType.Name = "btnEditType"
        btnEditType.Size = New Size(80, 28)
        btnEditType.TabIndex = 1
        btnEditType.Text = "✏ Modifier"
        btnEditType.UseVisualStyleBackColor = True
        ' 
        ' btnSaveType
        ' 
        btnSaveType.Location = New Point(175, 8)
        btnSaveType.Name = "btnSaveType"
        btnSaveType.Size = New Size(95, 28)
        btnSaveType.TabIndex = 2
        btnSaveType.Text = "💾 Enregistrer"
        btnSaveType.UseVisualStyleBackColor = True
        ' 
        ' btnCancelType
        ' 
        btnCancelType.Location = New Point(275, 8)
        btnCancelType.Name = "btnCancelType"
        btnCancelType.Size = New Size(80, 28)
        btnCancelType.TabIndex = 3
        btnCancelType.Text = "↩ Annuler"
        btnCancelType.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteType
        ' 
        btnDeleteType.Location = New Point(360, 8)
        btnDeleteType.Name = "btnDeleteType"
        btnDeleteType.Size = New Size(90, 28)
        btnDeleteType.TabIndex = 4
        btnDeleteType.Text = "🗑 Supprimer"
        btnDeleteType.UseVisualStyleBackColor = True
        ' 
        ' pnlTopSearchTypes
        ' 
        pnlTopSearchTypes.BackColor = Color.WhiteSmoke
        pnlTopSearchTypes.Controls.Add(lblSearchTypes)
        pnlTopSearchTypes.Controls.Add(txtSearchTypes)
        pnlTopSearchTypes.Controls.Add(btnSearchTypes)
        pnlTopSearchTypes.Controls.Add(btnClearSearchTypes)
        pnlTopSearchTypes.Controls.Add(lblCountTypes)
        pnlTopSearchTypes.Dock = DockStyle.Top
        pnlTopSearchTypes.Location = New Point(0, 0)
        pnlTopSearchTypes.Name = "pnlTopSearchTypes"
        pnlTopSearchTypes.Size = New Size(494, 48)
        pnlTopSearchTypes.TabIndex = 0
        ' 
        ' lblSearchTypes
        ' 
        lblSearchTypes.AutoSize = True
        lblSearchTypes.Location = New Point(8, 15)
        lblSearchTypes.Name = "lblSearchTypes"
        lblSearchTypes.Size = New Size(62, 15)
        lblSearchTypes.TabIndex = 0
        lblSearchTypes.Text = "Recherche"
        ' 
        ' txtSearchTypes
        ' 
        txtSearchTypes.Location = New Point(75, 12)
        txtSearchTypes.Name = "txtSearchTypes"
        txtSearchTypes.Size = New Size(150, 23)
        txtSearchTypes.TabIndex = 1
        ' 
        ' btnSearchTypes
        ' 
        btnSearchTypes.Location = New Point(230, 11)
        btnSearchTypes.Name = "btnSearchTypes"
        btnSearchTypes.Size = New Size(30, 25)
        btnSearchTypes.TabIndex = 2
        btnSearchTypes.Text = "🔍"
        btnSearchTypes.UseVisualStyleBackColor = True
        ' 
        ' btnClearSearchTypes
        ' 
        btnClearSearchTypes.Location = New Point(265, 11)
        btnClearSearchTypes.Name = "btnClearSearchTypes"
        btnClearSearchTypes.Size = New Size(30, 25)
        btnClearSearchTypes.TabIndex = 3
        btnClearSearchTypes.Text = "✖"
        btnClearSearchTypes.UseVisualStyleBackColor = True
        ' 
        ' lblCountTypes
        ' 
        lblCountTypes.Anchor = AnchorStyles.Right
        lblCountTypes.AutoSize = True
        lblCountTypes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCountTypes.Location = New Point(510, 15)
        lblCountTypes.Name = "lblCountTypes"
        lblCountTypes.Size = New Size(42, 15)
        lblCountTypes.TabIndex = 4
        lblCountTypes.Text = "0 type"
        lblCountTypes.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpValeurs
        ' 
        grpValeurs.Controls.Add(dgvValeurs)
        grpValeurs.Controls.Add(tlpValeurDetails)
        grpValeurs.Controls.Add(pnlActionsValeur)
        grpValeurs.Controls.Add(pnlTopSearchValeurs)
        grpValeurs.Dock = DockStyle.Fill
        grpValeurs.Font = New Font("Segoe UI", 9F)
        grpValeurs.Location = New Point(503, 3)
        grpValeurs.Name = "grpValeurs"
        grpValeurs.Size = New Size(547, 743)
        grpValeurs.TabIndex = 1
        grpValeurs.TabStop = False
        grpValeurs.Text = "VALEURS LIÉES"
        ' 
        ' dgvValeurs
        ' 
        dgvValeurs.AllowUserToAddRows = False
        dgvValeurs.AllowUserToDeleteRows = False
        dgvValeurs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvValeurs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvValeurs.Dock = DockStyle.Fill
        dgvValeurs.Location = New Point(3, 67)
        dgvValeurs.MultiSelect = False
        dgvValeurs.Name = "dgvValeurs"
        dgvValeurs.ReadOnly = True
        dgvValeurs.RowHeadersVisible = False
        dgvValeurs.RowTemplate.Height = 28
        dgvValeurs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvValeurs.Size = New Size(541, 421)
        dgvValeurs.TabIndex = 0
        ' 
        ' tlpValeurDetails
        ' 
        tlpValeurDetails.ColumnCount = 2
        tlpValeurDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100F))
        tlpValeurDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpValeurDetails.Controls.Add(lblCodeValeur, 0, 0)
        tlpValeurDetails.Controls.Add(txtCodeValeur, 1, 0)
        tlpValeurDetails.Controls.Add(lblLibelleValeur, 0, 1)
        tlpValeurDetails.Controls.Add(txtLibelleValeur, 1, 1)
        tlpValeurDetails.Controls.Add(lblOrdreValeur, 0, 2)
        tlpValeurDetails.Controls.Add(nudOrdreValeur, 1, 2)
        tlpValeurDetails.Controls.Add(lblValeurActive, 0, 3)
        tlpValeurDetails.Controls.Add(chkValeurActive, 1, 3)
        tlpValeurDetails.Controls.Add(txtIdEnum, 1, 4)
        tlpValeurDetails.Controls.Add(txtCodeEnum, 1, 5)
        tlpValeurDetails.Dock = DockStyle.Bottom
        tlpValeurDetails.Location = New Point(3, 488)
        tlpValeurDetails.Name = "tlpValeurDetails"
        tlpValeurDetails.Padding = New Padding(3)
        tlpValeurDetails.RowCount = 6
        tlpValeurDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpValeurDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpValeurDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpValeurDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 26F))
        tlpValeurDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpValeurDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5F))
        tlpValeurDetails.Size = New Size(541, 120)
        tlpValeurDetails.TabIndex = 1
        ' 
        ' lblCodeValeur
        ' 
        lblCodeValeur.Anchor = AnchorStyles.Left
        lblCodeValeur.AutoSize = True
        lblCodeValeur.Location = New Point(6, 8)
        lblCodeValeur.Name = "lblCodeValeur"
        lblCodeValeur.Size = New Size(35, 15)
        lblCodeValeur.TabIndex = 0
        lblCodeValeur.Text = "Code"
        ' 
        ' txtCodeValeur
        ' 
        txtCodeValeur.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtCodeValeur.CharacterCasing = CharacterCasing.Upper
        txtCodeValeur.Location = New Point(106, 6)
        txtCodeValeur.MaxLength = 40
        txtCodeValeur.Name = "txtCodeValeur"
        txtCodeValeur.Size = New Size(429, 23)
        txtCodeValeur.TabIndex = 0
        ' 
        ' lblLibelleValeur
        ' 
        lblLibelleValeur.Anchor = AnchorStyles.Left
        lblLibelleValeur.AutoSize = True
        lblLibelleValeur.Location = New Point(6, 34)
        lblLibelleValeur.Name = "lblLibelleValeur"
        lblLibelleValeur.Size = New Size(41, 15)
        lblLibelleValeur.TabIndex = 2
        lblLibelleValeur.Text = "Libellé"
        ' 
        ' txtLibelleValeur
        ' 
        txtLibelleValeur.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtLibelleValeur.Location = New Point(106, 32)
        txtLibelleValeur.MaxLength = 120
        txtLibelleValeur.Name = "txtLibelleValeur"
        txtLibelleValeur.Size = New Size(429, 23)
        txtLibelleValeur.TabIndex = 1
        ' 
        ' lblOrdreValeur
        ' 
        lblOrdreValeur.Anchor = AnchorStyles.Left
        lblOrdreValeur.AutoSize = True
        lblOrdreValeur.Location = New Point(6, 60)
        lblOrdreValeur.Name = "lblOrdreValeur"
        lblOrdreValeur.Size = New Size(37, 15)
        lblOrdreValeur.TabIndex = 4
        lblOrdreValeur.Text = "Ordre"
        ' 
        ' nudOrdreValeur
        ' 
        nudOrdreValeur.Anchor = AnchorStyles.Left
        nudOrdreValeur.Location = New Point(106, 58)
        nudOrdreValeur.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        nudOrdreValeur.Minimum = New Decimal(New Integer() {10000, 0, 0, Integer.MinValue})
        nudOrdreValeur.Name = "nudOrdreValeur"
        nudOrdreValeur.Size = New Size(80, 23)
        nudOrdreValeur.TabIndex = 2
        ' 
        ' lblValeurActive
        ' 
        lblValeurActive.Anchor = AnchorStyles.Left
        lblValeurActive.AutoSize = True
        lblValeurActive.Location = New Point(6, 86)
        lblValeurActive.Name = "lblValeurActive"
        lblValeurActive.Size = New Size(40, 15)
        lblValeurActive.TabIndex = 6
        lblValeurActive.Text = "Active"
        ' 
        ' chkValeurActive
        ' 
        chkValeurActive.Anchor = AnchorStyles.Left
        chkValeurActive.AutoSize = True
        chkValeurActive.Checked = True
        chkValeurActive.CheckState = CheckState.Checked
        chkValeurActive.Location = New Point(106, 87)
        chkValeurActive.Name = "chkValeurActive"
        chkValeurActive.Size = New Size(15, 14)
        chkValeurActive.TabIndex = 3
        chkValeurActive.UseVisualStyleBackColor = True
        ' 
        ' txtIdEnum
        ' 
        txtIdEnum.Location = New Point(106, 110)
        txtIdEnum.Name = "txtIdEnum"
        txtIdEnum.Size = New Size(1, 23)
        txtIdEnum.TabIndex = 8
        txtIdEnum.TabStop = False
        txtIdEnum.Visible = False
        ' 
        ' txtCodeEnum
        ' 
        txtCodeEnum.Location = New Point(106, 115)
        txtCodeEnum.Name = "txtCodeEnum"
        txtCodeEnum.Size = New Size(1, 23)
        txtCodeEnum.TabIndex = 9
        txtCodeEnum.TabStop = False
        txtCodeEnum.Visible = False
        ' 
        ' pnlActionsValeur
        ' 
        pnlActionsValeur.Controls.Add(btnNewValeur)
        pnlActionsValeur.Controls.Add(btnEditValeur)
        pnlActionsValeur.Controls.Add(btnSaveValeur)
        pnlActionsValeur.Controls.Add(btnCancelValeur)
        pnlActionsValeur.Controls.Add(btnDeleteValeur)
        pnlActionsValeur.Dock = DockStyle.Bottom
        pnlActionsValeur.Location = New Point(3, 608)
        pnlActionsValeur.Name = "pnlActionsValeur"
        pnlActionsValeur.Size = New Size(541, 132)
        pnlActionsValeur.TabIndex = 2
        ' 
        ' btnNewValeur
        ' 
        btnNewValeur.Location = New Point(5, 6)
        btnNewValeur.Name = "btnNewValeur"
        btnNewValeur.Size = New Size(80, 28)
        btnNewValeur.TabIndex = 0
        btnNewValeur.Text = "➕ Nouveau"
        btnNewValeur.UseVisualStyleBackColor = True
        ' 
        ' btnEditValeur
        ' 
        btnEditValeur.Location = New Point(90, 6)
        btnEditValeur.Name = "btnEditValeur"
        btnEditValeur.Size = New Size(80, 28)
        btnEditValeur.TabIndex = 1
        btnEditValeur.Text = "✏ Modifier"
        btnEditValeur.UseVisualStyleBackColor = True
        ' 
        ' btnSaveValeur
        ' 
        btnSaveValeur.Location = New Point(175, 6)
        btnSaveValeur.Name = "btnSaveValeur"
        btnSaveValeur.Size = New Size(95, 28)
        btnSaveValeur.TabIndex = 2
        btnSaveValeur.Text = "💾 Enregistrer"
        btnSaveValeur.UseVisualStyleBackColor = True
        ' 
        ' btnCancelValeur
        ' 
        btnCancelValeur.Location = New Point(275, 6)
        btnCancelValeur.Name = "btnCancelValeur"
        btnCancelValeur.Size = New Size(80, 28)
        btnCancelValeur.TabIndex = 3
        btnCancelValeur.Text = "↩ Annuler"
        btnCancelValeur.UseVisualStyleBackColor = True
        ' 
        ' btnDeleteValeur
        ' 
        btnDeleteValeur.Location = New Point(360, 6)
        btnDeleteValeur.Name = "btnDeleteValeur"
        btnDeleteValeur.Size = New Size(90, 28)
        btnDeleteValeur.TabIndex = 4
        btnDeleteValeur.Text = "🗑 Supprimer"
        btnDeleteValeur.UseVisualStyleBackColor = True
        ' 
        ' pnlTopSearchValeurs
        ' 
        pnlTopSearchValeurs.BackColor = Color.WhiteSmoke
        pnlTopSearchValeurs.Controls.Add(lblSearchValeurs)
        pnlTopSearchValeurs.Controls.Add(txtSearchValeurs)
        pnlTopSearchValeurs.Controls.Add(btnSearchValeurs)
        pnlTopSearchValeurs.Controls.Add(btnClearSearchValeurs)
        pnlTopSearchValeurs.Controls.Add(chkValeursActives)
        pnlTopSearchValeurs.Controls.Add(lblCountValeurs)
        pnlTopSearchValeurs.Dock = DockStyle.Top
        pnlTopSearchValeurs.Location = New Point(3, 19)
        pnlTopSearchValeurs.Name = "pnlTopSearchValeurs"
        pnlTopSearchValeurs.Size = New Size(541, 48)
        pnlTopSearchValeurs.TabIndex = 3
        ' 
        ' lblSearchValeurs
        ' 
        lblSearchValeurs.AutoSize = True
        lblSearchValeurs.Location = New Point(8, 13)
        lblSearchValeurs.Name = "lblSearchValeurs"
        lblSearchValeurs.Size = New Size(62, 15)
        lblSearchValeurs.TabIndex = 0
        lblSearchValeurs.Text = "Recherche"
        ' 
        ' txtSearchValeurs
        ' 
        txtSearchValeurs.Location = New Point(75, 10)
        txtSearchValeurs.Name = "txtSearchValeurs"
        txtSearchValeurs.Size = New Size(120, 23)
        txtSearchValeurs.TabIndex = 1
        ' 
        ' btnSearchValeurs
        ' 
        btnSearchValeurs.Location = New Point(200, 9)
        btnSearchValeurs.Name = "btnSearchValeurs"
        btnSearchValeurs.Size = New Size(30, 25)
        btnSearchValeurs.TabIndex = 2
        btnSearchValeurs.Text = "🔍"
        btnSearchValeurs.UseVisualStyleBackColor = True
        ' 
        ' btnClearSearchValeurs
        ' 
        btnClearSearchValeurs.Location = New Point(235, 9)
        btnClearSearchValeurs.Name = "btnClearSearchValeurs"
        btnClearSearchValeurs.Size = New Size(30, 25)
        btnClearSearchValeurs.TabIndex = 3
        btnClearSearchValeurs.Text = "✖"
        btnClearSearchValeurs.UseVisualStyleBackColor = True
        ' 
        ' chkValeursActives
        ' 
        chkValeursActives.AutoSize = True
        chkValeursActives.Location = New Point(275, 12)
        chkValeursActives.Name = "chkValeursActives"
        chkValeursActives.Size = New Size(101, 19)
        chkValeursActives.TabIndex = 4
        chkValeursActives.Text = "Actives uniqu."
        chkValeursActives.UseVisualStyleBackColor = True
        ' 
        ' lblCountValeurs
        ' 
        lblCountValeurs.Anchor = AnchorStyles.Right
        lblCountValeurs.AutoSize = True
        lblCountValeurs.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCountValeurs.Location = New Point(539, 13)
        lblCountValeurs.Name = "lblCountValeurs"
        lblCountValeurs.Size = New Size(52, 15)
        lblCountValeurs.TabIndex = 5
        lblCountValeurs.Text = "0 valeur"
        lblCountValeurs.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UC_RefEnum
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlMain)
        Name = "UC_RefEnum"
        Size = New Size(1069, 765)
        pnlMain.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        pnlLeftTypes.ResumeLayout(False)
        CType(dgvTypes, ComponentModel.ISupportInitialize).EndInit()
        grpTypeDetails.ResumeLayout(False)
        tlpTypeDetails.ResumeLayout(False)
        tlpTypeDetails.PerformLayout()
        CType(nudOrdreType, ComponentModel.ISupportInitialize).EndInit()
        pnlActionsType.ResumeLayout(False)
        pnlTopSearchTypes.ResumeLayout(False)
        pnlTopSearchTypes.PerformLayout()
        grpValeurs.ResumeLayout(False)
        CType(dgvValeurs, ComponentModel.ISupportInitialize).EndInit()
        tlpValeurDetails.ResumeLayout(False)
        tlpValeurDetails.PerformLayout()
        CType(nudOrdreValeur, ComponentModel.ISupportInitialize).EndInit()
        pnlActionsValeur.ResumeLayout(False)
        pnlTopSearchValeurs.ResumeLayout(False)
        pnlTopSearchValeurs.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents tlpCenter As TableLayoutPanel

    ' Section Types (gauche)
    Friend WithEvents pnlLeftTypes As Panel
    Friend WithEvents pnlTopSearchTypes As Panel
    Friend WithEvents lblSearchTypes As Label
    Friend WithEvents txtSearchTypes As TextBox
    Friend WithEvents btnSearchTypes As Button
    Friend WithEvents btnClearSearchTypes As Button
    Friend WithEvents lblCountTypes As Label
    Friend WithEvents dgvTypes As DataGridView

    ' Détails Type (sous la grid Types)
    Friend WithEvents grpTypeDetails As GroupBox
    Friend WithEvents tlpTypeDetails As TableLayoutPanel
    Friend WithEvents lblCodeType As Label
    Friend WithEvents txtCodeType As TextBox
    Friend WithEvents lblLibelleType As Label
    Friend WithEvents txtLibelleType As TextBox
    Friend WithEvents lblOrdreType As Label
    Friend WithEvents nudOrdreType As NumericUpDown
    Friend WithEvents lblTypeActif As Label
    Friend WithEvents chkTypeActif As CheckBox
    Friend WithEvents txtIdEnumType As TextBox
    Friend WithEvents txtCodeEnumType As TextBox
    Friend WithEvents pnlActionsType As Panel
    Friend WithEvents btnNewType As Button
    Friend WithEvents btnEditType As Button
    Friend WithEvents btnSaveType As Button
    Friend WithEvents btnCancelType As Button
    Friend WithEvents btnDeleteType As Button

    ' Valeurs (droite)
    Friend WithEvents grpValeurs As GroupBox
    Friend WithEvents pnlTopSearchValeurs As Panel
    Friend WithEvents lblSearchValeurs As Label
    Friend WithEvents txtSearchValeurs As TextBox
    Friend WithEvents btnSearchValeurs As Button
    Friend WithEvents btnClearSearchValeurs As Button
    Friend WithEvents chkValeursActives As CheckBox
    Friend WithEvents lblCountValeurs As Label
    Friend WithEvents dgvValeurs As DataGridView
    Friend WithEvents tlpValeurDetails As TableLayoutPanel
    Friend WithEvents lblCodeValeur As Label
    Friend WithEvents txtCodeValeur As TextBox
    Friend WithEvents lblLibelleValeur As Label
    Friend WithEvents txtLibelleValeur As TextBox
    Friend WithEvents lblOrdreValeur As Label
    Friend WithEvents nudOrdreValeur As NumericUpDown
    Friend WithEvents lblValeurActive As Label
    Friend WithEvents chkValeurActive As CheckBox
    Friend WithEvents txtIdEnum As TextBox
    Friend WithEvents txtCodeEnum As TextBox
    Friend WithEvents pnlActionsValeur As Panel
    Friend WithEvents btnNewValeur As Button
    Friend WithEvents btnEditValeur As Button
    Friend WithEvents btnSaveValeur As Button
    Friend WithEvents btnCancelValeur As Button
    Friend WithEvents btnDeleteValeur As Button

End Class
