Fel 1: Krasch i ShoppingList.Load() (IndexOutOfRangeException)
Programmet kraschade när det läste in filen items.txt.

Varför: Koden delade upp hela filen manuellt på \r\n. Filen avslutades med en tom radbrytning vilket gjorde att sista raden var tom (""). När koden försökte köra .Split(';') på en tom rad fanns det inget index 1 (parts[1]), vilket kraschade koden.

Hur jag löste det: Jag bytte till File.ReadAllLines(path) för att hantera radbrytningar korrekt. Sedan lade jag till en if (parts.Length < 2) kontroll inuti loopen som använder continue för att hoppa över tomma eller trasiga rader utan att krascha.

Fel 2: 