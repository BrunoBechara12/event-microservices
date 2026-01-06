FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/GuestService/src/Adapters.Primary.API/Adapters.Primary.API.csproj", "Services/GuestService/src/Adapters.Primary.API/"]
COPY ["Services/GuestService/src/Adapters.Secondary.Data/Adapters.Secondary.Data.csproj", "Services/GuestService/src/Adapters.Secondary.Data/"]
COPY ["Services/GuestService/src/Domain/Domain.csproj", "Services/GuestService/src/Domain/"]
COPY ["Services/GuestService/src/Application/Application.csproj", "Services/GuestService/src/Application/"]
COPY ["Shared/Shared.Common.csproj", "Shared/"]
RUN dotnet restore "./Services/GuestService/src/Adapters.Primary.API/Adapters.Primary.API.csproj"
COPY . .
WORKDIR "/src/Services/GuestService/src/Adapters.Primary.API"
RUN dotnet build "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adapters.Primary.API.dll"]