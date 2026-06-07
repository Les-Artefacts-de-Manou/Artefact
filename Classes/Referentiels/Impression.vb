'------------------------------------------------------------
' 📌 Impression.vb
' Version : V1.0
' Date    : 14/03/2026
'
' Rôle :
' Classe métier représentant un type d'impression.
'
' Remarques :
' - id_impression   : clé technique
' - code_impression : code généré par la base, lecture seule côté UI
' - note            : note libre associée à l'impression
'------------------------------------------------------------

Public Class Impression

    Public Property IdImpression As ULong

    Public Property CodeImpression As String

    Public Property NomImpression As String

    Public Property DescriptionImpression As String

    Public Property NoteRtf As String
    Public Property NoteTxt As String

    Public Property EnvieCal As String

    Public Property IsActif As Boolean


End Class
