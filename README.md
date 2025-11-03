# 🚗 Minimal API - Gestão de Veículos

API RESTful minimalista desenvolvida com .NET 9, focada em boas práticas, testes automatizados e autenticação JWT. Este projeto implementa o CRUD completo de veículos, com integração de Swagger, mocks para testes e injeção de dependência.

## 🧭 Sumário

- [Visão Geral](#-visão-geral)
- [Arquitetura](#-arquitetura)
- [Tecnologias](#-tecnologias)
- [Configuração e Execução](#️-configuração-e-execução)
- [Endpoints Principais](#-endpoints-principais)
- [Autenticação JWT](#-autenticação-jwt)
- [Documentação via Swagger](#-documentação-via-swagger)
- [Testes Automatizados](#-testes-automatizados)
- [Padrão de Commits](#-padrão-de-commits)
- [Licença](#-licença)

## 🚀 Visão Geral

A **Minimal API - Veículos** é uma aplicação desenvolvida em .NET 9, com o objetivo de demonstrar uma arquitetura limpa, testável e segura. Ela permite o cadastro, listagem, atualização e exclusão de veículos, e inclui autenticação via JWT para proteger rotas sensíveis.

O projeto segue princípios como:

- Simplicidade e clareza (Minimal API)
- Segurança com JWT
- Testes de integração e mock de serviços
- Documentação interativa com Swagger

## 🧱 Arquitetura

```
📂 src/
 ┣ 📂 MinimalApi
 ┃ ┣ 📂 Dominio
 ┃ ┃ ┣ 📂 Entidades
 ┃ ┃ ┣ 📂 Interfaces
 ┃ ┃ ┗ 📂 ModelViews
 ┃ ┣ 📂 DTOs
 ┃ ┣ 📂 Servicos
 ┃ ┣ 📂 Config
 ┃ ┣ Program.cs
 ┃ ┗ appsettings.json
 ┗ 📂 Test
   ┣ 📂 Requests
   ┣ 📂 Mocks
   ┗ Test.csproj
```

A arquitetura é baseada em **Domínio → Serviço → API**, respeitando princípios de separação de responsabilidades.

## 🧰 Tecnologias

| Categoria        | Tecnologias                                    |
|------------------|------------------------------------------------|
| Framework        | .NET 9, ASP.NET Core Minimal API               |
| Linguagem        | C# 12                                          |
| Banco de Dados   | Entity Framework Core                          |
| Autenticação     | JWT (JSON Web Token)                           |
| Documentação     | Swagger / Swashbuckle                          |
| Testes           | MSTest + WebApplicationFactory                 |
| Mocks            | Serviços e repositórios em memória             |

## ⚙️ Configuração e Execução

### 1️⃣ Clonar o repositório

```bash
git clone https://github.com/seuusuario/minimal-api-veiculos.git
cd minimal-api-veiculos
```

### 2️⃣ Restaurar dependências

```bash
dotnet restore
```

### 3️⃣ Criar o banco de dados

```bash
dotnet ef database update
```

### 4️⃣ Executar a API

```bash
dotnet run --project MinimalApi
```

A API será iniciada em:  
👉 **http://localhost:5000**

## 🔗 Endpoints Principais

| Método | Rota             | Descrição                        |
|--------|------------------|----------------------------------|
| GET    | /veiculos        | Lista todos os veículos          |
| GET    | /veiculos/{id}   | Retorna um veículo pelo ID       |
| POST   | /veiculos        | Cadastra um novo veículo         |
| PUT    | /veiculos/{id}   | Atualiza um veículo existente    |
| DELETE | /veiculos/{id}   | Remove um veículo                |

### 📤 Exemplo de Requisição - POST /veiculos

```http
POST /veiculos HTTP/1.1
Content-Type: application/json
Authorization: Bearer {token}
```

```json
{
  "nome": "Corolla",
  "marca": "Toyota",
  "ano": 2024
}
```

### 📥 Exemplo de Resposta

```json
{
  "id": 3,
  "nome": "Corolla",
  "marca": "Toyota",
  "ano": 2024
}
```

## 🔐 Autenticação JWT

As rotas protegidas utilizam **JWT (Bearer Token)**. Após realizar login, inclua o token retornado no cabeçalho de suas requisições.

**Configuração Swagger:**

```csharp
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Insira o token JWT aqui"
});
```

No Swagger, clique em **Authorize** e insira:

```
Bearer seu_token_aqui
```

## 📘 Documentação via Swagger

Após iniciar o projeto, acesse:  
👉 **http://localhost:5000/swagger**

Lá você pode visualizar e testar todos os endpoints diretamente pelo navegador.

## 🧪 Testes Automatizados

O projeto contém testes de integração e unidade usando **MSTest** e mocks de serviço.

**Exemplos:**

- `Testar_Listar_Todos_Veiculos()` - Verifica se o endpoint `/veiculos` retorna com sucesso e contém registros
- `Testar_Incluir_Novo_Veiculo()` - Cria um novo veículo via POST e valida o retorno

```bash
dotnet test
```

✅ Todos os testes devem retornar **Status OK** ou **Created**, validando a API ponta a ponta.

## 🧾 Padrão de Commits

Adotamos o padrão **Conventional Commits**:

| Tipo     | Descrição              | Exemplo                                              |
|----------|------------------------|------------------------------------------------------|
| feat     | Nova funcionalidade    | `feat: adiciona autenticação JWT`                    |
| fix      | Correção de bug        | `fix: corrige validação de modelo`                   |
| test     | Testes e Mocks         | `test(request): adiciona testes de requisição para Veículo` |
| refactor | Refatorações           | `refactor: melhora serviço de veículos`              |
| chore    | Tarefas auxiliares     | `chore: ajusta Swagger e configurações`              |

## 📄 Licença

Este projeto está licenciado sob a **MIT License**. Você pode usar, modificar e distribuir livremente.

---

**💡 Autor:** Thiago Natividade  
**📧 Contato:** thiago@example.com  
**🌍 Feito com ❤️ em .NET 9**
