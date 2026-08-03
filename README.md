# FacilityFlow

[![CI](https://github.com/Ali-Sra/FacilityFlow/actions/workflows/ci.yml/badge.svg)](https://github.com/Ali-Sra/FacilityFlow/actions/workflows/ci.yml)
![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-REST%20API-512BD4)
![Blazor](https://img.shields.io/badge/Blazor-Web%20App-5C2D91)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED)
![Status](https://img.shields.io/badge/Status-Portfolio%20MVP-success)
![License](https://img.shields.io/badge/License-MIT-blue)

**FacilityFlow** ist eine webbasierte Fachanwendung zur Erfassung, Bearbeitung und Auswertung technischer Störungen und Serviceanfragen in Gebäuden.

Das Projekt demonstriert eine moderne und nachvollziehbare .NET-Architektur mit **ASP.NET Core Web API**, **Blazor**, **Entity Framework Core**, **SQLite**, automatisierten Tests, Docker und GitHub Actions.

> **Hinweis:** FacilityFlow ist ein unabhängiges Portfolio- und Lernprojekt. Sämtliche Personen-, Gebäude-, Raum-, Geräte- und Störungsdaten sind synthetisch und haben keinen Bezug zu einem realen Unternehmen, einer Behörde oder einer öffentlichen Einrichtung.

---

## Projektziel

FacilityFlow bildet einen typischen digitalen Bearbeitungsprozess für technische Serviceanfragen ab.

Mitarbeitende können Störungen erfassen, priorisieren, Gebäuden und Räumen zuordnen und den Bearbeitungsstatus nachvollziehen. Ein Dashboard stellt zusätzlich zentrale Kennzahlen und Auswertungen bereit.

Typische Anwendungsfälle:

- IT- und Netzwerkstörungen
- Elektrische Defekte
- Heizungs- und Sanitärprobleme
- Aufzugsstörungen
- Gebäudetechnische Serviceanfragen
- Sicherheits- und Reinigungsmeldungen
- Allgemeine Reparatur- und Wartungsanforderungen

---

## Funktionsumfang

- Serviceanfragen anlegen, anzeigen, bearbeiten und löschen
- Automatisch generierte Ticketnummern wie `SR-2026-000001`
- Kategorien, Prioritäten und Bearbeitungsstatus
- Gebäude- und Raumzuordnung
- Freitextsuche
- Filterung, Sortierung und Pagination
- Dashboard mit KPIs und statistischen Auswertungen
- Prüfung zulässiger Statusübergänge
- Optimistic Concurrency über `RowVersion`
- SQLite-Datenbank mit synthetischen Seed-Daten
- Swagger/OpenAPI für die REST API
- Blazor Web App mit deutscher Benutzeroberfläche
- Unit Tests und Integrationstests
- Dockerfile und Docker Compose
- GitHub Actions für Build und Tests
- Architektur-, Sicherheits- und Deployment-Dokumentation

---

## Tech Stack

| Bereich | Technologie |
|---|---|
| Programmiersprache | C# |
| Framework | .NET 10 |
| Backend | ASP.NET Core Web API |
| Frontend | Blazor Web App |
| Datenzugriff | Entity Framework Core |
| Datenbank | SQLite |
| API-Dokumentation | Swagger / OpenAPI |
| Unit Tests | xUnit |
| Integrationstests | WebApplicationFactory |
| Containerisierung | Docker / Docker Compose |
| Continuous Integration | GitHub Actions |
| Architektur | Modularer Monolith / Clean Architecture |

---

## Architektur

```text
Browser
   │
   ▼
FacilityFlow.Web
Blazor Web App
   │
   │ HTTP / JSON
   ▼
FacilityFlow.Api
ASP.NET Core REST API
   │
   ▼
FacilityFlow.Application
DTOs, Schnittstellen und Use Cases
   │
   ▼
FacilityFlow.Domain
Entitäten, Enums und Fachregeln
   ▲
   │
FacilityFlow.Infrastructure
EF Core, SQLite, Services und Seed-Daten
```

### Projektstruktur

```text
FacilityFlow/
│
├── src/
│   ├── FacilityFlow.Domain
│   ├── FacilityFlow.Application
│   ├── FacilityFlow.Infrastructure
│   ├── FacilityFlow.Api
│   └── FacilityFlow.Web
│
├── tests/
│   ├── FacilityFlow.UnitTests
│   └── FacilityFlow.IntegrationTests
│
├── docs/
│   ├── architecture.md
│   ├── security-concept.md
│   ├── authorization-matrix.md
│   ├── backup-and-recovery.md
│   ├── api-documentation.md
│   ├── testing-strategy.md
│   ├── deployment.md
│   ├── library-licenses.md
│   └── github-issues.md
│
├── docker-compose.yml
├── FacilityFlow.sln
└── README.md
```

### Verantwortlichkeiten der Schichten

#### `FacilityFlow.Domain`

Enthält die fachlichen Kernobjekte und Regeln:

- Entitäten
- Enums
- Statusregeln
- fachliche Validierungslogik
- keine Abhängigkeit zu Web, Datenbank oder Infrastructure

#### `FacilityFlow.Application`

Enthält Anwendungslogik und Verträge:

- DTOs
- Service-Schnittstellen
- Use-Case-Verträge
- Query- und Command-Modelle
- Pagination-Modelle

#### `FacilityFlow.Infrastructure`

Enthält technische Implementierungen:

- Entity Framework Core
- SQLite
- `DbContext`
- Datenbankkonfiguration
- Seed-Daten
- Serviceimplementierungen

#### `FacilityFlow.Api`

Stellt die REST API bereit:

- Controller
- Swagger/OpenAPI
- Health Endpoint
- Dependency Injection
- zentrale HTTP-Schnittstelle

#### `FacilityFlow.Web`

Enthält die Blazor-Oberfläche:

- Dashboard
- Listenansicht
- Formulare
- API-Client
- deutsche Benutzeroberfläche

---

## Architekturentscheidungen

FacilityFlow wurde bewusst als **modularer Monolith** umgesetzt.

Vorteile dieser Entscheidung:

- klare Trennung fachlicher und technischer Verantwortlichkeiten
- einfacher lokaler Betrieb
- überschaubare Deployment-Struktur
- gute Testbarkeit
- nachvollziehbare Erweiterbarkeit
- geringere Komplexität als bei einer Microservice-Architektur

Auf unnötige Komplexität wurde bewusst verzichtet. Aktuell nicht eingesetzt werden:

- Microservices
- Event Sourcing
- CQRS-Frameworks
- zusätzliche Repository-Abstraktionen ohne konkreten Nutzen
- unnötig komplexe Domain Events

Das Ziel ist eine Anwendung, die **verständlich, ausführbar, testbar und erweiterbar** bleibt.

---

## Voraussetzungen

Für die lokale Ausführung werden benötigt:

- .NET 10 SDK
- Git
- optional Docker Desktop
- optional Visual Studio oder Visual Studio Code

Installierte .NET-Version prüfen:

```powershell
dotnet --version
```

---

## Lokaler Start

Repository klonen:

```powershell
git clone https://github.com/Ali-Sra/FacilityFlow.git
cd FacilityFlow
```

Abhängigkeiten wiederherstellen:

```powershell
dotnet restore FacilityFlow.sln
```

Projekt kompilieren:

```powershell
dotnet build FacilityFlow.sln
```

API starten:

```powershell
dotnet run --project src/FacilityFlow.Api
```

In einem zweiten Terminal die Web-Anwendung starten:

```powershell
dotnet run --project src/FacilityFlow.Web
```

Standardadressen laut `launchSettings.json`:

- API: `https://localhost:7101`
- Swagger: `https://localhost:7101/swagger`
- Web: `https://localhost:7201`

Die tatsächlich verwendeten Ports werden beim Start im Terminal ausgegeben.

---

## Start mit Docker

Docker Desktop muss gestartet sein.

Anwendung erstellen und starten:

```powershell
docker compose up --build
```

Alternativ im Hintergrund:

```powershell
docker compose up --build -d
```

Danach stehen die Anwendungen unter folgenden Adressen zur Verfügung:

- Web: `http://localhost:8080`
- API: `http://localhost:8081`
- Swagger: `http://localhost:8081/swagger`

Containerstatus anzeigen:

```powershell
docker compose ps
```

Logs anzeigen:

```powershell
docker compose logs -f
```

Anwendung stoppen:

```powershell
docker compose down
```

Container und Datenbank-Volume löschen:

```powershell
docker compose down -v
```

> **Achtung:** Der Parameter `-v` entfernt das persistente Docker-Volume und damit die lokale Demo-Datenbank.

---

## Datenbank

FacilityFlow verwendet im aktuellen MVP eine SQLite-Datenbank.

Beim ersten Start der API wird die Datenbank automatisch erzeugt und mit synthetischen Beispieldaten befüllt.

Lokale Datenbankdatei:

```text
src/FacilityFlow.Api/facilityflow.db
```

Im Docker-Betrieb wird die Datenbank in einem persistenten Volume gespeichert:

```text
facilityflow-data
```

Die Datenbankdatei wird durch `.gitignore` vom Repository ausgeschlossen.

---

## API

Swagger ist im Development-Modus verfügbar:

```text
http://localhost:8081/swagger
```

Beispielhafte Endpunkte:

```text
GET    /api/service-requests
GET    /api/service-requests/{id}
POST   /api/service-requests
PUT    /api/service-requests/{id}
DELETE /api/service-requests/{id}
GET    /api/service-requests/dashboard
GET    /health
```

Weitere Informationen:

[`docs/api-documentation.md`](docs/api-documentation.md)

---

## Tests

Alle Tests ausführen:

```powershell
dotnet test FacilityFlow.sln
```

### Unit Tests

Die Unit Tests prüfen fachliche Regeln ohne Datenbank oder Webserver.

Beispiele:

- erlaubte Statusübergänge
- nicht erlaubte Statusübergänge
- fachliche Validierungslogik

### Integrationstests

Die Integrationstests starten die API in einer Testumgebung.

Beispiele:

- Erreichbarkeit des Health Endpoints
- HTTP-Verhalten der Anwendung
- Zusammenspiel mehrerer Anwendungsschichten

---

## Continuous Integration

Bei jedem Push und Pull Request wird über GitHub Actions automatisch eine CI-Pipeline ausgeführt.

Die Pipeline umfasst:

- Checkout des Repositorys
- Installation des .NET SDK
- Restore
- Build
- Unit Tests
- Integrationstests

Workflow-Datei:

```text
.github/workflows/ci.yml
```

Der aktuelle Status wird über das CI-Badge am Anfang dieses README angezeigt.

---

## Sicherheit

Das aktuelle MVP verwendet ausschließlich synthetische Daten.

Bereits berücksichtigt:

- keine realen Unternehmens- oder Behördendaten
- keine Secrets im Repository
- `.env.example` nur mit Platzhaltern
- Datenbankdateien in `.gitignore`
- Validierung fachlicher Eingaben
- Trennung von UI, API, Anwendung und Datenzugriff
- zentrale Fehlerbehandlung
- Docker-Volumes für persistente Daten

Für einen produktiven Einsatz müssten unter anderem folgende Funktionen ergänzt werden:

- ASP.NET Core Identity
- rollenbasierte und policybasierte Autorisierung
- HTTPS über Reverse Proxy
- sicheres Secret Management
- Account Lockout
- Audit-Protokollierung
- Rate Limiting
- Security Headers
- externe produktive Datenbank
- Backup- und Restore-Automatisierung
- Monitoring und zentrales Logging

Details:

[`docs/security-concept.md`](docs/security-concept.md)

---

## Statusmodell

Serviceanfragen durchlaufen definierte Bearbeitungszustände.

```text
Neu
  ↓
Zugewiesen
  ↓
InBearbeitung
  ├── WartetAufMaterial
  ├── WartetAufRueckmeldung
  └── Geloest
        ↓
    Geschlossen
```

Nicht erlaubte direkte Übergänge werden durch fachliche Regeln verhindert.

Beispiel:

```text
Neu → Geschlossen
```

Dieser Übergang ist ohne vorherige Bearbeitung nicht zulässig.

---

## Dokumentation

| Dokument | Inhalt |
|---|---|
| [`architecture.md`](docs/architecture.md) | Architektur und technische Entscheidungen |
| [`security-concept.md`](docs/security-concept.md) | Sicherheitskonzept |
| [`authorization-matrix.md`](docs/authorization-matrix.md) | geplante Rollen und Berechtigungen |
| [`backup-and-recovery.md`](docs/backup-and-recovery.md) | Backup- und Wiederherstellungsstrategie |
| [`api-documentation.md`](docs/api-documentation.md) | API-Beschreibung |
| [`testing-strategy.md`](docs/testing-strategy.md) | Teststrategie |
| [`deployment.md`](docs/deployment.md) | Deployment- und Docker-Hinweise |
| [`library-licenses.md`](docs/library-licenses.md) | Bibliotheken und Lizenzen |
| [`github-issues.md`](docs/github-issues.md) | geplante Weiterentwicklung |

---

## Bekannte Einschränkungen

Das Repository stellt aktuell ein Portfolio-MVP dar.

Noch nicht vollständig umgesetzt:

- Benutzeranmeldung
- Rollen und Berechtigungen
- Audit-Protokollierung
- Datei-Uploads
- PDF-Export
- CSV-Export
- Benachrichtigungen
- vollständige SLA-Berechnung
- produktive SQL-Server-Konfiguration
- automatisches Deployment
- vollständige UI-Tests
- produktionsreifes Monitoring

Diese Funktionen sind bewusst Teil der Roadmap und keine verdeckten Fehler.

---

## Roadmap

### Version 1.1

- Verbesserung der Benutzeroberfläche
- Screenshots im README
- zusätzliche Unit Tests
- zusätzliche Integrationstests
- einheitliche Fehlerseiten
- Erweiterung des Dashboards

### Version 1.2

- ASP.NET Core Identity
- Login und Logout
- Rollen `Administrator`, `Dispatcher`, `Techniker`, `Analyst` und `Viewer`
- policybasierte Autorisierung
- Audit-Protokollierung
- Statushistorie

### Version 1.3

- Datei-Uploads
- CSV-Export
- PDF-Berichte
- SLA-Berechnung
- Benachrichtigungen
- erweiterte Auswertungen

### Version 2.0

- SQL Server
- produktives Deployment
- Reverse Proxy mit TLS
- zentrales Logging
- Monitoring
- automatisierte Backups
- Cloud-Deployment

---

## Git-Strategie

Verwendete Branch-Struktur:

```text
main
feature/*
fix/*
docs/*
```

Beispiele:

```text
feature/authentication
feature/dashboard
feature/audit-log
fix/status-transition-validation
docs/security-concept
```

Commit-Nachrichten orientieren sich an Conventional Commits:

```text
feat: add service request creation
fix: repair integration tests
test: add status transition tests
docs: improve deployment documentation
ci: add github actions workflow
```

---

## Portfolio-Ziel

FacilityFlow soll zeigen, wie eine typische Unternehmensanwendung strukturiert umgesetzt werden kann.

Das Projekt demonstriert insbesondere:

- saubere Trennung von Verantwortlichkeiten
- Verständnis für Backend- und Frontend-Entwicklung
- API-Design
- Datenbankzugriff
- fachliche Regeln
- Testautomatisierung
- Containerisierung
- CI/CD-Grundlagen
- technische Dokumentation
- sicherheitsbewusstes Arbeiten

Das Repository ist nicht als fertiges kommerzielles Produkt gedacht, sondern als nachvollziehbarer technischer Nachweis für Bewerbungen und Vorstellungsgespräche.

---

## Haftungsausschluss

FacilityFlow ist ein unabhängiges Portfolio- und Lernprojekt.

Alle verwendeten Personen-, Gebäude-, Raum-, Geräte- und Störungsdaten sind vollständig synthetisch und haben keinen Bezug zu einem realen Unternehmen, einer Behörde oder einer öffentlichen Einrichtung.

Das Projekt enthält keine internen, vertraulichen oder nicht öffentlichen Informationen einer realen Organisation.

---

## Autor

**Mohammadali Seraji**

- GitHub: [Ali-Sra](https://github.com/Ali-Sra)

---

## Lizenz

Dieses Projekt steht unter der MIT-Lizenz.

Weitere Informationen:

[LICENSE](LICENSE)
