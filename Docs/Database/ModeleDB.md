#  Architecture de la base de données Artefact

[TOC]

 Mise à jour du 30/05/2026

---

## 🧠 Principes fondateurs

1. Clé primaire numérique unique par table
2. Aucune clé primaire textuelle
3. Pas d’usage de `AUTO_INCREMENT` (SEQUENCE utilisée)
4. `id_xxx` = technique
5. `code_xxx` = lisible humain
6. Les `code_xxx` ne sont jamais des clés étrangères
7. Référentiels créés avant les tables métier

### 🆔 Gestion des identifiants

#### 🔹 ID technique

- Type : `BIGINT UNSIGNED`
- Alimenté par une **SEQUENCE dédiée**

- Défini par :

  ```
  DEFAULT (NEXT VALUE FOR seq_xxx)
  ```

#### 🔹 Code lisible

Format :

```
<Préfixe><ID sur 6 chiffres>
```

Exemples :

- A000042
- B000001
- LF000012

Implémentation type :

```
GENERATED ALWAYS AS (
  CONCAT('A', LPAD(id_auteur, 6, '0'))
) STORED
```

### 🧱Liste de tables avec leur Séquences éventuelles

Tables avec leurs séquences dédiées :

| Type Table         | Table                          | Table Séquence                 | PK                                                        | Code colonne                | Préfixe |
| ------------------ | ------------------------------ | ------------------------------ | --------------------------------------------------------- | --------------------------- | -----|
| Référentielle      | auteurs                        | seq_auteurs                    | id_auteur                                                 | code_auteur                 |    A    |
| Référentielle      | tags                           | seq_tags                       | id_tag                                                    | code_tag                    |    T    |
| Référentielle      | series                         | seq_series                     | id_serie                                                  | code_serie                  |    S    |
| Sous-Référentielle | series_format                  | seq_series_format              | id_series_format                                          | code_series_format          |   SF    |
| Sous-Référentielle | series_statut                  | seq_series_statut              | id_series_statut                                          | code_series_statut          |   SS    |
| Référentielle      | editeurs                       | seq_editeurs                   | id_editeur                                                | code_editeur                |    E    |
| Référentielle      | contacts                       | seq_contacts                   | id_contact                                                | code_contact                |    C    |
| Référentielle      | impression                     | seq_impression                 | id_impression                                             | code_impression             |    I    |
| Référentielle      | langues                        | seq_langues                    | id_langue                                                 | code_langue                 |    L    |
| Référentielle      | pays                           | seq_pays                       | id_pays                                                   | code_pays                   |    P    |
| Référentielle      | prixlit                        | seq_prixLit                    | id_prixLit                                                | code_prixLit                |    R    |
| Sous-Référentielle | prixlit_annee                  | seq_prixLit_Annee              | id_prixLit_annee                                          | code_prixLit_Annee          |   RA    |
| Sous-Référentielle | prixlit_categorie              | seq_prixlit_categorie          | id_prixlit_categorie                                      | code_prixlit_categorie      |   RC    |
| Référentielle      | formatFile                     | seq_formatFile                 | id_formatFile                                             | code_formatFile             |    F    |
| Référentielle      | recommandations                | seq_recommandations            | id_recommandation                                         | code_recommandation         |    R    |
| Sous-Référentielle | ref_origine_recommandation     | seq_ref_origine_recommandation | id_origine_recommandation                                 | code_origine_recommandation |   OR    |
| Principale         | livres                         | seq_livres                     | id_livre                                                  | code_livre                  |    B    |
| Générique          | ref_enum                       | seq_ref_enum                   | id_enum                                                   | code_enum                   |    N    |
| Sous-générique     | ref_enum_type                  | seq_ref_enum_type              | id_enum_type                                              | code_enum_type              |   ET    |
| Liaison            | livres_fichier                 | seq_livres_fichier             | id_livre_fichier                                          | code_livre_fichier          |   LF    |
| Sous-Liaison       | auteurs_pays                   | PK Composite                   | id_auteur + id_pays + id_type_relation_pays (ref_enum)    |                             |         |
| Liaison            | livres_auteurs                 | PK Composite                   | id_livre  +  id_auteur + id_role_auteur (ref_enum)        |                             |         |
| Liaison            | livres_tags                    | PK Composite                   | id_livre +  id_tag                                        |                             |         |
| Liaison            | livres_prixLit_annee           | PK Composite                   | id_livre  +  idprixlit_annee                              |                             |         |
| Liaison            | livres_contacts                | PK Composite                   | id_livre  +  id_contact                                   |                             |         |
| Liaison            | livres_recommandations         | PK Composite                   | id_livre + id_recommandation                              |                             |         |
| Principal          | livres_staging                 | Seq_livres_staging             | id_livre_staging                                          | code_livre_staging          |   BS    |
| Liaison            | livres_staging_auteurs         | PK Composite                   | id_livre_staging  + id_auteur + id_role_auteur (ref_enum) |                             |         |
| Liaison            | livres_staging_recommandations | PK Composite                   | id_livre_staging +  id_recommandation                     |                             |         |
| Paramètre          | param_api                      | seq_param_api                  | id_param_api                                              | code_param_api              |   AP    |
| Paramètre          | param_db                       | seq_param_db                   | id_param_db                                               | code_param_db               |   DB    |
| Paramètre          | param_paths                    | seq_param_paths                | id_param_paths                                            | code_param_path             |   PA    |
| Configuration      | meta_schema                    |                                | id                                                        |                             |         |

---

# 📚 Modèle fonctionnel des données

## TABLES

 >ℹ️ Les sections détaillées sont complétées progressivement selon l'avancement fonctionnel.
> Pour garder une base exploitable dès maintenant, un tableau de colonnes consolidé est maintenu ici : [`Tableaux_Colonnes.md`](Tableaux_Colonnes.md).

### 1️⃣ Tables principales

#### 	🥅`livres`
Table centrale du modèle, pivot de l’ensemble du système.
Contient exclusivement des livres :

- validés  = bibliothèque validée
- normalisés
- reliés aux référentiels
- prêts à être exploités

| Colonne               | Type                  | Null | Default                        | Clé/Contrainte                           |
| --------------------- | --------------------- | ---- | ------------------------------ | ---------------------------------------- |
| `id_livre`            | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_livres)` | PK                                       |
| `id_calibre`          | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX                                    |
| `code_livre`          | `varchar(12)`         | YES  | ``                             | UNIQUE; GENERATED                        |
| `titre`               | `varchar(350)`        | NO   | ``                             | INDEX                                    |
| `titre_normalise`     | `varchar(380)`        | YES  | `NULL`                         | INDEX                                    |
| `annee_publication`   | `smallint(6)`         | YES  | `NULL`                         | INDEX                                    |
| `date_publication`    | `date`                | YES  | `NULL`                         | INDEX                                    |
| `synopsis`            | `text`                | YES  | `NULL`                         |                                          |
| `commentaire`         | `text`                | YES  | `NULL`                         |                                          |
| `id_langue`           | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `langues`.`id_langue`        |
| `id_impression`       | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `impression`.`id_impression` |
| `id_editeur`          | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `editeurs`.`id_editeur`      |
| `id_serie`            | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `series`.`id_serie`          |
| `num_tome`            | `decimal(6,2)`        | YES  | `NULL`                         | INDEX                                    |
| `tome_libelle`        | `varchar(50)`         | YES  | `NULL`                         |                                          |
| `id_statut_lecture`   | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `ref_enum`.`id_enum`         |
| `id_support_lecture`  | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `ref_enum`.`id_enum`         |
| `id_type_acquisition` | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `ref_enum`.`id_enum`         |
| `created_at`          | `datetime`            | NO   | `current_timestamp()`          |                                          |
| `updated_at`          | `datetime`            | NO   | `current_timestamp()`          |                                          |

###### Diagram

<img src="Diagrams/artefact - livres_All.png" alt="livres" style="zoom:90%;" />

###### Extrait de données

> [!WARNING]
>
> 

#### 	🥅`livres_staging`
Table de staging pour l’import des données brutes issues de diverses sources
 Zone tampon d’intégration = zone de transition
  - Contient :
	- imports Calibre bruts
	 - ajouts incomplets
	 - données non reliées
- Différence majeure :
   - Beaucoup de champs en texte libre
  - Référentiels non encore liés
  - Auteur relié via table de liaison
- Objectif : validation humaine avant passage vers `livres`.

> Les nouveautés éditoriales externes, les recommandations etc.. seront gérées par des tables distinctes.

| Colonne                    | Type                  | Null | Default                                | Clé/Contrainte                           |
| -------------------------- | --------------------- | ---- | -------------------------------------- | ---------------------------------------- |
| `id_livre_staging`         | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_livres_staging)` | PK                                       |
| `code_livre_staging`       | `varchar(12)`         | YES  | ``                                     | UNIQUE; GENERATED                        |
| `id_source_import`         | `bigint(20) unsigned` | YES  | `NULL`                                 | INDEX; FK → `ref_enum`.`id_enum`         |
| `id_calibre`               | `bigint(20) unsigned` | YES  | `NULL`                                 | INDEX                                    |
| `url_source`               | `varchar(600)`        | YES  | `NULL`                                 |                                          |
| `date_import`              | `datetime`            | NO   | `current_timestamp()`                  |                                          |
| `titre_source`             | `varchar(500)`        | NO   | ``                                     |                                          |
| `avec_fichier`             | `tinyint(1)`          | NO   | `0`                                    |                                          |
| `titre_normalise`          | `varchar(500)`        | YES  | `NULL`                                 | INDEX                                    |
| `langue_source`            | `varchar(80)`         | YES  | `NULL`                                 |                                          |
| `editeur_source`           | `varchar(255)`        | YES  | `NULL`                                 |                                          |
| `isbn_source`              | `varchar(20)`         | YES  | `NULL`                                 |                                          |
| `annee_publication_source` | `int(11)`             | YES  | `NULL`                                 |                                          |
| `date_publication_source`  | `date`                | YES  | `NULL`                                 |                                          |
| `synopsis_source`          | `text`                | YES  | `NULL`                                 |                                          |
| `tags_source`              | `text`                | YES  | `NULL`                                 |                                          |
| `serie_source`             | `varchar(255)`        | YES  | `NULL`                                 |                                          |
| `id_serie`                 | `bigint(20) unsigned` | YES  | `NULL`                                 | INDEX; FK → `series`.`id_serie`          |
| `id_impression`            | `bigint(20) unsigned` | YES  | `NULL`                                 | INDEX; FK → `impression`.`id_impression` |
| `num_tome_source`          | `decimal(6,2)`        | YES  | `NULL`                                 |                                          |
| `tome_libelle_source`      | `varchar(50)`         | YES  | `NULL`                                 |                                          |
| `id_statut_staging`        | `bigint(20) unsigned` | YES  | `NULL`                                 | INDEX; FK → `ref_enum`.`id_enum`         |
| `commentaire_staging`      | `varchar(500)`        | YES  | `NULL`                                 |                                          |
| `id_livre_cible`           | `bigint(20) unsigned` | YES  | `NULL`                                 | INDEX; FK → `livres`.`id_livre`          |
| `created_at`               | `datetime`            | NO   | `current_timestamp()`                  |                                          |
| `updated_at`               | `datetime`            | NO   | `current_timestamp()`                  |                                          |

###### Diagram

<img src="Diagrams/artefact - livres_staging_All.png" alt="livres_staging" style="zoom:90%;" />

###### Extrait de données

> [!WARNING]
>
> 

### 2️⃣ Gestion des fichiers

#### 	🥅`livres_fichiers`
Table de liaison entre les livres et leurs fichiers associés (ebooks, couvertures, staging etc..)
- Table unique pour tous les fichiers :

  -  ebooks

  - couvertures

  - fichiers liés au staging

-  Le fichier principal d’un livre est déterminé par :
   -  ordre_affichage le plus bas
   -  Priorité logique :
      -  ePub
      -  AZW3
      -  sinon format unique

| Colonne              | Type                  | Null | Default                                       | Clé/Contrainte                                          |
| -------------------- | --------------------- | ---- | --------------------------------------------- | ------------------------------------------------------- |
| `id_livre_fichier`   | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_livres_fichiers)`       | PK                                                      |
| `code_livre_fichier` | `varchar(12)`         | YES  | ``                                            | UNIQUE; GENERATED                                       |
| `id_scope_livre`     | `bigint(20) unsigned` | NO   | `` | UNIQUE; INDEX; FK → `ref_enum`.`id_enum` |                                                         |
| `id_livre`           | `bigint(20) unsigned` | YES  | `NULL`                                        | UNIQUE; INDEX; FK → `livres`.`id_livre`                 |
| `id_livre_staging`   | `bigint(20) unsigned` | YES  | `NULL`                                        | UNIQUE; INDEX; FK → `livres_staging`.`id_livre_staging` |
| `id_type_fichier`    | `bigint(20) unsigned` | NO   | `` | UNIQUE; INDEX; FK → `ref_enum`.`id_enum` |                                                         |
| `id_formatFile`      | `bigint(20) unsigned` | YES  | `NULL`                                        | UNIQUE; INDEX; FK → `formatfile`.`id_formatFile`        |
| `chemin_fichier`     | `varchar(800)`        | NO   | ``                                            | UNIQUE                                                  |
| `nom_fichier`        | `varchar(255)`        | YES  | `NULL`                                        |                                                         |
| `extension_source`   | `varchar(20)`         | YES  | `NULL`                                        |                                                         |
| `taille_octets`      | `bigint(20) unsigned` | YES  | `NULL`                                        |                                                         |
| `hash_sha1`          | `char(40)`            | YES  | `NULL`                                        |                                                         |
| `is_principal`       | `tinyint(1)`          | NO   | `0`                                           |                                                         |
| `created_at`         | `datetime`            | NO   | `current_timestamp()`                         |                                                         |
| `updated_at`         | `datetime`            | NO   | `current_timestamp()`                         |                                                         |

###### Diagram

<img src="Diagrams/artefact - livres_fichiers_All.png" alt="livres_fichiers" style="zoom:90%;" />

###### Extrait de données

> [!WARNING]
>
> 

### 3️⃣ Tables référentielles

#### 	🥅`auteurs`
table référentielle des auteurs, avec des champs dédiés à la gestion de la biographie, de la nationalité, de la langue d’écriture, de la photo etc.. 
| Colonne                | Type                  | Null | Default                         | Clé/Contrainte                    |
| ---------------------- | --------------------- | ---- | ------------------------------- | --------------------------------- |
| `id_auteur`            | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_auteurs)` | PK                                |
| `nom_auteur`           | `varchar(150)`        | NO   | ``                              | INDEX                             |
| `prenom_auteur`        | `varchar(150)`        | YES  | `NULL`                          |                                   |
| `nomcomplet_auteur`    | `varchar(320)`        | NO   | ``                              | INDEX                             |
| `id_pays`              | `bigint(20) unsigned` | YES  | `NULL`                          | INDEX; FK → `pays`.`id_pays`      |
| `id_langue_ecriture`   | `bigint(20) unsigned` | YES  | `NULL`                          | INDEX; FK → `langues`.`id_langue` |
| `date_naissance`       | `date`                | YES  | `NULL`                          |                                   |
| `annee_naissance`      | `smallint(6)`         | YES  | `NULL`                          |                                   |
| `biographie`           | `text`                | YES  | `NULL`                          |                                   |
| `site_web`             | `varchar(300)`        | YES  | `NULL`                          |                                   |
| `chemin_photo_auteur`  | `varchar(600)`        | YES  | `NULL`                          |                                   |
| `nom_normalise`        | `varchar(160)`        | YES  | `NULL`                          |                                   |
| `prenom_normalise`     | `varchar(160)`        | YES  | `NULL`                          |                                   |
| `nomcomplet_normalise` | `varchar(340)`        | YES  | `NULL`                          |                                   |
| `is_actif`             | `tinyint(1)`          | NO   | `1`                             |                                   |
| `created_at`           | `datetime`            | NO   | `current_timestamp()`           |                                   |
| `updated_at`           | `datetime`            | NO   | `current_timestamp()`           |                                   |
| `code_auteur`          | `varchar(12)`         | YES  | ``                              | UNIQUE; GENERATED                 |

###### Diagrams

<img src="Diagrams/artefact - auteurs_All.png" alt="auteurs" style="zoom:90%;" />

###### Extrait de données

> [!CAUTION]
>
> 

#### 	🥅`series`

table référentielle des séries. Contient des champs dédiés à la gestion du format de la série (roman, BD, manga etc..), de son statut (en cours, terminée, abandonnée etc..), du nombre de tomes total, du prochain tome à paraître etc.. 

| Colonne                | Type                  | Null | Default                        | Clé/Contrainte                                 |
| ---------------------- | --------------------- | ---- | ------------------------------ | ---------------------------------------------- |
| `id_serie`             | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_series)` | PK                                             |
| `id_series_format`     | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `series_format`.`id_series_format` |
| `id_series_statut`     | `bigint(20) unsigned` | YES  | `NULL`                         | INDEX; FK → `series_statut`.`id_series_statut` |
| `id_format`            | `int(11)`             | YES  | `NULL`                         | INDEX                                          |
| `id_statut`            | `int(11)`             | YES  | `NULL`                         | INDEX                                          |
| `nom_serie`            | `varchar(200)`        | NO   | ``                             | UNIQUE                                         |
| `serie_normalise`      | `varchar(220)`        | YES  | `NULL`                         | INDEX                                          |
| `nombre_tomes_total`   | `int(11)`             | YES  | `NULL`                         |                                                |
| `prochain_tome_numero` | `int(11)`             | YES  | `NULL`                         |                                                |
| `prochain_tome_date`   | `date`                | YES  | `NULL`                         |                                                |
| `pitch_serie`          | `text`                | YES  | `NULL`                         |                                                |
| `is_terminee`          | `tinyint(1)`          | NO   | `0`                            |                                                |
| `created_at`           | `datetime`            | NO   | `current_timestamp()`          |                                                |
| `updated_at`           | `datetime`            | NO   | `current_timestamp()`          |                                                |
| `code_serie`           | `varchar(12)`         | YES  | ``                             | UNIQUE; GENERATED                              |

###### Diagrams

<img src="Diagrams/artefact - series_All.png" alt="series" style="zoom:90%;" />

###### Extrait de données

> [!WARNING]
>
> 

#### 	🥅`editeurs`

table référentielle des éditeurs. Contient des champs dédiés à la gestion du pays d’origine de l’éditeur, de son site web, de ses notes diverses etc.. 
| Colonne             | Type                  | Null | Default                          | Clé/Contrainte               |
| ------------------- | --------------------- | ---- | -------------------------------- | ---------------------------- |
| `id_editeur`        | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_editeurs)` | PK                           |
| `nom_editeur`       | `varchar(200)`        | NO   | ``                               | UNIQUE                       |
| `id_pays`           | `bigint(20) unsigned` | YES  | `NULL`                           | INDEX; FK → `pays`.`id_pays` |
| `site_web`          | `varchar(300)`        | YES  | `NULL`                           |                              |
| `notes_editeur_rtf` | `mediumtext`          | YES  | `NULL`                           |                              |
| `notes_editeur_txt` | `text`                | YES  | `NULL`                           |                              |
| `created_at`        | `datetime`            | NO   | `current_timestamp()`            |                              |
| `updated_at`        | `datetime`            | NO   | `current_timestamp()`            |                              |
| `code_editeur`      | `varchar(12)`         | YES  | ``                               | UNIQUE; GENERATED            |

###### Diagram

<img src="Diagrams/artefact - editeurs_All.png" alt="editeurs" style="zoom:90%;" />

###### Extrait de données

| **editeurs**   |                        |             |                                  |                                                              |                                                              |                          |                          |                  |
| -------------- | ---------------------- | ----------- | -------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------ | ------------------------ | ---------------- |
| **id_editeur** | **nom_editeur**        | **id_pays** | **site_web**                     | **notes_editeur_rtf**                                        | **notes_editeur_txt**                                        | **created_at**           | **updated_at**           | **code_editeur** |
| 8              | Éditions  Flammarion   | 7           | https://editions.flammarion.com/ |                                                              | Éditions Flammarion  82, rue Saint-Lazare  CS 10124  75009 Paris.  Standard : 01 40 51  31 00  accueil.flammarion@flammarion.fr     Généraliste     Une maison d'édition  majeure qui publie une variété de genres littéraires (fiction, non-fiction,  poésie, etc.).     Soumettre son  manuscrit : en ligne, par l’intermédiaire du formulaire de contact      https://editions.flammarion.com/envoyer-un-manuscrit/ | 2026-03-10  16:36:49.000 | 2026-03-19  17:03:23.000 | E000008          |
| 9              | Éditions  Albin Michel | 7           | https://www.albin-michel.fr/     |                                                              | Éditions Albin Michel  – 22 rue Huyghens – 75014 Paris        Généraliste       Publient une gamme diversifiée de livres, notamment de la fiction, de la  non-fiction, des thrillers et des romans à succès.     Pour soumettre son  manuscrit : envoi postal uniquement     Plus d’informations : https://www.albin-michel.fr/deposer-un-manuscrit | 2026-03-10  16:38:10.000 | 2026-03-19  17:03:23.000 | E000009          |
| 12             | Test  Editions         | 5           | https://www.hhaau.be             | {\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang2060{\fonttbl{\f0\fnil\fcharset0  Segoe UI;}{\f1\fnil Segoe UI;}{\f2\fnil\fcharset2 Symbol;}}  {\*\generator  Riched20 10.0.26100}\viewkind4\uc1   \pard\b\f0\fs18  C\rquote est align\'e9 avec tes r\'e8gles de factorisation\b0 , de  s\'e9paration stricte SQL / m\'e9tier / UI, et avec la r\'e8gle officielle du  \i stockage RTF \i0 + texte miroir pour la recherche. \par     \pard{\pntext\f2\'B7\tab}{\*\pn\pnlvlblt\pnf2\pnindent0{\pntxtb\'B7}}Mon  avis d\rquote architecture est celui-ci : Tr\'e8s bon endroit\f1\par     \pard\par  \f0\tab Mon avis  c'est pas mal.\par  \ul\b\i Encore une  bonne id\'e9e\par  \b0\i0 toujours  beau\ulnone\f1\par  } | C’est aligné avec tes  règles de factorisation, de séparation stricte SQL / métier / UI, et avec la  règle officielle du stockage RTF + texte miroir pour la recherche.   Mon avis  d’architecture est celui-ci : Très bon endroit     Mon avis c'est pas  mal.  Encore une bonne idée  toujours beau | 2026-03-19  18:27:04.000 | 2026-03-20  12:20:19.000 | E000012          |
| 13             | Fre  Editions          | 4           | qds                              | {\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang2060{\fonttbl{\f0\fnil\fcharset0  Segoe UI;}{\f1\fnil Segoe UI;}}  {\*\generator  Riched20 10.0.26100}\viewkind4\uc1   \pard\f0\fs18 Tout  est bon chez banania\f1\par  } | Tout est bon chez  banania                                   |                          |                          |                  |

#### 	🥅`tags`

Table référentielle des tags. Contient des champs dédiés à la gestion du type de tag (genre, thème, étiquette etc..), de sa source (Artefact, Calibre, autre), de sa couleur d’affichage etc.. 
| Colonne       | Type                                        | Null | Default                      | Clé/Contrainte    |
| ------------- | ------------------------------------------- | ---- | ---------------------------- | ----------------- |
| `id_tag`      | `bigint(20) unsigned`                       | NO   | `nextval(artefact.seq_tags)` | PK                |
| `libelle_tag` | `varchar(180)`                              | NO   | ``                           | UNIQUE; INDEX     |
| `type_tag`    | `enum('Genre','Etiquette','Theme','Autre')` | NO   | `'Etiquette'`                | UNIQUE; INDEX     |
| `source_tag`  | `varchar(60)`                               | NO   | `'Artefact'`                 | UNIQUE; INDEX     |
| `couleur_tag` | `varchar(20)`                               | YES  | `NULL`                       |                   |
| `mapping_tag` | `varchar(600)`                              | YES  | `NULL`                       |                   |
| `is_actif`    | `tinyint(1)`                                | NO   | `1`                          |                   |
| `created_at`  | `datetime`                                  | NO   | `current_timestamp()`        |                   |
| `updated_at`  | `datetime`                                  | NO   | `current_timestamp()`        |                   |
| `code_tag`    | `varchar(12)`                               | YES  | ``                           | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - tags_All.png" alt="tags" style="zoom:90%;" />

###### Extrait de données

> [!WARNING]
>
> 

#### 	🥅`langues`

table référentielle permettant la gestion des langues. artefact - Contient des champs dédiés à la gestion du nom de la langue, de son abréviation, de ses codes ISO etc..
| Colonne       | Type                                        | Null | Default                      | Clé/Contrainte    |
| ------------- | ------------------------------------------- | ---- | ---------------------------- | ----------------- |
| `id_tag`      | `bigint(20) unsigned`                       | NO   | `nextval(artefact.seq_tags)` | PK                |
| `libelle_tag` | `varchar(180)`                              | NO   | ``                           | UNIQUE; INDEX     |
| `type_tag`    | `enum('Genre','Etiquette','Theme','Autre')` | NO   | `'Etiquette'`                | UNIQUE; INDEX     |
| `source_tag`  | `varchar(60)`                               | NO   | `'Artefact'`                 | UNIQUE; INDEX     |
| `couleur_tag` | `varchar(20)`                               | YES  | `NULL`                       |                   |
| `mapping_tag` | `varchar(600)`                              | YES  | `NULL`                       |                   |
| `is_actif`    | `tinyint(1)`                                | NO   | `1`                          |                   |
| `created_at`  | `datetime`                                  | NO   | `current_timestamp()`        |                   |
| `updated_at`  | `datetime`                                  | NO   | `current_timestamp()`        |                   |
| `code_tag`    | `varchar(12)`                               | YES  | ``                           | UNIQUE; GENERATED |

###### Diagram de la table

<img src="Diagrams/artefact - langues_All.png" alt="langues" style="zoom:80%;" /> 

###### Extrait de données

| **langues**    |                  |              |              |                          |                          |                 |
| -------------- | ---------------- | ------------ | ------------ | ------------------------ | ------------------------ | --------------- |
| **nom_langue** | **abrev_langue** | **iso639_1** | **iso639_2** | **created_at**           | **updated_at**           | **code_langue** |
| Français       | FR               | fr           | fra          | 2026-03-02  17:38:54.000 | 2026-03-02  17:38:54.000 | L000001         |
| Anglais        | EN               | en           | eng          | 2026-03-02  18:23:19.000 | 2026-03-02  18:52:05.000 | L000003         |
| Italien        | IT               | it           | ita          | 2026-03-03  13:13:00.000 | 2026-03-03  13:13:00.000 | L000005         |

#### 	🥅`pays`
Table référentielles permettant la gestion des pays. Contient des champs dédiés à la gestion du nom du pays, de son code ISO2, ISO3 etc.. 
| Colonne      | Type                  | Null | Default                      | Clé/Contrainte    |
| ------------ | --------------------- | ---- | ---------------------------- | ----------------- |
| `id_pays`    | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_pays)` | PK                |
| `nom_pays`   | `varchar(150)`        | NO   | ``                           | UNIQUE            |
| `iso2`       | `char(2)`             | YES  | `NULL`                       | INDEX             |
| `iso3`       | `char(3)`             | YES  | `NULL`                       | INDEX             |
| `created_at` | `datetime`            | NO   | `current_timestamp()`        |                   |
| `updated_at` | `datetime`            | NO   | `current_timestamp()`        |                   |
| `code_pays`  | `varchar(12)`         | YES  | ``                           | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - pays_All.png" alt="pays" style="zoom:80%;" />

###### Extrait de données

| **pays**     |          |          |                          |                          |               |
| ------------ | -------- | -------- | ------------------------ | ------------------------ | ------------- |
| **nom_pays** | **iso2** | **iso3** | **created_at**           | **updated_at**           | **code_pays** |
| Royaume-Uni  | GB       | GBR      | 2026-03-03  16:50:32.000 | 2026-03-03  16:50:32.000 | P000004       |
| Belgique     | BE       | BEL      | 2026-03-03  16:50:32.000 | 2026-03-03  16:50:32.000 | P000005       |
| Italie       | IT       | ITA      | 2026-03-03  17:24:43.000 | 2026-03-03  17:24:43.000 | P000006       |
| France       | FR       | FRA      | 2026-03-03  17:42:33.000 | 2026-03-03  17:42:33.000 | P000007       |

#### 	🥅`contacts`
Table référentielle des contacts, pour la gestion des prêts, échanges, recommandations etc..

| Colonne           | Type                  | Null | Default                          | Clé/Contrainte    |
| ----------------- | --------------------- | ---- | -------------------------------- | ----------------- |
| `id_contact`      | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_contacts)` | PK                |
| `nom_contact`     | `varchar(200)`        | NO   | ``                               | UNIQUE            |
| `email_perso`     | `varchar(254)`        | YES  | `NULL`                           | INDEX             |
| `adresse_liseuse` | `varchar(254)`        | YES  | `NULL`                           |                   |
| `type_liseuse`    | `varchar(100)`        | YES  | `NULL`                           |                   |
| `created_at`      | `datetime`            | NO   | `current_timestamp()`            |                   |
| `updated_at`      | `datetime`            | NO   | `current_timestamp()`            |                   |
| `code_contact`    | `varchar(12)`         | YES  | ``                               | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - contacts_All.png" alt="contact" style="zoom:80%;" />

###### Extrait de données

| **contacts**   |                 |                     |                             |                  |                          |                          |                  |
| -------------- | --------------- | ------------------- | --------------------------- | ---------------- | ------------------------ | ------------------------ | ---------------- |
| **id_contact** | **nom_contact** | **email_perso**     | **adresse_liseuse**         | **type_liseuse** | **created_at**           | **updated_at**           | **code_contact** |
| 1              | Pearl           | pearlnduy@gmail.com | pearlnduy_hd6nrx@kindle.com | Kindle           | 2026-03-09  10:20:01.000 | 2026-03-09  10:20:01.000 | C000001          |

#### 	🥅`prixLit`

Table référentielle des prix littéraires. Contient des champs dédiés à la gestion du nom du prix, de sa description, de ses notes diverses etc..

| Colonne               | Type                  | Null | Default                         | Clé/Contrainte    |
| --------------------- | --------------------- | ---- | ------------------------------- | ----------------- |
| `id_prixLit`          | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_prixlit)` | PK                |
| `nom_prixLit`         | `varchar(200)`        | NO   | ``                              | UNIQUE            |
| `description_prixLit` | `varchar(200)`        | YES  | `NULL`                          |                   |
| `Notes_PrixLit_txt`   | `text`                | YES  | `NULL`                          |                   |
| `Notes_PrixLit_rtf`   | `text`                | YES  | `NULL`                          |                   |
| `is_actif`            | `tinyint(1)`          | NO   | `1`                             |                   |
| `created_at`          | `datetime`            | NO   | `current_timestamp()`           |                   |
| `updated_at`          | `datetime`            | NO   | `current_timestamp()`           |                   |
| `code_prixLit`        | `varchar(12)`         | YES  | ``                              | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - prixlit_All.png" alt="prixlit" style="zoom:80%;" />

###### Extrait de données

> [!WARNING]
>
> 
>

#### 	🥅`formatFile`
Table référentielle des formats de fichiers (ePub, AZW3, PDF, etc..). Contient des champs dédiés à la gestion du nom du format, de son extension, de son mime type, de son ordre d’affichage etc..

| Colonne           | Type                  | Null | Default                            | Clé/Contrainte    |
| ----------------- | --------------------- | ---- | ---------------------------------- | ----------------- |
| `id_formatFile`   | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_formatfile)` | PK                |
| `nom_format`      | `varchar(40)`         | NO   | ``                                 | UNIQUE            |
| `extension`       | `varchar(10)`         | YES  | `NULL`                             | INDEX             |
| `mime_type`       | `varchar(100)`        | YES  | `NULL`                             |                   |
| `ordre_affichage` | `int(11)`             | NO   | `1`                                |                   |
| `is_actif`        | `tinyint(1)`          | NO   | `1`                                |                   |
| `created_at`      | `datetime`            | NO   | `current_timestamp()`              |                   |
| `updated_at`      | `datetime`            | NO   | `current_timestamp()`              |                   |
| `code_formatFile` | `varchar(12)`         | YES  | ``                                 | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - formatfile_All.png" alt="formatfile" style="zoom:80%;" />

###### Extrait de données

| **formatfile**    |                |               |                                    |                     |              |                          |                          |                     |
| ----------------- | -------------- | ------------- | ---------------------------------- | ------------------- | ------------ | ------------------------ | ------------------------ | ------------------- |
| **id_formatFile** | **nom_format** | **extension** | **mime_type**                      | **ordre_affichage** | **is_actif** | **created_at**           | **updated_at**           | **code_formatFile** |
| 1                 | EPUB           | epub          | application/epub+zip               | 1                   | 1            | 2026-02-14  18:17:18.000 | 2026-02-14  18:17:18.000 | F000001             |
| 2                 | AZW3           | azw3          | application/vnd.amazon.mobi8-ebook | 2                   | 1            | 2026-02-14  18:17:18.000 | 2026-02-14  18:17:18.000 | F000002             |
| 3                 | PDF            | pdf           | application/pdf                    | 3                   | 1            | 2026-02-14  18:17:18.000 | 2026-02-14  18:17:18.000 | F000003             |
| 4                 | CBR            | cbr           | application/x-cbr                  | 4                   | 1            | 2026-02-14  18:17:18.000 | 2026-02-14  18:17:18.000 | F000004             |
| 5                 | CBZ            | cbz           | application/vnd.comicbook+zip      | 5                   | 1            | 2026-02-14  18:17:18.000 | 2026-02-14  18:17:18.000 | F000005             |
| 6                 | MOBI           | mobi          | application/x-mobipocket-ebook     | 6                   | 1            | 2026-02-14  18:17:18.000 | 2026-02-14  18:17:18.000 | F000006             |

#### 	🥅`impression`
Table référentielle des impressions (Avant lecture, ce qui peut aller dans la PAL). Contient des champs dédiés à la gestion du nom de l’impression, de sa description, de ses notes diverses etc..

| Colonne                  | Type                  | Null | Default                            | Clé/Contrainte    |
| ------------------------ | --------------------- | ---- | ---------------------------------- | ----------------- |
| `id_impression`          | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_impression)` | PK                |
| `code_impression`        | `varchar(12)`         | YES  | ``                                 | UNIQUE; GENERATED |
| `nom_impression`         | `varchar(120)`        | NO   | ``                                 | UNIQUE            |
| `description_impression` | `varchar(400)`        | YES  | `NULL`                             |                   |
| `note_rtf`               | `mediumtext`          | YES  | `NULL`                             |                   |
| `note_txt`               | `text`                | YES  | `NULL`                             |                   |
| `envie_Cal`              | `varchar(10)`         | YES  | `NULL`                             |                   |
| `is_actif`               | `tinyint(1)`          | NO   | `1`                                |                   |
| `created_at`             | `datetime`            | NO   | `current_timestamp()`              |                   |
| `updated_at`             | `datetime`            | NO   | `current_timestamp()`              |                   |

###### Diagram

<img src="Diagrams/artefact - impression_All.png" alt="impression" style="zoom:80%;" />

###### Extrait de données

| **impression**    |                     |                    |                                 |                                                              |                                                              |               |              |                          |                          |
| ----------------- | ------------------- | ------------------ | ------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------- | ------------ | ------------------------ | ------------------------ |
| **id_impression** | **code_impression** | **nom_impression** | **description_impression**      | **note_rtf**                                                 | **note_txt**                                                 | **envie_Cal** | **is_actif** | **created_at**           | **updated_at**           |
| 1                 | I000001             | DIRECT_PAL         | Direct  dans ma PAL             |                                                              |                                                              | A             | 1            | 2026-03-15  17:09:15.000 | 2026-03-15  17:09:15.000 |
| 2                 | I000002             | PAL_ATTENTE        | PAL  en attente                 |                                                              |                                                              | B             | 1            | 2026-03-15  17:09:15.000 | 2026-03-15  17:09:15.000 |
| 3                 | I000003             | AIR_PAS_MAL        | Ça  a l'air pas mal             |                                                              |                                                              | C             | 1            | 2026-03-15  17:09:15.000 | 2026-03-15  17:09:15.000 |
| 4                 | I000004             | INTERESSANT        | Intéressant                     |                                                              |                                                              | D             | 1            | 2026-03-15  17:09:15.000 | 2026-03-15  17:09:15.000 |
| 5                 | I000005             | SANS_PLUS          | Sans  plus                      |                                                              | sans  plus = pourrait avoir quelque interêt, sans doute      | E             | 1            | 2026-03-15  17:09:15.000 | 2026-03-20  12:56:22.000 |
| 6                 | I000006             | MAIS_POURQUOI      | Mais  pourquoi ai-je ce livre ? | {\rtf1\ansi\deff0\nouicompat{\fonttbl{\f0\fnil\fcharset0  Segoe UI;}{\f1\fnil Segoe UI;}{\f2\fnil\fcharset2 Symbol;}}  {\*\generator  Riched20 10.0.26100}\viewkind4\uc1   \pard\b\f0\fs18\lang2060  oui \b0\f1 vraiment je me demande ce qu'il fait l\f0\'e0 :)\par  \par  \i Il devrait peut  \'eatre sortir? \par  \par  \ul\i0 Qu'en  penses-tu ?\par     \pard{\pntext\f2\'B7\tab}{\*\pn\pnlvlblt\pnf2\pnindent0{\pntxtb\'B7}}\ulnone  travers\par  {\pntext\f2\'B7\tab}ballon\par  {\pntext\f2\'B7\tab}rutabaga\par  } | oui  vraiment je me demande ce qu'il fait là :)     Il  devrait peut être sortir?      Qu'en  penses-tu ?  travers  ballon  rutabaga | F             | 1            | 2026-03-15  17:09:15.000 | 2026-03-20  15:24:11.000 |

#### 	🥅`recommandation`

Table référentielle des recommandations, pour la gestion des recommandations de livres, d’auteurs, de séries etc... Contient des champs dédiés à la gestion de l’origine de la recommandation (contact, réseau social, autre), de sa date, de son commentaire etc..

| Colonne                  | Type                  | Null | Default                            | Clé/Contrainte    |
| ------------------------ | --------------------- | ---- | ---------------------------------- | ----------------- |
| `id_impression`          | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_impression)` | PK                |
| `code_impression`        | `varchar(12)`         | YES  | ``                                 | UNIQUE; GENERATED |
| `nom_impression`         | `varchar(120)`        | NO   | ``                                 | UNIQUE            |
| `description_impression` | `varchar(400)`        | YES  | `NULL`                             |                   |
| `note_rtf`               | `mediumtext`          | YES  | `NULL`                             |                   |
| `note_txt`               | `text`                | YES  | `NULL`                             |                   |
| `envie_Cal`              | `varchar(10)`         | YES  | `NULL`                             |                   |
| `is_actif`               | `tinyint(1)`          | NO   | `1`                                |                   |
| `created_at`             | `datetime`            | NO   | `current_timestamp()`              |                   |
| `updated_at`             | `datetime`            | NO   | `current_timestamp()`              |                   |

###### Diagram

<img src="Diagrams/artefact - recommandations_All.png" alt="recommandations" style="zoom:80%;" />

###### Extrait de données

| **recommandations**   |                         |                               |                   |                     |                                          |                         |                     |                                    |              |                          |                          |
| --------------------- | ----------------------- | ----------------------------- | ----------------- | ------------------- | ---------------------------------------- | ----------------------- | ------------------- | ---------------------------------- | ------------ | ------------------------ | ------------------------ |
| **id_recommandation** | **code_recommandation** | **id_origine_recommandation** | **source_nom**    | **source_login**    | **source_url**                           | **date_recommandation** | **commentaire_rtf** | **commentaire_txt**                | **is_actif** | **created_at**           | **updated_at**           |
| 1                     | R000001                 | 1                             | Camille  Reads    | @camille_reads      | https://tiktok.com/@camille_reads        | 2026-03-15              |                     | Vu  passer 3 fois, hype énorme     | 1            | 2026-03-18  15:55:43.000 | 2026-03-20  16:34:40.000 |
| 2                     | R000002                 | 1                             | BookAddictFR      | @bookaddictfr       | https://tiktok.com/@bookaddictfr         | 2026-03-16              |                     | Semble  très addictif              | 1            | 2026-03-18  15:55:43.000 | 2026-03-20  16:34:40.000 |
| 3                     | R000003                 | 2                             | LecturesNocturnes | @lectures_nocturnes | https://instagram.com/lectures_nocturnes | 2026-03-14              |                     | Très  belles photos + avis positif | 1            | 2026-03-18  15:55:43.000 | 2026-03-20  16:34:40.000 |
| 4                     | R000004                 | 2                             | BiblioPassion     | @bibliopassion      | https://instagram.com/bibliopassion      |                         |                     | Recommandé  en story               | 1            | 2026-03-18  15:55:43.000 | 2026-03-20  16:34:40.000 |

### 4️⃣ Tables Sous-référentielles

#### 	🥅`prixLit_Annee`
Table sous-référentielle des années d’attribution des prix littéraires, avec la possibilité de faire le lien avec une catégorie (ex: Prix Goncourt 2020, Prix Goncourt 2021, etc..). Contient des champs dédiés à la gestion de l’année d’attribution du prix, de sa catégorie, de ses notes diverses etc..

| Colonne                | Type                  | Null | Default                                                      | Clé/Contrainte    |
| ---------------------- | --------------------- | ---- | ------------------------------------------------------------ | ----------------- |
| `id_prixLit_Annee`     | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_prixlit_annee)`                        | PK                |
| `id_prixlit_categorie` | `bigint(20) unsigned` | NO   | `` | UNIQUE; INDEX; FK → `prixlit_categorie`.`id_prixlit_categorie` |                   |
| `annee`                | `smallint(6)`         | NO   | ``                                                           | UNIQUE; INDEX     |
| `created_at`           | `datetime`            | NO   | `current_timestamp()`                                        |                   |
| `updated_at`           | `datetime`            | NO   | `current_timestamp()`                                        |                   |
| `code_prixLit_Annee`   | `varchar(12)`         | YES  | ``                                                           | UNIQUE; GENERATED |

###### Diagramme de la table

<img src="Diagrams/artefact - prixLit_Annee_All.png" alt="prixLit_Annee" style="zoom:80%;" />

###### Extrait de données

| `prixlit_annee`  |                      |       |                     |                     |                    |
| ---------------- | -------------------- | ----- | ------------------- | ------------------- | ------------------ |
| id_prixLit_Annee | id_prixlit_categorie | annee | created_at          | updated_at          | code_prixLit_Annee |
| 1                | 1                    | 1 946 | 2026-03-28 13:17:04 | 2026-03-28 13:17:04 | RA000001           |
| 2                | 15                   | 2 020 | 2026-03-28 16:29:20 | 2026-03-28 16:29:20 | RA000002           |
| 3                | 2                    | 2 015 | 2026-03-28 16:30:09 | 2026-03-28 16:30:09 | RA000003           |
| 5                | 17                   | 2 027 | 2026-05-27 17:58:52 | 2026-05-27 18:00:34 | RA000005           |

#### 	🥅`prixLit_categorie`
Table sous-référentielle des catégories de prix littéraires (ex: Prix Goncourt, Prix Femina, etc..), avec la possibilité de faire le lien avec les années d’attribution (ex: Prix Goncourt 2020, Prix Goncourt 2021, etc..). Contient des champs dédiés à la gestion du nom de la catégorie, de sa description, de son ordre d’affichage, de ses notes diverses etc..

| Colonne                  | Type                  | Null | Default                                         | Clé/Contrainte    |
| ------------------------ | --------------------- | ---- | ----------------------------------------------- | ----------------- |
| `id_prixlit_categorie`   | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_prixlit_categorie)`       | PK; UNIQUE        |
| `id_prixLit`             | `bigint(20) unsigned` | NO   | `` | UNIQUE; INDEX; FK → `prixlit`.`id_prixLit` |                   |
| `libelle_categorie`      | `varchar(200)`        | NO   | ``                                              | UNIQUE            |
| `description_categorie`  | `varchar(200)`        | YES  | `NULL`                                          |                   |
| `ordre_affichage`        | `int(11)`             | NO   | `0`                                             | INDEX             |
| `is_actif`               | `tinyint(1)`          | NO   | `1`                                             |                   |
| `created_at`             | `datetime`            | NO   | `current_timestamp()`                           |                   |
| `updated_at`             | `datetime`            | NO   | `current_timestamp()`                           |                   |
| `code_prixlit_categorie` | `varchar(12)`         | YES  | ``                                              | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - prixLit_categorie_All.png" alt="prixLit_categorie" style="zoom:80%;" />

###### Extrait de données

| `prixlit_categorie`  |            |                                  |                       |                 |          |                     |                     |                        |
| -------------------- | ---------- | -------------------------------- | --------------------- | --------------- | -------- | ------------------- | ------------------- | ---------------------- |
| id_prixlit_categorie | id_prixLit | libelle_categorie                | description_categorie | ordre_affichage | is_actif | created_at          | updated_at          | code_prixlit_categorie |
| 1                    | 1          | Lauréat                          |                       | 0               | 1        | 2026-03-28 13:16:43 | 2026-03-28 13:16:43 | RC000001               |
| 2                    | 2          | Meilleur roman francophone       |                       | 1               | 1        | 2026-03-28 15:00:25 | 2026-03-28 15:00:25 | RC000002               |
| 3                    | 2          | Meilleur roman étranger          |                       | 2               | 1        | 2026-03-28 15:00:41 | 2026-03-28 15:00:41 | RC000003               |
| 4                    | 2          | Meilleur roman                   | Avant 2005            | 3               | 1        | 2026-03-28 15:01:20 | 2026-03-28 15:01:20 | RC000004               |
| 5                    | 2          | Meilleure œuvre pour la jeunesse |                       | 4               | 1        | 2026-03-28 15:01:45 | 2026-03-28 15:01:45 | RC000005               |
| 6                    | 2          | Meilleure nouvelle               |                       | 5               | 1        | 2026-03-28 15:02:05 | 2026-03-28 15:02:05 | RC000006               |
| 7                    | 2          | Meilleure illustration           |                       | 6               | 1        | 2026-03-28 15:02:33 | 2026-03-28 15:02:33 | RC000007               |
| 8                    | 2          | Meilleure bande-dessinée         |                       | 7               | 1        | 2026-03-28 15:02:51 | 2026-03-28 15:02:51 | RC000008               |
| 9                    | 2          | Prix spécial du jury             |                       | 8               | 1        | 2026-03-28 15:03:07 | 2026-03-28 15:03:07 | RC000009               |

#### 	🥅`series_format`
Table sous-référentielle des formats de séries (Oneshot, Duologie, Trilogie, etc..). Contient des champs dédiés à la gestion du nom du format, de son nombre de tomes de référence, de son ordre d’affichage etc..

| Colonne              | Type                  | Null | Default                               | Clé/Contrainte    |
| -------------------- | --------------------- | ---- | ------------------------------------- | ----------------- |
| `id_series_format`   | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_series_format)` | PK                |
| `libelle_format`     | `varchar(80)`         | NO   | ``                                    | UNIQUE            |
| `nb_tomes_ref`       | `tinyint(4)`          | YES  | `NULL`                                |                   |
| `ordre_affichage`    | `int(11)`             | NO   | `0`                                   | INDEX             |
| `is_actif`           | `tinyint(1)`          | NO   | `1`                                   |                   |
| `created_at`         | `datetime`            | NO   | `current_timestamp()`                 |                   |
| `updated_at`         | `datetime`            | NO   | `current_timestamp()`                 |                   |
| `code_series_format` | `varchar(12)`         | YES  | ``                                    | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - series_format_All.png" alt="series_format" style="zoom:80%;" />

###### Extrait de données

| `series_format`  |                |              |                 |          |                     |                     |                    |
| ---------------- | -------------- | ------------ | --------------- | -------- | ------------------- | ------------------- | ------------------ |
| id_series_format | libelle_format | nb_tomes_ref | ordre_affichage | is_actif | created_at          | updated_at          | code_series_format |
| 1                | Oneshot        | 1            | 1               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000001           |
| 2                | Duologie       | 2            | 2               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000002           |
| 3                | Trilogie       | 3            | 3               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000003           |
| 4                | Tétralogie     | 4            | 4               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000004           |
| 5                | Pentalogie     | 5            | 5               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000005           |
| 6                | Série de 6     | 6            | 6               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000006           |
| 7                | Série de 7     | 7            | 7               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000007           |
| 8                | Série de 8     | 8            | 8               | 1        | 2026-02-26 19:09:09 | 2026-02-26 19:09:09 | SF000008           |

#### 	🥅`series_statut`
Table sous-référentielle des statuts de séries (En cours, Terminée, Abandonnée, etc..). Contient des champs dédiés à la gestion du nom du statut, de son caractère final ou non, de son ordre d’affichage etc..

| Colonne              | Type                  | Null | Default                               | Clé/Contrainte    |
| -------------------- | --------------------- | ---- | ------------------------------------- | ----------------- |
| `id_series_statut`   | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_series_statut)` | PK                |
| `libelle_statut`     | `varchar(80)`         | NO   | ``                                    | UNIQUE            |
| `est_final`          | `tinyint(1)`          | NO   | `0`                                   |                   |
| `ordre_affichage`    | `int(11)`             | NO   | `0`                                   | INDEX             |
| `is_actif`           | `tinyint(1)`          | NO   | `1`                                   |                   |
| `created_at`         | `datetime`            | NO   | `current_timestamp()`                 |                   |
| `updated_at`         | `datetime`            | NO   | `current_timestamp()`                 |                   |
| `code_series_statut` | `varchar(12)`         | YES  | ``                                    | UNIQUE; GENERATED |

###### Diagram

<img src="Diagrams/artefact - series_statut_All.png" alt="series_statut" style="zoom:80%;" />

###### Extrait de données

#### 	🥅`ref_enum_type`
Table sous-référentielle, attachée à ref_enum, listant les différentes catégories (type) d'énumérations. Contient des champs dédiés à la gestion du nom du type d'énumération, de son code, de son ordre d’affichage etc..

| Colonne           | Type                  | Null | Default                               | Clé/Contrainte    |
| ----------------- | --------------------- | ---- | ------------------------------------- | ----------------- |
| `id_enum_type`    | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_ref_enum_type)` | PK                |
| `code_enum_type`  | `varchar(12)`         | YES  | ``                                    | UNIQUE; GENERATED |
| `code_type`       | `varchar(60)`         | NO   | ``                                    | UNIQUE            |
| `libelle_type`    | `varchar(120)`        | NO   | ``                                    |                   |
| `ordre_affichage` | `int(11)`             | NO   | `0`                                   |                   |
| `is_actif`        | `tinyint(1)`          | NO   | `1`                                   |                   |
| `created_at`      | `datetime`            | NO   | `current_timestamp()`                 |                   |
| `updated_at`      | `datetime`            | NO   | `current_timestamp()`                 |                   |

###### Diagram

<img src="Diagrams/artefact - ref_enum_type_All.png" alt="ref_enum_type" style="zoom:80%;" />

###### Extrait de données

| **ref_enum_type** |                    |                           |                           |                     |              |                          |                          |
| ----------------- | ------------------ | ------------------------- | ------------------------- | ------------------- | ------------ | ------------------------ | ------------------------ |
| **id_enum_type**  | **code_enum_type** | **code_type**             | **libelle_type**          | **ordre_affichage** | **is_actif** | **created_at**           | **updated_at**           |
| 1                 | ET000001           | ROLE_AUTEUR_LIVRE         | role_auteur_livre         | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |
| 2                 | ET000002           | SOURCE_IMPORT             | source_import             | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |
| 3                 | ET000003           | STATUT_LECTURE            | statut_lecture            | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |
| 4                 | ET000004           | STATUT_STAGING            | statut_staging            | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |
| 5                 | ET000005           | SUPPORT_LECTURE           | support_lecture           | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |
| 6                 | ET000006           | TYPE_ACQUISITION          | type_acquisition          | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |
| 7                 | ET000007           | TYPE_FICHIER              | type_fichier              | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |
| 8                 | ET000008           | TYPE_RELATION_AUTEUR_PAYS | type_relation_auteur_pays | 0                   | 1            | 2026-03-04  14:33:52.000 | 2026-03-04  14:33:52.000 |

#### 	🥅`ref_origine_recommandation`

Table sous-référentielle, attachée à recommandation, listant les différentes origines possibles pour une recommandation (ex : lecture personnelle, recommandation d’un ami, critique littéraire, etc..)

| Colonne                          | Type                  | Null | Default                                            | Clé/Contrainte    |
| -------------------------------- | --------------------- | ---- | -------------------------------------------------- | ----------------- |
| `id_origine_recommandation`      | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_ref_origine_recommandation)` | PK                |
| `code_origine_recommandation`    | `varchar(12)`         | YES  | ``                                                 | UNIQUE; GENERATED |
| `libelle_origine_recommandation` | `varchar(100)`        | NO   | ``                                                 |                   |
| `ordre_affichage`                | `int(11)`             | NO   | `0`                                                |                   |
| `is_actif`                       | `tinyint(1)`          | NO   | `1`                                                |                   |
| `created_at`                     | `datetime`            | NO   | `current_timestamp()`                              |                   |
| `updated_at`                     | `datetime`            | NO   | `current_timestamp()`                              |                   |

###### Diagram

<img src="Diagrams/artefact - ref_origine_recommandation_All.png" alt="ref_origine_recommandation" style="zoom:80%;" />

###### Extrait de données

| id_origine_recommandation | code_origine_recommandation | libelle_origine_recommandation | ordre_affichage | is_actif | created_at          | updated_at          |
| ------------------------- | --------------------------- | ------------------------------ | --------------- | -------- | ------------------- | ------------------- |
| 1                         | OR000001                    | BookTok                        | 1               | 1        | 2026-03-18 15:43:02 | 2026-05-27 10:44:38 |
| 2                         | OR000002                    | Instagram                      | 2               | 1        | 2026-03-18 15:43:02 | 2026-03-18 19:22:11 |
| 3                         | OR000003                    | Blog littéraire                | 3               | 1        | 2026-03-18 15:43:02 | 2026-03-18 15:43:02 |
| 4                         | OR000004                    | Conseil ami                    | 4               | 1        | 2026-03-18 15:43:02 | 2026-03-18 15:43:02 |
| 5                         | OR000005                    | Librairie                      | 5               | 1        | 2026-03-18 15:43:02 | 2026-03-18 15:43:02 |
| 6                         | OR000006                    | Podcast                        | 6               | 1        | 2026-03-18 15:43:02 | 2026-03-18 15:43:02 |
| 7                         | OR000007                    | Facebook                       | 7               | 1        | 2026-03-18 15:43:02 | 2026-03-18 15:43:02 |
| 8                         | OR000008                    | YouTube                        | 8               | 1        | 2026-03-18 15:43:02 | 2026-03-18 15:43:02 |
| 11                        | OR000011                    | Emission télévision            | 0               | 1        | 2026-05-27 10:42:02 | 2026-05-27 10:42:02 |

### 5️⃣ Tables de liaison (Many-to-Many ou Many-to-One)

#### a. `Livres` et  `livres_staging`

#### 	🥅`livres_auteurs`
Table de liaison entre les livres et les auteurs, permettant de gérer les relations many-to-many entre ces deux entités, ainsi que le rôle de l’auteur pour chaque livre (ex: auteur principal, co-auteur, illustrateur, etc..). Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `livres`, `auteurs` et `ref_enum` (pour le rôle de l’auteur).

| Colonne          | Type                  | Null | Default                                    | Clé/Contrainte |
| ---------------- | --------------------- | ---- | ------------------------------------------ | -------------- |
| `id_livre`       | `bigint(20) unsigned` | NO   | `` | PK; FK → `livres`.`id_livre`          |                |
| `id_auteur`      | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `auteurs`.`id_auteur` |                |
| `id_role_auteur` | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `ref_enum`.`id_enum`  |                |
| `created_at`     | `datetime`            | NO   | `current_timestamp()`                      |                |

###### Diagram

<img src="Diagrams/artefact - livres_auteurs_All.png" alt="livres_auteurs" style="zoom:80%;" />

###### Extrait de données



#### 	🥅`livres_staging_auteurs`
Table de liaison entre les livres en staging et les auteurs, permettant de gérer les relations many-to-many entre ces deux entités, ainsi que le rôle de l’auteur pour chaque livre en staging (ex: auteur principal, co-auteur, illustrateur, etc..). Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `livres_staging`, `auteurs` et `ref_enum` (pour le rôle de l’auteur).

| Colonne            | Type                  | Null | Default                                           | Clé/Contrainte |
| ------------------ | --------------------- | ---- | ------------------------------------------------- | -------------- |
| `id_livre_staging` | `bigint(20) unsigned` | NO   | `` | PK; FK → `livres_staging`.`id_livre_staging` |                |
| `id_auteur`        | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `auteurs`.`id_auteur`        |                |
| `id_role_auteur`   | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `ref_enum`.`id_enum`         |                |
| `created_at`       | `datetime`            | NO   | `current_timestamp()`                             |                |

###### Diagram

<img src="Diagrams/artefact - livres_staging_auteurs_All.png" alt="livres_staging_auteurs" style="zoom:80%;" />

###### Extrait de données



#### 	`🥅livres_tags`
Table de liaison entre les livres et les tags, permettant de gérer les relations many-to-many entre ces deux entités. Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `livres` et `tags`.

| Colonne      | Type                  | Null | Default                              | Clé/Contrainte |
| ------------ | --------------------- | ---- | ------------------------------------ | -------------- |
| `id_livre`   | `bigint(20) unsigned` | NO   | `` | PK; FK → `livres`.`id_livre`    |                |
| `id_tag`     | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `tags`.`id_tag` |                |
| `created_at` | `datetime`            | NO   | `current_timestamp()`                |                |

###### Diagram

<img src="Diagrams/artefact - livres_tags_All.png" alt="livres_tags" style="zoom:80%;" />

###### Extrait de données



#### 	🥅`livres_prixLit_annee`
Table de liaison entre les livres et les années d’attribution des prix littéraires, permettant de gérer les relations many-to-many entre ces deux entités. Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `livres` et `prixLit_Annee`.

| Colonne            | Type                  | Null | Default                                                 | Clé/Contrainte |
| ------------------ | --------------------- | ---- | ------------------------------------------------------- | -------------- |
| `id_livre`         | `bigint(20) unsigned` | NO   | `` | PK; FK → `livres`.`id_livre`                       |                |
| `id_prixLit_Annee` | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `prixlit_annee`.`id_prixLit_Annee` |                |
| `created_at`       | `datetime`            | NO   | `current_timestamp()`                                   |                |

###### Diagram

<img src="Diagrams/artefact - livres_prixlit_annee_All.png" alt="livres_prixlit_annee" style="zoom:80%;" />

###### Extrait de données



#### 	`🥅livres_contacts`
Table de liaison entre les livres et les contacts, permettant de gérer les relations many-to-many entre ces deux entités, ainsi que des informations complémentaires sur la nature du contact (date d’envoi du livre, commentaire sur le contact, etc..). Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `livres`, `contacts` et `livres_fichiers` (pour le fichier du livre envoyé au contact).

| Colonne            | Type                  | Null | Default                                      | Clé/Contrainte                                   |
| ------------------ | --------------------- | ---- | -------------------------------------------- | ------------------------------------------------ |
| `id_livre`         | `bigint(20) unsigned` | NO   | `` | PK; FK → `livres`.`id_livre`            |                                                  |
| `id_contact`       | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `contacts`.`id_contact` |                                                  |
| `id_livre_fichier` | `bigint(20) unsigned` | YES  | `NULL`                                       | INDEX; FK → `livres_fichiers`.`id_livre_fichier` |
| `date_envoi`       | `datetime`            | YES  | `NULL`                                       |                                                  |
| `commentaire`      | `varchar(255)`        | YES  | `NULL`                                       |                                                  |
| `created_at`       | `datetime`            | NO   | `current_timestamp()`                        |                                                  |

###### Diagram

<img src="Diagrams/artefact - livres_contacts_All.png" alt="livres_contacts" style="zoom:80%;" />

###### Extrait de données



#### 	`	🥅livres_recommandations` 
Table de liaison entre les livres et les recommandations, permettant de gérer les relations many-to-many entre ces deux entités. Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `livres` et `recommandations`.

| Colonne             | Type                  | Null | Default                                                    | Clé/Contrainte |
| ------------------- | --------------------- | ---- | ---------------------------------------------------------- | -------------- |
| `id_livre`          | `bigint(20) unsigned` | NO   | `` | PK; FK → `livres`.`id_livre`                          |                |
| `id_recommandation` | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `recommandations`.`id_recommandation` |                |
| `created_at`        | `datetime`            | NO   | `current_timestamp()`                                      |                |

###### Diagram

<img src="Diagrams/artefact - livres_recommandations_All.png" alt="livres_recommandations" style="zoom:80%;" />

###### Extrait de données



#### 	🥅`livres_staging_recommandations` 
Table de liaison entre les livres en staging et les recommandations, permettant de gérer les relations many-to-many entre ces deux entités. Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `livres_staging` et `recommandations`.

| Colonne             | Type                  | Null | Default                                                    | Clé/Contrainte |
| ------------------- | --------------------- | ---- | ---------------------------------------------------------- | -------------- |
| `id_livre_staging`  | `bigint(20) unsigned` | NO   | `` | PK; FK → `livres_staging`.`id_livre_staging`          |                |
| `id_recommandation` | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `recommandations`.`id_recommandation` |                |
| `created_at`        | `datetime`            | NO   | `current_timestamp()`                                      |                |

###### Diagram

<img src="Diagrams/artefact - livres_staging_recommandations_All.png" alt="livres_staging_recommandations" style="zoom:80%;" />

###### Extrait de données



#### b.  `Auteurs`

#### 	🥅`auteurs_pays`
Table de liaison entre les auteurs et les pays, permettant de gérer les relations many-to-many entre ces deux entités, ainsi que le type de relation entre l’auteur et le pays (ex: nationalité, lieu de résidence, etc..). Contient des champs dédiés à la gestion de la date de création de la relation, ainsi que des clés étrangères vers les tables `auteurs`, `pays` et `ref_enum` (pour le type de relation entre l’auteur et le pays).

| Colonne                 | Type                  | Null | Default                                    | Clé/Contrainte |
| ----------------------- | --------------------- | ---- | ------------------------------------------ | -------------- |
| `id_auteur`             | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `auteurs`.`id_auteur` |                |
| `id_pays`               | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `pays`.`id_pays`      |                |
| `id_type_relation_pays` | `bigint(20) unsigned` | NO   | `` | PK; INDEX; FK → `ref_enum`.`id_enum`  |                |
| `created_at`            | `datetime`            | NO   | `current_timestamp()`                      |                |

###### Diagram

<img src="Diagrams/artefact - auteurs_pays_All.png" alt="auteurs_pays" style="zoom:80%;" />

###### Extrait de données



### 5️⃣ Table générique 

#### `ref_enum`
Table générique qui remplace les ENUM SQL natifs. Permet de gérer de manière plus flexible et évolutive les différentes énumérations utilisées dans la base de données, en les associant à des types d'énumérations définis dans la table `ref_enum_type`. Contient des champs dédiés à la gestion du code et du libellé de chaque valeur d'énumération, de son ordre d’affichage, de son statut actif ou non, etc..

  - Utilisée pour par ex statut_lecture ou support_lecture ou role_auteur_livre etc..
| Colonne           | Type                  | Null | Default                                          | Clé/Contrainte    |
| ----------------- | --------------------- | ---- | ------------------------------------------------ | ----------------- |
| `id_enum`         | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_ref_enum)`                 | PK                |
| `code_enum`       | `varchar(12)`         | YES  | ``                                               | UNIQUE; GENERATED |
| `id_enum_type`    | `bigint(20) unsigned` | NO   | `` | UNIQUE; FK → `ref_enum_type`.`id_enum_type` |                   |
| `code_valeur`     | `varchar(40)`         | NO   | ``                                               | UNIQUE            |
| `libelle_valeur`  | `varchar(120)`        | NO   | ``                                               | INDEX             |
| `ordre_affichage` | `int(11)`             | NO   | `0`                                              |                   |
| `is_actif`        | `tinyint(1)`          | NO   | `1`                                              |                   |
| `created_at`      | `datetime`            | NO   | `current_timestamp()`                            |                   |
| `updated_at`      | `datetime`            | NO   | `current_timestamp()`                            |                   |

###### Diagram

<img src="Diagrams/artefact - ref_enum_All.png" alt="ref_enum" style="zoom:80%;" />

###### Extrait de données

| **ref_enum** |               |                  |                 |                    |                     |              |                          |                          |
| ------------ | ------------- | ---------------- | --------------- | ------------------ | ------------------- | ------------ | ------------------------ | ------------------------ |
| **id_enum**  | **code_enum** | **id_enum_type** | **code_valeur** | **libelle_valeur** | **ordre_affichage** | **is_actif** | **created_at**           | **updated_at**           |
| 30           | N000030       | 4                | A_COMPLETER     | À  compléter       | 2                   | 1            | 2026-02-15  17:57:20.000 | 2026-03-04  14:34:56.000 |
| 29           | N000029       | 4                | A_VALIDER       | À  valider         | 1                   | 1            | 2026-02-15  17:57:20.000 | 2026-03-04  14:34:56.000 |
| 9            | N000009       | 6                | ABONNEMENT      | Abonnement         | 3                   | 1            | 2026-02-13  14:23:47.000 | 2026-03-04  14:34:56.000 |
| 7            | N000007       | 6                | ACHAT           | Achat              | 1                   | 1            | 2026-02-13  14:23:47.000 | 2026-03-04  14:34:56.000 |
| 5            | N000005       | 5                | AUDIBLE         | Audible            | 2                   | 1            | 2026-02-13  14:23:47.000 | 2026-03-04  14:34:56.000 |
| 18           | N000018       | 1                | AUTEUR          | Auteur  principal  | 1                   | 1            | 2026-02-15  15:49:49.000 | 2026-03-04  14:34:56.000 |
| 28           | N000028       | 2                | AUTRE           | Autre  source      | 4                   | 1            | 2026-02-15  17:57:20.000 | 2026-03-04  14:34:56.000 |
| 25           | N000025       | 2                | CALIBRE         | Import  Calibre    | 1                   | 1            | 2026-02-15  17:57:20.000 | 2026-03-04  14:34:56.000 |
| 19           | N000019       | 1                | COAUTEUR        | Co-auteur          | 2                   | 1            | 2026-02-15  15:49:49.000 | 2026-03-04  14:34:56.000 |
| 12           | N000012       | 7                | COVER           | Couverture         | 2                   | 1            | 2026-02-13  14:23:47.000 | 2026-03-04  14:34:56.000 |
| 17           | N000017       | 8                | CULTURE         | Contexte  culturel | 5                   | 1            | 2026-02-14  19:00:25.000 | 2026-03-04  14:34:56.000 |

### 6️⃣ Tables paramètres

#### 	🥅`param_db`
Connexion base : IP serveur, nom DB, user etc... Tous les paramètres de connexion aux bases de données utilisées par Artefact (MariaDB, SQLite etc..).

| Colonne         | Type                  | Null | Default                          | Clé/Contrainte    |
| --------------- | --------------------- | ---- | -------------------------------- | ----------------- |
| `id_param_db`   | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_param_db)` | PK                |
| `code_param_db` | `varchar(12)`         | YES  | ``                               | UNIQUE; GENERATED |
| `env_code`      | `varchar(20)`         | NO   | `'LOCAL'`                        | UNIQUE            |
| `nom_connexion` | `varchar(60)`         | NO   | ``                               | UNIQUE            |
| `type_db`       | `varchar(20)`         | NO   | ``                               | INDEX             |
| `host`          | `varchar(255)`        | YES  | `NULL`                           |                   |
| `port`          | `int(11)`             | YES  | `NULL`                           |                   |
| `nom_base`      | `varchar(255)`        | YES  | `NULL`                           |                   |
| `user_name`     | `varchar(255)`        | YES  | `NULL`                           |                   |
| `password_hint` | `varchar(255)`        | YES  | `NULL`                           |                   |
| `password_enc`  | `blob`                | YES  | `NULL`                           |                   |
| `options_conn`  | `text`                | YES  | `NULL`                           |                   |
| `is_actif`      | `tinyint(1)`          | NO   | `1`                              | INDEX             |
| `created_at`    | `datetime`            | NO   | `current_timestamp()`            |                   |
| `updated_at`    | `datetime`            | NO   | `current_timestamp()`            |                   |

###### Diagram

<img src="Diagrams/artefact - param_db_All.png" alt="param_db" style="zoom:90%;" />

###### Extrait de données

| `param_db`  |               |          |                  |         |             |      |               |           |                                    |              |                                                     |          |                     |                     |
| ----------- | ------------- | -------- | ---------------- | ------- | ----------- | ---- | ------------- | --------- | ---------------------------------- | ------------ | --------------------------------------------------- | -------- | ------------------- | ------------------- |
| id_param_db | code_param_db | env_code | nom_connexion    | type_db | host        | port | nom_base      | user_name | password_hint                      | password_enc | options_conn                                        | is_actif | created_at          | updated_at          |
| 1           | DB000001      | LOCAL    | MariaDB_Artefact | MARIADB | xxx.xxx.xxx | xxxx | Artefact      | xxxx      | Password chiffré via appli (DPAPI) |              | SslMode=none                                        | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |
| 2           | DB000002      | LOCAL    | Calibre_SQLite   | SQLITE  |             |      | myMetadata.db |           |                                    |              | Chemin via Path_General + Path_DBCalibre + nom_base | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |

#### 	🥅`param_paths`
Tous les chemins : Path_General, Path_Data, Path_Data_LivBiblio etc. Tous les chemins utilisés par Artefact pour stocker ou accéder aux données (ex: chemin de la bibliothèque Calibre, chemin de stockage des données Artefact, etc..). Contient des champs dédiés à la gestion du code et de la clé de chaque chemin, de sa valeur, de son environnement d’application (ex: LOCAL, PROD, etc..), de sa description, etc..

| Colonne           | Type                  | Null | Default                             | Clé/Contrainte    |
| ----------------- | --------------------- | ---- | ----------------------------------- | ----------------- |
| `id_param_path`   | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_param_paths)` | PK                |
| `code_param_path` | `varchar(12)`         | YES  | ``                                  | UNIQUE; GENERATED |
| `env_code`        | `varchar(20)`         | NO   | `'LOCAL'`                           | UNIQUE            |
| `cle_path`        | `varchar(80)`         | NO   | ``                                  | UNIQUE            |
| `valeur_path`     | `varchar(1000)`       | NO   | ``                                  |                   |
| `description`     | `varchar(255)`        | YES  | `NULL`                              |                   |
| `is_actif`        | `tinyint(1)`          | NO   | `1`                                 | INDEX             |
| `created_at`      | `datetime`            | NO   | `current_timestamp()`               |                   |
| `updated_at`      | `datetime`            | NO   | `current_timestamp()`               |                   |

###### Diagram

<img src="Diagrams/artefact - param_paths_All.png" alt="param_paths" style="zoom:90%;" />

###### Extrait de données

| `param_paths` |                 |          |                     |                                   |                               |          |                     |                     |
| ------------- | --------------- | -------- | ------------------- | --------------------------------- | ----------------------------- | -------- | ------------------- | ------------------- |
| id_param_path | code_param_path | env_code | cle_path            | valeur_path                       | description                   | is_actif | created_at          | updated_at          |
| 1             | PA000001        | LOCAL    | Path_General        | C:\Users\Joelle\OneDrive\Artefact | Path général                  | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |
| 2             | PA000002        | LOCAL    | Path_DBCalibre      | BdeD\DBCalibre                    | Sous-dossier copie DB Calibre | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:13:48 |
| 3             | PA000003        | LOCAL    | Path_Biblio_Calibre | Z:\2 Ebookb                       | Bibliothèque Calibre          | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |
| 4             | PA000004        | LOCAL    | Path_Data           | Artefact                          | Sous-dossier data Artefact    | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |

#### 	🥅`param_api`
OpenAI_API_Key, GoogleBooks_API_Key, endpoints, timeouts, paramètres d’enrichissement. Tous les paramètres liés aux APIs utilisées par Artefact (ex: clés API, endpoints, timeouts, paramètres d’enrichissement, etc..). Contient des champs dédiés à la gestion du code et du libellé de chaque paramètre d’API, de sa valeur (ex: clé API chiffrée), de son environnement d’application (ex: LOCAL, PROD, etc..), de sa description, etc..

| Colonne           | Type                  | Null | Default                           | Clé/Contrainte    |
| ----------------- | --------------------- | ---- | --------------------------------- | ----------------- |
| `id_param_api`    | `bigint(20) unsigned` | NO   | `nextval(artefact.seq_param_api)` | PK                |
| `code_param_api`  | `varchar(12)`         | YES  | ``                                | UNIQUE; GENERATED |
| `env_code`        | `varchar(20)`         | NO   | `'LOCAL'`                         | UNIQUE            |
| `service_code`    | `varchar(40)`         | NO   | ``                                | UNIQUE            |
| `service_libelle` | `varchar(100)`        | YES  | `NULL`                            |                   |
| `base_url`        | `varchar(600)`        | YES  | `NULL`                            |                   |
| `api_key_enc`     | `blob`                | YES  | `NULL`                            |                   |
| `api_key_hint`    | `varchar(255)`        | YES  | `NULL`                            |                   |
| `options_json`    | `text`                | YES  | `NULL`                            |                   |
| `is_actif`        | `tinyint(1)`          | NO   | `1`                               | INDEX             |
| `created_at`      | `datetime`            | NO   | `current_timestamp()`             |                   |
| `updated_at`      | `datetime`            | NO   | `current_timestamp()`             |                   |

###### Diagram

<img src="Diagrams/artefact - param_api_All.png" alt="param_api" style="zoom:90%;" />

###### Extrait de données

| `param_api`  |                |          |              |                 |                            |             |                                                  |                                                |          |                     |                     |
| ------------ | -------------- | -------- | ------------ | --------------- | -------------------------- | ----------- | ------------------------------------------------ | ---------------------------------------------- | -------- | ------------------- | ------------------- |
| id_param_api | code_param_api | env_code | service_code | service_libelle | base_url                   | api_key_enc | api_key_hint                                     | options_json                                   | is_actif | created_at          | updated_at          |
| 1            | AP000001       | LOCAL    | OPENAI       | OpenAI          | https://api.openai.com     |             | Clé fournie localement (ne pas stocker en clair) | { "chat_completions": "/v1/chat/completions" } | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |
| 2            | AP000002       | LOCAL    | GOOGLE_BOOKS | Google Books    | https://www.googleapis.com |             | Clé fournie localement (ne pas stocker en clair) | { "volumes_endpoint": "/books/v1/volumes" }    | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |
| 3            | AP000003       | LOCAL    | CHATGPT_WEB  | ChatGPT Web     | https://chat.openai.com    |             |                                                  |                                                | 1        | 2026-02-16 18:10:22 | 2026-02-16 18:10:22 |

### 7️⃣  Tables Configuration

####  	🥅`meta_schema` 
Table de suivi de l’évolution du schéma de la base de données, permettant de garder une trace des différentes versions du schéma, des modifications apportées, des dates d’application des migrations, etc.. Contient des champs dédiés à la gestion de la version du schéma, de la date d’application de chaque migration, ainsi que des notes descriptives sur les modifications apportées.

| Colonne          | Type           | Null | Default | Clé/Contrainte |
| ---------------- | -------------- | ---- | ------- | -------------- |
| `id`             | `tinyint(4)`   | NO   | ``      | PK             |
| `schema_version` | `int(11)`      | NO   | ``      |                |
| `applied_at`     | `datetime`     | NO   | ``      |                |
| `notes`          | `varchar(255)` | YES  | `NULL`  |                |

###### Diagram

<img src="Diagrams/artefact - meta_schema_All.png" alt="meta_schema" style="zoom:90%;" />

###### Extrait de données

| `meta_schema` |                |                     |                         |
| ------------- | -------------- | ------------------- | ----------------------- |
| id            | schema_version | applied_at          | notes                   |
| 1             | 6              | 2026-03-20 16:45:07 | Ajout table meta_schema |

 ### 8️⃣  Tables Séquences
 Toutes les séquences utilisées pour les clés primaires auto-incrémentées des différentes tables de la base de données. Contient des champs dédiés à la gestion du nom de chaque séquence, de sa valeur actuelle, de son incrément, de sa valeur minimale et maximale, etc..

#### 	🥅`seq_auteurs`
Clé primaire auto-incrémentée de la table `auteurs`.

#### 	🥅`seq_contacts`
Clé primaire auto-incrémentée de la table `contacts`.

#### 	🥅`seq_editeurs`
Clé primaire auto-incrémentée de la table `editeurs`.

#### 	🥅`seq_formatFile`
Clé primaire auto-incrémentée de la table `formatFile`.

#### 	🥅`seq_impression`
Clé primaire auto-incrémentée de la table `impression`.

#### 	🥅`seq_langues`
Clé primaire auto-incrémentée de la table `langues`.

#### 	🥅`seq_livres`
Clé primaire auto-incrémentée de la table `livres`.

#### 	🥅`seq_livres_fichier`
Clé primaire auto-incrémentée de la table `livres_fichier`.

#### 	🥅`Seq_livres_staging`
Clé primaire auto-incrémentée de la table `livres_staging`.

#### 	🥅`seq_param_api`
Clé primaire auto-incrémentée de la table `param_api`.

#### 	🥅`seq_param_db`
Clé primaire auto-incrémentée de la table `param_db`.

#### 	🥅`seq_param_paths`
Clé primaire auto-incrémentée de la table `param_paths`.

#### 	🥅`seq_pays`
Clé primaire auto-incrémentée de la table `pays`.

#### 	🥅`seq_prixLit`
Clé primaire auto-incrémentée de la table `prixLit`.

#### 	🥅`seq_prixLit_Annee`
Clé primaire auto-incrémentée de la table `prixLit_Annee`.

#### 	🥅`seq_prixlit_categorie`
Clé primaire auto-incrémentée de la table `prixlit_categorie`.

#### 	`🥅seq_recommandations`
Clé primaire auto-incrémentée de la table `recommandations`.

#### 	🥅`seq_ref_enum`
Clé primaire auto-incrémentée de la table `ref_enum`.

#### 	🥅`seq_ref_enum_type`
Clé primaire auto-incrémentée de la table `ref_enum_type`.

#### 	🥅`seq_ref_origine_recommandation`
Clé primaire auto-incrémentée de la table `ref_origine_recommandation`.

#### 	`🥅seq_series`
Clé primaire auto-incrémentée de la table `series`.

#### 	🥅`seq_series_format`
Clé primaire auto-incrémentée de la table `series_format`.

#### 	`🥅seq_series_statut`
Clé primaire auto-incrémentée de la table `series_statut`.

#### 	🥅`seq_tags`
Clé primaire auto-incrémentée de la table `tags`.

## Views

#### **v_prixlit_recherche**
View de recherche sur les prix littéraires, permettant de regrouper les informations des différentes tables liées aux prix littéraires (prixLit, prixlit_categorie, prixlit_annee) en une seule vue pour faciliter les requêtes de recherche et d’affichage des données liées aux prix littéraires. Contient des champs dédiés à la gestion des informations principales du prix littéraire (nom, description, notes, code, etc..), ainsi que des champs agrégés pour les catégories et les années associées à chaque prix littéraire.

```
select
    distinct `p`.`id_prixLit` as `id_prixlit`,
    `p`.`nom_prixLit` as `nom_prixlit`,
    `p`.`description_prixLit` as `description_prixlit`,
    `p`.`Notes_PrixLit_txt` as `notes_prixlit_txt`,
    `p`.`is_actif` as `is_Actif`,
    `p`.`code_prixLit` as `code_prixlit`,
    `p`.`created_at` as `created_at`,
    `p`.`updated_at` as `updated_at`,
    group_concat(distinct `pc`.`libelle_categorie` separator ' | ') as `categories`,
    group_concat(distinct `pa`.`annee` separator ', ') as `annees`
from
    ((`prixlit` `p`
left join `prixlit_categorie` `pc` on
    (`p`.`id_prixLit` = `pc`.`id_prixLit`))
left join `prixlit_annee` `pa` on
    (`pc`.`id_prixlit_categorie` = `pa`.`id_prixlit_categorie`))
group by
    `p`.`id_prixLit`;
```

| Colonne             | Type                | Null  | Défaut                            | Clé/Contrainte    |
| ------------------- | ------------------- | ----- | --------------------------------- | ----------------- |
| id_prixlit          | bigint(20) unsigned | true  | nextval(`artefact`.`seq_prixlit`) | PK                |
| nom_prixlit         | varchar(200)        | true  |                                   |                   |
| description_prixlit | varchar(200)        | false |                                   |                   |
| notes_prixlit_txt   | text                | false |                                   |                   |
| is_Actif            | tinyint(1)          | true  | 1                                 |                   |
| code_prixlit        | varchar(12)         | false |                                   | UNIQUE; GENERATED |
| created_at          | datetime            | true  | current_timestamp()               |                   |
| updated_at          | datetime            | true  | current_timestamp()               |                   |
| categories          | mediumtext          | false |                                   |                   |
| annees              | mediumtext          | false |                                   |                   |

###### Extrait de données

| `v_prixlit_recherche` |                                 |                                                |                                                              |          |              |                     |                     |                                                              |            |
| --------------------- | ------------------------------- | ---------------------------------------------- | ------------------------------------------------------------ | -------- | ------------ | ------------------- | ------------------- | ------------------------------------------------------------ | ---------- |
| id_prixlit            | nom_prixlit                     | description_prixlit                            | notes_prixlit_txt                                            | is_Actif | code_prixlit | created_at          | updated_at          | categories                                                   | annees     |
| 1                     | Prix du Quai des Orfèvres       | Policier - France                              | Le prix du Quai des Orfèvres a été fondé en 1946 par Jacques Catineau, personnalité du monde de la communication et ami de la police et de la magistrature. Il récompense un ma... | 1        | R000001      | 2026-03-28 13:15:36 | 2026-03-28 13:15:36 | Lauréat                                                      | 1946, 2027 |
| 2                     | Prix Imaginales                 | Fantasy - France                               |                                                              | 1        | R000002      | 2026-03-28 13:25:29 | 2026-03-28 13:25:29 | Album \| Bibliothécaires \| Collégiens \| Ecoliers \| Lycéens \| Meilleur roman \| Meilleur roman étranger \| Meilleur roman francophone \| Meilleure bande-dessinée \| Meilleure illustration \| Meilleure nouvelle \| Meilleure œuvre pour la jeunesse \| Prix spécial du jury | 2015, 2028 |
| 3                     | Prix du Meilleur Livre étranger | Romans et Essais traduits en français - France | Le prix du Meilleur Livre étranger Sofitel est un prix littéraire qui récompense chaque année un roman et un essai publiés à l’étranger et traduits en français.  Créé | 1        | R000003      | 2026-03-28 15:10:44 | 2026-03-28 15:10:44 | Essai \| Roman                                               | 2020       |
| 4                     | Prix du Test des activités      | test - Essais                                  | Taratata kqjdhqjd   ghjdsahj dqddds sdqdqdq                  | 1        | R000004      | 2026-05-27 17:55:32 | 2026-05-27 17:57:39 | Meilleur petit test \| Meilleur Test                         | 2023, 2027 |

###### Diagram

<img src="Diagrams/artefact - v_prixlit_recherche.png" alt="v_prixlit_recherche" style="zoom:90%;" />

## TRIGGERS

#### `trg_auteurs_set_annee_naissance_bi`
Table auteurs - date_naissance et annee_naissance - Création. 
Trigger qui se déclenche avant l’insertion d’un nouvel auteur dans la table `auteurs`, permettant de remplir automatiquement le champ `annee_naissance` à partir du champ `date_naissance` si ce dernier est renseigné, ou de laisser la valeur de `annee_naissance` inchangée si `date_naissance` est nul.

`SET NEW.annee_naissance = IF(NEW.date_naissance IS NULL, NEW.annee_naissance, YEAR(NEW.date_naissance))`

#### `trg_auteurs_set_annee_naissance_bu`
Table auteurs - date_naissance et annee_naissance - Before update. 
Trigger qui se déclenche avant la mise à jour d’un auteur dans la table `auteurs`, permettant de mettre à jour automatiquement le champ `annee_naissance` à partir du champ `date_naissance` si ce dernier est renseigné, ou de laisser la valeur de `annee_naissance` inchangée si `date_naissance` est nul.

`SET NEW.annee_naissance = IF(NEW.date_naissance IS NULL, NEW.annee_naissance, YEAR(NEW.date_naissance))`

# Version Base de donnée

- Versionnement du schéma Artefact, vérification au démarrage de l'application
- Artefact utilise un versionnement interne du schéma de base de données.

  - Table concernée : `meta_schema`
  - Champ clé : `schema_version`
  - Vérification effectuée au démarrage de l'application
  - Version ***7*** au ***2026-03-20 16:45:07***

# Diagrammes de la base de données

<img src="artefact_key.png" alt="Artefact" style="zoom:80%;" />



---


# Dump
	- Backups de la base de données (sans données et avec données de test)
	- Scripts SQL pour la création de la base de données, les tables, les séquences, les procédures stockées, etc.
	- Script SQL pour la migration de la base de données, les modifications de schéma, etc.

- `Backup Database Artefact sans données` : [`backup_NoData_artefact.sql`](Docs/Database/backup_NoData_artefact.sql) - Backup de la base de données SQLite sans données, pour initialiser le projet

- `Backup Database Artefact avec données` : [`backup_WithData_artefact.sql`](Docs/Database/backup_WithData_artefact.sql) -  Pour les données de base (Tests)

  ------

  

# Documentation liée

- 
- `Diagrammes DB` : [`artefact_schema_erdiagram.mmd`](Docs/Database/artefact_schema_erdiagram.mmd) - Diagrammes de la base de données
- `Diagramme image` : [`artefact_key.png`](Docs/Database/artefact_key.png) - Diagramme de la base de données au format image
- `Modèle database`  : [`ModeleDB.md`](Docs/Database/ModeleDB.md) - Description détaillée du modèle de données, des tables, des relations, etc.
