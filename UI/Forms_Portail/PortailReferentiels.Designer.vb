<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PortailReferentiels
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PortailReferentiels))
        pnlTop = New Panel()
        lblTitre = New Label()
        lblContexte = New Label()
        btnClose = New Button()
        pnlNavigation = New Panel()
        btnAccueil = New Button()
        btnLangues = New Button()
        btnPays = New Button()
        btnContacts = New Button()
        btnEditeurs = New Button()
        btnFormatFile = New Button()
        btnImpression = New Button()
        btnPrixLit = New Button()
        btnRecommandations = New Button()
        btnRefEnum = New Button()
        pnlMain = New Panel()
        stsStatus = New StatusStrip()
        stsLabelStatus = New ToolStripStatusLabel()
        errProvider = New ErrorProvider(components)
        ttMain = New ToolTip(components)
        pnlTop.SuspendLayout()
        pnlNavigation.SuspendLayout()
        stsStatus.SuspendLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlTop
        ' 
        pnlTop.BackColor = SystemColors.ControlLight
        pnlTop.Controls.Add(lblTitre)
        pnlTop.Controls.Add(lblContexte)
        pnlTop.Controls.Add(btnClose)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(0, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Size = New Size(1400, 60)
        pnlTop.TabIndex = 0
        ' 
        ' lblTitre
        ' 
        lblTitre.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblTitre.Location = New Point(39, 15)
        lblTitre.Name = "lblTitre"
        lblTitre.Size = New Size(239, 30)
        lblTitre.TabIndex = 2
        lblTitre.Text = "Portail des Référentiels"
        lblTitre.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblContexte
        ' 
        lblContexte.Font = New Font("Segoe UI", 11F)
        lblContexte.Location = New Point(397, 15)
        lblContexte.Name = "lblContexte"
        lblContexte.Size = New Size(718, 30)
        lblContexte.TabIndex = 0
        lblContexte.Text = "Portail Référentiels > Accueil"
        lblContexte.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnClose
        ' 
        btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnClose.Location = New Point(1250, 15)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(130, 30)
        btnClose.TabIndex = 1
        btnClose.Text = "⬅ Retour Home"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' pnlNavigation
        ' 
        pnlNavigation.BackColor = SystemColors.Control
        pnlNavigation.Controls.Add(btnAccueil)
        pnlNavigation.Controls.Add(btnLangues)
        pnlNavigation.Controls.Add(btnPays)
        pnlNavigation.Controls.Add(btnContacts)
        pnlNavigation.Controls.Add(btnEditeurs)
        pnlNavigation.Controls.Add(btnFormatFile)
        pnlNavigation.Controls.Add(btnImpression)
        pnlNavigation.Controls.Add(btnPrixLit)
        pnlNavigation.Controls.Add(btnRecommandations)
        pnlNavigation.Controls.Add(btnRefEnum)
        pnlNavigation.Dock = DockStyle.Left
        pnlNavigation.Location = New Point(0, 60)
        pnlNavigation.Name = "pnlNavigation"
        pnlNavigation.Size = New Size(180, 700)
        pnlNavigation.TabIndex = 1
        ' 
        ' btnAccueil
        ' 
        btnAccueil.BackColor = SystemColors.Info
        btnAccueil.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAccueil.Location = New Point(10, 10)
        btnAccueil.Name = "btnAccueil"
        btnAccueil.Size = New Size(160, 40)
        btnAccueil.TabIndex = 0
        btnAccueil.Text = "🏠 Accueil"
        btnAccueil.UseVisualStyleBackColor = False
        ' 
        ' btnLangues
        ' 
        btnLangues.Location = New Point(10, 60)
        btnLangues.Name = "btnLangues"
        btnLangues.Size = New Size(160, 35)
        btnLangues.TabIndex = 1
        btnLangues.Text = "Langues"
        btnLangues.UseVisualStyleBackColor = True
        ' 
        ' btnPays
        ' 
        btnPays.Location = New Point(10, 100)
        btnPays.Name = "btnPays"
        btnPays.Size = New Size(160, 35)
        btnPays.TabIndex = 2
        btnPays.Text = "Pays"
        btnPays.UseVisualStyleBackColor = True
        ' 
        ' btnContacts
        ' 
        btnContacts.Location = New Point(10, 140)
        btnContacts.Name = "btnContacts"
        btnContacts.Size = New Size(160, 35)
        btnContacts.TabIndex = 3
        btnContacts.Text = "Contacts"
        btnContacts.UseVisualStyleBackColor = True
        ' 
        ' btnEditeurs
        ' 
        btnEditeurs.Location = New Point(10, 180)
        btnEditeurs.Name = "btnEditeurs"
        btnEditeurs.Size = New Size(160, 35)
        btnEditeurs.TabIndex = 4
        btnEditeurs.Text = "Éditeurs"
        btnEditeurs.UseVisualStyleBackColor = True
        ' 
        ' btnFormatFile
        ' 
        btnFormatFile.Location = New Point(10, 220)
        btnFormatFile.Name = "btnFormatFile"
        btnFormatFile.Size = New Size(160, 35)
        btnFormatFile.TabIndex = 5
        btnFormatFile.Text = "Format Fichier"
        btnFormatFile.UseVisualStyleBackColor = True
        ' 
        ' btnImpression
        ' 
        btnImpression.Location = New Point(10, 260)
        btnImpression.Name = "btnImpression"
        btnImpression.Size = New Size(160, 35)
        btnImpression.TabIndex = 6
        btnImpression.Text = "Impression"
        btnImpression.UseVisualStyleBackColor = True
        ' 
        ' btnPrixLit
        ' 
        btnPrixLit.Location = New Point(10, 300)
        btnPrixLit.Name = "btnPrixLit"
        btnPrixLit.Size = New Size(160, 35)
        btnPrixLit.TabIndex = 7
        btnPrixLit.Text = "Prix Littéraires"
        btnPrixLit.UseVisualStyleBackColor = True
        ' 
        ' btnRecommandations
        ' 
        btnRecommandations.Location = New Point(10, 340)
        btnRecommandations.Name = "btnRecommandations"
        btnRecommandations.Size = New Size(160, 35)
        btnRecommandations.TabIndex = 8
        btnRecommandations.Text = "Recommandations"
        btnRecommandations.UseVisualStyleBackColor = True
        ' 
        ' btnRefEnum
        ' 
        btnRefEnum.Location = New Point(10, 380)
        btnRefEnum.Name = "btnRefEnum"
        btnRefEnum.Size = New Size(160, 35)
        btnRefEnum.TabIndex = 9
        btnRefEnum.Text = "Énumérations"
        btnRefEnum.UseVisualStyleBackColor = True
        ' 
        ' pnlMain
        ' 
        pnlMain.AutoScroll = True
        pnlMain.BackColor = SystemColors.Window
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(180, 60)
        pnlMain.Name = "pnlMain"
        pnlMain.Size = New Size(1220, 700)
        pnlMain.TabIndex = 2
        ' 
        ' stsStatus
        ' 
        stsStatus.Items.AddRange(New ToolStripItem() {stsLabelStatus})
        stsStatus.Location = New Point(0, 760)
        stsStatus.Name = "stsStatus"
        stsStatus.Size = New Size(1400, 22)
        stsStatus.TabIndex = 3
        stsStatus.Text = "StatusStrip1"
        ' 
        ' stsLabelStatus
        ' 
        stsLabelStatus.Name = "stsLabelStatus"
        stsLabelStatus.Size = New Size(31, 17)
        stsLabelStatus.Text = "Prêt."
        ' 
        ' errProvider
        ' 
        errProvider.ContainerControl = Me
        ' 
        ' PortailReferentiels
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1400, 782)
        Controls.Add(pnlMain)
        Controls.Add(pnlNavigation)
        Controls.Add(pnlTop)
        Controls.Add(stsStatus)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MinimumSize = New Size(1000, 600)
        Name = "PortailReferentiels"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Artefact - Portail des Référentiels"
        pnlTop.ResumeLayout(False)
        pnlNavigation.ResumeLayout(False)
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        CType(errProvider, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblContexte As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents pnlNavigation As Panel
    Friend WithEvents btnAccueil As Button
    Friend WithEvents btnLangues As Button
    Friend WithEvents btnPays As Button
    Friend WithEvents btnContacts As Button
    Friend WithEvents btnEditeurs As Button
    Friend WithEvents btnFormatFile As Button
    Friend WithEvents btnImpression As Button
    Friend WithEvents btnPrixLit As Button
    Friend WithEvents btnRecommandations As Button
    Friend WithEvents btnRefEnum As Button
    Friend WithEvents pnlMain As Panel
    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents errProvider As ErrorProvider
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents lblTitre As Label
End Class
