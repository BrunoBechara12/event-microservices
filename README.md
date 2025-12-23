## Sistema de gerenciamento de eventos baseado em microsserviços e arquitetura hexagonal, desenvolvido em .NET.

## Arquitetura

O projeto é composto por microsserviços independentes, cada um com sua própria responsabilidade, seguindo os princípios da arquitetura hexagonal para garantir alto desacoplamento e testabilidade.

### Microsserviços

- **EventService**: Gerenciamento do ciclo de vida dos eventos
- **AuthService**: Autenticação e autorização com JWT
- **GuestService**: Gerenciamento de convidados e suas respostas
- **NotificationService**: Envio de convites via WhatsApp
- **API Gateway**: Ponto de entrada único para todos os serviços, gerenciando roteamento, autenticação e rate limiting


### Tecnologias Principais

- **.NET Core**: Framework para desenvolvimento dos microsserviços
- **Arquitetura Hexagonal**: Design pattern que separa as camadas de aplicação
- **RabbitMQ**: Sistema de mensageria para comunicação assíncrona entre serviços
- **Docker**: Containerização de todos os serviços
- **JWT**: Autenticação e autorização entre serviços

### Testes
- **xUnit**: Framework de testes unitários e de integração
- **Testcontainers**: Testes de integração com containers reais (PostgreSQL, RabbitMQ)
- **TDD (Test-Driven Development)**: Desenvolvimento guiado por testes
- **Respawn**: Reset de banco de dados entre testes
- **FluentAssertions**: Assertions expressivas para testes

## Funcionalidades

- Criação e gerenciamento de eventos
- Envio de convites em massa via WhatsApp
- Acompanhamento de confirmações/recusas em tempo real

## Como Começar

### Pré-requisitos
- Docker & Docker Compose

### Executando o Projeto
```bash
# Clone o repositório
git clone [url-do-repositorio]

# Navegue até um serviço específico
cd Services/EventService

# Suba os containers
docker-compose up -d --build

# Execute os testes
dotnet test
```


