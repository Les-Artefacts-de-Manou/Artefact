Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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
