' ****************************************************************************************************
' Module/Class : PrixLit.vb
' Auteur       : Artefact / ChatGPT
' Créé le      : 21/03/2026
' Version      : 1.0
' But          : Classe métier mappée sur la table prixlit
' ****************************************************************************************************

Option Strict On
Option Explicit On

Public Class PrixLit

#Region "Propriétés"

    Public Property IdPrixLit As ULong
    Public Property NomPrixLit As String = String.Empty
    Public Property DescriptionPrixLit As String = String.Empty

    Public Property NotesPrixLitTxt As String = String.Empty
    Public Property NotesPrixLitRtf As String = String.Empty

    Public Property IsActif As Boolean = True

    Public Property CreatedAt As DateTime
    Public Property UpdatedAt As DateTime

    Public Property CodePrixLit As String = String.Empty

#End Region


End Class
