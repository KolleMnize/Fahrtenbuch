Fahrtenbuch
Dieses Projekt entwickelt ein digitales Fahrtenbuch, mit dem Fahrzeuge, Kilometerstände und Fahrten zentral verwaltet werden können.

Über die REST-API lassen sich aktuell:

Fahrzeuge anlegen und abrufen
Kilometerstände zu Fahrzeugen erfassen und abrufen
Fahrten mit Start-Kilometerstand anlegen
Fahrten mit einem End-Kilometerstand abschließen
Ereignisse beziehungsweise Einträge verwalten

Das Backend ist mit ASP.NET Core auf Basis von .NET 10 umgesetzt. Die Anwendung ist in die Bereiche API, Application, Domain und Infrastructure aufgeteilt. Dadurch sind HTTP-Schnittstellen, Anwendungslogik, Domänenregeln und Datenzugriff voneinander getrennt.

Für die Persistenz wird Entity Framework Core verwendet. Im Entwicklungsmodus steht zusätzlich eine OpenAPI-/Swagger-Dokumentation zur Verfügung. Ein Frontend ist als nächster Ausbauschritt vorgesehen.
