Option Strict On
Option Explicit On

Imports System.Data

Friend Module UC_ReferentielsCrudHelpers
    Public Function TryGetMaxId(dt As DataTable,
                                idColumn As String,
                                Optional filterColumn As String = Nothing,
                                Optional filterValue As String = Nothing) As ULong

        If dt Is Nothing OrElse Not dt.Columns.Contains(idColumn) Then Return 0UL

        Dim bestId As ULong = 0UL
        Dim hasFilter As Boolean = Not String.IsNullOrWhiteSpace(filterColumn) AndAlso dt.Columns.Contains(filterColumn)
        Dim wanted As String = If(filterValue, "").Trim()

        For Each row As DataRow In dt.Rows
            If hasFilter Then
                Dim current As String = If(row(filterColumn), "").ToString().Trim()
                If Not String.Equals(current, wanted, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If
            End If

            Dim n As ULong
            If ULong.TryParse(If(row(idColumn), "").ToString(), n) AndAlso n > bestId Then
                bestId = n
            End If
        Next

        Return bestId
    End Function
End Module

Public Class UC_Contacts
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion des contacts",
            title:="Gestion des contacts",
            idColumnName:="id_contact",
            mainColumnName:="nom_contact",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_contact", "Code", [readOnly]:=True),
                New CrudFieldDefinition("nom_contact", "Nom", required:=True, maxLength:=160),
                New CrudFieldDefinition("email_perso", "Email perso", maxLength:=255),
                New CrudFieldDefinition("adresse_liseuse", "Adresse liseuse", maxLength:=255),
                New CrudFieldDefinition("type_liseuse", "Type liseuse", maxLength:=120)
            },
            loadAllFunc:=AddressOf GestionReferentiel.Contact_GetAll,
            searchFunc:=AddressOf GestionReferentiel.Contact_GetBySearch,
            insertFunc:=AddressOf InsertContact,
            updateFunc:=AddressOf UpdateContact,
            deleteFunc:=AddressOf GestionReferentiel.Contact_Delete,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function InsertContact(values As Dictionary(Of String, Object)) As ULong
        Dim c As New Contact With {
            .NomContact = ValueToString(values, "nom_contact"),
            .EmailPerso = ValueToString(values, "email_perso"),
            .AdresseLiseuse = ValueToString(values, "adresse_liseuse"),
            .TypeLiseuse = ValueToString(values, "type_liseuse")
        }
        GestionReferentiel.Contact_Insert(c)
        Dim dt = GestionReferentiel.Contact_GetBySearch(c.NomContact)
        Return TryGetMaxId(dt, "id_contact", "nom_contact", c.NomContact)
    End Function

    Private Shared Sub UpdateContact(id As ULong, values As Dictionary(Of String, Object))
        Dim c As New Contact With {
            .IdContact = id,
            .NomContact = ValueToString(values, "nom_contact"),
            .EmailPerso = ValueToString(values, "email_perso"),
            .AdresseLiseuse = ValueToString(values, "adresse_liseuse"),
            .TypeLiseuse = ValueToString(values, "type_liseuse")
        }
        GestionReferentiel.Contact_Update(c)
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim nom As String = ValueToString(values, "nom_contact")
        Dim nb As Integer = GestionReferentiel.Contact_CountUsage(id)
        If nb <= 0 Then
            Return $"Supprimer le contact '{nom}' ?"
        End If

        Return (
            $"Attention : ce contact est utilisé dans {nb} liaison(s)." & Environment.NewLine & Environment.NewLine &
            "Si vous confirmez, les références concernées seront vidées (SET NULL)." & Environment.NewLine & Environment.NewLine &
            $"Supprimer le contact '{nom}' ?"
        )
    End Function
End Class

Public Class UC_Editeurs
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion des éditeurs",
            title:="Gestion des éditeurs",
            idColumnName:="id_editeur",
            mainColumnName:="nom_editeur",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_editeur", "Code", [readOnly]:=True),
                New CrudFieldDefinition("nom_editeur", "Nom éditeur", required:=True, maxLength:=160),
                New CrudFieldDefinition("id_pays", "Id pays", CrudFieldKind.Integer),
                New CrudFieldDefinition("site_web", "Site web", maxLength:=255),
                New CrudFieldDefinition("notes_editeur_rtf", "Notes (RTF)", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("notes_editeur_txt", "Notes (TXT)", CrudFieldKind.MultilineText)
            },
            loadAllFunc:=AddressOf GestionReferentiel.Editeurs_GetAll,
            searchFunc:=AddressOf SearchEditeurs,
            insertFunc:=AddressOf InsertEditeur,
            updateFunc:=AddressOf UpdateEditeur,
            deleteFunc:=AddressOf GestionReferentiel.Editeurs_Delete,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function SearchEditeurs(search As String) As DataTable
        Return GestionReferentiel.Editeurs_GetBySearch(search, True)
    End Function

    Private Shared Function InsertEditeur(values As Dictionary(Of String, Object)) As ULong
        Dim e As New Editeur With {
            .NomEditeur = ValueToString(values, "nom_editeur"),
            .IdPays = ValueToNullableULong(values, "id_pays"),
            .SiteWeb = ValueToString(values, "site_web"),
            .NotesEditeurRtf = ValueToString(values, "notes_editeur_rtf"),
            .NotesEditeurTxt = ValueToString(values, "notes_editeur_txt")
        }
        Return GestionReferentiel.Editeurs_Insert(e)
    End Function

    Private Shared Sub UpdateEditeur(id As ULong, values As Dictionary(Of String, Object))
        Dim e As New Editeur With {
            .IdEditeur = id,
            .NomEditeur = ValueToString(values, "nom_editeur"),
            .IdPays = ValueToNullableULong(values, "id_pays"),
            .SiteWeb = ValueToString(values, "site_web"),
            .NotesEditeurRtf = ValueToString(values, "notes_editeur_rtf"),
            .NotesEditeurTxt = ValueToString(values, "notes_editeur_txt")
        }
        GestionReferentiel.Editeurs_Update(e)
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim nom As String = ValueToString(values, "nom_editeur")
        Dim nb As Integer = GestionReferentiel.Editeurs_CountLivres(id)
        If nb <= 0 Then
            Return $"Supprimer l'éditeur '{nom}' ?"
        End If

        Return (
            $"Attention : cet éditeur est lié à {nb} livre(s)." & Environment.NewLine & Environment.NewLine &
            "La suppression remettra la référence éditeur à NULL dans les livres concernés." & Environment.NewLine & Environment.NewLine &
            $"Supprimer l'éditeur '{nom}' ?"
        )
    End Function
End Class

Public Class UC_FormatFile
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion format file",
            title:="Gestion format file",
            idColumnName:="id_formatFile",
            mainColumnName:="nom_format",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_formatFile", "Code", [readOnly]:=True),
                New CrudFieldDefinition("nom_format", "Nom format", required:=True, maxLength:=120),
                New CrudFieldDefinition("extension", "Extension", maxLength:=30),
                New CrudFieldDefinition("mime_type", "Mime type", maxLength:=120),
                New CrudFieldDefinition("ordre_affichage", "Ordre", CrudFieldKind.Integer),
                New CrudFieldDefinition("is_actif", "Actif", CrudFieldKind.Boolean)
            },
            loadAllFunc:=AddressOf GestionReferentiel.FormatFile_GetAll,
            searchFunc:=AddressOf GestionReferentiel.FormatFile_GetBySearch,
            insertFunc:=AddressOf InsertFormatFile,
            updateFunc:=AddressOf UpdateFormatFile,
            deleteFunc:=AddressOf GestionReferentiel.FormatFile_Delete,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function InsertFormatFile(values As Dictionary(Of String, Object)) As ULong
        Dim nom = ValueToString(values, "nom_format")
        GestionReferentiel.FormatFile_Insert(
            nom,
            ValueToString(values, "extension"),
            ValueToString(values, "mime_type"),
            ValueToInt(values, "ordre_affichage"),
            ValueToBool(values, "is_actif", True)
        )
        Dim dt = GestionReferentiel.FormatFile_GetBySearch(nom)
        Return TryGetMaxId(dt, "id_formatFile", "nom_format", nom)
    End Function

    Private Shared Sub UpdateFormatFile(id As ULong, values As Dictionary(Of String, Object))
        GestionReferentiel.FormatFile_Update(
            id,
            ValueToString(values, "nom_format"),
            ValueToString(values, "extension"),
            ValueToString(values, "mime_type"),
            ValueToInt(values, "ordre_affichage"),
            ValueToBool(values, "is_actif", True)
        )
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim nom As String = ValueToString(values, "nom_format")
        Dim nb As Integer = GestionReferentiel.FormatFile_CountLivresFichiers(id)
        If nb <= 0 Then
            Return $"Supprimer le format '{nom}' ?"
        End If

        Return (
            $"Attention : ce format est utilisé dans {nb} fichier(s) de livre." & Environment.NewLine & Environment.NewLine &
            $"Supprimer le format '{nom}' ?"
        )
    End Function
End Class

Public Class UC_Impression
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion impression",
            title:="Gestion impression",
            idColumnName:="id_impression",
            mainColumnName:="nom_impression",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_impression", "Code", [readOnly]:=True),
                New CrudFieldDefinition("nom_impression", "Nom", required:=True, maxLength:=160),
                New CrudFieldDefinition("description_impression", "Description", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("note_rtf", "Note (RTF)", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("note_txt", "Note (TXT)", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("envie_Cal", "Envie Cal", maxLength:=80),
                New CrudFieldDefinition("is_actif", "Actif", CrudFieldKind.Boolean)
            },
            loadAllFunc:=AddressOf GestionReferentiel.Impression_GetAll,
            searchFunc:=AddressOf SearchImpression,
            insertFunc:=AddressOf InsertImpression,
            updateFunc:=AddressOf UpdateImpression,
            deleteFunc:=AddressOf GestionReferentiel.Impression_Delete,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function SearchImpression(search As String) As DataTable
        Return GestionReferentiel.Impression_GetBySearch(search, True)
    End Function

    Private Shared Function InsertImpression(values As Dictionary(Of String, Object)) As ULong
        Dim nom = ValueToString(values, "nom_impression")
        Dim i As New Impression With {
            .NomImpression = nom,
            .DescriptionImpression = ValueToString(values, "description_impression"),
            .NoteRtf = ValueToString(values, "note_rtf"),
            .NoteTxt = ValueToString(values, "note_txt"),
            .EnvieCal = ValueToString(values, "envie_Cal"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        GestionReferentiel.Impression_Insert(i)
        Dim dt = GestionReferentiel.Impression_GetBySearch(nom, includeNotes:=False)
        Return TryGetMaxId(dt, "id_impression", "nom_impression", nom)
    End Function

    Private Shared Sub UpdateImpression(id As ULong, values As Dictionary(Of String, Object))
        Dim i As New Impression With {
            .IdImpression = id,
            .NomImpression = ValueToString(values, "nom_impression"),
            .DescriptionImpression = ValueToString(values, "description_impression"),
            .NoteRtf = ValueToString(values, "note_rtf"),
            .NoteTxt = ValueToString(values, "note_txt"),
            .EnvieCal = ValueToString(values, "envie_Cal"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        GestionReferentiel.Impression_Update(i)
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim nom As String = ValueToString(values, "nom_impression")
        Dim nb As Integer = GestionReferentiel.Impression_CountLivres(id) + GestionReferentiel.Impression_CountLivresStaging(id)
        If nb <= 0 Then
            Return $"Supprimer le type d'impression '{nom}' ?"
        End If

        Return (
            $"Attention : ce type d'impression est encore référencé ({nb} utilisation(s))." & Environment.NewLine & Environment.NewLine &
            $"Supprimer '{nom}' ?"
        )
    End Function
End Class

Public Class UC_Pays
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion des pays",
            title:="Gestion des pays",
            idColumnName:="id_pays",
            mainColumnName:="nom_pays",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_pays", "Code", [readOnly]:=True),
                New CrudFieldDefinition("nom_pays", "Nom", required:=True, maxLength:=160),
                New CrudFieldDefinition("iso2", "ISO2", maxLength:=2, characterCasing:=CharacterCasing.Upper),
                New CrudFieldDefinition("iso3", "ISO3", maxLength:=3, characterCasing:=CharacterCasing.Upper)
            },
            loadAllFunc:=AddressOf GestionReferentiel.Pays_GetAll,
            searchFunc:=AddressOf GestionReferentiel.Pays_GetBySearch,
            insertFunc:=AddressOf InsertPays,
            updateFunc:=AddressOf UpdatePays,
            deleteFunc:=AddressOf GestionReferentiel.Pays_Delete,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function InsertPays(values As Dictionary(Of String, Object)) As ULong
        Dim p As New Pays With {
            .NomPays = ValueToString(values, "nom_pays"),
            .Iso2 = ValueToString(values, "iso2"),
            .Iso3 = ValueToString(values, "iso3")
        }
        Return GestionReferentiel.Pays_Insert(p)
    End Function

    Private Shared Sub UpdatePays(id As ULong, values As Dictionary(Of String, Object))
        Dim p As New Pays With {
            .IdPays = id,
            .NomPays = ValueToString(values, "nom_pays"),
            .Iso2 = ValueToString(values, "iso2"),
            .Iso3 = ValueToString(values, "iso3")
        }
        GestionReferentiel.Pays_Update(p)
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim nom As String = ValueToString(values, "nom_pays")
        Dim nb As Integer =
            GestionReferentiel.Pays_CountUsageInAuteurs(id) +
            GestionReferentiel.Pays_CountUsageInAuteursPays(id) +
            GestionReferentiel.Pays_CountUsageInEditeurs(id)

        If nb <= 0 Then
            Return $"Supprimer le pays '{nom}' ?"
        End If

        Return (
            $"Attention : ce pays est utilisé {nb} fois dans les données." & Environment.NewLine & Environment.NewLine &
            $"Supprimer le pays '{nom}' ?"
        )
    End Function
End Class

Public Class UC_PrixLit
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion des prix littéraires",
            title:="Gestion des prix littéraires",
            idColumnName:="id_prixLit",
            mainColumnName:="nom_prixLit",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_prixLit", "Code", [readOnly]:=True),
                New CrudFieldDefinition("nom_prixLit", "Nom", required:=True, maxLength:=180),
                New CrudFieldDefinition("description_prixLit", "Description", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("Notes_PrixLit_rtf", "Notes (RTF)", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("Notes_PrixLit_txt", "Notes (TXT)", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("is_actif", "Actif", CrudFieldKind.Boolean)
            },
            loadAllFunc:=AddressOf LoadPrixLit,
            searchFunc:=AddressOf SearchPrixLit,
            insertFunc:=AddressOf InsertPrixLit,
            updateFunc:=AddressOf UpdatePrixLit,
            deleteFunc:=AddressOf GestionReferentiel.PrixLit_Delete,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function LoadPrixLit() As DataTable
        Return GestionReferentiel.PrixLit_GetAll(False)
    End Function

    Private Shared Function SearchPrixLit(search As String) As DataTable
        Return GestionReferentiel.PrixLit_GetBySearch(search, includeNotes:=True, actifsOnly:=False)
    End Function

    Private Shared Function InsertPrixLit(values As Dictionary(Of String, Object)) As ULong
        Dim p As New PrixLit With {
            .NomPrixLit = ValueToString(values, "nom_prixLit"),
            .DescriptionPrixLit = ValueToString(values, "description_prixLit"),
            .NotesPrixLitRtf = ValueToString(values, "Notes_PrixLit_rtf"),
            .NotesPrixLitTxt = ValueToString(values, "Notes_PrixLit_txt"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        Return GestionReferentiel.PrixLit_Insert(p)
    End Function

    Private Shared Sub UpdatePrixLit(id As ULong, values As Dictionary(Of String, Object))
        Dim p As New PrixLit With {
            .IdPrixLit = id,
            .NomPrixLit = ValueToString(values, "nom_prixLit"),
            .DescriptionPrixLit = ValueToString(values, "description_prixLit"),
            .NotesPrixLitRtf = ValueToString(values, "Notes_PrixLit_rtf"),
            .NotesPrixLitTxt = ValueToString(values, "Notes_PrixLit_txt"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        GestionReferentiel.PrixLit_Update(p)
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim nom As String = ValueToString(values, "nom_prixLit")
        Dim nb As Integer = GestionReferentiel.PrixLit_CountCategories(id)
        If nb <= 0 Then
            Return $"Supprimer le prix littéraire '{nom}' ?"
        End If

        Return (
            $"Attention : ce prix possède {nb} catégorie(s)." & Environment.NewLine &
            "La suppression échouera tant que des catégories existent." & Environment.NewLine & Environment.NewLine &
            $"Supprimer '{nom}' ?"
        )
    End Function
End Class

Public Class UC_Recommandations
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion des recommandations",
            title:="Gestion des recommandations",
            idColumnName:="id_recommandation",
            mainColumnName:="source_nom",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_recommandation", "Code", [readOnly]:=True),
                New CrudFieldDefinition("id_origine_recommandation", "Id origine", CrudFieldKind.Integer, required:=True),
                New CrudFieldDefinition("source_nom", "Source nom", required:=True, maxLength:=180),
                New CrudFieldDefinition("source_login", "Source login", maxLength:=180),
                New CrudFieldDefinition("source_url", "Source URL", maxLength:=400),
                New CrudFieldDefinition("date_recommandation", "Date recommandation", CrudFieldKind.Date),
                New CrudFieldDefinition("commentaire_rtf", "Commentaire (RTF)", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("commentaire_txt", "Commentaire (TXT)", CrudFieldKind.MultilineText),
                New CrudFieldDefinition("is_actif", "Actif", CrudFieldKind.Boolean)
            },
            loadAllFunc:=AddressOf LoadRecommandations,
            searchFunc:=AddressOf SearchRecommandations,
            insertFunc:=AddressOf InsertRecommandation,
            updateFunc:=AddressOf UpdateRecommandation,
            deleteFunc:=AddressOf GestionReferentiel.Recommandation_Delete,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function LoadRecommandations() As DataTable
        Return GestionReferentiel.Recommandation_GetAll(False)
    End Function

    Private Shared Function SearchRecommandations(search As String) As DataTable
        Return GestionReferentiel.Recommandation_GetBySearch(search, actifsOnly:=False, includeNotes:=True)
    End Function

    Private Shared Function InsertRecommandation(values As Dictionary(Of String, Object)) As ULong
        Dim idOrigine As Integer = ValueToInt(values, "id_origine_recommandation")
        If idOrigine <= 0 Then Throw New InvalidOperationException("Id origine doit être supérieur à 0.")

        Dim r As New Recommandation With {
            .IdOrigineRecommandation = CULng(idOrigine),
            .SourceNom = ValueToString(values, "source_nom"),
            .SourceLogin = ValueToString(values, "source_login"),
            .SourceURL = ValueToString(values, "source_url"),
            .DateRecommandation = ValueToNullableDate(values, "date_recommandation"),
            .CommentaireRtf = ValueToString(values, "commentaire_rtf"),
            .CommentaireTxt = ValueToString(values, "commentaire_txt"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        Return GestionReferentiel.Recommandation_Insert(r)
    End Function

    Private Shared Sub UpdateRecommandation(id As ULong, values As Dictionary(Of String, Object))
        Dim idOrigine As Integer = ValueToInt(values, "id_origine_recommandation")
        If idOrigine <= 0 Then Throw New InvalidOperationException("Id origine doit être supérieur à 0.")

        Dim r As New Recommandation With {
            .IdRecommandation = id,
            .IdOrigineRecommandation = CULng(idOrigine),
            .SourceNom = ValueToString(values, "source_nom"),
            .SourceLogin = ValueToString(values, "source_login"),
            .SourceURL = ValueToString(values, "source_url"),
            .DateRecommandation = ValueToNullableDate(values, "date_recommandation"),
            .CommentaireRtf = ValueToString(values, "commentaire_rtf"),
            .CommentaireTxt = ValueToString(values, "commentaire_txt"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        GestionReferentiel.Recommandation_Update(r)
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim label As String = ValueToString(values, "source_nom")
        Dim nb As Integer = GestionReferentiel.Recommandation_CountUsageInLivres(id) + GestionReferentiel.Recommandation_CountUsageInStaging(id)
        If nb <= 0 Then
            Return $"Supprimer la recommandation '{label}' ?"
        End If

        Return (
            $"Attention : cette recommandation est utilisée {nb} fois." & Environment.NewLine & Environment.NewLine &
            $"Supprimer '{label}' ?"
        )
    End Function
End Class

Public Class UC_RefEnum
    Inherits UC_SimpleReferentielCrud

    Public Sub New(context As IReferentielShellContext)
        MyBase.New(
            moduleName:="Gestion des références énumération",
            title:="Gestion des références énumération",
            idColumnName:="id_enum_type",
            mainColumnName:="libelle_type",
            context:=context,
            fields:=New List(Of CrudFieldDefinition) From {
                New CrudFieldDefinition("code_enum_type", "Code", [readOnly]:=True),
                New CrudFieldDefinition("code_type", "Code type", required:=True, maxLength:=80, characterCasing:=CharacterCasing.Upper),
                New CrudFieldDefinition("libelle_type", "Libellé", required:=True, maxLength:=180),
                New CrudFieldDefinition("ordre_affichage", "Ordre", CrudFieldKind.Integer),
                New CrudFieldDefinition("is_actif", "Actif", CrudFieldKind.Boolean)
            },
            loadAllFunc:=AddressOf GestionReferentiel.RefEnumType_GetAll,
            searchFunc:=AddressOf GestionReferentiel.RefEnumType_GetBySearch,
            insertFunc:=AddressOf InsertRefEnumType,
            updateFunc:=AddressOf UpdateRefEnumType,
            deleteFunc:=AddressOf GestionReferentiel.RefEnumType_DeleteWithValues,
            buildDeleteMessageFunc:=AddressOf BuildDeleteMessage
        )
    End Sub

    Private Shared Function InsertRefEnumType(values As Dictionary(Of String, Object)) As ULong
        Dim t As New RefEnumType With {
            .CodeType = ValueToString(values, "code_type"),
            .LibelleType = ValueToString(values, "libelle_type"),
            .OrdreAffichage = ValueToInt(values, "ordre_affichage"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        Return GestionReferentiel.RefEnumType_Insert(t)
    End Function

    Private Shared Sub UpdateRefEnumType(id As ULong, values As Dictionary(Of String, Object))
        Dim t As New RefEnumType With {
            .IdEnumType = id,
            .CodeType = ValueToString(values, "code_type"),
            .LibelleType = ValueToString(values, "libelle_type"),
            .OrdreAffichage = ValueToInt(values, "ordre_affichage"),
            .IsActif = ValueToBool(values, "is_actif", True)
        }
        GestionReferentiel.RefEnumType_Update(t)
    End Sub

    Private Shared Function BuildDeleteMessage(id As ULong, values As Dictionary(Of String, Object)) As String
        Dim libelle As String = ValueToString(values, "libelle_type")
        Dim nb As Integer = GestionReferentiel.RefEnum_CountByType(id)
        If nb <= 0 Then
            Return $"Supprimer le type '{libelle}' ?"
        End If

        Return (
            $"Attention : ce type possède {nb} valeur(s) enfant." & Environment.NewLine &
            "La suppression supprimera aussi les valeurs associées." & Environment.NewLine & Environment.NewLine &
            $"Supprimer le type '{libelle}' ?"
        )
    End Function
End Class
