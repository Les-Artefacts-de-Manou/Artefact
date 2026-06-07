# Règles de conception

Ce document décrit les règles de conception et décisions techniques prises lors de la création de la base de données **Artefact**.

Ces règles sont considérées comme **fondatrices** pour Artefact v1.

---

## Règles générales de développement

### 1. Planifier d'abord

Comprendre, anticiper, questionner avant d'agir. Ne jamais foncer tête baissée. Une bonne analyse précède un bon code.

### 2. Step by step, toujours

Chaque étape est validée avant d'aller plus loin. On avance par petits pas, avec des points de contrôle réguliers pour éviter les dérives et garantir la cohérence globale.

### 3. Code clair, versionné, commenté

- Nom explicite de la fonction/procédure
- Commentaire en-tête obligatoire (entier copié même pour une petite modif)
- Ajout systématique dans le changelog `.md` à la fin de chaque thread
- Documentation des décisions prises et des raisons derrière chaque choix

### 4. Documenter dès le départ

- Changelog `.md` → **fin de la journée pour toute la journée**
- Résumé OneNote → en parallèle si besoin pour la journée
- README.md → si impact structurel ou de logique globale
- Rules.md → pour les règles de conception et de développement

### 5. Factorisation

- Jamais de doublon sans raison
- Créer ou réutiliser des fonctions centrales dans des modules séparés et dédiés (ex : `Utils.vb`, `DatabaseManager.vb`, `ConfigManager.vb`, `GestionLog.vb`)
- Toujours se demander : "Est-ce que cette logique existe déjà quelque part ?"
- Ne pas hésiter à refactoriser si nécessaire

### 6. Target framework

- `net8.0-windows` → attention aux packages compatibles

### 7. Mémo Démarrage d'une Modif

| Élément | Action |
|--------|--------|
| Version | Obligatoire (Vx.x + date) |
| Localisation du code | Module / classe / section |
| ToolstripStatus + log | Si UI ou process |
| Répercussions | Indiquer les appels, docs, etc. |

### 8. Conventions de nommage

#### Fichiers et classes

| Type | Nommage |
|------|---------|
| Entité DB | `Livre.vb`, `Auteur.vb` |
| Entité Calibre | `LivreCalibre.vb` |
| Entité non normalisée | `LivreAnorm.vb` |
| Manager | `DatabaseManager.vb`, `ConfigManager.vb` |
| Formulaire | `NomForm.vb` explicite |
| UserControl | `UC_NomExplicite.vb` |
| Module logique | `GestionLivresCalibre.vb`, etc. |
| Utilitaires | `Utils.vb`, `UtilsForm.vb`, `UtilsUCReferentiels.vb` |

#### Fonctions

| Préfixe | Usage |
|---------|------|
| `GetOrInsertX()` | Lookup + Insert |
| `NettoyerX()` | Nettoyage / transformation |
| `ConstruireX()` | Génération (chemin, clause SQL…) |
| `TraiterX()` | Traitement global |
| `InsererXDansDB()` | Insertion DB |
| `ConfigurerX()` | Configuration UI ou composant |
| `ValidateX()` | Validation métier |

#### Contrôles UI

| Préfixe | Contrôle |
|---------|----------|
| `txt`| TextBox |
| `btn` | Boutons |
| `dtp` | DateTimePicker |
| `lsv` | ListView |
| `chk` | CheckBox |
| `dgv` | DataGridView |
| `cmb` | ComboBox |
| `lbl` | Labels |
| `lst` | ListBox |
| `nud` | numericUpDown |
| `tab` | TabControl |
| `grp` | GroupBox |
| `pnl` | Panel |
| `pic` | PictureBox |
| `tls` | ToolStrip |
| `men` | MenuStrip |
| `sts` | StatusStrip |
| `wbw` | WebView2 |
| `pgb` | ProgressBar |
| `lnk` | LinkLabel |
| `rtb` | RichTextBox |
| `trv` | TreeView |
| `spc` | SplitContainer |
| `flp` | FlowLayoutPanel |
| `tlp` | TableLayoutPanel |
| `ttp` | ToolTip |
| `err` | ErrorProvider |

---

## Database Artefact

### Clés primaires (IDs)

#### Principe
- Toutes les tables utilisent une clé primaire numérique
- Type : `BIGINT UNSIGNED`
- Pas de clé primaire composite
- Pas de clé primaire textuelle
- Le nom de ID est toujours `id_[nom de la Table]` pour garantir la clarté et la cohérence

#### Convention
`id_<nom_table>`

Exemples : `id_auteur`, `id_tag`, `id_serie`

#### Génération des IDs (SEQUENCE)
- AUTO_INCREMENT n'est pas utilisé
- Chaque table nécessitant un ID lisible dispose de :
  - une SEQUENCE dédiée
  - un DEFAULT (NEXT VALUE FOR seq_xxx)
- Création en 2 fois (contrainte MariaDB) :

```SQL
CREATE TABLE ma_table (
  id_ma_table BIGINT UNSIGNED NOT NULL
);

ALTER TABLE ma_table
  ALTER COLUMN id_ma_table SET DEFAULT (NEXT VALUE FOR seq_ma_table);
```

#### Configuration SEQUENCE standard

```SQL
CREATE SEQUENCE seq_auteurs
	START WITH 1
	INCREMENT BY 1
	MINVALUE 1
	NO MAXVALUE
	CACHE 1
	NOCYCLE;
```

- **START WITH 1** : Convention de départ
- **INCREMENT BY 1** : Incrément séquentiel simple
- **MINVALUE 1** : Interdiction des valeurs négatives
- **NO MAXVALUE** : Pas de limite (BIGINT permet 9 quintillions)
- **CACHE 1** : Évite les sauts de 1000, comportement prévisible
- **NOCYCLE** : Interdit le retour à 1

#### TRUNCATE TABLE
TRUNCATE TABLE ne réinitialise pas les séquences → utiliser `ALTER SEQUENCE seq_xxx RESTART WITH 1`

### Codes lisibles (code_xxx)

- Chaque entité possède un code lisible
- Le code est stocké, généré automatiquement
- N'est jamais une clé primaire
- N'est jamais utilisé comme clé étrangère

Format : `<Préfixe><ID sur 6 chiffres>`

Exemples : `A000042` (Auteur), `T000017` (Tag), `S000003` (Série)

#### Implémentation

```SQL
ALTER TABLE auteurs
  ADD COLUMN code_auteur VARCHAR(12)
  GENERATED ALWAYS AS (
	CONCAT('A', LPAD(id_auteur, 6, '0'))
  ) STORED,
  ADD UNIQUE KEY uq_auteurs_code (code_auteur);
```

**Nota : Si Drop de table**
1. Supprimer la séquence associée
2. Supprimer les références DEFAULT dans autres tables
3. DROP les clés primaires et étrangères avant DROP table

---

## Connection Database et démarrage application

### Point d'accès unique

- L'ouverture est gérée automatiquement dans `GetConnexionMariaDB()`
- La fermeture est implicite via le bloc `Using`
- Le pooling ADO.NET assure la performance

### Démarrage de l'application

- L'application démarre sur la Form `home.vb` (menu principal)
- `home.vb` lance `PortailReferentiels` pour la gestion des référentiels
- La vérification de la connexion MariaDB est effectuée au démarrage via `DatabaseManager`
- Aucun accès direct à la base en dehors de `DatabaseManager`

### Usage standard

```vb
Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()
	If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
		' Requête ici
	End If
End Using
```

**Règles strictes** :
- `conn.Open()` ne doit **jamais** être appelé manuellement
- L'ouverture est gérée automatiquement
- La fermeture est implicite via `Using`

---

## Architecture UI - Migration UserControls (Mars 2026)

### Principe architectural

Les anciennes Forms de gestion (`GestionImpression`, `GestionRecommandations`, `GestionPrixLit`) ont été **migrées vers des UserControls** hébergés dans `PortailReferentiels`.

### Bénéfices

- **Contexte partagé** : ToolTip, ErrorProvider, StatusStrip, NavigationManager
- **Code factorisé** : Helpers centralisés dans `UtilsUCReferentiels`
- **Interface homogène** : Navigation fluide avec fil d'Ariane
- **Maintenance simplifiée** : Patterns standardisés

### Structure UI

#### Forms_Portail

- **`home.vb`** : Menu principal de l'application (point d'entrée)
- **`PortailReferentiels.vb`** : Portail hébergeant tous les UC référentiels

#### UserControls_Referentiels

**UC simples (1 table)** :
- `UC_Langues`, `UC_Pays`, `UC_Contacts`, `UC_Editeurs`, `UC_FormatFile`, `UC_Impression`

**UC hiérarchiques (master-detail)** :
- `UC_RefEnum` (types + valeurs)
- `UC_Recommandations` (origines + recommandations)
- `UC_PrixLit` (prix → catégories → années)

### Composants transverses obligatoires

#### UserControlContext

Tous les UC reçoivent un contexte unifié depuis `PortailReferentiels` :
- `SharedToolTip` : ToolTip partagé
- `SharedErrorProvider` : ErrorProvider partagé
- `SharedStatusStrip` : StatusStrip partagé
- `NavigationManager` : Gestionnaire de navigation et fil d'Ariane

#### Interface IContextAwareUserControl

Tous les UC référentiels doivent implémenter :

```vb
Public Interface IContextAwareUserControl
	Sub SetContext(context As UserControlContext)
End Interface
```

#### NavigationManager

Gère le fil d'Ariane et la navigation :
- `PushNavigation(title As String)` : Ajoute un niveau
- `PopNavigation()` : Retour arrière
- `ClearNavigation()` : Réinitialise
- Synchronisation automatique avec le label de navigation

### Helpers partagés (UtilsUCReferentiels)

Module centralisant les fonctions communes aux UC :

**Configuration UI** :
- `ConfigurerStyleGrid(dgv)` : Style DataGridView uniforme
- `ConfigurerBoutonsMode(mode, btnNew, btnEdit, btnSave, btnCancel, btnDelete, Optional btnNewChild)` : Gestion états boutons
- `ConfigurerRecherche(txtSearch, btnSearch, btnClear)` : Configuration zone de recherche

**Validation** :
- `ValidateRequiredField(errProvider, control, fieldName, value)` : Validation champs requis

**Conversions sécurisées** :
- `DbToBool(value)`, `DbToInt(value)`, `SafeULong(value)` : Conversions depuis DB

**Manipulation DataGridView** :
- `HideTechnicalColumns(dgv)` : Masquage colonnes ID/codes
- `MasquerColonnesTechniques(dgv)` : Alias francophone
- `SetColonneVisible(dgv, columnName, visible)` : Visibilité colonne
- `UpdateCountLabel(lbl, count)` : Mise à jour compteur

**Extraction valeurs** :
- `GetStringValue(row, columnName)`, `GetBoolValue(row, columnName)`, `GetIntValue(row, columnName)`

### RichTextBox enrichi (RichTextNotesHelper)

Système standardisé pour notes formatées :

#### Principe

Chaque champ de notes est stocké dans deux colonnes :
- `xxx_rtf` : contenu formaté (affichage UI)
- `xxx_txt` : texte brut (recherche SQL)

#### UserControl standard : UC_RichTextEditor

Composant réutilisable avec toolbar intégrée :
- Gras, Italique, Souligné
- Liste à puces
- Tabulation
- Synchronisation automatique RTF ↔ TXT

#### API RichTextNotesHelper

```vb
' Initialisation
RichTextNotesHelper.InitializeRichTextBox(rtb)

' Chargement
RichTextNotesHelper.LoadRtfContent(rtb, rtfFromDb)

' Sauvegarde
Dim rtfContent As String = RichTextNotesHelper.GetRtfContent(rtb)
Dim txtContent As String = RichTextNotesHelper.GetPlainText(rtb)

' Actions formatage
RichTextNotesHelper.ToggleBold(rtb)
RichTextNotesHelper.ToggleItalic(rtb)
RichTextNotesHelper.ToggleUnderline(rtb)
RichTextNotesHelper.ToggleBulletList(rtb)
```

#### Règles strictes

- Le RTF n'est **jamais** utilisé pour les recherches SQL
- Le texte brut n'est **jamais** utilisé pour l'affichage riche
- Toute manipulation passe par le helper
- Pas de manipulation directe du RTF dans les écrans UI

### Modes d'édition (UtilsForm.ModeEdition)

Enum partagé par tous les UC :

```vb
Public Enum ModeEdition
	Consultation
	Nouveau
	Modification
End Enum
```

#### Workflow standard

1. **Consultation** (par défaut) :
   - Champs désactivés
   - Boutons : Nouveau, Modifier, Supprimer actifs
   - Navigation libre dans DataGridView

2. **Nouveau** :
   - Champs vides et actifs
   - Boutons : Enregistrer, Annuler actifs
   - DataGridView désactivé

3. **Modification** :
   - Champs actifs avec données chargées
   - Snapshot des données pour annulation
   - Boutons : Enregistrer, Annuler actifs
   - DataGridView désactivé

#### Gestion boutons automatisée

```vb
UtilsUCReferentiels.ConfigurerBoutonsMode(
	_currentMode,
	btnNew, btnEdit, btnSave, btnCancel, btnDelete
)
```

### Forms_Communs

#### DialogChoix.vb

Form de dialogue réutilisable pour choix utilisateur :

```vb
Public Enum DialogResult
	Yes
	No
	Cancel
End Enum

' Usage
Dim dialog As New DialogChoix("Titre", "Message", showCancel:=True)
Dim result As DialogResult = dialog.ShowDialog()
```

**Caractéristiques** :
- Titre personnalisable
- Message personnalisable
- Boutons optionnels (Yes, No, Cancel)
- Icône contextuelle (Question, Warning, Info, Error)
- Retour DialogResult typé

---

## Référentiels - Architecture post-migration UC

### 1. Architecture générale

- Un UserControl par référentiel
- Hébergement dans `PortailReferentiels`
- Séparation stricte des couches :
  - **SQL** → QueryModule (partiel par domaine)
  - **Exécution DB** → GestionReferentiel (partiel par domaine)
  - **UI** → UserControl
  - **Entité métier** → Classe dédiée (ex: `Langue`, `Pays`, `PrixLit`)

### 2. Design UI

**À utiliser** :
- TableLayoutPanel pour structuration liste/détails
- Panel pour organisation logique
- FlowLayoutPanel pour boutons

**À éviter** :
- SplitContainer (instabilité Designer)
- Panels superflus sans logique

**Pattern standard UC référentiel** :
```
pnlTop           → Titre + navigation (fil d'Ariane)
pnlSearch        → Recherche (TextBox + boutons)
tlpMain          → Liste (DataGridView) + Détails (champs)
pnlActions       → Boutons d'action
(StatusStrip hérité du contexte partagé)
```

### 3. Modes d'édition

- Enum `ModeEdition` partagé via `UtilsForm`
- Modification uniquement via bouton explicite
- Pas de passage automatique en mode Modification sur TextChanged
- Annulation via snapshot des données initiales

**Implémentation SetMode** :

```vb
Private Sub SetMode(mode As ModeEdition)
	_currentMode = mode

	Select Case mode
		Case ModeEdition.Consultation
			' Désactiver champs, activer navigation
			UtilsUCReferentiels.ConfigurerBoutonsMode(mode, btnNew, btnEdit, btnSave, btnCancel, btnDelete)

		Case ModeEdition.Nouveau
			' Vider et activer champs, snapshot = Nothing
			UtilsUCReferentiels.ConfigurerBoutonsMode(mode, btnNew, btnEdit, btnSave, btnCancel, btnDelete)

		Case ModeEdition.Modification
			' Activer champs, créer snapshot pour annulation
			_snapshot = New ClasseMetier With {.Prop1 = valeur1, ...}
			UtilsUCReferentiels.ConfigurerBoutonsMode(mode, btnNew, btnEdit, btnSave, btnCancel, btnDelete)
	End Select
End Sub
```

### 4. Chargement des données

**Référentiels à faible volume** :
- Chargement automatique au Load

**Référentiels volumineux** (ex: Tags, Auteurs) :
- Pas de chargement complet automatique
- Recherche obligatoire ou bouton explicite

### 5. Validation UI

- Utiliser `ErrorProvider` (partagé via contexte) pour validation locale
- Utiliser `StatusStrip` (partagé via contexte) pour message global
- Ne pas utiliser `MessageBox` pour validation simple
- Exceptions DB → log via `GestionLog`

**Exemple** :

```vb
Private Function ValidateForm() As Boolean
	_context.SharedErrorProvider.Clear()
	Dim isValid As Boolean = True

	isValid = UtilsUCReferentiels.ValidateRequiredField(
		_context.SharedErrorProvider,
		txtNom, "Nom", txtNom.Text.Trim()
	) And isValid

	If Not isValid Then
		_context.SharedStatusStrip.Items(0).Text = "Veuillez corriger les erreurs"
	End If

	Return isValid
End Function
```

### 6. Majuscules/minuscules

- `CharacterCasing = Upper` côté UI pour champs codes
- Normalisation appliquée côté DB via helper `DbValueOrNullUpper`

### 7. Standardisation DataGridView

**Configuration obligatoire** :

```vb
Private Sub ConfigurerGrid()
	UtilsUCReferentiels.ConfigurerStyleGrid(dgvListe)
	UtilsUCReferentiels.HideTechnicalColumns(dgvListe)
	dgvListe.Columns("nom_xxx").DefaultCellStyle.Font = New Font(dgvListe.Font, FontStyle.Bold)
End Sub
```

**Règles visuelles** :
- Header centré + bold
- Lignes alternées sobres
- Sélection douce
- Colonne principale (`nom_xxx`, `libelle_xxx`) en gras
- Masquage automatique des colonnes techniques (ID, codes)

### 8. Factorisation

Helpers génériques autorisés dans `UtilsUCReferentiels` uniquement si :
- Utilisés dans au minimum 2 UC
- Indépendants de la logique métier

**Helpers validés** :
- Tous ceux listés dans la section "Helpers partagés" ci-dessus

### 9. Pattern UC validé

Le pattern UC est désormais considéré **stable et reproductible**.

Tout nouveau référentiel doit suivre strictement la checklist :
1. Créer classe métier dans `Classes/Referentiels/`
2. Ajouter requêtes SQL dans `QueryModule.<Domaine>.vb`
3. Ajouter méthodes CRUD dans `GestionReferentiel.<Domaine>.vb`
4. Créer UserControl dans `UserControls_Referentiels/`
5. Implémenter `IContextAwareUserControl`
6. Utiliser `ConfigurerStyleGrid`, `ConfigurerBoutonsMode`, `ValidateRequiredField`
7. Implémenter les 3 modes (Consultation, Nouveau, Modification)
8. Gérer snapshot pour annulation
9. Ajouter l'UC dans `PortailReferentiels` avec navigation

### 10. Référentiels hiérarchiques (parent/enfant)

Certains référentiels sont composés de plusieurs niveaux :

**Exemples** :
- `UC_RefEnum` : types + valeurs
- `UC_Recommandations` : origines + recommandations
- `UC_PrixLit` : prix → catégories → années

#### Principe

- La table **parent** définit les catégories
- La(les) table(s) **enfant(s)** contiennent les détails
- Les tables métier référencent toujours les tables enfants

#### Gestion modes hiérarchiques

**UC à 2 niveaux** (ex: `UC_Recommandations`) :
- 2 DataGridView (master + detail)
- 2 jeux de boutons
- 2 méthodes SetMode distinctes
- Le bouton "Nouveau" du niveau enfant est désactivé si aucun parent sélectionné

**UC à 3 niveaux** (ex: `UC_PrixLit`) :
- 3 DataGridView
- 3 jeux de boutons
- 3 méthodes SetMode distinctes
- Cascade de dépendances : Prix → Catégories → Années

#### ConfigurerBoutonsMode avec hiérarchie

```vb
' Niveau parent (prix)
UtilsUCReferentiels.ConfigurerBoutonsMode(
	_modePrixLit,
	btnNewPrix, btnEditPrix, btnSavePrix, btnCancelPrix, btnDeletePrix,
	btnNewCategorie  ' Bouton "Nouveau" du niveau enfant
)

' Niveau enfant (catégorie)
UtilsUCReferentiels.ConfigurerBoutonsMode(
	_modeCategorie,
	btnNewCategorie, btnEditCategorie, btnSaveCategorie, btnCancelCategorie, btnDeleteCategorie,
	btnNewAnnee  ' Bouton "Nouveau" du niveau petit-enfant
)
```

### 11. Gestion des suppressions

Tous les référentiels doivent appliquer une logique de suppression cohérente.

#### Principe général

Avant toute suppression :
1. Vérifier les dépendances dans les tables liées
2. Distinguer les cas `RESTRICT` et `SET NULL`
3. Informer clairement l'utilisateur
4. Bloquer la suppression si nécessaire

#### Cas RESTRICT

Si une table possède une FK avec `RESTRICT` :
- La suppression doit être **bloquée** côté application
- Un message explicite indique les tables utilisant encore l'élément

#### Cas SET NULL

Si une table possède une FK avec `SET NULL` :
- La suppression reste possible
- L'utilisateur doit être averti que les références seront vidées

#### Règle de maintenance

Si une nouvelle table référence un référentiel existant :
- Les contrôles de suppression doivent être mis à jour
- Les requêtes de comptage d'usage doivent être complétées
- Les messages utilisateur doivent être adaptés

### 12. Synchronisation DataGridView / Détails

**Règle obligatoire** :
- Utiliser `DataGridView.CurrentRow` pour synchronisation
- **Ne pas utiliser** `SelectedRows(0)`

**Raison** :
`SelectedRows` et `CurrentRow` peuvent diverger après certaines opérations (reload, suppression, changement de mode).

**Implémentation** :

```vb
Private Sub BindSelectedToDetails()
	If dgvListe.CurrentRow Is Nothing Then Exit Sub

	Dim row As DataGridViewRow = dgvListe.CurrentRow
	txtNom.Text = UtilsUCReferentiels.GetStringValue(row, "nom_xxx")
	' ...
End Sub
```

### 13. Événement obligatoire

Chaque DataGridView doit implémenter :

```vb
Private Sub dgvListe_SelectionChanged(sender As Object, e As EventArgs) Handles dgvListe.SelectionChanged
	If _currentMode = ModeEdition.Consultation Then
		BindSelectedToDetails()
	End If
End Sub
```

**But** : Garantir la synchronisation immédiate entre la sélection utilisateur et les champs détail.

---

## Standards de structure du code

### 1. En-tête de fichier obligatoire

```vb
'------------------------------------------------------------
' DatabaseManager.vb
' Version : V1.0
' Date    : 18/02/2026
' Auteur  : Joëlle
'
' Rôle :
' Gestion centralisée de la connexion MariaDB.
' Point d'accès unique à la base Artefact.
'
' Évolution :
' - V1.0 : Création de la structure initiale.
'------------------------------------------------------------
```

### 2. Versionnage systématique des procédures/fonctions

```vb
'------------------------------------------------------------
' V1.0 - 18/02/2026
' GetConnexionMariaDB
'
' Retourne une connexion MariaDB ouverte.
' Initialise la chaîne de connexion si nécessaire.
'
' Appelé par : PortailReferentiels_Load, ConfigManager
'------------------------------------------------------------
Public Shared Function GetConnexionMariaDB() As MySqlConnection
```

### 3. Commentaire fonctionnel court

Chaque procédure doit contenir un commentaire synthétique expliquant :
- Ce qu'elle fait
- Ce qu'elle retourne
- Les cas d'erreur gérés

Pas de roman. Pas de paraphrase du code. Juste l'intention.

### 4. Structuration par régions

```vb
#Region "Imports"
...
#End Region

#Region "Variables / Constantes / Enum"
...
#End Region

#Region "Initialisation / Load"
...
#End Region

#Region "Gestion Modes"
...
#End Region

#Region "Actions Boutons"
...
#End Region

#Region "Validation"
...
#End Region

#Region "Chargement Données"
...
#End Region
```

**Objectifs** :
- Lisibilité
- Maintenance facilitée
- Navigation rapide
- Structure cohérente dans tout le projet

### 5. Principe architectural

- Une classe = une responsabilité principale
- Si une classe commence à gérer plusieurs processus distincts → refactorisation

---

## Organisation des dossiers

- **Core** : Infrastructure système (DatabaseManager, ConfigManager, QueryModule, GestionLog)
- **Utils** : Outils génériques transversaux (UtilsForm, UtilsUCReferentiels, RichTextNotesHelper, InputHelpers, NavigationManager, UserControlContext)
- **Forms_Portail** : Navigation principale (home.vb, PortailReferentiels.vb)
- **Forms_Config** : Configuration (GestionConnexionMariaDb.vb)
- **Forms_Communs** : Dialogues réutilisables (DialogChoix.vb)
- **UserControls_Referentiels** : Tous les UC référentiels
- **Classes/Referentiels** : Modèles métier
- **Metier/Referentiels** : Logique métier (partiels par domaine)

**Principe** : Tout élément nécessaire au démarrage appartient à `Core`.

---

## Initialisation des services

L'initialisation des services critiques (DB, Config, etc.) doit être :
- Explicite au démarrage
- Avec fallback lazy sécurisé

---

## Packages NuGet

Pour MariaDB / MySQL sous .NET 8 :
- Utiliser **MySqlConnector**
- Éviter MySql.Data sauf contrainte spécifique

---

## Bootstrap Infrastructure

Lors de la mise en place initiale :
- Un mode bootstrap temporaire est autorisé
- Il doit être clairement identifié comme transitoire
- Il doit être supprimé dès que ConfigManager est opérationnel

---

## Gestion des mots de passe (Infrastructure)

- Toute modification de mot de passe doit être explicite (mode dédié)
- La logique de décision du mot de passe ne doit jamais être implicite
- Une seule méthode est autorisée pour construire un modèle depuis l'UI (ex : `BuildConfigFromUI()`)
- Les méthodes de test ne doivent jamais dépendre d'un état interne global (`_cfg`)
- Une form ne peut se fermer automatiquement que si l'action métier est validée
- Aucune sauvegarde ne doit intervenir après un test échoué

---

## Versionnement du schéma DB

### Principe

Toute modification structurelle de la base Artefact doit :
- Être réalisée via un script SQL numéroté (`00N_description.sql`)
- Mettre à jour `meta_schema.schema_version`
- Être accompagnée de la mise à jour de `ExpectedSchemaVersion` dans l'application

### Synchronisation obligatoire

La version du schéma en base et la constante `ExpectedSchemaVersion` doivent toujours être identiques.

### Démarrage applicatif

Au démarrage :
- L'application vérifie la compatibilité du schéma
- Aucun contournement silencieux n'est autorisé
- En cas de mismatch, le statut `SchemaMismatch` est retourné et loggé

### Discipline

Aucun changement structurel direct en base sans :
- Script versionné
- Mise à jour du changelog
- Alignement de la constante applicative

---

## Logging Production

### Philosophie

Les niveaux de log (Rapide, Succinct, Complet) sont des marqueurs de profondeur, pas un mécanisme de réduction.

```vb
GestionLog.EcrireLog("UI: config DB locale chargée.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)
```

Toute information loguée doit être écrite.

### Gestion des erreurs

Tout `Catch` doit comporter :
- Un log Succinct (description synthétique)
- Un log avec exception (ex.Message)
- Si nécessaire, niveau Complet pour contexte technique

```vb
Catch ex As Exception
	_context.SharedStatusStrip.Items(0).Text = "Erreur au chargement."
	GestionLog.EcrireLog("UI: erreur Load UC_Langues.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

	MessageBox.Show(
		"Erreur au chargement." & Environment.NewLine & ex.Message,
		"Artefact",
		MessageBoxButtons.OK,
		MessageBoxIcon.Error
	)
End Try
```

Aucune exception ne doit être silencieuse.

### Sécurité

Il est strictement interdit de logguer :
- Mot de passe
- Connection string complète contenant un secret

Les messages doivent passer par le mécanisme de masquage.

### Session Header

Chaque exécution de l'application doit produire un header de session dans le fichier journalier incluant :
- Date/Heure
- Machine
- User Windows

### Discipline

Tout nouveau processus doit intégrer :
- Logging structuré
- Gestion d'erreur complète
- Catégorie adaptée (Startup, Database, UI, Process)

---

## Séparation des responsabilités (Logging & Crypto)

1. Les modules techniques bas niveau (Crypto, DTO, modèles) ne doivent pas logguer. Le logging appartient aux couches d'orchestration ou d'infrastructure.

2. Toute opération IO (lecture/écriture fichier) doit :
   - Être encapsulée dans un Try/Catch
   - Logguer l'erreur avec exception complète
   - Relancer l'exception si critique

3. Toute opération de sécurité (DPAPI, déchiffrement) doit :
   - Throw une exception explicite
   - Ne jamais masquer une erreur
   - Ne jamais logguer de secret

4. Une seule source de vérité pour :
   - Construction de connection string
   - Calcul des chemins système

---

## UI & Sécurité des mots de passe

1. Le mot de passe ne doit jamais être affiché en clair par défaut
2. Toute visualisation en clair doit être :
   - Temporaire (action maintenue)
   - Volontaire (action utilisateur explicite)
   - Non logguée
3. Aucun secret (mot de passe, connection string complète) ne doit apparaître dans les logs

---

## Validation UI

1. Les validations de formulaire :
   - N'utilisent pas de MsgBox
   - Utilisent ErrorProvider (partagé via contexte) + Focus
   - Logguent uniquement en cas de KO (niveau Rapide, catégorie UI)

2. Les MsgBox sont réservées :
   - Aux blocages critiques
   - Aux erreurs empêchant la poursuite normale du flux

---

## Orchestration Startup

1. `AppStartupManager` :
   - Ne contient aucune logique UI
   - Ne déclenche aucun MsgBox
   - Retourne uniquement un statut

2. Toute erreur critique au démarrage :
   - Est logguée (Succinct + exception)
   - Provoque la fermeture contrôlée de l'application

---

## ComboBox (critique)

Une ComboBox ne peut pas avoir un double rôle :
- Filtre
- Champ métier

Si nécessaire :
- Utiliser deux contrôles distincts

Sinon :
- Prévoir validation stricte empêchant toute valeur invalide en base

**Exemple d'erreur à éviter** :
Une ComboBox contenant "Toutes origines" (filtre UI) ne doit jamais permettre de sauvegarder cette valeur en base.

---

## Gestion des recommandations

Un système de recommandations permet d'enregistrer les sources ayant suggéré un livre.

### Principe

Une recommandation est considérée comme un **événement documenté** provenant d'une source externe ou humaine.

Exemples : TikTok, Blog, Ami, Libraire, Podcast, Réseau social

### Structure

Les recommandations sont stockées dans la table `recommandations`.

Chaque recommandation contient :
- Une origine (référentiel `origines_recommandation`)
- Un nom ou identifiant de source
- Éventuellement une URL
- Une date
- Un commentaire utilisateur (RTF + TXT)

### Association aux livres

Une recommandation peut être associée :
- À un livre normalisé (`livres`)
- À un livre en phase de staging (`livres_staging`)

L'association est réalisée via des **tables de liaison** :
- `livres_recommandations`
- `livres_staging_recommandations`

### Justification

Ce modèle permet :
- Plusieurs recommandations pour un même livre
- Plusieurs sources distinctes
- Une séparation claire entre l'événement de recommandation et l'état du livre dans le système

---

28/06/26
