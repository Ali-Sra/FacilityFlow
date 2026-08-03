# Architektur

FacilityFlow verwendet einen modularen Monolithen mit Clean-Architecture-Abhängigkeitsrichtung. Domain und Application kennen keine technischen Details. Infrastructure implementiert Datenzugriff. API und Web sind austauschbare Präsentationsschichten.

## Trade-offs

SQLite vereinfacht den Portfolio-Start, ist jedoch kein Ersatz für SQL Server in einer größeren Mehrbenutzerumgebung. Auf CQRS, MediatR und Repository-over-Repository wurde bewusst verzichtet.
