# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
EXPOSE 8080
WORKDIR "/app"

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
# src/src/Threads.Api
WORKDIR "/src"
COPY ["src/BalazsGarazs.Api/BalazsGarazs.Api.csproj", "src/BalazsGarazs.Api/"]
RUN dotnet restore "./src/BalazsGarazs.Api/BalazsGarazs.Api.csproj"
COPY [".", "."]
RUN dotnet publish "./src/BalazsGarazs.Api/BalazsGarazs.Api.csproj" -c $BUILD_CONFIGURATION -o "/app/publish" /p:UseAppHost=false

FROM base AS final
WORKDIR "/app"
COPY --from=build ["/app/publish/", "."]
ENTRYPOINT ["dotnet", "BalazsGarazs.Api.dll"]

