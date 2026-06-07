'====================================================================
' 📌 DialogChoix.vb
' Version : V1.0
' Date    : 24/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Form de dialogue personnalisée avec boutons configurables.
' Remplace les MessageBox à 3 choix déroutants (Yes/No/Cancel).
'
' Utilisation :
' Dim dlg As New DialogChoix()
' dlg.Titre = "Suppression avec dépendances"
' dlg.Message = "Ce type contient 5 valeurs associées."
' dlg.SetBoutons("Voir les valeurs", "Supprimer tout", "Annuler")
' Dim result As DialogResult = dlg.ShowDialog()
' If result = DialogResult.Yes Then ' Bouton 1
' ElseIf result = DialogResult.No Then ' Bouton 2
' Else ' DialogResult.Cancel (Bouton 3 ou fermeture)
'
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class DialogChoix

#Region "Propriétés"

    ''' <summary>
    ''' Titre de la boîte de dialogue
    ''' </summary>
    Public Property Titre As String = "Confirmation"

    ''' <summary>
    ''' Message principal affiché
    ''' </summary>
    Public Property Message As String = ""

#End Region

#Region "Constructeur"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Configuration"

    ''' <summary>
    ''' Configure les 3 boutons avec des textes personnalisés.
    ''' </summary>
    Public Sub SetBoutons(bouton1 As String, bouton2 As String, bouton3 As String)
        btn1.Text = bouton1
        btn2.Text = bouton2
        btn3.Text = bouton3
    End Sub

    ''' <summary>
    ''' Configure seulement 2 boutons (le 3ème est masqué).
    ''' </summary>
    Public Sub SetBoutons(bouton1 As String, bouton2 As String)
        btn1.Text = bouton1
        btn2.Text = bouton2
        btn3.Visible = False
    End Sub

#End Region

#Region "Affichage"

    Private Sub DialogChoix_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Titre
        lblMessage.Text = Message
    End Sub

#End Region

#Region "Actions boutons"

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class
