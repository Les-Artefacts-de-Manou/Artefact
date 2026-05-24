'------------------------------------------------------------
' 📌 QueryModule.vb
' Version : V1.6
' Date    : 21/03/2026
' Auteur  : Joëlle
'
' Rôle :
' Centralisation des requêtes SQL utilisées par les modules métier.
' Le module expose uniquement les textes SQL ; aucune exécution ici.
'
' Règles Artefact :
' - Aucune logique UI.
' - Aucune exécution DB.
' - Les requêtes sont consommées par GestionReferentiel.
'
' Évolution :
' - V1.0 : socle initial des requêtes SQL.
' - V1.1 : enrichissement progressif des référentiels.
' - V1.2 : ajout des référentiels parent/enfant ref_enum_type / ref_enum.
' - V1.3 : ajout des contacts et éditeurs.
' - V1.4 : remise en ordre des propriétés par région et homogénéisation des en-têtes.
' - V1.5 : ajout gestion RichTextBox notes_editeurRtf et notes_editeurTxt
' - V1.6 : ajout gestion prixLit 
'------------------------------------------------------------

Option Strict On
Option Infer On
Option Explicit On

Public Partial Module QueryModule

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


#Region "RECOMMANDATION - SQL"

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_SelectByOrigine
    '
    '   • Retourne les recommandations enfants d'une origine parent
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectByOrigine As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
                   "FROM recommandations " &
                   "WHERE id_origine_recommandation = @id_origine_recommandation " &
                   "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_SelectByOrigine_ActifsOnly
    '
    '   • Retourne uniquement les recommandations actives d'une origine parent
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectByOrigine_ActifsOnly As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
                   "FROM recommandations " &
                   "WHERE id_origine_recommandation = @id_origine_recommandation AND is_actif = 1 " &
                   "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_SelectByOrigineAndSearch
    '
    '   • Recherche les recommandations d'une origine parent
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectByOrigineAndSearch As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, is_actif " &
                   "FROM recommandations " &
                   "WHERE id_origine_recommandation = @id_origine_recommandation " &
                   "  AND (source_nom LIKE @s OR source_login LIKE @s OR source_url LIKE @s OR commentaire LIKE @s) " &
                   "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_SelectByOrigineAndSearch_ActifsOnly
    '
    '   • Recherche les recommandations actives d'une origine parent
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectByOrigineAndSearch_ActifsOnly As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, is_actif " &
                   "FROM recommandations " &
                   "WHERE id_origine_recommandation = @id_origine_recommandation AND is_actif = 1 " &
                   "  AND (source_nom LIKE @s OR source_login LIKE @s OR source_url LIKE @s OR commentaire_txt LIKE @s) " &
                   "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.1 - 19/03/2026
    ' Recommandation_SelectByOrigineAndSearchIncludingNotes
    '
    '   • Recherche les recommandations d'une origine parent
    '   • Inclut le commentaire
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectByOrigineAndSearchIncludingNotes As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "WHERE id_origine_recommandation = @id_origine_recommandation " &
               "  AND (source_nom LIKE @s OR source_login LIKE @s OR source_url LIKE @s OR commentaire_txt LIKE @s) " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.1 - 19/03/2026
    ' Recommandation_SelectByOrigineAndSearchIncludingNotes_ActifsOnly
    '
    '   • Recherche les recommandations actives d'une origine parent
    '   • Inclut le commentaire
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectByOrigineAndSearchIncludingNotes_ActifsOnly As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "WHERE id_origine_recommandation = @id_origine_recommandation AND is_actif = 1 " &
               "  AND (source_nom LIKE @s OR source_login LIKE @s OR source_url LIKE @s OR commentaire_txt LIKE @s) " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_SelectAll
    '
    '   • Retourne toutes les recommandations
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectAll As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_SelectAll_ActifsOnly
    '
    '   • Retourne toutes les recommandations actives
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectAll_ActifsOnly As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "WHERE is_actif = 1 " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_SelectBySearch
    '
    '   • Recherche globale hors commentaire
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectBySearch As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "WHERE source_nom LIKE @search OR source_login LIKE @search OR source_url LIKE @search " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_SelectBySearch_ActifsOnly
    '
    '   • Recherche globale active hors commentaire
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectBySearch_ActifsOnly As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "WHERE is_actif = 1 " &
               "  AND (source_nom LIKE @search OR source_login LIKE @search OR source_url LIKE @search) " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_SelectBySearchIncludingNotes
    '
    '   • Recherche globale avec commentaire
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectBySearchIncludingNotes As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "WHERE source_nom LIKE @search OR source_login LIKE @search OR source_url LIKE @search OR commentaire_txt LIKE @search " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.2 - 19/03/2026
    ' Recommandation_SelectBySearchIncludingNotes_ActifsOnly
    '
    '   • Recherche globale active avec commentaire
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectBySearchIncludingNotes_ActifsOnly As String
        Get
            Return "SELECT id_recommandation, code_recommandation, id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif " &
               "FROM recommandations " &
               "WHERE is_actif = 1 " &
               "  AND (source_nom LIKE @search OR source_login LIKE @search OR source_url LIKE @search OR commentaire_txt LIKE @search) " &
               "ORDER BY date_recommandation DESC, id_recommandation DESC;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_Insert
    '
    '   • Insère une nouvelle recommandation enfant
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_Insert As String
        Get
            Return "INSERT INTO recommandations (id_origine_recommandation, source_nom, source_login, source_url, date_recommandation, commentaire_rtf, commentaire_txt, is_actif) " &
                   "VALUES (@id_origine_recommandation, @source_nom, @source_login, @source_url, @date_recommandation, @commentaire_rtf, @commentaire_txt, @actif);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_Update
    '
    '   • Met à jour une recommandation enfant existante
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_Update As String
        Get
            Return "UPDATE recommandations " &
                   "SET id_origine_recommandation=@id_origine_recommandation, " &
                   "    source_nom=@source_nom, " &
                   "    source_login=@source_login, " &
                   "    source_url=@source_url, " &
                   "    date_recommandation=@date_recommandation, " &
               "    commentaire_rtf=@commentaire_rtf, " &
               "    commentaire_txt=@commentaire_txt, " &
                   "    is_actif=@actif " &
                   "WHERE id_recommandation=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_Delete
    '
    '   • Supprime une recommandation enfant via id_recommandation
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_Delete As String
        Get
            Return "DELETE FROM recommandations WHERE id_recommandation=@id_recommandation;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_SelectLastId
    '
    '   • Récupère le dernier identifiant inséré
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_SelectLastId As String
        Get
            Return "SELECT MAX(id_recommandation) FROM recommandations;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_CountByOrigine
    '
    '   • Compte le nombre de recommandations enfants rattachées à une origine parent
    '------------------------------------------------------------
    Public Function Recommandation_CountByOrigine() As String
        Return "
SELECT COUNT(*)
FROM recommandations
WHERE id_origine_recommandation = @id_origine_recommandation;"
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_DeleteByOrigine
    '
    '   • Supprime toutes les recommandations enfants d'une origine parent
    '------------------------------------------------------------
    Public Function Recommandation_DeleteByOrigine() As String
        Return "
DELETE FROM recommandations
WHERE id_origine_recommandation = @id_origine_recommandation;"
    End Function

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_CountUsageInLivres
    '
    '   • Compte les usages d'une recommandation dans livres_recommandations
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_CountUsageInLivres As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_recommandations
WHERE id_recommandation = @id_recommandation;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 18/03/2026
    ' Recommandation_CountUsageInStaging
    '
    '   • Compte les usages d'une recommandation dans livres_staging_recommandations
    '------------------------------------------------------------
    Public ReadOnly Property Recommandation_CountUsageInStaging As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_staging_recommandations
WHERE id_recommandation = @id_recommandation;"
        End Get
    End Property

#End Region

End Module
