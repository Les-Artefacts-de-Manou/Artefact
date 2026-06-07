<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DialogChoix
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
        pnlMain = New Panel()
        lblMessage = New Label()
        pnlBoutons = New Panel()
        btn1 = New Button()
        btn2 = New Button()
        btn3 = New Button()
        pnlMain.SuspendLayout()
        pnlBoutons.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.Controls.Add(lblMessage)
        pnlMain.Controls.Add(pnlBoutons)
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(15)
        pnlMain.Size = New Size(500, 200)
        pnlMain.TabIndex = 0
        ' 
        ' lblMessage
        ' 
        lblMessage.Dock = DockStyle.Fill
        lblMessage.Font = New Font("Segoe UI", 10.0F)
        lblMessage.Location = New Point(15, 15)
        lblMessage.Name = "lblMessage"
        lblMessage.Size = New Size(470, 115)
        lblMessage.TabIndex = 0
        lblMessage.Text = "Message de confirmation"
        lblMessage.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlBoutons
        ' 
        pnlBoutons.Controls.Add(btn3)
        pnlBoutons.Controls.Add(btn2)
        pnlBoutons.Controls.Add(btn1)
        pnlBoutons.Dock = DockStyle.Bottom
        pnlBoutons.Location = New Point(15, 130)
        pnlBoutons.Name = "pnlBoutons"
        pnlBoutons.Size = New Size(470, 55)
        pnlBoutons.TabIndex = 1
        ' 
        ' btn1
        ' 
        btn1.Anchor = AnchorStyles.Right
        btn1.Location = New Point(50, 12)
        btn1.Name = "btn1"
        btn1.Size = New Size(120, 35)
        btn1.TabIndex = 0
        btn1.Text = "Bouton 1"
        btn1.UseVisualStyleBackColor = True
        ' 
        ' btn2
        ' 
        btn2.Anchor = AnchorStyles.Right
        btn2.Location = New Point(176, 12)
        btn2.Name = "btn2"
        btn2.Size = New Size(120, 35)
        btn2.TabIndex = 1
        btn2.Text = "Bouton 2"
        btn2.UseVisualStyleBackColor = True
        ' 
        ' btn3
        ' 
        btn3.Anchor = AnchorStyles.Right
        btn3.DialogResult = DialogResult.Cancel
        btn3.Location = New Point(302, 12)
        btn3.Name = "btn3"
        btn3.Size = New Size(120, 35)
        btn3.TabIndex = 2
        btn3.Text = "Annuler"
        btn3.UseVisualStyleBackColor = True
        ' 
        ' DialogChoix
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn3
        ClientSize = New Size(500, 200)
        Controls.Add(pnlMain)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogChoix"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Confirmation"
        pnlMain.ResumeLayout(False)
        pnlBoutons.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents lblMessage As Label
    Friend WithEvents pnlBoutons As Panel
    Friend WithEvents btn1 As Button
    Friend WithEvents btn2 As Button
    Friend WithEvents btn3 As Button

End Class
