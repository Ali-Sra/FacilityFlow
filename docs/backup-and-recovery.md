# Backup und Wiederherstellung

## Development
Die SQLite-Datei wird bei gestoppter Anwendung kopiert. Vor einem Restore wird die aktuelle Datei versioniert gesichert.

## Production-Zielbild
SQL-Server-Full-Backups täglich, differenzielle Backups alle sechs Stunden und Transaktionsprotokoll-Backups alle 15 Minuten. Zielwerte: RPO 15 Minuten, RTO 4 Stunden. Backups werden verschlüsselt, getrennt gespeichert und regelmäßig testweise wiederhergestellt.
