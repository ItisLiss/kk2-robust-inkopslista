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

# Fel 8: Sökfunktionen hittade inte varor när man använde stora och små bostäver på olika platser.
Om man sökte på en vara med små bokstäver (t.ex. "mjölk") när varan var sparad med stor bokstav ("Mjölk"), rapporterade programmet att varan inte fanns i listan.

Varför: Koden jämförde strängar direkt med item.Name == name. Detta krävde exakt matchning av alla tecken och radbrytningstecken (\r) hamnade i namnet när listan lästes in från fil.

Hur jag löste det: I Load() lade jag till .Trim() på varans namn när det läses in för att rensa bort dolda tecken. I Find() i ShoppingList.cs uppdaterade jag jämförelsen till att rensa mellanslag och konvertera båda strängarna till små bokstäver med .ToLower(). Sökningen hittar nu varor oavsett om man skriver med stora eller små bokstäver.

# Fel 9: Menyval för sparande saknades samt tyst felhantering vid sparning (Missing Menu Logic / Silent Failure)
När användaren valde alternativ 3 i menyn hände ingenting (eller så gavs ingen feedback om att listan sparats). Dessutom användes en tom catch vid filskrivning som fångade fel och påstod att filen sparats även om ett fel uppstod.

Varför: I Program.cs saknades logik för choice == 3. catch-blocket var också tomt utan specifika undantagstyp.

Hur jag löste det: I Program.cs lades hantering till för choice == 3 med ett anrop till list.Save() och en paus med Console.ReadKey() så att användaren hinner läsa meddelandet. I ShoppingList.cs flyttades utskriften in i try-blocket och catch uppdaterades till att fånga specifika undantag som IOException och UnauthorizedAccessException med tydliga felmeddelanden.

# DEL 2

# 1: Validering i Item-konstruktorn (Domain Protection)
Item-klassens konstruktor uppdaterades med valideringsregler för inkommande parametrar.

Om name är tomt eller bara innehåller blanksteg kastas ett ArgumentException.

Om price är mindre än 0 kastas ett ArgumentOutOfRangeException.

Detta för att förhindra skapandet av ogiltiga eller trasiga domänobjekt i minnet (t.ex. varor utan namn eller med negativt pris).

# Steg 2: Budgettak i ShoppingList (Domain Rule)
ShoppingList har fått en Budget-egenskap samt att Add()-metoden ändrades från void till att returnera bool.

Regel: Om Total() + item.Price > Budget returnerar Add() false och varan läggs inte till i listan.  Add() returnerar en bool (true/false) istället för att kasta ett undantag.

Varför: Att överskrida en budget i en inköpslista är inte ett oväntat krasch-fel eller ett systemundantag, utan en helt normal affärsregel (business logic rule). Genom att returnera bool kan Program.cs enkelt fånga resultatet och ge användaren ett tydligt meddelande i gränssnittet utan onödig try-catch-logik kring normala användarval.

# Steg 3: Felhantering och användar-feedback i Program.cs 
Program.cs uppdaterades för att hantera både undantag från Item och bool-returen från ShoppingList.Add().

ArgumentException och ArgumentOutOfRangeException fångas via try-catch. Programmet kraschar inte utan visar ett pedagogiskt felmeddelande till användaren.

Om list.Add() returnerar false informeras användaren om att varan inte kunde läggas till eftersom budgettaket skulle överskridas.

# 2. Designval (Budgettak)
Metoden ShoppingList.Add() returnerar en bool (true/false) istället för att kasta ett undantag.
Detta för att en vara överstiger budgettaket i en inköpslista är inte ett oväntat krasch-fel eller ett systemundantag, utan en helt normal affärsregel i applikationer. Genom att returnera bool signalerar metoden tydligt till Program.cs om varan lades till eller inte, anroparen (Program.cs) kan därefter enkelt informera användaren via ett standardmeddelande i gränssnittet utan att behöva bygga prestandakrävande try-catch-strukturer kring normal användarinteraktion.

# Klassdiagram

+------------------------------------+
|                Item                |
+------------------------------------+
| + Name : string                    |
| + Price : int                      |
+------------------------------------+
| + Item(name: string, price: int)   |
| + ToString() : string              |
+------------------------------------+
^
| 1..* (contains)
+------------------------------------+
|            ShoppingList            |
+------------------------------------+
| - items : List               |
| - path : string                    |
| + Budget : int                     |
+------------------------------------+
| + Add(item: Item) : bool           |
| + RemoveAt(index: int) : bool      |
| + Total() : int                    |
| + Find(query: string) : List |
| + Print() : void                   |
| + Save() : void                    |
| + Load() : void                    |
+------------------------------------+
^
| uses
+------------------------------------+
|              Program               |
+------------------------------------+
| + Main(args: string[]) : void      |
+------------------------------------+
