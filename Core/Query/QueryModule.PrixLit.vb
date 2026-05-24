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

#Region "PRIXLIT - SQL"

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_SelectAll
    '
    '   • Retourne la liste complète des prix littéraires
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_SelectAll As String
        Get
            Return "SELECT id_prixLit, code_prixLit, nom_prixLit, description_prixLit, Notes_PrixLit_rtf, Notes_PrixLit_txt, is_actif " &
               "FROM prixlit " &
               "ORDER BY nom_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_SelectAll_ActifsOnly
    '
    '   • Retourne uniquement les prix littéraires actifs
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_SelectAll_ActifsOnly As String
        Get
            Return "SELECT id_prixLit, code_prixLit, nom_prixLit, description_prixLit, Notes_PrixLit_rtf, Notes_PrixLit_txt, is_actif " &
               "FROM prixlit " &
               "WHERE is_actif = 1 " &
               "ORDER BY nom_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_SelectBySearch
    '
    '   • Recherche les prix littéraires par nom, description ou code
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_SelectBySearch As String
        Get
            Return "SELECT id_prixLit, code_prixLit, nom_prixLit, description_prixLit, Notes_PrixLit_rtf, Notes_PrixLit_txt, is_actif " &
               "FROM prixlit " &
               "WHERE nom_prixLit LIKE @search " &
               "   OR description_prixLit LIKE @search " &
               "   OR code_prixLit LIKE @search " &
               "ORDER BY nom_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_SelectBySearch_ActifsOnly
    '
    '   • Recherche les prix littéraires actifs par nom, description ou code
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_SelectBySearch_ActifsOnly As String
        Get
            Return "SELECT id_prixLit, code_prixLit, nom_prixLit, description_prixLit, Notes_PrixLit_rtf, Notes_PrixLit_txt, is_actif " &
               "FROM prixlit " &
               "WHERE is_actif = 1 " &
               "  AND (nom_prixLit LIKE @search " &
               "       OR description_prixLit LIKE @search " &
               "       OR code_prixLit LIKE @search) " &
               "ORDER BY nom_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.1 - 21/03/2026
    ' PrixLit_SelectBySearchIncludingNotes
    '
    '   • Recherche les prix littéraires par nom, description, code ou notes
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_SelectBySearchIncludingNotes As String
        Get
            Return "SELECT id_prixLit, code_prixLit, nom_prixLit, description_prixLit, Notes_PrixLit_rtf, Notes_PrixLit_txt, is_actif " &
               "FROM prixlit " &
               "WHERE nom_prixLit LIKE @search " &
               "   OR description_prixLit LIKE @search " &
               "   OR code_prixLit LIKE @search " &
               "   OR Notes_PrixLit_txt LIKE @search " &
               "ORDER BY nom_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.1 - 21/03/2026
    ' PrixLit_SelectBySearchIncludingNotes_ActifsOnly
    '
    '   • Recherche les prix littéraires actifs par nom, description, code ou notes
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_SelectBySearchIncludingNotes_ActifsOnly As String
        Get
            Return "SELECT id_prixLit, code_prixLit, nom_prixLit, description_prixLit, Notes_PrixLit_rtf, Notes_PrixLit_txt, is_actif " &
               "FROM prixlit " &
               "WHERE is_actif = 1 " &
               "  AND (nom_prixLit LIKE @search " &
               "       OR description_prixLit LIKE @search " &
               "       OR code_prixLit LIKE @search " &
               "       OR Notes_PrixLit_txt LIKE @search) " &
               "ORDER BY nom_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_Insert
    '
    '   • Ajout d'un prix littéraire
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_Insert As String
        Get
            Return "INSERT INTO prixlit (nom_prixLit, description_prixLit, Notes_PrixLit_rtf, Notes_PrixLit_txt, is_actif) " &
               "VALUES (@nom_prixLit, @description_prixLit, @Notes_PrixLit_rtf, @Notes_PrixLit_txt, @is_actif);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_Update
    '
    '   • Mise à jour d'un prix littéraire
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_Update As String
        Get
            Return "UPDATE prixlit " &
               "SET nom_prixLit = @nom_prixLit, " &
               "    description_prixLit = @description_prixLit, " &
               "    Notes_PrixLit_rtf = @Notes_PrixLit_rtf, " &
               "    Notes_PrixLit_txt = @Notes_PrixLit_txt, " &
               "    is_actif = @is_actif " &
               "WHERE id_prixLit = @id_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_Delete
    '
    '   • Suppression d'un prix littéraire
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_Delete As String
        Get
            Return "DELETE FROM prixlit WHERE id_prixLit = @id_prixLit;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLit_CountCategories
    '
    '   • Compte les catégories liées au prix littéraire
    '------------------------------------------------------------
    Public ReadOnly Property PrixLit_CountCategories As String
        Get
            Return "SELECT COUNT(*) FROM prixlit_categorie WHERE id_prixLit = @id_prixLit;"
        End Get
    End Property

#End Region


#Region "PRIXLIT_CATEGORIE - SQL"

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitCategorie_SelectByPrixLit
    '
    '   • Retourne les catégories d’un prix littéraire
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitCategorie_SelectByPrixLit As String
        Get
            Return "
            SELECT
                pc.id_prixlit_categorie,
                pc.id_prixLit,
                pc.libelle_categorie,
                pc.description_categorie,
                pc.ordre_affichage,
                pc.is_actif,
                pc.created_at,
                pc.updated_at,
                pc.code_prixlit_categorie
            FROM prixlit_categorie pc
            WHERE pc.id_prixLit = @id_prixLit
            ORDER BY pc.ordre_affichage, pc.libelle_categorie;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitCategorie_SelectBySearch
    '
    '   • Recherche dans les catégories d’un prix
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitCategorie_SelectBySearch As String
        Get
            Return "
            SELECT
                pc.id_prixlit_categorie,
                pc.id_prixLit,
                pc.libelle_categorie,
                pc.description_categorie,
                pc.ordre_affichage,
                pc.is_actif,
                pc.created_at,
                pc.updated_at,
                pc.code_prixlit_categorie
            FROM prixlit_categorie pc
            WHERE
                pc.id_prixLit = @id_prixLit
                AND (
                    pc.libelle_categorie LIKE @search
                    OR pc.description_categorie LIKE @search
                    OR pc.code_prixlit_categorie LIKE @search
                )
            ORDER BY pc.ordre_affichage, pc.libelle_categorie;"
        End Get
    End Property



    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitCategorie_Insert
    '
    '   • Ajout d'une catégorie pour un prix
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitCategorie_Insert As String
        Get
            Return "
            INSERT INTO prixlit_categorie (
                id_prixLit,
                libelle_categorie,
                description_categorie,
                ordre_affichage,
                is_actif
            )
            VALUES (
                @id_prixLit,
                @libelle_categorie,
                @description_categorie,
                @ordre_affichage,
                @is_actif
            );"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitCategorie_Update
    '
    '   • Mise à jour d'une catégorie
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitCategorie_Update As String
        Get
            Return "
            UPDATE prixlit_categorie
            SET
                libelle_categorie = @libelle_categorie,
                description_categorie = @description_categorie,
                ordre_affichage = @ordre_affichage,
                is_actif = @is_actif
            WHERE id_prixlit_categorie = @id_prixlit_categorie;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitCategorie_Delete
    '
    '   • Suppression d'une catégorie
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitCategorie_Delete As String
        Get
            Return "
            DELETE FROM prixlit_categorie
            WHERE id_prixlit_categorie = @id_prixlit_categorie;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitCategorie_CountAnnees
    '
    '   • Compte les années liées (contrôle suppression)
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitCategorie_CountAnnees As String
        Get
            Return "
            SELECT COUNT(*)
            FROM prixlit_annee
            WHERE id_prixlit_categorie = @id_prixlit_categorie;"
        End Get
    End Property

#End Region


#Region "PRIXLIT_ANNEE - SQL"

    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitAnnee_SelectByCategorie
    '
    '   • Retourne les années pour une catégorie donnée
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitAnnee_SelectByCategorie As String
        Get
            Return "
            SELECT
                pa.id_prixLit_Annee,
                pa.id_prixlit_categorie,
                pa.annee,
                pa.created_at,
                pa.updated_at,
                pa.code_prixLit_Annee,

                pc.libelle_categorie,
                pc.id_prixLit

            FROM prixlit_annee pa
            INNER JOIN prixlit_categorie pc
                ON pc.id_prixlit_categorie = pa.id_prixlit_categorie
            WHERE pa.id_prixlit_categorie = @id_prixlit_categorie
            ORDER BY pa.annee DESC;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitAnnee_SelectByPrixLit
    '
    '   • Retourne toutes les années d’un prix (toutes catégories)
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitAnnee_SelectByPrixLit As String
        Get
            Return "
            SELECT
                pa.id_prixLit_Annee,
                pa.id_prixlit_categorie,
                pa.annee,
                pa.created_at,
                pa.updated_at,
                pa.code_prixLit_Annee,

                pc.libelle_categorie,
                pc.ordre_affichage,

                pl.nom_prixLit

            FROM prixlit_annee pa
            INNER JOIN prixlit_categorie pc
                ON pc.id_prixlit_categorie = pa.id_prixlit_categorie
            INNER JOIN prixlit pl
                ON pl.id_prixLit = pc.id_prixLit
            WHERE pc.id_prixLit = @id_prixLit
            ORDER BY pa.annee DESC, pc.ordre_affichage;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitAnnee_SelectBySearch
    '
    '   • Recherche dans les années d’un prix
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitAnnee_SelectBySearch As String
        Get
            Return "
            SELECT
                pa.id_prixLit_Annee,
                pa.id_prixlit_categorie,
                pa.annee,
                pa.created_at,
                pa.updated_at,
                pa.code_prixLit_Annee,

                pc.libelle_categorie,
                pc.ordre_affichage,

                pl.nom_prixLit

            FROM prixlit_annee pa
            INNER JOIN prixlit_categorie pc
                ON pc.id_prixlit_categorie = pa.id_prixlit_categorie
            INNER JOIN prixlit pl
                ON pl.id_prixLit = pc.id_prixLit
            WHERE
                pc.id_prixLit = @id_prixLit
                AND (
                    CAST(pa.annee AS CHAR) LIKE @search
                    OR pc.libelle_categorie LIKE @search
                    OR pa.code_prixLit_Annee LIKE @search
                )
            ORDER BY pa.annee DESC, pc.ordre_affichage;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitAnnee_Insert
    '
    '   • Ajout d'une année pour une catégorie
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitAnnee_Insert As String
        Get
            Return "
            INSERT INTO prixlit_annee (
                id_prixlit_categorie,
                annee
            )
            VALUES (
                @id_prixlit_categorie,
                @annee
            );"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitAnnee_Update
    '
    '   • Mise à jour d'une année
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitAnnee_Update As String
        Get
            Return "
            UPDATE prixlit_annee
            SET
                id_prixlit_categorie = @id_prixlit_categorie,
                annee = @annee
            WHERE id_prixLit_Annee = @id_prixLit_Annee;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitAnnee_Delete
    '
    '   • Suppression d'une année
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitAnnee_Delete As String
        Get
            Return "
            DELETE FROM prixlit_annee
            WHERE id_prixLit_Annee = @id_prixLit_Annee;"
        End Get
    End Property


    '------------------------------------------------------------
    ' 📌 V1.0 - 21/03/2026
    ' PrixLitAnnee_CountLivres
    '
    '   • Compte les livres liés (contrôle suppression)
    '------------------------------------------------------------
    Public ReadOnly Property PrixLitAnnee_CountLivres As String
        Get
            Return "
            SELECT COUNT(*)
            FROM livres_prixlit_annee
            WHERE id_prixLit_Annee = @id_prixLit_Annee;"
        End Get
    End Property



#End Region

End Module
