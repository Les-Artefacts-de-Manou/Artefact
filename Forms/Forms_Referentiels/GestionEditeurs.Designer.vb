<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionEditeurs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GestionEditeurs))
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        pnlForm = New Panel()
        tlpCenter = New TableLayoutPanel()
        dgvEditeurs = New DataGridView()
        grpDetail = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblNotes = New Label()
        rtbNotesEditeur = New RichTextBox()
        txtCodeEditeur = New TextBox()
        lblCodeEditeur = New Label()
        lblNomEditeur = New Label()
        txtNomEditeur = New TextBox()
        lblPays = New Label()
        cboPays = New ComboBox()
        lblSiteWeb = New Label()
        txtSiteWeb = New TextBox()
        txtIdEditeur = New TextBox()
        tsNotes = New ToolStrip()
        btnBold = New ToolStripButton()
        btnItalic = New ToolStripButton()
        btnUnderline = New ToolStripButton()
        btnBullet = New ToolStripButton()
        btnTab = New ToolStripButton()
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
        lblTitreForm = New Label()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        stsStatus.SuspendLayout()
        pnlForm.SuspendLayout()
        tlpCenter.SuspendLayout()
        CType(dgvEditeurs, ComponentModel.ISupportInitialize).BeginInit()
        grpDetail.SuspendLayout()
        tlpDetails.SuspendLayout()
        tsNotes.SuspendLayout()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(8, 611)
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
        pnlForm.AutoSize = True
        pnlForm.BackColor = Color.OldLace
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tlpCenter)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(8, 31)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(868, 580)
        pnlForm.TabIndex = 17
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenter.Controls.Add(dgvEditeurs, 0, 0)
        tlpCenter.Controls.Add(grpDetail, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 56)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(848, 464)
        tlpCenter.TabIndex = 5
        ' 
        ' dgvEditeurs
        ' 
        dgvEditeurs.AllowUserToAddRows = False
        dgvEditeurs.AllowUserToDeleteRows = False
        dgvEditeurs.AllowUserToResizeColumns = False
        dgvEditeurs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEditeurs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvEditeurs.Dock = DockStyle.Fill
        dgvEditeurs.Location = New Point(3, 3)
        dgvEditeurs.MultiSelect = False
        dgvEditeurs.Name = "dgvEditeurs"
        dgvEditeurs.ReadOnly = True
        dgvEditeurs.RowHeadersVisible = False
        dgvEditeurs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEditeurs.Size = New Size(514, 458)
        dgvEditeurs.TabIndex = 2
        ' 
        ' grpDetail
        ' 
        grpDetail.Controls.Add(tlpDetails)
        grpDetail.Dock = DockStyle.Fill
        grpDetail.Location = New Point(526, 6)
        grpDetail.Margin = New Padding(6)
        grpDetail.Name = "grpDetail"
        grpDetail.Size = New Size(316, 452)
        grpDetail.TabIndex = 3
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
        tlpDetails.Controls.Add(lblNotes, 0, 4)
        tlpDetails.Controls.Add(rtbNotesEditeur, 0, 5)
        tlpDetails.Controls.Add(txtCodeEditeur, 1, 0)
        tlpDetails.Controls.Add(lblCodeEditeur, 0, 0)
        tlpDetails.Controls.Add(lblNomEditeur, 0, 1)
        tlpDetails.Controls.Add(txtNomEditeur, 1, 1)
        tlpDetails.Controls.Add(lblPays, 0, 2)
        tlpDetails.Controls.Add(cboPays, 1, 2)
        tlpDetails.Controls.Add(lblSiteWeb, 0, 3)
        tlpDetails.Controls.Add(txtSiteWeb, 1, 3)
        tlpDetails.Controls.Add(txtIdEditeur, 1, 6)
        tlpDetails.Controls.Add(tsNotes, 1, 4)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(3, 19)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(5)
        tlpDetails.RowCount = 7
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 15F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 12F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 9F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 37F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 7F))
        tlpDetails.Size = New Size(310, 430)
        tlpDetails.TabIndex = 0
        ' 
        ' lblNotes
        ' 
        lblNotes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblNotes.AutoSize = True
        lblNotes.Location = New Point(8, 224)
        lblNotes.Name = "lblNotes"
        lblNotes.Size = New Size(38, 15)
        lblNotes.TabIndex = 28
        lblNotes.Text = "Notes"
        ' 
        ' rtbNotesEditeur
        ' 
        rtbNotesEditeur.AcceptsTab = True
        tlpDetails.SetColumnSpan(rtbNotesEditeur, 2)
        rtbNotesEditeur.Dock = DockStyle.Fill
        rtbNotesEditeur.Location = New Point(8, 242)
        rtbNotesEditeur.Name = "rtbNotesEditeur"
        rtbNotesEditeur.Size = New Size(294, 149)
        rtbNotesEditeur.TabIndex = 24
        rtbNotesEditeur.Text = ""
        ' 
        ' txtCodeEditeur
        ' 
        txtCodeEditeur.Dock = DockStyle.Left
        txtCodeEditeur.Location = New Point(88, 8)
        txtCodeEditeur.Name = "txtCodeEditeur"
        txtCodeEditeur.ReadOnly = True
        txtCodeEditeur.Size = New Size(120, 23)
        txtCodeEditeur.TabIndex = 15
        txtCodeEditeur.TabStop = False
        ' 
        ' lblCodeEditeur
        ' 
        lblCodeEditeur.AutoSize = True
        lblCodeEditeur.Location = New Point(8, 5)
        lblCodeEditeur.Name = "lblCodeEditeur"
        lblCodeEditeur.Size = New Size(44, 30)
        lblCodeEditeur.TabIndex = 14
        lblCodeEditeur.Text = "Code Editeur"
        ' 
        ' lblNomEditeur
        ' 
        lblNomEditeur.AutoSize = True
        lblNomEditeur.Location = New Point(8, 47)
        lblNomEditeur.Name = "lblNomEditeur"
        lblNomEditeur.Size = New Size(74, 15)
        lblNomEditeur.TabIndex = 16
        lblNomEditeur.Text = "Nom Editeur"
        ' 
        ' txtNomEditeur
        ' 
        txtNomEditeur.Location = New Point(88, 50)
        txtNomEditeur.MaxLength = 200
        txtNomEditeur.Multiline = True
        txtNomEditeur.Name = "txtNomEditeur"
        txtNomEditeur.Size = New Size(196, 41)
        txtNomEditeur.TabIndex = 17
        ' 
        ' lblPays
        ' 
        lblPays.AutoSize = True
        lblPays.Location = New Point(8, 110)
        lblPays.Name = "lblPays"
        lblPays.Size = New Size(31, 15)
        lblPays.TabIndex = 18
        lblPays.Text = "Pays"
        ' 
        ' cboPays
        ' 
        cboPays.FormattingEnabled = True
        cboPays.Location = New Point(88, 113)
        cboPays.Name = "cboPays"
        cboPays.Size = New Size(192, 23)
        cboPays.TabIndex = 19
        ' 
        ' lblSiteWeb
        ' 
        lblSiteWeb.AutoSize = True
        lblSiteWeb.Location = New Point(8, 152)
        lblSiteWeb.Name = "lblSiteWeb"
        lblSiteWeb.Size = New Size(53, 15)
        lblSiteWeb.TabIndex = 20
        lblSiteWeb.Text = "Site Web"
        ' 
        ' txtSiteWeb
        ' 
        txtSiteWeb.Dock = DockStyle.Fill
        txtSiteWeb.ForeColor = Color.CornflowerBlue
        txtSiteWeb.Location = New Point(88, 155)
        txtSiteWeb.Multiline = True
        txtSiteWeb.Name = "txtSiteWeb"
        txtSiteWeb.Size = New Size(214, 44)
        txtSiteWeb.TabIndex = 21
        ' 
        ' txtIdEditeur
        ' 
        txtIdEditeur.Location = New Point(88, 397)
        txtIdEditeur.Name = "txtIdEditeur"
        txtIdEditeur.Size = New Size(74, 23)
        txtIdEditeur.TabIndex = 13
        txtIdEditeur.TabStop = False
        txtIdEditeur.Visible = False
        ' 
        ' tsNotes
        ' 
        tsNotes.Anchor = AnchorStyles.Bottom
        tsNotes.AutoSize = False
        tsNotes.Dock = DockStyle.None
        tsNotes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        tsNotes.Items.AddRange(New ToolStripItem() {btnBold, btnItalic, btnUnderline, btnBullet, btnTab})
        tsNotes.Location = New Point(85, 214)
        tsNotes.Name = "tsNotes"
        tsNotes.Size = New Size(220, 25)
        tsNotes.TabIndex = 29
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
        ' pnlActions
        ' 
        pnlActions.Controls.Add(btnEdit)
        pnlActions.Controls.Add(btnClose)
        pnlActions.Controls.Add(btnDelete)
        pnlActions.Controls.Add(btnCancel)
        pnlActions.Controls.Add(btnSave)
        pnlActions.Controls.Add(btnNew)
        pnlActions.Dock = DockStyle.Bottom
        pnlActions.Location = New Point(8, 520)
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
        pnlTop.TabIndex = 3
        ' 
        ' chkSearchNotes
        ' 
        chkSearchNotes.AutoSize = True
        chkSearchNotes.Location = New Point(355, 12)
        chkSearchNotes.Name = "chkSearchNotes"
        chkSearchNotes.Size = New Size(111, 19)
        chkSearchNotes.TabIndex = 4
        chkSearchNotes.Text = "Inclure les notes"
        chkSearchNotes.UseVisualStyleBackColor = True
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(691, 15)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(66, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 editeur(s)"
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
        lblSearch.Location = New Point(11, 15)
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
        lblTitreForm.Location = New Point(8, 4)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Padding = New Padding(0, 0, 0, 4)
        lblTitreForm.Size = New Size(173, 27)
        lblTitreForm.TabIndex = 2
        lblTitreForm.Text = "Gestion des Editeurs"
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' GestionEditeurs
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(884, 634)
        Controls.Add(pnlForm)
        Controls.Add(stsStatus)
        Controls.Add(lblTitreForm)
        MinimumSize = New Size(900, 600)
        Name = "GestionEditeurs"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion des Editeurs"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvEditeurs, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents pnlForm As Panel
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents pnlTop As Panel
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
    Friend WithEvents dgvEditeurs As DataGridView
    Friend WithEvents grpDetail As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents txtIdEditeur As TextBox
    Friend WithEvents lblCodeEditeur As Label
    Friend WithEvents txtCodeEditeur As TextBox
    Friend WithEvents lblNomEditeur As Label
    Friend WithEvents txtNomEditeur As TextBox
    Friend WithEvents lblPays As Label
    Friend WithEvents cboPays As ComboBox
    Friend WithEvents lblSiteWeb As Label
    Friend WithEvents txtSiteWeb As TextBox
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents chkSearchNotes As CheckBox
    Friend WithEvents rtbNotesEditeur As RichTextBox
    Friend WithEvents lblNotes As Label
    Friend WithEvents tsNotes As ToolStrip
    Friend WithEvents btnBold As ToolStripButton
    Friend WithEvents btnItalic As ToolStripButton
    Friend WithEvents btnUnderline As ToolStripButton
    Friend WithEvents btnBullet As ToolStripButton
    Friend WithEvents btnTab As ToolStripButton
End Class
