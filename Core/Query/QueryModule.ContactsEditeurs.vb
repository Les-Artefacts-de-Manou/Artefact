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

End Module
