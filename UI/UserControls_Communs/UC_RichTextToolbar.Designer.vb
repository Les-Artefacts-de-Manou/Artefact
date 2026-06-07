<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_RichTextToolbar
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
        tsToolbar = New ToolStrip()
        btnBold = New ToolStripButton()
        btnItalic = New ToolStripButton()
        btnUnderline = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnBullet = New ToolStripButton()
        btnTab = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnColor = New ToolStripButton()
        btnFontSize = New ToolStripButton()
        tsToolbar.SuspendLayout()
        SuspendLayout()
        ' 
        ' tsToolbar
        ' 
        tsToolbar.BackColor = Color.Transparent
        tsToolbar.Dock = DockStyle.Fill
        tsToolbar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        tsToolbar.GripStyle = ToolStripGripStyle.Hidden
        tsToolbar.Items.AddRange(New ToolStripItem() {btnBold, btnItalic, btnUnderline, ToolStripSeparator1, btnBullet, btnTab, ToolStripSeparator2, btnColor, btnFontSize})
        tsToolbar.Location = New Point(0, 0)
        tsToolbar.Name = "tsToolbar"
        tsToolbar.Size = New Size(320, 25)
        tsToolbar.TabIndex = 0
        tsToolbar.Text = "ToolStrip1"
        ' 
        ' btnBold
        ' 
        btnBold.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnBold.ImageTransparentColor = Color.Magenta
        btnBold.Name = "btnBold"
        btnBold.Size = New Size(23, 22)
        btnBold.Text = "B"
        btnBold.ToolTipText = "Gras"
        ' 
        ' btnItalic
        ' 
        btnItalic.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnItalic.Font = New Font("Segoe UI", 9.0F, FontStyle.Italic)
        btnItalic.ImageTransparentColor = Color.Magenta
        btnItalic.Name = "btnItalic"
        btnItalic.Size = New Size(23, 22)
        btnItalic.Text = "I"
        btnItalic.ToolTipText = "Italique"
        ' 
        ' btnUnderline
        ' 
        btnUnderline.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnUnderline.Font = New Font("Segoe UI", 9.0F, FontStyle.Underline)
        btnUnderline.ImageTransparentColor = Color.Magenta
        btnUnderline.Name = "btnUnderline"
        btnUnderline.Size = New Size(23, 22)
        btnUnderline.Text = "U"
        btnUnderline.ToolTipText = "Souligné"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 25)
        ' 
        ' btnBullet
        ' 
        btnBullet.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnBullet.ImageTransparentColor = Color.Magenta
        btnBullet.Name = "btnBullet"
        btnBullet.Size = New Size(23, 22)
        btnBullet.Text = "•"
        btnBullet.ToolTipText = "Liste à puces"
        ' 
        ' btnTab
        ' 
        btnTab.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTab.ImageTransparentColor = Color.Magenta
        btnTab.Name = "btnTab"
        btnTab.Size = New Size(29, 22)
        btnTab.Text = "→"
        btnTab.ToolTipText = "Tabulation"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 25)
        ' 
        ' btnColor
        ' 
        btnColor.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnColor.ImageTransparentColor = Color.Magenta
        btnColor.Name = "btnColor"
        btnColor.Size = New Size(23, 22)
        btnColor.Text = "🎨"
        btnColor.ToolTipText = "Couleur"
        ' 
        ' btnFontSize
        ' 
        btnFontSize.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnFontSize.ImageTransparentColor = Color.Magenta
        btnFontSize.Name = "btnFontSize"
        btnFontSize.Size = New Size(23, 22)
        btnFontSize.Text = "T"
        btnFontSize.ToolTipText = "Taille police"
        ' 
        ' UC_RichTextToolbar
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(tsToolbar)
        Name = "UC_RichTextToolbar"
        Size = New Size(320, 25)
        tsToolbar.ResumeLayout(False)
        tsToolbar.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents tsToolbar As ToolStrip
    Friend WithEvents btnBold As ToolStripButton
    Friend WithEvents btnItalic As ToolStripButton
    Friend WithEvents btnUnderline As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnBullet As ToolStripButton
    Friend WithEvents btnTab As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnColor As ToolStripButton
    Friend WithEvents btnFontSize As ToolStripButton

End Class
