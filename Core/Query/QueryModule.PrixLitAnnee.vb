Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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
