# ✅ TODO – Projet Artefact (Module Livres) - MàJ 28/02/2026

Ce document regroupe toutes les étapes planifiées pour le développement de l’application Artefact. 
Il est structuré par grands axes de travail avec des points détaillés pour chaque objectif.

---

## Fondations – Base de données & référentiels

- [x] Création database `artefact`
- [x] Création des tables de référentiels 
- [x]  Tables principales livres et livres_staging
- [x] Création table `auteurs` 
- [x] Généraliser le pattern SEQUENCE + code_xxx aux tables
- [x] Vérifier les redémarrages de séquence (RESTART WITH max+1)
- [x] Unicité des libellés métiers
- [ ] Validation des types de données (ex : prix en DECIMAL(10,2) ?)
- [ ] Validation des contraintes d’intégrité (ex : NOT NULL, UNIQUE, etc.)
- [ ] Validation des relations entre tables (ex : FK, ON DELETE CASCADE, etc.)

##  Référentiels complémentaires (à discuter avant création)

  - [x] Table Types de séries (ex: Oneshot, duologie, trilogie...etc). Nom?
  - [x] Ajouter un champ Categorie dans la table prixLit (pour la categorie d'un prix littéraire : Prix Untel, catégorie "Roman policier", etc.)
  - [x] Ajouter table meta_schema pour stocker les infos de versionnement de la base, historique des modifications, etc.
  - [x] Ajouter "Recommandations" avec leur origine - Tables + UI

---

## Tables principales

- [x] Choix définitif des noms :
  - `livres` ?
  - `livres_anorm` ? ==> **livres_staging** = *Choix définitif pour la table d’import non normalisé*
- [x] Définition des champs strictement nécessaires
- [x] Définition des liaisons (auteurs, tags, séries, formats, prix)

##  Tables principales : champs complémentaires 
- [x] Dans livres_staging, le fait qu'il y ait ou non un fichier, permettant ensuite d'ajouter des livres sans fichier ex: nouveautés, prochaines sorties, recommandations mais sans avoir le livre (avec_fichier)

---

## Documentation
- [ ] Rédaction d’une documentation technique à mettre à jour au fur et à mesure de l’avancement du projet
	- [x] Readme.md avec présentation du projet, objectifs, technologies utilisées, etc.. Mis à jour régulièrement au fur et à mesure de l’avancement du projet.  avec date dans le nom du fichier.
	- [x] Changelog.md pour suivre les évolutions de l’application. Evolution par date du jour.
	- [x] Rules.md pour définir les règles de développement - Au fur et à mesure de l’avancement du projet, ce document pourra être enrichi avec des règles de développement, des bonnes pratiques, etc. avec date dans le nom du fichier.
	- [x] Process_Artefact.md pour documenter les processus de développement, les étapes de validation, etc. 
	- [x] diagramme de processus pour chaque processus. Au fur et à mesure : à chaque fin de processus validé.  avec date dans le nom du fichier.
	- [ ] Documentation technique détaillée pour chaque module, classe, Form. Au fur et à mesure de l’avancement du projet, ce document pourra être enrichi avec des descriptions détaillées de chaque module, classe, Form, etc. avec date dans le nom du fichier.
	- [x] TODO.md pour suivre les tâches à réaliser, les étapes de développement, etc. (ce document). Mis à jour régulièrement. Validation de la ligne de tâches à chaque fin de tâche.  
	- [x] 	▫ Diagramme complet de la base (image)
	- [x] ModeleDB.md : description détaillée de la base de données, avec les tables, les champs, les relations, etc. avec date dans le nom du fichier.

---

## Application (squelette)

### 	🔹 Infrastructure 
- [x] Création des modules de base (DatabaseManager, GestionLog, QueryModule, ConfigManager)
- [x] Connexion MariaDB centralisée - fichier json de configuration pour les paramètres de connexion
- [x] Connexion au départ de l'application testée et validée
- [x] Gestion du path de configuration - création

### 	🎯🔹 UI
- [x] Form principale (Menu) : Navigation de base, accès aux différentes fonctionnalités. Création. Complétion au fur et à mesure du développement de l'application
- [x] Form Modèle : avec les contrôles de base pour les forms ==> ToolStripStatus, Panel, lblTitre
- [ ] Gestion des tables de paramètres
- [ ] Gestion des fichiers .bat : copie de la base Calibre dans un path dédié
- [ ] [🎯]  Forms de gestion référentiels (CRUD simples) 
  - [x] **Phase 1** : langues, pays, ref_enum, contacts, editeurs, formatfile, impression, 
  - [ ] [🎯] **Phase 2** :
    - [x] recommandation
    - [ ] [🎯]  PrixLit
  - [ ] **Phase 3** :
     - [ ] Auteurs
     - [ ] Series
     - [ ] Tags
  - [ ] **Phase 4** :
    - [ ] Les 3 tables de paramètres
    - [ ] Autres tables techniques (?)

- [x] Gestion des erreurs : affichage dans ToolStripStatus global ou MessageBox si erreur critique
- [ ] Gestion de la copie DB de Calibre pour éviter les problèmes de verrouillage de la DB d’origine (fichiers.bat déja créer - à intégrer dans l’application)
- [ ] Imports de masse de référentiels de base depuis les forms de gestion respectives: Tags, formats, Pays, Contacts
- [x] Gestion des logs uniformisée (Module spécifique)
  - [ ] Affichage dans une form dédiée pour une consultation aisée ?

### 	🔹 UI Contrôles spécifiques

- [ ] Menus
- [x] RichTextBox enrichi avec petite barre de menu pour gérer les notes (Gras, Italique, Souligné, Tabulations, listes)
- [ ] Calendrier

---

##  Import & logique métier
- [ ] Import Calibre
- [ ] Gestion des paths Data (e-books, couvertures, Photos auteurs etc...
- [ ] Forms de gestion des livres (CRUD)
- [ ] gestion des fichiers e_books et couvertures + Définition des dossiers de stockage
- [ ] Gestion livres en staging (importés mais pas encore validés)
- [ ] Validation / normalisation des données importées en staging
- [ ] Mapping tags
- [ ] Premiers exports simples

---

## Enrichissement 
- [ ] IA
- [ ] Auteurs : biographie, news, critiques
- [x] Recommandations et origines des recommandations (ex: TikTok, Booktpk, Instagram, librairie etc..)
- [ ] Evaluation d'un livre : après lecture. pas des étoiles. Trouver autre façon de représenter cette appréciation (par ex : une mini représentation de Bookzilla) Une seule = moins bonne, 6 la meilleure
- [ ] Notes personnelles
- [ ] Fiche de lecture
- [ ] Critiques
- [ ] Nouveautés
- [ ] Prochaines sorties
- [ ] Prochain(s) tome(s) d'une série
- [ ] Statistiques
- [ ] Calendrier
- [ ] Notes de rappel
- [ ] News
- [ ] Panel pendant consultation :  5 (10) derniers livres ajoutés, les 5 (10) derniers livres lus, et peut être les 5 dernières recherches.
- [ ] Wiki : Artefactopedia ?
- [ ] Représentation graphique de l’application, animations, voix, etc. Changement de nom ? (Bookzilla?, Manou ?
- [ ] Conversion de formats e-books notamment ePub vers AZW3
- [ ] Compter le nombre de pages dans un livre
- [ ] Indiquer le nombre de livres possédés d'un auteur ainsi que le nombre dans une série
- [ ] Modifier un ePub en ajoutant des infos supplémentaires comme Résumé, Bio Auteur ou autres.
- [ ] Ouvrir un livre depuis l'application, le consulter, le feuilleter
- [ ] Revue d'un livre, écrit d'abord avec passage sur Rewordify, puis voix pour diffusion sur réseaux sociaux avec     ElevenLabs

---

## Navigation, ergonomie, design
- [ ] Navigation par menus et/ou onglets et/ou dashboard
- [ ] Ergonomie – simplicité d’utilisation, fluidité, accessibilité
- [ ] Design - style bibliothèque steampunk, vintage, ou autre à définir
- [ ] Sécurité – gestion des accès, protection des données, etc.
- [ ] Interface Web
- [ ] Thèmes personnalisables
- [ ] Versionnement de l’application (ex : v1.0, v1.1, etc.) et gestion des mises à jour
- [ ] Gestion des fichiers l'installation et de la configuration initiale de l'application, y compris la création de la base de données et des tables nécessaires.
- [ ] Rédaction d’une documentation utilisateur pour expliquer les fonctionnalités de l’application 



---
---

> **Contact** : ***Joëlle (Manou)  - Les Artefacts de Manou***
>
> Projet personnel, expérimental, réalisé pour le fun, le test et l'étude de connaissances techniques.
> mailto: `joelle@nguyen.eu`
>
> - GitHub privé : Artefact    https://github.com/AngeljoNG/Artefact
> - GitHub public : Artefact  https://github.com/Les-Artefacts-de-Manou/Artefact
>

---
---




---





