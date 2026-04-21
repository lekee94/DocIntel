# Document Intelligence Platform

A cloud-native SaaS platform for uploading, processing, and searching documents using AI. Users upload PDFs, images, and Word files; the system extracts structured data, generates summaries, auto-assigns tags, and enables natural-language search via a RAG (Retrieval-Augmented Generation) pipeline.

This project is built in seven phases, evolving from a local .NET 8 monolith into a multi-region, event-driven microservices platform on Azure.

> **Status:** Phase 0 — Modern .NET Foundation (in progress)
 
---

## Why This Project Exists

Organizations deal with large volumes of unstructured documents — invoices, contracts, resumes, reports — and need a way to extract meaning, organize, and search across them efficiently. This platform automates that workflow end-to-end while serving as a learning vehicle for modern .NET, Azure, and AI integration patterns.
 
---

## Tech Stack

### Current (Phase 0)
- **.NET 8** — ASP.NET Core Minimal APIs
- **MediatR** — CQRS command/query separation
- **FluentValidation** — request validation via MediatR pipeline
- **EF Core 8** — data access, SQLite for local dev, SQL Server in Docker
- **Serilog** — structured logging to console + rolling files
- **xUnit + Moq + FluentAssertions** — unit testing
- **Docker + Docker Compose** — local containerized runtime
### Planned
- **Azure:** App Service → Container Apps, Cosmos DB, Service Bus, Key Vault, AI Search, OpenAI, Front Door, APIM
- **AI:** Azure OpenAI (GPT-4o), Azure Document Intelligence, Semantic Kernel, vector embeddings
- **DevOps:** GitHub Actions, Bicep (IaC), blue-green deployments
- **Observability:** Application Insights, Azure Monitor, Log Analytics
---

## Architecture

Clean Architecture with four layers:

```
┌─────────────────────────────────────────────────────────┐
│  DocIntel.Api           (HTTP entry point, endpoints)   │
├─────────────────────────────────────────────────────────┤
│  DocIntel.Application   (CQRS handlers, DTOs, behaviors)│
├─────────────────────────────────────────────────────────┤
│  DocIntel.Domain        (entities, value objects, rules)│
├─────────────────────────────────────────────────────────┤
│  DocIntel.Infrastructure (EF Core, repositories, I/O)   │
└─────────────────────────────────────────────────────────┘
```

**Dependency rule:** inner layers know nothing about outer layers. `Domain` has zero external package references. `Application` depends on `Domain` only. `Infrastructure` implements interfaces defined in `Application`. `Api` wires everything up.

Architecture diagrams for each phase will live under [`docs/architecture/`](docs/architecture/).
 
---

## Project Structure

```
DocIntel/
├── .editorconfig
├── .gitignore
├── Directory.Build.props
├── DocIntel.sln
├── docker-compose.yml
├── src/
│   ├── DocIntel.Api/            # Minimal API, Program.cs, endpoints
│   ├── DocIntel.Application/    # Commands, queries, handlers, behaviors
│   ├── DocIntel.Domain/         # Entities, value objects, domain exceptions
│   └── DocIntel.Infrastructure/ # EF Core DbContext, repositories, migrations
├── tests/
│   └── DocIntel.Tests/          # xUnit tests for Application layer
└── docs/
    ├── architecture/            # Phase-by-phase diagrams
    └── adr/                     # Architecture Decision Records (Phase 6)
```
 
---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for the containerized setup)
- A code editor: Visual Studio 2022, JetBrains Rider, or VS Code with the C# Dev Kit
  Verify your setup:

```bash
dotnet --version   # should print 8.0.x
docker --version
```

### Run Locally (SQLite)

```bash
# Clone
git clone https://github.com/<your-username>/docintel.git
cd docintel
 
# Restore and build
dotnet restore
dotnet build
 
# Apply EF Core migrations (creates docintel.db)
dotnet ef database update -p src/DocIntel.Infrastructure -s src/DocIntel.Api
 
# Run the API
dotnet run --project src/DocIntel.Api
```

Open [https://localhost:5001/swagger](https://localhost:5001/swagger) to explore the API.

### Run with Docker (SQL Server)

```bash
docker compose up --build
```

API is available at [http://localhost:8080/swagger](http://localhost:8080/swagger). SQL Server runs in a sibling container with a persistent volume.

To reset the database:

```bash
docker compose down -v
docker compose up --build
```
 
---

## Running Tests

```bash
dotnet test
```

Tests cover MediatR handlers and validators in the Application layer. The repository is mocked with Moq — no database required.
 
---

## API Overview

Current endpoints (Phase 0):

| Method | Route               | Purpose                         |
|--------|---------------------|---------------------------------|
| POST   | `/documents`        | Upload a document               |
| GET    | `/documents`        | List documents (paginated)      |
| GET    | `/documents/{id}`   | Get document by id              |
| DELETE | `/documents/{id}`   | Delete a document               |
| POST   | `/users`            | Create a user                   |
| GET    | `/users`            | List users                      |
| GET    | `/users/{id}`       | Get user by id                  |

Full interactive documentation is generated by Swashbuckle and available at `/swagger` when the API is running.
 
---

## Roadmap

| Phase | Focus                               | Certification | Status      |
|-------|-------------------------------------|---------------|-------------|
| 0     | Modern .NET 8 foundation            | —             | In progress |
| 1     | Azure fundamentals, deploy, auth    | AZ-900        | Planned     |
| 2     | Service Bus, Functions, Cosmos DB   | AZ-204        | Planned     |
| 3     | Azure OpenAI, Document Intelligence, RAG | AI-102   | Planned     |
| 4     | VNets, private endpoints, Container Apps | AZ-104   | Planned     |
| 5     | CI/CD, Bicep, blue-green deploys    | AZ-400        | Planned     |
| 6     | Microservices, multi-region, ADRs   | AZ-305        | Planned     |

Each phase ends with a git tag (`post-phase0`, `post-az900`, …) so the history is browsable by milestone.
 
---

## Conventions

- **Commits** reference the task ID from the project plan: `P0-04.3: add UploadDocumentCommand handler`.
- **Branches** follow trunk-based development — short-lived feature branches merged into `main` via PR.
- **Code style** is enforced via `.editorconfig` at the solution root. Warnings are treated as errors.
- **Nullable reference types** are enabled project-wide.
---

## License

TBD (MIT is the likely pick — will confirm before first public tag.)