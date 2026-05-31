# Artefact

---

## Pourquoi Artefact ?

Artefact est né d'une frustration concrète : après des années d'usage de Calibre et des milliers de livres gérés, certains besoins personnels restaient mal couverts.

Ce projet est à la fois un laboratoire technique, un projet plaisir et une bibliothèque de cœur. L'objectif est de construire une application robuste, élégante et évolutive, qui reste fidèle à ma façon de lire, classer et explorer mes livres.

## Description

**Artefact** est une application Windows Forms développée en **VB.NET (.NET 8 LTS)** destinée à la gestion avancée d'une bibliothèque personnelle de livres numériques, implémentant des fonctionnalités de normalisation, d'enrichissement et de recommandation.

Le projet est une **reconstruction volontaire et maîtrisée** d'Artefactothèque, avec un objectif clair : simplifier, normaliser, structurer proprement et documenter dès le départ.

`Artefact` est un outil développé pour gérer une bibliothèque personnelle enrichie avec importation depuis **Calibre**, et d'autres sources de e-books, avec un stockage dans **MariaDB**, et gestion via une interface **VB.NET (WinForms)**, avec plus tard une version pour le Web.

L'IA est fortement impliquée pour enrichir les fiches de livres, proposer des recommandations, des nouveautés, les dates de sortie, proposer des news et automatiser certaines tâches de gestion.

Icône Artefact : <img src="Assets/Icons/Artefact.png" alt="Artefact" width="64" />

## Objectifs

- Concevoir une **base de données propre, cohérente et extensible**
- Séparer strictement : données validées / données en cours de normalisation
- Éliminer les dérives structurelles héritées du POC
- Poser une architecture saine pour : import Calibre, enrichissement externe, intégration IA future
- Documenter chaque décision structurante

## Vision IA

L'IA est prévue comme un moteur d'enrichissement progressif : aide au résumé, détection d'incohérences, suggestions de métadonnées, recommandations contextualisées et automatisation de certaines tâches répétitives.

L'objectif n'est pas de remplacer le jugement humain, mais de gagner du temps sur les étapes à faible valeur et de garder l'énergie pour la curation.

## Philosophie générale

Artefact repose sur :
- séparation stricte validation / import
- aucune donnée floue dans `livres`
- intégrité référentielle forte
- extensibilité maîtrisée
- documentation continue

---

## Environnement technique

### Application

- **Visual Studio Community 2026** - Version 18.6.1
- **VB.NET** - .NET 8 LTS
- **Windows Forms**

### Base de données

**MariaDB 12.1.2**
- Charset : `utf8mb4`
- Collation : `utf8mb4_uca1400_ai_ci`
- Nom base : `Artefact`

**Outils** :
- **HeidiSQL 12.17** : Gestion DB, requêtes SQL, visualisation
- **DBeaver 26.0.5** : Gestion DB, requêtes SQL, diagrammes

### Outils divers

- **Typora 1.13.6** : Documentation Markdown

### Versioning et sauvegarde

**GitHub** :
- Private : https://github.com/AngeljoNG/Artefact (mise à jour continue)
- Public : https://github.com/Les-Artefacts-de-Manou/Artefact (dernière MàJ : 2026-05-22)

### Calibre 9.9 (Kovid Goyal)

Fichier `Metadata.db` de Calibre, utilisé pour l'importation des livres qui ont déja été insérés antérieurement dans l'application Calibre. Ce fichier est copié sous le nom de `myMetadata.db` dans le dossier `MDCalibre` pour une utilisation locale.

La copie du fichier se fait avec 2 fichiers .bat, pour l'instant manuellement, mais qui seront automatisés dans l'application.

- **Copie du fichier Metadata.db** : 
  - `CopyMetadata.bat` : copie le fichier Metadata.db de Calibre vers le dossier `MDCalibre` sous le nom `metadata_%curdate%.db` et nettoie les anciens fichiers
  - `ReplaceMyMetadata.bat` : `metadata_%curdate%.db` renommé en `myMetadata.db`
- **DBBrowser for SQLLite** : V. 3.13.1 - utilisé pour visualiser le fichier `myMetadata.db` de Calibre

### Packages et bibliothèques

**MySqlConnector 2.5.0**
- Par Bradley Grainger - https://mysqlconnector.net/
- Driver ADO.NET pour MariaDB sous .NET 8

---

## Connexion à la base de données

Artefact utilise le package **MySqlConnector** pour la connexion MariaDB sous .NET 8.

### Philosophie

- Point d'accès unique : `DatabaseManager`
- Aucune connexion ouverte manuellement ailleurs
- Utilisation systématique du bloc `Using`
- Pooling ADO.NET activé

---

## Architecture Solution

```
Artefact/
│
├── ApplicationEvents.vb                → Événements application WinForms
│
├── Core/                               → Couche technique/infrastructure
│   ├── Configuration/
│   │   ├── ConfigLocalManager.vb      → Chargement config locale (JSON)
│   │   └── LocalDbConfig.vb           → Modèle configuration DB MariaDB
│   │
│   ├── Database/                       → Accès base de données
│   │   └── DatabaseManager.vb         → Point d'accès unique à MariaDB
│   │
│   ├── Query/                          → Requêtes SQL centralisées par domaine
│   │   ├── QueryModule.vb             → Module requêtes de base + utilitaires
│   │   ├── QueryModule.ContactsEditeurs.vb
│   │   ├── QueryModule.FormatFileImpression.vb
│   │   ├── QueryModule.PrixLit.vb
│   │   ├── QueryModule.Recommandations.vb
│   │   └── QueryModule.RefEnum.vb
│   │
│   ├── Logging/                        → Journalisation
│   │   └── GestionLog.vb              → Gestion des logs (niveaux, rotation)
│   │
│   ├── Security/                       → Sécurité et chiffrement
│   │   └── CryptoManagerDPAPI.vb      → Chiffrement mot de passe (DPAPI Windows)
│   │
│   └── Startup/                        → Initialisation application
│       └── AppStartupManager.vb       → Démarrage, vérification config, init DB
│
├── Classes/                            → Modèles métier (entités du domaine)
│   └── Referentiels/
│       ├── Contact.vb
│       ├── Editeur.vb
│       ├── FormatFile.vb
│       ├── Impression.vb
│       ├── Langue.vb
│       ├── Pays.vb
│       ├── PrixLit.vb
│       ├── PrixLitAnnee.vb
│       ├── PrixLitCategorie.vb
│       ├── Recommandation.vb
│       ├── RefEnumType.vb
│       ├── RefEnumValeur.vb
│       └── RefOrigineRecommandation.vb
│
├── Metier/                             → Couche métier / logique applicative
│   └── Referentiels/
│       ├── GestionReferentiel.vb
│       ├── GestionReferentiel.ContactsEditeurs.vb
│       ├── GestionReferentiel.FormatFileImpressions.vb
│       ├── GestionReferentiel.PrixLit.vb
│       ├── GestionReferentiel.Recommandations.vb
│       └── GestionReferentiel.RefEnum.vb
│
├── UI/                                 → Interface utilisateur
│   ├── Forms_Portail/
│   │   ├── home.vb                    → Menu principal de l'application
│   │   ├── home.Designer.vb
│   │   ├── home.resx
│   │   ├── PortailReferentiels.vb     → Portail hébergeant les UserControls
│   │   ├── PortailReferentiels.Designer.vb
│   │   └── PortailReferentiels.resx
│   │
│   ├── Forms_Config/
│   │   ├── GestionConnexionMariaDb.vb
│   │   ├── GestionConnexionMariaDb.Designer.vb
│   │   └── GestionConnexionMariaDb.resx
│   │
│   └── UserControls_Referentiels/
│       ├── UC_Langues.vb              → UC référentiel Langues
│       ├── UC_Pays.vb                 → UC référentiel Pays
│       ├── UC_Contacts.vb             → UC référentiel Contacts
│       ├── UC_Editeurs.vb             → UC référentiel Éditeurs
│       ├── UC_FormatFile.vb           → UC référentiel Formats
│       ├── UC_Impression.vb           → UC référentiel Impressions
│       ├── UC_RefEnum.vb              → UC référentiel Énumérations (hiérarchique)
│       ├── UC_Recommandations.vb      → UC référentiel Recommandations (hiérarchique)
│       └── UC_PrixLit.vb              → UC référentiel Prix littéraires (hiérarchique)
│
├── Utils/    
│   └── Context/                       → Contexte partagé pour UserControls 
│       ├── IContextAwareUserControl.vb    → Interface pour contexte partagé
│       └── UserControlContext.vb          → Contexte partagé pour tous les UC
│   └── Helpers/                  → Helpers et utilitaires transverses
│       ├── RichTextNotesHelper.vb         → Utilitaires gestion notes RTF/TXT
│       ├── UtilsForm.vb                   → Utilitaires WinForms (DataGridView, ModeEdition)
│       └── UtilsUCReferentiels.vb         → Helpers partagés pour UserControls référentiels
│   └── Navigation/                         → Gestion de la navigation et du fil d'Ariane
│       └── NavigationManager.vb           → Gestionnaire de navigation et fil d'Ariane
│
├── Docs/                               → Documentation projet
│   ├── ADR_Artefact.md
│   ├── GLOSSAIRE.md
│   ├── Process_Artefact.md
│   ├── Documentation_technique_UI.md
│   ├── REPRISE.md
│   ├── Rules.md
│   ├── TODO.md
│   ├── VISION.md
│   ├── Database/
│   │   ├── ModeleDB.md
│   │   ├── artefact_schema_erdiagram.mmd
│   │   └── Diagrams/
│
├── Assets/                             → Ressources statiques
│   └── Icons_Appli/
│       ├── Artefact.ico
│       ├── Artefact.png
│   └── Icons_Tech/
│       ├── Calibre.png
│       ├── Forms.ico
│       ├── statut_ko.png
│       ├── statut_ok.png
│       └── œil.png
│
└── README.md                         → Configuration projet VB.NET
└── CHANGELOG.md			→ Historique des modifications
```

### Architecture en 4 couches

1. **Core** : Infrastructure technique (DB, config, logs, sécurité, requêtes SQL)
2. **Classes** : Modèles de données (entités métier)
3. **Metier** : Logique applicative centralisée (business logic)
4. **UI** : Présentation (Forms + UserControls)
   - **Forms_Portail** : Menu principal (`home.vb`) + Portail référentiels (`PortailReferentiels.vb`)
   - **UserControls_Referentiels** : UC des référentiels (architecture moderne)
   - **Forms_Config** : Configuration connexion DB

---

## Migration Forms → UserControls (Mars 2026)

Les anciennes Forms de gestion (`GestionImpression`, `GestionRecommandations`, `GestionPrixLit`) ont été **migrées vers des UserControls** hébergés dans `PortailReferentiels` pour une architecture unifiée, maintenable et cohérente.

### Bénéfices

- **Contexte partagé** : navigation, tooltips, ErrorProvider, StatusStrip
- **Code factorisé** : helpers centralisés (`UtilsUCReferentiels`)
- **Interface homogène** : navigation fluide, fil d'Ariane dynamique
- **Maintenance simplifiée** : patterns standardisés

### Structure UI actuelle

#### Forms_Portail

- **`home.vb`** : Menu principal de l'application (point d'entrée)
- **`PortailReferentiels.vb`** : Portail hébergeant tous les UserControls de référentiels avec contexte partagé

#### Forms_Config

- **`GestionConnexionMariaDb.vb`** : Configuration connexion base de données avec test intégré

#### UserControls_Referentiels

**UC simples (1 table)** : 
- `UC_Langues`, `UC_Pays`, `UC_Contacts`, `UC_Editeurs`, `UC_FormatFile`, `UC_Impression`

**UC hiérarchiques (master-detail)** : 
- `UC_RefEnum` (types + valeurs)
- `UC_Recommandations` (origines + recommandations)
- `UC_PrixLit` (prix → catégories → années)

**Tous les UC partagent** : 
- Contexte unifié (`UserControlContext`)
- Modes d'édition standardisés (`ModeEdition`)
- Validation centralisée
- Helpers communs (`UtilsUCReferentiels`)
- Navigation avec fil d'Ariane (`NavigationManager`)

---

## Composants transverses

### Contexte partagé (UserControlContext)

Tous les UC reçoivent un contexte unifié depuis `PortailReferentiels` contenant :
- `ToolTip` partagé
- `ErrorProvider` partagé
- `StatusStrip` partagé
- `NavigationManager` pour fil d'Ariane

Interface : `IContextAwareUserControl`

### Helpers partagés (UtilsUCReferentiels)

Module centralisant les fonctions communes aux UC :
- `ConfigurerStyleGrid` : Style DataGridView uniforme
- `ValidateRequiredField` : Validation champs requis
- `ConfigurerBoutonsMode` : Gestion états boutons selon mode
- `ConfigurerRecherche` : Configuration zone de recherche
- `DbToBool`, `DbToInt`, `SafeULong` : Conversions sécurisées
- `HideTechnicalColumns` : Masquage colonnes techniques (ID, codes)
- `UpdateCountLabel` : Mise à jour compteur d'enregistrements

### Gestion des notes enrichies (RichTextNotesHelper)

Système standardisé pour notes formatées :
- Stockage dual : `_rtf` (formaté) + `_txt` (recherche)
- Toolbar standard : gras, italique, souligné, liste, tabulation
- Synchronisation automatique RTF ↔ TXT

---

## Classes et Modules

### Core

**Classes** :
- `DatabaseManager` : Point d'accès unique MariaDB (connexion, requêtes, transactions)
- `LocalDBConfig` : Configuration DB locale (JSON, DPAPI)

**Modules** :
- `GestionLog` : Journalisation (niveaux, catégories, rotation)
- `QueryModule` : Requêtes SQL centralisées (CRUD par domaine)
- `ConfigLocalManager` : Configuration locale (JSON)
- `CryptoManagerDPAPI` : Chiffrement données sensibles (DPAPI Windows)

### Utils

- `UtilsForm` : Utilitaires WinForms (DataGridView, validation, `ModeEdition`)
- `UtilsUCReferentiels` : Helpers partagés pour UserControls référentiels
- `RichTextNotesHelper` : Gestion notes enrichies (RTF/TXT)
- `InputHelpers` : Validation saisie clavier
- `UserControlContext` : Contexte partagé pour tous les UC
- `NavigationManager` : Navigation et fil d'Ariane

### Metier

- `GestionReferentiel` : Logique métier référentiels (CRUD, validation)
  - Partialisé par domaine : `ContactsEditeurs`, `FormatFileImpressions`, `PrixLit`, `Recommandations`, `RefEnum`

### Classes métier

Modèles : `Langue`, `Pays`, `Contact`, `Editeur`, `FormatFile`, `Impression`, `Recommandation`, `RefOrigineRecommandation`, `PrixLit`, `PrixLitCategorie`, `PrixLitAnnee`, `RefEnumType`, `RefEnumValeur`

---

## Démarrage & Connexion MariaDB

Artefact démarre toujours en vérifiant la connexion à MariaDB via une configuration locale sécurisée.

### Configuration locale

- Fichier : `%APPDATA%\Artefact\artefact.local.json`
- Contient : Host, Port, Database, UserName, Options
- Mot de passe : chiffré via DPAPI (Base64)
- Le JSON est la **source de vérité au démarrage**

### Flux de démarrage

1. `home.vb` s'ouvre (menu principal)
2. Lecture du fichier JSON local
3. Tentative de connexion MariaDB
4. Si connexion OK → activation de la navigation
5. Si échec → ouverture de `GestionConnexionMariaDb`

### GestionConnexionMariaDb

Permet :
- Création ou modification des paramètres de connexion
- Test en temps réel
- Sauvegarde locale
- Chiffrement automatique du mot de passe
- Modification explicite du mot de passe uniquement sur demande

### Versionnement du Schéma

Artefact utilise un versionnement interne du schéma de base de données.

- Table concernée : `meta_schema`
- Champ clé : `schema_version`
- Vérification effectuée au démarrage de l'application

Version actuelle : **6** (modifié : 2026-03-20)

### Utilisateurs MariaDB

Deux comptes distincts :
- `root` → administration (DDL, gestion structure)
- `artefact_app` → exploitation applicative

Droits applicatifs : SELECT, INSERT, UPDATE, DELETE, EXECUTE, SHOW VIEW

### Remarques importantes

- Toujours utiliser `127.0.0.1` (et non `localhost`) pour forcer TCP
- Éviter l'utilisation du compte `root` dans l'application
- Le préfixe commun des mots de passe chiffrés DPAPI est normal

---

## Système de Logging

Artefact dispose d'un système de logging production-ready.

### Caractéristiques

- Fichier journalier : `Artefact_YYYY-MM-DD.log`
- Emplacement : `%APPDATA%\Artefact\Logs`
- Rotation automatique (7 jours)
- Thread-safe
- Masquage des secrets
- Header de session à chaque lancement

### Niveaux

- **Rapide** : jalons majeurs
- **Succinct** : états et erreurs significatives
- **Complet** : détails techniques (stack trace, inner exception)

---

## Sécurité & Configuration

Artefact utilise DPAPI (DataProtectionScope.CurrentUser) pour protéger les mots de passe de connexion MariaDB.

### Principes

- Le mot de passe est stocké chiffré en Base64 dans `artefact.local.json`
- Le déchiffrement est effectué uniquement au moment de la construction de la connection string
- Ne déchiffre jamais le mot de passe pour affichage
- Permet une visualisation temporaire via bouton "œil"
- N'affiche jamais le mot de passe dans les logs

---

## Référentiels – Pattern Implémenté

Le référentiel `Langues` constitue le modèle de référence pour tous les futurs référentiels.

### Structure

- Classe métier dédiée (ex: `Langue`)
- Requêtes SQL centralisées dans `QueryModule`
- Exécution via `GestionReferentiel`
- UI dédiée par entité (UserControl)

### Workflow

- Consultation par défaut
- Modification via bouton explicite
- Annulation via snapshot interne
- Validation locale via `ErrorProvider`
- Messages utilisateur via `StatusStrip`

### Design

- TableLayoutPanel privilégié pour stabilité du Designer
- Tous les référentiels utilisent une structure UC/coque homogène
- Style DataGridView centralisé
- Gestion des modes uniforme (Consultation / Nouveau / Modification)

### Conventions

- `code_xxx` généré automatiquement (jamais édité)
- ISO 639 (Langues) : minuscules
- ISO 3166 (Pays) : majuscules

### Référentiels multi-tables

Le projet inclut des référentiels simples (1 table) et des référentiels composés (plusieurs tables).

**Référentiels simples** : `langues`, `pays`, `contacts`, `editeurs`, `formats`, `impressions`

**Référentiels composés** :
- `ref_enum` (ref_enum_type + ref_enum)
- `recommandations` (origines_recommandation + recommandations)
- `prix_lit` (prix_lit + prix_lit_categorie + prix_lit_annee)

### Gestion des dépendances référentielles

Les référentiels peuvent être utilisés par de nombreuses tables métier.

Pour éviter les erreurs de suppression :
- L'application effectue un contrôle préalable des dépendances
- Les suppressions sont adaptées selon le type de contrainte (`RESTRICT` ou `SET NULL`)

Cela garantit des messages utilisateurs compréhensibles et l'absence d'erreurs SQL exposées directement à l'interface.

---

## Système de recommandations

Artefact intègre un système permettant de documenter les **sources de recommandation de livres**.

Une recommandation représente une suggestion provenant d'une source externe ou humaine : réseaux sociaux, blogs, libraires, amis, podcasts, etc.

Chaque recommandation est stockée dans la table `recommandations` et peut être associée à un livre normalisé (`livres`) ou à un livre en phase de capture (`livres_staging`).

Le système permet ainsi :
- de conserver l'historique des recommandations
- de documenter les sources
- d'analyser ultérieurement l'influence des différentes sources

---

## Gestion des notes enrichies

Artefact utilise un système standardisé pour la gestion des notes enrichies basé sur RichTextBox.

### Principe

Chaque champ de notes est stocké dans deux colonnes :
- `_rtf` : contenu formaté (affichage UI)
- `_txt` : texte brut (recherche SQL)

### Objectifs

- Conserver la mise en forme utilisateur
- Garantir des recherches fiables et performantes
- Permettre une réutilisation uniforme du système

### Implémentation

- Helper central : `RichTextNotesHelper`
- Toolbar standard : Gras, Italique, Souligné, Liste, Tabulation

### Règles importantes

- Le RTF n'est jamais utilisé pour les recherches
- Le texte brut n'est jamais utilisé pour l'affichage riche
- Toute manipulation passe par le helper

---

## Paths utilisés dans Artefact

### Dossiers de configuration

| Niveau 1      | Niveau 2  | Niveau 3 | Key       | Explication                                      | Création                        |
| ------------- | --------- | -------- | --------- | ------------------------------------------------ | ------------------------------- |
| **%APPDATA%** | Artefact  |          | Path_Conn | Dossier de configuration générale                | Au 1er démarrage                |
|               |           | Logs     | Path_Logs | Dossier logs                                     | Au 1er démarrage                |

### Dossiers Datas

| Niveau 1     | Niveau 2      | Niveau 3     | Key                    | Explication                                   |
| :----------- | :------------ | :----------- | :--------------------- | :-------------------------------------------- |
| **Artefact** |               |              | Path_General           | Path général                                  |
|              | **Datas**     |              | Path_Data              | Stockage data fichiers physiques              |
|              |               | AutPhoto     | Path_Data_AutPhoto     | Photos des auteurs                            |
|              |               | Fichelecture | Path_Data_FicheLecture | Fiches de lecture                             |
|              |               | LivStaging   | Path_Data_LivStaging   | Livres non normalisés                         |
|              |               | LivBiblio    | Path_Data_LivBiblio    | Livres normalisés - Bibliothèque principale   |
|              |               | LivWaiting   | Path_Data_LivWaiting   | Stockage d'attente                            |
|              |               | Statistiques | Path_Data_Stat         | Fichiers Statistiques                         |
|              | **DBCalibre** |              | Path_DBCalibre         | Base de données de Calibre                    |

---

## Documentation liée

- **CHANGELOG** : [`CHANGELOG.md`](CHANGELOG.md) - Changelog des modifications
- **Rules** : [`Rules.md`](Docs/Rules.md) - Règles de codage et conventions
- **Processus Artefact** : [`Process_Artefact.md`](Docs/Process_Artefact.md) - Processus métier et workflows
- **TODO** : [`TODO.md`](Docs/TODO.md) - Tâches à réaliser
- **Backup Database** : [`backup_NoData_artefact.sql`](Docs/Database/backup_NoData_artefact.sql) - Backup sans données
- **Backup avec données** : [`backup_WithData_artefact.sql`](Docs/Database/backup_WithData_artefact.sql) - Données de test
- **Diagrammes DB** : [`artefact_schema_erdiagram.mmd`](Docs/Database/artefact_schema_erdiagram.mmd)
- **Modèle database** : [`ModeleDB.md`](Docs/Database/ModeleDB.md) - Description détaillée du modèle
- **Documentation Technique UI** : [`Documentation_technique_UI.md`](Docs/Documentation_technique_UI.md)
- **Vision produit** : [`VISION.md`](Docs/VISION.md) - Vision et ADN du projet
- **Guide de reprise** : [`REPRISE.md`](Docs/REPRISE.md) - Démarrage rapide
- **Glossaire** : [`GLOSSAIRE.md`](Docs/GLOSSAIRE.md) - Définitions métier et techniques

---

**Contact** : ***Joëlle (Manou) - Les Artefacts de Manou***

Projet personnel, expérimental, réalisé pour le fun, le test et l'étude de connaissances techniques.

mailto: `joelle@nguyen.eu`

- GitHub privé : https://github.com/AngeljoNG/Artefact
- GitHub public : https://github.com/Les-Artefacts-de-Manou/Artefact

---
