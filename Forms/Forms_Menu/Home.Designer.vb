<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Home
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Home))
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        pnlConnexion = New Panel()
        btnOpenGestionPrixLit = New Button()
        btnOpenGestionRecommandation = New Button()
        btnOpenGestionImpression = New Button()
        btnOpenGestionFormatFile = New Button()
        btnOpenGestionEditeurs = New Button()
        btnOpenGestionContacts = New Button()
        btnOpenGestionRefEnum = New Button()
        btnOpenGestionPays = New Button()
        btnOpenGestionLangues = New Button()
        lblGestionReferentiel = New Label()
        lblTitreForm = New Label()
        stsStatus.SuspendLayout()
        pnlConnexion.SuspendLayout()
        SuspendLayout()
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(0, 428)
        stsStatus.Name = "stsStatus"
        stsStatus.Size = New Size(800, 22)
        stsStatus.TabIndex = 7
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
        pnlConnexion.BackColor = Color.WhiteSmoke
        pnlConnexion.BorderStyle = BorderStyle.Fixed3D
        pnlConnexion.Controls.Add(btnOpenGestionPrixLit)
        pnlConnexion.Controls.Add(btnOpenGestionRecommandation)
        pnlConnexion.Controls.Add(btnOpenGestionImpression)
        pnlConnexion.Controls.Add(btnOpenGestionFormatFile)
        pnlConnexion.Controls.Add(btnOpenGestionEditeurs)
        pnlConnexion.Controls.Add(btnOpenGestionContacts)
        pnlConnexion.Controls.Add(btnOpenGestionRefEnum)
        pnlConnexion.Controls.Add(btnOpenGestionPays)
        pnlConnexion.Controls.Add(btnOpenGestionLangues)
        pnlConnexion.Controls.Add(lblGestionReferentiel)
        pnlConnexion.Location = New Point(12, 46)
        pnlConnexion.Name = "pnlConnexion"
        pnlConnexion.Size = New Size(175, 366)
        pnlConnexion.TabIndex = 9
        ' 
        ' btnOpenGestionPrixLit
        ' 
        btnOpenGestionPrixLit.Location = New Point(7, 326)
        btnOpenGestionPrixLit.Name = "btnOpenGestionPrixLit"
        btnOpenGestionPrixLit.Size = New Size(156, 23)
        btnOpenGestionPrixLit.TabIndex = 9
        btnOpenGestionPrixLit.Text = "Prix Littéraires"
        btnOpenGestionPrixLit.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionRecommandation
        ' 
        btnOpenGestionRecommandation.Location = New Point(7, 287)
        btnOpenGestionRecommandation.Name = "btnOpenGestionRecommandation"
        btnOpenGestionRecommandation.Size = New Size(156, 23)
        btnOpenGestionRecommandation.TabIndex = 8
        btnOpenGestionRecommandation.Text = "Recommandations"
        btnOpenGestionRecommandation.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionImpression
        ' 
        btnOpenGestionImpression.Location = New Point(7, 246)
        btnOpenGestionImpression.Name = "btnOpenGestionImpression"
        btnOpenGestionImpression.Size = New Size(156, 23)
        btnOpenGestionImpression.TabIndex = 7
        btnOpenGestionImpression.Text = "Impression"
        btnOpenGestionImpression.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionFormatFile
        ' 
        btnOpenGestionFormatFile.Location = New Point(7, 208)
        btnOpenGestionFormatFile.Name = "btnOpenGestionFormatFile"
        btnOpenGestionFormatFile.Size = New Size(156, 23)
        btnOpenGestionFormatFile.TabIndex = 6
        btnOpenGestionFormatFile.Text = "Format File"
        btnOpenGestionFormatFile.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionEditeurs
        ' 
        btnOpenGestionEditeurs.Location = New Point(7, 170)
        btnOpenGestionEditeurs.Name = "btnOpenGestionEditeurs"
        btnOpenGestionEditeurs.Size = New Size(156, 23)
        btnOpenGestionEditeurs.TabIndex = 5
        btnOpenGestionEditeurs.Text = "Editeurs"
        btnOpenGestionEditeurs.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionContacts
        ' 
        btnOpenGestionContacts.Location = New Point(9, 135)
        btnOpenGestionContacts.Name = "btnOpenGestionContacts"
        btnOpenGestionContacts.Size = New Size(156, 23)
        btnOpenGestionContacts.TabIndex = 4
        btnOpenGestionContacts.Text = "Contacts"
        btnOpenGestionContacts.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionRefEnum
        ' 
        btnOpenGestionRefEnum.Location = New Point(9, 100)
        btnOpenGestionRefEnum.Name = "btnOpenGestionRefEnum"
        btnOpenGestionRefEnum.Size = New Size(156, 23)
        btnOpenGestionRefEnum.TabIndex = 3
        btnOpenGestionRefEnum.Text = "Références Enumération"
        btnOpenGestionRefEnum.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionPays
        ' 
        btnOpenGestionPays.Location = New Point(9, 65)
        btnOpenGestionPays.Name = "btnOpenGestionPays"
        btnOpenGestionPays.Size = New Size(156, 23)
        btnOpenGestionPays.TabIndex = 2
        btnOpenGestionPays.Text = "Pays"
        btnOpenGestionPays.UseVisualStyleBackColor = True
        ' 
        ' btnOpenGestionLangues
        ' 
        btnOpenGestionLangues.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnOpenGestionLangues.Location = New Point(9, 30)
        btnOpenGestionLangues.Name = "btnOpenGestionLangues"
        btnOpenGestionLangues.Size = New Size(156, 23)
        btnOpenGestionLangues.TabIndex = 1
        btnOpenGestionLangues.Text = "Langues"
        btnOpenGestionLangues.UseVisualStyleBackColor = True
        ' 
        ' lblGestionReferentiel
        ' 
        lblGestionReferentiel.AutoSize = True
        lblGestionReferentiel.Dock = DockStyle.Top
        lblGestionReferentiel.FlatStyle = FlatStyle.Popup
        lblGestionReferentiel.Location = New Point(0, 0)
        lblGestionReferentiel.Name = "lblGestionReferentiel"
        lblGestionReferentiel.Size = New Size(111, 15)
        lblGestionReferentiel.TabIndex = 0
        lblGestionReferentiel.Text = "Gestion Référentiels"
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.Font = New Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitreForm.ForeColor = Color.Tomato
        lblTitreForm.Location = New Point(9, 9)
        lblTitreForm.Margin = New Padding(0)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Size = New Size(194, 23)
        lblTitreForm.TabIndex = 8
        lblTitreForm.Text = "Bienvenue sur Artefact"
        ' 
        ' Home
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(pnlConnexion)
        Controls.Add(lblTitreForm)
        Controls.Add(stsStatus)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Home"
        Text = "Home"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlConnexion.ResumeLayout(False)
        pnlConnexion.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents pnlConnexion As Panel
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents lblGestionReferentiel As Label
    Friend WithEvents btnOpenGestionLangues As Button
    Friend WithEvents btnOpenGestionPays As Button
    Friend WithEvents btnOpenGestionRefEnum As Button
    Friend WithEvents btnOpenGestionContacts As Button
    Friend WithEvents btnOpenGestionEditeurs As Button
    Friend WithEvents btnOpenGestionFormatFile As Button
    Friend WithEvents btnOpenGestionImpression As Button
    Friend WithEvents btnOpenGestionRecommandation As Button
    Friend WithEvents btnOpenGestionPrixLit As Button

End Class
