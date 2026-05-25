<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionPays
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
        tlpCenter = New TableLayoutPanel()
        dgvPays = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblNomPays = New Label()
        lblAbrevPays = New Label()
        lblIso2 = New Label()
        lblIso3 = New Label()
        lblCodePays = New Label()
        lblMode = New Label()
        txtNomPays = New TextBox()
        txtAbrevPays = New TextBox()
        txtIso2 = New TextBox()
        txtIso3 = New TextBox()
        txtCodePays = New TextBox()
        txtIdPays = New TextBox()
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
        lblTitreForm = New Label()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        stsStatus.SuspendLayout()
        pnlForm.SuspendLayout()
        tlpCenter.SuspendLayout()
        CType(dgvPays, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
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
        stsStatus.TabIndex = 15
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
        pnlForm.AutoSize = True
        pnlForm.BackColor = Color.OldLace
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tlpCenter)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Controls.Add(lblTitreForm)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(8, 4)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(868, 534)
        pnlForm.TabIndex = 18
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenter.Controls.Add(dgvPays, 0, 0)
        tlpCenter.Controls.Add(grpDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 83)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(848, 391)
        tlpCenter.TabIndex = 3
        ' 
        ' dgvPays
        ' 
        dgvPays.AllowUserToAddRows = False
        dgvPays.AllowUserToDeleteRows = False
        dgvPays.AllowUserToResizeColumns = False
        dgvPays.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPays.BorderStyle = BorderStyle.Fixed3D
        dgvPays.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPays.Dock = DockStyle.Fill
        dgvPays.Location = New Point(3, 3)
        dgvPays.MultiSelect = False
        dgvPays.Name = "dgvPays"
        dgvPays.ReadOnly = True
        dgvPays.RowHeadersVisible = False
        dgvPays.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPays.Size = New Size(514, 385)
        dgvPays.TabIndex = 0
        ' 
        ' grpDetails
        ' 
        grpDetails.Controls.Add(tlpDetails)
        grpDetails.Dock = DockStyle.Fill
        grpDetails.Location = New Point(523, 3)
        grpDetails.Name = "grpDetails"
        grpDetails.Padding = New Padding(6)
        grpDetails.Size = New Size(322, 385)
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
        tlpDetails.Controls.Add(lblNomPays, 0, 0)
        tlpDetails.Controls.Add(lblAbrevPays, 0, 1)
        tlpDetails.Controls.Add(lblIso2, 0, 2)
        tlpDetails.Controls.Add(lblIso3, 0, 3)
        tlpDetails.Controls.Add(lblCodePays, 0, 4)
        tlpDetails.Controls.Add(lblMode, 0, 5)
        tlpDetails.Controls.Add(txtNomPays, 1, 0)
        tlpDetails.Controls.Add(txtAbrevPays, 1, 1)
        tlpDetails.Controls.Add(txtIso2, 1, 2)
        tlpDetails.Controls.Add(txtIso3, 1, 3)
        tlpDetails.Controls.Add(txtCodePays, 1, 4)
        tlpDetails.Controls.Add(txtIdPays, 1, 5)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(6, 22)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(8, 30, 4, 8)
        tlpDetails.RowCount = 6
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17.17172F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.1616154F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17.1717167F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17.1717167F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.1616154F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.1616154F))
        tlpDetails.Size = New Size(310, 357)
        tlpDetails.TabIndex = 0
        ' 
        ' lblNomPays
        ' 
        lblNomPays.AutoSize = True
        lblNomPays.Location = New Point(11, 30)
        lblNomPays.Name = "lblNomPays"
        lblNomPays.Size = New Size(34, 15)
        lblNomPays.TabIndex = 0
        lblNomPays.Text = "Nom"
        ' 
        ' lblAbrevPays
        ' 
        lblAbrevPays.AutoSize = True
        lblAbrevPays.Location = New Point(11, 84)
        lblAbrevPays.Name = "lblAbrevPays"
        lblAbrevPays.Size = New Size(68, 15)
        lblAbrevPays.TabIndex = 1
        lblAbrevPays.Text = "Abréviation"
        ' 
        ' lblIso2
        ' 
        lblIso2.AutoSize = True
        lblIso2.Location = New Point(11, 135)
        lblIso2.Name = "lblIso2"
        lblIso2.Size = New Size(31, 15)
        lblIso2.TabIndex = 2
        lblIso2.Text = "ISO2"
        ' 
        ' lblIso3
        ' 
        lblIso3.AutoSize = True
        lblIso3.Location = New Point(11, 189)
        lblIso3.Name = "lblIso3"
        lblIso3.Size = New Size(31, 15)
        lblIso3.TabIndex = 3
        lblIso3.Text = "ISO3"
        ' 
        ' lblCodePays
        ' 
        lblCodePays.AutoSize = True
        lblCodePays.Location = New Point(11, 243)
        lblCodePays.Name = "lblCodePays"
        lblCodePays.Size = New Size(35, 15)
        lblCodePays.TabIndex = 4
        lblCodePays.Text = "Code"
        ' 
        ' lblMode
        ' 
        lblMode.AutoSize = True
        lblMode.ForeColor = Color.Gray
        lblMode.Location = New Point(11, 294)
        lblMode.Name = "lblMode"
        lblMode.Size = New Size(75, 15)
        lblMode.TabIndex = 5
        lblMode.Text = "Consultation"
        ' 
        ' txtNomPays
        ' 
        txtNomPays.Dock = DockStyle.Fill
        txtNomPays.Location = New Point(131, 33)
        txtNomPays.MaxLength = 120
        txtNomPays.Name = "txtNomPays"
        txtNomPays.Size = New Size(172, 23)
        txtNomPays.TabIndex = 6
        ' 
        ' txtAbrevPays
        ' 
        txtAbrevPays.CharacterCasing = CharacterCasing.Upper
        txtAbrevPays.Dock = DockStyle.Fill
        txtAbrevPays.Location = New Point(131, 87)
        txtAbrevPays.MaxLength = 10
        txtAbrevPays.Name = "txtAbrevPays"
        txtAbrevPays.Size = New Size(172, 23)
        txtAbrevPays.TabIndex = 7
        ' 
        ' txtIso2
        ' 
        txtIso2.CharacterCasing = CharacterCasing.Upper
        txtIso2.Dock = DockStyle.Left
        txtIso2.Location = New Point(131, 138)
        txtIso2.MaxLength = 2
        txtIso2.Name = "txtIso2"
        txtIso2.Size = New Size(60, 23)
        txtIso2.TabIndex = 8
        ' 
        ' txtIso3
        ' 
        txtIso3.CharacterCasing = CharacterCasing.Upper
        txtIso3.Dock = DockStyle.Left
        txtIso3.Location = New Point(131, 192)
        txtIso3.MaxLength = 3
        txtIso3.Name = "txtIso3"
        txtIso3.Size = New Size(60, 23)
        txtIso3.TabIndex = 9
        ' 
        ' txtCodePays
        ' 
        txtCodePays.Dock = DockStyle.Left
        txtCodePays.Location = New Point(131, 246)
        txtCodePays.Name = "txtCodePays"
        txtCodePays.ReadOnly = True
        txtCodePays.Size = New Size(120, 23)
        txtCodePays.TabIndex = 10
        txtCodePays.TabStop = False
        ' 
        ' txtIdPays
        ' 
        txtIdPays.Location = New Point(131, 297)
        txtIdPays.Name = "txtIdPays"
        txtIdPays.Size = New Size(100, 23)
        txtIdPays.TabIndex = 11
        txtIdPays.TabStop = False
        txtIdPays.Visible = False
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
        pnlActions.TabIndex = 2
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
        pnlTop.Location = New Point(8, 35)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(848, 48)
        pnlTop.TabIndex = 1
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(700, 16)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(40, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 pays"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(472, 10)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 23)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(364, 10)
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
        txtSearch.Size = New Size(260, 23)
        txtSearch.TabIndex = 0
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(16, 18)
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
        lblTitreForm.Size = New Size(143, 27)
        lblTitreForm.TabIndex = 0
        lblTitreForm.Text = "Gestion des pays"
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' GestionPays
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(884, 561)
        Controls.Add(pnlForm)
        Controls.Add(stsStatus)
        MinimumSize = New Size(900, 600)
        Name = "GestionPays"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion des Pays"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        pnlForm.PerformLayout()
        tlpCenter.ResumeLayout(False)
        CType(dgvPays, ComponentModel.ISupportInitialize).EndInit()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
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
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents dgvPays As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblNomPays As Label
    Friend WithEvents lblAbrevPays As Label
    Friend WithEvents lblIso2 As Label
    Friend WithEvents lblIso3 As Label
    Friend WithEvents lblCodePays As Label
    Friend WithEvents lblMode As Label
    Private WithEvents txtNomPays As TextBox
    Friend WithEvents txtAbrevPays As TextBox
    Friend WithEvents txtIso2 As TextBox
    Friend WithEvents txtIso3 As TextBox
    Friend WithEvents txtCodePays As TextBox
    Friend WithEvents txtIdPays As TextBox
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblCount As Label
    Friend WithEvents btnClearSearch As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
End Class
