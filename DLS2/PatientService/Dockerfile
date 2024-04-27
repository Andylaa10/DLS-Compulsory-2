FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DLS2/PatientService/PatientService.csproj", "DLS2/PatientService/"]
COPY ["DLS2/Monitoring/Monitoring.csproj", "DLS2/Monitoring/"]
COPY ["DLS2/Cache/Cache.csproj", "DLS2/Cache/"]
COPY ["DLS2/FeatureHub/FeatureHub.csproj", "DLS2/FeatureHub/"]
COPY ["DLS2/Messaging/Messaging.csproj", "DLS2/Messaging/"]
RUN dotnet restore "./DLS2/PatientService/PatientService.csproj"
COPY . .
WORKDIR "/src/DLS2/PatientService"
RUN dotnet build "./PatientService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientService.dll"]
