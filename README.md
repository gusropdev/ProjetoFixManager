# Projeto FixManager

## Visão Geral

O **FixManager** é uma API desenvolvida em **ASP.NET** que tem como objetivo facilitar a administração de ordens de serviço para assistências técnicas. O sistema permite cadastrar e gerenciar clientes, ordens de serviço, dispositivos e peças associadas ao reparo.

## Funcionalidades Atuais

Atualmente, a API conta com um CRUD completo para as principais entidades do sistema:

- **Customer (Cliente)**: Gerenciamento de clientes e suas ordens de serviço.
- **ServiceOrder (Ordem de Serviço)**: Registro e acompanhamento de ordens de serviço, associadas a um cliente.
- **Device (Dispositivo)**: Vinculação de dispositivos a uma ordem de serviço.
- **Part (Peça)**: Registro de peças utilizadas nos reparos.

## Tecnologias Utilizadas

- **Linguagem**: C#
- **Framework**: ASP.NET
- **Banco de Dados**: Entity Framework Core, SQL Server
- **Arquitetura**: RESTful API com Minimal APIs
- **Versionamento**: Git

## Estrutura do Projeto

### `FixManager.Api`

- **`Common/`** - Contém utilitários e configurações compartilhadas.
- **`Data/`** - Configuração do banco de dados e mapeamento das entidades.
- **`Endpoints/`** - Define os endpoints da API, organizados por entidade.
- **`Handlers/`** - Contém a lógica de tratamento das requisições.
- **`Migrations/`** - Contém as migrações do banco de dados.
- **`Program.cs`** - Arquivo principal de inicialização da API.

### `FixManager.Core`

- **`Models/`** - Representação das entidades do domínio.
- **`Requests/`** - Estruturas de requisição para criação e atualização de entidades.
- **`Responses/`** - Estruturas de resposta da API.
- **`Handlers/`** - Contém a lógica de processamento de dados e regras de negócio.
- **`Configuration.cs`** - Configurações gerais do projeto.

## Melhorias Futuras

O projeto ainda está em desenvolvimento, e algumas funcionalidades serão adicionadas nas próximas etapas:

### 🔹 **Integração com Blazor**

- Desenvolvimento de um **dashboard interativo** para gerenciamento dos dados.

### 🔹 **Autenticação e Autorização**

- Implementação de login e controle de acessos para diferentes níveis de usuários.

### 🔹 **Notificações**

- Envio de e-mails ou SMS para clientes quando uma ordem de serviço for atualizada.

### 🔹 **Relatórios e Dashboards**

- Geração de relatórios com informações sobre ordens de serviço, dispositivos e custos.

### 🔹 **Melhorias no Banco de Dados**

- Implementação de migrações e otimizações na estrutura do banco.

## Contribuição

O projeto está em fase inicial e sugestões são muito bem-vindas.

