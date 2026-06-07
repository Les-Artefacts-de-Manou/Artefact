# CHANGELOG Artefact

[TOC]

## 🧭 Convention de ce changelog

- Format privilégié : **Added / Changed / Fixed / Removed**
- Les entrées sont classées par ordre **antéchronologique** (plus récent en haut)
- Les changements par date sont séparés par une ligne horizontale
- Chaque entrée reste courte, factuelle et vérifiable

---

## 📅 Mai 2026 (Semaine 4) - Migration UserControls et Documentation

### Added
- Création du portail `PortailReferentiels` pour héberger les UserControls référentiels
- Ajout du système de contexte partagé via `UserControlContext` et `IContextAwareUserControl`
- Ajout du `NavigationManager` pour gestion centralisée du fil d'Ariane
- Création du module `UtilsUCReferentiels` avec helpers partagés :
  - `ConfigurerStyleGrid` (remplace `FormatReferentielGrid` + `HighlightMainColumn`)
  - `ValidateRequiredField`
  - `ConfigurerBoutonsMode`
  - `ConfigurerRecherche`
- Ajout de `DialogChoix` (form réutilisable pour remplacer MessageBox standards)
- Ajout de `UC_RichTextToolbar` (composant réutilisable pour notes enrichies)
- Création des UserControls référentiels :
  - `UC_Langues`
  - `UC_Pays`
  - `UC_Contacts`
  - `UC_Editeurs`
  - `UC_FormatFile`
  - `UC_Impression`
  - `UC_Recommandations`
  - `UC_PrixLit`
  - `UC_RefEnum`
- Ajout des modules partiels :
  - `GestionReferentiel.*.vb` (par domaine métier)
  - `QueryModule.*.vb` (par domaine métier)

### Changed
- Migration complète des référentiels Forms → UserControls hébergés
- Révision complète de la documentation :
  - `README.md` : Vue d'ensemble mise à jour avec UC
  - `Docs/Rules.md` : Règles adaptées au contexte UC
  - `Docs/Documentation_technique_UI.md` : Architecture UC détaillée
  - `Docs/Process_Artefact.md` : Processus 03-07 mis à jour + nouveaux processus contexte/navigation
  - `Docs/ADR_Artefact.md` : Ajout décisions ARCH-003-v2, ARCH-009 à ARCH-012, UI-003-v2 à UI-006
- Commenté le code legacy des anciens launchers dans `Home.vb` et `Home.Designer.vb`

### Removed
- Suppression des anciennes forms référentielles :
  - `GestionLangues`
  - `GestionContacts`
  - `GestionEditeurs`
  - `GestionFormatFile`
  - `GestionImpression`
  - `GestionPays`
  - `GestionPrixLit`
  - `GestionRecommandations`
  - `GestionRefEnum`
- Suppression de `ShowFormIfNotOpen` (navigation legacy)
- Suppression du dossier `Forms_Menu/` (vide et orphelin)

### Fixed
- Correction de la cohérence des helpers (centralisation dans `UtilsUCReferentiels`)
- Correction de la duplication d'ErrorProvider (désormais partagé via contexte)
- Correction de la validation partagée via helper centralisé

---

## 📅 26/03/2026

### Added
- Alimentation de :
  - `cboFiltrePrixLit`
  - `cboPrixLitParentCategorie`
  - `cboPrixLitCategorieAnnee`
- Ajout de la logique de rafraîchissement des combos après insertion / modification
- Ajout des `SelectionChanged` sur les 3 DataGridView
- Ajout du formatage des grilles dans les méthodes `Charger...`
- Ajout du contexte parent en lecture seule dans l'onglet Année :
  - `txtPrixLitParentAnnee`
  - `txtCategorieParentAnnee`

### Changed
- Correction du chargement et de la synchronisation inter-onglets
- Ajustement UX de l'onglet Année :
  - affichage du PrixLit parent en lecture seule
  - affichage de la catégorie courante en lecture seule en consultation
  - bascule sur `cboPrixLitCategorieAnnee` en mode Nouveau / Modification
- Correction du `NumericUpDown` Année :
  - gestion des bornes
  - initialisation correcte

### Fixed
- Correction du bug `CurrentCell` sur colonne invisible
- Correction du bug de non-refresh des combos après création d'un `PrixLit`
- Identification et correction du schéma DB résiduel dans `prixlit_annee` :
  - suppression logique attendue de `id_prixLit`
  - conservation de `UNIQUE(id_prixlit_categorie, annee)`

---

## 📅 25/03/2026

### Added
- Mise en place du squelette complet de `GestionPrixLit`
- Ajout des régions principales de la Form :
  - Déclarations
  - Initialisation
  - Gestion des modes
  - Interface utilisateur
  - Actions utilisateur
  - Validation
  - Recherche
- Implémentation de :
  - `btnNew_Click`
  - `btnEdit_Click`
  - `btnCancel_Click`
  - `btnSave_Click`
- Ajout des méthodes :
  - `BindSelectedPrixLitToDetails`
  - `BindSelectedPrixLitCategorieToDetails`
  - `BindSelectedPrixLitAnneeToDetails`
  - `ClearPrixLitDetails`
  - `ClearPrixLitCategorieDetails`
  - `ClearPrixLitAnneeDetails`

### Changed
- Alignement strict du code-behind sur le modèle `GestionRecommandations`
- Intégration correcte de `RichTextNotesHelper`
- Remplacement des méthodes helper inventées par les vraies méthodes du projet
- Correction du mode `Ajout` en `ModeEdition.Nouveau`

### Fixed
- Correction des erreurs de compilation liées aux snapshots manquants
- Correction des erreurs liées à la synchronisation des détails
- Réintégration de `SetStatus`

---

## 📅 24/03/2026

### Added
- Implémentation complète des régions SQL pour :
  - `PRIXLIT - SQL`
  - `PRIXLIT_CATEGORIE - SQL`
  - `PRIXLIT_ANNEE - SQL`
- Ajout de la recherche avec et sans notes pour `PrixLit`
- Ajout des variantes `_ActifsOnly`
- Implémentation complète des régions CRUD dans `GestionReferentiel` pour :
  - `PRIXLIT - CRUD`
  - `PRIXLIT_CATEGORIE - CRUD`
  - `PRIXLIT_ANNEE - CRUD`

### Changed
- Uniformisation du paramètre SQL `@search`
- Alignement strict des noms de propriétés entre classes VB et paramètres SQL
- Adoption de `LAST_INSERT_ID()` pour les insertions

### Design
- Validation définitive du modèle hiérarchique :
  - `PrixLit`
  - `PrixLitCategorie`
  - `PrixLitAnnee`
- Maintien d'une structure UI à 3 onglets
- Abandon d'une tentative de fusion Catégorie / Année dans un seul écran

---

## 📅 23/03/2026

### Added
- Implémentation complète des requêtes SQL pour :
  - PrixLit
  - PrixLitCategorie
  - PrixLitAnnee
- Ajout des variantes de recherche :
  - avec / sans notes
  - avec / sans filtre actifs
- Implémentation CRUD complète dans GestionReferentiel :
  - PrixLit
  - PrixLitCategorie
  - PrixLitAnnee

### Changed
- Uniformisation du paramètre SQL `@search`
- Alignement strict des noms de propriétés entre classes et DB
- Utilisation de `LAST_INSERT_ID()` pour récupération d'identifiants

### Fixed
- Correction du mapping propriétés (PascalCase vs snake_case)
- Recommandation d'utilisation de `CAST(... AS CHAR)` pour recherche sur champs numériques

### Design
- Validation de l'architecture UI :
  - Structure en 3 onglets conservée
  - Abandon de la fusion Catégorie / Année
- Normalisation des conventions UI :
  - pnlTop et pnlActions communs
  - ID cachés + Code en lecture seule
  - ComboBox pour relations parent

---

## 📅 22/03/2026

### Added
- Implémentation complète des requêtes SQL pour :
  - PrixLit
  - PrixLitCategorie
  - PrixLitAnnee
- Ajout des variantes de recherche :
  - avec / sans notes
  - avec / sans filtre actifs
- Implémentation CRUD complète dans GestionReferentiel :
  - PrixLit
  - PrixLitCategorie
  - PrixLitAnnee

### Changed
- Uniformisation du paramètre SQL `@search`
- Alignement strict des noms de propriétés entre classes et DB
- Utilisation de `LAST_INSERT_ID()` pour récupération d'identifiants

### Fixed
- Correction du mapping propriétés (PascalCase vs snake_case)
- Recommandation d'utilisation de `CAST(... AS CHAR)` pour recherche sur champs numériques

### Design
- Validation de l'architecture UI :
  - Structure en 3 onglets conservée
  - Abandon de la fusion Catégorie / Année
- Normalisation des conventions UI :
  - pnlTop et pnlActions communs
  - ID cachés + Code en lecture seule
  - ComboBox pour relations parent

---

## 📅 20/03/2026 (Partie 2)

### Added
- Intégration du modèle standard **RichTextBox enrichie** sur les recommandations
  - Utilisation de `RichTextNotesHelper`
  - Ajout de la toolbar (gras, italique, souligné, liste, tabulation)
  - Gestion RTF + texte brut (`commentaire_rtf`, `commentaire_txt`)
- Standardisation du stockage des notes enrichies :
  - Double colonne systématique (RTF + TXT)
  - Chargement sécurisé avec fallback texte
  - Sauvegarde centralisée via helper

### Changed
- Correction majeure de conception UI sur les recommandations :
  - Séparation du combo :
	- `cboFiltreOrigineRecommandation` → filtre de liste
	- `cboOrigineRecommandation` → champ métier (édition)
  - Suppression de la dépendance au filtre pour les opérations CRUD
- Mise à jour des méthodes :
  - `LoadOrigines` → alimentation des 2 combos
  - `BindSelectedRecommandationToDetails` → synchronisation origine réelle
  - `ClearRecommandationDetails` → reset du champ origine
  - `ValidateRecommandationForm` → validation stricte origine
  - `SaveRecommandation` → utilisation du combo métier
  - `dgvOriginesRecommandation_SelectionChanged` → suppression couplage filtre

### Fixed
- Correction bug critique :
  - insertion / update avec `id_origine_recommandation = 0`
  - cause : utilisation du combo filtre au lieu du champ métier
- Correction requêtes de recherche incluant notes (WHERE ajusté)

---

## 📅 20/03/2026 (Partie 1)

### Added
- Mise en place du modèle standard pour les champs de notes enrichies (RTF + texte brut)
- Intégration complète sur le référentiel **Editeurs**
- Helper central `RichTextNotesHelper`
- Toolbar minimale (gras, italique, souligné, liste, tabulation)

### Changed
- Migration DB :
  - Suppression du champ `notes_editeur`
  - Ajout des champs :
	- `notes_editeur_rtf` (MEDIUMTEXT)
	- `notes_editeur_txt` (TEXT)
- Migration DB Impression :
  - ajout `note_rtf`, `note_txt`
  - suppression `note`
- Migration DB Recommandations :
  - ajout `commentaire_rtf`, `commentaire_txt`
  - suppression `commentaire`
- Mise à jour QueryModule (SELECT / INSERT / UPDATE / SEARCH)
- Adaptation GestionReferentiel
- Adaptation Forms

### Fixed
- Correction des requêtes de recherche incluant les notes :
  - utilisation systématique des colonnes `_txt`
- Correction oubli WHERE dans Impression (notes)
- Correction flux après sauvegarde :
  - récupération de l'ID inséré
  - re-sélection automatique de l'enregistrement

---

## 📅 19/03/2026

### Added
- Ajout de la recherche avancée dans `GestionRecommandations`
- Ajout de `chkSearchNotes` pour inclure ou non le champ `commentaire`
- Mise en place d'un mode de recherche globale sur toutes les origines
- Ajout d'une entrée spéciale dans `cboOrigineRecommandation` :
  - `[Toutes les origines]`
- Distinction entre :
  - recherche ciblée par origine
  - recherche globale

### Changed
- Form – GestionRecommandations
  - Ajout de la méthode `ApplySearch()`
  - Adaptation de `btnSearch_Click`
  - Adaptation de `btnClearSearch_Click`
  - Mise à jour de `UpdateSearchControlsState()`
  - Conservation du pattern `GestionRefEnum`
  - Validation de la mécanique parent / enfant :
	- `tabOrigines`
	- `tabRecommandations`
- GestionReferentiel
  - Ajout / adaptation des méthodes :
	- `Recommandation_GetAll(actifsOnly)`
	- `Recommandation_GetBySearch(searchText, actifsOnly, includeNotes)`
	- `Recommandation_GetByOrigine(idOrigine, actifsOnly)`
	- `Recommandation_GetByOrigineAndSearch(idOrigine, searchText, actifsOnly, includeNotes)`
- QueryModule
  - Ajout / adaptation des requêtes SQL :
	- `Recommandation_SelectAll`
	- `Recommandation_SelectAll_ActifsOnly`
	- `Recommandation_SelectBySearch`
	- `Recommandation_SelectBySearch_ActifsOnly`
	- `Recommandation_SelectBySearchIncludingNotes`
	- `Recommandation_SelectBySearchIncludingNotes_ActifsOnly`
	- `Recommandation_SelectByOrigineAndSearch`
	- `Recommandation_SelectByOrigineAndSearch_ActifsOnly`
	- `Recommandation_SelectByOrigineAndSearchIncludingNotes`
	- `Recommandation_SelectByOrigineAndSearchIncludingNotes_ActifsOnly`

---

## 📅 18/03/2026

### Added
- Implémentation complète du module `GestionRecommandations`
- Ajout de la région `Recherche` dans la Form
- Ajout des requêtes SQL pour :
  - ref_origine_recommandation
  - recommandations
- Ajout des méthodes CRUD dans `GestionReferentiel`
- Gestion des recherches avec filtre actif/inactif
- Intégration du filtrage par origine via `cboOrigineRecommandation`

### Changed
- Harmonisation complète des noms entre :
  - Form
  - GestionReferentiel
  - QueryModule
- Alignement du pattern sur `GestionRefEnum`
- Simplification des dépendances (uniquement livres et staging)

### Fixed
- Réécriture complète de `QueryModule` pour ce référentiel
- Réécriture complète des méthodes `GestionReferentiel` liées aux recommandations
- Nettoyage des incohérences de nommage (RefXXX vs XXX)
- Correction incohérence noms méthodes (critique)
- Correction des requêtes SQL pour correspondre au schéma réel
- Correction binding DataGridView (colonnes id masquées)

---

## 📅 16/03/2026

### Added
- Création du modèle de données pour la gestion des recommandations de livres
- Tables impliquées :
  - ref_origine_recommandation
  - recommandations
  - livres_recommandations
  - livres_staging_recommandations
- Table : `ref_origine_recommandation`
  - Objectif : gestion des sources de recommandation (TikTok, blog, ami, etc.)
- Table : `recommandations`
  - Permet de stocker un événement de recommandation indépendant
- Tables de liaison avec PK composite :
  - livres_recommandations : PRIMARY KEY (id_livre, id_recommandation)
  - livres_staging_recommandations : PRIMARY KEY (id_livre_staging, id_recommandation)

### Changed
- Abandon de l'utilisation d'un identifiant technique + séquence pour les tables de liaison
- Suppression des séquences :
  - seq_livres_recommandations
  - seq_livres_staging_recommandations
- Simplification du modèle
- Ajout du champ `livres_staging.avec_fichier` (TINYINT(1)) :
  - distinguer livres possédés sous forme de fichier
  - livres recensés uniquement (recommandations, nouveautés, sorties)

### Design
- Nouvelle form prévue `GestionRecommandations`
  - Tab 1 : gestion du référentiel `ref_origine_recommandation`
  - Tab 2 : gestion des `recommandations`
- Suppression de l'affichage des champs techniques :
  - created_at
  - updated_at
- Champs ID cachés dans les forms :
  - txtidOrigineRecommandation
  - txtidRecommandation
- Sélection de l'origine via `cboOrigineRecommandation` (pnlTop)
- Recherche active uniquement dans l'onglet recommandations
- Respect strict du modèle `GestionRefEnum`
- Champ `txtSourceURL` doit rester :
  - affiché en **CornflowerBlue**
  - cliquable
  - actif même en mode ReadOnly

---

## 📅 15/03/2026

### Added
- Référentiel Impression terminé avec tests complets
- Ajout du champ `notes_impression` (TEXT) dans la table `impression`
- Ajout du système de recommandations
- Nouvelles tables :
  - `recommandations`
  - `livres_recommandations`
  - `livres_staging_recommandations`
- Nouveau référentiel `ref_origine_recommandation`

### Changed
- Une recommandation représente un **événement documenté** provenant d'une source
- Champs principaux :
  - `id_recommandation`
  - `code_recommandation`
  - `id_origine_recommandation`
  - `source_nom`
  - `source_login`
  - `source_url`
  - `date_recommandation`
  - `commentaire`
  - `is_actif`
  - `created_at`
  - `updated_at`
- Ajout du champ `livres_staging.avec_fichier TINYINT(1) NOT NULL DEFAULT 0`

### Fixed
- Tables de liaison avec contrainte unique :
  - `UNIQUE (id_livre, id_recommandation)`
  - `UNIQUE (id_livre_staging, id_recommandation)`
- Gestion des séquences :
  - `seq_recommandations`
  - `seq_livres_recommandations`
  - `seq_livres_staging_recommandations`
- Les colonnes `id_*` utilisent : `DEFAULT (NEXT VALUE FOR seq_xxx)`

### Database
- Mise à jour de : meta_schema.schema_version et synchronisation (5) avec ExpectedSchemaVersion dans l'application

---

## 📅 13/03/2026

### Added
- Implémentation complète du référentiel **FormatFile**
- Ajout de la classe métier `FormatFile`
- Ajout des requêtes SQL dans `QueryModule` :
  - FormatFile_SelectAll
  - FormatFile_SelectBySearch
  - FormatFile_Insert
  - FormatFile_Update
  - FormatFile_Delete
  - FormatFile_CountLivresFichiers
- Implémentation dans `GestionReferentiel` :
  - FormatFile_GetAll
  - FormatFile_GetBySearch
  - FormatFile_Insert
  - FormatFile_Update
  - FormatFile_Delete
  - FormatFile_CountLivresFichiers

### Changed
- Création de la form `GestionFormatFile` sur le modèle des référentiels existants :
  - structure standard des régions
  - CRUD complet
  - recherche
  - synchronisation DataGridView / détails

### Fixed
- Correction d'un problème de synchronisation entre la sélection de la DataGridView et les champs détail
- Cause : mélange entre `SelectedRows(0)` et `CurrentRow`
- Correction : utilisation cohérente de `DataGridView.CurrentRow` dans `BindSelectedToDetails()`
- Résultat :
  - mise à jour correcte des détails lors d'un changement de sélection
  - comportement correct après modification ou suppression

---

## 📅 10/03/2026

### Added
- Création complète du référentiel **GestionEditeurs**
- Implémentation des requêtes SQL :
  - Editeurs_SelectAll
  - Editeurs_SelectBySearch
  - Editeurs_Insert
  - Editeurs_Update
  - Editeurs_Delete
  - Editeurs_CountLivres
- Ajout du champ `notes_editeur TEXT` dans la base de données
- Ajout d'un champ `RichTextBox` pour les notes éditeur
- Ajout de l'affichage du **nom du pays** dans la grille via `JOIN pays`
- Ajout d'une option de recherche incluant les notes via :
  - `Editeurs_SelectBySearch`
  - `Editeurs_SelectBySearchIncludingNotes`
- Ajout d'une checkbox UI permettant d'inclure ou non les notes dans la recherche

### Changed
- Masquage de `notes_editeur` dans la DataGridView
- Champ `site_web` rendu cliquable même en mode lecture
- Masquage automatique des colonnes techniques dans la grille :
  - id_editeur
  - id_pays
  - created_at
  - updated_at
  - notes_editeur

### Fixed
- Correction du bug de suppression supprimant la première ligne au lieu de la ligne sélectionnée
- Implémentation du bouton **Close**
- Implémentation du **Search / ClearSearch**

---

## 📅 09/03/2026

### Added
- Implémentation d'un système homogène de suppression pour les référentiels Phase 1
- Langues : Ajout du contrôle d'usage avant suppression
- Pays : Ajout du comptage d'usage dans :
  - `auteurs`
  - `auteurs_pays`
  - `editeurs`
- RefEnumType : Amélioration du flux de suppression :
  - navigation vers les valeurs liées
  - suppression complète type + valeurs possible
  - gestion transactionnelle
- RefEnum (valeurs) : Ajout d'un système complet de comptage d'usage

### Changed
- Message utilisateur informatif si la langue est encore utilisée
- Gestion correcte du comportement `SET NULL`
- Blocage applicatif si usage dans `auteurs_pays` (`RESTRICT`)
- Avertissement utilisateur si usage dans tables `SET NULL`
- Création des requêtes SQL de comptage dans `QueryModule`
- Ajout des fonctions correspondantes dans `GestionReferentiel`
- Mise à jour de `DeleteValeur()` :
  - blocage si FK `RESTRICT`
  - avertissement si FK `SET NULL`
  - suppression simple sinon

### Fixed
- Tables analysées :
  - `auteurs_pays`
  - `livres`
  - `livres_auteurs`
  - `livres_fichiers`
  - `livres_staging`
  - `livres_staging_auteurs`
- Stabilisation de la sélection automatique de la première ligne dans toutes les grilles
- Harmonisation des comportements de chargement (`LoadGrid`, `BindSelected...`)

---

## 📅 07/03/2026

### Added
- Ajout d'un comportement utilisateur lorsque la suppression d'un `ref_enum_type` est bloquée par des valeurs existantes
- Proposition de deux actions :
  - basculer vers les valeurs liées
  - suppression complète (type + valeurs) en une seule opération
- Implémentation d'une suppression transactionnelle `RefEnumType_DeleteWithValues`
- Création de la classe `Contact`
- Ajout de la région dans QueryModule :
  - `Contact_SelectAll`
  - `Contact_Insert`
  - `Contact_Update`
  - `Contact_Delete`
  - `Contact_ExistsByNom`
  - `Contact_ExistsByNomExceptId`
  - `Contact_CountUsageInLivresContacts`
- Ajout des méthodes dans GestionReferentiel :
  - `Contact_GetAll`
  - `Contact_Insert`
  - `Contact_Update`
  - `Contact_Delete`
  - `Contact_ExistsByNom`
  - `Contact_ExistsByNomExceptId`
  - `Contact_CountUsage`
- Création de la form `GestionContacts`

### Changed
- Stabilisation du formateur commun des DataGridView référentiels
- Gestion :
  - colonnes techniques masquées (`id_`)
  - colonnes principales en gras (`nom_`, `libelle_`)
  - alignement des codes
  - wrap automatique du texte
  - nettoyage des headers
- Début de réflexion pour suppression future de `HighlightMainColumn` devenu probablement redondant
- Analyse de la table `contacts`
- Vérification de la relation `livres_contacts → contacts`
- Design basé sur le modèle `GestionLangues`
- Ajout du champ `txtCodeContact` (lecture seule)
- Implémentation initiale :
  - variables privées
  - `GestionContacts_Load`
  - `LoadGrid`
  - `ClearContactDetails`
  - `BindSelectedContactToDetails`
  - `SetMode`
  - `SetStatus`

### Fixed
- Correction d'un problème de paramètre SQL (`@id` remplacé par `@id_enum_type`)

---

## 📅 05/03/2026

### Added
- Ajout des requêtes SQL dans `QueryModule` pour :
  - ref_enum_type
  - ref_enum
- Ajout des méthodes dans GestionReferentiel :
  - RefEnumType : GetAll, Insert, Update, Delete
  - RefEnum : GetByType, GetByTypeAndSearch, Insert, Update, Delete
  - RefEnum_CountByType (vérifier si un type possède encore des valeurs associées)
- Création et implémentation complète de l'écran `GestionRefEnum`
- Ajout d'un contrôle avant suppression :
  1. Vérifier le nombre de valeurs liées au type
  2. Si > 0 : suppression refusée + message utilisateur explicite
  3. Sinon : confirmation + suppression autorisée

### Changed
- Organisation des requêtes dans les régions correspondantes
- Chargement des types
- Chargement des valeurs selon type sélectionné
- Ajout / modification / suppression des types
- Ajout / modification / suppression des valeurs
- Recherche sur les valeurs
- Filtre valeurs actives
- Éviter erreur SQL brute
- Améliorer UX
- Conserver sécurité DB via FK

### Design
- Analyse de la table `ref_enum` :
  - FK vers `ref_enum_type`
  - comportement ON DELETE implicite = RESTRICT
- Conséquence : suppression d'un type avec valeurs impossible au niveau DB
- RefEnum devient le premier exemple de référentiel gérant :
  - table parent
  - table enfant
- Ce pattern servira pour d'autres structures similaires dans l'application

---

## 📅 04/03/2026

### Added
- Introduction d'une relation parent/enfant :
  - `ref_enum_type` → `ref_enum`
- Les tables métier continuent de référencer `ref_enum.id_enum`
- Création du design de la form `GestionRefEnum`
  - Structure :
	- `pnlTop`
	- `tabMain`
	  - `tabTypes`
	  - `tabValeurs`
	- `pnlActions`
	- `StatusStrip`
  - Fonctionnalités prévues :
	- gestion des **types d'énumérations**
	- gestion des **valeurs associées**
	- filtrage des valeurs par type
- Ajout de deux nouvelles classes :
  - `RefEnumType`
  - `RefEnumValeur`
  - Emplacement : `Classes/Referentiels/`

### Changed
- Décision de refactoriser le référentiel `ref_enum` en **deux tables distinctes** :
  - `ref_enum_type` (types d'énumération)
  - `ref_enum` (valeurs d'énumération)
- Conventions retenues :
  - `code_type` en majuscules
  - `code_valeur` en majuscules
  - tri des types : `ordre_affichage`, `libelle_type`
  - tri des valeurs : `ordre_affichage`, `libelle_valeur`

### Fixed
- Correction d'un problème de compilation causé par un copier/coller de designer
- Nettoyage du fichier `.Designer` après mélange involontaire de contrôles provenant d'un autre formulaire

---

## 📅 03/03/2026

### Added
- Création de la classe `Pays`
- Ajout région `PAYS` dans `QueryModule`
- Ajout région `PAYS` dans `GestionReferentiel` :
  - Pays_GetAll
  - Pays_GetBySearch
  - Pays_Insert
  - Pays_Update
  - Pays_Delete
  - Pays_SelectIdByNom
- Implémentation complète de la form `GestionPays` :
  - Load automatique (volume faible)
  - New / Edit / Cancel (Option B)
  - Save (Insert / Update)
  - Delete avec confirmation explicite
  - Recherche + Clear + Enter
- Validation ISO 3166 (longueur + majuscule)
- Ajout helpers génériques DataGridView dans `UtilsForms` :
  - DgvGetSelectedId
  - DgvSelectRowById
- Ajout standardisation visuelle des DataGridView :
  - FormatReferentielGrid
  - HighlightMainColumn

### Changed
- Simplification `HasSelectedXxx` via helper générique
- Remplacement repositionnement spécifique par `DgvSelectRowById`
- Harmonisation comportement Load automatique pour référentiels volume faible

### Architecture
- Validation du Pattern Référentiel sur 2 implémentations (Langues + Pays)
- Centralisation du style DataGridView pour tous les référentiels

---

## 📅 02/03/2026 - Référentiel Langues

### Added
- Création du modèle `Langue` (Classes/Referentiels)
- Ajout région `LANGUES` dans `QueryModule` :
  - Langues_SelectAll
  - Langues_SelectBySearch
  - Langues_Insert
  - Langues_Update
  - Langues_Delete
  - Langues_SelectIdByNomAbrev
- Implémentation CRUD `Langues` dans `GestionReferentiel`
- Création form `GestionLangues`
- Mise en place gestion des modes via Enum `ModeEdition` (partagé via UtilsForms)
- Implémentation LoadGrid + Binding sélection
- Implémentation workflow Option B (bouton Modifier explicite)
- Implémentation snapshot pour gestion Annuler
- Implémentation Save (Insert / Update) avec validation `errProvider`
- Repositionnement automatique sur enregistrement après sauvegarde
- Intégration ToolTips (`ttMain`)
- Affichage compteur (`lblCount`)

### Changed
- Abandon du `SplitContainer` (instabilité Designer)
- Adoption `TableLayoutPanel` pour layout liste/détails
- ISO 639-1 et 639-2 désormais stockés en minuscules (norme ISO)
- `abrev_langue` conservé en majuscules

### Fixed
- Incohérence ISO minuscules/majuscules
- Centralisation complète des requêtes SQL dans `QueryModule`
- Suppression SQL inline dans `GestionReferentiel`

### Architecture
- Séparation stricte :
  - UI → GestionLangues
  - SQL → QueryModule
  - Exécution DB → GestionReferentiel
  - Modèle métier → Classe Langue

---

## 📅 27/02/2026 (Partie 2)

### Added
- DB: tables `series_format` et `series_statut` (référentiels)
- DB: colonnes `series.id_series_format` et `series.id_series_statut` + contraintes FK
- DB: table `prixlit_categorie` (catégories de prix littéraires)
- DB: colonne `prixlit_annee.id_prixlit_categorie` + contrainte FK
- DB: sequences dédiées pour les nouvelles tables (conformément aux règles Artefact)
- DB: codes lisibles `code_series_format`, `code_series_statut`, `code_prixlit_categorie` (GENERATED STORED)

### Changed
- Version du schéma DB portée à **2** dans `meta_schema`
- `AppStartupManager.ExpectedSchemaVersion` porté à **2**

### Notes
- FK composite prix/catégorie non retenue (incompatible avec `ON DELETE SET NULL` sur colonne `id_prixLit` NOT NULL). Contrôle de cohérence géré côté UI, triggers optionnels ultérieurs

---

## 📅 26/02/2026 (Partie 1)

### Added
- Ajout d'un bouton "œil" pour visualisation temporaire du mot de passe
- Création / unification de `BuildConnectionString(cfg)` comme source unique
- Ajout d'un bloc Try/Catch autour du déchiffrement DPAPI
- Ajout Try/Catch IO dans `SauvegarderConfigDb`
- Gestion explicite des exceptions :
  - Base64 invalide
  - CryptographicException (utilisateur différent / données corrompues)
- Gestion explicite de CryptographicException dans `EncryptStringToBase64`

### Changed
- Affichage en clair uniquement tant que le bouton est enfoncé
- Masquage automatique au relâchement / sortie souris
- Visible uniquement en mode `PasswordMode.SetNew`
- Centralisation de la gestion visibilité bouton + état masque
- Appelé systématiquement après modification de `_pwMode`
- Normalisation automatique `localhost` → `127.0.0.1`
- Sécurisation port (borne 1–65535)
- Assainissement `OptionsConn` (ajout `;` si nécessaire)
- Vérification stricte du mode KeepExisting (mot de passe existant requis)
- Validation sans MsgBox
- StatusStrip + Focus sur contrôle fautif
- Log Rapide/UI uniquement si KO
- Suppression MsgBox en cas de succès
- Logs cohérents (niveau + catégorie)
- MsgBox conservée uniquement pour blocage critique (enregistrement refusé)
- Suppression de toute logique dupliquée de construction de connection string
- Log Succinct + exception complète en cas d'échec
- Remontée de l'exception (pas de swallow)
- Logging PROD standardisé (Level + Category + ex)
- Catégorie `Process`
- Gestion propre des erreurs JSON
- Log en cas d'échec + Throw
- Suppression du code redondant de création dossier
- Unification du path via `GetArtefactFolderPath()`
- Utilise désormais `GetArtefactFolderPath()` (source unique)
- Suppression de la constante APP_FOLDER_NAME devenue obsolète
- Throw enrichi avec InnerException conservée
- Aucun logging interne (séparation des responsabilités)

### Fixed
- Ajout Try/Catch global autour de RunStartupFlow()
- Log Succinct + exception complète en cas d'erreur inattendue
- Message critique uniquement si l'application ne peut démarrer
- Logs Startup explicites
- Mise à jour StatusStrip au verrouillage/déverrouillage
- Logs catégorisés :
  - Startup → catégorie Startup
  - UI → catégorie UI
  - DB → catégorie Database
  - Config locale → catégorie Process
- Harmonisation logs
  - Catégorie Process pour config locale
  - Catégorie Database pour erreurs DB / schéma
  - Catégorie Startup pour orchestration
- Vérification version via `EnsureSchemaCompatible(conn, ExpectedSchemaVersion)`
- Mismatch identifié via préfixe structuré `SchemaMismatch|`

---

## 📅 25/02/2026

### Added
- Ajout d'un bloc Try/Catch autour du déchiffrement DPAPI
- Log Succinct + exception complète en cas d'échec
- Remontée de l'exception (pas de swallow)

### Changed
- Création / unification de `BuildConnectionString(cfg)` comme source unique
- Suppression de toute logique dupliquée de construction de connection string
- Logging PROD standardisé (Level + Category + ex)
- Catégorie `Process`
- Gestion propre des erreurs JSON
- Ajout Try/Catch IO
- Log en cas d'échec + Throw
- Suppression du code redondant de création dossier
- Unification du path via `GetArtefactFolderPath()`
- Utilise désormais `GetArtefactFolderPath()` (source unique)
- Suppression de la constante APP_FOLDER_NAME devenue obsolète

### Fixed
- Gestion explicite des exceptions :
  - Base64 invalide
  - CryptographicException (utilisateur différent / données corrompues)
- Throw enrichi avec InnerException conservée
- Aucun logging interne (séparation des responsabilités)
- Gestion explicite de CryptographicException

---

## 📅 23/02/2026

### Added
- Ajout support natif des exceptions :
  - ex.Message (Succinct)
  - ex.Message + StackTrace + InnerException (Complet)
- Masquage des données sensibles (Password / Pwd)
- Écriture thread-safe (SyncLock)
- Header de session écrit au premier log de chaque exécution :
  - Séparateur visuel
  - Date/Heure
  - MachineName
  - UserName Windows
- Logs journaliers : Artefact_YYYY-MM-DD.log
- Dossier : %APPDATA%\Artefact\Logs
- Création automatique des dossiers
- Purge automatique > 7 jours

### Changed
- Suppression du mécanisme de réduction (NiveauActif)
- Niveaux conservés comme marqueurs de profondeur :
  - Rapide
  - Succinct
  - Complet
- Suppression Console.WriteLine (prod orienté fichier)
- Maintien Debug.WriteLine (utile en dev)
- Mise à jour RunStartup()
  - Logging adaptatif :
	- Succès → Rapide/Succinct
	- Erreur → Succinct + Complet (avec exception)
  - Ajout contexte technique (Host, Port, DB, User) sans secret

### Architecture
- Process Startup stabilisé
- Logging V1 considéré comme baseline production
- Architecture cohérente (Startup centralisé, logs centralisés)

---

## 📅 22/02/2026

### Added
- Table `meta_schema` :
  - id (PK)
  - schema_version INT
  - applied_at DATETIME
  - notes VARCHAR(255)
- Initialisation schema_version = 1 (version initiale officielle Artefact V1)
- `AppStartupManager` (Infrastructure)
  - Orchestration complète du démarrage :
	- Lecture config locale
	- Test connexion MariaDB
	- Vérification version schéma
  - Enum `StartupStatus`
  - Class `StartupResult`

### Changed
- `DatabaseManager.EnsureSchemaCompatible`
  - Suppression dépendance à `SchemaInfo`
  - Ajout paramètre `expectedVersion`
  - Exception structurée : `SchemaMismatch|DB=X|Expected=Y`

### Tests
- Test nominal :
  - schema_version = 1
  - StartupStatus = Ok
- Test mismatch :
  - schema_version = 999
  - Détection correcte → Status = SchemaMismatch
  - Message d'incompatibilité affiché
  - Log correctement alimenté

### Architecture
- Versionnement DB opérationnel
- Garde-fou validé
- Flux définitif de démarrage non encore implémenté (Home encore en mode test TEMP)

---

## 📅 21/02/2026

### Changed
- Suppression de la variable `_changerMotDePasse`
- Introduction de l'enum `PasswordMode` (KeepExisting / SetNew)
- Ajout de la variable `_pwMode`
- Centralisation de la construction du modèle via `BuildConfigFromUI()`
- Suppression de `LireConfigDepuisChamps()` (obsolète)
- `btnTester_Click` utilise exclusivement `BuildConfigFromUI()`
- `btnEnregistrer_Click` :
  - Test obligatoire des valeurs écran avant sauvegarde
  - Sauvegarde uniquement si test OK
  - Fermeture uniquement si sauvegarde réussie

### Fixed
- Vérification que `TesterConnexion(cfg)` ne dépend que du paramètre `cfg`
- Confirmation du comportement normal du chiffrement DPAPI (préfixe Base64 constant)
- Identification d'un comportement inattendu côté MariaDB lors de l'utilisation de `root` en local

### Architecture
- Form logique stabilisée
- Comportement restant à valider côté authentification MariaDB

---

## 📅 18/02/2026

### Added
- Création de `DatabaseManager.vb` (dossier Core)
- Implémentation :
  - InitializeMariaDb()
  - GetConnexionMariaDB()
  - Construction dynamique de la connection string
- Mise en place d'un système bootstrap temporaire pour lecture de `param_db`
- Installation de **MySqlConnector** (bgrainger)
- Création de `QueryModule.vb`
- Ajout de la requête SelectParamDbByNomConnexion
- Création d'un `GestionLog` minimal (bootstrap)
- Écriture Debug + Console
- Prévu : évolution vers log fichier via param_paths
- Rédaction du header structuré versionné
- Création d'un bouton de test connexion
- Organisation par panel (pnlConnexion)

### Changed
- Suppression dépendance prématurée à ConfigManager
- Abandon de MySql.Data
- Alignement avec .NET 8 et architecture moderne

### Architecture
- Core = infrastructure système
- Utils = outils génériques
- Une classe = une responsabilité principale
- Initialisation explicite au démarrage + fallback lazy
- Phase Infrastructure en cours
- Prochaine étape : stabilisation ConfigManager v2

---

## 📅 16/02/2026

### Added
- `param_db` : connexions bases de données
- `param_paths` : chemins applicatifs
- `param_api` : services API et URLs
- `param_app` : paramètres applicatifs généraux
- Création des séquences associées
- Champ `password_enc` (BLOB)
- Chiffrement DPAPI Windows côté application
- Écran dédié pour modification du mot de passe

### Changed
- Abandon du modèle `parametres` unique au profit d'une architecture séparée
- Aucun secret stocké en clair dans la base
- Répartition des paramètres existants :
  - param_paths : Path_General, Path_DBCalibre, Path_Biblio_Calibre, Path_Data (renommé Artefact)
  - param_db : MariaDB_Artefact, Calibre_SQLite
  - param_api (jusqu'à ancien enregistrement 22) : OPENAI, GOOGLE_BOOKS, CHATGPT_WEB
- Les autres services/API sont reportés à une phase ultérieure

### Architecture
- Nouvelle structure modulaire et évolutive pour la gestion des paramètres
- Base prête pour implémentation VB.NET

---

## 📅 15/02/2026

### Added
- Création des tables :
  - `livres_auteurs` (avec rôle via ref_enum)
  - `livres_tags`
  - `livres_prixLit_annee`
  - `livres_contacts` (version traçable avec date_envoi et id_livre_fichier)
- Ajout des colonnes dans `livres` :
  - `id_serie` (FK vers `series`)
  - `num_tome`
  - `tome_libelle`
  - `id_calibre`
- Ajout des index :
  - `idx_livres_serie`
  - `idx_livres_serie_tome`
  - `idx_livres_id_calibre`
- Insertion des rôles :
  - AUTEUR
  - COAUTEUR
  - TRADUCTEUR
  - ILLUSTRATEUR
  - DIRECTEUR
  - PREFACE
  - POSTFACE
- Création de :
  - `livres_staging`
  - `livres_staging_auteurs`
- Ajout des référentiels :
  - `source_import`
  - `statut_staging`
- Ajout de la FK `livres_fichiers.id_livre_staging` → `livres_staging.id_livre_staging`

### Architecture
- Le modèle relationnel côté livres est désormais complet et cohérent
- Pipeline Import → Staging → Normalisation prêt à être testé

---

## 📅 14/02/2026

### Added
- Mise en place d'une table unifiée pour gérer :
  - les fichiers des livres normalisés
  - les futurs fichiers des livres staging
- Ajout des champs :
  - `id_scope_livre` (ref_enum)
  - `id_type_fichier` (ref_enum)
  - `id_formatFile` (FK formatFile)
  - `hash_sha1`
  - `is_principal`
- Mise en place d'une SEQUENCE dédiée
- La FK vers `livres_staging` sera ajoutée ultérieurement
- Ajout des colonnes :
  - `mime_type`
  - `ordre_affichage`
- Insertion des formats principaux :
  - EPUB
  - AZW3
  - PDF
  - CBR
  - CBZ
  - MOBI
- Définition de la règle métier : Le fichier principal est celui dont `ordre_affichage` est le plus bas
- Création de la table de liaison `auteurs_pays`
- PK composite : `(id_auteur, id_pays, id_type_relation_pays)`
- Utilisation de `ref_enum` avec le type `type_relation_auteur_pays`
- Valeurs insérées :
  - NAISSANCE
  - RESIDENCE
  - NATIONALITE
  - ECRITURE
  - CULTURE
- Décision d'intégrer Québec comme entité dans `pays`
- Pas de SEQUENCE ni de code généré pour cette table de liaison

### Changed
- Validation que les tables migrées utilisent bien `SEQUENCE + DEFAULT nextval(...)`
- Confirmation que l'affichage `AUTO_INCREMENT` dans HeidiSQL est un artefact visuel
- Vérification via `SHOW CREATE TABLE` et `information_schema`

### Architecture
- Architecture relationnelle enrichie
- Structure fichiers stabilisée
- Base cohérente et extensible
- Prête pour les tables de liaison pour `livres` :
  - `livres_auteurs`
  - `livres_tags`
  - `livres_prixLit_annee`
  - `livres_contacts`

### Fixed
- Ajout à prévoir de `id_serie` dans `livres` (FK vers `series`) : champ oublié lors de la création initiale

---

## 📅 13/02/2026

### Added
- Création complète de la table avec :
  - id_livre basé sur SEQUENCE
  - code_livre généré automatiquement
  - titre / titre_normalise
  - annee_publication / date_publication
  - synopsis
  - commentaire
  - FK vers langues, impression, editeurs
  - FK vers ref_enum pour :
	- id_statut_lecture
	- id_support_lecture
	- id_type_acquisition
- Ajout des index de recherche
- Création d'une table générique pour remplacer les ENUM SQL natifs
- Utilisée pour :
  - statut_lecture
  - support_lecture
  - type_acquisition
  - type_fichier
- Ajout des valeurs initiales :
  - Lu / Non lu / En cours
  - Kindle / Audible / Papier
  - Achat / Down / Abonnement / Prêt
  - Ebook / Cover

### Changed
- Adoption du préfixe `B` pour `code_livre` (Book)

### Architecture
- Validation du principe :
  - Intégrité garantie par FK
  - Filtrage des valeurs par type effectué côté application
  - Abandon définitif des ENUM SQL natifs

---

## 📅 12/02/2026

### Added
- Adoption officielle du nom `livres_staging` (remplace anciens termes)
- Clarification de la séparation :
  - `livres` = données validées
  - `livres_staging` = données importées en attente de validation
- Confirmation que `livres` et `livres_staging` concernent exclusivement des ouvrages disposant d'un fichier e-book
- Distinction claire avec les futures tables de nouveautés / recommandations (sans obligation de fichier)
- Remplacement du champ `resume` par `synopsis`
- Validation du modèle avec une seule table `livre_fichier` pour gérer :
  - e-book
  - couvertures
  - autres fichiers liés
- Séparation du concept en deux champs distincts :
  - `id_statut_lecture`
  - `id_support_lecture`
- Maintien des champs normalisés dans les tables principales
- Ajout prévu d'une normalisation spécifique pour le nom d'auteur seul
- Rejet des ENUM SQL natifs
- Orientation vers tables référentielles typées ou dédiées

---

## 📅 11/02/2026

### Added
- Nouvelle définition standard :
```sql
CREATE SEQUENCE seq_<table>
	START WITH 1
	INCREMENT BY 1
	MINVALUE 1
	NO MAXVALUE
	CACHE 1
	NOCYCLE;
```

### Changed
- Incompatibilité entre AUTO_INCREMENT et colonnes GENERATED STORED (erreur 1901)
- Impossibilité d'utiliser UPDATE dans un trigger AFTER INSERT (erreur 1442)
- Sauts d'identifiants dus au cache par défaut des SEQUENCE (CACHE 1000)
- Abandon officiel de AUTO_INCREMENT
- Adoption du modèle :
  - 1 SEQUENCE dédiée par table
  - id_xxx alimenté via DEFAULT (NEXT VALUE FOR seq_xxx)
  - code_xxx généré via GENERATED ALWAYS AS (...) STORED

---

## 📅 09/02/2026

### Added
- Création d'une nouvelle solution Visual Studio
- Ciblage : **VB.NET – .NET 8 LTS – WinForms**
- Abandon officiel de .NET Framework 4.8 pour cette version
- Validation du choix "modernité maîtrisée"
- Création DB avec :
  - Charset : `utf8mb4`
  - Collation : `utf8mb4_uca1400_ai_ci`
- Mise en place des **tables référentielles** :
  - `auteurs`
  - `series` (remplace collections)
  - `contacts`
  - `editeurs`
  - `tags`
  - `impression`
  - `langues`
  - `pays`
  - `prixLit` (remplace prix_rec)
  - `prixLit_Annee`
  - `formatFile` (remplace types_ebooks)
- Suppression de `news_auteur`
- Conservation de `chemin_photo_auteur`
- Ajout de `annee_naissance`
- Mise en place de triggers pour synchroniser automatiquement `annee_naissance` si `date_naissance` est renseignée
- `adresse_kindle` renommé en `adresse_liseuse`
- Ajout de `type_liseuse`
- Ajout du champ `note` dans impression

### Changed
- Adoption du modèle :
  - 1 **SEQUENCE dédiée par table**
  - `id_xxx` alimenté par :
```SQL
DEFAULT (NEXT VALUE FOR seq_xxx)
```
  - `code_xxx` généré automatiquement :
```SQL
GENERATED ALWAYS AS (CONCAT('X', LPAD(id_xxx, 6, '0'))) STORED
```

### Architecture
- Séparation stricte :
  - `id_xxx` = technique
  - `code_xxx` = lisible humain
  - Pas d'usage de `code_xxx` comme clé étrangère
  - Référentiels créés avant tables principales
  - Documentation et règles posées dès le départ

---

## 📅 08/02/2026

### Added
- Création d'un README propre au projet Artefact
- Rédaction des règles de conception DB
- Rédaction du TODO détaillé pour les prochaines phases

### Changed
- Abandon du projet Artefactothèque comme base évolutive
- Décision de repartir de zéro, en tenant compte cependant de l'expérience acquise et des besoins identifiés
- Nouvelle application : Artefact
- Nouvelle base de données : Artefact
- Objectif : simplification et cohérence
- Fusion conceptuelle des genres / étiquettes dans une table tags
- Réduction des duplications inutiles
- Réflexion approfondie sur :
  - gestion des profils
  - gestion des IDs
  - structure référentielle propre
- Mise en place d'une logique "structure d'abord, code ensuite"

### Architecture
- Artefact v1 est désormais basé sur :
  - Modernité maîtrisée (.NET 8)
  - MariaDB structurée proprement
  - IDs séquentiels robustes
  - Codes lisibles stockés
  - Refactorisation réfléchie
  - Documentation continue

---

*Document maintenu en parallèle du développement.*  
*Toute modification structurante doit être documentée ici.*
