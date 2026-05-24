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
