# Étape 1 : Image de base pour runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Étape 2 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie des .csproj uniquement
COPY ["Scratch2025.API/Scratch2025.API.csproj", "Scratch2025.API/"]
COPY ["Scracth2025.DATABASE/Scracth2025.DATABASE.csproj", "Scracth2025.DATABASE/"]

# Restauration
RUN dotnet restore "Scratch2025.API/Scratch2025.API.csproj"

# Copie du reste du code
COPY . .

WORKDIR "/src/Scratch2025.API"
RUN dotnet build "Scratch2025.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Étape 3 : Publication
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Scratch2025.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Étape 4 : Image finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scratch2025.API.dll"]
