<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_Contacts
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
        dgvContacts = New DataGridView()
        grpDetails = New GroupBox()
        tlpDetails = New TableLayoutPanel()
        lblNomContact = New Label()
        lblEmailPerso = New Label()
        lblAdresseLiseuse = New Label()
        lblTypeLiseuse = New Label()
        lblCodeContact = New Label()
        txtNomContact = New TextBox()
        txtEmailPerso = New TextBox()
        txtAdresseLiseuse = New TextBox()
        txtTypeLiseuse = New TextBox()
        txtCodeContact = New TextBox()
        txtIdContact = New TextBox()
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
        CType(dgvContacts, ComponentModel.ISupportInitialize).BeginInit()
        grpDetails.SuspendLayout()
        tlpDetails.SuspendLayout()
        pnlActions.SuspendLayout()
        pnlTop.SuspendLayout()
        SuspendLayout()
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
        tlpCenter.Controls.Add(dgvContacts, 0, 0)
        tlpCenter.Controls.Add(grpDetails, 1, 0)
        tlpCenter.Dock = DockStyle.Fill
        tlpCenter.Location = New Point(8, 56)
        tlpCenter.Name = "tlpCenter"
        tlpCenter.RowCount = 1
        tlpCenter.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCenter.Size = New Size(980, 580)
        tlpCenter.TabIndex = 2
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
        dgvContacts.Size = New Size(514, 574)
        dgvContacts.TabIndex = 0
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
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 130F))
        tlpDetails.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpDetails.Controls.Add(lblNomContact, 0, 0)
        tlpDetails.Controls.Add(lblEmailPerso, 0, 1)
        tlpDetails.Controls.Add(lblAdresseLiseuse, 0, 2)
        tlpDetails.Controls.Add(lblTypeLiseuse, 0, 3)
        tlpDetails.Controls.Add(lblCodeContact, 0, 4)
        tlpDetails.Controls.Add(txtNomContact, 1, 0)
        tlpDetails.Controls.Add(txtEmailPerso, 1, 1)
        tlpDetails.Controls.Add(txtAdresseLiseuse, 1, 2)
        tlpDetails.Controls.Add(txtTypeLiseuse, 1, 3)
        tlpDetails.Controls.Add(txtCodeContact, 1, 4)
        tlpDetails.Controls.Add(txtIdContact, 1, 5)
        tlpDetails.Dock = DockStyle.Fill
        tlpDetails.Location = New Point(6, 22)
        tlpDetails.Name = "tlpDetails"
        tlpDetails.Padding = New Padding(8, 30, 4, 8)
        tlpDetails.RowCount = 6
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tlpDetails.RowStyles.Add(New RowStyle(SizeType.Percent, 10F))
        tlpDetails.Size = New Size(442, 546)
        tlpDetails.TabIndex = 0
        ' 
        ' lblNomContact
        ' 
        lblNomContact.Anchor = AnchorStyles.Left
        lblNomContact.AutoSize = True
        lblNomContact.Location = New Point(11, 75)
        lblNomContact.Name = "lblNomContact"
        lblNomContact.Size = New Size(85, 15)
        lblNomContact.TabIndex = 0
        lblNomContact.Text = "Nom contact :"
        ' 
        ' lblEmailPerso
        ' 
        lblEmailPerso.Anchor = AnchorStyles.Left
        lblEmailPerso.AutoSize = True
        lblEmailPerso.Location = New Point(11, 177)
        lblEmailPerso.Name = "lblEmailPerso"
        lblEmailPerso.Size = New Size(79, 15)
        lblEmailPerso.TabIndex = 1
        lblEmailPerso.Text = "Email perso :"
        ' 
        ' lblAdresseLiseuse
        ' 
        lblAdresseLiseuse.Anchor = AnchorStyles.Left
        lblAdresseLiseuse.AutoSize = True
        lblAdresseLiseuse.Location = New Point(11, 279)
        lblAdresseLiseuse.Name = "lblAdresseLiseuse"
        lblAdresseLiseuse.Size = New Size(101, 15)
        lblAdresseLiseuse.TabIndex = 2
        lblAdresseLiseuse.Text = "Adresse liseuse :"
        ' 
        ' lblTypeLiseuse
        ' 
        lblTypeLiseuse.Anchor = AnchorStyles.Left
        lblTypeLiseuse.AutoSize = True
        lblTypeLiseuse.Location = New Point(11, 381)
        lblTypeLiseuse.Name = "lblTypeLiseuse"
        lblTypeLiseuse.Size = New Size(84, 15)
        lblTypeLiseuse.TabIndex = 3
        lblTypeLiseuse.Text = "Type liseuse :"
        ' 
        ' lblCodeContact
        ' 
        lblCodeContact.Anchor = AnchorStyles.Left
        lblCodeContact.AutoSize = True
        lblCodeContact.Location = New Point(11, 443)
        lblCodeContact.Name = "lblCodeContact"
        lblCodeContact.Size = New Size(85, 15)
        lblCodeContact.TabIndex = 10
        lblCodeContact.Text = "Code (auto) :"
        ' 
        ' txtNomContact
        ' 
        txtNomContact.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtNomContact.Location = New Point(141, 71)
        txtNomContact.MaxLength = 150
        txtNomContact.Name = "txtNomContact"
        txtNomContact.Size = New Size(294, 23)
        txtNomContact.TabIndex = 4
        ' 
        ' txtEmailPerso
        ' 
        txtEmailPerso.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtEmailPerso.Location = New Point(141, 173)
        txtEmailPerso.MaxLength = 255
        txtEmailPerso.Name = "txtEmailPerso"
        txtEmailPerso.Size = New Size(294, 23)
        txtEmailPerso.TabIndex = 5
        ' 
        ' txtAdresseLiseuse
        ' 
        txtAdresseLiseuse.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtAdresseLiseuse.Location = New Point(141, 275)
        txtAdresseLiseuse.MaxLength = 255
        txtAdresseLiseuse.Name = "txtAdresseLiseuse"
        txtAdresseLiseuse.Size = New Size(294, 23)
        txtAdresseLiseuse.TabIndex = 6
        ' 
        ' txtTypeLiseuse
        ' 
        txtTypeLiseuse.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtTypeLiseuse.Location = New Point(141, 377)
        txtTypeLiseuse.MaxLength = 100
        txtTypeLiseuse.Name = "txtTypeLiseuse"
        txtTypeLiseuse.Size = New Size(294, 23)
        txtTypeLiseuse.TabIndex = 7
        ' 
        ' txtCodeContact
        ' 
        txtCodeContact.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtCodeContact.BackColor = Color.LightGray
        txtCodeContact.Location = New Point(141, 439)
        txtCodeContact.Name = "txtCodeContact"
        txtCodeContact.ReadOnly = True
        txtCodeContact.Size = New Size(294, 23)
        txtCodeContact.TabIndex = 8
        txtCodeContact.TabStop = False
        ' 
        ' txtIdContact
        ' 
        txtIdContact.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtIdContact.Location = New Point(141, 490)
        txtIdContact.Name = "txtIdContact"
        txtIdContact.ReadOnly = True
        txtIdContact.Size = New Size(294, 23)
        txtIdContact.TabIndex = 9
        txtIdContact.TabStop = False
        txtIdContact.Visible = False
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
        lblCount.Size = New Size(63, 15)
        lblCount.TabIndex = 4
        lblCount.Text = "0 contact(s)"
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
        'UC_Contacts
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlMain)
        Font = New Font("Segoe UI", 9F)
        Name = "UC_Contacts"
        Size = New Size(1000, 700)
        pnlMain.ResumeLayout(False)
        tlpCenter.ResumeLayout(False)
        CType(dgvContacts, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents dgvContacts As DataGridView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents tlpDetails As TableLayoutPanel
    Friend WithEvents lblNomContact As Label
    Friend WithEvents lblEmailPerso As Label
    Friend WithEvents lblAdresseLiseuse As Label
    Friend WithEvents lblTypeLiseuse As Label
    Friend WithEvents lblCodeContact As Label
    Friend WithEvents txtNomContact As TextBox
    Friend WithEvents txtEmailPerso As TextBox
    Friend WithEvents txtAdresseLiseuse As TextBox
    Friend WithEvents txtTypeLiseuse As TextBox
    Friend WithEvents txtCodeContact As TextBox
    Friend WithEvents txtIdContact As TextBox
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
