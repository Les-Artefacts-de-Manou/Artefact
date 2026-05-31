'------------------------------------------------------------
' 📌 RichTextNotesHelper.vb
' Version : V1.0
' Date    : 20/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Centralise les opérations génériques liées aux champs de notes
' enrichies utilisant un RichTextBox avec stockage RTF persistant.
'
' Évolution :
' - V1.0 : Création du helper central pour configuration,
'          chargement sécurisé et extraction RTF/TXT.
'------------------------------------------------------------

Imports System.Drawing
Imports System.Windows.Forms

Public Module RichTextNotesHelper

#Region "Configuration"

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' ConfigurerRichTextBoxNotes
    '
    '   • Applique la configuration standard d'un RichTextBox
    '     utilisé pour les notes enrichies
    '------------------------------------------------------------
    Public Sub ConfigurerRichTextBoxNotes(rtb As RichTextBox)

        If rtb Is Nothing Then Exit Sub

        rtb.AcceptsTab = True
        rtb.DetectUrls = True
        rtb.HideSelection = False
        rtb.WordWrap = True
        rtb.ScrollBars = RichTextBoxScrollBars.Vertical

    End Sub

#End Region

#Region "Chargement / Extraction"

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' ChargerContenuNotes
    '
    '   • Charge le contenu RTF dans le RichTextBox
    '   • Utilise le texte brut en fallback si le RTF est vide ou invalide
    '------------------------------------------------------------
    Public Sub ChargerContenuNotes(rtb As RichTextBox, rtfValue As String, txtFallback As String)

        If rtb Is Nothing Then Exit Sub

        rtb.Clear()

        If String.IsNullOrWhiteSpace(rtfValue) Then
            rtb.Text = If(txtFallback, "")
            Exit Sub
        End If

        Try
            rtb.Rtf = rtfValue
        Catch
            rtb.Text = If(txtFallback, "")
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' GetNotesRtf
    '
    '   • Retourne le contenu RTF à sauvegarder
    '   • Retourne chaîne vide si le contrôle est vide visuellement
    '------------------------------------------------------------
    Public Function GetNotesRtf(rtb As RichTextBox) As String

        If rtb Is Nothing Then Return ""

        If String.IsNullOrWhiteSpace(rtb.Text) Then
            Return ""
        End If

        Return rtb.Rtf

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' GetNotesTxt
    '
    '   • Retourne le texte brut miroir à sauvegarder
    '------------------------------------------------------------
    Public Function GetNotesTxt(rtb As RichTextBox) As String

        If rtb Is Nothing Then Return ""

        Return rtb.Text.Trim()

    End Function

#End Region

#Region "Mise en forme"

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' BasculerGras
    '
    '   • Active ou désactive le gras sur la sélection courante
    '------------------------------------------------------------
    Public Sub BasculerGras(rtb As RichTextBox)

        BasculerStyleSelection(rtb, FontStyle.Bold)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' BasculerItalique
    '
    '   • Active ou désactive l'italique sur la sélection courante
    '------------------------------------------------------------
    Public Sub BasculerItalique(rtb As RichTextBox)

        BasculerStyleSelection(rtb, FontStyle.Italic)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' BasculerSouligne
    '
    '   • Active ou désactive le soulignement sur la sélection courante
    '------------------------------------------------------------
    Public Sub BasculerSouligne(rtb As RichTextBox)

        BasculerStyleSelection(rtb, FontStyle.Underline)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' BasculerListe
    '
    '   • Active ou désactive le mode liste à puces
    '------------------------------------------------------------
    Public Sub BasculerListe(rtb As RichTextBox)

        If rtb Is Nothing Then Exit Sub

        rtb.SelectionBullet = Not rtb.SelectionBullet

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' InsererTabulation
    '
    '   • Insère une tabulation à la position courante
    '------------------------------------------------------------
    Public Sub InsererTabulation(rtb As RichTextBox)

        If rtb Is Nothing Then Exit Sub

        rtb.SelectedText = vbTab

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/03/2026
    ' BasculerStyleSelection
    '
    '   • Applique ou retire un style de police sur la sélection
    '------------------------------------------------------------
    Private Sub BasculerStyleSelection(rtb As RichTextBox, style As FontStyle)

        If rtb Is Nothing Then Exit Sub
        If rtb.SelectionFont Is Nothing Then Exit Sub

        Dim currentFont As Font = rtb.SelectionFont
        Dim newStyle As FontStyle

        If currentFont.Style.HasFlag(style) Then
            newStyle = currentFont.Style And Not style
        Else
            newStyle = currentFont.Style Or style
        End If

        rtb.SelectionFont = New Font(currentFont, newStyle)

    End Sub

#End Region

End Module
