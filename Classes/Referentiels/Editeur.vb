'------------------------------------------------------------
' 📌 Editeur.vb
' Version : V1.1
' Date    : 19/03/2026
'
' Rôle :
' Classe métier représentant un éditeur.
'
' Remarques :
' - id_editeur   : clé technique
' - code_editeur : code généré par la base, lecture seule côté UI
' - id_pays      : nullable (FK SET NULL)
' -NotesEditeurRtf = source d’affichage UI
' -NotesEditeurTxt = source de recherche SQL

' Historique des modifications :
' 1.1 - 19/03/2026 - Joelle - Ajout de notes_editeurRtf et notes_editeurTxt ((RichTextBox enrichi)
'------------------------------------------------------------
Public Class Editeur

    Public Property IdEditeur As ULong

    Public Property CodeEditeur As String

    Public Property NomEditeur As String

    Public Property IdPays As ULong?

    Public Property SiteWeb As String

    Public Property NotesEditeurRtf As String
    Public Property NotesEditeurTxt As String


End Class
