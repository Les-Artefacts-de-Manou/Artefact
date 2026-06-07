'====================================================================
' 📌 UC_Accueil.vb
' Version : V1.0
' Date    : 23/03/2026
' Auteur  : Joëlle
'
' Rôle :
' UserControl d'accueil du portail des référentiels.
' Affiche un message de bienvenue et des statistiques (à venir).
'
' Évolution :
' - V1.0 : Création de la page d'accueil
'====================================================================

Option Strict On
Option Explicit On

Imports System.Windows.Forms

Public Class UC_Accueil
    Implements IContextAwareUserControl

#Region "Déclarations"

    ' Contexte partagé
    Private _context As UserControlContext = Nothing

#End Region

#Region "Constructeur"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "IContextAwareUserControl Implementation"

    Public Sub SetContext(context As UserControlContext) Implements IContextAwareUserControl.SetContext
        _context = context
    End Sub

    Public Sub OnActivated() Implements IContextAwareUserControl.OnActivated
        Try
            If _context IsNot Nothing Then
                _context.SetContextDisplay("Portail Référentiels > Accueil")
                _context.SetStatus("Bienvenue dans le portail des référentiels.")
            End If
        Catch ex As Exception
            GestionLog.EcrireLog(
                "UI: erreur activation UC_Accueil.",
                GestionLog.LogLevel.Succinct,
                GestionLog.LogCategory.UI,
                ex
            )
        End Try
    End Sub

    Public Sub OnDeactivated() Implements IContextAwareUserControl.OnDeactivated
        ' Rien à faire pour l'accueil
    End Sub

#End Region

End Class
