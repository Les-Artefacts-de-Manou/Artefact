<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_Editeurs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UC_Editeurs))
        pnlMain = New Panel()
        tlpCenter = New TableLayoutPanel()
        dgvEditeurs = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblCodeEditeur = New Label()
        txtCodeEditeur = New TextBox()
        lblNomEditeur = New Label()
        txtNomEditeur = New TextBox()
        lblPays = New Label()
        cboPays = New ComboBox()
        lblSiteWeb = New Label()
        txtSiteWeb = New TextBox()
        lblNotes = New Label()
        ucToolbar = New UC_RichTextToolbar()
        rtbNotes = New RichTextBox()
        txtIdEditeur = New TextBox()
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
        CType(dgvEditeurs, ComponentModel.ISupportInitialize).BeginInit()
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
        tlpCenter.Controls.Add(dgvEditeurs, 0, 0)
        tlpCenter.Controls.Add(grpDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 56)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpCenter.Size = New Size(884, 488)
        tlpCenter.TabIndex = 2
        ' 
        ' dgvEditeurs
        ' 
        dgvEditeurs.AllowUserToAddRows = False
        dgvEditeurs.AllowUserToDeleteRows = False
        dgvEditeurs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEditeurs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvEditeurs.Dock = DockStyle.Fill
        dgvEditeurs.Location = New Point(3, 3)
        dgvEditeurs.MultiSelect = False
        dgvEditeurs.Name = "dgvEditeurs"
        dgvEditeurs.ReadOnly = True
        dgvEditeurs.RowHeadersVisible = False
        dgvEditeurs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEditeurs.Size = New Size(494, 482)
        dgvEditeurs.TabIndex = 0
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
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80.0F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpDetails.Controls.Add(lblCodeEditeur, 0, 0)
        tlpDetails.Controls.Add(txtCodeEditeur, 1, 0)
        tlpDetails.Controls.Add(lblNomEditeur, 0, 1)
        tlpDetails.Controls.Add(txtNomEditeur, 1, 1)
        tlpDetails.Controls.Add(lblPays, 0, 2)
        tlpDetails.Controls.Add(cboPays, 1, 2)
        tlpDetails.Controls.Add(lblSiteWeb, 0, 3)
        tlpDetails.Controls.Add(txtSiteWeb, 1, 3)
        tlpDetails.Controls.Add(lblNotes, 0, 4)
        tlpDetails.Controls.Add(ucToolbar, 1, 4)
        tlpDetails.Controls.Add(rtbNotes, 0, 5)
        tlpDetails.Controls.Add(txtIdEditeur, 1, 6)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(3, 19)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(5)
        tlpDetails.RowCount = 7
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 50.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 50.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Absolute, 5.0F))
        tlpDetails.Size = New Size(372, 460)
        tlpDetails.TabIndex = 0
        ' 
        ' lblCodeEditeur
        ' 
        lblCodeEditeur.Anchor = AnchorStyles.Left
        lblCodeEditeur.AutoSize = True
        lblCodeEditeur.Location = New Point(8, 12)
        lblCodeEditeur.Name = "lblCodeEditeur"
        lblCodeEditeur.Size = New Size(35, 15)
        lblCodeEditeur.TabIndex = 0
        lblCodeEditeur.Text = "Code"
        ' 
        ' txtCodeEditeur
        ' 
        txtCodeEditeur.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtCodeEditeur.Location = New Point(88, 8)
        txtCodeEditeur.Name = "txtCodeEditeur"
        txtCodeEditeur.ReadOnly = True
        txtCodeEditeur.Size = New Size(276, 23)
        txtCodeEditeur.TabIndex = 1
        txtCodeEditeur.TabStop = False
        ' 
        ' lblNomEditeur
        ' 
        lblNomEditeur.Anchor = AnchorStyles.Left
        lblNomEditeur.AutoSize = True
        lblNomEditeur.Location = New Point(8, 52)
        lblNomEditeur.Name = "lblNomEditeur"
        lblNomEditeur.Size = New Size(34, 15)
        lblNomEditeur.TabIndex = 2
        lblNomEditeur.Text = "Nom"
        ' 
        ' txtNomEditeur
        ' 
        txtNomEditeur.Dock = DockStyle.Fill
        txtNomEditeur.Location = New Point(88, 38)
        txtNomEditeur.MaxLength = 200
        txtNomEditeur.Multiline = True
        txtNomEditeur.Name = "txtNomEditeur"
        txtNomEditeur.Size = New Size(276, 44)
        txtNomEditeur.TabIndex = 0
        ' 
        ' lblPays
        ' 
        lblPays.Anchor = AnchorStyles.Left
        lblPays.AutoSize = True
        lblPays.Location = New Point(8, 92)
        lblPays.Name = "lblPays"
        lblPays.Size = New Size(31, 15)
        lblPays.TabIndex = 4
        lblPays.Text = "Pays"
        ' 
        ' cboPays
        ' 
        cboPays.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboPays.DropDownStyle = ComboBoxStyle.DropDownList
        cboPays.FormattingEnabled = True
        cboPays.Location = New Point(88, 88)
        cboPays.Name = "cboPays"
        cboPays.Size = New Size(276, 23)
        cboPays.TabIndex = 1
        ' 
        ' lblSiteWeb
        ' 
        lblSiteWeb.Anchor = AnchorStyles.Left
        lblSiteWeb.AutoSize = True
        lblSiteWeb.Location = New Point(8, 137)
        lblSiteWeb.Name = "lblSiteWeb"
        lblSiteWeb.Size = New Size(53, 15)
        lblSiteWeb.TabIndex = 6
        lblSiteWeb.Text = "Site Web"
        ' 
        ' txtSiteWeb
        ' 
        txtSiteWeb.Dock = DockStyle.Fill
        txtSiteWeb.ForeColor = Color.CornflowerBlue
        txtSiteWeb.Location = New Point(88, 123)
        txtSiteWeb.Multiline = True
        txtSiteWeb.Name = "txtSiteWeb"
        txtSiteWeb.Size = New Size(276, 44)
        txtSiteWeb.TabIndex = 2
        ' 
        ' lblNotes
        ' 
        lblNotes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblNotes.AutoSize = True
        lblNotes.Location = New Point(8, 185)
        lblNotes.Name = "lblNotes"
        lblNotes.Size = New Size(38, 15)
        lblNotes.TabIndex = 8
        lblNotes.Text = "Notes"
        ' 
        ' ucToolbar
        ' 
        ucToolbar.Anchor = AnchorStyles.Left
        ucToolbar.Location = New Point(85, 173)
        ucToolbar.Name = "ucToolbar"
        ucToolbar.Size = New Size(280, 25)
        ucToolbar.TabIndex = 9
        ' 
        ' rtbNotes
        ' 
        rtbNotes.AcceptsTab = True
        tlpDetails.SetColumnSpan(rtbNotes, 2)
        rtbNotes.Dock = DockStyle.Fill
        rtbNotes.Location = New Point(8, 203)
        rtbNotes.Name = "rtbNotes"
        rtbNotes.Size = New Size(356, 244)
        rtbNotes.TabIndex = 3
        rtbNotes.Text = ""
        ' 
        ' txtIdEditeur
        ' 
        txtIdEditeur.Location = New Point(88, 453)
        txtIdEditeur.Name = "txtIdEditeur"
        txtIdEditeur.Size = New Size(74, 23)
        txtIdEditeur.TabIndex = 11
        txtIdEditeur.TabStop = False
        txtIdEditeur.Visible = False
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
        chkSearchNotes.TabIndex = 4
        chkSearchNotes.Text = "Inclure les notes"
        chkSearchNotes.UseVisualStyleBackColor = True
        ' 
        ' lblCount
        ' 
        lblCount.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCount.AutoSize = True
        lblCount.Location = New Point(783, 17)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(75, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 éditeur(s)"
        ' 
        ' UC_Editeurs
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlMain)
        Name = "UC_Editeurs"
        Size = New Size(900, 600)
        pnlMain.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvEditeurs, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents dgvEditeurs As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblCodeEditeur As Label
    Friend WithEvents txtCodeEditeur As TextBox
    Friend WithEvents lblNomEditeur As Label
    Friend WithEvents txtNomEditeur As TextBox
    Friend WithEvents lblPays As Label
    Friend WithEvents cboPays As ComboBox
    Friend WithEvents lblSiteWeb As Label
    Friend WithEvents txtSiteWeb As TextBox
    Friend WithEvents lblNotes As Label
    Friend WithEvents ucToolbar As UC_RichTextToolbar
    Friend WithEvents rtbNotes As RichTextBox
    Friend WithEvents txtIdEditeur As TextBox
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
