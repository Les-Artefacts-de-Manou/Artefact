'------------------------------------------------------------
' 📌 Pays.vb
' Version : V1.0
' Date    : 03/03/2026
'
' Rôle :
' Entité métier correspondant à la table pays.
' - Aucun SQL
' - Aucun accès DB
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class Pays

    Public Property IdPays As ULong
    Public Property NomPays As String
    Public Property Iso2 As String
    Public Property Iso3 As String
    Public Property CodePays As String

End Class
