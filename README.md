# 📦 Estoque MarmoArt API

<div align="center">

API RESTful e Painel Web desenvolvidos em **.NET 9** e **JavaScript/HTML5** para gerenciamento inteligente de estoque, controle de movimentações (entradas e saídas) e autenticação segura voltada para o setor de marmorarias.

[![.NET Version](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-blue?style=flat&logo=efcore)](https://learn.microsoft.com/en-us/ef/core/)
[![JWT Auth](https://img.shields.io/badge/Auth-JWT%20Bearer-black?style=flat&logo=jsonwebtokens)](https://jwt.io/)
[![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow?style=flat)]()

</div>

---

## 🚀 Sobre o Projeto

O **Estoque MarmoArt** é um sistema completo (API Backend + Dashboard Frontend) idealizado para resolver desafios práticos de controle de inventário e gestão de insumos em ambiente industrial e comercial. O sistema centraliza o fluxo de materiais, permitindo rastrear desde o cadastro de produtos até o registro de saídas e movimentações de estoque, garantindo histórico temporal, autenticação segura e alta confiabilidade nos dados.

---

## 🛠️ Tecnologias e Arquitetura

- **Linguagem Backend:** C# 13 (.NET 9 ASP.NET Core Web API)
- **Frontend:** HTML5, CSS3, JavaScript 
- **Segurança & Autenticação:** JWT (JSON Web Tokens) e ASP.NET Core 
- **ORM:** Entity Framework Core 9
- **Banco de Dados:** SQL Server / LocalDB
- **Validação:** FluentValidation
- **Arquitetura:** Separação limpa de responsabilidades orientada a casos de uso (*Use Cases*), DTOs (Data Transfer Objects), Controllers REST e Domain Entities.

---

## 📋 Funcionalidades Principais

- **Autenticação & Segurança:**
  - Login seguro com criptografia de senha via hash.
  - Proteção de rotas via tokens JWT Bearer.
  - Fluxo inteligente de *Bootstrap* (inicialização e criação do primeiro administrador do sistema).
- **Controle de Estoque em Tempo Real:** Consulta instantânea do saldo atual de insumos, cubas, tanques, EPIs e materiais disponíveis.
- **Módulo de Movimentações:**
  - *Entradas:* Registro de chegada de materiais com atualização automática de saldo.
  - *Saídas:* Lançamento de retirada de itens vinculando o responsável e a quantidade.
- **Relatórios e Filtros Temporais:** Capacidade de filtrar dados e movimentações por período.
- **Painel Responsivo:** Interface web limpa para gestão intuitiva das operações de estoque.

---

## 📂 Estrutura do Repositório

```text
MarmorariaProjeto/
├── Comunication/     # Objetos de Transferência de Dados (Requests/Responses DTOs)
├── Controller/       # Endpoints REST da API (Auth, Estoque, Movimentacao)
├── Domain/           # Entidades de Domínio (ItemEstoque, MovimentacaoEstoque, Login)
├── FrontEnd/         # Páginas Web, Estilos CSS e Scripts JavaScript do Dashboard
├── Infrastructure/   # Contexto do Banco de Dados (EF Core), Segurança e Serviços JWT
├── Migrations/       # Histórico de migrações do banco de dados
├── UseCases/         # Regras de negócio isoladas por caso de uso
├── appsettings.example.json # Modelo seguro de configuração
└── Program.cs        # Configuração de serviços, injeção de dependência e pipeline
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

7.Acesse o sistema:
 Abra o navegador no endereço exibido no terminal (ex: https://localhost:5001). No primeiro acesso, o sistema redirecionará para a tela de Bootstrap para você cadastrar a
 conta de administrador inicial.
```
---

## 👨‍💻 Desenvolvedor
Feito com dedicação por Murillo Veridiano. Se este projeto te chamou a atenção, sinta-se à vontade para entrar em contato!
