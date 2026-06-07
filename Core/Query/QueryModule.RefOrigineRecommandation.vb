Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

#Region "REF_ORIGINE_RECOMMANDATION - SQL"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RefOrigineRecommandation_SelectAll
    '
    '   • Retourne la liste complète des origines parents
    '------------------------------------------------------------
    Public ReadOnly Property RefOrigineRecommandation_SelectAll As String
        Get
            Return "SELECT id_origine_recommandation, code_origine_recommandation, libelle_origine_recommandation, ordre_affichage, is_actif " &
                   "FROM ref_origine_recommandation " &
                   "ORDER BY ordre_affichage, libelle_origine_recommandation;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RefOrigineRecommandation_SelectActifs
    '
    '   • Retourne uniquement les origines parents actives
    '------------------------------------------------------------
    Public ReadOnly Property RefOrigineRecommandation_SelectActifs As String
        Get
            Return "SELECT id_origine_recommandation, code_origine_recommandation, libelle_origine_recommandation, ordre_affichage, is_actif " &
                   "FROM ref_origine_recommandation " &
                   "WHERE is_actif = 1 " &
                   "ORDER BY ordre_affichage, libelle_origine_recommandation;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RefOrigineRecommandation_SelectBySearch
    '
    '   • Recherche les origines parents par libellé
    '------------------------------------------------------------
    Public ReadOnly Property RefOrigineRecommandation_SelectBySearch As String
        Get
            Return "SELECT id_origine_recommandation, code_origine_recommandation, libelle_origine_recommandation, ordre_affichage, is_actif " &
                   "FROM ref_origine_recommandation " &
                   "WHERE libelle_origine_recommandation LIKE @s " &
                   "ORDER BY ordre_affichage, libelle_origine_recommandation;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RefOrigineRecommandation_Insert
    '
    '   • Insère une nouvelle origine parent
    '------------------------------------------------------------
    Public ReadOnly Property RefOrigineRecommandation_Insert As String
        Get
            Return "INSERT INTO ref_origine_recommandation (libelle_origine_recommandation, ordre_affichage, is_actif) " &
                   "VALUES (@libelle_origine_recommandation, @ordre, @actif);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RefOrigineRecommandation_Update
    '
    '   • Met à jour une origine parent existante
    '------------------------------------------------------------
    Public ReadOnly Property RefOrigineRecommandation_Update As String
        Get
            Return "UPDATE ref_origine_recommandation " &
                   "SET libelle_origine_recommandation=@libelle_origine_recommandation, " &
                   "    ordre_affichage=@ordre, " &
                   "    is_actif=@actif " &
                   "WHERE id_origine_recommandation=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RefOrigineRecommandation_Delete
    '
    '   • Supprime une origine parent via id_origine_recommandation
    '------------------------------------------------------------
    Public ReadOnly Property RefOrigineRecommandation_Delete As String
        Get
            Return "DELETE FROM ref_origine_recommandation WHERE id_origine_recommandation=@id_origine_recommandation;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' RefOrigineRecommandation_SelectIdByLibelle
    '
    '   • Récupère l'identifiant d'une origine via son libellé
    '------------------------------------------------------------
    Public ReadOnly Property RefOrigineRecommandation_SelectIdByLibelle As String
        Get
            Return "SELECT id_origine_recommandation " &
                   "FROM ref_origine_recommandation " &
                   "WHERE libelle_origine_recommandation=@libelle_origine_recommandation " &
                   "ORDER BY id_origine_recommandation DESC " &
                   "LIMIT 1;"
        End Get
    End Property

#End Region

End Module
