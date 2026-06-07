'------------------------------------------------------------
' 📌 RefOrigineRecommandation.vb
' Version : V1.0
' Date    : 17/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Représente une origine de recommandation.
'
' Évolution :
' - V1.0 : Création de la classe métier.
'------------------------------------------------------------

Public Class RefOrigineRecommandation

#Region "Propriétés"

    Public Property IdOrigineRecommandation As ULong
    Public Property CodeOrigineRecommandation As String = String.Empty
    Public Property LibelleOrigineRecommandation As String = String.Empty
    Public Property OrdreAffichage As Integer
    Public Property IsActif As Boolean = True

#End Region

End Class
