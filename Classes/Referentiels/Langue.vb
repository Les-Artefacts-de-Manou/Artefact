'------------------------------------------------------------
' 📌 Langue.vb
' Version : V1.0
' Date    : 02/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Modèle représentant une Langue (table langues).
' Aucun SQL, aucun accès DB, aucun log.
'
' Évolution :
' - V1.0 : Création du modèle Langue.
'------------------------------------------------------------
Public Class Langue

#Region "Propriétés"

    Public Property IdLangue As ULong
    Public Property NomLangue As String
    Public Property AbrevLangue As String
    Public Property Iso639_1 As String
    Public Property Iso639_2 As String
    Public Property CodeLangue As String

#End Region

End Class
