ARG buildVersion
ARG buildDate
ARG gitCommit
ARG gitBranch

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomAssemblyPropertiesTest/CustomAssemblyPropertiesTest.csproj", "CustomAssemblyPropertiesTest/"]
RUN dotnet restore "./CustomAssemblyPropertiesTest/CustomAssemblyPropertiesTest.csproj"
COPY . .
WORKDIR "/src/CustomAssemblyPropertiesTest"
RUN dotnet build "./CustomAssemblyPropertiesTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CustomAssemblyPropertiesTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:Version=$buildVersion /p:InformationalVersion=$buildDate+$gitCommit+$gitBranch

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomAssemblyPropertiesTest.dll"]