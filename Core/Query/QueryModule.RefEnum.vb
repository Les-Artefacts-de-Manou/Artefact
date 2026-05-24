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

#Region "REF_ENUM_TYPE - SQL"

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnumType_SelectAll
    '
    '   • Retourne la liste complète des types parents ref_enum_type
    '------------------------------------------------------------
    Public ReadOnly Property RefEnumType_SelectAll As String
        Get
            Return "SELECT id_enum_type, code_enum_type, code_type, libelle_type, ordre_affichage, is_actif " &
                   "FROM ref_enum_type " &
                   "ORDER BY ordre_affichage, libelle_type;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnumType_SelectActifs
    '
    '   • Retourne uniquement les types parents actifs
    '------------------------------------------------------------
    Public ReadOnly Property RefEnumType_SelectActifs As String
        Get
            Return "SELECT id_enum_type, code_enum_type, code_type, libelle_type, ordre_affichage, is_actif " &
                   "FROM ref_enum_type " &
                   "WHERE is_actif = 1 " &
                   "ORDER BY ordre_affichage, libelle_type;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnumType_SelectBySearch
    '
    '   • Recherche les types parents par code ou libellé
    '------------------------------------------------------------
    Public ReadOnly Property RefEnumType_SelectBySearch As String
        Get
            Return "SELECT id_enum_type, code_enum_type, code_type, libelle_type, ordre_affichage, is_actif " &
                   "FROM ref_enum_type " &
                   "WHERE code_type LIKE @s OR libelle_type LIKE @s " &
                   "ORDER BY ordre_affichage, libelle_type;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnumType_Insert
    '
    '   • Insère un nouveau type parent ref_enum_type
    '------------------------------------------------------------
    Public ReadOnly Property RefEnumType_Insert As String
        Get
            Return "INSERT INTO ref_enum_type (code_type, libelle_type, ordre_affichage, is_actif) " &
                   "VALUES (@code_type, @libelle_type, @ordre, @actif);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnumType_Update
    '
    '   • Met à jour un type parent existant
    '------------------------------------------------------------
    Public ReadOnly Property RefEnumType_Update As String
        Get
            Return "UPDATE ref_enum_type " &
                   "SET code_type=@code_type, " &
                   "    libelle_type=@libelle_type, " &
                   "    ordre_affichage=@ordre, " &
                   "    is_actif=@actif " &
                   "WHERE id_enum_type=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnumType_Delete
    '
    '   • Supprime un type parent via id_enum_type
    '------------------------------------------------------------
    Public ReadOnly Property RefEnumType_Delete As String
        Get
            Return "DELETE FROM ref_enum_type WHERE id_enum_type=@id_enum_type;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnumType_SelectIdByCodeType
    '
    '   • Récupère l'identifiant d'un type parent via code_type
    '------------------------------------------------------------
    Public ReadOnly Property RefEnumType_SelectIdByCodeType As String
        Get
            Return "SELECT id_enum_type FROM ref_enum_type WHERE code_type=@code_type;"
        End Get
    End Property

#End Region


#Region "REF_ENUM (VALEURS) - SQL"

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_SelectByType
    '
    '   • Retourne les valeurs enfants d'un type parent
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_SelectByType As String
        Get
            Return "SELECT id_enum, code_enum, id_enum_type, code_valeur, libelle_valeur, ordre_affichage, is_actif " &
               "FROM ref_enum " &
               "WHERE id_enum_type = @id_enum_type " &
               "ORDER BY ordre_affichage, libelle_valeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_SelectByType_ActifsOnly
    '
    '   • Retourne uniquement les valeurs actives d'un type parent
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_SelectByType_ActifsOnly As String
        Get
            Return "SELECT id_enum, code_enum, id_enum_type, code_valeur, libelle_valeur, ordre_affichage, is_actif " &
               "FROM ref_enum " &
               "WHERE id_enum_type = @id_enum_type AND is_actif = 1 " &
               "ORDER BY ordre_affichage, libelle_valeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_SelectByTypeAndSearch
    '
    '   • Recherche les valeurs d'un type parent par code ou libellé
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_SelectByTypeAndSearch As String
        Get
            Return "SELECT id_enum, code_enum, id_enum_type, code_valeur, libelle_valeur, ordre_affichage, is_actif " &
               "FROM ref_enum " &
               "WHERE id_enum_type = @id_enum_type " &
               "  AND (code_valeur LIKE @s OR libelle_valeur LIKE @s) " &
               "ORDER BY ordre_affichage, libelle_valeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_SelectByTypeAndSearch_ActifsOnly
    '
    '   • Recherche les valeurs actives d'un type parent par code ou libellé
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_SelectByTypeAndSearch_ActifsOnly As String
        Get
            Return "SELECT id_enum, code_enum, id_enum_type, code_valeur, libelle_valeur, ordre_affichage, is_actif " &
               "FROM ref_enum " &
               "WHERE id_enum_type = @id_enum_type AND is_actif = 1 " &
               "  AND (code_valeur LIKE @s OR libelle_valeur LIKE @s) " &
               "ORDER BY ordre_affichage, libelle_valeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_Insert
    '
    '   • Insère une nouvelle valeur enfant dans ref_enum
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_Insert As String
        Get
            Return "INSERT INTO ref_enum (id_enum_type, code_valeur, libelle_valeur, ordre_affichage, is_actif) " &
               "VALUES (@id_enum_type, @code_valeur, @libelle_valeur, @ordre, @actif);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_Update
    '
    '   • Met à jour une valeur enfant existante
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_Update As String
        Get
            Return "UPDATE ref_enum " &
               "SET code_valeur=@code_valeur, " &
               "    libelle_valeur=@libelle_valeur, " &
               "    ordre_affichage=@ordre, " &
               "    is_actif=@actif " &
               "WHERE id_enum=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_Delete
    '
    '   • Supprime une valeur enfant via id_enum
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_Delete As String
        Get
            Return "DELETE FROM ref_enum WHERE id_enum=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_SelectIdByTypeAndCodeValeur
    '
    '   • Récupère l'identifiant d'une valeur via son type parent et son code
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_SelectIdByTypeAndCodeValeur As String
        Get
            Return "SELECT id_enum FROM ref_enum WHERE id_enum_type=@id_enum_type AND code_valeur=@code_valeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountByType
    '
    '   • Compte le nombre de valeurs enfants rattachées à un type parent
    '------------------------------------------------------------
    Public Function RefEnum_CountByType() As String
        Return "
SELECT COUNT(*)
FROM ref_enum
WHERE id_enum_type = @id_enum_type;"
    End Function

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_DeleteByType
    '
    '   • Supprime toutes les valeurs enfants d'un type parent
    '------------------------------------------------------------
    Public Function RefEnum_DeleteByType() As String
        Return "
    DELETE FROM ref_enum
    WHERE id_enum_type = @id_enum_type;"
    End Function

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInAuteursPaysTypeRelation
    '
    '   • Compte les usages d'une valeur dans auteurs_pays.type_relation
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInAuteursPaysTypeRelation As String
        Get
            Return "
SELECT COUNT(*)
FROM auteurs_pays
WHERE id_type_relation_pays = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresAuteursRole
    '
    '   • Compte les usages d'une valeur dans livres_auteurs.role
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresAuteursRole As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_auteurs
WHERE id_role_auteur = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresFichiersScope
    '
    '   • Compte les usages d'une valeur dans livres_fichiers.scope
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresFichiersScope As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_fichiers
WHERE id_scope_livre = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresFichiersType
    '
    '   • Compte les usages d'une valeur dans livres_fichiers.type
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresFichiersType As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_fichiers
WHERE id_type_fichier = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresStagingSourceImport
    '
    '   • Compte les usages d'une valeur dans livres_staging.source_import
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresStagingSourceImport As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_staging
WHERE id_source_import = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresStagingStatut
    '
    '   • Compte les usages d'une valeur dans livres_staging.statut
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresStagingStatut As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_staging
WHERE id_statut_staging = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresStagingAuteursRole
    '
    '   • Compte les usages d'une valeur dans livres_staging_auteurs.role
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresStagingAuteursRole As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_staging_auteurs
WHERE id_role_auteur = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresStatutLecture
    '
    '   • Compte les usages d'une valeur dans livres.statut_lecture
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresStatutLecture As String
        Get
            Return "
SELECT COUNT(*)
FROM livres
WHERE id_statut_lecture = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresSupportLecture
    '
    '   • Compte les usages d'une valeur dans livres.support_lecture
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresSupportLecture As String
        Get
            Return "
SELECT COUNT(*)
FROM livres
WHERE id_support_lecture = @id_enum;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' RefEnum_CountUsageInLivresTypeAcquisition
    '
    '   • Compte les usages d'une valeur dans livres.type_acquisition
    '------------------------------------------------------------
    Public ReadOnly Property RefEnum_CountUsageInLivresTypeAcquisition As String
        Get
            Return "
SELECT COUNT(*)
FROM livres
WHERE id_type_acquisition = @id_enum;"
        End Get
    End Property

#End Region

End Module
