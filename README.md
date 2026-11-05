# 🎮 FTELMarket - Full-Stack Application (.NET / React / PostgreSQL)

FTELMarket est une application web full-stack de gestion de catalogue de jeux vidéo. Elle est composée d'une API backend développée en ASP.NET Core, d'une interface utilisateur créée avec React (Vite) et d'une base de données relationnelle PostgreSQL.

---

## 🏗️ Architecture du Projet

```text
.
├── FTELMarket/           # API Back-End (ASP.NET Core / EF Core)
├── FTEL-MARKET-FRONT/    # Front-End React / Vite (Conteneurisé)
└── README.md             # Documentation du projet
```

- **Front-End** : React + Vite (Tourne dans le conteneur Docker `ftelmarket-front` sur le port `3000`).
- **Back-End** : ASP.NET Core Web API (Tourne localement sur le port `5082`).
- **Base de Données** : PostgreSQL (Tourne dans le conteneur Docker `postgres-ftelmarket` sur le port `5432`).

---

## 📋 Prérequis

Avant de démarrer l'application, assure-toi d'avoir installé :

1. [Docker Desktop](https://www.docker.com/products/docker-desktop/) (actif sur ta machine).
2. [.NET 9 SDK](https://dotnet.microsoft.com/download) (ou version ultérieure).
3. [Node.js](https://nodejs.org/) (optionnel si tu utilises uniquement le conteneur Docker pour le front).

---

## 🚀 Procédure de Démarrage Pas à Pas

### Étape 1 : Démarrer Docker Desktop
1. Ouvre l'application **Docker Desktop** sur ta machine.
2. Attends que le statut de Docker indique que le moteur est démarré (*Docker Desktop is running*).

---

### Étape 2 : Démarrer les Conteneurs Docker (PostgreSQL & Front-End)

Si tes conteneurs sont déjà créés, démarre-les depuis ton terminal ou depuis l'interface Docker Desktop :

```powershell
docker start postgres-ftelmarket ftelmarket-front
```

> **Note :** Si les conteneurs ne sont pas encore créés, tu peux les lancer via Docker Compose ou manuellement :
> - Conteneur BDD : `postgres-ftelmarket` sur le port `5432:5432`
> - Conteneur Front : `ftelmarket-front` sur le port `3000:3000`

---

### Étape 3 : Lancer l'API Back-End (.NET Core)

1. Ouvre un terminal VS Code dans le dossier du backend **`FTELMarket`** :
   ```bash
   cd FTELMarket
   ```

2. Exécute l'API en forçant le port HTTP **5082** (port attendu par le Front-End) :
   ```bash
   dotnet run --urls "http://localhost:5082"
   ```

3. Le terminal doit afficher :  
   `Now listening on: http://localhost:5082`

---

### Étape 4 : Accéder à l'Application

1. Ouvre ton navigateur web et rends-toi sur :  
   👉 **`http://localhost:3000/produits`**
2. Rafraîchis la page (`F5`).

Tous les badges d'API en haut de l'écran (`GET /products`, `GET /categories`, `POST /products`, `PUT /products/{id}`, `DELETE /products/{id}`) doivent passer au **vert** ! 🟢

---

## 🛠️ Endpoints de l'API

L'API expose les routes HTTP suivantes sur `http://localhost:5082` :

| Méthode | Route | Description |
| :--- | :--- | :--- |
| `GET` | `/api/categories` | Récupère la liste de toutes les catégories |
| `GET` | `/api/products` | Récupère la liste complète des jeux |
| `GET` | `/api/products/{id}` | Récupère un jeu spécifique par son ID |
| `POST` | `/api/products` | Ajoute un nouveau jeu dans le catalogue |
| `PUT` | `/api/products/{id}` | Modifie un jeu existant |
| `DELETE` | `/api/products/{id}` | Supprime un jeu de la base de données |

---

## 📝 Remarques & Crédits

- La partie **Front-End React** (`FTEL-MARKET-FRONT`) a été fournie dans le cadre du sujet TP/Projet.
- La conception de l'**API Back-End ASP.NET Core**, la modélisation **EF Core / PostgreSQL**, la gestion du **CORS**, l'alignement des **DTOs** et la conteneurisation ont été réalisés dans le cadre de cette intégration.