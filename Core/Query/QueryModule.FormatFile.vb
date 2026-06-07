Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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

End Module
