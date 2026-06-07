<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_Pays
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
        dgvPays = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblNomPays = New Label()
        lblIso2 = New Label()
        lblIso3 = New Label()
        lblCodePays = New Label()
        txtNomPays = New TextBox()
        txtIso2 = New TextBox()
        txtIso3 = New TextBox()
        txtCodePays = New TextBox()
        txtIdPays = New TextBox()
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
        CType(dgvPays, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.Controls.Add(tlpCenter)
        pnlMain.Controls.Add(pnlActions)
        pnlMain.Controls.Add(pnlTop)
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Name = "pnlMain"
        pnlMain.Size = New Size(1000, 650)
        pnlMain.TabIndex = 0
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520.0F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpCenter.Controls.Add(dgvPays, 0, 0)
        tlpCenter.Controls.Add(grpDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(0, 50)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpCenter.Size = New Size(1000, 550)
        tlpCenter.TabIndex = 2
        ' 
        ' dgvPays
        ' 
        dgvPays.AllowUserToAddRows = False
        dgvPays.AllowUserToDeleteRows = False
        dgvPays.AllowUserToResizeColumns = False
        dgvPays.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPays.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPays.Dock = DockStyle.Fill
        dgvPays.Location = New Point(3, 3)
        dgvPays.MultiSelect = False
        dgvPays.Name = "dgvPays"
        dgvPays.ReadOnly = True
        dgvPays.RowHeadersVisible = False
        dgvPays.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPays.Size = New Size(514, 544)
        dgvPays.TabIndex = 0
        ' 
        ' grpDetails
        ' 
        grpDetails.Controls.Add(tlpDetails)
        grpDetails.Dock = DockStyle.Fill
        grpDetails.Location = New Point(523, 3)
        grpDetails.Name = "grpDetails"
        grpDetails.Padding = New Padding(10)
        grpDetails.Size = New Size(474, 544)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Détails du pays"
        ' 
        ' tlpDetails
        ' 
        tlpDetails.ColumnCount = 2
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120.0F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpDetails.Controls.Add(lblNomPays, 0, 0)
        tlpDetails.Controls.Add(lblIso2, 0, 1)
        tlpDetails.Controls.Add(lblIso3, 0, 2)
        tlpDetails.Controls.Add(lblCodePays, 0, 3)
        tlpDetails.Controls.Add(txtNomPays, 1, 0)
        tlpDetails.Controls.Add(txtIso2, 1, 1)
        tlpDetails.Controls.Add(txtIso3, 1, 2)
        tlpDetails.Controls.Add(txtCodePays, 1, 3)
        tlpDetails.Controls.Add(txtIdPays, 1, 4)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(10, 26)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.RowCount = 6
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 35.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 35.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 35.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 35.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 35.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpDetails.Size = New Size(454, 508)
        tlpDetails.TabIndex = 0
        ' 
        ' lblNomPays
        ' 
        lblNomPays.Anchor = AnchorStyles.Left
        lblNomPays.AutoSize = True
        lblNomPays.Location = New Point(3, 10)
        lblNomPays.Name = "lblNomPays"
        lblNomPays.Size = New Size(75, 15)
        lblNomPays.TabIndex = 0
        lblNomPays.Text = "Nom du pays"
        ' 
        ' lblIso2
        ' 
        lblIso2.Anchor = AnchorStyles.Left
        lblIso2.AutoSize = True
        lblIso2.Location = New Point(3, 45)
        lblIso2.Name = "lblIso2"
        lblIso2.Size = New Size(32, 15)
        lblIso2.TabIndex = 1
        lblIso2.Text = "ISO2"
        ' 
        ' lblIso3
        ' 
        lblIso3.Anchor = AnchorStyles.Left
        lblIso3.AutoSize = True
        lblIso3.Location = New Point(3, 80)
        lblIso3.Name = "lblIso3"
        lblIso3.Size = New Size(32, 15)
        lblIso3.TabIndex = 2
        lblIso3.Text = "ISO3"
        ' 
        ' lblCodePays
        ' 
        lblCodePays.Anchor = AnchorStyles.Left
        lblCodePays.AutoSize = True
        lblCodePays.Location = New Point(3, 115)
        lblCodePays.Name = "lblCodePays"
        lblCodePays.Size = New Size(64, 15)
        lblCodePays.TabIndex = 3
        lblCodePays.Text = "Code Pays"
        ' 
        ' txtNomPays
        ' 
        txtNomPays.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtNomPays.Location = New Point(123, 6)
        txtNomPays.Name = "txtNomPays"
        txtNomPays.Size = New Size(328, 23)
        txtNomPays.TabIndex = 4
        ' 
        ' txtIso2
        ' 
        txtIso2.Anchor = AnchorStyles.Left
        txtIso2.Location = New Point(123, 41)
        txtIso2.MaxLength = 2
        txtIso2.Name = "txtIso2"
        txtIso2.Size = New Size(50, 23)
        txtIso2.TabIndex = 5
        ' 
        ' txtIso3
        ' 
        txtIso3.Anchor = AnchorStyles.Left
        txtIso3.Location = New Point(123, 76)
        txtIso3.MaxLength = 3
        txtIso3.Name = "txtIso3"
        txtIso3.Size = New Size(60, 23)
        txtIso3.TabIndex = 6
        ' 
        ' txtCodePays
        ' 
        txtCodePays.Anchor = AnchorStyles.Left
        txtCodePays.BackColor = SystemColors.Control
        txtCodePays.Location = New Point(123, 111)
        txtCodePays.Name = "txtCodePays"
        txtCodePays.ReadOnly = True
        txtCodePays.Size = New Size(100, 23)
        txtCodePays.TabIndex = 7
        txtCodePays.TabStop = False
        ' 
        ' txtIdPays
        ' 
        txtIdPays.Location = New Point(123, 143)
        txtIdPays.Name = "txtIdPays"
        txtIdPays.Size = New Size(100, 23)
        txtIdPays.TabIndex = 8
        txtIdPays.Visible = False
        ' 
        ' pnlActions
        ' 
        pnlActions.Controls.Add(btnEdit)
        pnlActions.Controls.Add(btnDelete)
        pnlActions.Controls.Add(btnCancel)
        pnlActions.Controls.Add(btnSave)
        pnlActions.Controls.Add(btnNew)
        pnlActions.Dock = DockStyle.Bottom
        pnlActions.Location = New Point(0, 600)
        pnlActions.Name = "pnlActions"
        pnlActions.Size = New Size(1000, 50)
        pnlActions.TabIndex = 1
        ' 
        ' btnEdit
        ' 
        btnEdit.Location = New Point(110, 10)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(100, 30)
        btnEdit.TabIndex = 1
        btnEdit.Text = "Modifier"
        btnEdit.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(220, 10)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(100, 30)
        btnDelete.TabIndex = 2
        btnDelete.Text = "Supprimer"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(440, 10)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(100, 30)
        btnCancel.TabIndex = 4
        btnCancel.Text = "Annuler"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(330, 10)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(100, 30)
        btnSave.TabIndex = 3
        btnSave.Text = "Enregistrer"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnNew
        ' 
        btnNew.Location = New Point(10, 10)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(90, 30)
        btnNew.TabIndex = 0
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
        pnlTop.Location = New Point(0, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Size = New Size(1000, 50)
        pnlTop.TabIndex = 0
        ' 
        ' lblCount
        ' 
        lblCount.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCount.Location = New Point(850, 15)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(140, 20)
        lblCount.TabIndex = 4
        lblCount.Text = "0 pays"
        lblCount.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(420, 12)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(80, 26)
        btnClearSearch.TabIndex = 3
        btnClearSearch.Text = "Effacer"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(330, 12)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(80, 26)
        btnSearch.TabIndex = 2
        btnSearch.Text = "Rechercher"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(90, 14)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(230, 23)
        txtSearch.TabIndex = 1
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(10, 17)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(62, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Recherche"
        ' 
        ' UC_Pays
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlMain)
        Name = "UC_Pays"
        Size = New Size(1000, 650)
        pnlMain.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvPays, ComponentModel.ISupportInitialize).EndInit()
        grpDetails.ResumeLayout(False)
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
        pnlActions.ResumeLayout(False)
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents dgvPays As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblNomPays As Label
    Friend WithEvents lblIso2 As Label
    Friend WithEvents lblIso3 As Label
    Friend WithEvents lblCodePays As Label
    Friend WithEvents txtNomPays As TextBox
    Friend WithEvents txtIso2 As TextBox
    Friend WithEvents txtIso3 As TextBox
    Friend WithEvents txtCodePays As TextBox
    Friend WithEvents txtIdPays As TextBox
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
