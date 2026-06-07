<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_Accueil
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
        lblBienvenue = New Label()
        lblDescription = New Label()
        pnlMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.Controls.Add(lblDescription)
        pnlMain.Controls.Add(lblBienvenue)
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Name = "pnlMain"
        pnlMain.Size = New Size(1000, 650)
        pnlMain.TabIndex = 0
        ' 
        ' lblBienvenue
        ' 
        lblBienvenue.Anchor = AnchorStyles.None
        lblBienvenue.Font = New Font("Segoe UI", 24.0F, FontStyle.Bold)
        lblBienvenue.ForeColor = SystemColors.HotTrack
        lblBienvenue.Location = New Point(200, 150)
        lblBienvenue.Name = "lblBienvenue"
        lblBienvenue.Size = New Size(600, 60)
        lblBienvenue.TabIndex = 0
        lblBienvenue.Text = "Bienvenue dans le Portail Référentiels"
        lblBienvenue.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblDescription
        ' 
        lblDescription.Anchor = AnchorStyles.None
        lblDescription.Font = New Font("Segoe UI", 12.0F)
        lblDescription.Location = New Point(200, 230)
        lblDescription.Name = "lblDescription"
        lblDescription.Size = New Size(600, 150)
        lblDescription.TabIndex = 1
        lblDescription.Text = "Sélectionnez un référentiel dans le menu de gauche" & Environment.NewLine & Environment.NewLine & "Langues • Pays • Contacts • Éditeurs" & Environment.NewLine & "Formats Fichier • Impressions • Prix Littéraires" & Environment.NewLine & "Recommandations • Énumérations"
        lblDescription.TextAlign = ContentAlignment.TopCenter
        ' 
        ' UC_Accueil
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Window
        Controls.Add(pnlMain)
        Name = "UC_Accueil"
        Size = New Size(1000, 650)
        pnlMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents lblBienvenue As Label
    Friend WithEvents lblDescription As Label
End Class
