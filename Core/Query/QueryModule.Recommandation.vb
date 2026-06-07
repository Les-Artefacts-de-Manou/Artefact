Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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
