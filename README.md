# FacilityFlow

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Status](https://img.shields.io/badge/Status-Portfolio--MVP-success)
![License](https://img.shields.io/badge/License-MIT-blue)

**FacilityFlow** ist eine webbasierte Anwendung zur Erfassung, Bearbeitung und Auswertung technischer Störungen und Serviceanfragen in Gebäuden. Das Projekt demonstriert eine moderne, nachvollziehbare .NET-Architektur mit REST API, Blazor-Oberfläche, Entity Framework Core, SQLite, Tests, Docker und CI.

> FacilityFlow ist ein unabhängiges Portfolio- und Lernprojekt. Alle verwendeten Personen-, Gebäude-, Raum-, Geräte- und Störungsdaten sind vollständig synthetisch und haben keinen Bezug zu einem realen Unternehmen, einer Behörde oder einer öffentlichen Einrichtung.

## Funktionsumfang

- Serviceanfragen anlegen, anzeigen, bearbeiten und löschen
- Automatisch generierte Ticketnummern (`SR-2026-000001`)
- Kategorien, Prioritäten und Bearbeitungsstatus
- Gebäude- und Raumzuordnung
- Filterung, Suche, Sortierung und Pagination
- Dashboard mit KPIs und statistischen Auswertungen
- Prüfung zulässiger Statusübergänge
- Optimistic Concurrency über `RowVersion`
- SQLite-Datenbank mit synthetischen Seed-Daten
- Swagger/OpenAPI für die REST API
- Blazor Web App mit deutscher Benutzeroberfläche
- Unit- und Integrationstest-Grundlage
- Dockerfile, Docker Compose und GitHub Actions
- Sicherheits-, Architektur- und Berechtigungsdokumentation

## Architektur

```text
FacilityFlow.Domain
        ↑
FacilityFlow.Application
        ↑
FacilityFlow.Infrastructure
       ↗ ↖
FacilityFlow.Api   FacilityFlow.Web
```

- **Domain:** Fachliche Entitäten, Enums und Regeln
- **Application:** DTOs, Schnittstellen und Use-Case-Verträge
- **Infrastructure:** EF Core, SQLite, Repository-Implementierung und Seed-Daten
- **Api:** REST-Endpunkte, Fehlerbehandlung und OpenAPI
- **Web:** Blazor-Benutzeroberfläche und API-Client

## Voraussetzungen

- .NET 10 SDK
- optional: Docker Desktop

## Lokaler Start

```powershell
dotnet restore FacilityFlow.sln
dotnet build FacilityFlow.sln

dotnet run --project src/FacilityFlow.Api
dotnet run --project src/FacilityFlow.Web
```

Standardadressen laut `launchSettings.json`:

- API: `https://localhost:7101`
- Swagger: `https://localhost:7101/swagger`
- Web: `https://localhost:7201`

## Datenbank

Beim ersten Start der API wird die SQLite-Datenbank automatisch erzeugt und mit synthetischen Beispieldaten befüllt.

```text
src/FacilityFlow.Api/facilityflow.db
```

Die Datei wird durch `.gitignore` ausgeschlossen.

## Tests

```powershell
dotnet test FacilityFlow.sln
```

## Docker

```bash
docker compose up --build
```

Web: `http://localhost:8080`  
API/Swagger: `http://localhost:8081/swagger`

```bash
docker compose down
```

Achtung: `docker compose down -v` entfernt persistente Volumes.

## Sicherheit

Das MVP verwendet ausschließlich synthetische Daten. Für einen produktiven Einsatz sind insbesondere Identity, rollenbasierte Autorisierung, Secret Management, ein Reverse Proxy mit TLS und ein externer Datenbankdienst zu ergänzen. Details stehen in [`docs/security-concept.md`](docs/security-concept.md).

## Portfolio-Hinweis

Dieses Repository zeigt bewusst eine verständliche, testbare und erweiterbare Modular-Monolith-Architektur. Auf unnötige Microservices, Event Sourcing und zusätzliche Abstraktionsschichten wurde verzichtet.

## Roadmap

- ASP.NET Core Identity und rollenbasierte Policies
- Audit-Protokollierung
- Datei-Uploads mit Malware-Prüfung
- CSV- und PDF-Export
- SLA-Kalender mit Feiertagen
- SQL Server Deployment
- erweiterte Integrationstests

## Autor

Mohammadali Seraji

## Lizenz

MIT – siehe [LICENSE](LICENSE).
