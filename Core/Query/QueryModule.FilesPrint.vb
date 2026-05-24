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

End Module
