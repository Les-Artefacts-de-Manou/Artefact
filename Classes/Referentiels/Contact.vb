
'------------------------------------------------------------
' 📌 Contact.vb
' Version : V1.0
' Date    : 07/03/2026
'
' Rôle :
' Classe métier représentant un contact de destination
' pour l’envoi de livres.
'
' Remarques :
' - id_contact : clé technique
' - code_contact : code généré par la base, lecture seule côté UI
' - created_at / updated_at non nécessaires dans la phase 1 UI
'------------------------------------------------------------

Public Class Contact

    Public Property IdContact As ULong

    Public Property CodeContact As String

    Public Property NomContact As String

    Public Property EmailPerso As String

    Public Property AdresseLiseuse As String

    Public Property TypeLiseuse As String

End Class
