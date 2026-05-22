# 📚 Glossaire Artefact

[TOC]

## Termes métier

### `livres`
Table des livres validés/normalisés, exploitables par l'application.

### `livres_staging`
Zone tampon des données importées ou incomplètes avant validation vers `livres`.

### recommandation
Événement de découverte d'un livre (source, contexte, date, commentaire), indépendant du cycle de vie du livre.

### référentiel
Table de valeurs structurantes (langues, pays, tags, etc.) réutilisées par les tables métier.

## Termes techniques

### `id_xxx`
Identifiant technique numérique (PK), alimenté par séquence.

### `code_xxx`
Code lisible généré automatiquement, non utilisé comme clé étrangère.

### `meta_schema`
Table de versionnement du schéma Artefact utilisée au démarrage de l'application.

### `ModeEdition`
Enum standard des forms référentielles : Consultation / Nouveau / Modification.

### snapshot
Copie de l'état courant d'un enregistrement pour permettre un `Annuler` fiable en mode modification.

### `_rtf` / `_txt`
Double stockage des notes enrichies :
- `_rtf` pour l'affichage riche
- `_txt` pour la recherche SQL

### DPAPI
Mécanisme de chiffrement Windows utilisé pour protéger localement le mot de passe de connexion.

## Termes de documentation

### Processus
Description pas-à-pas d'un flux métier/technique, idéalement avec diagramme Mermaid.

### règle fondatrice
Décision d'architecture considérée stable et à respecter sur la durée.

### document de reprise
Document orienté continuité : permet à une autre personne de reprendre sans dépendre de l'historique oral.
