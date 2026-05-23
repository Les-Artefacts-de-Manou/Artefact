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

--
-- Dumping data for table `auteurs`
--

LOCK TABLES `auteurs` WRITE;
/*!40000 ALTER TABLE `auteurs` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `auteurs` ENABLE KEYS */;
UNLOCK TABLES;
commit;
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
-- Dumping data for table `auteurs_pays`
--

LOCK TABLES `auteurs_pays` WRITE;
/*!40000 ALTER TABLE `auteurs_pays` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `auteurs_pays` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `contacts`
--

LOCK TABLES `contacts` WRITE;
/*!40000 ALTER TABLE `contacts` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `contacts` VALUES
(1,'Pearl','pearlnduy@gmail.com','pearlnduy_hd6nrx@kindle.com','Kindle','2026-03-09 10:20:01','2026-03-09 10:20:01','C000001'),
(7,'Nell',NULL,NULL,NULL,'2026-03-16 09:16:56','2026-03-16 09:16:56','C000007');
/*!40000 ALTER TABLE `contacts` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `editeurs`
--

LOCK TABLES `editeurs` WRITE;
/*!40000 ALTER TABLE `editeurs` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `editeurs` VALUES
(7,'Éditions Gallimard',7,'https://www.gallimard.fr/',NULL,'Contact : Éditions Gallimard – 5 rue Gaston-Gallimard – 75328 Paris cedex 07 France ; Tél. : +33 (0)1.49.54.42.00\n\nGénéraliste\n\nL\'une des maisons d\'édition les plus prestigieuses de la littérature francophone, avec une grande diversité de genres et d\'auteurs.\n\nÀ ce jour, cette maison ne semble plus accepter les manuscrits en raison de la forte demande.','2026-03-10 16:36:01','2026-03-19 17:03:23','E000007'),
(8,'Éditions Flammarion',7,'https://editions.flammarion.com/',NULL,'Éditions Flammarion\n82, rue Saint-Lazare CS 10124\n75009 Paris.\nStandard : 01 40 51 31 00\naccueil.flammarion@flammarion.fr\n\nGénéraliste\n\nUne maison d\'édition majeure qui publie une variété de genres littéraires (fiction, non-fiction, poésie, etc.).\n\nSoumettre son manuscrit : en ligne, par l’intermédiaire du formulaire de contact \n\nhttps://editions.flammarion.com/envoyer-un-manuscrit/','2026-03-10 16:36:49','2026-03-19 17:03:23','E000008'),
(9,'Éditions Albin Michel',7,'https://www.albin-michel.fr/',NULL,'Éditions Albin Michel – 22 rue Huyghens – 75014 Paris\nGénéraliste\nPublient une gamme diversifiée de livres, notamment de la fiction, de la non-fiction, des thrillers et des romans à succès.\n\nPour soumettre son manuscrit : envoi postal uniquementPlus d’informations : https://www.albin-michel.fr/deposer-un-manuscrit','2026-03-10 16:38:10','2026-03-19 17:03:23','E000009'),
(12,'Test Editions',5,'https://www.hhaau.be','{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2060{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}{\\f1\\fnil Segoe UI;}{\\f2\\fnil\\fcharset2 Symbol;}}\r\n{\\*\\generator Riched20 10.0.26100}\\viewkind4\\uc1 \r\n\\pard\\b\\f0\\fs18 C\\rquote est align\\\'e9 avec tes r\\\'e8gles de factorisation\\b0 , de s\\\'e9paration stricte SQL / m\\\'e9tier / UI, et avec la r\\\'e8gle officielle du \\i stockage RTF \\i0 + texte miroir pour la recherche. \\par\r\n\r\n\\pard{\\pntext\\f2\\\'B7\\tab}{\\*\\pn\\pnlvlblt\\pnf2\\pnindent0{\\pntxtb\\\'B7}}Mon avis d\\rquote architecture est celui-ci : Tr\\\'e8s bon endroit\\f1\\par\r\n\r\n\\pard\\par\r\n\\f0\\tab Mon avis c\'est pas mal.\\par\r\n\\ul\\b\\i Encore une bonne id\\\'e9e\\par\r\n\\b0\\i0 toujours beau\\ulnone\\f1\\par\r\n}\r\n','C’est aligné avec tes règles de factorisation, de séparation stricte SQL / métier / UI, et avec la règle officielle du stockage RTF + texte miroir pour la recherche. \nMon avis d’architecture est celui-ci : Très bon endroit\n\n	Mon avis c\'est pas mal.\nEncore une bonne idée\ntoujours beau','2026-03-19 18:27:04','2026-03-20 12:20:19','E000012'),
(13,'Fre Editions',4,'qds','{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2060{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}{\\f1\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.26100}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18 Tout est bon chez banania\\f1\\par\r\n}\r\n','Tout est bon chez banania','2026-03-20 11:42:53','2026-03-20 11:42:53','E000013');
/*!40000 ALTER TABLE `editeurs` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `formatfile`
--

LOCK TABLES `formatfile` WRITE;
/*!40000 ALTER TABLE `formatfile` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `formatfile` VALUES
(1,'EPUB','epub','application/epub+zip',1,1,'2026-02-14 18:17:18','2026-03-13 14:50:00','F000001'),
(2,'AZW3','azw3','application/vnd.amazon.mobi8-ebook',2,1,'2026-02-14 18:17:18','2026-02-14 18:17:18','F000002'),
(3,'PDF','pdf','application/pdf',3,1,'2026-02-14 18:17:18','2026-03-13 15:11:18','F000003'),
(4,'CBR','cbr','application/x-cbr',4,1,'2026-02-14 18:17:18','2026-02-14 18:17:18','F000004'),
(5,'CBZ','cbz','application/vnd.comicbook+zip',5,1,'2026-02-14 18:17:18','2026-02-14 18:17:18','F000005'),
(6,'MOBI','mobi','application/x-mobipocket-ebook',6,1,'2026-02-14 18:17:18','2026-02-14 18:17:18','F000006');
/*!40000 ALTER TABLE `formatfile` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `impression`
--

LOCK TABLES `impression` WRITE;
/*!40000 ALTER TABLE `impression` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `impression` VALUES
(1,'I000001','DIRECT_PAL','Direct dans ma PAL',NULL,NULL,'A',1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(2,'I000002','PAL_ATTENTE','PAL en attente',NULL,NULL,'B',1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(3,'I000003','AIR_PAS_MAL','Ça a l\'air pas mal',NULL,NULL,'C',1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(4,'I000004','INTERESSANT','Intéressant',NULL,NULL,'D',1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(5,'I000005','SANS_PLUS','Sans plus',NULL,'sans plus = pourrait avoir quelque interêt, sans doute','E',1,'2026-03-15 17:09:15','2026-03-20 12:56:22'),
(6,'I000006','MAIS_POURQUOI','Mais pourquoi ai-je ce livre ?','{\\rtf1\\ansi\\deff0\\nouicompat{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}{\\f1\\fnil Segoe UI;}{\\f2\\fnil\\fcharset2 Symbol;}}\r\n{\\*\\generator Riched20 10.0.26100}\\viewkind4\\uc1 \r\n\\pard\\b\\f0\\fs18\\lang2060 oui \\b0\\f1 vraiment je me demande ce qu\'il fait l\\f0\\\'e0 :)\\par\r\n\\par\r\n\\i Il devrait peut \\\'eatre sortir? \\par\r\n\\par\r\n\\ul\\i0 Qu\'en penses-tu ?\\par\r\n\r\n\\pard{\\pntext\\f2\\\'B7\\tab}{\\*\\pn\\pnlvlblt\\pnf2\\pnindent0{\\pntxtb\\\'B7}}\\ulnone travers\\par\r\n{\\pntext\\f2\\\'B7\\tab}ballon\\par\r\n{\\pntext\\f2\\\'B7\\tab}rutabaga\\par\r\n}\r\n','oui vraiment je me demande ce qu\'il fait là :)\n\nIl devrait peut être sortir? \n\nQu\'en penses-tu ?\ntravers\nballon\nrutabaga','F',1,'2026-03-15 17:09:15','2026-03-20 15:24:11'),
(7,'I000007','POURQUOI_PAS','Pourquoi pas ?',NULL,'','',1,'2026-03-15 17:09:15','2026-03-20 12:56:22'),
(8,'I000008','PAS_ENCORE_AVIS','Pas encore d\'avis',NULL,NULL,NULL,1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(9,'I000009','POUR_THANH','Ce serait bien pour Thanh',NULL,NULL,'Thanh',1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(10,'I000010','POUR_PEARL','Ce serait bien pour Pearl',NULL,NULL,'Pearl',1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(11,'I000011','POUR_FILLES','Ce serait bien pour les filles',NULL,NULL,'Filles',1,'2026-03-15 17:09:15','2026-03-15 17:09:15'),
(12,'I000012','POUR_BASILE','Ce serait bien pour Basile',NULL,'','Basile',1,'2026-03-15 17:09:15','2026-03-20 12:56:22');
/*!40000 ALTER TABLE `impression` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `langues`
--

LOCK TABLES `langues` WRITE;
/*!40000 ALTER TABLE `langues` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `langues` VALUES
(1,'Français','FR','fr','fra','2026-03-02 17:38:54','2026-03-09 14:06:24','L000001'),
(3,'Anglais','EN','en','eng','2026-03-02 18:23:19','2026-03-09 14:23:00','L000003'),
(5,'Italien','IT','it','ita','2026-03-03 13:13:00','2026-03-03 13:13:00','L000005');
/*!40000 ALTER TABLE `langues` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres`
--

LOCK TABLES `livres` WRITE;
/*!40000 ALTER TABLE `livres` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_auteurs`
--

LOCK TABLES `livres_auteurs` WRITE;
/*!40000 ALTER TABLE `livres_auteurs` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_auteurs` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_contacts`
--

LOCK TABLES `livres_contacts` WRITE;
/*!40000 ALTER TABLE `livres_contacts` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_contacts` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_fichiers`
--

LOCK TABLES `livres_fichiers` WRITE;
/*!40000 ALTER TABLE `livres_fichiers` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_fichiers` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_prixlit_annee`
--

LOCK TABLES `livres_prixlit_annee` WRITE;
/*!40000 ALTER TABLE `livres_prixlit_annee` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_prixlit_annee` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_recommandations`
--

LOCK TABLES `livres_recommandations` WRITE;
/*!40000 ALTER TABLE `livres_recommandations` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_recommandations` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_staging`
--

LOCK TABLES `livres_staging` WRITE;
/*!40000 ALTER TABLE `livres_staging` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_staging` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_staging_auteurs`
--

LOCK TABLES `livres_staging_auteurs` WRITE;
/*!40000 ALTER TABLE `livres_staging_auteurs` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_staging_auteurs` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_staging_recommandations`
--

LOCK TABLES `livres_staging_recommandations` WRITE;
/*!40000 ALTER TABLE `livres_staging_recommandations` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_staging_recommandations` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `livres_tags`
--

LOCK TABLES `livres_tags` WRITE;
/*!40000 ALTER TABLE `livres_tags` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `livres_tags` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `meta_schema`
--

LOCK TABLES `meta_schema` WRITE;
/*!40000 ALTER TABLE `meta_schema` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `meta_schema` VALUES
(1,6,'2026-03-20 16:45:07','Ajout table meta_schema');
/*!40000 ALTER TABLE `meta_schema` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `param_api`
--

LOCK TABLES `param_api` WRITE;
/*!40000 ALTER TABLE `param_api` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `param_api` VALUES
(1,'AP000001','LOCAL','OPENAI','OpenAI','https://api.openai.com',NULL,'Clé fournie localement (ne pas stocker en clair)','{ \"chat_completions\": \"/v1/chat/completions\" }',1,'2026-02-16 18:10:22','2026-02-16 18:10:22'),
(2,'AP000002','LOCAL','GOOGLE_BOOKS','Google Books','https://www.googleapis.com',NULL,'Clé fournie localement (ne pas stocker en clair)','{ \"volumes_endpoint\": \"/books/v1/volumes\" }',1,'2026-02-16 18:10:22','2026-02-16 18:10:22'),
(3,'AP000003','LOCAL','CHATGPT_WEB','ChatGPT Web','https://chat.openai.com',NULL,NULL,NULL,1,'2026-02-16 18:10:22','2026-02-16 18:10:22');
/*!40000 ALTER TABLE `param_api` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `param_db`
--

LOCK TABLES `param_db` WRITE;
/*!40000 ALTER TABLE `param_db` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `param_db` VALUES
(1,'DB000001','LOCAL','MariaDB_Artefact','MARIADB','192.168.1.138',3306,'Artefact','root','Password chiffré via appli (DPAPI)',NULL,'SslMode=none',1,'2026-02-16 18:10:22','2026-02-16 18:10:22'),
(2,'DB000002','LOCAL','Calibre_SQLite','SQLITE',NULL,NULL,'myMetadata.db',NULL,NULL,NULL,'Chemin via Path_General + Path_DBCalibre + nom_base',1,'2026-02-16 18:10:22','2026-02-16 18:10:22');
/*!40000 ALTER TABLE `param_db` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `param_paths`
--

LOCK TABLES `param_paths` WRITE;
/*!40000 ALTER TABLE `param_paths` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `param_paths` VALUES
(1,'PA000001','LOCAL','Path_General','C:\\Users\\Joelle\\OneDrive\\Artefact','Path général',1,'2026-02-16 18:10:22','2026-02-16 18:10:22'),
(2,'PA000002','LOCAL','Path_DBCalibre','BdeD\\DBCalibre','Sous-dossier copie DB Calibre',1,'2026-02-16 18:10:22','2026-02-16 18:13:48'),
(3,'PA000003','LOCAL','Path_Biblio_Calibre','Z:\\2 Ebookb','Bibliothèque Calibre',1,'2026-02-16 18:10:22','2026-02-16 18:10:22'),
(4,'PA000004','LOCAL','Path_Data','Artefact','Sous-dossier data Artefact',1,'2026-02-16 18:10:22','2026-02-16 18:10:22');
/*!40000 ALTER TABLE `param_paths` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `pays`
--

LOCK TABLES `pays` WRITE;
/*!40000 ALTER TABLE `pays` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `pays` VALUES
(4,'Royaume-Uni','GB','GBR','2026-03-03 16:50:32','2026-03-03 16:50:32','P000004'),
(5,'Belgique','BE','BEL','2026-03-03 16:50:32','2026-03-03 16:50:32','P000005'),
(6,'Italie','IT','ITA','2026-03-03 17:24:43','2026-03-03 17:24:43','P000006'),
(7,'France','FR','FRA','2026-03-03 17:42:33','2026-03-03 17:42:33','P000007');
/*!40000 ALTER TABLE `pays` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `prixlit`
--

LOCK TABLES `prixlit` WRITE;
/*!40000 ALTER TABLE `prixlit` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `prixlit` VALUES
(1,'Prix du Quai des Orfèvres','Policier - France','Le prix du Quai des Orfèvres a été fondé en 1946 par Jacques Catineau, personnalité du monde de la communication et ami de la police et de la magistrature. Il récompense un manuscrit inédit de roman policier écrit par un auteur de langue française.\n\nLe jury, composé de 22 membres — policiers, magistrats, avocats et journalistes — est présidé par le directeur de la police judiciaire (PJ) de la préfecture de police de Paris, depuis 2015 Christian Sainte, anciennement sise au célèbre 36, quai des Orfèvres à Paris. Les quelque 80 manuscrits reçus en moyenne chaque année par le secrétariat général du Prix du Quai des Orfèvres font l\'objet d\'une sélection par le secrétariat du Prix (36, rue du Bastion, 75017 Paris).\n\nLa proclamation du Prix du Quai des Orfèvres par le préfet de police a lieu généralement en même temps que la sortie du livre en librairie, vers la mi-novembre dans les locaux de la direction régionale de la police judiciaire. En outre, le montant du prix est de 777 euros remis à l\'auteur le jour de la proclamation du prix','{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2060{\\fonttbl{\\f0\\fnil Segoe UI;}{\\f1\\fnil\\fcharset0 Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.26100}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18 Le prix du Quai des Orf\\f1\\\'e8vres a \\\'e9t\\\'e9 fond\\\'e9 en \\b 1946 \\b0 par \\b\\i Jacques Catineau\\b0\\i0 , personnalit\\\'e9 du monde de la communication et ami de la police et de la magistrature. Il r\\\'e9compense un \\b manuscrit in\\\'e9dit de roman policier \\\'e9crit par un auteur de langue fran\\\'e7aise\\b0 .\\par\r\n\\par\r\nLe jury, compos\\\'e9 de 22 membres \\f0\\emdash  \\b\\f1 policiers, magistrats, avocats et journalistes \\b0\\f0\\emdash  est pr\\f1\\\'e9sid\\\'e9 par le \\b directeur de la police judiciaire \\b0 (PJ) de la pr\\\'e9fecture de police de Paris, depuis 2015 Christian Sainte, anciennement sise au c\\\'e9l\\\'e8bre 36, quai des Orf\\\'e8vres \\\'e0 Paris. Les quelque 80 manuscrits re\\\'e7us en moyenne chaque ann\\\'e9e par le secr\\\'e9tariat g\\\'e9n\\\'e9ral du Prix du Quai des Orf\\\'e8vres font l\'objet d\'une s\\\'e9lection par le secr\\\'e9tariat du Prix (36, rue du Bastion, 75017 Paris).\\par\r\n\\par\r\nLa proclamation du Prix du Quai des Orf\\\'e8vres par le pr\\\'e9fet de police a lieu g\\\'e9n\\\'e9ralement en m\\\'eame temps que la sortie du livre en librairie, vers la mi-novembre dans les locaux de la direction r\\\'e9gionale de la police judiciaire. En outre, le montant du prix est de 777 euros remis \\\'e0 l\'auteur le jour de la proclamation du prix\\f0\\par\r\n}\r\n',1,'2026-03-28 13:15:36','2026-03-28 13:15:36','R000001'),
(2,'Prix Imaginales','Fantasy - France',NULL,NULL,1,'2026-03-28 13:25:29','2026-03-28 13:25:29','R000002'),
(3,'Prix du Meilleur Livre étranger','Romans et Essais traduits en français - France','Le prix du Meilleur Livre étranger Sofitel est un prix littéraire qui récompense chaque année un roman et un essai publiés à l’étranger et traduits en français.\n\nCréé en 1948, autour d\'un groupe amical et informel de directeurs littéraires de maisons d’édition, le prix du Meilleur Livre étranger a été un des premiers à s\'intéresser aux livres traduits en français. Son fondateur est Robert Carlier, avec son ami André Bay. Depuis 2011, le prix est soutenu par Sofitel et est décerné dans un des hôtels de la chaîne, comme le Sofitel Paris Le Faubourg. Les délibérations du jury ont lieu à la Brasserie Lipp.','{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2060{\\fonttbl{\\f0\\fnil Segoe UI;}{\\f1\\fnil\\fcharset0 Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.26100}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18 Le prix du Meilleur Livre \\f1\\\'e9tranger Sofitel est un prix litt\\\'e9raire qui r\\\'e9compense chaque \\b ann\\\'e9e un roman et un essai publi\\\'e9s \\\'e0 l\\rquote\\\'e9tranger et traduits en fran\\\'e7ais.\\b0\\par\r\n\\par\r\nCr\\\'e9\\\'e9 en \\b 1948\\b0 , autour d\'un groupe amical et informel de directeurs litt\\\'e9raires de maisons d\\rquote\\\'e9dition, le prix du Meilleur Livre \\\'e9tranger a \\\'e9t\\\'e9 un des premiers \\\'e0 s\'int\\\'e9resser aux livres traduits en fran\\\'e7ais. Son fondateur est Robert Carlier, avec son ami Andr\\\'e9 Bay. Depuis 2011, le prix est soutenu par Sofitel et est d\\\'e9cern\\\'e9 dans un des h\\\'f4tels de la cha\\\'eene, comme le Sofitel Paris Le Faubourg. Les d\\\'e9lib\\\'e9rations du jury ont lieu \\\'e0 la \\b Brasserie Lipp\\b0 .\\f0\\par\r\n}\r\n',1,'2026-03-28 15:10:44','2026-03-28 15:10:44','R000003');
/*!40000 ALTER TABLE `prixlit` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `prixlit_annee`
--

LOCK TABLES `prixlit_annee` WRITE;
/*!40000 ALTER TABLE `prixlit_annee` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `prixlit_annee` VALUES
(1,1,1946,'2026-03-28 13:17:04','2026-03-28 13:17:04','RA000001'),
(2,15,2020,'2026-03-28 16:29:20','2026-03-28 16:29:20','RA000002'),
(3,2,2015,'2026-03-28 16:30:09','2026-03-28 16:30:09','RA000003');
/*!40000 ALTER TABLE `prixlit_annee` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `prixlit_categorie`
--

LOCK TABLES `prixlit_categorie` WRITE;
/*!40000 ALTER TABLE `prixlit_categorie` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `prixlit_categorie` VALUES
(1,1,'Lauréat',NULL,0,1,'2026-03-28 13:16:43','2026-03-28 13:16:43','RC000001'),
(2,2,'Meilleur roman francophone',NULL,1,1,'2026-03-28 15:00:25','2026-03-28 15:00:25','RC000002'),
(3,2,'Meilleur roman étranger',NULL,2,1,'2026-03-28 15:00:41','2026-03-28 15:00:41','RC000003'),
(4,2,'Meilleur roman','Avant 2005',3,1,'2026-03-28 15:01:20','2026-03-28 15:01:20','RC000004'),
(5,2,'Meilleure œuvre pour la jeunesse',NULL,4,1,'2026-03-28 15:01:45','2026-03-28 15:01:45','RC000005'),
(6,2,'Meilleure nouvelle',NULL,5,1,'2026-03-28 15:02:05','2026-03-28 15:02:05','RC000006'),
(7,2,'Meilleure illustration',NULL,6,1,'2026-03-28 15:02:33','2026-03-28 15:02:33','RC000007'),
(8,2,'Meilleure bande-dessinée',NULL,7,1,'2026-03-28 15:02:51','2026-03-28 15:02:51','RC000008'),
(9,2,'Prix spécial du jury',NULL,8,1,'2026-03-28 15:03:07','2026-03-28 15:03:07','RC000009'),
(10,2,'Album',NULL,12,1,'2026-03-28 15:03:49','2026-03-28 15:13:02','RC000010'),
(11,2,'Lycéens',NULL,9,1,'2026-03-28 15:04:05','2026-03-28 15:04:05','RC000011'),
(12,2,'Collégiens',NULL,10,1,'2026-03-28 15:04:29','2026-03-28 15:04:29','RC000012'),
(13,2,'Ecoliers',NULL,11,1,'2026-03-28 15:04:48','2026-03-28 15:04:48','RC000013'),
(14,2,'Bibliothécaires',NULL,13,1,'2026-03-28 15:05:05','2026-03-28 15:13:09','RC000014'),
(15,3,'Roman',NULL,1,1,'2026-03-28 15:11:48','2026-03-28 15:11:48','RC000015'),
(16,3,'Essai',NULL,2,1,'2026-03-28 15:11:54','2026-03-28 15:12:05','RC000016');
/*!40000 ALTER TABLE `prixlit_categorie` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `recommandations`
--

LOCK TABLES `recommandations` WRITE;
/*!40000 ALTER TABLE `recommandations` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `recommandations` VALUES
(1,'R000001',1,'Camille Reads','@camille_reads','https://tiktok.com/@camille_reads','2026-03-15',NULL,'Vu passer 3 fois, hype énorme',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(2,'R000002',1,'BookAddictFR','@bookaddictfr','https://tiktok.com/@bookaddictfr','2026-03-16',NULL,'Semble très addictif',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(3,'R000003',2,'LecturesNocturnes','@lectures_nocturnes','https://instagram.com/lectures_nocturnes','2026-03-14',NULL,'Très belles photos + avis positif',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(4,'R000004',2,'BiblioPassion','@bibliopassion','https://instagram.com/bibliopassion',NULL,NULL,'Recommandé en story',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(5,'R000005',3,'Blog du Polar','sqqqq','https://blogdupolar.com/article123','2026-03-10','{\\rtf1\\ansi\\deff0\\nouicompat{\\fonttbl{\\f0\\fnil Segoe UI;}{\\f1\\fnil\\fcharset0 Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.26100}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\lang2060 Critique tr\\f1\\\'e8s d\\\'e9taill\\\'e9e\\f0\\par\r\n}\r\n','Critique très détaillée',1,'2026-03-18 15:55:43','2026-03-21 16:45:52'),
(6,'R000006',4,'Pearl',NULL,NULL,'2026-03-12',NULL,'Insisté lourdement 😄',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(7,'R000007',5,'Librairie du Centre',NULL,NULL,NULL,NULL,'Mis en avant en vitrine',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(8,'R000008',6,'Les lecteurs anonymes',NULL,NULL,'2026-03-08',NULL,'Épisode entier dédié',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(9,'R000009',7,'Groupe SF Belgique',NULL,'https://facebook.com/groups/sfbelgique','2026-03-11',NULL,'Beaucoup de commentaires positifs',1,'2026-03-18 15:55:43','2026-03-20 16:34:40'),
(10,'R000010',8,'Le coin lecture','@lecoinlecture','https://youtube.com/watch?v=xxxxx','2026-03-13',NULL,'Top 10 du mois',1,'2026-03-18 15:55:43','2026-03-20 16:34:40');
/*!40000 ALTER TABLE `recommandations` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `ref_enum`
--

LOCK TABLES `ref_enum` WRITE;
/*!40000 ALTER TABLE `ref_enum` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `ref_enum` VALUES
(1,'N000001',3,'LU','Lu',1,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(2,'N000002',3,'NON_LU','Non lu',2,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(3,'N000003',3,'EN_COURS','En cours',3,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(4,'N000004',5,'KINDLE','Kindle',1,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(5,'N000005',5,'AUDIBLE','Audible',2,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(6,'N000006',5,'PAPIER','Papier',3,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(7,'N000007',6,'ACHAT','Achat',1,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(8,'N000008',6,'DOWN','Down',2,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(9,'N000009',6,'ABONNEMENT','Abonnement',3,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(10,'N000010',6,'PRET','Prêt',4,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(11,'N000011',7,'EBOOK','eBook',1,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(12,'N000012',7,'COVER','Couverture',2,1,'2026-02-13 14:23:47','2026-03-04 14:34:56'),
(13,'N000013',8,'NAISSANCE','Pays de naissance',1,1,'2026-02-14 19:00:25','2026-03-04 14:34:56'),
(14,'N000014',8,'RESIDENCE','Pays de résidence',2,1,'2026-02-14 19:00:25','2026-03-04 14:34:56'),
(15,'N000015',8,'NATIONALITE','Nationalité',3,1,'2026-02-14 19:00:25','2026-03-04 14:34:56'),
(16,'N000016',8,'ECRITURE','Pays d’écriture principal',4,1,'2026-02-14 19:00:25','2026-03-04 14:34:56'),
(17,'N000017',8,'CULTURE','Contexte culturel',5,1,'2026-02-14 19:00:25','2026-03-04 14:34:56'),
(18,'N000018',1,'AUTEUR','Auteur principal',1,1,'2026-02-15 15:49:49','2026-03-04 14:34:56'),
(19,'N000019',1,'COAUTEUR','Co-auteur',2,1,'2026-02-15 15:49:49','2026-03-04 14:34:56'),
(20,'N000020',1,'TRADUCTEUR','Traducteur',3,1,'2026-02-15 15:49:49','2026-03-04 14:34:56'),
(21,'N000021',1,'ILLUSTRATEUR','Illustrateur',4,1,'2026-02-15 15:49:49','2026-03-04 14:34:56'),
(22,'N000022',1,'DIRECTEUR','Directeur',5,1,'2026-02-15 15:49:49','2026-03-04 14:34:56'),
(23,'N000023',1,'PREFACE','Préfacier',6,1,'2026-02-15 15:49:49','2026-03-04 14:34:56'),
(24,'N000024',1,'POSTFACE','Postfacier',7,1,'2026-02-15 15:49:49','2026-03-04 14:34:56'),
(25,'N000025',2,'CALIBRE','Import Calibre',1,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(26,'N000026',2,'MANUEL','Ajout manuel',2,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(27,'N000027',2,'WEB','Source web',3,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(28,'N000028',2,'AUTRE','Autre source',4,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(29,'N000029',4,'A_VALIDER','À valider',1,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(30,'N000030',4,'A_COMPLETER','À compléter',2,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(31,'N000031',4,'OK','Validé (prêt à normaliser)',3,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(32,'N000032',4,'NORMALISE','Normalisé (copié dans livres)',4,1,'2026-02-15 17:57:20','2026-03-04 14:34:56'),
(33,'N000033',4,'REJETE','Rejeté',5,1,'2026-02-15 17:57:20','2026-03-04 14:34:56');
/*!40000 ALTER TABLE `ref_enum` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `ref_enum_type`
--

LOCK TABLES `ref_enum_type` WRITE;
/*!40000 ALTER TABLE `ref_enum_type` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `ref_enum_type` VALUES
(1,'ET000001','ROLE_AUTEUR_LIVRE','role_auteur_livre',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(2,'ET000002','SOURCE_IMPORT','source_import',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(3,'ET000003','STATUT_LECTURE','statut_lecture',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(4,'ET000004','STATUT_STAGING','statut_staging',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(5,'ET000005','SUPPORT_LECTURE','support_lecture',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(6,'ET000006','TYPE_ACQUISITION','type_acquisition',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(7,'ET000007','TYPE_FICHIER','type_fichier',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(8,'ET000008','TYPE_RELATION_AUTEUR_PAYS','type_relation_auteur_pays',0,1,'2026-03-04 14:33:52','2026-03-04 14:33:52'),
(13,'ET000013','TEST','test',0,1,'2026-03-09 16:57:38','2026-03-09 16:57:38');
/*!40000 ALTER TABLE `ref_enum_type` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `ref_origine_recommandation`
--

LOCK TABLES `ref_origine_recommandation` WRITE;
/*!40000 ALTER TABLE `ref_origine_recommandation` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `ref_origine_recommandation` VALUES
(1,'OR000001','BookTok',1,1,'2026-03-18 15:43:02','2026-03-18 15:43:02'),
(2,'OR000002','Instagram',2,1,'2026-03-18 15:43:02','2026-03-18 19:22:11'),
(3,'OR000003','Blog littéraire',3,1,'2026-03-18 15:43:02','2026-03-18 15:43:02'),
(4,'OR000004','Conseil ami',4,1,'2026-03-18 15:43:02','2026-03-18 15:43:02'),
(5,'OR000005','Librairie',5,1,'2026-03-18 15:43:02','2026-03-18 15:43:02'),
(6,'OR000006','Podcast',6,1,'2026-03-18 15:43:02','2026-03-18 15:43:02'),
(7,'OR000007','Facebook',7,1,'2026-03-18 15:43:02','2026-03-18 15:43:02'),
(8,'OR000008','YouTube',8,1,'2026-03-18 15:43:02','2026-03-18 15:43:02');
/*!40000 ALTER TABLE `ref_origine_recommandation` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `series`
--

LOCK TABLES `series` WRITE;
/*!40000 ALTER TABLE `series` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `series` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `series_format`
--

LOCK TABLES `series_format` WRITE;
/*!40000 ALTER TABLE `series_format` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `series_format` VALUES
(1,'Oneshot',1,1,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000001'),
(2,'Duologie',2,2,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000002'),
(3,'Trilogie',3,3,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000003'),
(4,'Tétralogie',4,4,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000004'),
(5,'Pentalogie',5,5,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000005'),
(6,'Série de 6',6,6,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000006'),
(7,'Série de 7',7,7,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000007'),
(8,'Série de 8',8,8,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000008'),
(9,'Série de 9',9,9,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000009'),
(10,'Série de 10',10,10,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000010'),
(11,'Série de 11',11,11,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000011'),
(12,'Série de 12',12,12,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000012'),
(13,'Série de 13',13,13,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000013'),
(14,'Série de 14',14,14,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000014'),
(15,'Série de 15',15,15,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000015'),
(16,'Série de 16',16,16,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000016'),
(17,'Série de 17',17,17,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000017'),
(18,'Série de 18',18,18,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000018'),
(19,'Série de 19',19,19,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000019'),
(20,'Série de 20',20,20,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000020'),
(21,'Série de X',NULL,99,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SF000021');
/*!40000 ALTER TABLE `series_format` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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
-- Dumping data for table `series_statut`
--

LOCK TABLES `series_statut` WRITE;
/*!40000 ALTER TABLE `series_statut` DISABLE KEYS */;
set autocommit=0;
INSERT INTO `series_statut` VALUES
(1,'En cours',0,1,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SS000001'),
(2,'Terminée',1,2,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SS000002'),
(3,'En pause',0,3,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SS000003'),
(4,'Arrêtée',1,4,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SS000004'),
(5,'Spin-off',0,5,1,'2026-02-26 19:09:09','2026-02-26 19:09:09','SS000005');
/*!40000 ALTER TABLE `series_statut` ENABLE KEYS */;
UNLOCK TABLES;
commit;

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

--
-- Dumping data for table `tags`
--

LOCK TABLES `tags` WRITE;
/*!40000 ALTER TABLE `tags` DISABLE KEYS */;
set autocommit=0;
/*!40000 ALTER TABLE `tags` ENABLE KEYS */;
UNLOCK TABLES;
commit;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*M!100616 SET NOTE_VERBOSITY=@OLD_NOTE_VERBOSITY */;

-- Dump completed on 2026-05-23  9:52:27
