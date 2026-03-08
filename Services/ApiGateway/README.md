# API Gateway

Ponto de entrada centralizado da aplicação, responsável pelo **roteamento de requisições** para os microsserviços internos e pela **autenticação JWT** dos usuários.

## Arquitetura

O API Gateway utiliza o **YARP (Yet Another Reverse Proxy)** da Microsoft para encaminhar requisições aos serviços internos. Além disso, possui um módulo de autenticação próprio com banco de dados dedicado.

```
ApiGateway/
├── Controllers/          # AuthController (registro, login, refresh token)
├── Services/             # AuthService (lógica de autenticação e geração de tokens)
├── Entities/             # User, RefreshToken
├── DTOs/                 # Request/Response DTOs
├── Data/                 # GatewayDbContext + Migrations
├── Extensions/           # MigrationExtensions
├── Middlewares/           # ExceptionMiddleware
├── Program.cs            # Entry point (JWT + YARP + Swagger)
├── appsettings.json      # Configuração de rotas YARP e JWT
└── Dockerfile
```

## Funcionalidades

### Reverse Proxy (YARP)

Todas as requisições autenticadas são roteadas automaticamente para o microsserviço correto:

| Rota | Serviço de Destino |
|------|--------------------|
| `/api/events/{**catch-all}` | EventService |
| `/api/users/{userId}/events/{**catch-all}` | EventService |
| `/api/collaborators/{**catch-all}` | EventService |
| `/api/guests/{**catch-all}` | GuestService |
| `/api/notifications/{**catch-all}` | NotificationService |
| `/api/templates/{**catch-all}` | NotificationService |

> Todas as rotas proxy exigem autenticação JWT (Authorization Policy: `default`).

### Autenticação JWT

- Registro de usuários com hash BCrypt
- Login com geração de Access Token (JWT) e Refresh Token
- Refresh Token com suporte a revogação
- Access Token expira em 60 minutos (configurável)
- Refresh Token expira em 7 dias (configurável)

## Tecnologias Utilizadas

- **Runtime:** .NET 8
- **Reverse Proxy:** YARP 2.2.0
- **Banco de Dados:** PostgreSQL 15
- **Autenticação:** JWT Bearer (HS256) + BCrypt
- **Containerização:** Docker

## API Endpoints

### Autenticação

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| POST | `/api/auth/register` | Registra novo usuário | Não |
| POST | `/api/auth/login` | Realiza login e retorna tokens | Não |
| POST | `/api/auth/refresh` | Renova o Access Token usando Refresh Token | Não |

## Configuração

### appsettings.json

```json
{
  "Jwt": {
    "SecretKey": "super-secret-key-for-jwt-token-generation-min-32-chars!",
    "Issuer": "EventMicroservices.Gateway",
    "Audience": "EventMicroservices",
    "AccessTokenExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  },
  "ConnectionStrings": {
    "GatewayDb": "Host=localhost;Port=5436;Database=gateway;Username=postgres;Password=postgres;"
  }
}
```

## Exemplo de Uso

### Registrar Usuário

```bash
curl -X POST http://localhost:5010/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "João Silva",
    "email": "joao@email.com",
    "password": "senha123"
  }'
```

Resposta:
```json
{
  "message": "User registered successfully",
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "base64-encoded-token...",
    "expiresAt": "2026-03-02T15:00:00Z"
  }
}
```

### Login

```bash
curl -X POST http://localhost:5010/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "joao@email.com",
    "password": "senha123"
  }'
```

### Refresh Token

```bash
curl -X POST http://localhost:5010/api/auth/refresh \
  -H "Content-Type: application/json" \
  -d '{
    "refreshToken": "seu-refresh-token-aqui"
  }'
```

### Acessar rotas protegidas (via proxy)

```bash
curl http://localhost:5010/api/events \
  -H "Authorization: Bearer seu-access-token-aqui"
```

# Como Rodar o Serviço

O serviço está totalmente containerizado. Para executá-lo, você precisa ter o **Docker** e o **Docker Compose** instalados.

## Pré-requisitos

Certifique-se de que a porta `5010` (API) e `5436` (PostgreSQL) não estejam sendo usadas por outros serviços na sua máquina.

## Passos para execução

### 1. Navegue até a raiz do projeto
```bash
cd event-microservices
```

### 2. Suba os containers em modo detached:
```bash
docker-compose up -d --build
```

> O comando `--build` garante que as alterações mais recentes no código sejam recompiladas.

### 3. Verifique se os containers estão rodando:
```bash
docker ps
```

### Subir apenas o Gateway e suas dependências
```bash
docker-compose up -d gateway.api gateway.database
```

> O Gateway depende de `gateway.database`, `events.api`, `guests.api` e `notifications.api`. Para funcionar completamente, todos os serviços devem estar rodando.

### Parar os containers (sem excluir)
```bash
docker-compose stop
```

### Iniciar containers parados
```bash
docker-compose start
```

### Derrubar e remover os containers
```bash
docker-compose down
```
> Remove os containers, mas mantém os volumes (dados do banco são preservados).

### Rodar localmente (sem Docker)

```bash
# Subir apenas o banco de dados
docker-compose up -d gateway.database

# Rodar a API localmente
dotnet run --project Services/ApiGateway/ApiGateway.csproj --urls "http://localhost:5010"
```

## Acessando a Aplicação

Após os containers iniciarem, os serviços estarão disponíveis nos seguintes endereços:

| Serviço | URL / Porta | Descrição |
|---------|-------------|-----------|
| **Swagger UI** | `http://localhost:5010/swagger` | Documentação interativa da API |
| **API Base** | `http://localhost:5010` | Endereço base para requisições HTTP |
| **PostgreSQL** | `localhost:5436` | Credenciais: `user: postgres`, `pass: postgres` |

## Fluxo de Autenticação

```
Cliente → POST /api/auth/login → Gateway (AuthController)
       ← { accessToken, refreshToken, expiresAt }

Cliente → GET /api/events (Header: Authorization: Bearer <token>)
       → Gateway (YARP valida JWT) → EventService
       ← Resposta do EventService
```
