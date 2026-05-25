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

Public Module QueryModule


#Region "LANGUES - SQL"

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_SelectAll
    '
    '   • Retourne la liste complète des langues triées par nom
    '------------------------------------------------------------
    Public ReadOnly Property Langues_SelectAll As String
        Get
            Return "SELECT id_langue, nom_langue, abrev_langue, iso639_1, iso639_2, code_langue " &
               "FROM langues " &
               "ORDER BY nom_langue;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_SelectBySearch
    '
    '   • Recherche les langues par nom, abréviation ou codes ISO
    '   • Utilise le paramètre @s au format LIKE
    '------------------------------------------------------------
    Public ReadOnly Property Langues_SelectBySearch As String
        Get
            Return "SELECT id_langue, nom_langue, abrev_langue, iso639_1, iso639_2, code_langue " &
               "FROM langues " &
               "WHERE nom_langue LIKE @s " &
               "   OR abrev_langue LIKE @s " &
               "   OR iso639_1 LIKE @s " &
               "   OR iso639_2 LIKE @s " &
               "ORDER BY nom_langue;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_Insert
    '
    '   • Insère une nouvelle langue
    '   • Ne fournit jamais id_langue ni code_langue généré
    '------------------------------------------------------------
    Public ReadOnly Property Langues_Insert As String
        Get
            Return "INSERT INTO langues (nom_langue, abrev_langue, iso639_1, iso639_2) " &
               "VALUES (@nom, @abrev, @iso1, @iso2);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_Update
    '
    '   • Met à jour une langue existante via son identifiant
    '------------------------------------------------------------
    Public ReadOnly Property Langues_Update As String
        Get
            Return "UPDATE langues " &
               "SET nom_langue=@nom, " &
               "    abrev_langue=@abrev, " &
               "    iso639_1=@iso1, " &
               "    iso639_2=@iso2 " &
               "WHERE id_langue=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_Delete
    '
    '   • Supprime une langue via id_langue
    '------------------------------------------------------------
    Public ReadOnly Property Langues_Delete As String
        Get
            Return "DELETE FROM langues WHERE id_langue=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_SelectIdByNomAbrev
    '
    '   • Récupère l'identifiant d'une langue après insertion
    '   • S'appuie sur le couple nom_langue / abrev_langue
    '------------------------------------------------------------
    Public ReadOnly Property Langues_SelectIdByNomAbrev As String
        Get
            Return "SELECT id_langue " &
                   "FROM langues " &
                   "WHERE nom_langue=@nom AND abrev_langue=@abrev;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_CountUsageInAuteurs
    '
    '   • Compte le nombre d'auteurs utilisant la langue
    '------------------------------------------------------------
    Public ReadOnly Property Langues_CountUsageInAuteurs As String
        Get
            Return "
SELECT COUNT(*)
FROM auteurs
WHERE id_langue_ecriture = @id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Langues_CountUsageInLivres
    '
    '   • Compte le nombre de livres utilisant la langue
    '------------------------------------------------------------
    Public ReadOnly Property Langues_CountUsageInLivres As String
        Get
            Return "
SELECT COUNT(*)
FROM livres
WHERE id_langue = @id;"
        End Get
    End Property

#End Region


#Region "PAYS - SQL"

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_SelectAll
    '
    '   • Retourne la liste complète des pays triés par nom
    '------------------------------------------------------------
    Public ReadOnly Property Pays_SelectAll As String
        Get
            Return "SELECT id_pays, nom_pays, iso2, iso3, code_pays " &
               "FROM pays " &
               "ORDER BY nom_pays;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_SelectBySearch
    '
    '   • Recherche les pays par nom ou codes ISO
    '------------------------------------------------------------
    Public ReadOnly Property Pays_SelectBySearch As String
        Get
            Return "SELECT id_pays, nom_pays, iso2, iso3, code_pays " &
               "FROM pays " &
               "WHERE nom_pays LIKE @s " &
               "   OR iso2 LIKE @s " &
               "   OR iso3 LIKE @s " &
               "ORDER BY nom_pays;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_Insert
    '
    '   • Insère un nouveau pays
    '------------------------------------------------------------
    Public ReadOnly Property Pays_Insert As String
        Get
            Return "INSERT INTO pays (nom_pays, iso2, iso3) " &
               "VALUES (@nom, @iso2, @iso3);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_Update
    '
    '   • Met à jour un pays existant via son identifiant
    '------------------------------------------------------------
    Public ReadOnly Property Pays_Update As String
        Get
            Return "UPDATE pays " &
               "SET nom_pays=@nom, " &
               "    iso2=@iso2, " &
               "    iso3=@iso3 " &
               "WHERE id_pays=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_Delete
    '
    '   • Supprime un pays via id_pays
    '------------------------------------------------------------
    Public ReadOnly Property Pays_Delete As String
        Get
            Return "DELETE FROM pays WHERE id_pays=@id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_SelectIdByNom
    '
    '   • Récupère l'identifiant d'un pays après insertion
    '   • S'appuie sur nom_pays déclaré unique
    '------------------------------------------------------------
    Public ReadOnly Property Pays_SelectIdByNom As String
        Get
            Return "SELECT id_pays FROM pays WHERE nom_pays=@nom;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_CountUsageInAuteurs
    '
    '   • Compte les usages d'un pays dans la table auteurs
    '------------------------------------------------------------
    Public ReadOnly Property Pays_CountUsageInAuteurs As String
        Get
            Return "
SELECT COUNT(*)
FROM auteurs
WHERE id_pays = @id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_CountUsageInAuteursPays
    '
    '   • Compte les usages d'un pays dans la table auteurs_pays
    '------------------------------------------------------------
    Public ReadOnly Property Pays_CountUsageInAuteursPays As String
        Get
            Return "
SELECT COUNT(*)
FROM auteurs_pays
WHERE id_pays = @id;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Pays_CountUsageInEditeurs
    '
    '   • Compte les usages d'un pays dans la table editeurs
    '------------------------------------------------------------
    Public ReadOnly Property Pays_CountUsageInEditeurs As String
        Get
            Return "
SELECT COUNT(*)
FROM editeurs
WHERE id_pays = @id;"
        End Get
    End Property

#End Region


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


#Region "CONTACTS - SQL"

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_SelectAll
    '
    '   • Retourne la liste complète des contacts triés par nom
    '------------------------------------------------------------
    Public ReadOnly Property Contact_SelectAll As String
        Get
            Return "
SELECT
    id_contact,
    code_contact,
    nom_contact,
    email_perso,
    adresse_liseuse,
    type_liseuse,
    created_at,
    updated_at
FROM contacts
ORDER BY nom_contact;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_SelectBySearch
    '
    '   • Recherche les contacts par nom, email, liseuse ou type
    '------------------------------------------------------------
    Public ReadOnly Property Contact_SelectBySearch As String
        Get
            Return "SELECT id_contact, code_contact, nom_contact, email_perso, adresse_liseuse, type_liseuse " &
               "FROM contacts " &
               "WHERE nom_contact LIKE @s " &
               "   OR email_perso LIKE @s " &
               "   OR adresse_liseuse LIKE @s " &
                "  OR type_liseuse LIKE @s " &
               "ORDER BY nom_contact;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_Insert
    '
    '   • Insère un nouveau contact
    '   • code_contact et les timestamps sont gérés par la base
    '------------------------------------------------------------
    Public ReadOnly Property Contact_Insert As String
        Get
            Return "
INSERT INTO contacts
(
    nom_contact,
    email_perso,
    adresse_liseuse,
    type_liseuse
)
VALUES
(
    @nom_contact,
    @email_perso,
    @adresse_liseuse,
    @type_liseuse
);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_Update
    '
    '   • Met à jour un contact existant
    '------------------------------------------------------------
    Public ReadOnly Property Contact_Update As String
        Get
            Return "
UPDATE contacts
SET
    nom_contact = @nom_contact,
    email_perso = @email_perso,
    adresse_liseuse = @adresse_liseuse,
    type_liseuse = @type_liseuse
WHERE id_contact = @id_contact;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_Delete
    '
    '   • Supprime un contact via id_contact
    '------------------------------------------------------------
    Public ReadOnly Property Contact_Delete As String
        Get
            Return "
DELETE FROM contacts
WHERE id_contact = @id_contact;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_ExistsByNom
    '
    '   • Vérifie si un nom de contact existe déjà
    '------------------------------------------------------------
    Public ReadOnly Property Contact_ExistsByNom As String
        Get
            Return "
SELECT COUNT(*)
FROM contacts
WHERE nom_contact = @nom_contact;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_ExistsByNomExceptId
    '
    '   • Vérifie l'existence d'un nom en excluant le contact courant
    '------------------------------------------------------------
    Public ReadOnly Property Contact_ExistsByNomExceptId As String
        Get
            Return "
SELECT COUNT(*)
FROM contacts
WHERE nom_contact = @nom_contact
  AND id_contact <> @id_contact;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Contact_CountUsageInLivresContacts
    '
    '   • Compte les usages d'un contact dans livres_contacts
    '------------------------------------------------------------
    Public ReadOnly Property Contact_CountUsageInLivresContacts As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_contacts
WHERE id_contact = @id_contact;"
        End Get
    End Property

#End Region


#Region "EDITEURS - SQL"

    '------------------------------------------------------------
    ' 📌 V1.5 - 19/03/2026
    ' Editeurs_SelectAll
    '
    '   • Retourne la liste complète des éditeurs avec leur pays éventuel
    '------------------------------------------------------------
    Public ReadOnly Property Editeurs_SelectAll As String
        Get
            Return "
        SELECT
    e.id_editeur,
    e.code_editeur,
    e.nom_editeur,
    e.id_pays,
    p.nom_pays,
    e.site_web,
    e.notes_editeur_rtf, 
    e.notes_editeur_txt,
    e.created_at,
    e.updated_at
FROM editeurs e
LEFT JOIN pays p
    ON e.id_pays = p.id_pays
ORDER BY e.nom_editeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.5 - 19/03/2026
    ' Editeurs_SelectBySearch
    '
    '   • Recherche les éditeurs par nom, code ou pays
    '------------------------------------------------------------
    Public ReadOnly Property Editeurs_SelectBySearch As String
        Get
            Return "
SELECT
    e.id_editeur,
    e.code_editeur,
    e.nom_editeur,
    e.id_pays,
    p.nom_pays,
    e.site_web,
    e.notes_editeur_rtf, 
    e.notes_editeur_txt,
    e.created_at,
    e.updated_at
FROM editeurs e
LEFT JOIN pays p
    ON e.id_pays = p.id_pays
WHERE
    e.nom_editeur LIKE CONCAT('%', @search, '%')
    OR e.code_editeur LIKE CONCAT('%', @search, '%')
    OR p.nom_pays LIKE CONCAT('%', @search, '%')
ORDER BY e.nom_editeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.5 - 19/03/2026
    ' Editeurs_SelectBySearchIncludingNotes
    '
    '   • Recherche les éditeurs par nom, code, pays ou notes
    '------------------------------------------------------------
    Public ReadOnly Property Editeurs_SelectBySearchIncludingNotes As String
        Get
            Return "
SELECT
    e.id_editeur,
    e.code_editeur,
    e.nom_editeur,
    e.id_pays,
    p.nom_pays,
    e.site_web,
    e.notes_editeur_rtf, 
    e.notes_editeur_txt,
    e.created_at,
    e.updated_at
FROM editeurs e
LEFT JOIN pays p
    ON e.id_pays = p.id_pays
WHERE
    e.nom_editeur LIKE CONCAT('%', @search, '%')
    OR e.code_editeur LIKE CONCAT('%', @search, '%')
    OR p.nom_pays LIKE CONCAT('%', @search, '%')
    OR e.notes_editeur_txt LIKE CONCAT('%', @search, '%')
ORDER BY e.nom_editeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.5 - 19/03/2026
    ' Editeurs_Insert
    '
    '   • Insère un nouvel éditeur
    '   • code_editeur et les timestamps sont gérés par la base
    '------------------------------------------------------------
    Public ReadOnly Property Editeurs_Insert As String
        Get
            Return "
        INSERT INTO editeurs
        (
            nom_editeur,
            id_pays,
            site_web,
            notes_editeur_rtf, notes_editeur_txt
        )
        VALUES
        (
            @nom_editeur,
            @id_pays,
            @site_web,
            @notes_editeur_rtf, @notes_editeur_txt
        );"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.5 - 19/03/2026
    ' Editeurs_Update
    '
    '   • Met à jour un éditeur existant
    '------------------------------------------------------------
    Public ReadOnly Property Editeurs_Update As String
        Get
            Return "
        UPDATE editeurs
        SET
            nom_editeur = @nom_editeur,
            id_pays = @id_pays,
            site_web = @site_web,
            notes_editeur_rtf = @notes_editeur_rtf,
            notes_editeur_txt = @notes_editeur_txt
        WHERE id_editeur = @id_editeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Editeurs_Delete
    '
    '   • Supprime un éditeur via id_editeur
    '   • La FK livres.id_editeur reste en SET NULL
    '------------------------------------------------------------
    Public ReadOnly Property Editeurs_Delete As String
        Get
            Return "
        DELETE FROM editeurs
        WHERE id_editeur = @id_editeur;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.4 - 12/03/2026
    ' Editeurs_CountLivres
    '
    '   • Compte le nombre de livres liés à un éditeur
    '------------------------------------------------------------
    Public ReadOnly Property Editeurs_CountLivres As String
        Get
            Return "
        SELECT COUNT(*)
        FROM livres
        WHERE id_editeur = @id_editeur;"
        End Get
    End Property

#End Region


#Region "FORMATFILE - SQL"

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_SelectAll
    '
    ' Retourne tous les formats de fichier triés par ordre
    ' d'affichage puis nom.
    '------------------------------------------------------------
    Public ReadOnly Property FormatFile_SelectAll As String
        Get
            Return "
SELECT
    id_formatFile,
    code_formatFile,
    nom_format,
    extension,
    mime_type,
    ordre_affichage,
    is_actif,
    created_at,
    updated_at
FROM formatfile
ORDER BY ordre_affichage, nom_format;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_SelectBySearch
    '
    ' Recherche sur nom_format, extension ou mime_type.
    '------------------------------------------------------------
    Public ReadOnly Property FormatFile_SelectBySearch As String
        Get
            Return "
SELECT
    id_formatFile,
    code_formatFile,
    nom_format,
    extension,
    mime_type,
    ordre_affichage,
    is_actif,
    created_at,
    updated_at
FROM formatfile
WHERE
    nom_format LIKE CONCAT('%', @search, '%')
    OR extension LIKE CONCAT('%', @search, '%')
    OR mime_type LIKE CONCAT('%', @search, '%')
ORDER BY ordre_affichage, nom_format;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Insert
    '
    ' Ajoute un nouveau format de fichier.
    ' code_formatFile est généré par la base.
    '------------------------------------------------------------
    Public ReadOnly Property FormatFile_Insert As String
        Get
            Return "
INSERT INTO formatfile
(
    nom_format,
    extension,
    mime_type,
    ordre_affichage,
    is_actif
)
VALUES
(
    @nom_format,
    @extension,
    @mime_type,
    @ordre_affichage,
    @is_actif
);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Update
    '
    ' Met à jour un format de fichier existant.
    '------------------------------------------------------------
    Public ReadOnly Property FormatFile_Update As String
        Get
            Return "
UPDATE formatfile
SET
    nom_format = @nom_format,
    extension = @extension,
    mime_type = @mime_type,
    ordre_affichage = @ordre_affichage,
    is_actif = @is_actif
WHERE id_formatFile = @id_formatFile;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_Delete
    '
    ' Supprime un format de fichier.
    ' FK livres_fichiers.id_formatFile : RESTRICT
    '------------------------------------------------------------
    Public ReadOnly Property FormatFile_Delete As String
        Get
            Return "
DELETE FROM formatfile
WHERE id_formatFile = @id_formatFile;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 13/03/2026
    ' FormatFile_CountLivresFichiers
    '
    ' Compte le nombre de lignes de livres_fichiers
    ' utilisant ce format.
    '------------------------------------------------------------
    Public ReadOnly Property FormatFile_CountLivresFichiers As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_fichiers
WHERE id_formatFile = @id_formatFile;"
        End Get
    End Property

#End Region


#Region "IMPRESSION - SQL"

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_SelectAll
    '
    ' Retourne toutes les impressions triées par nom.
    '------------------------------------------------------------
    Public ReadOnly Property Impression_SelectAll As String
        Get
            Return "
SELECT
    id_impression,
    code_impression,
    nom_impression,
    description_impression,
    note_rtf,
    note_txt,
    envie_Cal,
    is_actif,
    created_at,
    updated_at
FROM impression
ORDER BY nom_impression;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_SelectBySearch
    '
    ' Recherche sur nom, description ou envie_Cal.
    '------------------------------------------------------------
    Public ReadOnly Property Impression_SelectBySearch As String
        Get
            Return "
SELECT
    id_impression,
    code_impression,
    nom_impression,
    description_impression,
    note_rtf,
    note_txt,
    envie_Cal,
    is_actif,
    created_at,
    updated_at
FROM impression
WHERE
    nom_impression LIKE CONCAT('%', @search, '%')
    OR description_impression LIKE CONCAT('%', @search, '%')
    OR envie_Cal LIKE CONCAT('%', @search, '%')
ORDER BY nom_impression;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_SelectBySearchIncludingNotes
    '
    ' Recherche sur nom, description, envie_Cal et note.
    '------------------------------------------------------------
    Public ReadOnly Property Impression_SelectBySearchIncludingNotes As String
        Get
            Return "
SELECT
    id_impression,
    code_impression,
    nom_impression,
    description_impression,
    note_rtf,
    note_txt,
    envie_Cal,
    is_actif,
    created_at,
    updated_at
FROM impression
WHERE
    nom_impression LIKE CONCAT('%', @search, '%')
    OR description_impression LIKE CONCAT('%', @search, '%')
    OR envie_Cal LIKE CONCAT('%', @search, '%')
    OR note_rtf LIKE CONCAT('%', @search, '%')
ORDER BY nom_impression;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Insert
    '
    ' Ajoute une nouvelle impression.
    ' code_impression est généré par la base.
    '------------------------------------------------------------
    Public ReadOnly Property Impression_Insert As String
        Get
            Return "
INSERT INTO impression
(
    nom_impression,
    description_impression,
    note_rtf,
    note_txt,
    envie_Cal,
    is_actif
)
VALUES
(
    @nom_impression,
    @description_impression,
    @note_rtf,
    @note_txt,
    @envie_Cal,
    @is_actif
);"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Update
    '
    ' Met à jour une impression existante.
    '------------------------------------------------------------
    Public ReadOnly Property Impression_Update As String
        Get
            Return "
UPDATE impression
SET
    nom_impression = @nom_impression,
    description_impression = @description_impression,
    note_rtf = @note_rtf,
    note_txt = @note_txt,
    envie_Cal = @envie_Cal,
    is_actif = @is_actif
WHERE id_impression = @id_impression;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_Delete
    '
    ' Supprime une impression.
    ' FK livres.id_impression       : SET NULL
    ' FK livres_staging.id_impression : SET NULL
    '------------------------------------------------------------
    Public ReadOnly Property Impression_Delete As String
        Get
            Return "
DELETE FROM impression
WHERE id_impression = @id_impression;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_CountLivres
    '
    ' Compte les livres utilisant cette impression.
    '------------------------------------------------------------
    Public ReadOnly Property Impression_CountLivres As String
        Get
            Return "
SELECT COUNT(*)
FROM livres
WHERE id_impression = @id_impression;"
        End Get
    End Property

    '------------------------------------------------------------
    ' 📌 V1.0 - 14/03/2026
    ' Impression_CountLivresStaging
    '
    ' Compte les livres_staging utilisant cette impression.
    '------------------------------------------------------------
    Public ReadOnly Property Impression_CountLivresStaging As String
        Get
            Return "
SELECT COUNT(*)
FROM livres_staging
WHERE id_impression = @id_impression;"
        End Get
    End Property


#End Region


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
