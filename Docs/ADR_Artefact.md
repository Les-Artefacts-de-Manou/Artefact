# 🏛️ ADR – Architecture Decision Records – Artefact

[TOC]

---

## 🎯 Objectif de ce document

Ce document centralise toutes les **décisions d'architecture et de conception** prises pendant le développement d'Artefact.

Chaque décision est documentée avec son contexte, les alternatives considérées, la décision retenue, et la justification. Ce registre est la **mémoire technique du projet** : il permet de comprendre *pourquoi* le projet est structuré ainsi, pas seulement *comment*.

> 💡 Ce document est vivant. Il évolue à chaque décision structurante.  
> Il n'est pas rétrospectivement parfait — il capture la réalité au moment de la décision.

---

## 📐 Cadre ADR

### Portée

Ce registre couvre les décisions portant sur :

- l'architecture applicative (couches, modules, patterns)
- le modèle de base de données (structure, conventions, comportements)
- la sécurité et la configuration
- les choix techniques (frameworks, packages, outils)
- les conventions d'interface utilisateur (UI)

Il **ne couvre pas** :

- les détails d'implémentation spécifiques à une form (voir `Process_Artefact.md`)
- les règles de codage et conventions de nommage (voir `Rules.md`)
- le changelog technique quotidien (voir `CHANGELOG.md`)

---

### Format d'une décision

Chaque décision suit ce format :

| Champ | Description |
|---|---|
| **ID** | Identifiant unique (ex: `ARCH-001`) |
| **Titre** | Description courte de la décision |
| **Statut** | `✅ Adoptée` / `🔄 Révisée` / `🔍 Ouverte` / `❌ Abandonnée` |
| **Date** | Date de la décision |
| **Contexte** | Pourquoi cette décision était nécessaire |
| **Alternatives** | Ce qui a été envisagé |
| **Décision** | Ce qui a été retenu |
| **Justification** | Pourquoi ce choix |
| **Conséquences** | Ce que ça implique pour la suite |
| **Références** | Liens vers docs liées |

---

### Catégories d'ID

| Préfixe | Domaine |
|---|---|
| `ARCH-` | Architecture applicative |
| `DB-` | Base de données |
| `SEC-` | Sécurité |
| `UI-` | Interface utilisateur |
| `OD-` | Décision ouverte (*Open Decision*) |

---

### Statuts possibles

| Statut | Signification |
|---|---|
| ✅ Adoptée | Décision stable, en vigueur |
| 🔄 Révisée | Décision remplacée par une version ultérieure |
| 🔍 Ouverte | Sujet non encore tranché |
| ❌ Abandonnée | Piste explorée puis rejetée |

---

## 📋 Table des décisions

### Vue synthétique

| ID | Titre | Statut | Date |
|---|---|---|---|
| [ARCH-001](#arch-001) | Framework applicatif : VB.NET .NET 8 LTS WinForms | ✅ Adoptée | 09/02/2026 |
| [ARCH-002](#arch-002) | Package MariaDB : MySqlConnector au lieu de MySql.Data | ✅ Adoptée | 18/02/2026 |
| [ARCH-003](#arch-003) | Architecture en couches strictes (Form / Métier / QueryModule / DB) | ✅ Adoptée | 02/03/2026 |
| [ARCH-004](#arch-004) | Point d'accès DB unique via DatabaseManager | ✅ Adoptée | 18/02/2026 |
| [ARCH-005](#arch-005) | Démarrage bloquant avec AppStartupManager et vérification de schéma | ✅ Adoptée | 22/02/2026 |
| [ARCH-006](#arch-006) | Logging centralisé via GestionLog (niveaux + catégories + header session) | ✅ Adoptée | 23/02/2026 |
| [ARCH-007](#arch-007) | QueryModule : construction des requêtes uniquement, jamais d'exécution | ✅ Adoptée | 02/03/2026 |
| [ARCH-008](#arch-008) | Double stockage RTF + TXT pour les champs de notes enrichies | ✅ Adoptée | 03/2026 |
| [DB-001](#db-001) | Abandon de AUTO_INCREMENT : SEQUENCE dédiée par table | ✅ Adoptée | 09/02/2026 |
| [DB-002](#db-002) | Séparation id_xxx (technique) et code_xxx (lisible humain) | ✅ Adoptée | 09/02/2026 |
| [DB-003](#db-003) | Rejet des ENUM SQL natifs : tables référentielles typées | ✅ Adoptée | 12/02/2026 |
| [DB-004](#db-004) | Zone tampon livres_staging séparée de livres | ✅ Adoptée | 12/02/2026 |
| [DB-005](#db-005) | Table meta_schema pour le versionnement du schéma | ✅ Adoptée | 22/02/2026 |
| [DB-006](#db-006) | Architecture paramètres modulaire (param_db / param_paths / param_api / param_app) | ✅ Adoptée | 16/02/2026 |
| [DB-007](#db-007) | Comportement FK : RESTRICT pour liens critiques, SET NULL pour liens optionnels | ✅ Adoptée | 03/2026 |
| [DB-008](#db-008) | ref_enum : architecture parent/enfant (ref_enum_type / ref_enum) | ✅ Adoptée | 04/03/2026 |
| [DB-009](#db-009) | Recommandations indépendantes du cycle de vie du livre | ✅ Adoptée | 03/2026 |
| [SEC-001](#sec-001) | Chiffrement DPAPI CurrentUser pour le mot de passe local | ✅ Adoptée | 16/02/2026 |
| [SEC-002](#sec-002) | Aucun secret loggué — masquage automatique | ✅ Adoptée | 23/02/2026 |
| [SEC-003](#sec-003) | Pas de valeurs sentinelles UI en base de données | ✅ Adoptée | 03/2026 |
| [UI-001](#ui-001) | TableLayoutPanel au lieu de SplitContainer | ✅ Adoptée | 02/03/2026 |
| [UI-002](#ui-002) | Pattern ModeEdition + snapshot pour l'annulation | ✅ Adoptée | 02/03/2026 |
| [UI-003](#ui-003) | Validation via errProvider + StatusStrip, pas de MessageBox simple | ✅ Adoptée | 02/03/2026 |
| [UI-004](#ui-004) | DataGridView standardisée (FormatReferentielGrid + HighlightMainColumn) | ✅ Adoptée | 03/03/2026 |
| [OD-001](#od-001) | Import Calibre : pipeline complet (Processus 06) | 🔍 Ouverte | — |
| [OD-002](#od-002) | Intégration IA : périmètre, phases, garde-fous | 🔍 Ouverte | — |
| [OD-003](#od-003) | Style visuel de l'application (steampunk / vintage / autre) | 🔍 Ouverte | — |
| [OD-004](#od-004) | Interface Web : timing et périmètre MVP | 🔍 Ouverte | — |
| [OD-005](#od-005) | Recherche globale transverse | 🔍 Ouverte | — |
| [OD-006](#od-006) | Phase 3 référentiels : Auteurs, Séries, Tags | 🔍 Ouverte | — |
| [OD-007](#od-007) | Form dédiée pour la consultation des logs | 🔍 Ouverte | — |

---

## 🏗️ Décisions d'architecture applicative

### ARCH-001

**Titre** : Framework applicatif — VB.NET .NET 8 LTS WinForms  
**Statut** : ✅ Adoptée  
**Date** : 09/02/2026

#### Contexte

Artefact est une reconstruction volontaire d'Artefactothèque, un ancien projet en .NET Framework 4.8. La question était de choisir entre continuer sur .NET Framework, migrer vers .NET 8, ou partir sur une technologie différente (WPF, MAUI, Blazor Desktop…).

#### Alternatives considérées

| Option | Raison d'écartement |
|---|---|
| .NET Framework 4.8 | Technologie figée, pas d'avenir |
| WPF .NET 8 | Courbe d'apprentissage trop élevée pour ce projet |
| MAUI / Blazor Desktop | Trop éloigné des habitudes actuelles, over-engineering pour ce périmètre |
| VB.NET .NET 8 WinForms | ✅ Retenu |

#### Décision

**VB.NET, .NET 8 LTS, Windows Forms.**

#### Justification

- Modernité maîtrisée : LTS, support long terme garanti
- Continuité avec les habitudes de développement VB.NET
- WinForms suffisant pour le périmètre applicatif visé
- Compatibilité packages NuGet récents
- Possibilité future de factoriser certaines couches pour une version Web

#### Conséquences

- L'application est **Windows uniquement** (WinForms + DPAPI)
- Les packages NuGet doivent cibler `net8.0-windows`
- Une éventuelle version Web nécessitera une refactorisation de la couche UI

**Références** : `Readme.md`, `Rules.md §6`

---

### ARCH-002

**Titre** : Package MariaDB — MySqlConnector au lieu de MySql.Data  
**Statut** : ✅ Adoptée  
**Date** : 18/02/2026

#### Contexte

Besoin d'un pilote ADO.NET pour se connecter à MariaDB depuis .NET 8. Le pilote historique `MySql.Data` (Oracle) était disponible mais posait des problèmes de compatibilité moderne.

#### Alternatives considérées

| Option | Raison d'écartement |
|---|---|
| MySql.Data (Oracle) | Historique de problèmes de compatibilité .NET modern, licence restrictive |
| MySqlConnector (Bradley Grainger) | ✅ Retenu |

#### Décision

**MySqlConnector 2.5.0** (bgrainger — https://mysqlconnector.net/)

#### Justification

- Pilote open source, community-driven, conçu pour .NET moderne
- Meilleure compatibilité .NET 8 et async
- Pooling ADO.NET natif
- MariaDB et MySQL supportés nativement

#### Conséquences

- Tout le code de connexion utilise `MySqlConnection`, `MySqlCommand`, etc. de MySqlConnector
- Migration vers une autre base nécessiterait un remplacement du pilote

**Références** : `Readme.md §Packages`, `CHANGELOG.md 18/02/2026`

---

### ARCH-003

**Titre** : Architecture en couches strictes  
**Statut** : ✅ Adoptée  
**Date** : 02/03/2026 (formalisée, appliquée dès le départ)

#### Contexte

Sans séparation explicite des responsabilités, les applications WinForms tendent rapidement à mélanger SQL, logique métier et UI dans les formulaires — ce qui rend la maintenance cauchemardesque.

#### Décision

Architecture en 4 couches strictement séparées :

```
Form UI
   ↓ appels
GestionReferentiel (métier / accès données)
   ↓ SQL
QueryModule (construction des requêtes uniquement)
   ↓ connexion
MariaDB
```

**Règles absolues :**

- Une Form ne contient **aucun SQL**
- QueryModule **ne doit jamais exécuter** de requêtes
- GestionReferentiel est le **seul** point d'exécution DB pour la couche métier
- La logique UI reste dans la Form

#### Justification

- Maintenabilité : on sait exactement où trouver quoi
- Testabilité : les couches sont indépendantes
- Cohérence : un seul endroit pour modifier une requête
- Extensibilité : facile d'ajouter un nouveau référentiel

#### Conséquences

- Toute nouvelle fonctionnalité CRUD doit respecter ce découpage
- Toute déviation doit être explicitement justifiée et documentée

**Références** : `Process_Artefact.md §Processus 03`, `Rules.md §25`

---

### ARCH-004

**Titre** : Point d'accès DB unique via DatabaseManager  
**Statut** : ✅ Adoptée  
**Date** : 18/02/2026

#### Contexte

Sans centralisation, les connexions DB risquent d'être ouvertes n'importe où dans le code, rendant impossible le contrôle des fuites et du pooling.

#### Décision

`DatabaseManager` est **l'unique point de création de connexions MariaDB** dans Artefact.

**Règles :**

- Aucune `MySqlConnection` ouverte manuellement hors de DatabaseManager
- Utilisation systématique du bloc `Using` pour la fermeture automatique
- Pooling ADO.NET activé
- La connection string est construite par une **unique méthode** `BuildConnectionString(cfg)`

#### Justification

- Gestion centralisée des erreurs de connexion
- Source unique pour la connection string (pas de duplication de secret)
- Pooling garantit les performances

#### Conséquences

- Aucun code extérieur ne doit construire de connection string
- Toute modification du modèle de connexion n'affecte qu'un seul fichier

**Références** : `Readme.md §Connexion à la base de données`, `Rules.md §10`

---

### ARCH-005

**Titre** : Démarrage bloquant avec AppStartupManager et vérification de schéma  
**Statut** : ✅ Adoptée  
**Date** : 22/02/2026

#### Contexte

Une application ne peut pas fonctionner correctement si la DB est inaccessible ou si le schéma est incompatible avec la version applicative. Il fallait décider comment gérer ce cas dès l'ouverture.

#### Décision

**Démarrage entièrement bloquant** :

1. L'UI est verrouillée dès l'ouverture (`PortailReferentiels.Load`)
2. `AppStartupManager.RunStartup()` orchestre :
   - lecture config locale JSON
   - test connexion MariaDB
   - vérification de la version du schéma (`meta_schema`)
3. Tout échec → ouverture de `GestionConnexionMariaDb`
4. Boucle jusqu'à succès ou abandon utilisateur
5. Sur abandon → fermeture contrôlée de l'application

**AppStartupManager ne contient aucune logique UI** — il retourne uniquement un statut.

#### Justification

- Garantit l'intégrité de l'état applicatif
- Évite les erreurs silencieuses en cours d'utilisation
- Séparation nette UI / orchestration

#### Conséquences

- Impossible d'utiliser l'application sans DB valide
- Tout changement structurel DB **doit** incrémenter `ExpectedSchemaVersion`

**Références** : `Process_Artefact.md §Processus 01`, `Rules.md §18 §24`

---

### ARCH-006

**Titre** : Logging centralisé via GestionLog  
**Statut** : ✅ Adoptée  
**Date** : 23/02/2026

#### Contexte

Le logging éparpillé dans le code produit des logs inconsistants et rend le diagnostic difficile.

#### Décision

Module unique `GestionLog` avec :

- **3 niveaux** : Rapide (jalons), Succinct (erreurs/états), Complet (détails techniques)
- **5 catégories** : Startup, Database, UI, Process, General
- **Header de session** à chaque lancement (date/heure/machine/user)
- Fichier journalier `%APPDATA%\Artefact\Logs\Artefact_YYYY-MM-DD.log`
- Purge automatique > 7 jours
- Thread-safe (`SyncLock`)
- Masquage automatique des secrets (`Password` / `Pwd`)

**Règle clé** : les niveaux sont des marqueurs de *profondeur*, pas un mécanisme de filtre. Tout log écrit est écrit.

#### Justification

- Diagnostic fiable en production
- Source unique — pas de `Debug.WriteLine` orphelins
- Lisibilité : on peut filtrer par catégorie pour analyser un problème spécifique

#### Conséquences

- Toute exception doit être logguée (aucune exception silencieuse)
- Les modules bas niveau (Crypto, DTO) **ne loggent pas** — c'est la couche orchestration qui le fait

**Références** : `Process_Artefact.md §Processus 02`, `Rules.md §19 §20`

---

### ARCH-007

**Titre** : QueryModule — construction uniquement, jamais d'exécution  
**Statut** : ✅ Adoptée  
**Date** : 02/03/2026

#### Contexte

Où placer les requêtes SQL ? Soit directement dans les modules d'accès aux données, soit centralisées ailleurs.

#### Décision

`QueryModule` contient **uniquement les définitions de requêtes SQL** (sous forme de fonctions retournant des chaînes de texte ou des commandes paramétrées).

Il **ne s'exécute jamais lui-même**.

Structure de régions :

```vb
#Region "TABLE - SQL"
' Xxx_SelectAll
' Xxx_Insert
' Xxx_Update
' Xxx_Delete
' Xxx_CountDependances
#End Region
```

#### Justification

- Localisation unique de tout le SQL : une seule recherche pour retrouver une requête
- Séparation claire du SQL et de la logique de connexion
- Facilite les revues de code axées sur les requêtes

#### Conséquences

- Toute nouvelle requête SQL doit être ajoutée dans QueryModule
- GestionReferentiel appelle QueryModule pour obtenir la requête, puis l'exécute

**Références** : `Rules.md §25.1`

---

### ARCH-008

**Titre** : Double stockage RTF + TXT pour les notes enrichies  
**Statut** : ✅ Adoptée  
**Date** : Mars 2026

#### Contexte

Besoin de proposer des notes avec mise en forme (gras, italique, listes) tout en maintenant la possibilité de recherche SQL performante.

#### Alternatives considérées

| Option | Problème |
|---|---|
| Stockage RTF seul | Recherche SQL impossible sur RTF |
| Texte brut seul | Perte de la mise en forme utilisateur |
| HTML | Surdimensionné, complexité inutile |
| RTF + TXT miroir | ✅ Retenu |

#### Décision

Chaque champ notes est stocké dans **deux colonnes** :

- `xxx_rtf` : contenu formaté (affichage UI via RichTextBox)
- `xxx_txt` : texte brut (recherche SQL uniquement)

Tout passe par le helper central `RichTextNotesHelper`.

**Interdictions :**

- Manipuler directement le RTF dans les Forms
- Utiliser `_rtf` dans une requête SQL
- Utiliser `_txt` pour l'affichage riche

#### Justification

- Notes enrichies visibles, recherches fiables
- Le helper centralise toute la logique de synchronisation

**Références** : `Readme.md §Notes enrichies`, `Rules.md §26`

---

## 🗄️ Décisions de base de données

### DB-001

**Titre** : Abandon de AUTO_INCREMENT — SEQUENCE dédiée par table  
**Statut** : ✅ Adoptée  
**Date** : 09/02/2026 (stabilisée 11/02/2026)

#### Contexte

MariaDB présente une incompatibilité entre `AUTO_INCREMENT` et les colonnes `GENERATED STORED` : erreur 1901. La solution AUTO_INCREMENT provoquait aussi des sauts de 1000 (cache par défaut).

#### Décision

Chaque table utilisant un ID génère cet ID via une **SEQUENCE dédiée** :

```sql
CREATE SEQUENCE seq_auteurs
    START WITH 1
    INCREMENT BY 1
    MINVALUE 1
    NO MAXVALUE
    CACHE 1
    NOCYCLE;
```

Convention de création en 2 étapes (obligation MariaDB) :

```sql
-- Étape 1 : créer la table sans DEFAULT
id_auteur BIGINT UNSIGNED NOT NULL,

-- Étape 2 : assigner le DEFAULT via ALTER
ALTER TABLE auteurs
  ALTER COLUMN id_auteur SET DEFAULT (NEXT VALUE FOR seq_auteurs);
```

**CACHE 1** est impératif pour éviter les sauts d'identifiants en développement.

#### Justification

- Compatibilité avec les colonnes `GENERATED STORED`
- Comportement prévisible et contrôlé
- Réinitialisation possible via `ALTER SEQUENCE seq_xxx RESTART WITH 1`

#### Conséquences

- `TRUNCATE TABLE` ne remet pas les séquences à zéro — utiliser `ALTER SEQUENCE ... RESTART WITH 1` si besoin
- Toute nouvelle table doit avoir sa séquence dédiée créée avant l'ALTER DEFAULT

**Références** : `CHANGELOG.md 09-11/02/2026`, `Rules.md §9`, `Database/ModeleDB.md`

---

### DB-002

**Titre** : Séparation id_xxx (technique) et code_xxx (lisible humain)  
**Statut** : ✅ Adoptée  
**Date** : 09/02/2026

#### Contexte

Les identifiants purement numériques sont illisibles dans les logs, exports et outils d'administration. Mais utiliser des codes texte comme clés étrangères crée des problèmes de performance et d'intégrité.

#### Décision

Chaque entité a deux identifiants :

| Champ | Type | Rôle |
|---|---|---|
| `id_xxx` | `BIGINT UNSIGNED` | Clé technique, clé étrangère |
| `code_xxx` | `VARCHAR(12) GENERATED STORED` | Lisible humain, jamais FK |

Format du code : `<Préfixe><ID sur 6 chiffres>`  
Exemples : `A000042`, `B000001`, `LF000012`

```sql
ADD COLUMN code_auteur VARCHAR(12)
GENERATED ALWAYS AS (CONCAT('A', LPAD(id_auteur, 6, '0'))) STORED,
ADD UNIQUE KEY uq_auteurs_code (code_auteur);
```

#### Justification

- Lisibilité humaine sans sacrifier les performances FK
- `code_xxx` toujours cohérent avec l'ID (généré, pas saisi)
- Séparation propre des usages technique vs humain

#### Conséquences

- `code_xxx` ne peut jamais être modifié manuellement
- `code_xxx` ne doit **jamais** apparaître comme FK dans une autre table

**Références** : `Rules.md §9.3`, `Database/ModeleDB.md`

---

### DB-003

**Titre** : Rejet des ENUM SQL natifs — tables référentielles typées  
**Statut** : ✅ Adoptée  
**Date** : 12/02/2026

#### Contexte

MariaDB supporte les types ENUM natifs. Mais ces ENUM sont rigides : modifier une valeur nécessite un DDL, pas de traçabilité, pas d'extension sans migration.

#### Alternatives considérées

| Option | Problème |
|---|---|
| ENUM SQL natif | DDL pour tout changement, pas extensible |
| VARCHAR + contrainte CHECK | Fragile, non normalisé |
| Tables référentielles typées (`ref_enum`) | ✅ Retenu |

#### Décision

**Abandon total des ENUM SQL.** Toutes les valeurs énumérées passent par :

- Tables dédiées simples (ex: `langues`, `impression`) pour les référentiels métier
- Architecture `ref_enum_type / ref_enum` pour les énumérations génériques (voir [DB-008](#db-008))

#### Justification

- Flexibilité : ajouter une valeur = un INSERT, pas un DDL
- Traçabilité : les valeurs font partie des données
- Extensibilité : tout référentiel peut évoluer sans modification de schéma

#### Conséquences

- Tout nouveau champ "à valeurs prédéfinies" doit passer par une table référentielle
- Les FK vers ces tables garantissent l'intégrité

**Références** : `CHANGELOG.md 12/02/2026`, `Rules.md §25`

---

### DB-004

**Titre** : Zone tampon livres_staging séparée de livres  
**Statut** : ✅ Adoptée  
**Date** : 12/02/2026

#### Contexte

L'import depuis Calibre (ou d'autres sources) produit des données brutes, potentiellement incomplètes ou non normalisées. Mettre directement ces données dans la table principale `livres` compromettrait l'intégrité des données validées.

#### Décision

Deux tables distinctes et étanches :

| Table | Contenu |
|---|---|
| `livres_staging` | Données importées, en cours de normalisation |
| `livres` | Données validées, normalisées, exploitables |

**Principes :**

- Aucune donnée floue dans `livres`
- Le pipeline est : Import → `livres_staging` → validation/normalisation → `livres`
- `livres_staging` peut accueillir des livres sans fichier (nouveautés, précommandes)

#### Justification

- Intégrité absolue de la table principale
- Séparation claire entre "en cours" et "valide"
- Permet de rejeter, corriger, réimporter sans affecter les données saines

#### Conséquences

- Tout processus d'import doit passer par `livres_staging`
- La phase de validation/normalisation (Processus 06) doit être implémentée avant tout import

**Références** : `CHANGELOG.md 12/02/2026`, `VISION.md §Principes non négociables`

---

### DB-005

**Titre** : Table meta_schema pour le versionnement du schéma  
**Statut** : ✅ Adoptée  
**Date** : 22/02/2026

#### Contexte

Sans versionnement, une incompatibilité entre l'application et la DB peut provoquer des erreurs silencieuses ou des comportements imprévisibles.

#### Décision

Table `meta_schema` avec :

- `schema_version` (INT) : version courante du schéma
- `applied_at` (DATETIME) : date d'application
- `notes` (VARCHAR) : description de la migration

Constante applicative : `ExpectedSchemaVersion` dans `AppStartupManager`.

**Règle** : toute migration structurelle **doit** :

1. Être réalisée via un script SQL numéroté (`00N_description.sql`)
2. Mettre à jour `meta_schema.schema_version`
3. Mettre à jour `ExpectedSchemaVersion` dans le code
4. Être documentée dans `CHANGELOG.md`

#### Justification

- Détection immédiate des incompatibilités au démarrage
- Traçabilité complète des migrations
- Impossible d'oublier de synchroniser l'application et la DB

#### Conséquences

- Toute modification structurelle sans mise à jour de version est **une faute**
- La vérification est bloquante au démarrage (voir [ARCH-005](#arch-005))

**Références** : `CHANGELOG.md 22/02/2026`, `Rules.md §18`, `Readme.md §Versionnement du Schéma`

---

### DB-006

**Titre** : Architecture paramètres modulaire  
**Statut** : ✅ Adoptée  
**Date** : 16/02/2026

#### Contexte

Une table `parametres` unique devient rapidement un fourre-tout difficile à maintenir. Les paramètres de types très différents (connexions DB, chemins fichiers, APIs, config applicative) n'ont pas les mêmes contraintes de sécurité, de validation ni d'évolution.

#### Alternatives considérées

| Option | Problème |
|---|---|
| Table paramètres unique | Fourre-tout, pas de typage |
| Fichiers de config uniquement | Pas de centralisation DB |
| Architecture modulaire | ✅ Retenu |

#### Décision

4 tables dédiées :

| Table | Contenu |
|---|---|
| `param_db` | Connexions bases de données |
| `param_paths` | Chemins applicatifs (biblio, Calibre, Data) |
| `param_api` | Services API et URLs (OpenAI, Google Books…) |
| `param_app` | Paramètres applicatifs généraux |

Chacune avec sa propre séquence et son `code_xxx`.

#### Justification

- Séparation des préoccupations : chaque type de paramètre évolue indépendamment
- Sécurité renforcée : les secrets API/DB sont dans des tables isolées
- Extensibilité : ajout d'un nouveau type de paramètre sans modifier les existants

#### Conséquences

- La connexion MariaDB principale reste dans un fichier JSON local chiffré (DPAPI) — pas dans `param_db` (bootstrapping)
- `param_db` et `param_paths` serviront à des configurations secondaires

**Références** : `CHANGELOG.md 16/02/2026`

---

### DB-007

**Titre** : Comportement FK — RESTRICT pour liens critiques, SET NULL pour liens optionnels  
**Statut** : ✅ Adoptée  
**Date** : Mars 2026

#### Contexte

Quand un élément référentiel est supprimé, comment gérer les enregistrements qui y font référence ?

#### Décision

Deux comportements selon la criticité du lien :

| Comportement | Usage | Action applicative |
|---|---|---|
| `ON DELETE RESTRICT` | Lien critique (ex: `auteurs_pays`) | Blocage applicatif + message utilisateur |
| `ON DELETE SET NULL` | Lien optionnel (ex: `livres.id_editeur`) | Avertissement + suppression autorisée |

**Règle de maintenance** : si une nouvelle table référence un référentiel existant, les contrôles de suppression **doivent être mis à jour** dans l'application.

#### Justification

- L'application gère les dépendances *avant* la suppression pour afficher un message métier compréhensible
- Évite les erreurs SQL brutes exposées à l'interface
- La FK côté DB reste le dernier garde-fou

#### Conséquences

- Chaque référentiel doit avoir ses fonctions `CountDependances()` maintenues à jour
- Toute nouvelle table qui référence un référentiel déclenche une obligation de mise à jour des contrôles

**Références** : `Rules.md §25.11`, `Process_Artefact.md §Processus 03`

---

### DB-008

**Titre** : ref_enum — architecture parent/enfant (ref_enum_type / ref_enum)  
**Statut** : ✅ Adoptée  
**Date** : 04/03/2026

#### Contexte

Un modèle `ref_enum` à table unique mélangeait des valeurs de natures très différentes (statut lecture, type fichier, support lecture…) sans possibilité de filtrage propre par type.

#### Décision

**Refonte en deux tables** :

| Table | Rôle |
|---|---|
| `ref_enum_type` | Catégories (ex: STATUT_LECTURE, TYPE_FICHIER) |
| `ref_enum` | Valeurs associées à un type (ex: Lu, Non lu, En cours) |

Les tables métier référencent toujours `ref_enum.id_enum`.

**Conventions :**

- `code_type` : MAJUSCULES
- `code_valeur` : MAJUSCULES
- Tri : `ordre_affichage` puis `libelle`

Suppression d'un type : bloquée si des valeurs existent. L'UI propose :
1. Naviguer vers les valeurs liées
2. Supprimer le type + toutes ses valeurs en transaction

#### Justification

- Meilleure normalisation et extensibilité
- Filtrage côté application simple et fiable
- Structure claire pour l'UI (UC_RefEnum avec onglets Types/Valeurs)

#### Conséquences

- Ce pattern est le modèle de référence pour tout nouveau référentiel composé
- `ref_enum` est un référentiel fortement partagé : toute nouvelle table qui l'utilise doit mettre à jour les contrôles de suppression

**Références** : `CHANGELOG.md 04/03/2026`, `Rules.md §25.10`

---

### DB-009

**Titre** : Recommandations indépendantes du cycle de vie du livre  
**Statut** : ✅ Adoptée  
**Date** : Mars 2026

#### Contexte

Comment stocker l'historique des suggestions de livres ? Une recommandation peut arriver avant même que le livre soit dans le système.

#### Décision

Une recommandation est un **événement documenté indépendant** :

```
recommandations (1) ←——→ ref_origine_recommandation
recommandations (N) ←——→ livres (via livres_recommandations)
recommandations (N) ←——→ livres_staging (via livres_staging_recommandations)
```

Une recommandation peut exister **sans être liée à un livre**.

**Structure** : origine + nom/pseudo source + URL + date + commentaire.

Suppression d'une recommandation liée à un livre → **bloquée**.

#### Justification

- Capture de l'information de découverte même avant l'intégration du livre
- Séparation claire entre l'événement de recommandation et les données livre
- Permet l'analyse ultérieure des sources les plus influentes

#### Conséquences

- Le système de recommandations est conçu pour être enrichi par l'IA (veille automatique)
- Les tables de liaison permettent d'associer la même recommandation à plusieurs livres

**Références** : `Process_Artefact.md §Processus 04`, `Rules.md §27`

---

## 🔐 Décisions de sécurité

### SEC-001

**Titre** : Chiffrement DPAPI CurrentUser pour le mot de passe local  
**Statut** : ✅ Adoptée  
**Date** : 16/02/2026

#### Contexte

La configuration de connexion MariaDB doit être stockée localement sur le poste de l'utilisateur. Comment protéger le mot de passe ?

#### Alternatives considérées

| Option | Problème |
|---|---|
| Mot de passe en clair | Inacceptable |
| Chiffrement symétrique avec clé hardcodée | Clé exposée dans le code |
| Gestionnaire de mots de passe système | Complexité, dépendance externe |
| DPAPI CurrentUser | ✅ Retenu |

#### Décision

**DPAPI (Data Protection API) avec `DataProtectionScope.CurrentUser`.**

- Mot de passe chiffré en Base64 dans `%APPDATA%\Artefact\artefact.local.json`
- Déchiffrement uniquement au moment de la construction de la connection string
- Jamais affiché sauf via le bouton "œil" en mode `PasswordMode.SetNew`
- En cas d'erreur de déchiffrement : exception explicite, log détaillé (sans secret)

#### Justification

- Protection liée au compte Windows courant : aucune clé à gérer
- Standard Windows, fiable et transparent
- Le préfixe Base64 constant du DPAPI est normal et attendu

#### Conséquences

- **Application liée au poste** : le fichier de config chiffré n'est pas transférable entre comptes Windows
- Une réinstallation nécessite de ressaisir le mot de passe

**Références** : `CHANGELOG.md 16/02/2026`, `Rules.md §17`, `Readme.md §Sécurité`

---

### SEC-002

**Titre** : Aucun secret loggué — masquage automatique  
**Statut** : ✅ Adoptée  
**Date** : 23/02/2026

#### Contexte

Un log complet est précieux pour le diagnostic. Mais un log contenant un mot de passe ou une connection string complète est une faille de sécurité sérieuse.

#### Décision

**Règles absolues de logging :**

- Il est **strictement interdit** de logguer un mot de passe ou une connection string complète
- `GestionLog` applique un masquage automatique sur les termes `Password` et `Pwd`
- Le contexte technique loggué au démarrage ne contient que : Host, Port, DB, User — **jamais le mot de passe**

**Hiérarchie de responsabilité** :

- Les modules bas niveau (Crypto, DTO) ne loggent **pas** — ils throwent
- Les couches d'orchestration (AppStartupManager, GestionReferentiel) loggent avec catégorie et niveau

#### Justification

- Un secret loggué = une faille dans les fichiers de logs
- Un log fichier journalier peut être copié, envoyé, consulté par un tiers

#### Conséquences

- Tout développeur doit vérifier qu'aucune valeur sensible n'est passée à `EcrireLog()`
- Règle non négociable, même en environnement de développement

**Références** : `Rules.md §19.3 §20`

---

### SEC-003

**Titre** : Pas de valeurs sentinelles UI en base de données  
**Statut** : ✅ Adoptée  
**Date** : Mars 2026

#### Contexte

Les ComboBox de filtrage utilisent souvent une valeur "Toutes origines", "— Sélectionner —" etc. Il ne faut pas que ces valeurs UI atterrissent en base.

#### Décision

**Interdiction formelle** d'injecter des valeurs sentinelles UI dans des champs métier.

- Une ComboBox ne peut pas avoir un double rôle (filtre + champ métier)
- Si nécessaire : deux contrôles distincts, ou validation stricte empêchant les valeurs invalides
- Valeurs sentinelles côté code : `0` ou `Nothing` — jamais persistées

#### Justification

- Intégrité des données : une valeur UI ne correspond à aucun enregistrement réel
- Évite les FK orphelines et les comportements inattendus

#### Conséquences

- Toute ComboBox doit être analysée pour déterminer son rôle exact
- Les combobox de filtre doivent être distinctes des combobox de saisie

**Références** : `Rules.md §26 §ComboBox`, `Readme.md §Attention - Valeurs UI vs Métier`

---

## 🖥️ Décisions d'interface utilisateur

### UI-001

**Titre** : TableLayoutPanel au lieu de SplitContainer pour les layouts liste/détails  
**Statut** : ✅ Adoptée  
**Date** : 02/03/2026

#### Contexte

Les écrans référentiels ont toutes un layout liste (DataGridView) + panneau de détails. `SplitContainer` paraissait naturel mais s'est avéré instable dans le Designer Visual Studio.

#### Décision

**TableLayoutPanel** pour tous les layouts liste/détails.

Structure standard des écrans référentiels :

```
pnlTop      → recherche
tlpMain     → [gauche: DataGridView] [droite: détails]
pnlActions  → boutons CRUD
StatusStrip → stsStatus + stsLabelStatus
```

#### Justification

- `SplitContainer` provoque des bugs visuels dans le Designer .NET 8
- `TableLayoutPanel` est stable, prévisible, résistant aux redimensionnements

#### Conséquences

- Toute nouvelle form référentielle utilise obligatoirement `TableLayoutPanel`
- `SplitContainer` est explicitement exclu (`spc` dans la nomenclature mais jamais utilisé)

**Références** : `Rules.md §25.2`, `CHANGELOG.md 02/03/2026`

---

### UI-002

**Titre** : Pattern ModeEdition + snapshot pour l'annulation  
**Statut** : ✅ Adoptée  
**Date** : 02/03/2026

#### Contexte

La gestion des états d'une form CRUD (lecture / création / modification / annulation) est source de bugs si elle n'est pas standardisée.

#### Décision

**Enum partagé `ModeEdition`** (dans `UtilsForm`) :

- `Consultation` : affichage seul, DataGridView active, boutons lecture
- `Nouveau` : champs vidés, saisie active, pas de DataGridView
- `Modification` : champs pré-remplis depuis un **snapshot**, saisie active

**Snapshot** : copie de l'objet métier au moment de l'entrée en mode Modification. Restauré sur Annuler.

**Option B (obligatoire)** : passage en mode Modification uniquement via un bouton explicite — pas de déclenchement automatique sur `TextChanged`.

#### Justification

- Comportement homogène sur tous les écrans
- Le snapshot garantit un Annuler fiable sans rechargement DB
- Le bouton explicite évite les modifications accidentelles

#### Conséquences

- Toute form référentielle doit implémenter les 3 modes
- `_mode`, `_snapshot`, `_currentId` sont les variables de base de chaque form

**Références** : `Rules.md §25.3 §25.6`, `Process_Artefact.md §Processus 03`

---

### UI-003

**Titre** : Validation via errProvider + StatusStrip, pas de MessageBox pour validation simple  
**Statut** : ✅ Adoptée  
**Date** : 02/03/2026

#### Contexte

Les MessageBox sont intrusives pour la validation de formulaire. Mais l'utilisateur doit quand même être informé des erreurs.

#### Décision

**Hiérarchie des retours visuels :**

| Situation | Mécanisme |
|---|---|
| Validation champ | `errProvider` (icône rouge) + focus sur le contrôle |
| Message global | `StatusStrip` / `stsLabelStatus` |
| Erreur critique bloquante | `MessageBox` (cas rares) |
| Exception DB | `GestionLog` (invisible pour l'utilisateur sauf StatusStrip) |

**Aucun `MessageBox.Show()` pour une validation de champ ordinaire.**

#### Justification

- UX plus fluide : l'utilisateur voit l'erreur inline sans interruption modale
- StatusStrip toujours visible : contexte clair
- MessageBox réservée aux cas vraiment bloquants (fermeture impossible, erreur critique)

#### Conséquences

- Toute nouvelle form doit inclure un `errProvider` et un `StatusStrip`
- La disposition `stsStatus` + `stsLabelStatus` est standardisée

**Références** : `Rules.md §25.5 §23`

---

### UI-004

**Titre** : DataGridView standardisée pour tous les référentiels  
**Statut** : ✅ Adoptée  
**Date** : 03/03/2026

#### Contexte

Chaque DataGridView avait son propre style, créant une incohérence visuelle et du code dupliqué.

#### Décision

Deux helpers dans `UtilsForm`, **obligatoires** sur toute DataGridView référentielle :

- `FormatReferentielGrid(dgv)` : colonnes techniques masquées, headers centrés en gras, lignes alternées, colonnes principales en gras, sélection douce
- `HighlightMainColumn(dgv)` : mise en avant de la colonne principale (`nom_xxx`, `libelle_xxx`)

**Règle d'accès à la ligne courante** : utiliser `dgv.CurrentRow`, **jamais** `SelectedRows(0)`.

L'événement `SelectionChanged` est **obligatoire** sur toute DataGridView référentielle pour appeler `BindSelectedToDetails()`.

#### Justification

- Cohérence visuelle entre tous les écrans
- Factorisation : modification du style centralisée en un endroit
- `CurrentRow` vs `SelectedRows(0)` : évite les désynchronisations après reload

#### Conséquences

- Toute nouvelle DataGridView référentielle applique ces deux helpers
- Les écarts visuels sont des bugs

**Références** : `Rules.md §25.7 §25.12 §25.13`, `CHANGELOG.md 03/03/2026`

---

## 🔍 Décisions ouvertes

### OD-001

**Titre** : Import Calibre — pipeline complet (Processus 06)  
**Statut** : 🔍 Ouverte  
**Priorité** : Haute

#### Contexte

Artefact est conçu pour importer depuis Calibre (`Metadata.db` → copie locale `myMetadata.db`). Les fichiers `.bat` de copie existent mais le pipeline applicatif complet n'est pas encore implémenté.

#### Points à décider

- Déclenchement de la copie `.bat` depuis l'application
- Pré-contrôles sur la DB Calibre copiée
- Mapping auteur / tags / formats / séries
- Gestion des doublons et collisions de métadonnées
- Traçabilité des imports et rejets
- Journalisation des anomalies bloquantes et non bloquantes
- Workflow de normalisation staging → livres

#### Dépendances

- [DB-004](#db-004) livres_staging ✅
- Processus 06 à formaliser dans `Process_Artefact.md`

**Références** : `Process_Artefact.md §Processus 06`, `TODO.md §Import`

---

### OD-002

**Titre** : Intégration IA — périmètre, phases, garde-fous  
**Statut** : 🔍 Ouverte  
**Priorité** : Moyenne (après stabilisation du modèle de données)

#### Contexte

L'IA est au cœur de la vision d'Artefact (enrichissement de fiches, recommandations personnalisées, résumés, détection d'incohérences). Les cas d'usage sont identifiés mais l'architecture technique reste à définir.

#### Points à décider

- Cas d'usage IA prioritaires (résumé, enrichissement métadonnées, recommandations)
- Sources autorisées et niveau de confiance
- Mode "IA assistante" (proposition) vs "IA autonome" (action encadrée)
- Garde-fous : traçabilité, validation humaine, logs
- APIs à utiliser (OpenAI, Google Books, autres)
- Stockage des données enrichies par l'IA vs données saisies manuellement

#### Principes directeurs déjà posés

- **IA utile, traçable et toujours contrôlable** (voir `VISION.md`)
- L'IA ne doit jamais déposséder l'utilisateur de son jugement
- Les enrichissements IA doivent être distinguables des données manuelles

**Références** : `VISION.md §Place de l'IA`, `TODO.md §IA`

---

### OD-003

**Titre** : Style visuel de l'application  
**Statut** : 🔍 Ouverte  
**Priorité** : Basse (fonctionnel d'abord)

#### Contexte

L'identité visuelle d'Artefact est en suspens : steampunk, vintage, bibliothèque, ou autre. La décision n'est pas urgente mais influencera les thèmes, icônes et l'ergonomie générale.

#### Points à décider

- Direction artistique principale (steampunk / vintage / autre)
- Thèmes personnalisables ou style fixe
- Icônes et pictogrammes
- Éventuel changement de nom (Bookzilla ? Manou ? Artefact reste ?)

**Références** : `TODO.md §Navigation, ergonomie, design`

---

### OD-004

**Titre** : Interface Web — timing et périmètre MVP  
**Statut** : 🔍 Ouverte  
**Priorité** : Basse (après stabilisation WinForms)

#### Contexte

La `VISION.md` mentionne une version Web future. Les couches métier et DB d'Artefact sont potentiellement réutilisables, mais WinForms ne l'est pas.

#### Points à décider

- Technologie Web (Blazor, ASP.NET, autre)
- Périmètre du MVP Web (lecture seule ? CRUD complet ?)
- Timing par rapport à WinForms
- Stratégie de partage des couches métier

**Références** : `VISION.md §Horizon fonctionnel`, `TODO.md §En pause / à clarifier`

---

### OD-005

**Titre** : Recherche globale transverse  
**Statut** : 🔍 Ouverte  
**Priorité** : Basse (reportée)

#### Contexte

Une recherche transverse sur l'ensemble des données (livres, auteurs, tags, recommandations…) est identifiée comme utile mais sa complexité technique justifie son report.

#### Points à décider

- Périmètre de la recherche (quelles tables, quels champs)
- Mécanisme : SQL full-text, index spécifique, recherche Lucene-like
- Interface : barre de recherche globale, résultats par catégorie

**Références** : `TODO.md §En pause / à clarifier`

---

### OD-006

**Titre** : Phase 3 référentiels — Auteurs, Séries, Tags  
**Statut** : 🔍 Ouverte  
**Priorité** : Haute (prochaine phase après PrixLit)

#### Contexte

Les phases 1 et 2 des référentiels sont implémentées. La phase 3 couvre Auteurs, Séries et Tags — des référentiels plus complexes (volume élevé, relations multiples).

#### Points à décider

- `GestionAuteurs` : biographie, nationalités (`auteurs_pays`), rôles, photo
- `GestionSeries` : format, statut, nombre de tomes
- `GestionTags` : volume élevé (pas de chargement automatique — recherche obligatoire)
- Import de masse depuis les écrans de gestion respectifs

**Références** : `TODO.md §Phase 3`, `Rules.md §25.4`

---

### OD-007

**Titre** : Form dédiée pour la consultation des logs  
**Statut** : 🔍 Ouverte  
**Priorité** : Basse

#### Contexte

Les logs sont actuellement consultables uniquement via un éditeur de texte externe. Une form dédiée dans l'application faciliterait le diagnostic en production.

#### Points à décider

- Filtrage par date, niveau, catégorie
- Affichage temps réel (tail) vs consultation statique
- Export / copie de logs

**Références** : `TODO.md §Affichage dans une form dédiée`

---

## 🔄 Processus de maintenance ADR

### Quand créer une nouvelle décision

Une décision doit être enregistrée dans ce document si elle répond à **au moins un** de ces critères :

| Critère | Exemples |
|---|---|
| Changement structurel de la DB | Nouvelle table, modification de FK, nouvelle convention |
| Nouveau pattern architectural | Nouveau module, nouveau flux de démarrage |
| Choix technologique | Nouveau package, abandon d'un outil |
| Convention transversale UI | Nouveau standard de form, nouveau helper |
| Décision de sécurité | Nouveau mécanisme de protection, règle de logging |
| Décision ouverte tranchée | Un `OD-xxx` qui devient `ARCH-xxx` ou `DB-xxx` |

### Quand mettre à jour une décision existante

- Modification du statut : `🔍 Ouverte → ✅ Adoptée` quand une décision ouverte est tranchée
- Révision : `✅ Adoptée → 🔄 Révisée` + création d'une nouvelle décision qui la remplace
- Abandon : `✅ Adoptée → ❌ Abandonnée` si la décision est explicitement révoquée

### Workflow de mise à jour

```
1. Identifier la décision à créer ou modifier
2. Rédiger dans ce fichier (ADR_Artefact.md)
3. Mettre à jour la Table des décisions (section synthétique)
4. Documenter dans CHANGELOG.md
5. Aligner Rules.md si c'est une convention de code
6. Aligner Process_Artefact.md si c'est un processus fonctionnel
```

### Revue périodique

| Déclencheur | Action |
|---|---|
| Migration de schéma DB | Vérifier [DB-005](#db-005) + créer/mettre à jour la décision DB concernée |
| Nouveau processus fonctionnel (Process 06+) | Créer les décisions ARCH/DB associées |
| Nouveau package NuGet | Créer/mettre à jour [ARCH-002](#arch-002) ou décision associée |
| Décision ouverte tranchée | Fermer l'OD + créer la décision adoptée |
| Revue trimestrielle | Vérifier les statuts, clore les OD résolues |

---

## 🔗 Liens croisés avec la documentation existante

| Document | Relation avec les ADR |
|---|---|
| [`Readme.md`](../Readme.md) | Vue d'ensemble technique — les ADR approfondissent les décisions mentionnées |
| [`VISION.md`](VISION.md) | Principes directeurs — les ADR les opérationnalisent |
| [`Rules.md`](Rules.md) | Conventions de code — certaines sont la conséquence directe d'une ADR |
| [`Process_Artefact.md`](Process_Artefact.md) | Processus fonctionnels — leurs choix architecturaux sont tracés ici |
| [`CHANGELOG.md`](CHANGELOG.md) | Chronologie des changements — source primaire des décisions documentées ici |
| [`TODO.md`](TODO.md) | Tâches planifiées — les `OD-xxx` y trouvent leur source |
| [`GLOSSAIRE.md`](GLOSSAIRE.md) | Terminologie partagée avec ce document |
| [`Database/ModeleDB.md`](Database/ModeleDB.md) | Modèle de données — conséquence directe des décisions `DB-xxx` |
| [`REPRISE.md`](REPRISE.md) | Guide de continuité — ce document en est le complément architectural |

---

*Document créé le 24/05/2026. Maintenu en parallèle du CHANGELOG.*  
*Toute décision structurante non documentée ici est une dette documentaire.*
