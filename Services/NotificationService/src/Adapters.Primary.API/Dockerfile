# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/NotificationService/src/Adapters.Primary.API/Adapters.Primary.API.csproj", "Services/NotificationService/src/Adapters.Primary.API/"]
COPY ["Services/NotificationService/src/Adapters.Secondary/Adapters.Secondary.csproj", "Services/NotificationService/src/Adapters.Secondary/"]
COPY ["Services/NotificationService/src/Application/Application.csproj", "Services/NotificationService/src/Application/"]
COPY ["Services/NotificationService/src/Domain/Domain.csproj", "Services/NotificationService/src/Domain/"]
COPY ["Shared/Shared.Common.csproj", "Shared/"]
RUN dotnet restore "./Services/NotificationService/src/Adapters.Primary.API/Adapters.Primary.API.csproj"
COPY . .
WORKDIR "/src/Services/NotificationService/src/Adapters.Primary.API"
RUN dotnet build "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Adapters.Primary.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adapters.Primary.API.dll"]