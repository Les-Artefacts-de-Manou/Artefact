# 🏠 Architecture de la base de données Artefact

[TOC]

> Mise à jour du 24/05/2026 : aucun changement de schéma DB lié à la migration UI (passage Home/Forms vers portail + UC).

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
| ------------------ | ------------------------------ | ------------------------------ | --------------------------------------------------------- | --------------------------- | :-----: |
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

> ℹ️ Les sections détaillées sont complétées progressivement selon l'avancement fonctionnel.
> Pour garder une base exploitable dès maintenant, un tableau de colonnes consolidé est maintenu ici : [`Tableaux_Colonnes.md`](Tableaux_Colonnes.md).

### 1️⃣ Tables principales

#### 	🥅`livres`
Table centrale du modèle, pivot de l’ensemble du système.
Contient exclusivement des livres :
- validés  = bibliothèque validée
- normalisés
- reliés aux référentiels
- prêts à être exploités

##### Colonnes

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


### 3️⃣ Tables référentielles

#### 	🥅`auteurs`
table référentielle des auteurs, 

#### 	🥅`series`
table référentielle des séries


#### 	🥅`editeurs`
table référentielle des éditeurs

###### Colonnes

| Nom  de la colonne | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                             | Extra                          | Expression |
| ------------------ | ---- | -------------------- | --------- | ------------------- | ------ | ---------------------------------- | ------------------------------ | ---------- |
| id_editeur         | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_editeurs`) |                                | [NULL]     |
| nom_editeur        | 2    | varchar(200)         | true      | false               | UNI    | [NULL]                             |                                | [NULL]     |
| id_pays            | 3    | bigint(20)  unsigned | false     | false               | MUL    | NULL                               |                                | [NULL]     |
| site_web           | 4    | varchar(300)         | false     | false               | [NULL] | NULL                               |                                | [NULL]     |
| notes_editeur_rtf  | 5    | mediumtext           | false     | false               | [NULL] | NULL                               |                                | [NULL]     |
| notes_editeur_txt  | 6    | text                 | false     | false               | [NULL] | NULL                               |                                | [NULL]     |
| created_at         | 7    | datetime             | true      | false               | [NULL] | current_timestamp()                |                                | [NULL]     |
| updated_at         | 8    | datetime             | true      | false               | [NULL] | current_timestamp()                | on  update current_timestamp() | [NULL]     |
| code_editeur       | 9    | varchar(12)          | false     | false               | UNI    | NULL                               | STORED  GENERATED              | [NULL]     |

###### Constraints

| Nom              | Colonne      | Propriétaire | Type         | Check  expression |
| ---------------- | ------------ | ------------ | ------------ | ----------------- |
| PRIMARY          |              | editeurs     | PRIMARY  KEY |                   |
| id_editeur       | id_editeur   |              |              |                   |
| uq_editeurs_code |              | editeurs     | UNIQUE  KEY  |                   |
| code_editeur     | code_editeur |              |              |                   |
| uq_editeurs_nom  |              | editeurs     | UNIQUE  KEY  |                   |
| nom_editeur      | nom_editeur  |              |              |                   |

| Nom              | Colonne | Propriétaire | Référence de la table | Type        | Référence de l'objet | A la suppression | A la mise à jour |
| ---------------- | ------- | ------------ | --------------------- | ----------- | -------------------- | ---------------- | ---------------- |
| fk_editeurs_pays |         | editeurs     | pays                  | FOREIGN KEY | PRIMARY              | Set NULL         | Cascade          |
| id_pays          | id_pays |              |                       |             | id_pays              |                  |                  |

###### Références

| Nom               | Colonne    | Propriétaire | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ----------------- | ---------- | ------------ | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_livres_editeur |            | livres       | editeurs               | FOREIGN  KEY | PRIMARY               | Set  NULL         | Cascade           |
| id_editeur        | id_editeur |              |                        |              | id_editeur            |                   |                   |

###### Diagramme de la table

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

Table référentielle des tags

#### 	🥅`langues`
table référentielle permettant la gestion des langues

###### Colonnes

| Nom  de la colonne | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                            | Extra                          | Expression |
| ------------------ | ---- | -------------------- | --------- | ------------------- | ------ | --------------------------------- | ------------------------------ | ---------- |
| id_langue          | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_langues`) | [NULL]                         |            |
| nom_langue         | 2    | varchar(120)         | true      | false               | UNI    | [NULL]                            |                                | [NULL]     |
| abrev_langue       | 3    | varchar(10)          | true      | false               | UNI    | [NULL]                            |                                | [NULL]     |
| iso639_1           | 4    | char(2)              | false     | false               | MUL    | NULL                              |                                | [NULL]     |
| iso639_2           | 5    | char(3)              | false     | false               | MUL    | NULL                              |                                | [NULL]     |
| created_at         | 6    | datetime             | true      | false               | [NULL] | current_timestamp()               | [NULL]                         |            |
| updated_at         | 7    | datetime             | true      | false               | [NULL] | current_timestamp()               | on  update current_timestamp() | [NULL]     |
| code_langue        | 8    | varchar(12)          | false     | false               | UNI    | NULL                              | STORED  GENERATED              | [NULL]     |

###### Constraints

| Nom              | Colonne      | Propriétaire | Type         | Check  expression |
| ---------------- | ------------ | ------------ | ------------ | ----------------- |
| PRIMARY          | [NULL]       | langues      | PRIMARY  KEY | [NULL]            |
| id_langue        | id_langue    |              |              |                   |
| uq_langues_abrev | [NULL]       | langues      | UNIQUE  KEY  | [NULL]            |
| abrev_langue     | abrev_langue |              |              |                   |
| uq_langues_code  | [NULL]       | langues      | UNIQUE  KEY  | [NULL]            |
| code_langue      | code_langue  |              |              |                   |
| uq_langues_nom   | [NULL]       | langues      | UNIQUE  KEY  | [NULL]            |
| nom_langue       | nom_langue   |              |              |                   |

###### Références

| Nom                | Colonne            | Propriétaire | Référence de la table | Type        | Référence de l'objet | A la suppression | A la mise à jour |
| ------------------ | ------------------ | ------------ | --------------------- | ----------- | -------------------- | ---------------- | ---------------- |
| fk_auteurs_langues | auteurs            | langues      | FOREIGN KEY           | PRIMARY     | Set NULL             | Cascade          |                  |
| id_langue_ecriture | id_langue_ecriture |              |                       |             | id_langue            |                  |                  |
| fk_livres_langue   |                    | livres       | langues               | FOREIGN KEY | PRIMARY              | Set NULL         | Cascade          |
| id_langue          | id_langue          |              |                       |             | id_langue            |                  |                  |

###### Diagramme de la table

<img src="Diagrams/artefact - langues_All.png" alt="langues" style="zoom:80%;" /> 

###### Extrait de données

| **langues**    |                  |              |              |                          |                          |                 |
| -------------- | ---------------- | ------------ | ------------ | ------------------------ | ------------------------ | --------------- |
| **nom_langue** | **abrev_langue** | **iso639_1** | **iso639_2** | **created_at**           | **updated_at**           | **code_langue** |
| Français       | FR               | fr           | fra          | 2026-03-02  17:38:54.000 | 2026-03-02  17:38:54.000 | L000001         |
| Anglais        | EN               | en           | eng          | 2026-03-02  18:23:19.000 | 2026-03-02  18:52:05.000 | L000003         |
| Italien        | IT               | it           | ita          | 2026-03-03  13:13:00.000 | 2026-03-03  13:13:00.000 | L000005         |

#### 	🥅`pays`
Table référentielles permettant la gestion des pays.

###### Colonnes

| Nom de la colonne | #    | Type de donnée       | Non Null | Auto-Incrémentation | Clef   | Défaut                         | Extra                          | Expression |
| ----------------- | ---- | -------------------- | -------- | ------------------- | ------ | ------------------------------ | ------------------------------ | ---------- |
| id_pays           | 1    | bigint(20)  unsigned | true     | false               | PRI    | nextval(`artefact`.`seq_pays`) |                                | [NULL]     |
| nom_pays          | 2    | varchar(150)         | true     | false               | UNI    | [NULL]                         |                                | [NULL]     |
| iso2              | 3    | char(2)              | false    | false               | MUL    | NULL                           |                                | [NULL]     |
| iso3              | 4    | char(3)              | false    | false               | MUL    | NULL                           |                                | [NULL]     |
| created_at        | 5    | datetime             | true     | false               | [NULL] | current_timestamp()            |                                | [NULL]     |
| updated_at        | 6    | datetime             | true     | false               | [NULL] | current_timestamp()            | on  update current_timestamp() | [NULL]     |
| code_pays         | 7    | varchar(12)          | false    | false               | UNI    | NULL                           | STORED  GENERATED              | [NULL]     |

###### Constraints

| Nom          | Colonne   | Propriétaire | Type         | Check  expression |
| ------------ | --------- | ------------ | ------------ | ----------------- |
| PRIMARY      |           | pays         | PRIMARY  KEY | [NULL]            |
| id_pays      | id_pays   |              |              |                   |
| uq_pays_code |           | pays         | UNIQUE  KEY  | [NULL]            |
| code_pays    | code_pays |              |              |                   |
| uq_pays_nom  |           | pays         | UNIQUE  KEY  | [NULL]            |
| nom_pays     | nom_pays  |              |              |                   |

###### Références

| Nom              | Colonne | Propriétaire | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ---------------- | ------- | ------------ | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_auteurs_pays  |         | auteurs      | pays                   | FOREIGN  KEY | PRIMARY               | Set  NULL         | Cascade           |
| id_pays          | id_pays |              |                        |              | id_pays               |                   | [NULL]            |
| fk_ap_pays       |         | auteurs_pays | pays                   | FOREIGN  KEY | PRIMARY               | Restrict          | Restrict          |
| id_pays          | id_pays |              |                        |              | id_pays               |                   | [NULL]            |
| fk_editeurs_pays |         | editeurs     | pays                   | FOREIGN  KEY | PRIMARY               | Set  NULL         | Cascade           |
| id_pays          | id_pays |              |                        |              | id_pays               |                   | [NULL]            |

###### Diagramme de la table

<img src="Diagrams/artefact - pays_All.png" alt="pays" style="zoom:80%;" />

###### Extrait de données

| **pays**     |          |          |                          |                          |               |
| ------------ | -------- | -------- | ------------------------ | ------------------------ | ------------- |
| **nom_pays** | **iso2** | **iso3** | **created_at**           | **updated_at**           | **code_pays** |
| Royaume-Uni  | GB       | GBR      | 2026-03-03  16:50:32.000 | 2026-03-03  16:50:32.000 | P000004       |
| Belgique     | BE       | BEL      | 2026-03-03  16:50:32.000 | 2026-03-03  16:50:32.000 | P000005       |
| Italie       | IT       | ITA      | 2026-03-03  17:24:43.000 | 2026-03-03  17:24:43.000 | P000006       |
| France       | FR       | FRA      | 2026-03-03  17:42:33.000 | 2026-03-03  17:42:33.000 | P000007       |

`impression`
Table référentielle des impressions (Avant lecture, ce qui peut aller dans la PAL)


#### 	🥅`contacts`
Table référentielle des contacts, pour la gestion des prêts, échanges, recommandations etc..

###### Colonnes

| **Nom de la colonne** | **#** | **Type de donnée**   | **Non Null** | **Auto-Incrémentation** | **Clef** | **Défaut**                         | **Extra**                      |
| --------------------- | ----- | -------------------- | ------------ | ----------------------- | -------- | ---------------------------------- | ------------------------------ |
| id_contact            | 1     | bigint(20)  unsigned | true         | false                   | PRI      | nextval(`artefact`.`seq_contacts`) |                                |
| nom_contact           | 2     | varchar(200)         | true         | false                   | UNI      | [NULL]                             |                                |
| email_perso           | 3     | varchar(254)         | false        | false                   | MUL      | NULL                               |                                |
| adresse_liseuse       | 4     | varchar(254)         | false        | false                   | [NULL]   | NULL                               |                                |
| type_liseuse          | 5     | varchar(100)         | false        | false                   | [NULL]   | NULL                               |                                |
| created_at            | 6     | datetime             | true         | false                   | [NULL]   | current_timestamp()                |                                |
| updated_at            | 7     | datetime             | true         | false                   | [NULL]   | current_timestamp()                | on  update current_timestamp() |
| code_contact          | 8     | varchar(12)          | false        | false                   | UNI      | NULL                               | STORED  GENERATED              |

###### Constraints

| Nom              | Colonne      | Propriétaire | Type         | Check  expression |
| ---------------- | ------------ | ------------ | ------------ | ----------------- |
| PRIMARY          |              | contacts     | PRIMARY  KEY |                   |
| id_contact       | id_contact   |              |              |                   |
| uq_contacts_code |              | contacts     | UNIQUE  KEY  |                   |
| code_contact     | code_contact |              |              |                   |
|                  |              | contacts     | UNIQUE  KEY  |                   |
| nom_contact      | nom_contact  |              |              |                   |

###### Références

| Nom           | Colonne    | Propriétaire    | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ------------- | ---------- | --------------- | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_lc_contact |            | livres_contacts | contacts               | FOREIGN  KEY | PRIMARY               | Restrict          | Restrict          |
| id_contact    | id_contact |                 |                        |              | id_contact            |                   |                   |

###### Diagramme de la table

<img src="Diagrams/artefact - contacts_All.png" alt="contact" style="zoom:80%;" />

###### Extrait de données

| **contacts**   |                 |                     |                             |                  |                          |                          |                  |
| -------------- | --------------- | ------------------- | --------------------------- | ---------------- | ------------------------ | ------------------------ | ---------------- |
| **id_contact** | **nom_contact** | **email_perso**     | **adresse_liseuse**         | **type_liseuse** | **created_at**           | **updated_at**           | **code_contact** |
| 1              | Pearl           | pearlnduy@gmail.com | pearlnduy_hd6nrx@kindle.com | Kindle           | 2026-03-09  10:20:01.000 | 2026-03-09  10:20:01.000 | C000001          |

#### 	🥅`prixLit`

Table référentielle des prix littéraires

###### Colonnes

| Nom  de la colonne  | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                            | Extra                          |
| ------------------- | ---- | -------------------- | --------- | ------------------- | ------ | --------------------------------- | ------------------------------ |
| id_prixLit          | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_prixlit`) |                                |
| nom_prixLit         | 2    | varchar(200)         | true      | false               | UNI    | [NULL]                            |                                |
| description_prixLit | 3    | varchar(200)         | false     | false               | [NULL] | NULL                              |                                |
| Notes_PrixLit_txt   | 4    | text                 | false     | false               | [NULL] | NULL                              |                                |
| Notes_PrixLit_rtf   | 5    | text                 | false     | false               | [NULL] | NULL                              |                                |
| is_actif            | 6    | tinyint(1)           | true      | false               | [NULL] | 1                                 |                                |
| created_at          | 7    | datetime             | true      | false               | [NULL] | current_timestamp()               |                                |
| updated_at          | 8    | datetime             | true      | false               | [NULL] | current_timestamp()               | on  update current_timestamp() |
| code_prixLit        | 9    | varchar(12)          | false     | false               | UNI    | NULL                              | STORED  GENERATED              |

###### Constraints

| Nom             | Colonne      | Propriétaire | Type         | Check  expression |
| --------------- | ------------ | ------------ | ------------ | ----------------- |
| PRIMARY         |              | prixlit      | PRIMARY  KEY |                   |
| id_prixLit      | id_prixLit   |              |              |                   |
| uq_prixLit_code |              | prixlit      | UNIQUE  KEY  |                   |
| code_prixLit    | code_prixLit |              |              |                   |
| uq_prixLit_nom  |              | prixlit      | UNIQUE  KEY  |                   |
| nom_prixLit     | nom_prixLit  |              |              |                   |

###### Références

| Nom                          | Colonne    | Propriétaire      | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ---------------------------- | ---------- | ----------------- | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_prixlit_categorie_prixlit |            | prixlit_categorie | prixlit                | FOREIGN  KEY | PRIMARY               | Cascade           | Cascade           |
| id_prixLit                   | id_prixLit |                   |                        |              | id_prixLit            |                   |                   |

###### Diagramme de la table

<img src="Diagrams/artefact - prixlit_All.png" alt="prixlit" style="zoom:80%;" />

###### Extrait de données

ToDo

#### 	🥅`formatFile`
Table référentielle des formats de fichiers (ePub, AZW3, PDF, etc..)

###### Colonnes

| Nom  de la colonne | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                               | Extra                          | Expression | Commentaire |
| ------------------ | ---- | -------------------- | --------- | ------------------- | ------ | ------------------------------------ | ------------------------------ | ---------- | ----------- |
| id_formatFile      | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_formatfile`) |                                | [NULL]     |             |
| nom_format         | 2    | varchar(40)          | true      | false               | UNI    | [NULL]                               |                                | [NULL]     |             |
| extension          | 3    | varchar(10)          | false     | false               | MUL    | NULL                                 |                                | [NULL]     |             |
| mime_type          | 4    | varchar(100)         | false     | false               | [NULL] | NULL                                 |                                | [NULL]     |             |
| ordre_affichage    | 5    | int(11)              | true      | false               | [NULL] | 1                                    |                                | [NULL]     |             |
| is_actif           | 6    | tinyint(1)           | true      | false               | [NULL] | 1                                    |                                | [NULL]     |             |
| created_at         | 7    | datetime             | true      | false               | [NULL] | current_timestamp()                  |                                | [NULL]     |             |
| updated_at         | 8    | datetime             | true      | false               | [NULL] | current_timestamp()                  | on  update current_timestamp() | [NULL]     |             |
| code_formatFile    | 9    | varchar(12)          | false     | false               | UNI    | NULL                                 | STORED  GENERATED              | [NULL]     |             |

###### Constraints

| Nom                | Colonne         | Propriétaire | Type         | Check  expression |
| ------------------ | --------------- | ------------ | ------------ | ----------------- |
| PRIMARY            |                 | formatfile   | PRIMARY  KEY |                   |
| id_formatFile      | id_formatFile   |              |              |                   |
| uq_formatFile_code | formatfile      | UNIQUE  KEY  |              |                   |
| code_formatFile    | code_formatFile |              |              |                   |
| uq_formatFile_nom  |                 | formatfile   | UNIQUE  KEY  |                   |
| nom_format         | nom_format      |              |              |                   |

###### Références

| Nom           | Colonne       | Propriétaire    | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ------------- | ------------- | --------------- | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_lf_format  |               | livres_fichiers | formatfile             | FOREIGN  KEY | PRIMARY               | Restrict          | Restrict          |
| id_formatFile | id_formatFile |                 |                        |              | id_formatFile         |                   |                   |

###### Diagramme de la table

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
Table référentielle des impressions (Avant lecture, ce qui peut aller dans la PAL)

###### Colonnes

| Nom  de la colonne     | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                               | Extra                          | Expression |
| ---------------------- | ---- | -------------------- | --------- | ------------------- | ------ | ------------------------------------ | ------------------------------ | ---------- |
| id_impression          | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_impression`) |                                | [NULL]     |
| code_impression        | 2    | varchar(12)          | false     | false               | UNI    | NULL                                 | STORED  GENERATED              | [NULL]     |
| nom_impression         | 3    | varchar(120)         | true      | false               | UNI    | [NULL]                               |                                | [NULL]     |
| description_impression | 4    | varchar(400)         | false     | false               | [NULL] | NULL                                 |                                | [NULL]     |
| note_rtf               | 5    | mediumtext           | false     | false               | [NULL] | NULL                                 |                                | [NULL]     |
| note_txt               | 6    | text                 | false     | false               | [NULL] | NULL                                 |                                | [NULL]     |
| envie_Cal              | 7    | varchar(10)          | false     | false               | [NULL] | NULL                                 |                                | [NULL]     |
| is_actif               | 8    | tinyint(1)           | true      | false               | [NULL] | 1                                    |                                | [NULL]     |
| created_at             | 9    | datetime             | true      | false               | [NULL] | current_timestamp()                  |                                | [NULL]     |
| updated_at             | 10   | datetime             | true      | false               | [NULL] | current_timestamp()                  | on  update current_timestamp() | [NULL]     |

###### Constraints

| Nom                | Colonne         | Propriétaire | Type         | Check  expression |
| ------------------ | --------------- | ------------ | ------------ | ----------------- |
| PRIMARY            |                 | impression   | PRIMARY  KEY |                   |
| id_impression      | id_impression   |              |              |                   |
| uq_impression_code |                 | impression   | UNIQUE  KEY  |                   |
| code_impression    | code_impression |              |              |                   |
| uq_impression_nom  |                 | impression   | UNIQUE  KEY  |                   |
| nom_impression     | nom_impression  |              |              |                   |

###### Références

| Nom                  | Colonne       | Propriétaire   | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| -------------------- | ------------- | -------------- | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_livres_impression |               | livres         | impression             | FOREIGN  KEY | PRIMARY               | Set  NULL         | Cascade           |
| id_impression        | id_impression |                |                        |              | id_impression         |                   |                   |
| fk_ls_impression     |               | livres_staging | impression             | FOREIGN  KEY | PRIMARY               | Set  NULL         | Cascade           |
| id_impression        | id_impression |                |                        |              | id_impression         |                   |                   |

###### Diagramme de la table

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

Table référentielle des recommandations, pour la gestion des recommandations de livres, d’auteurs, de séries etc..

###### Colonnes

| Nom  de la colonne        | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                                    | Extra                          | Expression |
| ------------------------- | ---- | -------------------- | --------- | ------------------- | ------ | ----------------------------------------- | ------------------------------ | ---------- |
| id_recommandation         | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_recommandations`) |                                | [NULL]     |
| code_recommandation       | 2    | varchar(12)          | false     | false               | UNI    | NULL                                      | STORED  GENERATED              | [NULL]     |
| id_origine_recommandation | 3    | bigint(20)  unsigned | true      | false               | MUL    | [NULL]                                    |                                | [NULL]     |
| source_nom                | 4    | varchar(150)         | false     | false               | [NULL] | NULL                                      |                                | [NULL]     |
| source_login              | 5    | varchar(150)         | false     | false               | [NULL] | NULL                                      |                                | [NULL]     |
| source_url                | 6    | varchar(500)         | false     | false               | [NULL] | NULL                                      |                                | [NULL]     |
| date_recommandation       | 7    | date                 | false     | false               | [NULL] | NULL                                      |                                | [NULL]     |
| commentaire_rtf           | 8    | mediumtext           | false     | false               | [NULL] | NULL                                      |                                | [NULL]     |
| commentaire_txt           | 9    | text                 | false     | false               | [NULL] | NULL                                      |                                | [NULL]     |
| is_actif                  | 10   | tinyint(1)           | true      | false               | [NULL] | 1                                         |                                | [NULL]     |
| created_at                | 11   | datetime             | true      | false               | [NULL] | current_timestamp()                       |                                | [NULL]     |
| updated_at                | 12   | datetime             | true      | false               | [NULL] | current_timestamp()                       | on  update current_timestamp() | [NULL]     |

###### Constraints

| Nom                     | Colonne             | Propriétaire    | Type         | Check  expression |
| ----------------------- | ------------------- | --------------- | ------------ | ----------------- |
| PRIMARY                 |                     | recommandations | PRIMARY  KEY |                   |
| id_recommandation       | id_recommandation   |                 |              |                   |
| uq_recommandations_code |                     | recommandations | UNIQUE  KEY  |                   |
| code_recommandation     | code_recommandation |                 |              |                   |

| Nom                        | Colonne                   | Propriétaire    | Référence  de la table     | Type                      | Référence  de l'objet | A  la suppression | A  la mise à jour |
| -------------------------- | ------------------------- | --------------- | -------------------------- | ------------------------- | --------------------- | ----------------- | ----------------- |
| fk_recommandations_origine |                           | recommandations | ref_origine_recommandation | FOREIGN  KEY              | PRIMARY               | Restrict          | Cascade           |
| id_origine_recommandation  | id_origine_recommandation |                 |                            | id_origine_recommandation |                       |                   |                   |

###### Références

| Nom                                              | Colonne           | Propriétaire                   | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ------------------------------------------------ | ----------------- | ------------------------------ | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_livres_recommandations_recommandation         |                   | livres_recommandations         | recommandations        | FOREIGN  KEY | PRIMARY               | Cascade           | Cascade           |
| id_recommandation                                | id_recommandation |                                |                        |              | id_recommandation     |                   |                   |
| fk_livres_staging_recommandations_recommandation |                   | livres_staging_recommandations | recommandations        | FOREIGN  KEY | PRIMARY               | Cascade           | Cascade           |
| id_recommandation                                | id_recommandation |                                |                        |              | id_recommandation     |                   |                   |

###### Diagramme de la table

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
Table sous-référentielle des années d’attribution des prix littéraires, avec la possibilité de faire le lien avec une catégorie (ex: Prix Goncourt 2020, Prix Goncourt 2021, etc..)

###### Colonnes

| Nom  de la colonne   | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef | Défaut                                  | Extra                          | Expression |
| -------------------- | ---- | -------------------- | --------- | ------------------- | ---- | --------------------------------------- | ------------------------------ | ---------- |
| id_prixLit_Annee     | 1    | bigint(20)  unsigned | true      | false               | PRI  | nextval(`artefact`.`seq_prixlit_annee`) |                                |            |
| id_prixlit_categorie | 3    | bigint(20)  unsigned | true      | false               | MUL  | NULL                                    |                                |            |
| annee                | 4    | smallint(6)          | true      | false               | MUL  |                                         |                                |            |
| created_at           | 5    | datetime             | true      | false               |      | current_timestamp()                     |                                |            |
| updated_at           | 6    | datetime             | true      | false               |      | current_timestamp()                     | on  update current_timestamp() |            |
| code_prixLit_Annee   | 7    | varchar(12)          | false     | false               | UNI  | NULL                                    | STORED  GENERATED              |            |

###### Constraints

| Nom                              | Colonne              | Propriétaire  | Type         | Check  expression |
| -------------------------------- | -------------------- | ------------- | ------------ | ----------------- |
| PRIMARY                          |                      | prixlit_annee | PRIMARY  KEY |                   |
| id_prixLit_Annee                 | id_prixLit_Annee     |               |              |                   |
| uq_prixLit_Annee_code            |                      | prixlit_annee | UNIQUE  KEY  |                   |
| code_prixLit_Annee               | code_prixLit_Annee   |               |              |                   |
| uq_prixlit_annee_categorie_annee |                      | prixlit_annee | UNIQUE  KEY  |                   |
| id_prixlit_categorie             | id_prixlit_categorie |               |              |                   |
| annee                            | annee                |               |              |                   |

| Nom                                | Colonne              | Propriétaire  | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ---------------------------------- | -------------------- | ------------- | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_prixlit_annee_prixlit_categorie |                      | prixlit_annee | prixlit_categorie      | FOREIGN  KEY | PRIMARY               | Restrict          | Cascade           |
| id_prixlit_categorie               | id_prixlit_categorie |               |                        |              | id_prixlit_categorie  |                   |                   |

###### Références

| Nom              | Colonne          | Propriétaire         | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ---------------- | ---------------- | -------------------- | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_lpa_prixannee |                  | livres_prixlit_annee | prixlit_annee          | FOREIGN  KEY | PRIMARY               | Restrict          | Restrict          |
| id_prixLit_Annee | id_prixLit_Annee |                      |                        |              | id_prixLit_Annee      |                   |                   |

###### Diagramme de la table

<img src="Diagrams/artefact - prixLit_Annee_All.png" alt="prixLit_Annee" style="zoom:80%;" />

###### Extrait de données

ToDo

#### 	🥅`prixLit_categorie`
Table sous-référentielle des catégories de prix littéraires (ex: Prix Goncourt, Prix Femina, etc..), avec la possibilité de faire le lien avec les années d’attribution (ex: Prix Goncourt 2020, Prix Goncourt 2021, etc..)

###### Colonnes

| Nom  de la colonne     | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                                      | Extra                          | Expression |
| ---------------------- | ---- | -------------------- | --------- | ------------------- | ------ | ------------------------------------------- | ------------------------------ | ---------- |
| id_prixlit_categorie   | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_prixlit_categorie`) | [NULL]                         |            |
| id_prixLit             | 3    | bigint(20)  unsigned | true      | false               | MUL    | [NULL]                                      |                                | [NULL]     |
| libelle_categorie      | 4    | varchar(200)         | true      | false               | [NULL] | [NULL]                                      |                                | [NULL]     |
| description_categorie  | 5    | Varchar(200)         | false     | false               | [NULL] | NULL                                        |                                | [NULL]     |
| ordre_affichage        | 6    | int(11)              | true      | false               | MUL    | 0                                           |                                | [NULL]     |
| is_actif               | 7    | tinyint(1)           | true      | false               | [NULL] | 1                                           |                                | [NULL]     |
| created_at             | 8    | datetime             | true      | false               | [NULL] | current_timestamp()                         | [NULL]                         |            |
| updated_at             | 9    | datetime             | true      | false               | [NULL] | current_timestamp()                         | on  update current_timestamp() | [NULL]     |
| code_prixlit_categorie | 9    | varchar(12)          | false     | false               | UNI    | NULL                                        | STORED  GENERATED              | [NULL]     |

###### Constraints

| Nom                               | Colonne                | Propriétaire      | Type         | Check  expression |
| --------------------------------- | ---------------------- | ----------------- | ------------ | ----------------- |
| PRIMARY                           |                        | prixlit_categorie | PRIMARY  KEY |                   |
| id_prixlit_categorie              | id_prixlit_categorie   |                   |              |                   |
| uq_prixlit_categorie_code         | [NULL]                 | prixlit_categorie | UNIQUE  KEY  |                   |
| code_prixlit_categorie            | code_prixlit_categorie |                   |              |                   |
| uq_prixlit_categorie_idcat_idprix |                        | prixlit_categorie | UNIQUE  KEY  |                   |
| id_prixlit_categorie              | id_prixlit_categorie   |                   |              |                   |
| id_prixLit                        | id_prixLit             |                   |              |                   |
| uq_prixlit_categorie_prix_lib     |                        | prixlit_categorie | UNIQUE  KEY  |                   |
| id_prixLit                        | id_prixLit             |                   |              |                   |
| libelle_categorie                 | libelle_categorie      |                   |              |                   |

| Nom                          | Colonne    | Propriétaire      | Référence  de la table | Type         | Référence  de l'objet | A  la suppression | A  la mise à jour |
| ---------------------------- | ---------- | ----------------- | ---------------------- | ------------ | --------------------- | ----------------- | ----------------- |
| fk_prixlit_categorie_prixlit |            | prixlit_categorie | prixlit                | FOREIGN  KEY | PRIMARY               | Cascade           | Cascade           |
| id_prixLit                   | id_prixLit |                   |                        |              | id_prixLit            |                   |                   |

###### Références

| Nom                                 | Colonne              | Propriétaire  | Référence  de la table | Type         | Référence  de l'objet             | A  la suppression | A  la mise à jour |
| ----------------------------------- | -------------------- | ------------- | ---------------------- | ------------ | --------------------------------- | ----------------- | ----------------- |
| fk_prixlit_annee__prixlit_categorie |                      | prixlit_annee | prixlit_categorie      | FOREIGN  KEY | uq_prixlit_categorie_idcat_idprix | Set  NULL         | Cascade           |
| id_prixlit_categorie                | id_prixlit_categorie |               |                        |              | id_prixlit_categorie              |                   |                   |

###### Diagramme de la table

<img src="Diagrams/artefact - prixLit_categorie_All.png" alt="prixLit_categorie" style="zoom:80%;" />

###### Extrait de données

ToDo

#### 	🥅`series_format`

###### Colonnes



###### Constraints



###### Références



###### Diagramme de la table



###### Extrait de données

#### 	🥅`series_statut`

###### Colonnes



###### Constraints



###### Références



###### Diagramme de la table



###### Extrait de données

#### 	🥅`ref_enum_type`
Table sous-référentielle, attachée à ref_enum, listant les différentes catégories (type) d'énumérations.

###### Colonnes

| Nom  de la colonne | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                                  | Extra                          | Expression |
| ------------------ | ---- | -------------------- | --------- | ------------------- | ------ | --------------------------------------- | ------------------------------ | ---------- |
| code_enum_type     | 2    | varchar(12)          | false     | false               | UNI    | NULL                                    | STORED  GENERATED              | [NULL]     |
| code_type          | 3    | varchar(60)          | true      | false               | UNI    | [NULL]                                  |                                | [NULL]     |
| created_at         | 7    | datetime             | true      | false               | [NULL] | current_timestamp()                     |                                | [NULL]     |
| id_enum_type       | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_ref_enum_type`) |                                | [NULL]     |
| is_actif           | 6    | tinyint(1)           | true      | false               | [NULL] | 1                                       |                                | [NULL]     |
| libelle_type       | 4    | varchar(120)         | true      | false               | [NULL] | [NULL]                                  |                                | [NULL]     |
| ordre_affichage    | 5    | int(11)              | true      | false               | [NULL] | 0                                       |                                | [NULL]     |
| updated_at         | 8    | datetime             | true      | false               | [NULL] | current_timestamp()                     | on  update current_timestamp() | [NULL]     |

###### Constraints

| Nom          | Colonne   | Propriétaire | Type        | Check expression |
| ------------ | --------- | ------------ | ----------- | ---------------- |
| PRIMARY      |           | pays         | PRIMARY KEY |                  |
| id_pays      | id_pays   |              |             |                  |
| uq_pays_code |           | pays         | UNIQUE KEY  |                  |
| code_pays    | code_pays |              |             |                  |
| uq_pays_nom  |           | pays         | UNIQUE KEY  |                  |
| nom_pays     | nom_pays  |              |             |                  |

###### Références

| Nom              | Colonne | Propriétaire | Référence de la table | Type        | Référence de l'objet | A la suppression | A la mise à jour |
| ---------------- | ------- | ------------ | --------------------- | ----------- | -------------------- | ---------------- | ---------------- |
| fk_auteurs_pays  |         | auteurs      | pays                  | FOREIGN KEY | PRIMARY              | Set NULL         | Cascade          |
| id_pays          | id_pays |              |                       |             | id_pays              |                  |                  |
| fk_ap_pays       |         | auteurs_pays | pays                  | FOREIGN KEY | PRIMARY              | Restrict         | Restrict         |
| id_pays          | id_pays |              |                       |             | id_pays              |                  |                  |
| fk_editeurs_pays |         | editeurs     | pays                  | FOREIGN KEY | PRIMARY              | Set NULL         | Cascade          |
| id_pays          | id_pays |              |                       |             | id_pays              |                  |                  |

###### Diagramme de la table

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

###### Colonnes

| Nom  de la colonne             | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                                               | Extra                          | Expression | Commentaire |
| ------------------------------ | ---- | -------------------- | --------- | ------------------- | ------ | ---------------------------------------------------- | ------------------------------ | ---------- | ----------- |
| id_origine_recommandation      | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_ref_origine_recommandation`) | [NULL]                         |            |             |
| code_origine_recommandation    | 2    | varchar(30)          | true      | false               | UNI    | [NULL]                                               |                                | [NULL]     |             |
| libelle_origine_recommandation | 3    | varchar(100)         | true      | false               | [NULL] | [NULL]                                               |                                | [NULL]     |             |
| ordre_affichage                | 4    | int(11)              | true      | false               | [NULL] | 0                                                    |                                | [NULL]     |             |
| is_actif                       | 5    | tinyint(1)           | true      | false               | [NULL] | 1                                                    |                                | [NULL]     |             |
| created_at                     | 6    | datetime             | true      | false               | [NULL] | current_timestamp()                                  | [NULL]                         |            |             |
| updated_at                     | 7    | datetime             | true      | false               | [NULL] | current_timestamp()                                  | on  update current_timestamp() | [NULL]     |             |

###### Constraints

| Nom                                | Colonne                     | Propriétaire               | Type         | Check  expression |
| ---------------------------------- | --------------------------- | -------------------------- | ------------ | ----------------- |
| PRIMARY                            |                             | ref_origine_recommandation | PRIMARY  KEY |                   |
| id_origine_recommandation          | id_origine_recommandation   |                            |              |                   |
| uq_ref_origine_recommandation_code | ref_origine_recommandation  | UNIQUE  KEY                |              |                   |
| code_origine_recommandation        | code_origine_recommandation |                            |              |                   |

###### Références

| Nom                        | Colonne                   | Propriétaire               | Référence  de la table    | Type    | Référence  de l'objet | A  la suppression |
| -------------------------- | ------------------------- | -------------------------- | ------------------------- | ------- | --------------------- | ----------------- |
| fk_recommandations_origine | recommandations           | ref_origine_recommandation | FOREIGN  KEY              | PRIMARY | Restrict              | Cascade           |
| id_origine_recommandation  | id_origine_recommandation |                            | id_origine_recommandation |         |                       |                   |

###### Diagramme de la table

<img src="Diagrams/artefact - ref_origine_recommandation_All.png" alt="ref_origine_recommandation" style="zoom:80%;" />

###### Extrait de données

ToDo

### 5️⃣ Tables de liaison (Many-to-Many ou Many-to-One)

#### a. `Livres` et  `livres_staging`

#### 	🥅`livres_auteurs`



#### 	🥅`livres_staging_auteurs`



#### 	`🥅livres_tags`



#### 	🥅`livres_prixLit_annee`



#### 	`🥅livres_contacts`



#### 	🥅`livres_fichiers`



#### 	`	🥅livres_recommandations` 



#### 	🥅`livres_staging_recommandations` 



#### b.  `Auteurs`
#### 	🥅`auteurs_pays`

 

### 5️⃣ Table générique 

#### `ref_enum`

Table générique qui remplace les ENUM SQL natifs, 

  - Utilisée pour par ex statut_lecture ou support_lecture ou role_auteur_livre etc..
###### Colonnes

| Nom  de la colonne | #    | Type  de donnée      | Non  Null | Auto-Incrémentation | Clef   | Défaut                             | Extra                          |
| ------------------ | ---- | -------------------- | --------- | ------------------- | ------ | ---------------------------------- | ------------------------------ |
| code_enum          | 2    | varchar(12)          | false     | false               | UNI    | NULL                               | STORED  GENERATED              |
| code_valeur        | 4    | varchar(40)          | true      | false               | [NULL] | [NULL]                             |                                |
| created_at         | 8    | datetime             | true      | false               | [NULL] | current_timestamp()                |                                |
| id_enum            | 1    | bigint(20)  unsigned | true      | false               | PRI    | nextval(`artefact`.`seq_ref_enum`) |                                |
| id_enum_type       | 3    | bigint(20)  unsigned | true      | false               | MUL    | [NULL]                             |                                |
| is_actif           | 7    | tinyint(1)           | true      | false               | [NULL] | 1                                  |                                |
| libelle_valeur     | 5    | varchar(120)         | true      | false               | MUL    | [NULL]                             |                                |
| ordre_affichage    | 6    | int(11)              | true      | false               | [NULL] | 0                                  |                                |
| updated_at         | 9    | datetime             | true      | false               | [NULL] | current_timestamp()                | on  update current_timestamp() |

###### Constraints

| Nom                       | Colonne      | Propriétaire | Type         | Check  expression |
| ------------------------- | ------------ | ------------ | ------------ | ----------------- |
| PRIMARY                   |              | ref_enum     | PRIMARY  KEY |                   |
| id_enum                   | id_enum      |              |              |                   |
| uq_ref_enum_code          |              | ref_enum     | UNIQUE  KEY  |                   |
| code_enum                 | code_enum    | [NULL]       |              |                   |
| uq_ref_enum_enumtype_code |              | ref_enum     | UNIQUE  KEY  |                   |
| id_enum_type              | id_enum_type |              |              |                   |
| code_valeur               | code_valeur  |              |              |                   |

| Nom              | Colonne      | Propriétaire  | Référence de la table | Type    | Référence de l'objet | A la suppression | A la mise à jour |
| ---------------- | ------------ | ------------- | --------------------- | ------- | -------------------- | ---------------- | ---------------- |
| fk_ref_enum_type | ref_enum     | ref_enum_type | FOREIGN KEY           | PRIMARY | Restrict             | Restrict         |                  |
| id_enum_type     | id_enum_type |               |                       |         | id_enum_type         |                  |                  |

###### Références

| fk_ap_type                 | [NULL]                | auteurs_pays           | ref_enum | FOREIGN  KEY | PRIMARY | Restrict  | Restrict |
| -------------------------- | --------------------- | ---------------------- | -------- | ------------ | ------- | --------- | -------- |
| id_type_relation_pays      | id_type_relation_pays |                        |          |              | id_enum |           |          |
| fk_livres_statut_lecture   |                       | livres                 | ref_enum | FOREIGN  KEY | PRIMARY | Set  NULL | Cascade  |
| id_statut_lecture          | id_statut_lecture     |                        |          |              | id_enum |           |          |
| fk_livres_support_lecture  |                       | livres                 | ref_enum | FOREIGN  KEY | PRIMARY | Set  NULL | Cascade  |
| id_support_lecture         | id_support_lecture    |                        |          |              | id_enum |           |          |
| fk_livres_type_acquisition | [NULL]                | livres                 | ref_enum | FOREIGN  KEY | PRIMARY | Set  NULL | Cascade  |
| id_type_acquisition        | id_type_acquisition   |                        |          |              | id_enum |           |          |
| fk_la_role                 |                       | livres_auteurs         | ref_enum | FOREIGN  KEY | PRIMARY | Restrict  | Restrict |
| id_role_auteur             | id_role_auteur        |                        |          |              | id_enum |           |          |
| fk_lf_scope                |                       | livres_fichiers        | ref_enum | FOREIGN  KEY | PRIMARY | Restrict  | Restrict |
| id_scope_livre             | id_scope_livre        |                        |          |              | id_enum |           |          |
| fk_lf_type                 |                       | livres_fichiers        | ref_enum | FOREIGN  KEY | PRIMARY | Restrict  | Restrict |
| id_type_fichier            | id_type_fichier       |                        |          |              | id_enum |           |          |
| fk_ls_source_import        |                       | livres_staging         | ref_enum | FOREIGN  KEY | PRIMARY | Restrict  | Restrict |
| id_source_import           | id_source_import      |                        |          |              | id_enum |           |          |
| fk_ls_statut               |                       | livres_staging         | ref_enum | FOREIGN  KEY | PRIMARY | Restrict  | Restrict |
| id_statut_staging          | id_statut_staging     |                        |          |              | id_enum |           |          |
| fk_lsa_role                |                       | livres_staging_auteurs | ref_enum | FOREIGN  KEY | PRIMARY | Restrict  | Restrict |
| id_role_auteur             | id_role_auteur        |                        |          |              | id_enum |           |          |

###### Diagramme de la table

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

Connexion base : IP serveur, nom DB, user etc...

#### 	🥅`param_paths`
Tous les chemins :

Path_General, Path_Data, Path_Data_LivBiblio etc.

#### 	🥅`param_api`

OpenAI_API_Key, GoogleBooks_API_Key, endpoints, timeouts, paramètres d’enrichissement

### 7️⃣  Tables Configuration

####  	🥅`meta_schema` 



 ### 8️⃣  Tables Séquences
#### 	🥅`seq_auteurs`



#### 	🥅`seq_contacts`



#### 	🥅`seq_editeurs`



#### 	🥅`seq_formatFile`



#### 	🥅`seq_impression`



#### 	🥅`seq_langues`



#### 	🥅`seq_livres`



#### 	🥅`seq_livres_fichier`



#### 	🥅`Seq_livres_staging`



#### 	🥅`seq_param_api`



#### 	🥅`seq_param_db`



#### 	🥅`seq_param_paths`



#### 	🥅`seq_pays`



#### 	🥅`seq_prixLit`



#### 	🥅`seq_prixLit_Annee`



#### 	🥅`seq_prixlit_categorie`



#### 	`🥅seq_recommandations`



#### 	🥅`seq_ref_enum`



#### 	🥅`seq_ref_enum_type`



#### 	🥅`seq_ref_origine_recommandation`



#### 	`🥅seq_series`



#### 	🥅`seq_series_format`



#### 	`🥅seq_series_statut`



#### 	🥅`seq_tags`



## TRIGGERS

#### `trg_auteurs_set_annee_naissance_bi`

- Table auteurs - date_naissance et annee_naissance - Création

`SET NEW.annee_naissance = IF(NEW.date_naissance IS NULL, NEW.annee_naissance, YEAR(NEW.date_naissance))`

#### `trg_auteurs_set_annee_naissance_bu`

Table auteurs - date_naissance et annee_naissance - Before update

`SET NEW.annee_naissance = IF(NEW.date_naissance IS NULL, NEW.annee_naissance, YEAR(NEW.date_naissance))`

# 🦋 Version Base de donnée

- Versionnement du schéma Artefact, vérification au démarrage de l'application
- Artefact utilise un versionnement interne du schéma de base de données.

  - Table concernée : `meta_schema`
  - Champ clé : `schema_version`
  - Vérification effectuée au démarrage de l'application
  - Version ***6*** au ***2026-03-20 16:45:07***

----

# 🎡Diagrammes de la base de données

<img src="artefact_key.png" alt="Artefact" style="zoom:80%;" />



---


# 👯Dump
	- Backups de la base de données (sans données et avec données de test)
	- Scripts SQL pour la création de la base de données, les tables, les séquences, les procédures stockées, etc.
	- Script SQL pour la migration de la base de données, les modifications de schéma, etc.

- `Backup Database Artefact sans données` : [`backup_NoData_artefact.sql`](Docs/Database/backup_NoData_artefact.sql) - Backup de la base de données SQLite sans données, pour initialiser le projet

- `Backup Database Artefact avec données` : [`backup_WithData_artefact.sql`](Docs/Database/backup_WithData_artefact.sql) -  Pour les données de base (Tests)

  ------

  

# 📃Documentation liée

- 
- `Diagrammes DB` : [`artefact_schema_erdiagram.mmd`](Docs/Database/artefact_schema_erdiagram.mmd) - Diagrammes de la base de données
- `Diagramme image` : [`artefact_key.png`](Docs/Database/artefact_key.png) - Diagramme de la base de données au format image
- `Modèle database`  : [`ModeleDB.md`](Docs/Database/ModeleDB.md) - Description détaillée du modèle de données, des tables, des relations, etc.
