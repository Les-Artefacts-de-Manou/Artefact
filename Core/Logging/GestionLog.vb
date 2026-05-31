'------------------------------------------------------------
' 📌 V1. - 25/02/2026
' GestionLog (PROD baseline)
'
' - Log fichier journalier : %APPDATA%\Artefact\Logs\Artefact_YYYY-MM-DD.log
' - Création dossier/fichier au premier log
' - Purge > 7 jours
' - Niveaux = marqueurs de profondeur (pas de filtre global)
' - Support exceptions : ex.Message (Succinct) + StackTrace (Complet)
' - Thread-safe (SyncLock)
' - Masquage rudimentaire des secrets (Password=..., Pwd=...)
'------------------------------------------------------------

Option Strict On
Option Infer On
Option Explicit On

Imports System.IO
Imports System.Text

Public Module GestionLog

#Region "Process: Logging"

    Public Enum LogLevel
        Rapide = 1
        Succinct = 2
        Complet = 3
    End Enum

    Public Enum LogCategory
        General = 0
        Startup = 1
        Database = 2
        UI = 3
        Process = 4
    End Enum

    Private Const RetentionDays As Integer = 7
    Private ReadOnly _lockObj As New Object()
    Private _sessionHeaderWritten As Boolean = False
    Private _sessionHeaderLogFile As String = ""

    '------------------------------------------------------------
    ' 📌 V1.3 - 23/02/2026
    ' EcrireLog
    '
    ' Ecrit un message de log (sans filtre global).
    ' Ajoute un header de session au premier log de l'exécution.
    '------------------------------------------------------------
    Public Sub EcrireLog(message As String,
                    Optional level As LogLevel = LogLevel.Succinct,
                    Optional category As LogCategory = LogCategory.General,
                    Optional ex As Exception = Nothing)

        Try
            Dim safeMessage As String = MaskSensitive(message)
            Dim line As String = BuildLogLine(safeMessage, level, category, ex)

            SyncLock _lockObj

                EnsureLogFolder()
                PurgeOldLogs()

                Dim logFile As String = GetDailyLogFilePath()

                ' ✅ Header de session (une seule fois par run)
                EnsureSessionHeader(logFile)

                File.AppendAllText(logFile, line & Environment.NewLine, Encoding.UTF8)

            End SyncLock

            Debug.WriteLine(line)

        Catch ioEx As Exception
            Debug.WriteLine("GestionLog: erreur d'écriture log: " & ioEx.Message)
        End Try

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/02/2026
    ' EcrireException
    '
    ' Helper explicite pour journaliser une exception.
    '------------------------------------------------------------
    Public Sub EcrireException(contextMessage As String,
                              ex As Exception,
                              Optional level As LogLevel = LogLevel.Succinct,
                              Optional category As LogCategory = LogCategory.General)

        EcrireLog(contextMessage, level, category, ex)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/02/2026
    ' BuildLogLine
    ' Construit la ligne log finale (avec détails exception).
    '------------------------------------------------------------
    Private Function BuildLogLine(message As String,
                                  level As LogLevel,
                                  category As LogCategory,
                                  ex As Exception) As String

        Dim ts As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim head As String = $"{ts} [{level}] [{category}] {message}"

        If ex Is Nothing Then
            Return head
        End If

        Dim exMsg As String = MaskSensitive(ex.Message)

        ' Succinct : message exception uniquement.
        If level <> LogLevel.Complet Then
            Return head & " | EX: " & exMsg
        End If

        ' Complet : message + stack + inner
        Dim sb As New StringBuilder()
        sb.Append(head)
        sb.Append(" | EX: ").Append(exMsg)

        If Not String.IsNullOrWhiteSpace(ex.StackTrace) Then
            sb.Append(" | STACK: ").Append(ex.StackTrace.Replace(Environment.NewLine, " ⇢ "))
        End If

        Dim inner As Exception = ex.InnerException
        Dim depth As Integer = 0

        While inner IsNot Nothing AndAlso depth < 3
            sb.Append(" | INNER: ").Append(MaskSensitive(inner.Message))
            If Not String.IsNullOrWhiteSpace(inner.StackTrace) Then
                sb.Append(" | INNER_STACK: ").Append(inner.StackTrace.Replace(Environment.NewLine, " ⇢ "))
            End If
            inner = inner.InnerException
            depth += 1
        End While

        Return sb.ToString()

    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/02/2026
    ' EnsureLogFolder
    ' Crée %APPDATA%\Artefact\Logs si nécessaire.
    '------------------------------------------------------------
    Private Sub EnsureLogFolder()
        Dim folder As String = GetLogFolderPath()
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/02/2026
    ' PurgeOldLogs
    ' Supprime les logs plus vieux que RetentionDays.
    '------------------------------------------------------------
    Private Sub PurgeOldLogs()

        Dim folder As String = GetLogFolderPath()
        If Not Directory.Exists(folder) Then Exit Sub

        Dim limitDate As DateTime = DateTime.Now.Date.AddDays(-RetentionDays)

        For Each filePath In Directory.GetFiles(folder, "Artefact_*.log")
            Try
                Dim lastWrite = File.GetLastWriteTime(filePath)
                If lastWrite.Date < limitDate Then
                    File.Delete(filePath)
                End If
            Catch
                ' Volontaire : la purge ne doit jamais bloquer l'application (fichier verrouillé / droits).
            End Try
        Next

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/02/2026
    ' GetDailyLogFilePath
    '
    ' Retourne le chemin du fichier de log quotidien.
    '------------------------------------------------------------
    Private Function GetDailyLogFilePath() As String
        Dim folder As String = GetLogFolderPath()
        Dim fileName As String = $"Artefact_{DateTime.Now:yyyy-MM-dd}.log"
        Return Path.Combine(folder, fileName)
    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 23/02/2026
    ' GetLogFolderPath
    ' Source de vérité du chemin racine : ConfigLocalManager.GetArtefactFolderPath()
    '------------------------------------------------------------
    Private Function GetLogFolderPath() As String
        Dim root As String = ConfigLocalManager.GetArtefactFolderPath()
        Return Path.Combine(root, "Logs")
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/02/2026
    ' MaskSensitive
    '
    ' Masquage simple des secrets dans les messages de log.
    ' (Baseline prod : on protège au minimum Password/Pwd.)
    '------------------------------------------------------------
    Private Function MaskSensitive(input As String) As String
        If String.IsNullOrEmpty(input) Then Return input

        Dim s As String = input

        ' Masque "Password=xxxx" / "Pwd=xxxx" (insensible à la casse)
        s = ReplaceKeyValueInsensitive(s, "Password", "***")
        s = ReplaceKeyValueInsensitive(s, "Pwd", "***")

        Return s
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 23/02/2026
    ' ReplaceKeyValueInsensitive
    '
    ' Remplace les valeurs de type Key=Value (terminées par ; ou fin de string)
    ' Ex: Password=abc; -> Password=***;
    '------------------------------------------------------------
    Private Function ReplaceKeyValueInsensitive(text As String, key As String, replacement As String) As String

        Dim idx As Integer = 0
        Dim result As String = text

        While True
            Dim pos As Integer = result.IndexOf(key & "=", idx, StringComparison.OrdinalIgnoreCase)
            If pos < 0 Then Exit While

            Dim startVal As Integer = pos + key.Length + 1
            Dim endVal As Integer = result.IndexOf(";"c, startVal)

            If endVal < 0 Then
                ' Jusqu'à fin de string
                result = result.Substring(0, startVal) & replacement
                Exit While
            Else
                result = result.Substring(0, startVal) & replacement & result.Substring(endVal)
                idx = startVal + replacement.Length
            End If
        End While

        Return result

    End Function

    '------------------------------------------------------------
    ' 📌 V1.4 - 26/02/2026
    ' EnsureSessionHeader
    '
    ' Ecrit un séparateur au 1er log de l'exécution.
    ' Si le fichier de log change (ex: passage de minuit), ré-écrit un header.
    '------------------------------------------------------------
    Private Sub EnsureSessionHeader(logFile As String)

        If _sessionHeaderWritten AndAlso String.Equals(_sessionHeaderLogFile, logFile, StringComparison.OrdinalIgnoreCase) Then
            Exit Sub
        End If

        Dim sep As String = New String("-"c, 80)
        Dim ts As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim machine As String = Environment.MachineName
        Dim user As String = Environment.UserName

        Dim header As String =
        sep & Environment.NewLine &
        $"{ts} [SESSION] Démarrage application | Machine={machine} | User={user}" & Environment.NewLine &
        sep & Environment.NewLine

        File.AppendAllText(logFile, header, Encoding.UTF8)

        _sessionHeaderWritten = True
        _sessionHeaderLogFile = logFile

    End Sub

#End Region




End Module
