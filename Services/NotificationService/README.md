# NotificationService

Microserviço de notificações via WhatsApp com **EvolutionAPI**.

## Arquitetura

O serviço segue a arquitetura hexagonal (Ports and Adapters):

```
NotificationService/
├── src/
│   ├── Domain/                    # Entidades, Portas e Contratos
│   │   ├── Entities/              # Notification, MessageTemplate
│   │   ├── Ports/                 # Interfaces (In/Output)
│   │   ├── Contracts/             # DTOs (Input/Output)
│   │   └── Events/                # Eventos de domínio (RabbitMQ)
│   ├── Application/               # Casos de uso
│   │   └── UseCases/              # NotificationUseCase, TemplateUseCase
│   ├── Adapters.Primary.API/      # API REST (Controllers)
│   └── Adapters.Secondary/        # Implementações
│       ├── Context/               # NotificationDbContext
│       ├── Repositories/          # Notification, Template repos
│       ├── WhatsApp/              # EvolutionApiService
│       └── Messaging/             # RabbitMQ Consumers
└── tests/
    └── Application.IntegrationTests/
```

## Funcionalidades

### Envio de Notificações
- Envio de mensagens de texto via WhatsApp
- Suporte a diferentes tipos de notificação:
  - `EventInvitation` - Convite para evento
  - `EventReminder` - Lembrete de evento
  - `EventUpdate` - Atualização de evento
  - `EventCancellation` - Cancelamento de evento
  - `InviteConfirmation` - Confirmação de presença
  - `Custom` - Mensagem personalizada

### Templates de Mensagem
- Templates pré-configurados com placeholders
- CRUD de templates personalizados
- Formatação automática com variáveis

### Integração RabbitMQ
Consome eventos de outros microserviços:
- `GuestInvited` - Envia convite automaticamente
- `GuestConfirmed` - Envia confirmação de presença

### Integração WhatsApp (EvolutionAPI)
- Envio de mensagens de texto
- Envio de mídia (imagens, documentos)
- Verificação de status de conexão
- Obtenção de QR Code para autenticação

## Configuração

### appsettings.json

```json
{
  "ConnectionStrings": {
    "NotificationDb": "Host=localhost;Port=5434;Database=notifications;Username=postgres;Password=postgres"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest"
  },
  "EvolutionApi": {
    "BaseUrl": "http://localhost:8080",
    "ApiKey": "your-api-key-here",
    "InstanceName": "default"
  }
}
```

## API Endpoints

### Notifications

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/notifications/{id}` | Busca notificação por ID |
| GET | `/api/notifications/event/{eventId}` | Lista notificações de um evento |
| GET | `/api/notifications/guest/{guestId}` | Lista notificações de um convidado |
| POST | `/api/notifications` | Envia nova notificação |
| POST | `/api/notifications/{id}/resend` | Reenvia notificação falhada |
| GET | `/api/notifications/whatsapp/status` | Status da conexão WhatsApp |

### Templates

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/templates` | Lista todos os templates |
| GET | `/api/templates/{id}` | Busca template por ID |
| POST | `/api/templates` | Cria novo template |
| PUT | `/api/templates/{id}` | Atualiza template |
| DELETE | `/api/templates/{id}` | Remove template |

## Exemplo de Uso

### Enviar Notificação

```bash
curl -X POST http://localhost:5004/api/notifications \
  -H "Content-Type: application/json" \
  -d '{
    "phoneNumber": "5511999999999",
    "message": "Olá! Esta é uma mensagem de teste.",
    "type": 5,
    "eventId": 1,
    "guestId": 10
  }'
```

### Criar Template

```bash
curl -X POST http://localhost:5004/api/templates \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Boas-vindas",
    "type": 5,
    "template": "Olá {nome}! Bem-vindo ao evento {evento}!"
  }'
```

### Verificar Status WhatsApp

```bash
curl http://localhost:5004/api/notifications/whatsapp/status
```

Resposta quando conectado:
```json
{
  "isConnected": true,
  "qrCode": null
}
```

Resposta quando desconectado (retorna QR Code para scan):
```json
{
  "isConnected": false,
  "qrCode": "base64-encoded-qr-code..."
}
```

## Docker

O serviço está configurado no `docker-compose.yml`:

```bash
# Subir todos os serviços
docker-compose up -d

# Subir apenas o NotificationService
docker-compose up -d notifications.api notifications.database evolution-api evolution.database rabbitmq
```

### Portas

| Serviço | Porta |
|---------|-------|
| notifications.api | 5004 (HTTP), 5005 (HTTPS) |
| notifications.database | 5434 |
| evolution-api | 8080 |
| evolution.database | 5435 |

## Configuração EvolutionAPI

1. Acesse o painel da EvolutionAPI: `http://localhost:8080`
2. Crie uma instância com o nome configurado em `EvolutionApi:InstanceName`
3. Escaneie o QR Code com o WhatsApp
4. Configure a API Key em `EvolutionApi:ApiKey`

## Templates Padrão

O serviço já vem com templates pré-configurados:

| Tipo | Template |
|------|----------|
| EventInvitation | Olá {guestName}! 🎉 Você foi convidado para o evento *{eventName}*. 📅 Data: {eventDate} |
| EventReminder | Lembrete! ⏰ O evento *{eventName}* está chegando! 📅 Data: {eventDate} |
| InviteConfirmation | Obrigado, {guestName}! ✅ Sua presença no evento *{eventName}* foi confirmada. |
| EventCancellation | Atenção, {guestName}! ❌ Infelizmente o evento *{eventName}* foi cancelado. |

## Testes

```bash
cd Services/NotificationService/tests/Application.IntegrationTests
dotnet test
```

Os testes usam:
- **Testcontainers** para PostgreSQL e RabbitMQ
- **Mock** do serviço WhatsApp (não envia mensagens reais)
