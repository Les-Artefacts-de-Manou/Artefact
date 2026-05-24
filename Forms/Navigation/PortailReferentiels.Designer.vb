<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PortailReferentiels
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PortailReferentiels))
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
        pnlTop = New Panel()
        lblContexte = New Label()
        lblTitreForm = New Label()
        pnlContent = New Panel()
        ttMain = New ToolTip(components)
        errProvider = New ErrorProvider(components)
        stsStatus.SuspendLayout()
        pnlConnexion.SuspendLayout()
        pnlTop.SuspendLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'stsStatus
        '
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(0, 539)
        stsStatus.Name = "stsStatus"
        stsStatus.Size = New Size(1080, 22)
        stsStatus.TabIndex = 0
        '
        'stsLabelStatus
        '
        stsLabelStatus.Font = New Font("Calibri", 9.75F, FontStyle.Bold)
        stsLabelStatus.ForeColor = Color.Tomato
        stsLabelStatus.Name = "stsLabelStatus"
        stsLabelStatus.Size = New Size(52, 17)
        stsLabelStatus.Text = "Artefact"
        '
        'pnlConnexion
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
        pnlConnexion.Dock = DockStyle.Left
        pnlConnexion.Location = New Point(0, 0)
        pnlConnexion.Name = "pnlConnexion"
        pnlConnexion.Size = New Size(190, 539)
        pnlConnexion.TabIndex = 1
        '
        'btnOpenGestionPrixLit
        '
        btnOpenGestionPrixLit.Location = New Point(12, 345)
        btnOpenGestionPrixLit.Name = "btnOpenGestionPrixLit"
        btnOpenGestionPrixLit.Size = New Size(160, 23)
        btnOpenGestionPrixLit.TabIndex = 9
        btnOpenGestionPrixLit.Text = "Prix Littéraires"
        btnOpenGestionPrixLit.UseVisualStyleBackColor = True
        '
        'btnOpenGestionRecommandation
        '
        btnOpenGestionRecommandation.Location = New Point(12, 307)
        btnOpenGestionRecommandation.Name = "btnOpenGestionRecommandation"
        btnOpenGestionRecommandation.Size = New Size(160, 23)
        btnOpenGestionRecommandation.TabIndex = 8
        btnOpenGestionRecommandation.Text = "Recommandations"
        btnOpenGestionRecommandation.UseVisualStyleBackColor = True
        '
        'btnOpenGestionImpression
        '
        btnOpenGestionImpression.Location = New Point(12, 269)
        btnOpenGestionImpression.Name = "btnOpenGestionImpression"
        btnOpenGestionImpression.Size = New Size(160, 23)
        btnOpenGestionImpression.TabIndex = 7
        btnOpenGestionImpression.Text = "Impression"
        btnOpenGestionImpression.UseVisualStyleBackColor = True
        '
        'btnOpenGestionFormatFile
        '
        btnOpenGestionFormatFile.Location = New Point(12, 231)
        btnOpenGestionFormatFile.Name = "btnOpenGestionFormatFile"
        btnOpenGestionFormatFile.Size = New Size(160, 23)
        btnOpenGestionFormatFile.TabIndex = 6
        btnOpenGestionFormatFile.Text = "Format File"
        btnOpenGestionFormatFile.UseVisualStyleBackColor = True
        '
        'btnOpenGestionEditeurs
        '
        btnOpenGestionEditeurs.Location = New Point(12, 193)
        btnOpenGestionEditeurs.Name = "btnOpenGestionEditeurs"
        btnOpenGestionEditeurs.Size = New Size(160, 23)
        btnOpenGestionEditeurs.TabIndex = 5
        btnOpenGestionEditeurs.Text = "Éditeurs"
        btnOpenGestionEditeurs.UseVisualStyleBackColor = True
        '
        'btnOpenGestionContacts
        '
        btnOpenGestionContacts.Location = New Point(12, 155)
        btnOpenGestionContacts.Name = "btnOpenGestionContacts"
        btnOpenGestionContacts.Size = New Size(160, 23)
        btnOpenGestionContacts.TabIndex = 4
        btnOpenGestionContacts.Text = "Contacts"
        btnOpenGestionContacts.UseVisualStyleBackColor = True
        '
        'btnOpenGestionRefEnum
        '
        btnOpenGestionRefEnum.Location = New Point(12, 117)
        btnOpenGestionRefEnum.Name = "btnOpenGestionRefEnum"
        btnOpenGestionRefEnum.Size = New Size(160, 23)
        btnOpenGestionRefEnum.TabIndex = 3
        btnOpenGestionRefEnum.Text = "Références Enumération"
        btnOpenGestionRefEnum.UseVisualStyleBackColor = True
        '
        'btnOpenGestionPays
        '
        btnOpenGestionPays.Location = New Point(12, 79)
        btnOpenGestionPays.Name = "btnOpenGestionPays"
        btnOpenGestionPays.Size = New Size(160, 23)
        btnOpenGestionPays.TabIndex = 2
        btnOpenGestionPays.Text = "Pays"
        btnOpenGestionPays.UseVisualStyleBackColor = True
        '
        'btnOpenGestionLangues
        '
        btnOpenGestionLangues.Location = New Point(12, 41)
        btnOpenGestionLangues.Name = "btnOpenGestionLangues"
        btnOpenGestionLangues.Size = New Size(160, 23)
        btnOpenGestionLangues.TabIndex = 1
        btnOpenGestionLangues.Text = "Langues"
        btnOpenGestionLangues.UseVisualStyleBackColor = True
        '
        'lblGestionReferentiel
        '
        lblGestionReferentiel.AutoSize = True
        lblGestionReferentiel.Dock = DockStyle.Top
        lblGestionReferentiel.Location = New Point(0, 0)
        lblGestionReferentiel.Name = "lblGestionReferentiel"
        lblGestionReferentiel.Size = New Size(111, 15)
        lblGestionReferentiel.TabIndex = 0
        lblGestionReferentiel.Text = "Gestion Référentiels"
        '
        'pnlTop
        '
        pnlTop.Controls.Add(lblContexte)
        pnlTop.Controls.Add(lblTitreForm)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(190, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(12, 8, 12, 8)
        pnlTop.Size = New Size(890, 73)
        pnlTop.TabIndex = 2
        '
        'lblContexte
        '
        lblContexte.Dock = DockStyle.Bottom
        lblContexte.Font = New Font("Calibri", 10.0F, FontStyle.Italic)
        lblContexte.ForeColor = Color.DimGray
        lblContexte.Location = New Point(12, 44)
        lblContexte.Name = "lblContexte"
        lblContexte.Size = New Size(866, 21)
        lblContexte.TabIndex = 1
        lblContexte.Text = "Contexte : accueil"
        '
        'lblTitreForm
        '
        lblTitreForm.AutoSize = True
        lblTitreForm.Font = New Font("Calibri", 16.0F, FontStyle.Bold)
        lblTitreForm.ForeColor = Color.Tomato
        lblTitreForm.Location = New Point(12, 8)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Size = New Size(304, 27)
        lblTitreForm.TabIndex = 0
        lblTitreForm.Text = "Portail central des référentiels"
        '
        'pnlContent
        '
        pnlContent.BorderStyle = BorderStyle.FixedSingle
        pnlContent.Dock = DockStyle.Fill
        pnlContent.Location = New Point(190, 73)
        pnlContent.Name = "pnlContent"
        pnlContent.Padding = New Padding(8)
        pnlContent.Size = New Size(890, 466)
        pnlContent.TabIndex = 3
        '
        'errProvider
        '
        errProvider.ContainerControl = Me
        '
        'PortailReferentiels
        '
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1080, 561)
        Controls.Add(pnlContent)
        Controls.Add(pnlTop)
        Controls.Add(pnlConnexion)
        Controls.Add(stsStatus)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "PortailReferentiels"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Portail Référentiels"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        pnlConnexion.ResumeLayout(False)
        pnlConnexion.PerformLayout()
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents pnlConnexion As Panel
    Friend WithEvents btnOpenGestionPrixLit As Button
    Friend WithEvents btnOpenGestionRecommandation As Button
    Friend WithEvents btnOpenGestionImpression As Button
    Friend WithEvents btnOpenGestionFormatFile As Button
    Friend WithEvents btnOpenGestionEditeurs As Button
    Friend WithEvents btnOpenGestionContacts As Button
    Friend WithEvents btnOpenGestionRefEnum As Button
    Friend WithEvents btnOpenGestionPays As Button
    Friend WithEvents btnOpenGestionLangues As Button
    Friend WithEvents lblGestionReferentiel As Label
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblContexte As Label
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents pnlContent As Panel
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents errProvider As ErrorProvider
End Class
