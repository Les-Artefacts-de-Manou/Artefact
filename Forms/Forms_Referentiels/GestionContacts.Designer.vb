<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionContacts
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
        dgvContacts = New DataGridView()
        grpDetail = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        txtCodeContact = New TextBox()
        lblCodeContact = New Label()
        lblNomContact = New Label()
        txtNomContact = New TextBox()
        lblEmailPerso = New Label()
        txtEmailPerso = New TextBox()
        lblAdresseLiseuse = New Label()
        txtAdresseLiseuse = New TextBox()
        lblTypeLiseuse = New Label()
        txtTypeLiseuse = New TextBox()
        txtIdContact = New TextBox()
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
        CType(dgvContacts, ComponentModel.ISupportInitialize).BeginInit()
        grpDetail.SuspendLayout()
        tlpDetails.SuspendLayout()
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
        lblTitreForm.Size = New Size(176, 27)
        lblTitreForm.TabIndex = 1
        lblTitreForm.Text = "Gestion des Contacts"
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
        pnlForm.BackColor = Color.FloralWhite
        pnlForm.BorderStyle = BorderStyle.Fixed3D
        pnlForm.Controls.Add(tlpCenter)
        pnlForm.Controls.Add(pnlActions)
        pnlForm.Controls.Add(pnlTop)
        pnlForm.Dock = DockStyle.Fill
        pnlForm.Location = New Point(8, 31)
        pnlForm.Name = "pnlForm"
        pnlForm.Padding = New Padding(8)
        pnlForm.Size = New Size(868, 507)
        pnlForm.TabIndex = 16
        ' 
        ' tlpCenter
        ' 
        tlpCenter.ColumnCount = 2
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 520F))
        tlpCenter.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCenter.Controls.Add(dgvContacts, 0, 0)
        tlpCenter.Controls.Add(grpDetail, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 56)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(848, 391)
        tlpCenter.TabIndex = 4
        ' 
        ' dgvContacts
        ' 
        dgvContacts.AllowUserToAddRows = False
        dgvContacts.AllowUserToDeleteRows = False
        dgvContacts.AllowUserToResizeColumns = False
        dgvContacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvContacts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvContacts.Dock = DockStyle.Fill
        dgvContacts.Location = New Point(3, 3)
        dgvContacts.MultiSelect = False
        dgvContacts.Name = "dgvContacts"
        dgvContacts.ReadOnly = True
        dgvContacts.RowHeadersVisible = False
        dgvContacts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvContacts.Size = New Size(514, 385)
        dgvContacts.TabIndex = 1
        ' 
        ' grpDetail
        ' 
        grpDetail.Controls.Add(tlpDetails)
        grpDetail.Dock = DockStyle.Fill
        grpDetail.Location = New Point(523, 3)
        grpDetail.Name = "grpDetail"
        grpDetail.Padding = New Padding(6)
        grpDetail.Size = New Size(322, 385)
        grpDetail.TabIndex = 0
        grpDetail.TabStop = False
        grpDetail.Text = "Détails"
        ' 
        ' tlpDetails
        ' 
        tlpDetails.ColumnCount = 2
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 70F))
        tlpDetails.Controls.Add(txtCodeContact, 1, 4)
        tlpDetails.Controls.Add(lblCodeContact, 0, 4)
        tlpDetails.Controls.Add(lblNomContact, 0, 0)
        tlpDetails.Controls.Add(txtNomContact, 1, 0)
        tlpDetails.Controls.Add(lblEmailPerso, 0, 1)
        tlpDetails.Controls.Add(txtEmailPerso, 1, 1)
        tlpDetails.Controls.Add(lblAdresseLiseuse, 0, 2)
        tlpDetails.Controls.Add(txtAdresseLiseuse, 1, 2)
        tlpDetails.Controls.Add(lblTypeLiseuse, 0, 3)
        tlpDetails.Controls.Add(txtTypeLiseuse, 1, 3)
        tlpDetails.Controls.Add(txtIdContact, 1, 5)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(6, 22)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.RowCount = 6
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.6666679F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.6666679F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.6666679F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.6666679F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.6666679F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 16.6666679F))
        tlpDetails.Size = New Size(310, 357)
        tlpDetails.TabIndex = 0
        ' 
        ' txtCodeContact
        ' 
        txtCodeContact.Dock = DockStyle.Left
        txtCodeContact.Location = New Point(96, 239)
        txtCodeContact.Name = "txtCodeContact"
        txtCodeContact.ReadOnly = True
        txtCodeContact.Size = New Size(120, 23)
        txtCodeContact.TabIndex = 14
        txtCodeContact.TabStop = False
        ' 
        ' lblCodeContact
        ' 
        lblCodeContact.AutoSize = True
        lblCodeContact.Location = New Point(3, 236)
        lblCodeContact.Name = "lblCodeContact"
        lblCodeContact.Size = New Size(35, 15)
        lblCodeContact.TabIndex = 13
        lblCodeContact.Text = "Code"
        ' 
        ' lblNomContact
        ' 
        lblNomContact.AutoSize = True
        lblNomContact.Location = New Point(3, 0)
        lblNomContact.Name = "lblNomContact"
        lblNomContact.Size = New Size(34, 15)
        lblNomContact.TabIndex = 0
        lblNomContact.Text = "Nom"
        ' 
        ' txtNomContact
        ' 
        txtNomContact.ForeColor = SystemColors.WindowText
        txtNomContact.Location = New Point(96, 3)
        txtNomContact.MaxLength = 200
        txtNomContact.Multiline = True
        txtNomContact.Name = "txtNomContact"
        txtNomContact.Size = New Size(211, 23)
        txtNomContact.TabIndex = 1
        ' 
        ' lblEmailPerso
        ' 
        lblEmailPerso.AutoSize = True
        lblEmailPerso.Location = New Point(3, 59)
        lblEmailPerso.Name = "lblEmailPerso"
        lblEmailPerso.Size = New Size(68, 15)
        lblEmailPerso.TabIndex = 2
        lblEmailPerso.Text = "Email Perso"
        ' 
        ' txtEmailPerso
        ' 
        txtEmailPerso.ForeColor = Color.CornflowerBlue
        txtEmailPerso.Location = New Point(96, 62)
        txtEmailPerso.MaxLength = 254
        txtEmailPerso.Multiline = True
        txtEmailPerso.Name = "txtEmailPerso"
        txtEmailPerso.Size = New Size(211, 23)
        txtEmailPerso.TabIndex = 3
        ' 
        ' lblAdresseLiseuse
        ' 
        lblAdresseLiseuse.AutoSize = True
        lblAdresseLiseuse.Location = New Point(3, 118)
        lblAdresseLiseuse.Name = "lblAdresseLiseuse"
        lblAdresseLiseuse.Size = New Size(51, 30)
        lblAdresseLiseuse.TabIndex = 4
        lblAdresseLiseuse.Text = "Adresse Liseuse"
        ' 
        ' txtAdresseLiseuse
        ' 
        txtAdresseLiseuse.ForeColor = Color.CornflowerBlue
        txtAdresseLiseuse.Location = New Point(96, 121)
        txtAdresseLiseuse.MaxLength = 254
        txtAdresseLiseuse.Multiline = True
        txtAdresseLiseuse.Name = "txtAdresseLiseuse"
        txtAdresseLiseuse.Size = New Size(211, 23)
        txtAdresseLiseuse.TabIndex = 5
        ' 
        ' lblTypeLiseuse
        ' 
        lblTypeLiseuse.AutoSize = True
        lblTypeLiseuse.Location = New Point(3, 177)
        lblTypeLiseuse.Name = "lblTypeLiseuse"
        lblTypeLiseuse.Size = New Size(73, 15)
        lblTypeLiseuse.TabIndex = 6
        lblTypeLiseuse.Text = "Type Liseuse"
        ' 
        ' txtTypeLiseuse
        ' 
        txtTypeLiseuse.ForeColor = SystemColors.WindowText
        txtTypeLiseuse.Location = New Point(96, 180)
        txtTypeLiseuse.MaxLength = 100
        txtTypeLiseuse.Multiline = True
        txtTypeLiseuse.Name = "txtTypeLiseuse"
        txtTypeLiseuse.Size = New Size(211, 23)
        txtTypeLiseuse.TabIndex = 7
        ' 
        ' txtIdContact
        ' 
        txtIdContact.Location = New Point(96, 298)
        txtIdContact.Name = "txtIdContact"
        txtIdContact.Size = New Size(100, 23)
        txtIdContact.TabIndex = 12
        txtIdContact.TabStop = False
        txtIdContact.Visible = False
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
        pnlActions.Location = New Point(8, 447)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(8)
        pnlActions.Size = New Size(848, 48)
        pnlActions.TabIndex = 3
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
        pnlTop.Size = New Size(848, 48)
        pnlTop.TabIndex = 2
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(700, 18)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(69, 15)
        lblCount.TabIndex = 3
        lblCount.Text = "0 contact(s)"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Location = New Point(461, 11)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.Size = New Size(32, 23)
        btnClearSearch.TabIndex = 2
        btnClearSearch.Text = "X"
        btnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(365, 11)
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
        lblSearch.Location = New Point(11, 19)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(66, 15)
        lblSearch.TabIndex = 0
        lblSearch.Text = "Rechercher"
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' GestionContacts
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(884, 561)
        Controls.Add(pnlForm)
        Controls.Add(stsStatus)
        Controls.Add(lblTitreForm)
        MinimumSize = New Size(900, 600)
        Name = "GestionContacts"
        Padding = New Padding(8, 4, 8, 1)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestion des Contacts"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlForm.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvContacts, ComponentModel.ISupportInitialize).EndInit()
        grpDetail.ResumeLayout(False)
        tlpDetails.ResumeLayout(False)
        tlpDetails.PerformLayout()
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
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents tlpCenter As TableLayoutPanel
    Friend WithEvents grpDetail As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents dgvContacts As DataGridView
    Friend WithEvents lblNomContact As Label
    Friend WithEvents txtNomContact As TextBox
    Friend WithEvents lblEmailPerso As Label
    Friend WithEvents txtEmailPerso As TextBox
    Friend WithEvents lblAdresseLiseuse As Label
    Friend WithEvents txtAdresseLiseuse As TextBox
    Friend WithEvents lblTypeLiseuse As Label
    Friend WithEvents txtTypeLiseuse As TextBox
    Friend WithEvents txtIdContact As TextBox
    Friend WithEvents lblCodeContact As Label
    Friend WithEvents txtCodeContact As TextBox
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
End Class
