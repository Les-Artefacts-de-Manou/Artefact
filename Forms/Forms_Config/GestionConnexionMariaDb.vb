'------------------------------------------------------------
' 📌 GestionConnexionMariaDB.vb - Form
' Version : V1.2
' Date    : 26/02/2026
' Auteur  : Joëlle
'
' Rôle :
' - Gestion de la connexion MariaDB
' - Formulaire de gestion de la connexion MariaDB.
'- Gestion de la configuration locale de la base de données, lecture/écriture du fichier JSON, chiffrement/déchiffrement DPAPI.
'------------------------------------------------------------

Option Strict On
Option Infer On
Option Explicit On

Imports MySqlConnector
Public Class GestionConnexionMariaDb

#Region "Variables et Enum"

    '------------------------------------------------------------
    ' 📌 V1.1 - 21/02/2026
    ' États internes de la form
    '------------------------------------------------------------

    Private Enum PasswordMode
        KeepExisting = 0
        SetNew = 1
    End Enum

    Private _cfg As LocalDbConfig = Nothing
    Private _modeCreation As Boolean = False
    Private _pwMode As PasswordMode = PasswordMode.KeepExisting

#End Region

#Region "Load / Init"

    '------------------------------------------------------------
    ' 📌 V1.2 - 26/02/2026
    ' GestionConnexionMariaDb_Load
    '
    ' Charge la config locale si elle existe et pré-remplit les champs.
    ' Détermine le mode création et le mode password.
    '------------------------------------------------------------
    Private Sub GestionConnexionMariaDb_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            lblTitreForm.Text = "Gestion connexion MariaDB"

            GestionLog.EcrireLog("UI: ouverture GestionConnexionMariaDb.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)

            _cfg = ConfigLocalManager.LireConfigDb()
            _modeCreation = (_cfg Is Nothing)

            If _modeCreation Then
                _cfg = New LocalDbConfig()
                stsLabelStatus.Text = "Configuration absente : veuillez encoder les paramètres."
                _pwMode = PasswordMode.SetNew
                txtPassword.Enabled = True

                GestionLog.EcrireLog("UI: mode création config DB.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)
            Else
                stsLabelStatus.Text = "Configuration chargée."
                _pwMode = PasswordMode.KeepExisting
                txtPassword.Enabled = False

                GestionLog.EcrireLog("UI: config DB locale chargée.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)
            End If

            RemplirChampsDepuisConfig(_cfg)
            ' Appliquer l'état UI password (visible/masqué) dans tous les cas
            UpdatePasswordUiState()



            ' Toujours vider le champ password à l'affichage
            txtPassword.Text = ""

        Catch ex As Exception
            stsLabelStatus.Text = "Erreur au chargement de la configuration."
            GestionLog.EcrireLog("UI: erreur Load GestionConnexionMariaDb.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            ' Point critique : la form ne peut pas fonctionner correctement
            MessageBox.Show(
            "Erreur au chargement de la configuration." & Environment.NewLine & ex.Message,
            "Artefact",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Try

    End Sub

#End Region

#Region "Mapping, Config, Validation"

    '------------------------------------------------------------
    ' 📌 V1.0 - 20/02/2026
    ' RemplirChampsDepuisConfig
    '------------------------------------------------------------
    Private Sub RemplirChampsDepuisConfig(cfg As LocalDbConfig)

        txtHost.Text = cfg.Host
        nudPort.Value = If(cfg.Port > 0 AndAlso cfg.Port <= 65535, cfg.Port, 3306)
        txtDatabase.Text = cfg.Database
        txtUserName.Text = cfg.UserName
        txtOptionsConn.Text = cfg.OptionsConn

        ' On ne déchiffre pas automatiquement le password pour l’afficher.
        ' (sinon un screen-shot = fuite de secret)
        txtPassword.Text = ""

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 26/02/2026
    ' BuildConfigFromUI
    '
    ' Construit la configuration à partir des champs écran.
    ' - Normalise Host (localhost -> 127.0.0.1)
    ' - Assainit Port et OptionsConn
    ' - Password géré via PasswordMode (KeepExisting / SetNew)
    '
    ' ⚠️ Aucun log ici (fonction appelée souvent).
    '------------------------------------------------------------
    Private Function BuildConfigFromUI() As LocalDbConfig

        Dim host As String = txtHost.Text.Trim()
        If host.Equals("localhost", StringComparison.OrdinalIgnoreCase) Then
            host = "127.0.0.1"
        End If

        Dim port As Integer = CInt(nudPort.Value)
        If port <= 0 OrElse port > 65535 Then
            port = 3306
        End If

        Dim optionsConn As String = txtOptionsConn.Text.Trim()
        If optionsConn <> "" AndAlso Not optionsConn.EndsWith(";", StringComparison.Ordinal) Then
            optionsConn &= ";"
        End If

        Dim cfg As New LocalDbConfig With {
        .EnvCode = "LOCAL",
        .NomConnexion = "MariaDB_Artefact",
        .Host = host,
        .Port = port,
        .Database = txtDatabase.Text.Trim(),
        .UserName = txtUserName.Text.Trim(),
        .OptionsConn = optionsConn
    }

        If _modeCreation OrElse _pwMode = PasswordMode.SetNew Then

            If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                Throw New Exception("Mot de passe requis (création ou modification).")
            End If

            cfg.PasswordEncB64 = CryptoManagerDPAPI.EncryptStringToBase64(txtPassword.Text)

        Else
            ' KeepExisting : doit réellement exister
            Dim existing As String = If(_cfg IsNot Nothing, _cfg.PasswordEncB64, "")
            If String.IsNullOrWhiteSpace(existing) Then
                Throw New Exception("Aucun mot de passe existant. Clique sur 'Modifier mot de passe'.")
            End If
            cfg.PasswordEncB64 = existing
        End If

        Return cfg

    End Function

    '------------------------------------------------------------
    ' 📌 V1.1 - 26/02/2026
    ' ValiderConfig
    '
    ' Valide les champs indispensables d'une config.
    ' - Affiche une erreur dans le status + focus sur le contrôle fautif
    ' - Pas de MsgBox
    ' - Log UI (Rapide) uniquement si KO
    '------------------------------------------------------------
    Private Function ValiderConfig(cfg As LocalDbConfig) As Boolean

        If cfg Is Nothing Then
            stsLabelStatus.Text = "Configuration invalide (Nothing)."
            GestionLog.EcrireLog("UI: validation KO (cfg Nothing).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
            Return False
        End If

        If String.IsNullOrWhiteSpace(cfg.Host) Then
            stsLabelStatus.Text = "Host manquant."
            txtHost.Focus()
            GestionLog.EcrireLog("UI: validation KO (Host manquant).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
            Return False
        End If

        If cfg.Port <= 0 OrElse cfg.Port > 65535 Then
            stsLabelStatus.Text = "Port invalide."
            nudPort.Focus()
            GestionLog.EcrireLog("UI: validation KO (Port invalide).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
            Return False
        End If

        If String.IsNullOrWhiteSpace(cfg.Database) Then
            stsLabelStatus.Text = "Nom de base manquant."
            txtDatabase.Focus()
            GestionLog.EcrireLog("UI: validation KO (Database manquante).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
            Return False
        End If

        If String.IsNullOrWhiteSpace(cfg.UserName) Then
            stsLabelStatus.Text = "Utilisateur manquant."
            txtUserName.Focus()
            GestionLog.EcrireLog("UI: validation KO (User manquant).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
            Return False
        End If

        ' Optionnel : éviter des valeurs "dangereuses" côté host
        ' (Tu peux enlever si tu veux autoriser tout)
        If cfg.Host.Contains(" ", StringComparison.Ordinal) Then
            stsLabelStatus.Text = "Host invalide (espaces)."
            txtHost.Focus()
            GestionLog.EcrireLog("UI: validation KO (Host invalide).", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)
            Return False
        End If

        Return True

    End Function

    '------------------------------------------------------------
    ' 📌 V1.2 - 26/02/2026
    ' btnTester_Click
    '
    ' Teste la connexion avec EXACTEMENT les valeurs à l’écran.
    '------------------------------------------------------------
    Private Sub btnTester_Click(sender As Object, e As EventArgs) Handles btnTester.Click

        Try
            Dim cfgTest As LocalDbConfig = BuildConfigFromUI()

            If Not ValiderConfig(cfgTest) Then
                GestionLog.EcrireLog("UI: validation config KO (test connexion).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)
                Exit Sub
            End If

            DatabaseManager.TestConnexionMariaDb(cfgTest)

            stsLabelStatus.Text = "✅ Connexion OK."
            GestionLog.EcrireLog("UI: test connexion OK.", GestionLog.LogLevel.Rapide, GestionLog.LogCategory.UI)

            ' Règle : pas de MessageBox en cas OK

        Catch ex As Exception
            stsLabelStatus.Text = "❌ Connexion KO."
            GestionLog.EcrireLog("UI: test connexion KO.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            ' Point utile pour l'utilisateur : message (mais pas obligatoire)
            MessageBox.Show(
            "Connexion KO ❌" & Environment.NewLine & ex.Message,
            "Test connexion",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )
        End Try

    End Sub

    '------------------------------------------------------------
    '📌 V1.2 - 26/02/2026
    ' btnEnregistrer_Click
    '
    ' Enregistre uniquement si la connexion est OK avec les valeurs écran.
    ' Si KO : ne sauvegarde pas et ne ferme pas la fenêtre.
    '------------------------------------------------------------
    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click

        Try
            Dim cfgSave As LocalDbConfig = BuildConfigFromUI()

            If Not ValiderConfig(cfgSave) Then
                GestionLog.EcrireLog("UI: validation config KO (enregistrement).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)
                Exit Sub
            End If

            ' Test obligatoire sur les valeurs écran
            DatabaseManager.TestConnexionMariaDb(cfgSave)

            ' Si OK -> sauvegarde
            ConfigLocalManager.SauvegarderConfigDb(cfgSave)

            ' Adoption de la config comme référence
            _cfg = cfgSave
            _modeCreation = False
            _pwMode = PasswordMode.KeepExisting
            txtPassword.Text = ""
            txtPassword.Enabled = False

            stsLabelStatus.Text = "✅ Enregistré + connexion OK."

            GestionLog.EcrireLog("UI: enregistrement config OK.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            stsLabelStatus.Text = "❌ Enregistrement refusé : connexion KO."

            GestionLog.EcrireLog("UI: enregistrement refusé (connexion KO ou sauvegarde KO).", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            ' Point critique : l'utilisateur s'attend à comprendre pourquoi ça ne se ferme pas
            MessageBox.Show(
                "Enregistrement refusé ❌" & Environment.NewLine &
                "La connexion échoue avec les valeurs actuelles." & Environment.NewLine &
                "Corrige (ou Annule) puis réessaie." & Environment.NewLine & Environment.NewLine &
                ex.Message,
                "Enregistrer",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    '------------------------------------------------------------
    ' 📌 V1.2 - 26/02/2026
    ' btnModifierMotDePasse_Click
    '
    ' Active explicitement le changement de mot de passe.
    '------------------------------------------------------------
    Private Sub btnModifierMotDePasse_Click(sender As Object, e As EventArgs) Handles btnModifierMotDePasse.Click

        _pwMode = PasswordMode.SetNew
        txtPassword.Enabled = True
        txtPassword.Text = ""
        txtPassword.Focus()
        stsLabelStatus.Text = "Saisis le nouveau mot de passe puis teste/enregistre."

        HidePassword()
        UpdatePasswordUiState()

        GestionLog.EcrireLog("UI: activation modification mot de passe.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.1 - 26/02/2026
    ' UpdatePasswordUiState
    '
    ' Gère visibilité bouton et état masque.
    '------------------------------------------------------------
    Private Sub UpdatePasswordUiState()

        Dim showButton As Boolean = (_pwMode = PasswordMode.SetNew)

        btnShowPassword.Visible = showButton
        HidePassword()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 26/02/2026
    ' btnShowPassword_MouseDown
    '------------------------------------------------------------
    Private Sub btnShowPassword_MouseDown(sender As Object, e As MouseEventArgs) Handles btnShowPassword.MouseDown

        ShowPassword()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 26/02/2026
    ' btnShowPassword_MouseUp
    '------------------------------------------------------------
    Private Sub btnShowPassword_MouseUp(sender As Object, e As MouseEventArgs) Handles btnShowPassword.MouseUp

        HidePassword()

    End Sub

    Private Sub btnShowPassword_MouseLeave(sender As Object, e As EventArgs) Handles btnShowPassword.MouseLeave

        HidePassword()

    End Sub

    '------------------------------------------------------------
    ' 📌 V1.0 - 26/02/2026
    ' ShowPassword / HidePassword
    '
    ' Affiche/masque le mot de passe en gérant les 2 mécanismes WinForms.
    '------------------------------------------------------------
    Private Sub ShowPassword()
        txtPassword.UseSystemPasswordChar = False
        txtPassword.PasswordChar = ControlChars.NullChar
    End Sub

    Private Sub HidePassword()
        txtPassword.UseSystemPasswordChar = True
        ' Optionnel : si tu veux forcer l'étoile plutôt que le caractère système
        ' txtPassword.PasswordChar = "*"c
    End Sub

#End Region

End Class