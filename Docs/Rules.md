# 📐 Règles de conception 

[TOC]

Ce document décrit les règles de conception et décisions techniques prises
lors de la création de la base de données **Artefact**.

Ces règles sont considérées comme **fondatrices** pour Artefact v1.

---

## 📐 Règles générales de développement

### 1. **🧠 Planifier d’abord**  

Comprendre, anticiper, questionner avant d’agir. Ne jamais foncer tête baissée. Une bonne analyse précède un bon code.

---

### 2. **🪜 Step by step, toujours.**  

Chaque étape est validée avant d’aller plus loin. On avance par petits pas, avec des points de contrôle réguliers pour éviter les dérives et garantir la cohérence globale.

---

### 3. **📜 Code clair, versionné, commenté**

- Nom explicite de la fonction/procédure.
- Commentaire en-tête obligatoire :
 - Entier copié (même pour une petite modif) pour garantir cohérence du contexte.
- Ajout systématique dans le changelog `.md` à la fin de chaque thread.
- Documentation des décisions prises et des raisons derrière chaque choix.

---

### 4. **📝 Documenter dès le départ**

- Changelog `.md` → **fin de la journée pour toute la journée**
- Résumé OneNote → en parallèle si besoin pour la journée
- README.md → si impact structurel ou de logique globale
- Rules.md → pour les règles de conception et de développement

---

### 5. **♻️ Factorisation**

- Jamais de doublon sans raison
- Créer ou réutiliser des fonctions centrales dans  des modules séparés et dédiés (ex : `Utils.vb`, `DatabaseManager.vb`, `ConfigManager.vb`, `GestionLog.vb`, etc.) pour éviter la redondance et faciliter la maintenance.
- Toujours se demander : "Est-ce que cette logique existe déjà quelque part ? Peut-elle être réutilisée ou adaptée ?"
- Ne pas hésiter à refactoriser si nécessaire.
	
   ---

### 6.**🧪 Target framework**

- `net8.0-windows` → attention aux packages compatibles

---

### 7. **🛠 Mémo Démarrage d’une Modif**

| Élément | Action |
|--------|--------|
| Version | Obligatoire (Vx.x + date) |
| Localisation du code | Module / classe / section |
| ToolstripStatus + log | Si UI ou process |
| Répercussions | Indiquer les appels, docs, etc. |

---

### 8.**🧭 Conventions de nommage**

#### Fichiers et classes

| Type | Nommage |
|------|---------|
| Entité DB | `Livre.vb`, `Auteur.vb` |
| Entité Calibre | `LivreCalibre.vb` |
| Entité non normalisée | `LivreAnorm.vb` |
| Manager | `DatabaseManager.vb`, `ConfigManager.vb` |
| Formulaire | `NomForm.vb` explicite |
| Module logique | `GestionLivresCalibre.vb`, etc. |
| Utilitaires | `Utils.vb`, `UtilsForm.vb` |

#### Fonctions

| Préfixe | Usage |
|---------|------|
| `GetOrInsertX()` | Lookup + Insert |
| `NettoyerX()` | Nettoyage / transformation |
| `ConstruireX()` | Génération (chemin, clause SQL…) |
| `TraiterX()` | Traitement global |
| `InsererXDansDB()` | Insertion DB |

#### Contrôles UI

| Préfixe | Contrôle |
|---------|----------|
| `txt`| TextBox |
| `but` | Boutons |
| `dtp` | DateTimePicker |
| `lsv` | ListView |
| `chk` | CheckBox |   
| `dgv` | DataGridView |
| `cmb` | ComboBox |
| `lbl` | Labels |
| `lst` | ListBox |
| `nud` | numericUpDown  |
| `tab` | TabControl |
| `grp` | GroupBox |
| `pnl` | Panel |
| `pic` | PictureBox |
| `tls` | ToolStrip |
| `men_` | MenuStrip |
| `sts` | StatusStrip |
| `wbw` | WebView2 |
| `pgb` | ProgressBar |
| `lil` | LinkLabel |
| `rtb` | RichTextBox |
| `trv` | TreeView |
| `spc` | SplitContainer |
| `flp` | FlowLayoutPanel |
| `tlp` | TableLayoutPanel |

---

### 9. 📐 **Database Artefact**

#### 1. Clés primaires (IDs)

##### Principe
- Toutes les tables utilisent une clé primaire numérique.
- Type : `BIGINT UNSIGNED`
- Pas de clé primaire composite.
- Pas de clé primaire textuelle.
- le nom de ID est toujours id_[nom de la Table] pour garantir la clarté et la cohérence à travers tout le schéma.

##### Convention
id_<nom_table>
	- Exemples : id_auteur / id_tag   /   id_serie

##### Génération des IDs (SEQUENCE)
- AUTO_INCREMENT n’est pas utilisé
- Chaque table nécessitant un ID lisible dispose de :
- une SEQUENCE dédiée
- un DEFAULT (NEXT VALUE FOR seq_xxx)
- Faire la création d'une nouvelle table en 2 fois sinon MariaDB n'accepte pas:
```SQL
 id_series_format BIGINT UNSIGNED NOT NULL,
```
 et ensuite avec un ALTER:
 ```SQL
 ALTER TABLE series_format
  ALTER COLUMN id_series_format SET DEFAULT (NEXT VALUE FOR seq_series_format);
 ```
  ==> Permet l’usage de colonnes GENERATED STORED et de garantir l’unicité des IDs même

```SQL
CREATE SEQUENCE seq_auteurs
    START WITH 1
    INCREMENT BY 1
    MINVALUE 1
    NO MAXVALUE
    CACHE 1
    NOCYCLE;

ALTER TABLE auteurs
  MODIFY COLUMN id_auteur BIGINT UNSIGNED NOT NULL;

ALTER TABLE auteurs
  ALTER COLUMN id_auteur SET DEFAULT (NEXT VALUE FOR seq_auteurs);
```

- **START WITH 1** - On démarre à 1 par convention.
- **INCREMENT BY 1** - Incrément strictement séquentiel, logique métier simple.
- **MINVALUE 1** - On interdit les valeurs négatives.
- **NO MAXVALUE** - On laisse la séquence monter tant que le type BIGINT le permet (9 quintillions, on est tranquille).
- **CACHE 1** - Très important.
Évite les sauts de 1000 observés avec le cache par défaut.
Garantit un comportement prévisible en phase dev.
L’impact performance est négligeable pour Artefact.
- **NOCYCLE** - Interdit le retour à 1 quand la limite max est atteinte (comportement dangereux si activé).

#### 2. TRUNCATE TABLE
- TRUNCATE TABLE ne réinitialise pas les séquences. ==> utiliser `ALTER SEQUENCE seq_xxx RESTART WITH 1` pour repartir à 1 si besoin.

#### 3. Codes lisibles (code_xxx)
- Chaque entité possède un code lisible
- Le code :
	- est stocké
	- est généré automatiquement
	- n’est jamais une clé primaire
	- n’est jamais utilisé comme clé étrangère

		```<Préfixe><ID sur 6 chiffres>```
##### Exemples :
- A000042 (Auteur)
- T000017 (Tag)
- S000003 (Série)

##### Implémentation
```SQL
ALTER TABLE auteurs
  ADD COLUMN code_auteur VARCHAR(12)
  GENERATED ALWAYS AS (
    CONCAT('A', LPAD(id_auteur, 6, '0'))
  ) STORED,
  ADD UNIQUE KEY uq_auteurs_code (code_auteur);
```
**Nota : Si Drop de table**
1. penser à supprimer la séquence associée pour éviter les conflits à la recréation.
2. penser à supprimer les références à la séquence dans les autres tables (DEFAULT NEXT VALUE FOR seq_xxx) pour éviter les erreurs à la recréation.
3. Penser à faire un DROP des clés primaires et étrangères à part avant de faire un DROP de table pour éviter les erreurs de dépendance et de recréation

### 10. 🛩️ **Connection Database et démarrage application**

- Un point d’accès centralisé.
- L’ouverture est gérée automatiquement dans `GetConnexionMariaDB()`.
- La fermeture est implicite via le bloc `Using`.
- Le pooling ADO.NET assure la performance.

##### Démarrage de l’application :

- L’application démarre sur la Form `PortailReferentiels`.
- `PortailReferentiels` constitue le point d’entrée principal vers les modules métier.
- La vérification de la connexion MariaDB est effectuée au démarrage via `DatabaseManager`.
- Aucun accès direct à la base ne doit être réalisé en dehors de `DatabaseManager`.

La connexion MariaDB est obtenue exclusivement via :

```
Using conn As MySqlConnection = DatabaseManager.GetConnexionMariaDB()
    If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
        ' Requête ici
    End If
End Using
```
- `conn.Open()` ne doit jamais être appelé manuellement.
- L’ouverture est gérée automatiquement dans `GetConnexionMariaDB()`.
- La fermeture est implicite via le bloc `Using`.
- Le pooling ADO.NET assure la performance.

---

### 11. 🧩 **Modèle UI obligatoire (Form / UC)**

Tous les écrans de l’application (Forms ou UserControls) doivent hériter ou reproduire la structure standard suivante :

##### Contrôles obligatoires :

- `lblTitreForm` → Label titre principal
- `stsStatus` → StatusStrip
- `stsLabelStatus` → ToolStripStatusLabel
- `pnlForm` → Panel principal de contenu

##### Nommage strict :

```
Me.stsStatus.Name = "stsStatus"
stsLabelStatus.Name = "stsLabelStatus"
pnlForm.Name = "pnlForm"
```

##### Règles :

- Aucun MessageBox pour les statuts normaux.
- Les messages passent par `AfficherMessageStatus`.
- Les logs passent par `GestionLog`.

##### Structuration UI (Pannelisation obligatoire)

Tous les écrans doivent être structurés par zones logiques :
- ex:
- `pnlTop` → Titre / infos générales
- `pnlActions` → Boutons
- `pnlForm` → Contenu principal (de base dans le modèle)
- `pnlBottom` → Informations secondaires si nécessaire

##### Objectifs :

- UI modulaire
- Maintenance facilitée
- Refactoring plus simple
- Lisibilité du code designer
- Cohérence visuelle globale

==> Aucun contrôle métier directement posé sur la Form racine en vrac.

==> Tout est contenu dans des Panels dédiés.

---

### 12. 📜 **Standards de structure du code (Obligatoire)**

#### 1️⃣ En-tête de fichier obligatoire

Chaque nouveau fichier (Class, Module, Form) doit commencer par un en-tête descriptif et versionné.

```vb
'------------------------------------------------------------
' 📌 DatabaseManager.vb
' Version : V1.0
' Date    : 18/02/2026
' Auteur  : Joëlle
'
' Rôle :
' Gestion centralisée de la connexion MariaDB.
' Point d’accès unique à la base Artefact.
'
' Évolution :
' - V1.0 : Création de la structure initiale.
'------------------------------------------------------------
```
#### 2️⃣ Versionnage systématique des procédures / fonctions

Chaque Sub ou Function doit comporter :
- Version
- Date
- Description claire
- Indication des appels si pertinent

```vb
'------------------------------------------------------------
' 📌 V1.0 - 18/02/2026
' GetConnexionMariaDB
'
' Retourne une connexion MariaDB ouverte.
' Initialise la chaîne de connexion si nécessaire.
'
' Appelé par : PortailReferentiels_Load, ConfigManager
'------------------------------------------------------------
Public Shared Function GetConnexionMariaDB() As MySqlConnection
```
#### 3️⃣ Commentaire fonctionnel court

Chaque procédure doit contenir un commentaire synthétique expliquant :
- Ce qu’elle fait
- Ce qu’elle retourne
- Les cas d’erreur gérés

Pas de roman. Pas de paraphrase du code. Juste l’intention.

#### 4️⃣ Structuration par régions

- **Organisation par régions** : `#Region "Section"` pour structurer le code en sections logiques (Déclarations, Constructeurs, Puis pour chaque grand processus utilisé.)
- Obligatoire pour : Modules, Forms
- Essayer de garder le même nom de Région pour les mêmes types de sections dans tout le projet (ex : "Processus X" pour les processus métier, "Variables / Constantes / Enum" pour les déclarations, etc.)
- Recommandé si nécessaire pour classes volumineuses.
- Ordre standard : Permet une navigation rapide et une meilleure lisibilité :
```vb
#Region "Imports"
 .....
#End Region
_______________________________________________
#Region "Variables / Constantes / Enum"
......
#End Region
_______________________________________________
#Region "Processus X"
........
#End Region
_______________________________________________
```
##### Objectifs :

- Lisibilité
- Maintenance facilitée
- Navigation rapide
- Structure cohérente dans tout le projet

#### 5️⃣ Principe architectural

- Une classe = une responsabilité principale.
- Si une classe commence à gérer plusieurs processus distincts → refactorisation envisagée.

### 13. 🧩 **Organisation des dossiers**

- `Core` : infrastructure système
  - DatabaseManager
  - ConfigManager
  - QueryModule
  - GestionLog
- `Utils` : outils génériques transversaux
- `Forms_Menu` : navigation principale
- `Forms_*` : regroupement par domaine métier

Principe :
Tout élément nécessaire au démarrage de l'application appartient à `Core`.

### 14. 🔄 **Initialisation des services**

- L’initialisation des services critiques (DB, Config, etc.) doit être :
  - explicite au démarrage
  - avec fallback lazy sécurisé

### 15. 📦 **Packages NuGet**

- Pour MariaDB / MySQL sous .NET 8 :
  - Utiliser **MySqlConnector**
  - Éviter MySql.Data sauf contrainte spécifique

### 16. 🧱 **Bootstrap Infrastructure**

Lors de la mise en place initiale :

- Un mode bootstrap temporaire est autorisé
- Il doit être clairement identifié comme transitoire
- Il doit être supprimé dès que ConfigManager est opérationnel

### 17. 🔐 **Gestion des mots de passe (Infrastructure)**

- Toute modification de mot de passe doit être explicite (mode dédié).
- La logique de décision du mot de passe ne doit jamais être implicite.
- Une seule méthode est autorisée pour construire un modèle depuis l’UI (ex : BuildConfigFromUI()).
- Les méthodes de test ne doivent jamais dépendre d’un état interne global (_cfg).
- Une form ne peut se fermer automatiquement que si l’action métier est validée.
- Aucune sauvegarde ne doit intervenir après un test échoué.

### 18. 📌 **Versionnement du schéma DB**

#### 1. Principe
Toute modification structurelle de la base Artefact doit :
- être réalisée via un script SQL numéroté (`00N_description.sql`)
- mettre à jour `meta_schema.schema_version`
- être accompagnée de la mise à jour de `ExpectedSchemaVersion` dans l'application

#### 2. Synchronisation obligatoire
La version du schéma en base et la constante `ExpectedSchemaVersion` doivent toujours être identiques.

#### 3. Démarrage applicatif
Au démarrage :
- L'application vérifie la compatibilité du schéma.
- Aucun contournement silencieux n'est autorisé.
- En cas de mismatch, le statut `SchemaMismatch` est retourné et loggé.

#### 4. Discipline
Aucun changement structurel direct en base sans :
- Script versionné
- Mise à jour du changelog
- Alignement de la constante applicative

### 19. 📌 **Logging Production**

#### 1. Philosophie

Les niveaux de log (Rapide, Succinct, Complet) sont des marqueurs de profondeur, pas un mécanisme de réduction.
```vb	
GestionLog.EcrireLog("UI: config DB locale chargée.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI)
```
Toute information loguée doit être écrite.

#### 2. Gestion des erreurs

Tout Catch doit comporter :
- Un log Succinct (description synthétique)
- Un log avec exception (ex.Message)
- Si nécessaire, niveau Complet pour contexte technique

```vb
 Catch ex As Exception
            stsLabelStatus.Text = "Erreur au chargement de la configuration."
            GestionLog.EcrireLog("UI: erreur Load GestionConnexionMariaDb.", GestionLog.LogLevel.Succinct, GestionLog.LogCategory.UI, ex)

            ' Point critique : la form ne peut pas fonctionner correctement
            MessageBox.Show(
            "Erreur au chargement de la configuration." & Environment.NewLine & ex.Message,
            "Artefact",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )
```

Aucune exception ne doit être silencieuse.

#### 3. Sécurité

Il est strictement interdit de logguer :
- Mot de passe
- Connection string complète contenant un secret

Les messages doivent passer par le mécanisme de masquage.

#### 4. Session Header

Chaque exécution de l'application doit produire un header de session dans le fichier journalier incluant :
- Date/Heure
- Machine
- User Windows

#### 5. Discipline

Tout nouveau processus doit intégrer :
- Logging structuré
- Gestion d’erreur complète
- Catégorie adaptée (Startup, Database, UI, Process)

###  20. 📌 **Séparation des responsabilités (Logging & Crypto)**

1. Les modules techniques bas niveau (Crypto, DTO, modèles) ne doivent pas logguer.
   Le logging appartient aux couches d’orchestration ou d’infrastructure.

2. Toute opération IO (lecture/écriture fichier) doit :
   - être encapsulée dans un Try/Catch
   - logguer l’erreur avec exception complète
   - relancer l’exception si critique

3. Toute opération de sécurité (DPAPI, déchiffrement) doit :
   - Throw une exception explicite
   - Ne jamais masquer une erreur
   - Ne jamais logguer de secret

4. Une seule source de vérité pour :
   - Construction de connection string
   - Calcul des chemins système

###  21.  📌 **UI & Sécurité des mots de passe**

1. Le mot de passe ne doit jamais être affiché en clair par défaut.
2. Toute visualisation en clair doit être :
   - temporaire (action maintenue)
   - volontaire (action utilisateur explicite)
   - non logguée
3. Aucun secret (mot de passe, connection string complète) ne doit apparaître dans les logs.

###  22. 📌 **Modèle Form de base**

<img src="Images/Form_Modele._Plus.png" alt="FormModele" style="zoom:70%;" />

###  23. 📌 **Validation UI**

1. Les validations de formulaire :
   - n'utilisent pas de MsgBox
   - utilisent StatusStrip + Focus
   - logguent uniquement en cas de KO (niveau Rapide, catégorie UI)

2. Les MsgBox sont réservées :
   - aux blocages critiques
   - aux erreurs empêchant la poursuite normale du flux

###  24.  📌 **Orchestration Startup**

1. AppStartupManager :
   - ne contient aucune logique UI
   - ne déclenche aucun MsgBox
   - retourne uniquement un statut

2. Toute erreur critique au démarrage :
   - est logguée (Succinct + exception)
   - provoque la fermeture contrôlée de l'application

###  25. 📌 **Référentiels (mise à jour 02/03/2026)**

#### 1. Architecture Référentiels

- Une form par référentiel.
- Pas de mega-form multi-onglets.
- Séparation stricte des couches :
  - SQL → QueryModule
  - Exécution DB → GestionReferentiel
  - UI → Form
  - Entité métier → Classe dédiée (ex: Langue)

#### 2. Design UI

- Ne pas utiliser SplitContainer (instabilité Designer).
- Utiliser TableLayoutPanel pour structuration liste/détails.
- Limiter les panels superflus.
- Pattern standard :
  - pnlTop (recherche)
  - tlpMain (liste + détails)
  - pnlActions (boutons)
  - StatusStrip standard (stsStatus + stsLabelStatus)

#### 3. Modes d'édition

- Enum ModeEdition partagé via UtilsForms.
- Modification uniquement via bouton explicite (Option B).
- Pas de passage automatique en mode Modification sur TextChanged.
- Annulation via snapshot des données initiales.

#### 4. Chargement des données

- Référentiels à faible volume :
  - Chargement automatique au Load.
- Référentiels volumineux (ex: Tags) :
  - Pas de chargement complet automatique.
  - Recherche obligatoire ou bouton explicite.

#### 5. Validation UI

- Utiliser errProvider pour validation locale.
- Utiliser StatusStrip pour message global.
- Ne pas utiliser MessageBox pour validation simple.
- Exceptions DB → log via GestionLog.

#### 6. Majuscules/minuscules
- CharacterCasing = Upper côté UI.
- Normalisation appliquée côté DB via helper DbValueOrNullUpper.

#### 7. Standardisation DataGridView Référentiels

Tous les DataGridView référentiels doivent utiliser :

- FormatReferentielGrid(dgv)
- HighlightMainColumn(dgv)

Règles visuelles :
- Header centré + bold.
- Lignes alternées sobres.
- Sélection douce.
- Colonne principale (nom_xxx) en gras.
- Aucune décoration excessive.

#### 8. Factorisation autorisée

Helpers génériques autorisés dans UtilsForms uniquement si :
- utilisés au minimum dans 2 référentiels,
- indépendants de la logique métier.

Exemples validés :
- ModeEdition (Enum partagé)
- DgvGetSelectedId
- DgvSelectRowById
- FormatReferentielGrid
- HighlightMainColumn

#### 9. Pattern Référentiel validé

Le pattern est désormais considéré stable et reproductible.
Tout nouveau référentiel doit suivre strictement la checklist officielle.

#### 10. Référentiels structurés (parent/enfant)

Certains référentiels peuvent être composés de deux niveaux :

- une table **type**
- une table **valeur**

Exemple :

- `ref_enum_type`
- `ref_enum`

##### Principe

- la table **type** définit les catégories
- la table **valeur** contient les valeurs appartenant à ces catégories
- les tables métier référencent toujours la table **valeur**

##### Avantages

- meilleure normalisation
- meilleure extensibilité
- structure claire pour l'UI

##### Conventions

Codes techniques :

- `code_type` → MAJUSCULES
- `code_valeur` → MAJUSCULES

Libellés :

- `libelle_type`
- `libelle_valeur`

Tri :

- `ordre_affichage`
- puis `libelle`

#### 11. Gestion des suppressions dans les référentiels

Tous les référentiels doivent appliquer une logique de suppression cohérente côté application afin d’éviter l’apparition d’erreurs SQL brutes liées aux contraintes de clés étrangères.

##### 	Principe général

Avant toute suppression d’un élément référentiel :

1. vérifier les dépendances dans les tables liées
2. distinguer les cas `RESTRICT` et `SET NULL`
3. informer clairement l’utilisateur
4. bloquer la suppression si nécessaire

##### 	Cas RESTRICT

Si une table possède une FK avec `RESTRICT` vers le référentiel :

- la suppression doit être bloquée côté application
- un message explicite doit indiquer les tables utilisant encore l’élément

##### 	Cas SET NULL

Si une table possède une FK avec `SET NULL` :

- la suppression reste possible
- l’utilisateur doit être averti que les références seront vidées

##### 	Règle de maintenance

Si une nouvelle table référence un référentiel existant :

- les contrôles de suppression doivent être mis à jour
- les requêtes de comptage d’usage doivent être complétées
- les messages utilisateur doivent être adaptés.

Cette règle est particulièrement importante pour les référentiels fortement partagés comme `ref_enum`.

#### 12. Synchronisation DataGridView / Détails

Dans les écrans de gestion référentielle utilisant une DataGridView :

- La ligne courante doit être déterminée à partir de `DataGridView.CurrentRow`.
- L’utilisation de `SelectedRows(0)` doit être évitée pour la synchronisation des détails.
- La méthode `BindSelectedToDetails()` doit toujours utiliser `CurrentRow`.

##### Exemple :
`If dgv.CurrentRow Is Nothing Then Exit Sub
Dim row As DataGridViewRow = dgv.CurrentRow`

 ##### Raison :
SelectedRows et CurrentRow peuvent diverger après certaines opérations (reload, suppression, changement de mode), ce qui peut provoquer une désynchronisation entre la grille et le panneau de détails.

#### 13.  Événement obligatoire pour les référentiels

Chaque DataGridView utilisée dans un référentiel doit implémenter l'événement :

`SelectionChanged`

Cet événement doit appeler BindSelectedToDetails() lorsque l'application est en mode Consultation.

#####  But :
Garantir la synchronisation immédiate entre la sélection utilisateur et les champs détail.

 ### 26. 📌**Gestion des champs de notes enrichies**

 Lorsqu'un champ de notes nécessite une mise en forme (gras, italique, souligné, listes), l'application utilise une **RichTextBox avec stockage RTF persistant**.

#### Séparation stricte UI / Métier / DB

Un contrôle UI ne doit jamais mélanger :
- une valeur de filtre (ex: "Toutes origines")
- une valeur métier persistée en base

Les valeurs sentinelles (ex: 0) sont strictement interdites dans les champs métier.

#### Champs RichText standard

Tout champ nécessitant une mise en forme doit être implémenté avec :

- xxx_rtf : stockage riche
- xxx_txt : texte brut pour recherche

Aucune exception.

#### Recherche

Les recherches SQL doivent :
- utiliser exclusivement les colonnes `_txt`
- ne jamais utiliser les colonnes `_rtf`

#### UI RichText

Toute manipulation RichText doit passer par :
`RichTextNotesHelper`

Interdictions :
- manipulation directe du RTF dans les écrans UI
- duplication de logique

#### ComboBox (critique)

Une ComboBox ne peut pas avoir un double rôle :
- filtre
- champ métier

Si nécessaire :
- utiliser deux contrôles distincts

Sinon :
- prévoir validation stricte empêchant toute valeur invalide en base

###  27. 📌 **Gestion des recommandations**

Un système de recommandations permet d'enregistrer les sources ayant suggéré un livre.

#### Principe

Une recommandation est considérée comme un **événement documenté** provenant d'une source externe ou humaine.

Exemples :

- TikTok
- Blog
- Ami
- Libraire
- Podcast
- Réseau social

#### Structure

Les recommandations sont stockées dans la table :
`recommandations`
Chaque recommandation contient :
- une origine (référentiel `ref_origine_recommandation`)
- un nom ou identifiant de source
- éventuellement une URL
- une date
- un commentaire utilisateur

#### Association aux livres

Une recommandation peut être associée :

- à un livre normalisé (`livres`)
- à un livre en phase de staging (`livres_staging`)

L'association est réalisée via des **tables de liaison** :

- `livres_recommandations`
- `livres_staging_recommandations`

#### Justification

Ce modèle permet :

- plusieurs recommandations pour un même livre
- plusieurs sources distinctes
- une séparation claire entre :
  - l'événement de recommandation
  - l'état du livre dans le système
	
### Règle - ComboBox (critique)

Une ComboBox ne peut pas avoir un double rôle :
- filtre
- champ métier

Si nécessaire :
- utiliser deux contrôles distincts

Sinon :
- prévoir validation stricte empêchant toute valeur invalide en base
