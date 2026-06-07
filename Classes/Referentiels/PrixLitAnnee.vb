' ****************************************************************************************************
' Module/Class : PrixLitAnnee.vb
' Auteur       : Artefact / ChatGPT
' Créé le      : 21/03/2026
' Version      : 1.0
' But          : Classe métier mappée sur la table prixlit_annee
' ****************************************************************************************************

Option Strict On
Option Explicit On

Public Class PrixLitAnnee

#Region "Propriétés"

    Public Property IdPrixLitAnnee As ULong
    Public Property IdPrixLitCategorie As ULong

    Public Property Annee As Integer

    Public Property CreatedAt As DateTime
    Public Property UpdatedAt As DateTime

    Public Property CodePrixLitAnnee As String = String.Empty

#End Region

End Class