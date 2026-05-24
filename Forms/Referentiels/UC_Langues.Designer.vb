<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Langues
    Inherits System.Windows.Forms.UserControl

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        pnlForm = New Panel()
        tlpCenter = New TableLayoutPanel()
        dgvLangues = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblNomLangue = New Label()
        lblAbreLangue = New Label()
        lblIso639_1 = New Label()
        lblIso639_2 = New Label()
        lblCodeLangue = New Label()
        lblMode = New Label()
        txtNomLangue = New TextBox()
        txtAbrevLangue = New TextBox()
        txtIso639_1 = New TextBox()
        txtIso639_2 = New TextBox()
        txtCodeLangue = New TextBox()
        txtIdLangue = New TextBox()
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
        pnlForm.SuspendLayout()
        tlpCenter.SuspendLayout()
        CType(dgvLangues, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlForm
        ' 
        pnlForm.AutoSize = True
        pnlForm.BackColor = Color.FloralWhite
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tlpCenter)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Controls.Add(lblTitreForm)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(0, 0)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(900, 560)
        pnlForm.TabIndex = 0
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520.0F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpCenter.Controls.Add(dgvLangues, 0, 0)
        tlpCenter.Controls.Add(grpDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 83)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpCenter.Size = New Size(884, 417)
        tlpCenter.TabIndex = 3
        ' 
        ' dgvLangues
        ' 
        dgvLangues.AllowUserToAddRows = False
        dgvLangues.AllowUserToDeleteRows = False
        dgvLangues.AllowUserToResizeColumns = False
        dgvLangues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvLangues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvLangues.Dock = DockStyle.Fill
        dgvLangues.Location = New Point(3, 3)
        dgvLangues.MultiSelect = False
        dgvLangues.Name = "dgvLangues"
        dgvLangues.ReadOnly = True
        dgvLangues.RowHeadersVisible = False
        dgvLangues.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvLangues.Size = New Size(514, 411)
        dgvLangues.TabIndex = 0
        ' 
        ' grpDetails
        ' 
        grpDetails.Controls.Add(tlpDetails)
        grpDetails.Dock = DockStyle.Fill
        grpDetails.Location = New Point(523, 3)
        grpDetails.Name = "grpDetails"
        grpDetails.Padding = New Padding(6)
        grpDetails.Size = New Size(358, 411)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Détails"
        ' 
        ' tlpDetails
        ' 
        tlpDetails.AutoSize = True
        tlpDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpDetails.ColumnCount = 2
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120.0F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpDetails.Controls.Add(lblNomLangue, 0, 0)
        tlpDetails.Controls.Add(lblAbreLangue, 0, 1)
        tlpDetails.Controls.Add(lblIso639_1, 0, 2)
        tlpDetails.Controls.Add(lblIso639_2, 0, 3)
        tlpDetails.Controls.Add(lblCodeLangue, 0, 4)
        tlpDetails.Controls.Add(lblMode, 0, 5)
        tlpDetails.Controls.Add(txtNomLangue, 1, 0)
        tlpDetails.Controls.Add(txtAbrevLangue, 1, 1)
        tlpDetails.Controls.Add(txtIso639_1, 1, 2)
        tlpDetails.Controls.Add(txtIso639_2, 1, 3)
        tlpDetails.Controls.Add(txtCodeLangue, 1, 4)
        tlpDetails.Controls.Add(txtIdLangue, 1, 5)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(6, 22)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(8, 30, 4, 8)
        tlpDetails.RowCount = 6
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17.1717167F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.1616154F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17.1717167F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 17.1717167F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.1616154F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.1616154F))
        tlpDetails.Size = New Size(346, 383)
        tlpDetails.TabIndex = 0
        ' 
        ' lblNomLangue
        ' 
        lblNomLangue.AutoSize = True
        lblNomLangue.Location = New Point(11, 30)
        lblNomLangue.Name = "lblNomLangue"
        lblNomLangue.Size = New Size(34, 15)
        lblNomLangue.TabIndex = 0
        lblNomLangue.Text = "Nom"
        ' 
        ' lblAbreLangue
        ' 
        lblAbreLangue.AutoSize = True
        lblAbreLangue.Location = New Point(11, 88)
        lblAbreLangue.Name = "lblAbreLangue"
        lblAbreLangue.Size = New Size(68, 15)
        lblAbreLangue.TabIndex = 1
        lblAbreLangue.Text = "Abréviation"
        ' 
        ' lblIso639_1
        ' 
        lblIso639_1.AutoSize = True
        lblIso639_1.Location = New Point(11, 141)
        lblIso639_1.Name = "lblIso639_1"
        lblIso639_1.Size = New Size(57, 15)
        lblIso639_1.TabIndex = 2
        lblIso639_1.Text = "ISO 639-1"
        ' 
        ' lblIso639_2
        ' 
        lblIso639_2.AutoSize = True
        lblIso639_2.Location = New Point(11, 199)
        lblIso639_2.Name = "lblIso639_2"
        lblIso639_2.Size = New Size(57, 15)
        lblIso639_2.TabIndex = 3
        lblIso639_2.Text = "ISO 639-2"
        ' 
        ' lblCodeLangue
        ' 
        lblCodeLangue.AutoSize = True
        lblCodeLangue.Location = New Point(11, 257)
        lblCodeLangue.Name = "lblCodeLangue"
        lblCodeLangue.Size = New Size(35, 15)
        lblCodeLangue.TabIndex = 4
        lblCodeLangue.Text = "Code"
        ' 
        ' lblMode
        ' 
        lblMode.AutoSize = True
        lblMode.ForeColor = Color.Gray
        lblMode.Location = New Point(11, 310)
        lblMode.Name = "lblMode"
        lblMode.Size = New Size(75, 15)
        lblMode.TabIndex = 5
        lblMode.Text = "Consultation"
        ' 
        ' txtNomLangue
        ' 
        txtNomLangue.Dock = DockStyle.Fill
        txtNomLangue.Location = New Point(131, 33)
        txtNomLangue.MaxLength = 120
        txtNomLangue.Name = "txtNomLangue"
        txtNomLangue.Size = New Size(208, 23)
        txtNomLangue.TabIndex = 6
        ' 
        ' txtAbrevLangue
        ' 
        txtAbrevLangue.CharacterCasing = CharacterCasing.Upper
        txtAbrevLangue.Dock = DockStyle.Fill
        txtAbrevLangue.Location = New Point(131, 91)
        txtAbrevLangue.MaxLength = 10
        txtAbrevLangue.Name = "txtAbrevLangue"
        txtAbrevLangue.Size = New Size(208, 23)
        txtAbrevLangue.TabIndex = 7
        ' 
        ' txtIso639_1
        ' 
        txtIso639_1.CharacterCasing = CharacterCasing.Lower
        txtIso639_1.Dock = DockStyle.Left
        txtIso639_1.Location = New Point(131, 144)
        txtIso639_1.MaxLength = 2
        txtIso639_1.Name = "txtIso639_1"
        txtIso639_1.Size = New Size(60, 23)
        txtIso639_1.TabIndex = 8
        ' 
        ' txtIso639_2
        ' 
        txtIso639_2.CharacterCasing = CharacterCasing.Lower
        txtIso639_2.Dock = DockStyle.Left
        txtIso639_2.Location = New Point(131, 202)
        txtIso639_2.MaxLength = 3
        txtIso639_2.Name = "txtIso639_2"
        txtIso639_2.Size = New Size(60, 23)
        txtIso639_2.TabIndex = 9
        ' 
        ' txtCodeLangue
        ' 
        txtCodeLangue.Dock = DockStyle.Left
        txtCodeLangue.Location = New Point(131, 260)
        txtCodeLangue.Name = "txtCodeLangue"
        txtCodeLangue.ReadOnly = True
        txtCodeLangue.Size = New Size(120, 23)
        txtCodeLangue.TabIndex = 10
        txtCodeLangue.TabStop = False
        ' 
        ' txtIdLangue
        ' 
        txtIdLangue.Location = New Point(131, 313)
        txtIdLangue.Name = "txtIdLangue"
        txtIdLangue.Size = New Size(100, 23)
        txtIdLangue.TabIndex = 11
        txtIdLangue.TabStop = False
        txtIdLangue.Visible = False
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
        pnlActions.Location = New Point(8, 500)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(8)
        pnlActions.Size = New Size(884, 48)
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
        btnClose.Size = New Size(110, 23)
        btnClose.TabIndex = 34
        btnClose.Text = "Retour accueil"
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
        pnlTop.Size = New Size(884, 48)
        pnlTop.TabIndex = 1
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(760, 16)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(65, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 langue(s)"
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
        lblSearch.Location = New Point(16, 13)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(66, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Rechercher"
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.Dock = DockStyle.Top
        lblTitreForm.Font = New Font("Calibri", 14.0F, FontStyle.Bold)
        lblTitreForm.Location = New Point(8, 8)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Padding = New Padding(0, 0, 0, 4)
        lblTitreForm.Size = New Size(168, 27)
        lblTitreForm.TabIndex = 0
        lblTitreForm.Text = "Gestion des langues"
        ' 
        ' UC_Langues
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlForm)
        Name = "UC_Langues"
        Size = New Size(900, 560)
        pnlForm.ResumeLayout(False)
        pnlForm.PerformLayout()
        tlpCenter.ResumeLayout(False)
        CType(dgvLangues, ComponentModel.ISupportInitialize).EndInit()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
        pnlActions.ResumeLayout(False)
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlForm As Panel
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents pnlTop As Panel
    Friend WithEvents pnlActions As Panel
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents dgvLangues As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblNomLangue As Label
    Friend WithEvents lblAbreLangue As Label
    Friend WithEvents lblIso639_1 As Label
    Friend WithEvents lblIso639_2 As Label
    Friend WithEvents lblCodeLangue As Label
    Friend WithEvents lblMode As Label
    Friend WithEvents txtAbrevLangue As TextBox
    Friend WithEvents txtIso639_1 As TextBox
    Friend WithEvents txtIso639_2 As TextBox
    Friend WithEvents txtCodeLangue As TextBox
    Friend WithEvents txtIdLangue As TextBox
    Private WithEvents txtNomLangue As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnClearSearch As Button
    Friend WithEvents lblCount As Label
    Friend WithEvents btnNew As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnEdit As Button
End Class
