# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY ["apps.csproj", "./"]
# RUN dotnet restore 
# #"apps.csproj"
# COPY . .
# WORKDIR "/src/."
# RUN dotnet build "apps.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "apps.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
# coming from outside
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

COPY ./asp_api/publish .
# COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "apps.dll"]