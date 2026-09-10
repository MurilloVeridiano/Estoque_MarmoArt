# 📦 Estoque MarmoArt API

<div align="center">

API RESTful desenvolvida em **.NET 9** para gerenciamento inteligente de estoque, controle de movimentações (entradas e saídas) e auditoria de Equipamentos de Proteção Individual (EPIs) voltada para o setor de marmoraria.

[![.NET Version](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-blue?style=flat&logo=efcore)](https://learn.microsoft.com/en-us/ef/core/)
[![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow?style=flat)]()

</div>

---

## 🚀 Sobre o Projeto

O **Estoque MarmoArt** foi idealizado para resolver desafios práticos de controle de inventário e conformidade de segurança (EPIs) em ambiente industrial/comercial. O sistema centraliza o fluxo de materiais, permitindo rastrear desde a entrada de chapas e insumos até a distribuição de equipamentos de proteção aos funcionários, garantindo histórico temporal e confiabilidade nos dados.

---

## 🛠️ Tecnologias e Arquitetura

* **Linguagem:** C# 13
* **Plataforma:** .NET 9 (ASP.NET Core Web API)
* **ORM:** Entity Framework Core 9 (Code-First)
* **Banco de Dados:** SQL Server / LocalDB
* **Validação:** FluentValidation
* **Arquitetura:** Separação de responsabilidades orientada a casos de uso (*Use Cases*), Controllers e Domain Entities.

---

## 📋 Funcionalidades Principais

* **Controle de Estoque em Tempo Real:** Consulta instantânea do saldo atual de insumos e materiais disponíveis.
* **Módulo de Movimentações:** 
  * *Entradas:* Registro de chegada de produtos com data, fornecedor, quantidade e responsável pelo recebimento.
  * *Saídas:* Lançamento de retirada de itens vinculando o colaborador, entregador e quantidade.
  * *Gestão:* Estrutura preparada para auditoria, edição e exclusão de lançamentos.
* **Auditoria de EPIs:** Monitoramento específico do fornecimento e entrega de equipamentos de segurança por funcionário.
* **Relatórios e Filtros Temporais:** Capacidade de filtrar dados e movimentações por dia, semana, mês e ano.

---

## 📂 Estrutura do Repositório

```text
MarmorariaProjeto/
├── Comunication/     # Objetos de Transferência de Dados (Requests/Responses)
├── Controller/       # Endpoints da API (REST Controllers)
├── Domain/           # Entidades de Domínio (ItemEstoque, Movimentacoes, etc.)
├── Infrastructure/   # Contexto do Banco de Dados e Persistência (EF Core)
├── Migrations/       # Histórico de migrações do banco de dados
├── UseCases/         # Regras de negócio isoladas (GetAll, GetById, Register, Delete)
└── Program.cs        # Configuração de serviços e pipeline da aplicação
```
---

## ⚙️ Como Executar o Projeto
```text
**Pré-requisitos**
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado na sua máquina.
* SQL Server (ou LocalDB) configurado.

**Passos para rodar localmente**

1. Clone o repositório:
   git clone [https://github.com/MurilloVeridiano/Estoque_MarmoArt.git](https://github.com/MurilloVeridiano/Estoque_MarmoArt.git)

2. Navegue até a pasta do projeto:
   cd MarmorariaProjeto

3.Restaure as dependências do projeto:
  dotnet restore

4.Execute as migrações para criar o banco de dados:
  dotnet ef database update

5. Compile o projeto (opcional):
   dotnet build

6.Inicie a aplicação:
  dotnet run
```
---

## 👨‍💻 Desenvolvedor
Feito com dedicação por Murillo Veridiano. Se este projeto te chamou a atenção, sinta-se à vontade para entrar em contato!
