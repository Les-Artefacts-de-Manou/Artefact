Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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

End Module
