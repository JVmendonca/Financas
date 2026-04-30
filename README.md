## 💰 API de Finanças - .NET 8

**API REST** desenvolvida em **C#**  com **.NET 8** para gerenciamento de finanças pessoais, permitindo o controle de usuários e transações financeiras de forma estruturada e segura.

### Construído com

![badge-dotnet]
![badge-windows]
![badge-vs]
![badge-mysql]
![badge-swagger]

### 🚀 Sobre o projeto

Este projeto foi desenvolvido com foco em boas práticas de desenvolvimento backend, utilizando conceitos como arquitetura em camadas e princípios de **Domain-Driven Design (DDD)**.

A API permite o cadastro de usuários, **autenticação** e **gerenciamento** de **Despesas**, possibilitando **registrar**, **ver**, **atulizar** e **deletar** o usuario ou a despesas criadas

Além disso, a aplicação conta com documentação interativa via Swagger, facilitando o teste e a integração dos endpoints.

🧠 Tecnologias utilizadas
- **C# / .NET 8**
- **Entity Framework Core**
- **MySQL**
- **AutoMapper**
- **FluentValidation**
- **Docker**
- **Swagger**

### ⚙️ Funcionalidades
Cadastro e autenticação de usuários
CRUD de transações financeiras
Validação de dados com FluentValidation
Mapeamento de objetos com AutoMapper
Documentação com Swagger
Estrutura em camadas (Application, Domain, Infrastructure)

### 🏗️ Arquitetura

O projeto foi estruturado em camadas:

Domain: regras de negócio
Application: serviços e casos de uso
Infrastructure: acesso a dados
API: controllers

## ▶️ Como executar o projeto
Pré-requisitos
.NET 8 SDK
Banco de dados (MySQL)
Visual Studio ou VS Code
Passos

Clone o repositório:
git clone https://github.com/JVmendonca/Financas

Entre na pasta:
cd Financas

Configure o banco no appsettings.Development.json

Execute:
dotnet run

📄 Documentação

Acesse no navegador após rodar:
https://localhost:7190/swagger/index.html

👨‍💻 Autor
João Vitor Mendonça
Desenvolvedor Back-end | C# .NET

[badge-dotnet]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-vs]: https://img.shields.io/badge/VISUAL%20STUDIO-%235c2d91?style=for-the-badge
[badge-windows]: https://img.shields.io/badge/WINDOWS-%230078D4?style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
