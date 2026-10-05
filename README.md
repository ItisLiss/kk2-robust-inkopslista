# Fel 1: Krasch i ShoppingList.Load() (IndexOutOfRangeException)
Programmet kraschade när det läste in filen items.txt.

Varför: Koden delade upp hela filen manuellt på \r\n. Filen avslutades med en tom radbrytning vilket gjorde att sista raden var tom (""). När koden försökte köra .Split(';') på en tom rad fanns det inget index 1 (parts[1]), vilket kraschade koden.

Hur jag löste det: Jag bytte till File.ReadAllLines(path) för att hantera radbrytningar korrekt. Sedan lade jag till en if (parts.Length < 2) kontroll inuti loopen som använder continue för att hoppa över tomma eller trasiga rader utan att krascha.

# Fel 2: Krasch när items.txt saknas (FileNotFoundException)
Programmet kraschade direkt vid uppstart om filen items.txt saknades på datorn.

Varför: Load()-metoden försökte läsa filen med File.ReadAllLines(path) utan att först kontrollera om filen faktiskt existerade.

Hur jag löste det: Jag lade till en kontroll med if (!File.Exists(path)) högst upp i Load(). Om filen saknas gör metoden en return så att programmet kan starta med en tom lista istället för att krascha.

# Fel 3: Krasch vid bokstavsinmatning i menyn (FormatException)
Om vi skrev bokstäver, t.ex: abc i menyvalet kraschade programmet direkt.

Varför: Koden använde int.Parse(Console.ReadLine()) som kastar ett FormatException om texten inte är ett giltigt heltal.

Hur jag löste det: Jag ersatte int.Parse med int.TryParse inuti en while-loop. Om inläsningen misslyckas får användaren ett felmeddelande och en ny chans att skriva en siffra utan att programmet avbryts.
