FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY EventManager.WebApi/*.csproj ./EventManager.WebApi/
COPY EventManager.Domain/*.csproj ./EventManager.Domain/
COPY EventManager.Repository/*.csproj ./EventManager.Repository/
COPY EventManager.Services/*.csproj ./EventManager.Services/ 
RUN dotnet restore

# Copy everything else and build
COPY EventManager.WebApi/. ./EventManager.WebApi/
COPY EventManager.Domain/. ./EventManager.Domain/
COPY EventManager.Repository/. ./EventManager.Repository/
COPY EventManager.Services/. ./EventManager.Services/
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "EventManager.WebApi.dll"]
