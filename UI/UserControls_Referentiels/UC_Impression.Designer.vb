<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_Impression
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
        dgvImpression = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblCodeImpression = New Label()
        txtCodeImpression = New TextBox()
        lblNomImpression = New Label()
        txtNomImpression = New TextBox()
        lblDescription = New Label()
        txtDescriptionImpression = New TextBox()
        lblEnvieCal = New Label()
        txtEnvieCal = New TextBox()
        lblIsActif = New Label()
        chkIsActif = New CheckBox()
        lblNote = New Label()
        ucToolbar = New UC_RichTextToolbar()
        rtbNote = New RichTextBox()
        txtIdImpression = New TextBox()
        pnlActions = New Panel()
        btnNew = New Button()
        btnEdit = New Button()
        btnSave = New Button()
        btnCancel = New Button()
        btnDelete = New Button()
        pnlTop = New Panel()
        lblSearch = New Label()
        txtSearch = New TextBox()
        btnSearch = New Button()
        btnClearSearch = New Button()
        chkSearchNotes = New CheckBox()
        lblCount = New Label()
        pnlMain.SuspendLayout()
        tlpCenter.SuspendLayout()
        CType(dgvImpression, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.OldLace
        pnlMain.Controls.Add(tlpCenter)
        pnlMain.Controls.Add(pnlActions)
        pnlMain.Controls.Add(pnlTop)
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(8)
        pnlMain.Size = New Size(900, 600)
        pnlMain.TabIndex = 0
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 500.0F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpCenter.Controls.Add(dgvImpression, 0, 0)
        tlpCenter.Controls.Add(grpDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 56)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpCenter.Size = New Size(884, 488)
        tlpCenter.TabIndex = 2
        ' 
        ' dgvImpression
        ' 
        dgvImpression.AllowUserToAddRows = False
        dgvImpression.AllowUserToDeleteRows = False
        dgvImpression.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvImpression.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvImpression.Dock = DockStyle.Fill
        dgvImpression.Location = New Point(3, 3)
        dgvImpression.MultiSelect = False
        dgvImpression.Name = "dgvImpression"
        dgvImpression.ReadOnly = True
        dgvImpression.RowHeadersVisible = False
        dgvImpression.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvImpression.Size = New Size(494, 482)
        dgvImpression.TabIndex = 0
        ' 
        ' grpDetails
        ' 
        grpDetails.Controls.Add(tlpDetails)
        grpDetails.Dock = DockStyle.Fill
        grpDetails.Location = New Point(503, 3)
        grpDetails.Name = "grpDetails"
        grpDetails.Size = New Size(378, 482)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Détails"
        ' 
        ' tlpDetails
        ' 
        tlpDetails.AutoSize = True
        tlpDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpDetails.ColumnCount = 2
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100.0F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpDetails.Controls.Add(lblCodeImpression, 0, 0)
        tlpDetails.Controls.Add(txtCodeImpression, 1, 0)
        tlpDetails.Controls.Add(lblNomImpression, 0, 1)
        tlpDetails.Controls.Add(txtNomImpression, 1, 1)
        tlpDetails.Controls.Add(lblDescription, 0, 2)
        tlpDetails.Controls.Add(txtDescriptionImpression, 1, 2)
        tlpDetails.Controls.Add(lblEnvieCal, 0, 3)
        tlpDetails.Controls.Add(txtEnvieCal, 1, 3)
        tlpDetails.Controls.Add(lblIsActif, 0, 4)
        tlpDetails.Controls.Add(chkIsActif, 1, 4)
        tlpDetails.Controls.Add(lblNote, 0, 5)
        tlpDetails.Controls.Add(ucToolbar, 1, 5)
        tlpDetails.Controls.Add(rtbNote, 0, 6)
        tlpDetails.Controls.Add(txtIdImpression, 1, 7)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(3, 19)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(5)
        tlpDetails.RowCount = 8
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 50.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5.0F))
        tlpDetails.Size = New Size(372, 460)
        tlpDetails.TabIndex = 0
        ' 
        ' lblCodeImpression
        ' 
        lblCodeImpression.Anchor = AnchorStyles.Left
        lblCodeImpression.AutoSize = True
        lblCodeImpression.Location = New Point(8, 12)
        lblCodeImpression.Name = "lblCodeImpression"
        lblCodeImpression.Size = New Size(35, 15)
        lblCodeImpression.TabIndex = 0
        lblCodeImpression.Text = "Code"
        ' 
        ' txtCodeImpression
        ' 
        txtCodeImpression.Anchor = AnchorStyles.Left
        txtCodeImpression.Location = New Point(108, 8)
        txtCodeImpression.Name = "txtCodeImpression"
        txtCodeImpression.ReadOnly = True
        txtCodeImpression.Size = New Size(80, 23)
        txtCodeImpression.TabIndex = 1
        txtCodeImpression.TabStop = False
        ' 
        ' lblNomImpression
        ' 
        lblNomImpression.Anchor = AnchorStyles.Left
        lblNomImpression.AutoSize = True
        lblNomImpression.Location = New Point(8, 42)
        lblNomImpression.Name = "lblNomImpression"
        lblNomImpression.Size = New Size(34, 15)
        lblNomImpression.TabIndex = 2
        lblNomImpression.Text = "Nom"
        ' 
        ' txtNomImpression
        ' 
        txtNomImpression.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtNomImpression.Location = New Point(108, 38)
        txtNomImpression.MaxLength = 200
        txtNomImpression.Name = "txtNomImpression"
        txtNomImpression.Size = New Size(256, 23)
        txtNomImpression.TabIndex = 0
        ' 
        ' lblDescription
        ' 
        lblDescription.Anchor = AnchorStyles.Left
        lblDescription.AutoSize = True
        lblDescription.Location = New Point(8, 82)
        lblDescription.Name = "lblDescription"
        lblDescription.Size = New Size(67, 15)
        lblDescription.TabIndex = 4
        lblDescription.Text = "Description"
        ' 
        ' txtDescriptionImpression
        ' 
        txtDescriptionImpression.Dock = DockStyle.Fill
        txtDescriptionImpression.Location = New Point(108, 68)
        txtDescriptionImpression.MaxLength = 500
        txtDescriptionImpression.Multiline = True
        txtDescriptionImpression.Name = "txtDescriptionImpression"
        txtDescriptionImpression.Size = New Size(256, 44)
        txtDescriptionImpression.TabIndex = 1
        ' 
        ' lblEnvieCal
        ' 
        lblEnvieCal.Anchor = AnchorStyles.Left
        lblEnvieCal.AutoSize = True
        lblEnvieCal.Location = New Point(8, 127)
        lblEnvieCal.Name = "lblEnvieCal"
        lblEnvieCal.Size = New Size(57, 15)
        lblEnvieCal.TabIndex = 6
        lblEnvieCal.Text = "Envie Cal"
        ' 
        ' txtEnvieCal
        ' 
        txtEnvieCal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtEnvieCal.Location = New Point(108, 123)
        txtEnvieCal.MaxLength = 200
        txtEnvieCal.Name = "txtEnvieCal"
        txtEnvieCal.Size = New Size(256, 23)
        txtEnvieCal.TabIndex = 2
        ' 
        ' lblIsActif
        ' 
        lblIsActif.Anchor = AnchorStyles.Left
        lblIsActif.AutoSize = True
        lblIsActif.Location = New Point(8, 157)
        lblIsActif.Name = "lblIsActif"
        lblIsActif.Size = New Size(32, 15)
        lblIsActif.TabIndex = 8
        lblIsActif.Text = "Actif"
        ' 
        ' chkIsActif
        ' 
        chkIsActif.Anchor = AnchorStyles.Left
        chkIsActif.AutoSize = True
        chkIsActif.Checked = True
        chkIsActif.CheckState = CheckState.Checked
        chkIsActif.Location = New Point(108, 157)
        chkIsActif.Name = "chkIsActif"
        chkIsActif.Size = New Size(15, 14)
        chkIsActif.TabIndex = 3
        chkIsActif.UseVisualStyleBackColor = True
        ' 
        ' lblNote
        ' 
        lblNote.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblNote.AutoSize = True
        lblNote.Location = New Point(8, 195)
        lblNote.Name = "lblNote"
        lblNote.Size = New Size(33, 15)
        lblNote.TabIndex = 10
        lblNote.Text = "Note"
        ' 
        ' ucToolbar
        ' 
        ucToolbar.Anchor = AnchorStyles.Left
        ucToolbar.Location = New Point(105, 188)
        ucToolbar.Name = "ucToolbar"
        ucToolbar.Size = New Size(260, 25)
        ucToolbar.TabIndex = 11
        ' 
        ' rtbNote
        ' 
        rtbNote.AcceptsTab = True
        tlpDetails.SetColumnSpan(rtbNote, 2)
        rtbNote.Dock = DockStyle.Fill
        rtbNote.Location = New Point(8, 213)
        rtbNote.Name = "rtbNote"
        rtbNote.Size = New Size(356, 234)
        rtbNote.TabIndex = 4
        rtbNote.Text = ""
        ' 
        ' txtIdImpression
        ' 
        txtIdImpression.Location = New Point(108, 453)
        txtIdImpression.Name = "txtIdImpression"
        txtIdImpression.Size = New Size(74, 23)
        txtIdImpression.TabIndex = 13
        txtIdImpression.TabStop = False
        txtIdImpression.Visible = False
        ' 
        ' pnlActions
        ' 
        pnlActions.Controls.Add(btnNew)
        pnlActions.Controls.Add(btnEdit)
        pnlActions.Controls.Add(btnSave)
        pnlActions.Controls.Add(btnCancel)
        pnlActions.Controls.Add(btnDelete)
        pnlActions.Dock = DockStyle.Bottom
        pnlActions.Location = New Point(8, 544)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(8)
        pnlActions.Size = New Size(884, 48)
        pnlActions.TabIndex = 3
        ' 
        ' btnNew
        ' 
        btnNew.Location = New Point(11, 11)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(90, 28)
        btnNew.TabIndex = 0
        btnNew.Text = "Nouveau"
        btnNew.UseVisualStyleBackColor = True
        ' 
        ' btnEdit
        ' 
        btnEdit.Location = New Point(107, 11)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(90, 28)
        btnEdit.TabIndex = 1
        btnEdit.Text = "Modifier"
        btnEdit.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Location = New Point(203, 11)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(90, 28)
        btnSave.TabIndex = 2
        btnSave.Text = "Enregistrer"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Enabled = False
        btnCancel.Location = New Point(299, 11)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(90, 28)
        btnCancel.TabIndex = 3
        btnCancel.Text = "Annuler"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(395, 11)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 28)
        btnDelete.TabIndex = 4
        btnDelete.Text = "Supprimer"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' pnlTop
        ' 
        pnlTop.Controls.Add(lblSearch)
        pnlTop.Controls.Add(txtSearch)
        pnlTop.Controls.Add(btnSearch)
        pnlTop.Controls.Add(btnClearSearch)
        pnlTop.Controls.Add(chkSearchNotes)
        pnlTop.Controls.Add(lblCount)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(8, 8)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(884, 48)
        pnlTop.TabIndex = 1
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Location = New Point(11, 17)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(66, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Rechercher"
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(83, 13)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(250, 23)
        txtSearch.TabIndex = 0
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(339, 12)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(75, 25)
        btnSearch.TabIndex = 1
        btnSearch.Text = "Filtrer"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(420, 12)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 25)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' chkSearchNotes
        ' 
        chkSearchNotes.AutoSize = True
        chkSearchNotes.Location = New Point(458, 15)
        chkSearchNotes.Name = "chkSearchNotes"
        chkSearchNotes.Size = New Size(111, 19)
        chkSearchNotes.TabIndex = 3
        chkSearchNotes.Text = "Inclure les notes"
        chkSearchNotes.UseVisualStyleBackColor = True
        ' 
        ' lblCount
        ' 
        lblCount.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCount.AutoSize = True
        lblCount.Location = New Point(775, 17)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(83, 15)
        lblCount.TabIndex = 4
        lblCount.Text = "0 impression(s)"
        ' 
        ' UC_Impression
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlMain)
        Name = "UC_Impression"
        Size = New Size(900, 600)
        pnlMain.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvImpression, ComponentModel.ISupportInitialize).EndInit()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
        pnlActions.ResumeLayout(False)
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents dgvImpression As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblCodeImpression As Label
    Friend WithEvents txtCodeImpression As TextBox
    Friend WithEvents lblNomImpression As Label
    Friend WithEvents txtNomImpression As TextBox
    Friend WithEvents lblDescription As Label
    Friend WithEvents txtDescriptionImpression As TextBox
    Friend WithEvents lblEnvieCal As Label
    Friend WithEvents txtEnvieCal As TextBox
    Friend WithEvents lblIsActif As Label
    Friend WithEvents chkIsActif As CheckBox
    Friend WithEvents lblNote As Label
    Friend WithEvents ucToolbar As UC_RichTextToolbar
    Friend WithEvents rtbNote As RichTextBox
    Friend WithEvents txtIdImpression As TextBox
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnNew As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnClearSearch As Button
    Friend WithEvents chkSearchNotes As CheckBox
    Friend WithEvents lblCount As Label

End Class
