'------------------------------------------------------------
' 📌 RefEnumValeur.vb
' Version : V1.0
' Date    : 04/03/2026
'
' Rôle :
' Entité métier représentant une valeur d'un type (ref_enum).
'
' Notes :
' - Aucun SQL ici.
' - Aucun accès DB.
'------------------------------------------------------------

Option Strict On
Option Explicit On

Public Class RefEnumValeur

    Public Property IdEnum As ULong
    Public Property CodeEnum As String         ' Code généré (ex: E000123) selon ton préfixe
    Public Property IdEnumType As ULong        ' FK vers ref_enum_type

    Public Property CodeValeur As String       ' Clé technique (MAJUSCULE) ex: ACHAT
    Public Property LibelleValeur As String    ' Libellé humain ex: Achat

    Public Property OrdreAffichage As Integer
    Public Property IsActif As Boolean

End Class
