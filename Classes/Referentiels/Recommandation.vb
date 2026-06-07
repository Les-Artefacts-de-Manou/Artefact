'------------------------------------------------------------
' 📌 Recommandation.vb
' Version : V1.0
' Date    : 17/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Représente une recommandation liée à une origine.
'
' Évolution :
' - V1.0 : Création de la classe métier.
'------------------------------------------------------------

Public Class Recommandation

#Region "Propriétés"

    Public Property IdRecommandation As ULong
    Public Property CodeRecommandation As String = String.Empty

    Public Property IdOrigineRecommandation As ULong

    Public Property SourceNom As String = String.Empty
    Public Property SourceLogin As String = String.Empty
    Public Property SourceURL As String = String.Empty

    Public Property DateRecommandation As Date?
    Public Property CommentaireRtf As String
    Public Property CommentaireTxt As String

    Public Property IsActif As Boolean = True

#End Region

End Class
