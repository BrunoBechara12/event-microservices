# Guest Service
Microsserviço responsável pelo gerenciamento de convidados de eventos.


## Arquitetura Hexagonal

Este serviço segue estritamente a **Arquitetura Hexagonal** para isolar o domínio de dependências externas:

- **Domain:** Contém as Entidades, Value Objects, Eventos de Domínio e as interfaces das Portas (Ports/Input e Output). Nenhuma dependência externa.

- **Application:** Contém os Casos de Uso (Use Cases) e orquestra a lógica de negócio.

- **Adapters:**
  - • **Primary (API):** Entry point da aplicação (Controllers).
  - • **Secondary (Data/Messaging):** Implementação dos repositórios, acesso ao banco de dados e consumidores de mensagens.

## Tecnologias Utilizadas

- **Runtime:** .NET 8

- **Banco de Dados:** PostgreSQL 15

- **Mensageria:** RabbitMQ + MassTransit

- **Testes:** xUnit + FluentAssertions + Testcontainers (Integração e Unidade)

- **Containerização:** Docker 


## Integração com RabbitMQ

O GuestService **consome** eventos publicados pelo EventService via RabbitMQ para manter a sincronização de dados.

### Eventos Consumidos

| Evento | Descrição |
|--------|-----------|
| `EventCreated` | Quando um evento é criado no EventService, o GuestService cria uma cópia local do evento para manter a integridade referencial |
| `EventDeleted` | Quando um evento é excluído no EventService, o GuestService remove a cópia local correspondente |

### Consumers

- **EventCreatedConsumer:** Recebe a mensagem `EventCreated` e cria/atualiza o evento localmente.
- **EventDeletedConsumer:** Recebe a mensagem `EventDeleted` e remove o evento do banco local.


 # Como Rodar o Serviço

O serviço está totalmente containerizado. Para executá-lo, você precisa ter o **Docker** e o **Docker Compose** instalados.

## Pré-requisitos

Certifique-se de que as portas `5002`, `5003`, `5433`, `5672` e `15672` não estejam sendo usadas por outros serviços na sua máquina.

## Passos para execução

### 1. Navegue até a pasta do serviço
```bash
cd Services/GuestService
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
docker-compose up -d guests.database
```

### Subir apenas a API
```bash
docker-compose up -d guests.api
```

---

## Acessando a Aplicação

Após os containers iniciarem, os serviços estarão disponíveis nos seguintes endereços:

| Serviço | URL / Porta | Descrição |
|---------|-------------|-----------|
| **Swagger UI** | `http://localhost:5002/swagger` | Documentação interativa da API |
| **API Base** | `http://localhost:5002` | Endereço base para requisições HTTP |
| **PostgreSQL** | `localhost:5433` | Credenciais: `user: postgres`, `pass: postgres` |
| **RabbitMQ Management** | `http://localhost:15672` | Credenciais: `user: guest`, `pass: guest` |
| **RabbitMQ** | `localhost:5672` | Conexão AMQP |

---

## 🧪 Executando os Testes

Para rodar os testes automatizados (localizados na pasta `tests`), você pode usar a CLI do .NET ou pelo próprio Visual Studio.

### Rodando localmente:
```bash
dotnet test tests/Application.IntegrationTests/Application.IntegrationTests.csproj
```
