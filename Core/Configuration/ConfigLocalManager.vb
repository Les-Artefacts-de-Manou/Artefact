'------------------------------------------------------------
' 📌 ConfigLocalManager.vb - Module
' Version : V1.1
' Date    : 25/02/2026
' Auteur  : Joëlle
'
' Rôle :
' Lecture / écriture de la configuration locale Artefact (JSON).
' Contient la config DB, avec password chiffré DPAPI (Base64).
'------------------------------------------------------------

Option Strict On
Option Infer On
Option Explicit On

Imports System.IO
Imports System.Text.Json

Public Module ConfigLocalManager

#Region "Constantes / Paths"

    Private Const CONFIG_FILE_NAME As String = "artefact.local.json"

    '------------------------------------------------------------
    ' 📌 V1.1 - 25/02/2026
    ' GetConfigFilePath
    '
    ' Retourne le chemin complet du fichier de config local.
    ' Source unique : GetArtefactFolderPath().
    '------------------------------------------------------------
    Public Function GetConfigFilePath() As String
        Dim dir As String = GetArtefactFolderPath()
        Return Path.Combine(dir, CONFIG_FILE_NAME)
    End Function

#End Region

#Region "Lecture / Écriture DB Config"

    '------------------------------------------------------------
    ' 📌 V1.1 - 25/02/2026
    ' LireConfigDb
    '
    ' Lit la config DB depuis le fichier JSON local.
    ' Retourne Nothing si absent ou illisible.
    ' Logs PROD : niveau + catégorie + exception (si KO).
    '------------------------------------------------------------
    Public Function LireConfigDb() As LocalDbConfig

        EnsureAppFolder()

        Dim path As String = GetConfigFilePath()

        If Not File.Exists(path) Then
            GestionLog.EcrireLog(
            "Config locale absente.",
            GestionLog.LogLevel.Rapide,
            GestionLog.LogCategory.Process
        )
            Return Nothing
        End If

        Try
            Dim json As String = File.ReadAllText(path)

            Dim options As New JsonSerializerOptions With {
            .PropertyNameCaseInsensitive = True
        }

            Dim cfg As LocalDbConfig = JsonSerializer.Deserialize(Of LocalDbConfig)(json, options)

            If cfg Is Nothing Then
                GestionLog.EcrireLog(
                "Config locale illisible (désérialisation retourne Nothing).",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.Process
            )
                Return Nothing
            End If

            Return cfg

        Catch ex As Exception
            GestionLog.EcrireLog(
            "Lecture config locale KO.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Process,
            ex
        )
            Return Nothing
        End Try

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 25/02/2026
    ' SauvegarderConfigDb
    '
    ' Sauvegarde la config DB dans le fichier JSON local.
    ' Crée le dossier si nécessaire.
    ' Logs PROD : niveau + catégorie + exception (si KO).
    ' En cas d'échec d'écriture : log + Throw (erreur critique).
    '------------------------------------------------------------
    Public Sub SauvegarderConfigDb(cfg As LocalDbConfig)

        If cfg Is Nothing Then Throw New ArgumentNullException(NameOf(cfg))

        EnsureAppFolder()

        Dim filePath As String = GetConfigFilePath()

        Try
            Dim options As New JsonSerializerOptions With {
            .WriteIndented = True
        }

            Dim json As String = JsonSerializer.Serialize(cfg, options)
            File.WriteAllText(filePath, json)

            GestionLog.EcrireLog(
            "Config locale sauvegardée.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Process
        )

        Catch ex As Exception
            GestionLog.EcrireLog(
            "Sauvegarde config locale KO.",
            GestionLog.LogLevel.Succinct,
            GestionLog.LogCategory.Process,
            ex
        )
            Throw
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 23/02/2026
    ' EnsureAppFolder
    '
    ' Garantit l'existence du dossier %APPDATA%\Artefact\.
    '------------------------------------------------------------
    Private Sub EnsureAppFolder()

        Dim folder As String = GetArtefactFolderPath()

        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 23/02/2026
    ' GetArtefactFolderPath
    '
    ' Retourne le chemin racine : %APPDATA%\Artefact\
    '------------------------------------------------------------
    Public Function GetArtefactFolderPath() As String

        Dim appData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Return Path.Combine(appData, "Artefact")

    End Function

#End Region

End Module
