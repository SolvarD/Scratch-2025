# Étape 1 – Build Angular app
FROM node:20 AS build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

# Étape 2 – Serve via NGINX
FROM nginx:alpine

# Nettoyer les fichiers par défaut de Nginx
RUN rm -rf /usr/share/nginx/html/*

# ✅ Copier uniquement le contenu du dossier browser/
COPY --from=build /app/dist/Scracth2025.UI/browser/ /usr/share/nginx/html/

EXPOSE 80
