/*M!999999\- enable the sandbox mode */ 
-- MariaDB dump 10.19-12.1.2-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: artefact
-- ------------------------------------------------------
-- Server version	12.1.2-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*M!100616 SET @OLD_NOTE_VERBOSITY=@@NOTE_VERBOSITY, NOTE_VERBOSITY=0 */;

--
-- Sequence structure for `seq_auteurs`
--

DROP SEQUENCE IF EXISTS `seq_auteurs`;
CREATE SEQUENCE `seq_auteurs` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_auteurs`, 1, 0);

--
-- Sequence structure for `seq_contacts`
--

DROP SEQUENCE IF EXISTS `seq_contacts`;
CREATE SEQUENCE `seq_contacts` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_contacts`, 8, 0);

--
-- Sequence structure for `seq_editeurs`
--

DROP SEQUENCE IF EXISTS `seq_editeurs`;
CREATE SEQUENCE `seq_editeurs` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_editeurs`, 14, 0);

--
-- Sequence structure for `seq_formatfile`
--

DROP SEQUENCE IF EXISTS `seq_formatfile`;
CREATE SEQUENCE `seq_formatfile` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_formatfile`, 10, 0);

--
-- Sequence structure for `seq_impression`
--

DROP SEQUENCE IF EXISTS `seq_impression`;
CREATE SEQUENCE `seq_impression` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_impression`, 14, 0);

--
-- Sequence structure for `seq_langues`
--

DROP SEQUENCE IF EXISTS `seq_langues`;
CREATE SEQUENCE `seq_langues` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_langues`, 9, 0);

--
-- Sequence structure for `seq_livres`
--

DROP SEQUENCE IF EXISTS `seq_livres`;
CREATE SEQUENCE `seq_livres` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_livres`, 1, 0);

--
-- Sequence structure for `seq_livres_fichiers`
--

DROP SEQUENCE IF EXISTS `seq_livres_fichiers`;
CREATE SEQUENCE `seq_livres_fichiers` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_livres_fichiers`, 1, 0);

--
-- Sequence structure for `seq_livres_staging`
--

DROP SEQUENCE IF EXISTS `seq_livres_staging`;
CREATE SEQUENCE `seq_livres_staging` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_livres_staging`, 1, 0);

--
-- Sequence structure for `seq_param_api`
--

DROP SEQUENCE IF EXISTS `seq_param_api`;
CREATE SEQUENCE `seq_param_api` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_param_api`, 4, 0);

--
-- Sequence structure for `seq_param_db`
--

DROP SEQUENCE IF EXISTS `seq_param_db`;
CREATE SEQUENCE `seq_param_db` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_param_db`, 3, 0);

--
-- Sequence structure for `seq_param_paths`
--

DROP SEQUENCE IF EXISTS `seq_param_paths`;
CREATE SEQUENCE `seq_param_paths` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_param_paths`, 5, 0);

--
-- Sequence structure for `seq_pays`
--

DROP SEQUENCE IF EXISTS `seq_pays`;
CREATE SEQUENCE `seq_pays` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_pays`, 8, 0);

--
-- Sequence structure for `seq_prixlit`
--

DROP SEQUENCE IF EXISTS `seq_prixlit`;
CREATE SEQUENCE `seq_prixlit` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_prixlit`, 4, 0);

--
-- Sequence structure for `seq_prixlit_annee`
--

DROP SEQUENCE IF EXISTS `seq_prixlit_annee`;
CREATE SEQUENCE `seq_prixlit_annee` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_prixlit_annee`, 4, 0);

--
-- Sequence structure for `seq_prixlit_categorie`
--

DROP SEQUENCE IF EXISTS `seq_prixlit_categorie`;
CREATE SEQUENCE `seq_prixlit_categorie` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_prixlit_categorie`, 17, 0);

--
-- Sequence structure for `seq_recommandations`
--

DROP SEQUENCE IF EXISTS `seq_recommandations`;
CREATE SEQUENCE `seq_recommandations` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_recommandations`, 17, 0);

--
-- Sequence structure for `seq_ref_enum`
--

DROP SEQUENCE IF EXISTS `seq_ref_enum`;
CREATE SEQUENCE `seq_ref_enum` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_ref_enum`, 39, 0);

--
-- Sequence structure for `seq_ref_enum_type`
--

DROP SEQUENCE IF EXISTS `seq_ref_enum_type`;
CREATE SEQUENCE `seq_ref_enum_type` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_ref_enum_type`, 14, 0);

--
-- Sequence structure for `seq_ref_origine_recommandation`
--

DROP SEQUENCE IF EXISTS `seq_ref_origine_recommandation`;
CREATE SEQUENCE `seq_ref_origine_recommandation` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_ref_origine_recommandation`, 11, 0);

--
-- Sequence structure for `seq_series`
--

DROP SEQUENCE IF EXISTS `seq_series`;
CREATE SEQUENCE `seq_series` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_series`, 1, 0);

--
-- Sequence structure for `seq_series_format`
--

DROP SEQUENCE IF EXISTS `seq_series_format`;
CREATE SEQUENCE `seq_series_format` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_series_format`, 22, 0);

--
-- Sequence structure for `seq_series_statut`
--

DROP SEQUENCE IF EXISTS `seq_series_statut`;
CREATE SEQUENCE `seq_series_statut` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_series_statut`, 6, 0);

--
-- Sequence structure for `seq_tags`
--

DROP SEQUENCE IF EXISTS `seq_tags`;
CREATE SEQUENCE `seq_tags` start with 1 minvalue 1 maxvalue 9223372036854775806 increment by 1 cache 1 nocycle ENGINE=InnoDB;
DO SETVAL(`seq_tags`, 1, 0);

--
-- Table structure for table `auteurs`
--

DROP TABLE IF EXISTS `auteurs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `auteurs` (
  `id_auteur` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_auteurs`),
  `nom_auteur` varchar(150) NOT NULL,
  `prenom_auteur` varchar(150) DEFAULT NULL,
  `nomcomplet_auteur` varchar(320) NOT NULL,
  `id_pays` bigint(20) unsigned DEFAULT NULL,
  `id_langue_ecriture` bigint(20) unsigned DEFAULT NULL,
  `date_naissance` date DEFAULT NULL,
  `annee_naissance` smallint(6) DEFAULT NULL,
  `biographie` text DEFAULT NULL,
  `site_web` varchar(300) DEFAULT NULL,
  `chemin_photo_auteur` varchar(600) DEFAULT NULL,
  `nom_normalise` varchar(160) DEFAULT NULL,
  `prenom_normalise` varchar(160) DEFAULT NULL,
  `nomcomplet_normalise` varchar(340) DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_auteur` varchar(12) GENERATED ALWAYS AS (concat('A',lpad(`id_auteur`,6,'0'))) STORED,
  PRIMARY KEY (`id_auteur`),
  UNIQUE KEY `uq_auteurs_code` (`code_auteur`),
  KEY `idx_auteurs_nom` (`nom_auteur`),
  KEY `idx_auteurs_nomcomplet` (`nomcomplet_auteur`),
  KEY `idx_auteurs_pays` (`id_pays`),
  KEY `idx_auteurs_langue` (`id_langue_ecriture`),
  CONSTRAINT `fk_auteurs_langues` FOREIGN KEY (`id_langue_ecriture`) REFERENCES `langues` (`id_langue`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_auteurs_pays` FOREIGN KEY (`id_pays`) REFERENCES `pays` (`id_pays`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_uca1400_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER trg_auteurs_set_annee_naissance_bi
BEFORE INSERT ON auteurs
FOR EACH ROW
SET NEW.annee_naissance = IF(NEW.date_naissance IS NULL, NEW.annee_naissance, YEAR(NEW.date_naissance)) */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_uca1400_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER trg_auteurs_set_annee_naissance_bu
BEFORE UPDATE ON auteurs
FOR EACH ROW
SET NEW.annee_naissance = IF(NEW.date_naissance IS NULL, NEW.annee_naissance, YEAR(NEW.date_naissance)) */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `auteurs_pays`
--

DROP TABLE IF EXISTS `auteurs_pays`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `auteurs_pays` (
  `id_auteur` bigint(20) unsigned NOT NULL,
  `id_pays` bigint(20) unsigned NOT NULL,
  `id_type_relation_pays` bigint(20) unsigned NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_auteur`,`id_pays`,`id_type_relation_pays`),
  KEY `idx_ap_auteur` (`id_auteur`),
  KEY `idx_ap_pays` (`id_pays`),
  KEY `idx_ap_type` (`id_type_relation_pays`),
  CONSTRAINT `fk_ap_auteur` FOREIGN KEY (`id_auteur`) REFERENCES `auteurs` (`id_auteur`),
  CONSTRAINT `fk_ap_pays` FOREIGN KEY (`id_pays`) REFERENCES `pays` (`id_pays`),
  CONSTRAINT `fk_ap_type` FOREIGN KEY (`id_type_relation_pays`) REFERENCES `ref_enum` (`id_enum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `contacts`
--

DROP TABLE IF EXISTS `contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `contacts` (
  `id_contact` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_contacts`),
  `nom_contact` varchar(200) NOT NULL,
  `email_perso` varchar(254) DEFAULT NULL,
  `adresse_liseuse` varchar(254) DEFAULT NULL,
  `type_liseuse` varchar(100) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_contact` varchar(12) GENERATED ALWAYS AS (concat('C',lpad(`id_contact`,6,'0'))) STORED,
  PRIMARY KEY (`id_contact`),
  UNIQUE KEY `uq_contacts_nom` (`nom_contact`),
  UNIQUE KEY `uq_contacts_code` (`code_contact`),
  KEY `idx_contacts_email` (`email_perso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `editeurs`
--

DROP TABLE IF EXISTS `editeurs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `editeurs` (
  `id_editeur` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_editeurs`),
  `nom_editeur` varchar(200) NOT NULL,
  `id_pays` bigint(20) unsigned DEFAULT NULL,
  `site_web` varchar(300) DEFAULT NULL,
  `notes_editeur_rtf` mediumtext DEFAULT NULL,
  `notes_editeur_txt` text DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_editeur` varchar(12) GENERATED ALWAYS AS (concat('E',lpad(`id_editeur`,6,'0'))) STORED,
  PRIMARY KEY (`id_editeur`),
  UNIQUE KEY `uq_editeurs_nom` (`nom_editeur`),
  UNIQUE KEY `uq_editeurs_code` (`code_editeur`),
  KEY `idx_editeurs_pays` (`id_pays`),
  CONSTRAINT `fk_editeurs_pays` FOREIGN KEY (`id_pays`) REFERENCES `pays` (`id_pays`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `formatfile`
--

DROP TABLE IF EXISTS `formatfile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `formatfile` (
  `id_formatFile` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_formatfile`),
  `nom_format` varchar(40) NOT NULL,
  `extension` varchar(10) DEFAULT NULL,
  `mime_type` varchar(100) DEFAULT NULL,
  `ordre_affichage` int(11) NOT NULL DEFAULT 1,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_formatFile` varchar(12) GENERATED ALWAYS AS (concat('F',lpad(`id_formatFile`,6,'0'))) STORED,
  PRIMARY KEY (`id_formatFile`),
  UNIQUE KEY `uq_formatFile_nom` (`nom_format`),
  UNIQUE KEY `uq_formatFile_code` (`code_formatFile`),
  KEY `idx_formatFile_ext` (`extension`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `impression`
--

DROP TABLE IF EXISTS `impression`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `impression` (
  `id_impression` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_impression`),
  `code_impression` varchar(12) GENERATED ALWAYS AS (concat('I',lpad(`id_impression`,6,'0'))) STORED,
  `nom_impression` varchar(120) NOT NULL,
  `description_impression` varchar(400) DEFAULT NULL,
  `note_rtf` mediumtext DEFAULT NULL,
  `note_txt` text DEFAULT NULL,
  `envie_Cal` varchar(10) DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_impression`),
  UNIQUE KEY `uq_impression_nom` (`nom_impression`),
  UNIQUE KEY `uq_impression_code` (`code_impression`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `langues`
--

DROP TABLE IF EXISTS `langues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `langues` (
  `id_langue` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_langues`),
  `nom_langue` varchar(120) NOT NULL,
  `abrev_langue` varchar(10) NOT NULL,
  `iso639_1` char(2) DEFAULT NULL,
  `iso639_2` char(3) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_langue` varchar(12) GENERATED ALWAYS AS (concat('L',lpad(`id_langue`,6,'0'))) STORED,
  PRIMARY KEY (`id_langue`),
  UNIQUE KEY `uq_langues_nom` (`nom_langue`),
  UNIQUE KEY `uq_langues_abrev` (`abrev_langue`),
  UNIQUE KEY `uq_langues_code` (`code_langue`),
  KEY `idx_langues_iso1` (`iso639_1`),
  KEY `idx_langues_iso2` (`iso639_2`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres`
--

DROP TABLE IF EXISTS `livres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres` (
  `id_livre` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_livres`),
  `id_calibre` bigint(20) unsigned DEFAULT NULL,
  `code_livre` varchar(12) GENERATED ALWAYS AS (concat('B',lpad(`id_livre`,6,'0'))) STORED,
  `titre` varchar(350) NOT NULL,
  `titre_normalise` varchar(380) DEFAULT NULL,
  `annee_publication` smallint(6) DEFAULT NULL,
  `date_publication` date DEFAULT NULL,
  `synopsis` text DEFAULT NULL,
  `commentaire` text DEFAULT NULL,
  `id_langue` bigint(20) unsigned DEFAULT NULL,
  `id_impression` bigint(20) unsigned DEFAULT NULL,
  `id_editeur` bigint(20) unsigned DEFAULT NULL,
  `id_serie` bigint(20) unsigned DEFAULT NULL,
  `num_tome` decimal(6,2) DEFAULT NULL,
  `tome_libelle` varchar(50) DEFAULT NULL,
  `id_statut_lecture` bigint(20) unsigned DEFAULT NULL,
  `id_support_lecture` bigint(20) unsigned DEFAULT NULL,
  `id_type_acquisition` bigint(20) unsigned DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_livre`),
  UNIQUE KEY `uq_livres_code` (`code_livre`),
  KEY `idx_livres_titre` (`titre`),
  KEY `idx_livres_titre_norm` (`titre_normalise`),
  KEY `idx_livres_annee` (`annee_publication`),
  KEY `idx_livres_datepub` (`date_publication`),
  KEY `idx_livres_langue` (`id_langue`),
  KEY `idx_livres_impression` (`id_impression`),
  KEY `idx_livres_editeur` (`id_editeur`),
  KEY `idx_livres_statut` (`id_statut_lecture`),
  KEY `idx_livres_support` (`id_support_lecture`),
  KEY `idx_livres_acq` (`id_type_acquisition`),
  KEY `idx_livres_id_calibre` (`id_calibre`),
  KEY `idx_livres_serie` (`id_serie`),
  KEY `idx_livres_serie_tome` (`id_serie`,`num_tome`),
  CONSTRAINT `fk_livres_editeur` FOREIGN KEY (`id_editeur`) REFERENCES `editeurs` (`id_editeur`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_livres_impression` FOREIGN KEY (`id_impression`) REFERENCES `impression` (`id_impression`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_livres_langue` FOREIGN KEY (`id_langue`) REFERENCES `langues` (`id_langue`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_livres_serie` FOREIGN KEY (`id_serie`) REFERENCES `series` (`id_serie`),
  CONSTRAINT `fk_livres_statut_lecture` FOREIGN KEY (`id_statut_lecture`) REFERENCES `ref_enum` (`id_enum`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_livres_support_lecture` FOREIGN KEY (`id_support_lecture`) REFERENCES `ref_enum` (`id_enum`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_livres_type_acquisition` FOREIGN KEY (`id_type_acquisition`) REFERENCES `ref_enum` (`id_enum`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_auteurs`
--

DROP TABLE IF EXISTS `livres_auteurs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_auteurs` (
  `id_livre` bigint(20) unsigned NOT NULL,
  `id_auteur` bigint(20) unsigned NOT NULL,
  `id_role_auteur` bigint(20) unsigned NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_livre`,`id_auteur`,`id_role_auteur`),
  KEY `idx_la_auteur` (`id_auteur`),
  KEY `idx_la_role` (`id_role_auteur`),
  CONSTRAINT `fk_la_auteur` FOREIGN KEY (`id_auteur`) REFERENCES `auteurs` (`id_auteur`),
  CONSTRAINT `fk_la_livre` FOREIGN KEY (`id_livre`) REFERENCES `livres` (`id_livre`),
  CONSTRAINT `fk_la_role` FOREIGN KEY (`id_role_auteur`) REFERENCES `ref_enum` (`id_enum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_contacts`
--

DROP TABLE IF EXISTS `livres_contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_contacts` (
  `id_livre` bigint(20) unsigned NOT NULL,
  `id_contact` bigint(20) unsigned NOT NULL,
  `id_livre_fichier` bigint(20) unsigned DEFAULT NULL,
  `date_envoi` datetime DEFAULT NULL,
  `commentaire` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_livre`,`id_contact`),
  KEY `idx_lc_contact` (`id_contact`),
  KEY `idx_lc_fichier` (`id_livre_fichier`),
  CONSTRAINT `fk_lc_contact` FOREIGN KEY (`id_contact`) REFERENCES `contacts` (`id_contact`),
  CONSTRAINT `fk_lc_livre` FOREIGN KEY (`id_livre`) REFERENCES `livres` (`id_livre`),
  CONSTRAINT `fk_lc_livre_fichier` FOREIGN KEY (`id_livre_fichier`) REFERENCES `livres_fichiers` (`id_livre_fichier`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_fichiers`
--

DROP TABLE IF EXISTS `livres_fichiers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_fichiers` (
  `id_livre_fichier` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_livres_fichiers`),
  `code_livre_fichier` varchar(12) GENERATED ALWAYS AS (concat('LF',lpad(`id_livre_fichier`,6,'0'))) STORED,
  `id_scope_livre` bigint(20) unsigned NOT NULL,
  `id_livre` bigint(20) unsigned DEFAULT NULL,
  `id_livre_staging` bigint(20) unsigned DEFAULT NULL,
  `id_type_fichier` bigint(20) unsigned NOT NULL,
  `id_formatFile` bigint(20) unsigned DEFAULT NULL,
  `chemin_fichier` varchar(800) NOT NULL,
  `nom_fichier` varchar(255) DEFAULT NULL,
  `extension_source` varchar(20) DEFAULT NULL,
  `taille_octets` bigint(20) unsigned DEFAULT NULL,
  `hash_sha1` char(40) DEFAULT NULL,
  `is_principal` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_livre_fichier`),
  UNIQUE KEY `uq_livres_fichiers_code` (`code_livre_fichier`),
  UNIQUE KEY `uq_lf_dedup` (`id_scope_livre`,`id_livre`,`id_livre_staging`,`id_type_fichier`,`id_formatFile`,`chemin_fichier`) USING HASH,
  KEY `idx_lf_scope` (`id_scope_livre`),
  KEY `idx_lf_type` (`id_type_fichier`),
  KEY `idx_lf_format` (`id_formatFile`),
  KEY `idx_lf_livre` (`id_livre`),
  KEY `idx_lf_livre_staging` (`id_livre_staging`),
  CONSTRAINT `fk_lf_format` FOREIGN KEY (`id_formatFile`) REFERENCES `formatfile` (`id_formatFile`),
  CONSTRAINT `fk_lf_livre` FOREIGN KEY (`id_livre`) REFERENCES `livres` (`id_livre`),
  CONSTRAINT `fk_lf_livre_staging` FOREIGN KEY (`id_livre_staging`) REFERENCES `livres_staging` (`id_livre_staging`),
  CONSTRAINT `fk_lf_scope` FOREIGN KEY (`id_scope_livre`) REFERENCES `ref_enum` (`id_enum`),
  CONSTRAINT `fk_lf_type` FOREIGN KEY (`id_type_fichier`) REFERENCES `ref_enum` (`id_enum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_prixlit_annee`
--

DROP TABLE IF EXISTS `livres_prixlit_annee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_prixlit_annee` (
  `id_livre` bigint(20) unsigned NOT NULL,
  `id_prixLit_Annee` bigint(20) unsigned NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_livre`,`id_prixLit_Annee`),
  KEY `idx_lpa_prixannee` (`id_prixLit_Annee`),
  CONSTRAINT `fk_lpa_livre` FOREIGN KEY (`id_livre`) REFERENCES `livres` (`id_livre`),
  CONSTRAINT `fk_lpa_prixannee` FOREIGN KEY (`id_prixLit_Annee`) REFERENCES `prixlit_annee` (`id_prixLit_Annee`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_recommandations`
--

DROP TABLE IF EXISTS `livres_recommandations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_recommandations` (
  `id_livre` bigint(20) unsigned NOT NULL,
  `id_recommandation` bigint(20) unsigned NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_livre`,`id_recommandation`),
  KEY `idx_lr_recommandation` (`id_recommandation`),
  CONSTRAINT `fk_lr_livre` FOREIGN KEY (`id_livre`) REFERENCES `livres` (`id_livre`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_lr_recommandation` FOREIGN KEY (`id_recommandation`) REFERENCES `recommandations` (`id_recommandation`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_staging`
--

DROP TABLE IF EXISTS `livres_staging`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_staging` (
  `id_livre_staging` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_livres_staging`),
  `code_livre_staging` varchar(12) GENERATED ALWAYS AS (concat('BS',lpad(`id_livre_staging`,6,'0'))) STORED,
  `id_source_import` bigint(20) unsigned DEFAULT NULL,
  `id_calibre` bigint(20) unsigned DEFAULT NULL,
  `url_source` varchar(600) DEFAULT NULL,
  `date_import` datetime NOT NULL DEFAULT current_timestamp(),
  `titre_source` varchar(500) NOT NULL,
  `avec_fichier` tinyint(1) NOT NULL DEFAULT 0,
  `titre_normalise` varchar(500) DEFAULT NULL,
  `langue_source` varchar(80) DEFAULT NULL,
  `editeur_source` varchar(255) DEFAULT NULL,
  `isbn_source` varchar(20) DEFAULT NULL,
  `annee_publication_source` int(11) DEFAULT NULL,
  `date_publication_source` date DEFAULT NULL,
  `synopsis_source` text DEFAULT NULL,
  `tags_source` text DEFAULT NULL,
  `serie_source` varchar(255) DEFAULT NULL,
  `id_serie` bigint(20) unsigned DEFAULT NULL,
  `id_impression` bigint(20) unsigned DEFAULT NULL,
  `num_tome_source` decimal(6,2) DEFAULT NULL,
  `tome_libelle_source` varchar(50) DEFAULT NULL,
  `id_statut_staging` bigint(20) unsigned DEFAULT NULL,
  `commentaire_staging` varchar(500) DEFAULT NULL,
  `id_livre_cible` bigint(20) unsigned DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_livre_staging`),
  UNIQUE KEY `uq_livres_staging_code` (`code_livre_staging`),
  KEY `idx_ls_titre_norm` (`titre_normalise`),
  KEY `idx_ls_id_calibre` (`id_calibre`),
  KEY `idx_ls_id_serie` (`id_serie`),
  KEY `idx_ls_id_livre_cible` (`id_livre_cible`),
  KEY `fk_ls_source_import` (`id_source_import`),
  KEY `fk_ls_statut` (`id_statut_staging`),
  KEY `fk_ls_impression` (`id_impression`),
  CONSTRAINT `fk_ls_impression` FOREIGN KEY (`id_impression`) REFERENCES `impression` (`id_impression`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_ls_livre_cible` FOREIGN KEY (`id_livre_cible`) REFERENCES `livres` (`id_livre`),
  CONSTRAINT `fk_ls_serie` FOREIGN KEY (`id_serie`) REFERENCES `series` (`id_serie`),
  CONSTRAINT `fk_ls_source_import` FOREIGN KEY (`id_source_import`) REFERENCES `ref_enum` (`id_enum`),
  CONSTRAINT `fk_ls_statut` FOREIGN KEY (`id_statut_staging`) REFERENCES `ref_enum` (`id_enum`),
  CONSTRAINT `chk_livres_staging_avec_fichier` CHECK (`avec_fichier` in (0,1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_staging_auteurs`
--

DROP TABLE IF EXISTS `livres_staging_auteurs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_staging_auteurs` (
  `id_livre_staging` bigint(20) unsigned NOT NULL,
  `id_auteur` bigint(20) unsigned NOT NULL,
  `id_role_auteur` bigint(20) unsigned NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_livre_staging`,`id_auteur`,`id_role_auteur`),
  KEY `idx_lsa_auteur` (`id_auteur`),
  KEY `idx_lsa_role` (`id_role_auteur`),
  CONSTRAINT `fk_lsa_auteur` FOREIGN KEY (`id_auteur`) REFERENCES `auteurs` (`id_auteur`),
  CONSTRAINT `fk_lsa_role` FOREIGN KEY (`id_role_auteur`) REFERENCES `ref_enum` (`id_enum`),
  CONSTRAINT `fk_lsa_staging` FOREIGN KEY (`id_livre_staging`) REFERENCES `livres_staging` (`id_livre_staging`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_staging_recommandations`
--

DROP TABLE IF EXISTS `livres_staging_recommandations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_staging_recommandations` (
  `id_livre_staging` bigint(20) unsigned NOT NULL,
  `id_recommandation` bigint(20) unsigned NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_livre_staging`,`id_recommandation`),
  KEY `idx_lsr_recommandation` (`id_recommandation`),
  CONSTRAINT `fk_lsr_livre_staging` FOREIGN KEY (`id_livre_staging`) REFERENCES `livres_staging` (`id_livre_staging`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_lsr_recommandation` FOREIGN KEY (`id_recommandation`) REFERENCES `recommandations` (`id_recommandation`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `livres_tags`
--

DROP TABLE IF EXISTS `livres_tags`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_tags` (
  `id_livre` bigint(20) unsigned NOT NULL,
  `id_tag` bigint(20) unsigned NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_livre`,`id_tag`),
  KEY `idx_lt_tag` (`id_tag`),
  CONSTRAINT `fk_lt_livre` FOREIGN KEY (`id_livre`) REFERENCES `livres` (`id_livre`),
  CONSTRAINT `fk_lt_tag` FOREIGN KEY (`id_tag`) REFERENCES `tags` (`id_tag`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `meta_schema`
--

DROP TABLE IF EXISTS `meta_schema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `meta_schema` (
  `id` tinyint(4) NOT NULL,
  `schema_version` int(11) NOT NULL,
  `applied_at` datetime NOT NULL,
  `notes` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `param_api`
--

DROP TABLE IF EXISTS `param_api`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `param_api` (
  `id_param_api` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_param_api`),
  `code_param_api` varchar(12) GENERATED ALWAYS AS (concat('AP',lpad(`id_param_api`,6,'0'))) STORED,
  `env_code` varchar(20) NOT NULL DEFAULT 'LOCAL',
  `service_code` varchar(40) NOT NULL,
  `service_libelle` varchar(100) DEFAULT NULL,
  `base_url` varchar(600) DEFAULT NULL,
  `api_key_enc` blob DEFAULT NULL,
  `api_key_hint` varchar(255) DEFAULT NULL,
  `options_json` text DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_param_api`),
  UNIQUE KEY `uq_param_api_env_service` (`env_code`,`service_code`),
  UNIQUE KEY `uq_param_api_code` (`code_param_api`),
  KEY `idx_param_api_actif` (`is_actif`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `param_db`
--

DROP TABLE IF EXISTS `param_db`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `param_db` (
  `id_param_db` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_param_db`),
  `code_param_db` varchar(12) GENERATED ALWAYS AS (concat('DB',lpad(`id_param_db`,6,'0'))) STORED,
  `env_code` varchar(20) NOT NULL DEFAULT 'LOCAL',
  `nom_connexion` varchar(60) NOT NULL,
  `type_db` varchar(20) NOT NULL,
  `host` varchar(255) DEFAULT NULL,
  `port` int(11) DEFAULT NULL,
  `nom_base` varchar(255) DEFAULT NULL,
  `user_name` varchar(255) DEFAULT NULL,
  `password_hint` varchar(255) DEFAULT NULL,
  `password_enc` blob DEFAULT NULL,
  `options_conn` text DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_param_db`),
  UNIQUE KEY `uq_param_db_env_nom` (`env_code`,`nom_connexion`),
  UNIQUE KEY `uq_param_db_code` (`code_param_db`),
  KEY `idx_param_db_type` (`type_db`),
  KEY `idx_param_db_actif` (`is_actif`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `param_paths`
--

DROP TABLE IF EXISTS `param_paths`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `param_paths` (
  `id_param_path` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_param_paths`),
  `code_param_path` varchar(12) GENERATED ALWAYS AS (concat('PA',lpad(`id_param_path`,6,'0'))) STORED,
  `env_code` varchar(20) NOT NULL DEFAULT 'LOCAL',
  `cle_path` varchar(80) NOT NULL,
  `valeur_path` varchar(1000) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_param_path`),
  UNIQUE KEY `uq_param_paths_env_cle` (`env_code`,`cle_path`),
  UNIQUE KEY `uq_param_paths_code` (`code_param_path`),
  KEY `idx_param_paths_actif` (`is_actif`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pays`
--

DROP TABLE IF EXISTS `pays`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `pays` (
  `id_pays` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_pays`),
  `nom_pays` varchar(150) NOT NULL,
  `iso2` char(2) DEFAULT NULL,
  `iso3` char(3) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_pays` varchar(12) GENERATED ALWAYS AS (concat('P',lpad(`id_pays`,6,'0'))) STORED,
  PRIMARY KEY (`id_pays`),
  UNIQUE KEY `uq_pays_nom` (`nom_pays`),
  UNIQUE KEY `uq_pays_code` (`code_pays`),
  KEY `idx_pays_iso2` (`iso2`),
  KEY `idx_pays_iso3` (`iso3`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `prixlit`
--

DROP TABLE IF EXISTS `prixlit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `prixlit` (
  `id_prixLit` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_prixlit`),
  `nom_prixLit` varchar(200) NOT NULL,
  `description_prixLit` varchar(200) DEFAULT NULL,
  `Notes_PrixLit_txt` text DEFAULT NULL,
  `Notes_PrixLit_rtf` text DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_prixLit` varchar(12) GENERATED ALWAYS AS (concat('R',lpad(`id_prixLit`,6,'0'))) STORED,
  PRIMARY KEY (`id_prixLit`),
  UNIQUE KEY `uq_prixLit_nom` (`nom_prixLit`),
  UNIQUE KEY `uq_prixLit_code` (`code_prixLit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `prixlit_annee`
--

DROP TABLE IF EXISTS `prixlit_annee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `prixlit_annee` (
  `id_prixLit_Annee` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_prixlit_annee`),
  `id_prixlit_categorie` bigint(20) unsigned NOT NULL,
  `annee` smallint(6) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_prixLit_Annee` varchar(12) GENERATED ALWAYS AS (concat('RA',lpad(`id_prixLit_Annee`,6,'0'))) STORED,
  PRIMARY KEY (`id_prixLit_Annee`),
  UNIQUE KEY `uq_prixlit_annee_categorie_annee` (`id_prixlit_categorie`,`annee`),
  UNIQUE KEY `uq_prixLit_Annee_code` (`code_prixLit_Annee`),
  KEY `idx_prixLit_Annee_annee` (`annee`),
  KEY `idx_prixlit_annee_categorie` (`id_prixlit_categorie`),
  KEY `idx_prixlit_annee_id_prixlit_categorie` (`id_prixlit_categorie`),
  CONSTRAINT `fk_prixlit_annee_prixlit_categorie` FOREIGN KEY (`id_prixlit_categorie`) REFERENCES `prixlit_categorie` (`id_prixlit_categorie`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `prixlit_categorie`
--

DROP TABLE IF EXISTS `prixlit_categorie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `prixlit_categorie` (
  `id_prixlit_categorie` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_prixlit_categorie`),
  `id_prixLit` bigint(20) unsigned NOT NULL,
  `libelle_categorie` varchar(200) NOT NULL,
  `description_categorie` varchar(200) DEFAULT NULL,
  `ordre_affichage` int(11) NOT NULL DEFAULT 0,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_prixlit_categorie` varchar(12) GENERATED ALWAYS AS (concat('RC',lpad(`id_prixlit_categorie`,6,'0'))) STORED,
  PRIMARY KEY (`id_prixlit_categorie`) USING BTREE,
  UNIQUE KEY `uq_prixlit_categorie_prix_lib` (`id_prixLit`,`libelle_categorie`),
  UNIQUE KEY `uq_prixlit_categorie_idcat_idprix` (`id_prixlit_categorie`,`id_prixLit`),
  UNIQUE KEY `uq_prixlit_categorie_libelle` (`id_prixLit`,`libelle_categorie`),
  UNIQUE KEY `uq_prixlit_categorie_code` (`code_prixlit_categorie`),
  KEY `idx_prixlit_categorie_prix` (`id_prixLit`),
  KEY `idx_prixlit_categorie_ordre` (`ordre_affichage`),
  CONSTRAINT `fk_prixlit_categorie_prixlit` FOREIGN KEY (`id_prixLit`) REFERENCES `prixlit` (`id_prixLit`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recommandations`
--

DROP TABLE IF EXISTS `recommandations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `recommandations` (
  `id_recommandation` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_recommandations`),
  `code_recommandation` varchar(12) GENERATED ALWAYS AS (concat('R',lpad(`id_recommandation`,6,'0'))) STORED,
  `id_origine_recommandation` bigint(20) unsigned NOT NULL,
  `source_nom` varchar(150) DEFAULT NULL,
  `source_login` varchar(150) DEFAULT NULL,
  `source_url` varchar(500) DEFAULT NULL,
  `date_recommandation` date DEFAULT NULL,
  `commentaire_rtf` mediumtext DEFAULT NULL,
  `commentaire_txt` text DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_recommandation`),
  UNIQUE KEY `uq_recommandations_code` (`code_recommandation`),
  KEY `idx_recommandations_origine` (`id_origine_recommandation`),
  CONSTRAINT `fk_recommandations_origine` FOREIGN KEY (`id_origine_recommandation`) REFERENCES `ref_origine_recommandation` (`id_origine_recommandation`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ref_enum`
--

DROP TABLE IF EXISTS `ref_enum`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `ref_enum` (
  `id_enum` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_ref_enum`),
  `code_enum` varchar(12) GENERATED ALWAYS AS (concat('N',lpad(`id_enum`,6,'0'))) STORED,
  `id_enum_type` bigint(20) unsigned NOT NULL,
  `code_valeur` varchar(40) NOT NULL,
  `libelle_valeur` varchar(120) NOT NULL,
  `ordre_affichage` int(11) NOT NULL DEFAULT 0,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_enum`),
  UNIQUE KEY `uq_ref_enum_enumtype_code` (`id_enum_type`,`code_valeur`),
  UNIQUE KEY `uq_ref_enum_code` (`code_enum`),
  KEY `idx_ref_enum_libelle` (`libelle_valeur`),
  CONSTRAINT `fk_ref_enum_type` FOREIGN KEY (`id_enum_type`) REFERENCES `ref_enum_type` (`id_enum_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ref_enum_type`
--

DROP TABLE IF EXISTS `ref_enum_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `ref_enum_type` (
  `id_enum_type` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_ref_enum_type`),
  `code_enum_type` varchar(12) GENERATED ALWAYS AS (concat('ET',lpad(`id_enum_type`,6,'0'))) STORED,
  `code_type` varchar(60) NOT NULL,
  `libelle_type` varchar(120) NOT NULL,
  `ordre_affichage` int(11) NOT NULL DEFAULT 0,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_enum_type`),
  UNIQUE KEY `uq_ref_enum_type_code_type` (`code_type`),
  UNIQUE KEY `uq_ref_enum_type_code` (`code_enum_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ref_origine_recommandation`
--

DROP TABLE IF EXISTS `ref_origine_recommandation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `ref_origine_recommandation` (
  `id_origine_recommandation` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_ref_origine_recommandation`),
  `code_origine_recommandation` varchar(12) GENERATED ALWAYS AS (concat('OR',lpad(`id_origine_recommandation`,6,'0'))) STORED,
  `libelle_origine_recommandation` varchar(100) NOT NULL,
  `ordre_affichage` int(11) NOT NULL DEFAULT 0,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_origine_recommandation`),
  UNIQUE KEY `uq_ref_origine_recommandation_code` (`code_origine_recommandation`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `series`
--

DROP TABLE IF EXISTS `series`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `series` (
  `id_serie` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_series`),
  `id_series_format` bigint(20) unsigned DEFAULT NULL,
  `id_series_statut` bigint(20) unsigned DEFAULT NULL,
  `id_format` int(11) DEFAULT NULL,
  `id_statut` int(11) DEFAULT NULL,
  `nom_serie` varchar(200) NOT NULL,
  `serie_normalise` varchar(220) DEFAULT NULL,
  `nombre_tomes_total` int(11) DEFAULT NULL,
  `prochain_tome_numero` int(11) DEFAULT NULL,
  `prochain_tome_date` date DEFAULT NULL,
  `pitch_serie` text DEFAULT NULL,
  `is_terminee` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_serie` varchar(12) GENERATED ALWAYS AS (concat('S',lpad(`id_serie`,6,'0'))) STORED,
  PRIMARY KEY (`id_serie`),
  UNIQUE KEY `uq_series_nom` (`nom_serie`),
  UNIQUE KEY `uq_series_code` (`code_serie`),
  KEY `idx_series_normalise` (`serie_normalise`),
  KEY `ix_series_id_format` (`id_format`),
  KEY `ix_series_id_statut` (`id_statut`),
  KEY `idx_series_id_series_format` (`id_series_format`) USING BTREE,
  KEY `idx_series_id_series_statut` (`id_series_statut`) USING BTREE,
  CONSTRAINT `fk_series_series_format` FOREIGN KEY (`id_series_format`) REFERENCES `series_format` (`id_series_format`) ON DELETE SET NULL,
  CONSTRAINT `fk_series_series_statut` FOREIGN KEY (`id_series_statut`) REFERENCES `series_statut` (`id_series_statut`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `series_format`
--

DROP TABLE IF EXISTS `series_format`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `series_format` (
  `id_series_format` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_series_format`),
  `libelle_format` varchar(80) NOT NULL,
  `nb_tomes_ref` tinyint(4) DEFAULT NULL,
  `ordre_affichage` int(11) NOT NULL DEFAULT 0,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_series_format` varchar(12) GENERATED ALWAYS AS (concat('SF',lpad(`id_series_format`,6,'0'))) STORED,
  PRIMARY KEY (`id_series_format`) USING BTREE,
  UNIQUE KEY `uq_series_format_libelle` (`libelle_format`),
  UNIQUE KEY `uq_series_format_code` (`code_series_format`),
  KEY `idx_series_format_ordre` (`ordre_affichage`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `series_statut`
--

DROP TABLE IF EXISTS `series_statut`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `series_statut` (
  `id_series_statut` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_series_statut`),
  `libelle_statut` varchar(80) NOT NULL,
  `est_final` tinyint(1) NOT NULL DEFAULT 0,
  `ordre_affichage` int(11) NOT NULL DEFAULT 0,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_series_statut` varchar(12) GENERATED ALWAYS AS (concat('SS',lpad(`id_series_statut`,6,'0'))) STORED,
  PRIMARY KEY (`id_series_statut`) USING BTREE,
  UNIQUE KEY `uq_series_statut_libelle` (`libelle_statut`),
  UNIQUE KEY `uq_series_statut_code` (`code_series_statut`),
  KEY `idx_series_statut_ordre` (`ordre_affichage`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tags`
--

DROP TABLE IF EXISTS `tags`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `tags` (
  `id_tag` bigint(20) unsigned NOT NULL DEFAULT nextval(`artefact`.`seq_tags`),
  `libelle_tag` varchar(180) NOT NULL,
  `type_tag` enum('Genre','Etiquette','Theme','Autre') NOT NULL DEFAULT 'Etiquette',
  `source_tag` varchar(60) NOT NULL DEFAULT 'Artefact',
  `couleur_tag` varchar(20) DEFAULT NULL,
  `mapping_tag` varchar(600) DEFAULT NULL,
  `is_actif` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `code_tag` varchar(12) GENERATED ALWAYS AS (concat('T',lpad(`id_tag`,6,'0'))) STORED,
  PRIMARY KEY (`id_tag`),
  UNIQUE KEY `uq_tags_unique` (`libelle_tag`,`type_tag`,`source_tag`),
  UNIQUE KEY `uq_tags_code` (`code_tag`),
  KEY `idx_tags_type` (`type_tag`),
  KEY `idx_tags_source` (`source_tag`),
  KEY `idx_tags_libelle` (`libelle_tag`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*M!100616 SET NOTE_VERBOSITY=@OLD_NOTE_VERBOSITY */;

-- Dump completed on 2026-05-23  9:52:25
