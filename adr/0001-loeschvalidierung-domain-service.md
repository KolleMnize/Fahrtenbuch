# 0001: Löschvalidierung über Domain Service statt Repository

**Status:** Vorgeschlagen

## Kontext
Die Aggregates `Car`, `Mileage`, `Ride` und `Happening` hängen voneinander ab (`Mileage → Car`, `Ride → Mileage`, `Happening → Mileage`). Beim Löschen muss verhindert werden, dass fachlich sinnlose Zustände entstehen (z. B. ein `Ride` ohne Start-`Mileage`).

Naheliegend wäre, diese Prüfung direkt im Repository zu implementieren, da dort ohnehin auf die Datenbank zugegriffen wird. Das widerspricht aber einem DDD-Grundprinzip: ein Aggregate referenziert andere Aggregates nur über deren ID, nie umgekehrt. `Car` "weiß" also nichts von seinen `Mileage`-Einträgen. Die Frage "darf ich gelöscht werden?" kann folglich kein einzelnes Aggregate für sich beantworten — sie erfordert eine Koordination über mehrere Aggregates hinweg.

## Entscheidung
Wir trennen die Verantwortlichkeiten:

- **Repository** bleibt reine Persistenz: physisches Löschen (`Delete`, generisch in einer Basisklasse) sowie reine Existenz-/Such-Abfragen (`Exists...`, `Find...`). Es trifft keine fachliche Entscheidung.
- **Domain Service** (je Aggregate einer, abgeleitet von einer gemeinsamen Basisklasse) kennt die fachliche Regel, welche Abhängigkeiten ein Löschen blockieren, fragt dafür die Repositories ab und liefert ein `ErrorOr<Deleted>` mit allen gefundenen Blockierungsgründen inkl. konkreter IDs zurück.
- Abhängigkeiten werden generell **blockiert, nicht kaskadiert** — bewusste Entscheidung gegen automatisches Mitlöschen, um versehentlichen Datenverlust zu vermeiden.

## Betrachtete Alternativen
- **Prüfung im Repository selbst:** verworfen, da Repositories damit fachliche Verantwortung übernehmen würden, die eigentlich der Domain gehört, und weil ein Aggregate laut Konvention keine Rückreferenzen auf abhängige Aggregates halten sollte.
- **Kaskadierendes Löschen:** verworfen, da unbeabsichtigter Verlust größerer Datenmengen (z. B. Löschen eines `Car` löscht sämtliche Fahrten und Ereignisse) als zu riskant eingeschätzt wurde.
- **Exceptions statt `ErrorOr`:** verworfen, da ein blockiertes Löschen ein erwarteter, kein außergewöhnlicher Fall ist und die API die Gründe strukturiert an den Client weitergeben soll.

## Konsequenzen
**Positiv:**
- Aggregate-Grenzen bleiben sauber, keine Rückreferenzen nötig.
- Die generische Ablauflogik (Blocker sammeln → Ergebnis bauen → ggf. löschen) ist nur einmal implementiert.
- Der Nutzer erfährt in einem Aufruf alle Gründe, warum ein Löschen blockiert ist, inkl. konkreter IDs — kein iteratives Trial-and-Error.

**Negativ / bewusst in Kauf genommen:**
- Eine zusätzliche Schicht (Domain Service) und zusätzliche Typen (`BlockingReason`) im Vergleich zu einer einfachen Prüfung im Repository.
- Nutzer müssen Abhängigkeiten manuell in der richtigen Reihenfolge löschen (z. B. erst `Ride`/`Happening`, dann `Mileage`, dann `Car`) — es gibt keinen Komfort-Mechanismus zum automatischen Aufräumen.
