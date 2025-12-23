FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/EventService/src/Adapters.Primary.API/Adapters.Primary.API.csproj", "Services/EventService/src/Adapters.Primary.API/"]
COPY ["Services/EventService/src/Adapters.Secondary.Data/Adapters.Secondary.Data.csproj", "Services/EventService/src/Adapters.Secondary.Data/"]
COPY ["Services/EventService/src/Domain/Domain.csproj", "Services/EventService/src/Domain/"]
COPY ["Services/EventService/src/Application/Application.csproj", "Services/EventService/src/Application/"]
COPY ["Shared/Shared.Common.csproj", "Shared/"]
RUN dotnet restore "./Services/EventService/src/Adapters.Primary.API/Adapters.Primary.API.csproj"
COPY . .
WORKDIR "/src/Services/EventService/src/Adapters.Primary.API"
RUN dotnet build "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adapters.Primary.API.dll"]