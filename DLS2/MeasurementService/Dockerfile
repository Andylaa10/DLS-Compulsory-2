FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DLS2/MeasurementService/MeasurementService.csproj", "DLS2/MeasurementService/"]
COPY ["DLS2/Monitoring/Monitoring.csproj", "DLS2/Monitoring/"]
COPY ["DLS2/Cache/Cache.csproj", "/DLS2/Cache/Cache.csproj"]
RUN dotnet restore "./DLS2/MeasurementService/MeasurementService.csproj"
COPY . .
WORKDIR "/src/DLS2/MeasurementService"
RUN dotnet build "./MeasurementService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MeasurementService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeasurementService.dll"]

