'------------------------------------------------------------
' 📌 CryptoManagerDPAPI.vb - Module
' Version : V1.1
' Date    : 25/02/2026
' Auteur  : Joëlle
'
' Rôle :
' Chiffrement / déchiffrement via DPAPI Windows.
' Utilisé pour protéger les secrets (password DB).
'------------------------------------------------------------

Option Strict On
Option Infer On
Option Explicit On

Imports System.Security.Cryptography
Imports System.Text

Public Module CryptoManagerDPAPI

#Region "Encrypt / Decrypt String Base64"

    '------------------------------------------------------------
    ' 📌 V1.2 - 25/02/2026
    ' EncryptStringToBase64
    '
    ' Chiffre une chaîne en DPAPI et retourne un Base64.
    ' Throw si DPAPI Protect échoue.
    '------------------------------------------------------------
    Public Function EncryptStringToBase64(plainText As String) As String

        If plainText Is Nothing Then plainText = ""

        Try
            Dim dataBytes As Byte() = Encoding.UTF8.GetBytes(plainText)
            Dim cipherBytes As Byte() = ProtectedData.Protect(dataBytes, Nothing, DataProtectionScope.CurrentUser)
            Return Convert.ToBase64String(cipherBytes)

        Catch ex As CryptographicException
            Throw New Exception("DPAPI: chiffrement impossible.", ex)
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 25/02/2026
    ' DecryptStringFromBase64
    '
    ' Déchiffre un Base64 DPAPI et retourne la chaîne en clair.
    ' - Retourne "" si entrée vide
    ' - Throw si base64 invalide ou DPAPI Unprotect échoue
    '------------------------------------------------------------
    Public Function DecryptStringFromBase64(base64Cipher As String) As String

        If String.IsNullOrWhiteSpace(base64Cipher) Then Return ""

        Try
            Dim cipherBytes As Byte() = Convert.FromBase64String(base64Cipher)
            Dim dataBytes As Byte() = ProtectedData.Unprotect(cipherBytes, Nothing, DataProtectionScope.CurrentUser)
            Return Encoding.UTF8.GetString(dataBytes)

        Catch ex As FormatException
            Throw New Exception("DPAPI: Base64 invalide (format incorrect).", ex)

        Catch ex As CryptographicException
            Throw New Exception("DPAPI: déchiffrement impossible (données corrompues ou utilisateur Windows différent).", ex)
        End Try

    End Function

#End Region

End Module
