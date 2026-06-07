''' <summary>
''' Interface pour les UserControls qui doivent recevoir un contexte partagé
''' Implémentée par tous les UC_* de l'application
''' </summary>
Public Interface IContextAwareUserControl
    ''' <summary>
    ''' Injecte le contexte partagé dans le UserControl
    ''' Appelé par le conteneur (portail) lors du chargement de l'UC
    ''' </summary>
    Sub SetContext(context As UserControlContext)

    ''' <summary>
    ''' Appelé quand l'UC est activé (affiché)
    ''' Permet de rafraîchir les données ou de mettre à jour le contexte
    ''' </summary>
    Sub OnActivated()

    ''' <summary>
    ''' Appelé quand l'UC est désactivé (caché ou remplacé)
    ''' Permet de nettoyer les ressources temporaires
    ''' </summary>
    Sub OnDeactivated()
End Interface
