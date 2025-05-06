## Sistema de gerenciamento de eventos baseado em microsserviços e arquitetura hexagonal, desenvolvido em .NET.

⚠️ Projeto em fase inicial de desenvolvimento. Atualizações serão compartilhadas regularmente.

## Arquitetura

O projeto é composto por microsserviços independentes, cada um com sua própria responsabilidade, seguindo os princípios da arquitetura hexagonal para garantir alto desacoplamento e testabilidade.

### Microsserviços

- **EventService**: Gerenciamento do ciclo de vida dos eventos
- **AuthService**: Autenticação e autorização com JWT
- **GuestService**: Gerenciamento de convidados e suas respostas
- **NotificationService**: Envio de convites via WhatsApp
- **PaymentService**: Processamento de pagamentos para planos premium

## Tecnologias Principais

- **.NET Core**: Framework para desenvolvimento dos microsserviços
- **Arquitetura Hexagonal**: Design pattern que separa as camadas de aplicação
- **RabbitMQ**: Sistema de mensageria para comunicação assíncrona entre serviços
- **Docker**: Containerização de todos os serviços
- **JWT**: Autenticação e autorização entre serviços

## Funcionalidades

- Criação e gerenciamento de eventos
- Envio de convites em massa via WhatsApp
- Acompanhamento de confirmações/recusas em tempo real
- Plano premium com importação de contatos via planilha
- Gateway de pagamento para assinaturas premium
