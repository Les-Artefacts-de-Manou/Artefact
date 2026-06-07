' ****************************************************************************************************
' Module/Class : PrixLitCategorie.vb
' Auteur       : Artefact / ChatGPT
' Créé le      : 21/03/2026
' Version      : 1.0
' But          : Classe métier mappée sur la table prixlit_categorie
' ****************************************************************************************************

Option Strict On
Option Explicit On

Public Class PrixLitCategorie

#Region "Propriétés"

    Public Property IdPrixLitCategorie As ULong
    Public Property IdPrixLit As ULong

    Public Property LibelleCategorie As String = String.Empty
    Public Property DescriptionCategorie As String = String.Empty

    Public Property OrdreAffichage As Integer = 0
    Public Property IsActif As Boolean = True

    Public Property CreatedAt As DateTime
    Public Property UpdatedAt As DateTime

    Public Property CodePrixLitCategorie As String = String.Empty

#End Region


End Class
