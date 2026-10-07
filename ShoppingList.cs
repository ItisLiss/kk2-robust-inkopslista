// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }
    // Ändring 1:
    // Removes the item the user sees as number 1, 2, 3 ...
    // Lagt till en if-sats som kollar att numret ligger inom listans gränser.
    // Förhindrar ArgumentOutOfRangeException om användaren anger ett nummer som inte finns i listan.
  
    // Ändring 2:
    // Koden sparar ner varan till en variabel innan borttagning.
    // Det som ändrat är att vi kan ge tydligare feedback till användaren genom att skriva ut 
    // exakt vilken vara (namn och pris) som togs bort ur listan.
    public void RemoveAt(int number)
    {
        // Kontrollerar att numret är giltigt
        if (number >= 1 && number <= items.Count)
        {
            Item itemToRemove = items[number - 1];
            items.RemoveAt(number - 1); // Får inte glömma -1, annars blir det tok

            //Skriver ut exakt vilken vara som togs bort, fint som smör.
            Console.WriteLine($"{itemToRemove} har tagits bort ur listan.");
        }
        else
        {
            Console.WriteLine("Ogiltigt nummer! Ingen vara togs bort.");
        }
    }

    // Ger antalet varor i listan (så Program.cs kan kontrollera giltiga nummer)
    public int Count
    {
        get { return items.Count; }
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;
        //Ändrat startindex i for loopen från i = 1 till i = 0.
        // i = 1 hoppades den första varan över 
        // och räknades aldrig med i totalsumman (Höll på att missa).
        for (int i = 0; i < items.Count; i++) //Startar nu på index 0
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        string search = name.Trim().ToLower();

        foreach (Item item in items)
        {
            if (item.Name.Trim().ToLower() == search)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

   // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
            Console.WriteLine("Listan är sparad.");
        }
        // Från början ett helt tomt catch-fält. 
        // Vi fångar nu specifika undantagstyper: IOException och UnauthorizedAccessException.
        catch (IOException ex)
        {
            Console.WriteLine($"Kunde inte spara filen: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Saknar behörighet att spara filen: {ex.Message}");
        }
        // Raden längst ner togs bort så att det inte påstås att sparningen lyckades vid fel.
    }

    // Reads the file back into the list.
    public void Load()
    {
        // För att förhindra att koden kraschar om items.txt inte finns, lades en kontroll in.
        // Genom return avslutar/avbryter vi Load() och startar med en tom lista.
        if (!File.Exists(path))
        {
            return;
        }

        // Bytt från File.ReadAllText() till File.ReadAllLines().
        // ReadAllLines hanterar radbrytningar automatiskt och ger en array med rader.
        try
        {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                // Kontrollera att raden innehåller både pris och namn
                if (parts.Length < 2)
                {
                    continue;
                }

                // Säkra mot FormatException om priset inte är ett tal, och använd den färdiga variabeln 'price'
                if (int.TryParse(parts[0], out int price))
                {
                    // Trim() rensar bort dolda \r och mellanslag
                    items.Add(new Item(parts[1].Trim(), price));
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Kunde inte läsa in filen: {ex.Message}");
        }
    }
}