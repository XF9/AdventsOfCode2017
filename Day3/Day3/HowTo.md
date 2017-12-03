# Teil 1
Das habe ich so gelöst, dass ich der Reihe nach drumherum gehe, bis das Ende der Reihe größer ist, als meine Eingabe.

Das heißt, wenn ich zu Letzt nach unten gegangen bin, gehe ich zunächst einen Schritt nach außen und erhöhe meine Laufweite um Zwei (die neue Runde fängt ja eins weiter unten an und geht eins weiter nach oben). Dann errechne ich mir anhand der Laufweite und des letzten Wertes den Minimal-, den Maximal und den Mittelwert für einen Lauf. Im Beispiel wäre so ein Lauf von 14 bis 17 mit der Mitte 15. Der nächste wäre dann von 18 bis 21 mit der Mitte 19 usw.

Sobald der Maximalwert erreicht wurde ergibt sich die Entfernung zu Mitte als Abstand der Eingabe zur Mitte plus die Schicht auf der ich mich gerade befinde :)

# Teil 2
Hier habe ich mir ein kleines Hilfskonstruk „Punkt“ geschrieben. Dann laufe ich nach dem Muster von Teil 1 wieder rund herum und berechne mir den entsprechenden Wert. Diesen Wert trage ich dann mit Position in eine Liste ein und mache mit der nächsten Position weiter.

Zur Berechnung der Werte schaue ich einfach in meine Liste mit Werten und Positionen und rechne alles zusammen was drumherum liegt und in der Liste vorhanden ist.

Das ganze mache ich so lange bis ich einen Wert größer als das gewünschte Ergebnis habe.
