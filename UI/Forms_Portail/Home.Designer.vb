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
        lblTitreForm = New Label()
        btnOpenPortailReferentiels = New Button()
        Button1 = New Button()
        stsStatus.SuspendLayout()
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
        ' btnOpenPortailReferentiels
        ' 
        btnOpenPortailReferentiels.Location = New Point(21, 125)
        btnOpenPortailReferentiels.Name = "btnOpenPortailReferentiels"
        btnOpenPortailReferentiels.Size = New Size(166, 23)
        btnOpenPortailReferentiels.TabIndex = 10
        btnOpenPortailReferentiels.Text = "Open Portail Referentiels"
        btnOpenPortailReferentiels.TextImageRelation = TextImageRelation.ImageBeforeText
        btnOpenPortailReferentiels.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(60, 193)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 11
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Home
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button1)
        Controls.Add(btnOpenPortailReferentiels)
        Controls.Add(lblTitreForm)
        Controls.Add(stsStatus)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Home"
        Text = "Home"
        stsStatus.ResumeLayout(False)
        stsStatus.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents stsStatus As StatusStrip
    Friend WithEvents stsLabelStatus As ToolStripStatusLabel
    Friend WithEvents lblTitreForm As Label
    ' ⚠️ OBSOLÈTE - Boutons désactivés le 25/03/2026
    ' Friend WithEvents btnOpenGestionImpression As Button
    ' Friend WithEvents btnOpenGestionRecommandation As Button
    ' Friend WithEvents btnOpenGestionPrixLit As Button
    Friend WithEvents btnOpenPortailReferentiels As Button
    Friend WithEvents Button1 As Button


End Class
