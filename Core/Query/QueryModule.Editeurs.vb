Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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
