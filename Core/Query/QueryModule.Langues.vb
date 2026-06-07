Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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

End Module
