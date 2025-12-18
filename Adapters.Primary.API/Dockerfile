FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# compiling
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Adapters.Primary.API/Adapters.Primary.API.csproj", "Adapters.Primary.API/"]
COPY ["Adapters.Secondary.Data/Adapters.Secondary.Data.csproj", "Adapters.Secondary.Data/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Shared/Shared.Common.csproj", "Shared/"]
RUN dotnet restore "./Adapters.Primary.API/Adapters.Primary.API.csproj"
COPY . .
WORKDIR "/src/Adapters.Primary.API"
RUN dotnet build "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adapters.Primary.API.dll"]