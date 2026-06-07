'------------------------------------------------------------
' 📌 FormatFile.vb
' Version : V1.0
' Date    : 13/03/2026
'
' Rôle :
' Classe métier représentant un format de fichier.
'
' Remarques :
' - id_formatFile   : clé technique
' - code_formatFile : code généré par la base, lecture seule côté UI
'------------------------------------------------------------
Public Class FormatFile

    Public Property IdFormatFile As ULong

    Public Property CodeFormatFile As String

    Public Property NomFormat As String

    Public Property Extension As String

    Public Property MimeType As String

    Public Property OrdreAffichage As Integer

    Public Property IsActif As Boolean

End Class