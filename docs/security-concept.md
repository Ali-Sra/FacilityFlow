# Sicherheitskonzept

## Aktueller MVP-Stand

- serverseitige Eingabevalidierung
- EF Core mit parametrisierten Abfragen
- CORS auf bekannte Web-Origin begrenzt
- globale ProblemDetails-Fehlerantworten
- keine Secrets im Repository
- ausschließlich synthetische Daten

## Vor Produktion zwingend

ASP.NET Core Identity, rollenbasierte Policies, sichere Cookies, CSRF-Schutz für schreibende Browseraktionen, Rate Limiting, persistente Data-Protection-Keys, TLS-Termination, Malware-Prüfung für Uploads und zentralisiertes Audit Logging.
