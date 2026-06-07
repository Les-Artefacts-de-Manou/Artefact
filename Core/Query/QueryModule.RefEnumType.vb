Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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

End Module
