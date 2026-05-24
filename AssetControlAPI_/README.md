# AssetControlAPI

API RESTful desenvolvida em **ASP.NET Core 8** para gerenciamento e controle de patrimônios, com autenticação JWT, controle de usuários por perfil e rastreamento completo de movimentações de ativos.

---

## Tecnologias

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core 8 (SQL Server)
- JWT Bearer Authentication
- CsvHelper (importação de CSV)
- Swagger / Swashbuckle
- DotNetEnv (variáveis de ambiente)

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB ou instância completa)
- Git

---

## Configuração e execução

### 1. Clone o repositório

```bash
git clone <url-do-repositorio>
cd AssetControlAPI_
```

### 2. Configure as variáveis de ambiente

Crie um arquivo `.env` na raiz do projeto com base no `.env.example`:

```env
# Conexão Windows Authentication
CONNECTION_STRING=Server=SEU_SERVIDOR;Database=AssetDb_;Trusted_Connection=True;TrustServerCertificate=True

# Chave JWT (mínimo 32 caracteres)
JWT_KEY=sua_chave_secreta_com_pelo_menos_32_chars
```

### 3. Aplique as migrations

```bash
cd AssetControlAPI_
dotnet ef database update
```

### 4. Execute a aplicação

```bash
dotnet run
```

A API estará disponível em `http://localhost:5274`. A documentação Swagger abrirá automaticamente em `/swagger`.

---

## Estrutura do projeto

```
AssetControlAPI_/
├── Controllers/          # Endpoints HTTP
├── Applications/
│   ├── DTOs/             # Objetos de transferência de dados
│   ├── Services/         # Regras de negócio
│   ├── Autenticacao/     # Geração de token JWT e criptografia
│   ├── Mapeamentos/      # Mapeamento de CSV
│   └── Regras/           # Validações de domínio
├── Domains/              # Entidades do banco de dados
├── Interface/            # Contratos dos repositórios
├── Repository/           # Implementações dos repositórios (EF Core)
├── Contexts/             # DbContext do EF Core
└── Exceptions/           # Exceções customizadas
```

---

## Autenticação

A API utiliza **JWT Bearer**. Para acessar endpoints protegidos:

1. Faça login em `POST /api/Autenticacao/login` com NIF e senha.
2. Use o token retornado no header: `Authorization: Bearer <token>`.
3. No primeiro acesso, troque a senha via `PATCH /api/Autenticacao/trocar_senha`.

**Perfis de usuário:**
- `Coordenador` — acesso total (listar usuários, aprovar transferências, ver logs).
- `Responsável` — pode solicitar e responder transferências apenas do seu ambiente.

---

## Endpoints principais

### Autenticação
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/Autenticacao/login` | Login com NIF e senha |
| PATCH | `/api/Autenticacao/trocar_senha` | Troca de senha no primeiro acesso |

### Patrimônios
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/Patrimonio` | Lista todos os patrimônios |
| GET | `/api/Patrimonio/id/{id}` | Busca patrimônio por ID |
| POST | `/api/Patrimonio` | Cadastra patrimônio manualmente |
| PUT | `/api/Patrimonio` | Atualiza patrimônio |
| PATCH | `/api/Patrimonio/tipoPatrimonio/{id}` | Atualiza status do patrimônio |

### Solicitações de Transferência
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/SolicitacaoTransferencia` | Lista todas as solicitações |
| GET | `/api/SolicitacaoTransferencia/id/{id}` | Busca solicitação por ID |
| POST | `/api/SolicitacaoTransferencia` | Cria nova solicitação |
| PATCH | `/api/SolicitacaoTransferencia/{id}/responder` | Aprova ou recusa solicitação |

### Log de Patrimônio
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/LogPatrimonio` | Lista todos os logs |
| GET | `/api/LogPatrimonio/patrimonio/{id}` | Logs de um patrimônio específico *(Coordenador)* |

### Usuários
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/Usuario` | Lista usuários *(Coordenador)* |
| GET | `/api/Usuario/id/{id}` | Busca por ID |
| POST | `/api/Usuario` | Cadastra usuário |
| PUT | `/api/Usuario` | Atualiza usuário *(Coordenador)* |
| PATCH | `/api/Usuario/statusUsuario/{id}` | Ativa/inativa usuário *(Coordenador)* |

### Localização / Área
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/Localizacao` | Lista localizações |
| POST | `/api/Localizacao/criar/` | Cria localização |
| PUT | `/api/Localizacao/atualizar/{guid}` | Atualiza localização |
| GET | `/api/Area` | Lista áreas |
| POST | `/api/Area` | Cria área |

> Demais endpoints para Cidade, Bairro, Endereço, Cargo, TipoUsuário e TipoAlteração seguem o mesmo padrão REST.

---

## Importação de patrimônios via CSV

Envie um arquivo CSV via `POST /api/Patrimonio` (multipart/form-data) com o seguinte formato de colunas:

| Coluna | Descrição |
|--------|-----------|
| `Nº invent.` | Número do patrimônio |
| `Denominação do imobilizado.` | Nome/descrição do item |
| `Dt.incorp.` | Data de incorporação |
| `ValAquis.` | Valor de aquisição |

- Separador: `;`
- Registros duplicados (mesmo número de patrimônio) são ignorados automaticamente.
- Patrimônios importados recebem localização `Sem local` e status `Ativo` por padrão.

---

## Dados obrigatórios no banco (seeds)

Antes de usar o sistema, cadastre os seguintes registros:

- **Localização:** `Sem local` (usada na importação CSV)
- **StatusPatrimonio:** `Ativo`, `Transferido`
- **TipoAlteracao:** `Atualização de dados`, `Transferência`
- **StatusTransferencia:** `Pendente de aprovação.`, `Aprovado`, `Recusado`
- **TipoUsuario:** `Coordenador`, `Responsável`

---

## Fluxo de transferência de patrimônio

```
Usuário solicita transferência
        ↓
Solicitação criada com status "Pendente de aprovação."
        ↓
Responsável do ambiente de origem aprova ou recusa
        ↓
Se aprovado → patrimônio muda de localização e status para "Transferido"
           → log registrado automaticamente
```

---

## Variáveis de configuração (`appsettings.json`)

```json
{
  "Jwt": {
    "Issuer": "AssetControlAPI",
    "Audience": "AssetControlAPIFront",
    "ExpireEmMinutos": 120
  }
}
```

A `JWT_KEY` deve ser definida exclusivamente via variável de ambiente (não no appsettings).
