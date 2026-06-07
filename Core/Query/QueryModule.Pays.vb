Option Strict On
Option Infer On
Option Explicit On

Partial Module QueryModule

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

End Module
