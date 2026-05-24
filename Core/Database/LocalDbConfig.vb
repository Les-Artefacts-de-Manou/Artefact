'------------------------------------------------------------
' 📌 LocalDbConfig.vb - Classe
' Version : V1.1
' Date    : 25/02/2026
' Auteur  : Joëlle
'
' Rôle :
' Modèle de configuration locale de connexion MariaDB.
' Stockée dans artefact.local.json.
'------------------------------------------------------------

Option Strict On
Option Infer On
Option Explicit On

Imports System.Text.Json.Serialization

Public Class LocalDbConfig

#Region "Propriétés"

    Public Property EnvCode As String = "LOCAL"
    Public Property NomConnexion As String = "MariaDB_Artefact"

    Public Property Host As String = ""
    Public Property Port As Integer = 3306
    Public Property Database As String = ""
    Public Property UserName As String = ""

    ' Mot de passe chiffré DPAPI, encodé en Base64
    <JsonPropertyName("PasswordEncB64")>
    Public Property PasswordEncB64 As String = ""

    ' Compatibilité éventuelle (anciens JSON) :
    ' ancien champ password potentiellement en clair ou en Base64 DPAPI.
    <JsonPropertyName("Password"), JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property PasswordLegacyRaw As String = Nothing

    ' Options libres MySql (ex: "Connection Timeout=5;Allow User Variables=true")
    Public Property OptionsConn As String = ""

#End Region

End Class
