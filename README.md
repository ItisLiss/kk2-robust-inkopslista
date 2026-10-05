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

# Fel 4: Krasch vid felinmatning vid prissättning (FormatException)
När man lade till en vara och skrev bokstäver eller ogiltligt värde som pris så kraschade programmet.

Varför: int.Parse misslyckades med att omvandla textsträngen till ett heltal.

Hur jag löste det: Ersatte int.Parse med int.TryParse i en till while-loop som Fel: 3, som kräver att ett giltigt heltal matas in innan varan skapas och läggs till i listan.

# Fel 5: Krasch vid bokstavsinmatning vid borttagning av vara (FormatException)
Om jag skrev bokstäver när man skulle välja vilken vara som skulle tas bort kraschade programmet.

Vilket skedde pga att koden använde int.Parse() på inmatningen från Console.ReadLine(), vilket kastar ett FormatException om texten inte är ett heltal.

Hur jag löste det: Jag ersatte int.Parse() med en while-loop och int.TryParse() i Program.cs. Om inmatningen inte är ett giltigt heltal ber programmet användaren att försöka igen istället för att krascha.

# Fel 6: Krasch vid val av nummer som saknas i listan (ArgumentOutOfRangeException)
När användaren skrev ett nummer som inte motsvarade en vara i listan (t.ex. 68, 0 eller ett negativt tal) kraschade programmet.

Metoden items.RemoveAt() anropades direkt utan att kontrollera om det valda itemet faktiskt existerade i listan.

Hur jag löste det: Jag lade till en villkorsstyrd kontroll if (number >= 1 && number <= items.Count) i ShoppingList.cs. Om numret är giltigt tas varan bort med number - 1 (*minns inte vad det heter när man behöver ange -1*). Om numret ligger utanför intervallet visas ett tydligt felmeddelande och programmet körs vidare tryggt.

# Fel 7: Felaktig totalsumma
Totalsumman för alla varor i listan stämde inte utan var lägre än den faktiska summan pga att första summan inte räknades med.

Varför: ShoppingList.cs startade på index i = 1 istället för i = 0. Det gjorde att priset för den allra första varan i listan aldrig lades till i summan.

Hur jag löste det: Jag ändrade startvärdet i for-loopen från int i = 1 till int i = 0 så att beräkningen omfattar samtliga varor i listan.