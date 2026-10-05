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

        for (int i = 1; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
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
        }
        catch
        {
        }

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
        public void Load()
    {
        // För att förhindra att koden kraschar om items.txt inte finns, las en kontroll in
        // Genom return avslutar/avbryter vi Load() och startar om med en tom lista
        if (!File.Exists(path))
        {
            return;
        }

        // Bytt från File.ReadAllText() till File.ReadAllLines().
        // ReadAllLines hanterar både Windows (\r\n)  radbrytningar automatiskt
        // och ger oss en färdig array med alla rader i filen.
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');

            /// ÄNDRING: Lagt till en kontroll av arrayens längd innan vi hämtar värden från parts[0] och parts[1].
            // Anledning: Om det finns tomma rader i slutet av filen kraschar programmet med IndexOutOfRangeException 
            // eftersom det inte finns något index 1. Med if (parts.Length < 2) och continue hoppar vi säkert över tomma rader.
            if (parts.Length < 2)
            {
                continue;
            }

            // parts[0] = pris, parts[1] = namn
            items.Add(new Item(parts[1], int.Parse(parts[0])));
        }
    }
}
