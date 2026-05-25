<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionImpression
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GestionImpression))
        lblTitreForm = New Label()
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        pnlForm = New Panel()
        tlpCenter = New TableLayoutPanel()
        dgvImpression = New DataGridView()
        grpDetail = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        tsNotes = New ToolStrip()
        btnBold = New ToolStripButton()
        btnItalic = New ToolStripButton()
        btnUnderline = New ToolStripButton()
        btnBullet = New ToolStripButton()
        btnTab = New ToolStripButton()
        rtbNote = New RichTextBox()
        txtCodeImpression = New TextBox()
        lblCodeEImpression = New Label()
        txtIdImpression = New TextBox()
        chkIsActif = New CheckBox()
        lblDescriptionImpression = New Label()
        lblNomImpression = New Label()
        txtDescriptionImpression = New TextBox()
        txtNomImpression = New TextBox()
        lblEnvieCal = New Label()
        txtEnvieCal = New TextBox()
        lblNotes = New Label()
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
        btnClearSearch = New Button()
        btnSearch = New Button()
        txtSearch = New TextBox()
        lblSearch = New Label()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        stsStatus.SuspendLayout()
        pnlForm.SuspendLayout()
        tlpCenter.SuspendLayout()
        CType(dgvImpression, ComponentModel.ISupportInitialize).BeginInit()
        grpDetail.SuspendLayout()
        tlpDetails.SuspendLayout()
        tsNotes.SuspendLayout()
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
        lblTitreForm.Size = New Size(203, 27)
        lblTitreForm.TabIndex = 3
        lblTitreForm.Text = "Gestion des Impressions"
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(8, 626)
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
        pnlForm.BackColor = Color.Azure
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tlpCenter)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(8, 31)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(868, 595)
        pnlForm.TabIndex = 18
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenter.Controls.Add(dgvImpression, 0, 0)
        tlpCenter.Controls.Add(grpDetail, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 56)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(848, 479)
        tlpCenter.TabIndex = 6
        ' 
        ' dgvImpression
        ' 
        dgvImpression.AllowUserToAddRows = False
        dgvImpression.AllowUserToDeleteRows = False
        dgvImpression.AllowUserToResizeColumns = False
        dgvImpression.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvImpression.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvImpression.Dock = DockStyle.Fill
        dgvImpression.Location = New Point(3, 3)
        dgvImpression.MultiSelect = False
        dgvImpression.Name = "dgvImpression"
        dgvImpression.ReadOnly = True
        dgvImpression.RowHeadersVisible = False
        dgvImpression.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvImpression.Size = New Size(514, 473)
        dgvImpression.TabIndex = 3
        ' 
        ' grpDetail
        ' 
        grpDetail.Controls.Add(tlpDetails)
        grpDetail.Dock = DockStyle.Fill
        grpDetail.Location = New Point(523, 3)
        grpDetail.Name = "grpDetail"
        grpDetail.Padding = New Padding(6)
        grpDetail.Size = New Size(322, 473)
        grpDetail.TabIndex = 4
        grpDetail.TabStop = False
        grpDetail.Text = "Détails"
        ' 
        ' tlpDetails
        ' 
        tlpDetails.AutoSize = True
        tlpDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink
        tlpDetails.ColumnCount = 2
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpDetails.Controls.Add(tsNotes, 1, 4)
        tlpDetails.Controls.Add(rtbNote, 0, 5)
        tlpDetails.Controls.Add(txtCodeImpression, 1, 0)
        tlpDetails.Controls.Add(lblCodeEImpression, 0, 0)
        tlpDetails.Controls.Add(txtIdImpression, 1, 6)
        tlpDetails.Controls.Add(chkIsActif, 0, 6)
        tlpDetails.Controls.Add(lblDescriptionImpression, 0, 2)
        tlpDetails.Controls.Add(lblNomImpression, 0, 1)
        tlpDetails.Controls.Add(txtDescriptionImpression, 1, 2)
        tlpDetails.Controls.Add(txtNomImpression, 1, 1)
        tlpDetails.Controls.Add(lblEnvieCal, 0, 3)
        tlpDetails.Controls.Add(txtEnvieCal, 1, 3)
        tlpDetails.Controls.Add(lblNotes, 0, 4)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(6, 22)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(5)
        tlpDetails.RowCount = 7
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 8F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 13F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 8F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 45F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 6F))
        tlpDetails.Size = New Size(310, 445)
        tlpDetails.TabIndex = 0
        ' 
        ' tsNotes
        ' 
        tsNotes.Anchor = AnchorStyles.Bottom
        tsNotes.AutoSize = False
        tsNotes.Dock = DockStyle.None
        tsNotes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        tsNotes.Items.AddRange(New ToolStripItem() {btnBold, btnItalic, btnUnderline, btnBullet, btnTab})
        tsNotes.Location = New Point(89, 190)
        tsNotes.Name = "tsNotes"
        tsNotes.Size = New Size(211, 25)
        tsNotes.TabIndex = 30
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
        ' rtbNote
        ' 
        rtbNote.AcceptsTab = True
        tlpDetails.SetColumnSpan(rtbNote, 2)
        rtbNote.Dock = DockStyle.Fill
        rtbNote.Location = New Point(8, 218)
        rtbNote.Name = "rtbNote"
        rtbNote.Size = New Size(294, 189)
        rtbNote.TabIndex = 26
        rtbNote.Text = ""
        ' 
        ' txtCodeImpression
        ' 
        txtCodeImpression.Dock = DockStyle.Left
        txtCodeImpression.Location = New Point(88, 8)
        txtCodeImpression.Name = "txtCodeImpression"
        txtCodeImpression.ReadOnly = True
        txtCodeImpression.Size = New Size(120, 23)
        txtCodeImpression.TabIndex = 16
        txtCodeImpression.TabStop = False
        ' 
        ' lblCodeEImpression
        ' 
        lblCodeEImpression.AutoSize = True
        lblCodeEImpression.Location = New Point(8, 5)
        lblCodeEImpression.Name = "lblCodeEImpression"
        lblCodeEImpression.Size = New Size(35, 15)
        lblCodeEImpression.TabIndex = 15
        lblCodeEImpression.Text = "Code"
        ' 
        ' txtIdImpression
        ' 
        txtIdImpression.Location = New Point(88, 413)
        txtIdImpression.Name = "txtIdImpression"
        txtIdImpression.Size = New Size(100, 23)
        txtIdImpression.TabIndex = 17
        txtIdImpression.TabStop = False
        txtIdImpression.Visible = False
        ' 
        ' chkIsActif
        ' 
        chkIsActif.AutoSize = True
        chkIsActif.CheckAlign = ContentAlignment.TopLeft
        chkIsActif.Checked = True
        chkIsActif.CheckState = CheckState.Checked
        chkIsActif.Location = New Point(8, 413)
        chkIsActif.Name = "chkIsActif"
        chkIsActif.Size = New Size(60, 19)
        chkIsActif.TabIndex = 18
        chkIsActif.Text = "is actif"
        chkIsActif.UseVisualStyleBackColor = True
        ' 
        ' lblDescriptionImpression
        ' 
        lblDescriptionImpression.AutoSize = True
        lblDescriptionImpression.Location = New Point(8, 82)
        lblDescriptionImpression.Name = "lblDescriptionImpression"
        lblDescriptionImpression.Size = New Size(67, 15)
        lblDescriptionImpression.TabIndex = 19
        lblDescriptionImpression.Text = "Description"
        ' 
        ' lblNomImpression
        ' 
        lblNomImpression.AutoSize = True
        lblNomImpression.Location = New Point(8, 39)
        lblNomImpression.Name = "lblNomImpression"
        lblNomImpression.Size = New Size(34, 15)
        lblNomImpression.TabIndex = 21
        lblNomImpression.Text = "Nom"
        ' 
        ' txtDescriptionImpression
        ' 
        txtDescriptionImpression.Location = New Point(88, 85)
        txtDescriptionImpression.MaxLength = 400
        txtDescriptionImpression.Multiline = True
        txtDescriptionImpression.Name = "txtDescriptionImpression"
        txtDescriptionImpression.Size = New Size(208, 45)
        txtDescriptionImpression.TabIndex = 22
        ' 
        ' txtNomImpression
        ' 
        txtNomImpression.Location = New Point(88, 42)
        txtNomImpression.Name = "txtNomImpression"
        txtNomImpression.Size = New Size(208, 23)
        txtNomImpression.TabIndex = 23
        ' 
        ' lblEnvieCal
        ' 
        lblEnvieCal.AutoSize = True
        lblEnvieCal.Location = New Point(8, 138)
        lblEnvieCal.Name = "lblEnvieCal"
        lblEnvieCal.Size = New Size(55, 15)
        lblEnvieCal.TabIndex = 24
        lblEnvieCal.Text = "Envie Cal"
        ' 
        ' txtEnvieCal
        ' 
        txtEnvieCal.Location = New Point(88, 141)
        txtEnvieCal.MaxLength = 10
        txtEnvieCal.Name = "txtEnvieCal"
        txtEnvieCal.Size = New Size(100, 23)
        txtEnvieCal.TabIndex = 25
        ' 
        ' lblNotes
        ' 
        lblNotes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblNotes.AutoSize = True
        lblNotes.Location = New Point(8, 200)
        lblNotes.Name = "lblNotes"
        lblNotes.Size = New Size(38, 15)
        lblNotes.TabIndex = 27
        lblNotes.Text = "Notes"
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
        pnlActions.Location = New Point(8, 535)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(8)
        pnlActions.Size = New Size(848, 48)
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
        pnlTop.Controls.Add(chkSearchNotes)
        pnlTop.Controls.Add(lblCount)
        pnlTop.Controls.Add(btnClearSearch)
        pnlTop.Controls.Add(btnSearch)
        pnlTop.Controls.Add(txtSearch)
        pnlTop.Controls.Add(lblSearch)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(8, 8)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(848, 48)
        pnlTop.TabIndex = 4
        ' 
        ' chkSearchNotes
        ' 
        chkSearchNotes.AutoSize = True
        chkSearchNotes.Location = New Point(369, 12)
        chkSearchNotes.Name = "chkSearchNotes"
        chkSearchNotes.Size = New Size(111, 19)
        chkSearchNotes.TabIndex = 4
        chkSearchNotes.Text = "Inclure les notes"
        chkSearchNotes.UseVisualStyleBackColor = True
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(699, 23)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(87, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 impression(s)"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(582, 9)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 23)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(486, 9)
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
        ' GestionImpression
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(884, 649)
        Controls.Add(pnlForm)
        Controls.Add(stsStatus)
        Controls.Add(lblTitreForm)
        MinimumSize = New Size(900, 600)
        Name = "GestionImpression"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion des Impressions"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvImpression, ComponentModel.ISupportInitialize).EndInit()
        grpDetail.ResumeLayout(False)
        grpDetail.PerformLayout()
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
        tsNotes.ResumeLayout(False)
        tsNotes.PerformLayout()
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
    Friend WithEvents chkSearchNotes As CheckBox
    Friend WithEvents lblCount As Label
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
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents dgvImpression As DataGridView
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents grpDetail As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblCodeEImpression As Label
    Friend WithEvents txtCodeImpression As TextBox
    Friend WithEvents txtIdImpression As TextBox
    Friend WithEvents chkIsActif As CheckBox
    Friend WithEvents lblDescriptionImpression As Label
    Friend WithEvents lblNomImpression As Label
    Friend WithEvents txtDescriptionImpression As TextBox
    Friend WithEvents txtNomImpression As TextBox
    Friend WithEvents lblEnvieCal As Label
    Friend WithEvents txtEnvieCal As TextBox
    Friend WithEvents rtbNote As RichTextBox
    Friend WithEvents lblNotes As Label
    Friend WithEvents tsNotes As ToolStrip
    Friend WithEvents btnBold As ToolStripButton
    Friend WithEvents btnItalic As ToolStripButton
    Friend WithEvents btnUnderline As ToolStripButton
    Friend WithEvents btnBullet As ToolStripButton
    Friend WithEvents btnTab As ToolStripButton
End Class
