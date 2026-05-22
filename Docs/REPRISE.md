# 🧭 Guide de reprise – Artefact

[TOC]

## Objectif

Ce guide permet à une personne (future, externe, ou "moi plus tard") de reprendre Artefact rapidement et proprement.

## 1) Pré-requis techniques

- Windows
- Visual Studio (version compatible .NET 8 WinForms)
- SDK .NET 8
- MariaDB (version projet)
- HeidiSQL ou DBeaver (optionnel mais recommandé)

## 2) Base de données

- Restaurer `Docs/Database/backup_NoData_artefact.sql` (ou la version avec données de test)
- Vérifier que les séquences sont bien présentes
- Vérifier que la table `meta_schema` contient la version attendue par l'application

## 3) Configuration locale

- L'application lit `%APPDATA%\Artefact\artefact.local.json`
- Ce fichier contient les paramètres MariaDB + mot de passe chiffré DPAPI
- En cas d'échec, passer par la form `GestionConnexionMariaDb`

## 4) Démarrage application

- Ouvrir la solution dans Visual Studio
- Lancer `Home`
- Vérifier la phase startup : config → connexion DB → compatibilité schéma
- Si startup KO : corriger connexion avant toute action métier

## 5) Cartographie rapide du code

- `Core/DatabaseManager` : connexion DB centralisée
- `Core/QueryModule` : SQL centralisé
- `Metier/GestionReferentiel` : exécution CRUD et logique intermédiaire
- `Forms_Referentiels/*` : UI de gestion référentielle
- `Utils/RichTextNotesHelper` : notes enrichies (_rtf/_txt)

## 6) Ordre conseillé pour reprendre

1. Lire `Readme.md`
2. Lire `Rules.md`
3. Lire `Process_Artefact.md`
4. Lire `Database/ModeleDB.md` + `Database/Tableaux_Colonnes.md`
5. Lire ce guide (`REPRISE.md`)
6. Relire `TODO.md` pour prioriser le prochain sprint

## 7) Pièges connus / points de vigilance

- Ne pas injecter de valeurs UI sentinelles en base (ex: "Toutes origines")
- Respecter le pattern référentiel (Form → GestionReferentiel → QueryModule)
- Ne pas utiliser SQL inline dans les forms
- Contrôler les dépendances avant suppression référentielle
- Ne jamais logguer les secrets

## 8) Prochaine zone critique à documenter

- Pipeline complet d'import Calibre (Processus 06 à formaliser)
