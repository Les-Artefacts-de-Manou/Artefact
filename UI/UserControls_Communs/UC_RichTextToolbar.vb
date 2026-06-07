'====================================================================
' 📌 UC_RichTextToolbar.vb
' Version : V1.0
' Date    : 24/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl réutilisable pour la barre d'outils RichTextBox enrichi.
' Fournit les boutons de mise en forme : Gras, Italique, Souligné,
' Puces, Tab, Couleur, Taille police.
'
' Évolution :
' - V1.0 : Création du UserControl réutilisable
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_RichTextToolbar

#Region "Déclarations"

    ' RichTextBox cible (injectable)
    Private _targetRichTextBox As RichTextBox = Nothing

#End Region

#Region "Propriétés"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' TargetRichTextBox
    '
    '   • RichTextBox sur lequel les actions de formatting s'appliquent
    '------------------------------------------------------------
    Public Property TargetRichTextBox As RichTextBox
        Get
            Return _targetRichTextBox
        End Get
        Set(value As RichTextBox)
            _targetRichTextBox = value
        End Set
    End Property

#End Region

#Region "Actions de formatage"

    '------------------------------------------------------------
    ' 📌 V1.0 - 24/03/2026
    ' Handlers ToolStrip buttons
    '
    '   • Actions de mise en forme sur le RichTextBox cible
    '------------------------------------------------------------
    Private Sub btnBold_Click(sender As Object, e As EventArgs) Handles btnBold.Click
        If _targetRichTextBox IsNot Nothing Then
            RichTextNotesHelper.BasculerGras(_targetRichTextBox)
            _targetRichTextBox.Focus()
        End If
    End Sub

    Private Sub btnItalic_Click(sender As Object, e As EventArgs) Handles btnItalic.Click
        If _targetRichTextBox IsNot Nothing Then
            RichTextNotesHelper.BasculerItalique(_targetRichTextBox)
            _targetRichTextBox.Focus()
        End If
    End Sub

    Private Sub btnUnderline_Click(sender As Object, e As EventArgs) Handles btnUnderline.Click
        If _targetRichTextBox IsNot Nothing Then
            RichTextNotesHelper.BasculerSouligne(_targetRichTextBox)
            _targetRichTextBox.Focus()
        End If
    End Sub

    Private Sub btnBullet_Click(sender As Object, e As EventArgs) Handles btnBullet.Click
        If _targetRichTextBox IsNot Nothing Then
            RichTextNotesHelper.BasculerListe(_targetRichTextBox)
            _targetRichTextBox.Focus()
        End If
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs) Handles btnTab.Click
        If _targetRichTextBox IsNot Nothing Then
            RichTextNotesHelper.InsererTabulation(_targetRichTextBox)
            _targetRichTextBox.Focus()
        End If
    End Sub

    Private Sub btnColor_Click(sender As Object, e As EventArgs) Handles btnColor.Click
        If _targetRichTextBox IsNot Nothing Then
            Using colorDialog As New ColorDialog()
                colorDialog.Color = _targetRichTextBox.SelectionColor
                If colorDialog.ShowDialog() = DialogResult.OK Then
                    _targetRichTextBox.SelectionColor = colorDialog.Color
                    _targetRichTextBox.Focus()
                End If
            End Using
        End If
    End Sub

    Private Sub btnFontSize_Click(sender As Object, e As EventArgs) Handles btnFontSize.Click
        If _targetRichTextBox IsNot Nothing Then
            Dim currentSize As Single = If(_targetRichTextBox.SelectionFont?.Size, 10.0F)

            Dim newSize As String = InputBox("Taille de police (6-72) :", "Taille police", currentSize.ToString())
            If String.IsNullOrWhiteSpace(newSize) Then Return

            Dim size As Single
            If Single.TryParse(newSize, size) AndAlso size >= 6 AndAlso size <= 72 Then
                Dim currentFont = _targetRichTextBox.SelectionFont
                If currentFont IsNot Nothing Then
                    _targetRichTextBox.SelectionFont = New Font(currentFont.FontFamily, size, currentFont.Style)
                End If
                _targetRichTextBox.Focus()
            End If
        End If
    End Sub

#End Region

End Class
