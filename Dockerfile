# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./ ./
RUN dotnet publish -c Release --output ./publish /p:PublishReadyToRun=false --no-self-contained

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000
# EXPOSE 5005

COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "apps.dll"]
