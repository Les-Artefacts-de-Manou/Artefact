# 🧾 Tableaux de colonnes – Base Artefact

[TOC]

Ce document centralise les **champs, types et contraintes visibles dans le script SQL**.
Il sert de référence de travail pour les tables déjà en place, même si certaines zones fonctionnelles ne sont pas encore stabilisées.

Source : [`backup_NoData_artefact.sql`](backup_NoData_artefact.sql).

## `auteurs`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_auteur` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_auteurs)` | PK |
| `nom_auteur` | `varchar(150)` | NO | `` | INDEX |
| `prenom_auteur` | `varchar(150)` | YES | `NULL` |  |
| `nomcomplet_auteur` | `varchar(320)` | NO | `` | INDEX |
| `id_pays` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `pays`.`id_pays` |
| `id_langue_ecriture` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `langues`.`id_langue` |
| `date_naissance` | `date` | YES | `NULL` |  |
| `annee_naissance` | `smallint(6)` | YES | `NULL` |  |
| `biographie` | `text` | YES | `NULL` |  |
| `site_web` | `varchar(300)` | YES | `NULL` |  |
| `chemin_photo_auteur` | `varchar(600)` | YES | `NULL` |  |
| `nom_normalise` | `varchar(160)` | YES | `NULL` |  |
| `prenom_normalise` | `varchar(160)` | YES | `NULL` |  |
| `nomcomplet_normalise` | `varchar(340)` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_auteur` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `auteurs_pays`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_auteur` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `auteurs`.`id_auteur` |
| `id_pays` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `pays`.`id_pays` |
| `id_type_relation_pays` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `ref_enum`.`id_enum` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `contacts`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_contact` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_contacts)` | PK |
| `nom_contact` | `varchar(200)` | NO | `` | UNIQUE |
| `email_perso` | `varchar(254)` | YES | `NULL` | INDEX |
| `adresse_liseuse` | `varchar(254)` | YES | `NULL` |  |
| `type_liseuse` | `varchar(100)` | YES | `NULL` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_contact` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `editeurs`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_editeur` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_editeurs)` | PK |
| `nom_editeur` | `varchar(200)` | NO | `` | UNIQUE |
| `id_pays` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `pays`.`id_pays` |
| `site_web` | `varchar(300)` | YES | `NULL` |  |
| `notes_editeur_rtf` | `mediumtext` | YES | `NULL` |  |
| `notes_editeur_txt` | `text` | YES | `NULL` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_editeur` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `formatfile`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_formatFile` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_formatfile)` | PK |
| `nom_format` | `varchar(40)` | NO | `` | UNIQUE |
| `extension` | `varchar(10)` | YES | `NULL` | INDEX |
| `mime_type` | `varchar(100)` | YES | `NULL` |  |
| `ordre_affichage` | `int(11)` | NO | `1` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_formatFile` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `impression`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_impression` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_impression)` | PK |
| `code_impression` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `nom_impression` | `varchar(120)` | NO | `` | UNIQUE |
| `description_impression` | `varchar(400)` | YES | `NULL` |  |
| `note_rtf` | `mediumtext` | YES | `NULL` |  |
| `note_txt` | `text` | YES | `NULL` |  |
| `envie_Cal` | `varchar(10)` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `langues`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_langue` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_langues)` | PK |
| `nom_langue` | `varchar(120)` | NO | `` | UNIQUE |
| `abrev_langue` | `varchar(10)` | NO | `` | UNIQUE |
| `iso639_1` | `char(2)` | YES | `NULL` | INDEX |
| `iso639_2` | `char(3)` | YES | `NULL` | INDEX |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_langue` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `livres`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_livres)` | PK |
| `id_calibre` | `bigint(20) unsigned` | YES | `NULL` | INDEX |
| `code_livre` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `titre` | `varchar(350)` | NO | `` | INDEX |
| `titre_normalise` | `varchar(380)` | YES | `NULL` | INDEX |
| `annee_publication` | `smallint(6)` | YES | `NULL` | INDEX |
| `date_publication` | `date` | YES | `NULL` | INDEX |
| `synopsis` | `text` | YES | `NULL` |  |
| `commentaire` | `text` | YES | `NULL` |  |
| `id_langue` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `langues`.`id_langue` |
| `id_impression` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `impression`.`id_impression` |
| `id_editeur` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `editeurs`.`id_editeur` |
| `id_serie` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `series`.`id_serie` |
| `num_tome` | `decimal(6,2)` | YES | `NULL` | INDEX |
| `tome_libelle` | `varchar(50)` | YES | `NULL` |  |
| `id_statut_lecture` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `ref_enum`.`id_enum` |
| `id_support_lecture` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `ref_enum`.`id_enum` |
| `id_type_acquisition` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `ref_enum`.`id_enum` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_auteurs`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre` | `bigint(20) unsigned` | NO | `` | PK; FK → `livres`.`id_livre` |
| `id_auteur` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `auteurs`.`id_auteur` |
| `id_role_auteur` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `ref_enum`.`id_enum` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_contacts`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre` | `bigint(20) unsigned` | NO | `` | PK; FK → `livres`.`id_livre` |
| `id_contact` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `contacts`.`id_contact` |
| `id_livre_fichier` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `livres_fichiers`.`id_livre_fichier` |
| `date_envoi` | `datetime` | YES | `NULL` |  |
| `commentaire` | `varchar(255)` | YES | `NULL` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_fichiers`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre_fichier` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_livres_fichiers)` | PK |
| `code_livre_fichier` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `id_scope_livre` | `bigint(20) unsigned` | NO | `` | UNIQUE; INDEX; FK → `ref_enum`.`id_enum` |
| `id_livre` | `bigint(20) unsigned` | YES | `NULL` | UNIQUE; INDEX; FK → `livres`.`id_livre` |
| `id_livre_staging` | `bigint(20) unsigned` | YES | `NULL` | UNIQUE; INDEX; FK → `livres_staging`.`id_livre_staging` |
| `id_type_fichier` | `bigint(20) unsigned` | NO | `` | UNIQUE; INDEX; FK → `ref_enum`.`id_enum` |
| `id_formatFile` | `bigint(20) unsigned` | YES | `NULL` | UNIQUE; INDEX; FK → `formatfile`.`id_formatFile` |
| `chemin_fichier` | `varchar(800)` | NO | `` | UNIQUE |
| `nom_fichier` | `varchar(255)` | YES | `NULL` |  |
| `extension_source` | `varchar(20)` | YES | `NULL` |  |
| `taille_octets` | `bigint(20) unsigned` | YES | `NULL` |  |
| `hash_sha1` | `char(40)` | YES | `NULL` |  |
| `is_principal` | `tinyint(1)` | NO | `0` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_prixlit_annee`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre` | `bigint(20) unsigned` | NO | `` | PK; FK → `livres`.`id_livre` |
| `id_prixLit_Annee` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `prixlit_annee`.`id_prixLit_Annee` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_recommandations`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre` | `bigint(20) unsigned` | NO | `` | PK; FK → `livres`.`id_livre` |
| `id_recommandation` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `recommandations`.`id_recommandation` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_staging`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre_staging` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_livres_staging)` | PK |
| `code_livre_staging` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `id_source_import` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `ref_enum`.`id_enum` |
| `id_calibre` | `bigint(20) unsigned` | YES | `NULL` | INDEX |
| `url_source` | `varchar(600)` | YES | `NULL` |  |
| `date_import` | `datetime` | NO | `current_timestamp()` |  |
| `titre_source` | `varchar(500)` | NO | `` |  |
| `avec_fichier` | `tinyint(1)` | NO | `0` |  |
| `titre_normalise` | `varchar(500)` | YES | `NULL` | INDEX |
| `langue_source` | `varchar(80)` | YES | `NULL` |  |
| `editeur_source` | `varchar(255)` | YES | `NULL` |  |
| `isbn_source` | `varchar(20)` | YES | `NULL` |  |
| `annee_publication_source` | `int(11)` | YES | `NULL` |  |
| `date_publication_source` | `date` | YES | `NULL` |  |
| `synopsis_source` | `text` | YES | `NULL` |  |
| `tags_source` | `text` | YES | `NULL` |  |
| `serie_source` | `varchar(255)` | YES | `NULL` |  |
| `id_serie` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `series`.`id_serie` |
| `id_impression` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `impression`.`id_impression` |
| `num_tome_source` | `decimal(6,2)` | YES | `NULL` |  |
| `tome_libelle_source` | `varchar(50)` | YES | `NULL` |  |
| `id_statut_staging` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `ref_enum`.`id_enum` |
| `commentaire_staging` | `varchar(500)` | YES | `NULL` |  |
| `id_livre_cible` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `livres`.`id_livre` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_staging_auteurs`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre_staging` | `bigint(20) unsigned` | NO | `` | PK; FK → `livres_staging`.`id_livre_staging` |
| `id_auteur` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `auteurs`.`id_auteur` |
| `id_role_auteur` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `ref_enum`.`id_enum` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_staging_recommandations`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre_staging` | `bigint(20) unsigned` | NO | `` | PK; FK → `livres_staging`.`id_livre_staging` |
| `id_recommandation` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `recommandations`.`id_recommandation` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `livres_tags`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_livre` | `bigint(20) unsigned` | NO | `` | PK; FK → `livres`.`id_livre` |
| `id_tag` | `bigint(20) unsigned` | NO | `` | PK; INDEX; FK → `tags`.`id_tag` |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |

## `meta_schema`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id` | `tinyint(4)` | NO | `` | PK |
| `schema_version` | `int(11)` | NO | `` |  |
| `applied_at` | `datetime` | NO | `` |  |
| `notes` | `varchar(255)` | YES | `NULL` |  |

## `param_api`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_param_api` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_param_api)` | PK |
| `code_param_api` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `env_code` | `varchar(20)` | NO | `'LOCAL'` | UNIQUE |
| `service_code` | `varchar(40)` | NO | `` | UNIQUE |
| `service_libelle` | `varchar(100)` | YES | `NULL` |  |
| `base_url` | `varchar(600)` | YES | `NULL` |  |
| `api_key_enc` | `blob` | YES | `NULL` |  |
| `api_key_hint` | `varchar(255)` | YES | `NULL` |  |
| `options_json` | `text` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` | INDEX |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `param_db`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_param_db` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_param_db)` | PK |
| `code_param_db` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `env_code` | `varchar(20)` | NO | `'LOCAL'` | UNIQUE |
| `nom_connexion` | `varchar(60)` | NO | `` | UNIQUE |
| `type_db` | `varchar(20)` | NO | `` | INDEX |
| `host` | `varchar(255)` | YES | `NULL` |  |
| `port` | `int(11)` | YES | `NULL` |  |
| `nom_base` | `varchar(255)` | YES | `NULL` |  |
| `user_name` | `varchar(255)` | YES | `NULL` |  |
| `password_hint` | `varchar(255)` | YES | `NULL` |  |
| `password_enc` | `blob` | YES | `NULL` |  |
| `options_conn` | `text` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` | INDEX |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `param_paths`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_param_path` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_param_paths)` | PK |
| `code_param_path` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `env_code` | `varchar(20)` | NO | `'LOCAL'` | UNIQUE |
| `cle_path` | `varchar(80)` | NO | `` | UNIQUE |
| `valeur_path` | `varchar(1000)` | NO | `` |  |
| `description` | `varchar(255)` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` | INDEX |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `pays`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_pays` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_pays)` | PK |
| `nom_pays` | `varchar(150)` | NO | `` | UNIQUE |
| `iso2` | `char(2)` | YES | `NULL` | INDEX |
| `iso3` | `char(3)` | YES | `NULL` | INDEX |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_pays` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `prixlit`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_prixLit` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_prixlit)` | PK |
| `nom_prixLit` | `varchar(200)` | NO | `` | UNIQUE |
| `description_prixLit` | `varchar(200)` | YES | `NULL` |  |
| `Notes_PrixLit_txt` | `text` | YES | `NULL` |  |
| `Notes_PrixLit_rtf` | `text` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_prixLit` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `prixlit_annee`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_prixLit_Annee` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_prixlit_annee)` | PK |
| `id_prixlit_categorie` | `bigint(20) unsigned` | NO | `` | UNIQUE; INDEX; FK → `prixlit_categorie`.`id_prixlit_categorie` |
| `annee` | `smallint(6)` | NO | `` | UNIQUE; INDEX |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_prixLit_Annee` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `prixlit_categorie`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_prixlit_categorie` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_prixlit_categorie)` | PK; UNIQUE |
| `id_prixLit` | `bigint(20) unsigned` | NO | `` | UNIQUE; INDEX; FK → `prixlit`.`id_prixLit` |
| `libelle_categorie` | `varchar(200)` | NO | `` | UNIQUE |
| `description_categorie` | `varchar(200)` | YES | `NULL` |  |
| `ordre_affichage` | `int(11)` | NO | `0` | INDEX |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_prixlit_categorie` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `recommandations`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_recommandation` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_recommandations)` | PK |
| `code_recommandation` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `id_origine_recommandation` | `bigint(20) unsigned` | NO | `` | INDEX; FK → `ref_origine_recommandation`.`id_origine_recommandation` |
| `source_nom` | `varchar(150)` | YES | `NULL` |  |
| `source_login` | `varchar(150)` | YES | `NULL` |  |
| `source_url` | `varchar(500)` | YES | `NULL` |  |
| `date_recommandation` | `date` | YES | `NULL` |  |
| `commentaire_rtf` | `mediumtext` | YES | `NULL` |  |
| `commentaire_txt` | `text` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `ref_enum`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_enum` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_ref_enum)` | PK |
| `code_enum` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `id_enum_type` | `bigint(20) unsigned` | NO | `` | UNIQUE; FK → `ref_enum_type`.`id_enum_type` |
| `code_valeur` | `varchar(40)` | NO | `` | UNIQUE |
| `libelle_valeur` | `varchar(120)` | NO | `` | INDEX |
| `ordre_affichage` | `int(11)` | NO | `0` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `ref_enum_type`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_enum_type` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_ref_enum_type)` | PK |
| `code_enum_type` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `code_type` | `varchar(60)` | NO | `` | UNIQUE |
| `libelle_type` | `varchar(120)` | NO | `` |  |
| `ordre_affichage` | `int(11)` | NO | `0` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `ref_origine_recommandation`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_origine_recommandation` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_ref_origine_recommandation)` | PK |
| `code_origine_recommandation` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
| `libelle_origine_recommandation` | `varchar(100)` | NO | `` |  |
| `ordre_affichage` | `int(11)` | NO | `0` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |

## `series`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_serie` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_series)` | PK |
| `id_series_format` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `series_format`.`id_series_format` |
| `id_series_statut` | `bigint(20) unsigned` | YES | `NULL` | INDEX; FK → `series_statut`.`id_series_statut` |
| `id_format` | `int(11)` | YES | `NULL` | INDEX |
| `id_statut` | `int(11)` | YES | `NULL` | INDEX |
| `nom_serie` | `varchar(200)` | NO | `` | UNIQUE |
| `serie_normalise` | `varchar(220)` | YES | `NULL` | INDEX |
| `nombre_tomes_total` | `int(11)` | YES | `NULL` |  |
| `prochain_tome_numero` | `int(11)` | YES | `NULL` |  |
| `prochain_tome_date` | `date` | YES | `NULL` |  |
| `pitch_serie` | `text` | YES | `NULL` |  |
| `is_terminee` | `tinyint(1)` | NO | `0` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_serie` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `series_format`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_series_format` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_series_format)` | PK |
| `libelle_format` | `varchar(80)` | NO | `` | UNIQUE |
| `nb_tomes_ref` | `tinyint(4)` | YES | `NULL` |  |
| `ordre_affichage` | `int(11)` | NO | `0` | INDEX |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_series_format` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `series_statut`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_series_statut` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_series_statut)` | PK |
| `libelle_statut` | `varchar(80)` | NO | `` | UNIQUE |
| `est_final` | `tinyint(1)` | NO | `0` |  |
| `ordre_affichage` | `int(11)` | NO | `0` | INDEX |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_series_statut` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |

## `tags`

| Colonne | Type | Null | Default | Clé/Contrainte |
|---|---|---|---|---|
| `id_tag` | `bigint(20) unsigned` | NO | `nextval(artefact.seq_tags)` | PK |
| `libelle_tag` | `varchar(180)` | NO | `` | UNIQUE; INDEX |
| `type_tag` | `enum('Genre','Etiquette','Theme','Autre')` | NO | `'Etiquette'` | UNIQUE; INDEX |
| `source_tag` | `varchar(60)` | NO | `'Artefact'` | UNIQUE; INDEX |
| `couleur_tag` | `varchar(20)` | YES | `NULL` |  |
| `mapping_tag` | `varchar(600)` | YES | `NULL` |  |
| `is_actif` | `tinyint(1)` | NO | `1` |  |
| `created_at` | `datetime` | NO | `current_timestamp()` |  |
| `updated_at` | `datetime` | NO | `current_timestamp()` |  |
| `code_tag` | `varchar(12)` | YES | `` | UNIQUE; GENERATED |
