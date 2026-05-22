<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionFormatFile
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
        lblTitreForm = New Label()
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        pnlForm = New Panel()
        tlpCenter = New TableLayoutPanel()
        dgvFormatFile = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        txtCodeFormatFile = New TextBox()
        lblCode = New Label()
        txtIdFormatFile = New TextBox()
        lblNomFormat = New Label()
        txtNomFormat = New TextBox()
        lblExtension = New Label()
        txtExtension = New TextBox()
        lblMimeType = New Label()
        txtMimeType = New TextBox()
        nudOrdreAffichage = New NumericUpDown()
        lblOrdreAffichage = New Label()
        chkIsActif = New CheckBox()
        pnlActions = New Panel()
        btnEdit = New Button()
        btnClose = New Button()
        btnDelete = New Button()
        btnCancel = New Button()
        btnSave = New Button()
        btnNew = New Button()
        pnlTop = New Panel()
        lblCount = New Label()
        btnClearSearch = New Button()
        btnSearch = New Button()
        txtSearch = New TextBox()
        lblSearch = New Label()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        stsStatus.SuspendLayout()
        pnlForm.SuspendLayout()
        tlpCenter.SuspendLayout()
        CType(dgvFormatFile, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
        CType(nudOrdreAffichage, ComponentModel.ISupportInitialize).BeginInit()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.Dock = DockStyle.Top
        lblTitreForm.Font = New Font("Calibri", 14F, FontStyle.Bold)
        lblTitreForm.Location = New Point(8, 4)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Padding = New Padding(0, 0, 0, 4)
        lblTitreForm.Size = New Size(267, 27)
        lblTitreForm.TabIndex = 3
        lblTitreForm.Text = "Gestion des Formats des fichiers"
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(8, 538)
        stsStatus.Name = "stsStatus"
        stsStatus.Size = New Size(868, 22)
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
        ' pnlForm
        ' 
        pnlForm.BackColor = Color.Snow
        pnlForm.Controls.Add(tlpCenter)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.ForeColor = SystemColors.ControlText
        pnlForm.Location = New Point(8, 31)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(868, 507)
        pnlForm.TabIndex = 18
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenter.Controls.Add(dgvFormatFile, 0, 0)
        tlpCenter.Controls.Add(grpDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 56)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(852, 395)
        tlpCenter.TabIndex = 6
        ' 
        ' dgvFormatFile
        ' 
        dgvFormatFile.AllowUserToAddRows = False
        dgvFormatFile.AllowUserToDeleteRows = False
        dgvFormatFile.AllowUserToResizeColumns = False
        dgvFormatFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFormatFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFormatFile.Dock = DockStyle.Fill
        dgvFormatFile.Location = New Point(3, 3)
        dgvFormatFile.MultiSelect = False
        dgvFormatFile.Name = "dgvFormatFile"
        dgvFormatFile.ReadOnly = True
        dgvFormatFile.RowHeadersVisible = False
        dgvFormatFile.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFormatFile.Size = New Size(514, 389)
        dgvFormatFile.TabIndex = 3
        ' 
        ' grpDetails
        ' 
        grpDetails.AutoSize = True
        grpDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        grpDetails.Controls.Add(tlpDetails)
        grpDetails.Dock = DockStyle.Fill
        grpDetails.Location = New Point(523, 3)
        grpDetails.Name = "grpDetails"
        grpDetails.Size = New Size(326, 389)
        grpDetails.TabIndex = 4
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
        tlpDetails.Controls.Add(txtCodeFormatFile, 1, 0)
        tlpDetails.Controls.Add(lblCode, 0, 0)
        tlpDetails.Controls.Add(txtIdFormatFile, 0, 5)
        tlpDetails.Controls.Add(lblNomFormat, 0, 1)
        tlpDetails.Controls.Add(txtNomFormat, 1, 1)
        tlpDetails.Controls.Add(lblExtension, 0, 2)
        tlpDetails.Controls.Add(txtExtension, 1, 2)
        tlpDetails.Controls.Add(lblMimeType, 0, 3)
        tlpDetails.Controls.Add(txtMimeType, 1, 3)
        tlpDetails.Controls.Add(nudOrdreAffichage, 1, 4)
        tlpDetails.Controls.Add(lblOrdreAffichage, 0, 4)
        tlpDetails.Controls.Add(chkIsActif, 1, 5)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(3, 19)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(8, 30, 4, 8)
        tlpDetails.RowCount = 6
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 15F))
        tlpDetails.Size = New Size(320, 367)
        tlpDetails.TabIndex = 0
        ' 
        ' txtCodeFormatFile
        ' 
        txtCodeFormatFile.Dock = DockStyle.Left
        txtCodeFormatFile.Location = New Point(131, 33)
        txtCodeFormatFile.Name = "txtCodeFormatFile"
        txtCodeFormatFile.ReadOnly = True
        txtCodeFormatFile.Size = New Size(120, 23)
        txtCodeFormatFile.TabIndex = 17
        txtCodeFormatFile.TabStop = False
        ' 
        ' lblCode
        ' 
        lblCode.AutoSize = True
        lblCode.Location = New Point(11, 30)
        lblCode.Name = "lblCode"
        lblCode.Size = New Size(35, 15)
        lblCode.TabIndex = 18
        lblCode.Text = "Code"
        ' 
        ' txtIdFormatFile
        ' 
        txtIdFormatFile.Location = New Point(11, 308)
        txtIdFormatFile.Name = "txtIdFormatFile"
        txtIdFormatFile.Size = New Size(100, 23)
        txtIdFormatFile.TabIndex = 19
        txtIdFormatFile.TabStop = False
        txtIdFormatFile.Visible = False
        ' 
        ' lblNomFormat
        ' 
        lblNomFormat.AutoSize = True
        lblNomFormat.Location = New Point(11, 85)
        lblNomFormat.Name = "lblNomFormat"
        lblNomFormat.Size = New Size(75, 15)
        lblNomFormat.TabIndex = 20
        lblNomFormat.Text = "Nom Format"
        ' 
        ' txtNomFormat
        ' 
        txtNomFormat.Location = New Point(131, 88)
        txtNomFormat.MaxLength = 40
        txtNomFormat.Name = "txtNomFormat"
        txtNomFormat.Size = New Size(182, 23)
        txtNomFormat.TabIndex = 21
        ' 
        ' lblExtension
        ' 
        lblExtension.AutoSize = True
        lblExtension.Location = New Point(11, 140)
        lblExtension.Name = "lblExtension"
        lblExtension.Size = New Size(57, 15)
        lblExtension.TabIndex = 22
        lblExtension.Text = "Extension"
        ' 
        ' txtExtension
        ' 
        txtExtension.Location = New Point(131, 143)
        txtExtension.MaxLength = 10
        txtExtension.Name = "txtExtension"
        txtExtension.Size = New Size(100, 23)
        txtExtension.TabIndex = 23
        ' 
        ' lblMimeType
        ' 
        lblMimeType.AutoSize = True
        lblMimeType.Location = New Point(11, 195)
        lblMimeType.Name = "lblMimeType"
        lblMimeType.Size = New Size(66, 15)
        lblMimeType.TabIndex = 24
        lblMimeType.Text = "Mime Type"
        ' 
        ' txtMimeType
        ' 
        txtMimeType.Location = New Point(131, 198)
        txtMimeType.MaxLength = 100
        txtMimeType.Multiline = True
        txtMimeType.Name = "txtMimeType"
        txtMimeType.Size = New Size(182, 23)
        txtMimeType.TabIndex = 25
        ' 
        ' nudOrdreAffichage
        ' 
        nudOrdreAffichage.Location = New Point(131, 253)
        nudOrdreAffichage.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        nudOrdreAffichage.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudOrdreAffichage.Name = "nudOrdreAffichage"
        nudOrdreAffichage.Size = New Size(85, 23)
        nudOrdreAffichage.TabIndex = 26
        nudOrdreAffichage.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' lblOrdreAffichage
        ' 
        lblOrdreAffichage.AutoSize = True
        lblOrdreAffichage.Location = New Point(11, 250)
        lblOrdreAffichage.Name = "lblOrdreAffichage"
        lblOrdreAffichage.Size = New Size(91, 15)
        lblOrdreAffichage.TabIndex = 27
        lblOrdreAffichage.Text = "Ordre Affichage"
        ' 
        ' chkIsActif
        ' 
        chkIsActif.AutoSize = True
        chkIsActif.Checked = True
        chkIsActif.CheckState = CheckState.Checked
        chkIsActif.Location = New Point(131, 308)
        chkIsActif.Name = "chkIsActif"
        chkIsActif.Size = New Size(62, 19)
        chkIsActif.TabIndex = 28
        chkIsActif.Text = "Is Actif"
        chkIsActif.UseVisualStyleBackColor = True
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
        pnlActions.Location = New Point(8, 451)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(8)
        pnlActions.Size = New Size(852, 48)
        pnlActions.TabIndex = 5
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
        pnlTop.Controls.Add(btnClearSearch)
        pnlTop.Controls.Add(btnSearch)
        pnlTop.Controls.Add(txtSearch)
        pnlTop.Controls.Add(lblSearch)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(8, 8)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(852, 48)
        pnlTop.TabIndex = 4
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(697, 15)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(65, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 format(s)"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(484, 9)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 23)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(388, 9)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(90, 23)
        btnSearch.TabIndex = 1
        btnSearch.Text = "Filtrer"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(88, 10)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(277, 23)
        txtSearch.TabIndex = 0
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(11, 13)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(66, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Rechercher"
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' GestionFormatFile
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(884, 561)
        Controls.Add(pnlForm)
        Controls.Add(stsStatus)
        Controls.Add(lblTitreForm)
        MinimumSize = New Size(900, 600)
        Name = "GestionFormatFile"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion de Format de fichier"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        tlpCenter.PerformLayout()
        CType(dgvFormatFile, ComponentModel.ISupportInitialize).EndInit()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
        CType(nudOrdreAffichage, ComponentModel.ISupportInitialize).EndInit()
        pnlActions.ResumeLayout(False)
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitreForm As Label
    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents pnlForm As Panel
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblCount As Label
    Friend WithEvents btnClearSearch As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label

    Friend WithEvents Panel1 As Panel
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents dgvFormatFile As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents txtCodeFormatFile As TextBox
    Friend WithEvents lblCode As Label
    Friend WithEvents txtIdFormatFile As TextBox
    Friend WithEvents lblNomFormat As Label
    Friend WithEvents txtNomFormat As TextBox
    Friend WithEvents lblExtension As Label
    Friend WithEvents txtExtension As TextBox
    Friend WithEvents lblMimeType As Label
    Friend WithEvents txtMimeType As TextBox
    Friend WithEvents nudOrdreAffichage As NumericUpDown
    Friend WithEvents lblOrdreAffichage As Label
    Friend WithEvents chkIsActif As CheckBox
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
End Class
