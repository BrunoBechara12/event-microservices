# Event Service
Microsserviço responsável pelo gerenciamento de eventos.


## Arquitetura Hexagonal

Este serviço segue estritamente a **Arquitetura Hexagonal** para isolar o domínio de dependências externas:

- **Domain:** Contém as Entidades, Value Objects, Eventos de Domínio e as interfaces das Portas (Ports/Input e Output). Nenhuma dependência externa.

- **Application:** Contém os Casos de Uso (Use Cases) e orquestra a lógica de negócio.

- **Adapters:**
  - • **Primary (API):** Entry point da aplicação (Controllers).
  - • **Secondary (Data/Messaging):** Implementação dos repositórios, acesso ao banco de dados e publicação de mensagens.

## Tecnologias Utilizadas

- **Runtime:** .NET 8

- **Banco de Dados:** PostgreSQL 15

- **Mensageria:** RabbitMQ + MassTransit

- **Testes:** xUnit + FluentAssertions + Testcontainers (Integração e Unidade)

- **Containerização:** Docker


## Integração com RabbitMQ

O EventService **publica** eventos via RabbitMQ para notificar outros microsserviços sobre mudanças nos dados.

### Eventos Publicados

| Evento | Quando é publicado | Payload |
|--------|-------------------|---------|
| `EventCreated` | Após criação de um novo evento | `{ Id: int, Name: string }` |
| `EventDeleted` | Após exclusão de um evento | `{ Id: int }` |

### Implementação

A publicação é feita através do `MassTransitPublisher`, que implementa a interface `IMessagePublisher`:

```csharp
public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken);
}
```

### Fluxo de Mensageria

```
EventService (Publisher) → RabbitMQ → GuestService (Consumer)
                                    → NotificationService (Consumer)
```

Quando um evento é criado ou excluído:
1. O Use Case executa a operação no banco de dados
2. O Use Case publica a mensagem via `IMessagePublisher`
3. RabbitMQ distribui a mensagem para os consumers registrados
4. GuestService e NotificationService processam a mensagem de forma assíncrona 


 # Como Rodar o Serviço

O serviço está totalmente containerizado. Para executá-lo, você precisa ter o **Docker** e o **Docker Compose** instalados.

## Pré-requisitos

Certifique-se de que as portas `5000`, `5001`, `5432`, `5672` e `15672` não estejam sendo usadas por outros serviços na sua máquina.

## Passos para execução

### 1. Navegue até a pasta do serviço
```bash
cd Services/EventService
```

### 2. Suba os containers (API + Banco de Dados + RabbitMQ) em modo detached:
```bash
docker-compose up -d --build
```

> 💡 O comando `--build` garante que as alterações mais recentes no código sejam recompiladas.

### 3. Verifique se os containers estão rodando:
```bash
docker ps
```

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


### Subir apenas o banco de dados
```bash
docker-compose up -d events.database
```

### Subir apenas a API
```bash
docker-compose up -d events.api
```

---

## Acessando a Aplicação

Após os containers iniciarem, os serviços estarão disponíveis nos seguintes endereços:

| Serviço | URL / Porta | Descrição |
|---------|-------------|-----------|
| **Swagger UI** | `http://localhost:5000/swagger` | Documentação interativa da API |
| **API Base** | `http://localhost:5000` | Endereço base para requisições HTTP |
| **PostgreSQL** | `localhost:5432` | Credenciais: `user: postgres`, `pass: postgres` |
| **RabbitMQ Management** | `http://localhost:15672` | Credenciais: `user: guest`, `pass: guest` |
| **RabbitMQ** | `localhost:5672` | Conexão AMQP |

---

## 🧪 Executando os Testes

Para rodar os testes automatizados (localizados na pasta `tests`), você pode usar a CLI do .NET ou pelo próprio Visual Studio.

### Rodando localmente:
```bash
dotnet test tests/Application.IntegrationTests/Application.IntegrationTests.csproj

```


