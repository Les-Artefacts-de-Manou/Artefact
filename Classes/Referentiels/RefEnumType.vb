'------------------------------------------------------------
' 📌 RefEnumType.vb
' Version : V1.0
' Date    : 04/03/2026
'
' Rôle :
' Entité métier représentant une catégorie de valeurs (type_enum).
'
' Notes :
' - Aucun SQL ici.
' - Aucun accès DB.
'------------------------------------------------------------

Public Class RefEnumType

    Public Property IdEnumType As ULong
    Public Property CodeEnumType As String     ' Code généré (ex: ET000001)
    Public Property CodeType As String         ' Clé technique (MAJUSCULE) ex: TYPE_ACQUISITION
    Public Property LibelleType As String      ' Libellé humain
    Public Property OrdreAffichage As Integer
    Public Property IsActif As Boolean

    End Class


