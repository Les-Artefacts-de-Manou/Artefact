Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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

End Module
