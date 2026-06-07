<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GestionConnexionMariaDb
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GestionConnexionMariaDb))
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        pnlConnexion = New Panel()
        pnlActions = New Panel()
        btnModifierMotDePasse = New Button()
        btnAnnuler = New Button()
        btnEnregistrer = New Button()
        btnTester = New Button()
        pnlFields = New Panel()
        btnShowPassword = New Button()
        lblUserName = New Label()
        lblDatabase = New Label()
        nudPort = New NumericUpDown()
        lblOptionConn = New Label()
        txtOptionsConn = New TextBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        txtUserName = New TextBox()
        lblPort = New Label()
        txtDatabase = New TextBox()
        lblHost = New Label()
        txtHost = New TextBox()
        lbllibelle = New Label()
        lblTitreForm = New Label()
        stsStatus.SuspendLayout()
        pnlConnexion.SuspendLayout()
        pnlActions.SuspendLayout()
        pnlFields.SuspendLayout()
        CType(nudPort, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(0, 360)
        stsStatus.Name = "stsStatus"
        stsStatus.Size = New Size(620, 22)
        stsStatus.TabIndex = 14
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
        ' pnlConnexion
        ' 
        pnlConnexion.BorderStyle = BorderStyle.Fixed3D
        pnlConnexion.Controls.Add(pnlActions)
        pnlConnexion.Controls.Add(pnlFields)
        pnlConnexion.Controls.Add(lbllibelle)
        pnlConnexion.Location = New Point(12, 51)
        pnlConnexion.Name = "pnlConnexion"
        pnlConnexion.Size = New Size(594, 374)
        pnlConnexion.TabIndex = 13
        ' 
        ' pnlActions
        ' 
        pnlActions.Controls.Add(btnModifierMotDePasse)
        pnlActions.Controls.Add(btnAnnuler)
        pnlActions.Controls.Add(btnEnregistrer)
        pnlActions.Controls.Add(btnTester)
        pnlActions.Location = New Point(12, 245)
        pnlActions.Name = "pnlActions"
        pnlActions.Size = New Size(580, 54)
        pnlActions.TabIndex = 2
        ' 
        ' btnModifierMotDePasse
        ' 
        btnModifierMotDePasse.Location = New Point(3, 13)
        btnModifierMotDePasse.Name = "btnModifierMotDePasse"
        btnModifierMotDePasse.Size = New Size(116, 23)
        btnModifierMotDePasse.TabIndex = 3
        btnModifierMotDePasse.Text = "Modifier Password"
        btnModifierMotDePasse.UseVisualStyleBackColor = True
        ' 
        ' btnAnnuler
        ' 
        btnAnnuler.Location = New Point(500, 13)
        btnAnnuler.Name = "btnAnnuler"
        btnAnnuler.Size = New Size(75, 23)
        btnAnnuler.TabIndex = 2
        btnAnnuler.Text = "Annuler"
        btnAnnuler.UseVisualStyleBackColor = True
        ' 
        ' btnEnregistrer
        ' 
        btnEnregistrer.Location = New Point(419, 13)
        btnEnregistrer.Name = "btnEnregistrer"
        btnEnregistrer.Size = New Size(75, 23)
        btnEnregistrer.TabIndex = 1
        btnEnregistrer.Text = "Enregistrer"
        btnEnregistrer.UseVisualStyleBackColor = True
        ' 
        ' btnTester
        ' 
        btnTester.Location = New Point(338, 13)
        btnTester.Name = "btnTester"
        btnTester.Size = New Size(75, 23)
        btnTester.TabIndex = 0
        btnTester.Text = "Tester"
        btnTester.UseVisualStyleBackColor = True
        ' 
        ' pnlFields
        ' 
        pnlFields.Controls.Add(btnShowPassword)
        pnlFields.Controls.Add(lblUserName)
        pnlFields.Controls.Add(lblDatabase)
        pnlFields.Controls.Add(nudPort)
        pnlFields.Controls.Add(lblOptionConn)
        pnlFields.Controls.Add(txtOptionsConn)
        pnlFields.Controls.Add(lblPassword)
        pnlFields.Controls.Add(txtPassword)
        pnlFields.Controls.Add(txtUserName)
        pnlFields.Controls.Add(lblPort)
        pnlFields.Controls.Add(txtDatabase)
        pnlFields.Controls.Add(lblHost)
        pnlFields.Controls.Add(txtHost)
        pnlFields.Location = New Point(11, 39)
        pnlFields.Name = "pnlFields"
        pnlFields.Size = New Size(581, 200)
        pnlFields.TabIndex = 1
        ' 
        ' btnShowPassword
        ' 
        btnShowPassword.Image = CType(resources.GetObject("btnShowPassword.Image"), Image)
        btnShowPassword.Location = New Point(195, 149)
        btnShowPassword.Margin = New Padding(1)
        btnShowPassword.Name = "btnShowPassword"
        btnShowPassword.Size = New Size(35, 39)
        btnShowPassword.TabIndex = 17
        btnShowPassword.TabStop = False
        btnShowPassword.UseVisualStyleBackColor = True
        ' 
        ' lblUserName
        ' 
        lblUserName.AutoSize = True
        lblUserName.Location = New Point(385, 71)
        lblUserName.Name = "lblUserName"
        lblUserName.Size = New Size(65, 15)
        lblUserName.TabIndex = 16
        lblUserName.Text = "User Name"
        ' 
        ' lblDatabase
        ' 
        lblDatabase.AutoSize = True
        lblDatabase.Location = New Point(23, 69)
        lblDatabase.Name = "lblDatabase"
        lblDatabase.Size = New Size(55, 15)
        lblDatabase.TabIndex = 15
        lblDatabase.Text = "Database"
        ' 
        ' nudPort
        ' 
        nudPort.Location = New Point(385, 38)
        nudPort.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        nudPort.Name = "nudPort"
        nudPort.Size = New Size(120, 23)
        nudPort.TabIndex = 14
        ' 
        ' lblOptionConn
        ' 
        lblOptionConn.AutoSize = True
        lblOptionConn.Location = New Point(388, 138)
        lblOptionConn.Name = "lblOptionConn"
        lblOptionConn.Size = New Size(73, 15)
        lblOptionConn.TabIndex = 11
        lblOptionConn.Text = "OptionConn"
        ' 
        ' txtOptionsConn
        ' 
        txtOptionsConn.Location = New Point(385, 158)
        txtOptionsConn.Name = "txtOptionsConn"
        txtOptionsConn.Size = New Size(169, 23)
        txtOptionsConn.TabIndex = 10
        ' 
        ' lblPassword
        ' 
        lblPassword.AutoSize = True
        lblPassword.Location = New Point(25, 138)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(57, 15)
        lblPassword.TabIndex = 9
        lblPassword.Text = "Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(22, 158)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(169, 23)
        txtPassword.TabIndex = 8
        ' 
        ' txtUserName
        ' 
        txtUserName.Location = New Point(382, 90)
        txtUserName.Name = "txtUserName"
        txtUserName.Size = New Size(169, 23)
        txtUserName.TabIndex = 6
        ' 
        ' lblPort
        ' 
        lblPort.AutoSize = True
        lblPort.Location = New Point(382, 13)
        lblPort.Name = "lblPort"
        lblPort.Size = New Size(29, 15)
        lblPort.TabIndex = 5
        lblPort.Text = "Port"
        ' 
        ' txtDatabase
        ' 
        txtDatabase.Location = New Point(22, 90)
        txtDatabase.Name = "txtDatabase"
        txtDatabase.Size = New Size(169, 23)
        txtDatabase.TabIndex = 2
        ' 
        ' lblHost
        ' 
        lblHost.AutoSize = True
        lblHost.Location = New Point(25, 13)
        lblHost.Name = "lblHost"
        lblHost.Size = New Size(32, 15)
        lblHost.TabIndex = 1
        lblHost.Text = "Host"
        ' 
        ' txtHost
        ' 
        txtHost.Location = New Point(22, 33)
        txtHost.Name = "txtHost"
        txtHost.Size = New Size(169, 23)
        txtHost.TabIndex = 0
        ' 
        ' lbllibelle
        ' 
        lbllibelle.AutoSize = True
        lbllibelle.Location = New Point(11, 6)
        lbllibelle.Name = "lbllibelle"
        lbllibelle.Size = New Size(64, 15)
        lbllibelle.TabIndex = 0
        lbllibelle.Text = "Connexion"
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.Font = New Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitreForm.ForeColor = Color.Tomato
        lblTitreForm.Location = New Point(9, 5)
        lblTitreForm.Margin = New Padding(0)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Size = New Size(286, 23)
        lblTitreForm.TabIndex = 12
        lblTitreForm.Text = "Gestion de la connexion à MariaDB"
        ' 
        ' GestionConnexionMariaDb
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(620, 382)
        Controls.Add(stsStatus)
        Controls.Add(pnlConnexion)
        Controls.Add(lblTitreForm)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "GestionConnexionMariaDb"
        Text = "Gestion Connexion MariaDb"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlConnexion.ResumeLayout(False)
        pnlConnexion.PerformLayout()
        pnlActions.ResumeLayout(False)
        pnlFields.ResumeLayout(False)
        pnlFields.PerformLayout()
        CType(nudPort, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents pnlConnexion As Panel
    Friend WithEvents lbllibelle As Label
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents pnlActions As Panel
    Friend WithEvents pnlFields As Panel
    Friend WithEvents btnAnnuler As Button
    Friend WithEvents btnEnregistrer As Button
    Friend WithEvents btnTester As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents lblOptionConn As Label
    Friend WithEvents txtOptionsConn As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents lblPort As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDatabase As TextBox
    Friend WithEvents lblHost As Label
    Friend WithEvents txtHost As TextBox
    Friend WithEvents nudPort As NumericUpDown
    Friend WithEvents lblUserName As Label
    Friend WithEvents lblDatabase As Label
    Friend WithEvents btnModifierMotDePasse As Button
    Friend WithEvents btnShowPassword As Button
End Class
