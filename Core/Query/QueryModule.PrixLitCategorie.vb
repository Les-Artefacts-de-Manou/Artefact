Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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

End Module
