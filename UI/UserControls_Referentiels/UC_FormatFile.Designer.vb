<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_FormatFile
    Inherits System.Windows.Forms.UserControl

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlMain = New Panel()
        tlpCenter = New TableLayoutPanel()
        dgvFormatFile = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblNomFormat = New Label()
        lblExtension = New Label()
        lblMimeType = New Label()
        lblOrdreAffichage = New Label()
        lblIsActif = New Label()
        lblCodeFormatFile = New Label()
        txtNomFormat = New TextBox()
        txtExtension = New TextBox()
        txtMimeType = New TextBox()
        nudOrdreAffichage = New NumericUpDown()
        chkIsActif = New CheckBox()
        txtCodeFormatFile = New TextBox()
        txtIdFormatFile = New TextBox()
        pnlActions = New Panel()
        btnEdit = New Button()
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
        pnlMain.SuspendLayout()
        tlpCenter.SuspendLayout()
        CType(dgvFormatFile, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
        CType(nudOrdreAffichage, ComponentModel.ISupportInitialize).BeginInit()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.FloralWhite
        pnlMain.BorderStyle = BorderStyle.Fixed3D
        pnlMain.Controls.Add(tlpCenter)
        pnlMain.Controls.Add(pnlActions)
        pnlMain.Controls.Add(pnlTop)
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(8)
        pnlMain.Size = New Size(1000, 700)
        pnlMain.TabIndex = 0
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
        tlpCenter.Size = New Size(980, 580)
        tlpCenter.TabIndex = 2
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
        dgvFormatFile.Size = New Size(514, 574)
        dgvFormatFile.TabIndex = 0
        ' 
        ' grpDetails
        ' 
        grpDetails.Controls.Add(tlpDetails)
        grpDetails.Dock = DockStyle.Fill
        grpDetails.Location = New Point(523, 3)
        grpDetails.Name = "grpDetails"
        grpDetails.Padding = New Padding(6)
        grpDetails.Size = New Size(454, 574)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Détails"
        ' 
        ' tlpDetails
        ' 
        tlpDetails.AutoSize = True
        tlpDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpDetails.ColumnCount = 2
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpDetails.Controls.Add(lblNomFormat, 0, 0)
        tlpDetails.Controls.Add(lblExtension, 0, 1)
        tlpDetails.Controls.Add(lblMimeType, 0, 2)
        tlpDetails.Controls.Add(lblOrdreAffichage, 0, 3)
        tlpDetails.Controls.Add(lblIsActif, 0, 4)
        tlpDetails.Controls.Add(lblCodeFormatFile, 0, 5)
        tlpDetails.Controls.Add(txtNomFormat, 1, 0)
        tlpDetails.Controls.Add(txtExtension, 1, 1)
        tlpDetails.Controls.Add(txtMimeType, 1, 2)
        tlpDetails.Controls.Add(nudOrdreAffichage, 1, 3)
        tlpDetails.Controls.Add(chkIsActif, 1, 4)
        tlpDetails.Controls.Add(txtCodeFormatFile, 1, 5)
        tlpDetails.Controls.Add(txtIdFormatFile, 1, 6)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(6, 22)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(8, 30, 4, 8)
        tlpDetails.RowCount = 7
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 18F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 18F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 18F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 18F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 18F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 5F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 5F))
        tlpDetails.Size = New Size(442, 546)
        tlpDetails.TabIndex = 0
        ' 
        ' lblNomFormat
        ' 
        lblNomFormat.Anchor = AnchorStyles.Left
        lblNomFormat.AutoSize = True
        lblNomFormat.Location = New Point(11, 74)
        lblNomFormat.Name = "lblNomFormat"
        lblNomFormat.Size = New Size(77, 15)
        lblNomFormat.TabIndex = 0
        lblNomFormat.Text = "Nom format :"
        ' 
        ' lblExtension
        ' 
        lblExtension.Anchor = AnchorStyles.Left
        lblExtension.AutoSize = True
        lblExtension.Location = New Point(11, 166)
        lblExtension.Name = "lblExtension"
        lblExtension.Size = New Size(64, 15)
        lblExtension.TabIndex = 1
        lblExtension.Text = "Extension :"
        ' 
        ' lblMimeType
        ' 
        lblMimeType.Anchor = AnchorStyles.Left
        lblMimeType.AutoSize = True
        lblMimeType.Location = New Point(11, 258)
        lblMimeType.Name = "lblMimeType"
        lblMimeType.Size = New Size(73, 15)
        lblMimeType.TabIndex = 2
        lblMimeType.Text = "MIME Type :"
        ' 
        ' lblOrdreAffichage
        ' 
        lblOrdreAffichage.Anchor = AnchorStyles.Left
        lblOrdreAffichage.AutoSize = True
        lblOrdreAffichage.Location = New Point(11, 350)
        lblOrdreAffichage.Name = "lblOrdreAffichage"
        lblOrdreAffichage.Size = New Size(102, 15)
        lblOrdreAffichage.TabIndex = 3
        lblOrdreAffichage.Text = "Ordre affichage :"
        ' 
        ' lblIsActif
        ' 
        lblIsActif.Anchor = AnchorStyles.Left
        lblIsActif.AutoSize = True
        lblIsActif.Location = New Point(11, 442)
        lblIsActif.Name = "lblIsActif"
        lblIsActif.Size = New Size(36, 15)
        lblIsActif.TabIndex = 4
        lblIsActif.Text = "Actif :"
        ' 
        ' lblCodeFormatFile
        ' 
        lblCodeFormatFile.Anchor = AnchorStyles.Left
        lblCodeFormatFile.AutoSize = True
        lblCodeFormatFile.Location = New Point(11, 490)
        lblCodeFormatFile.Name = "lblCodeFormatFile"
        lblCodeFormatFile.Size = New Size(85, 15)
        lblCodeFormatFile.TabIndex = 11
        lblCodeFormatFile.Text = "Code (auto) :"
        ' 
        ' txtNomFormat
        ' 
        txtNomFormat.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtNomFormat.Location = New Point(151, 70)
        txtNomFormat.MaxLength = 100
        txtNomFormat.Name = "txtNomFormat"
        txtNomFormat.Size = New Size(284, 23)
        txtNomFormat.TabIndex = 5
        ' 
        ' txtExtension
        ' 
        txtExtension.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtExtension.Location = New Point(151, 162)
        txtExtension.MaxLength = 20
        txtExtension.Name = "txtExtension"
        txtExtension.Size = New Size(284, 23)
        txtExtension.TabIndex = 6
        ' 
        ' txtMimeType
        ' 
        txtMimeType.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtMimeType.Location = New Point(151, 254)
        txtMimeType.MaxLength = 100
        txtMimeType.Name = "txtMimeType"
        txtMimeType.Size = New Size(284, 23)
        txtMimeType.TabIndex = 7
        ' 
        ' nudOrdreAffichage
        ' 
        nudOrdreAffichage.Anchor = AnchorStyles.Left
        nudOrdreAffichage.Location = New Point(151, 346)
        nudOrdreAffichage.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudOrdreAffichage.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudOrdreAffichage.Name = "nudOrdreAffichage"
        nudOrdreAffichage.Size = New Size(100, 23)
        nudOrdreAffichage.TabIndex = 8
        nudOrdreAffichage.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' chkIsActif
        ' 
        chkIsActif.Anchor = AnchorStyles.Left
        chkIsActif.AutoSize = True
        chkIsActif.Checked = True
        chkIsActif.CheckState = CheckState.Checked
        chkIsActif.Location = New Point(151, 441)
        chkIsActif.Name = "chkIsActif"
        chkIsActif.Size = New Size(15, 14)
        chkIsActif.TabIndex = 9
        chkIsActif.UseVisualStyleBackColor = True
        ' 
        ' txtCodeFormatFile
        ' 
        txtCodeFormatFile.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtCodeFormatFile.BackColor = Color.LightGray
        txtCodeFormatFile.Location = New Point(151, 484)
        txtCodeFormatFile.Name = "txtCodeFormatFile"
        txtCodeFormatFile.ReadOnly = True
        txtCodeFormatFile.Size = New Size(284, 23)
        txtCodeFormatFile.TabIndex = 10
        txtCodeFormatFile.TabStop = False
        ' 
        ' txtIdFormatFile
        ' 
        txtIdFormatFile.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtIdFormatFile.Location = New Point(151, 512)
        txtIdFormatFile.Name = "txtIdFormatFile"
        txtIdFormatFile.ReadOnly = True
        txtIdFormatFile.Size = New Size(284, 23)
        txtIdFormatFile.TabIndex = 11
        txtIdFormatFile.TabStop = False
        txtIdFormatFile.Visible = False
        ' 
        ' pnlActions
        ' 
        pnlActions.Controls.Add(btnEdit)
        pnlActions.Controls.Add(btnDelete)
        pnlActions.Controls.Add(btnCancel)
        pnlActions.Controls.Add(btnSave)
        pnlActions.Controls.Add(btnNew)
        pnlActions.Dock = DockStyle.Bottom
        pnlActions.Location = New Point(8, 636)
        pnlActions.Name = "pnlActions"
        pnlActions.Size = New Size(980, 52)
        pnlActions.TabIndex = 3
        ' 
        ' btnEdit
        ' 
        btnEdit.Location = New Point(120, 10)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(100, 32)
        btnEdit.TabIndex = 11
        btnEdit.Text = "✏ Modifier"
        btnEdit.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(230, 10)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(100, 32)
        btnDelete.TabIndex = 12
        btnDelete.Text = "🗑 Supprimer"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(460, 10)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(100, 32)
        btnCancel.TabIndex = 14
        btnCancel.Text = "Annuler"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(350, 10)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(100, 32)
        btnSave.TabIndex = 13
        btnSave.Text = "💾 Enregistrer"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnNew
        ' 
        btnNew.Location = New Point(10, 10)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(100, 32)
        btnNew.TabIndex = 10
        btnNew.Text = "➕ Nouveau"
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
        pnlTop.Size = New Size(980, 48)
        pnlTop.TabIndex = 1
        ' 
        ' lblCount
        ' 
        lblCount.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCount.AutoSize = True
        lblCount.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        lblCount.Location = New Point(880, 16)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(58, 15)
        lblCount.TabIndex = 4
        lblCount.Text = "0 format(s)"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(330, 10)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(75, 28)
        btnClearSearch.TabIndex = 3
        btnClearSearch.Text = "❌ Effacer"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(240, 10)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(80, 28)
        btnSearch.TabIndex = 2
        btnSearch.Text = "🔍 Chercher"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(70, 12)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(160, 23)
        txtSearch.TabIndex = 1
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(10, 15)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(54, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Filtre :"
        '
        'UC_FormatFile
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlMain)
        Font = New Font("Segoe UI", 9F)
        Name = "UC_FormatFile"
        Size = New Size(1000, 700)
        pnlMain.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvFormatFile, ComponentModel.ISupportInitialize).EndInit()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
        CType(nudOrdreAffichage, ComponentModel.ISupportInitialize).EndInit()
        pnlActions.ResumeLayout(False)
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents dgvFormatFile As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblNomFormat As Label
    Friend WithEvents lblExtension As Label
    Friend WithEvents lblMimeType As Label
    Friend WithEvents lblOrdreAffichage As Label
    Friend WithEvents lblIsActif As Label
    Friend WithEvents lblCodeFormatFile As Label
    Friend WithEvents txtNomFormat As TextBox
    Friend WithEvents txtExtension As TextBox
    Friend WithEvents txtMimeType As TextBox
    Friend WithEvents nudOrdreAffichage As NumericUpDown
    Friend WithEvents chkIsActif As CheckBox
    Friend WithEvents txtCodeFormatFile As TextBox
    Friend WithEvents txtIdFormatFile As TextBox
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnEdit As Button
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
End Class
