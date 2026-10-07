ShoppingList list = new ShoppingList("items.txt");
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    // Bytte ut int.Parse mot int.TryParse i while loopen för menyinvalen.
    // Så när man matar in bokstäver eller tom text kraschar inte programmet med FormatException,
    // utan ber istället användaren att skriva en giltig siffra tills inmatningen lyckas.
    int choice;
    Console.Write("Välj: ");

    while (!int.TryParse(Console.ReadLine(), out choice))
    {
        Console.Write("Ogiltigt val! Skriv en siffra: ");
    }


    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        // Bytte även ut int.Parse mot int.TryParse vid prisinmatning.
        // Vilket förhindrar FormatException om användaren anger bokstäver eller annat felaktigt format som 14.5 för priset.
        int price;
        Console.Write("Pris: ");

        // Snurrar tills användaren anger ett giltigt heltal
        while (!int.TryParse(Console.ReadLine(), out price))
        {
            Console.Write("Ogiltigt pris! Skriv ett heltal: ");
        }
       /* Console.Write("Pris: ");
        int price = int.Parse(Console.ReadLine()); */
        list.Add(new Item(name, price));
        
    }

    // Bytt ut int.Parse mot int.TryParse vid borttagning av vara.
    // Så om man nu råkar skriva bokstäver istället för ett nummer kraschar inte programmet,
    // utan användaren får ett meddelande och ett nytt försök att skriva en siffra.
    else if (choice == 2)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Listan är tom! Det finns inga varor att ta bort.");
        }
        else
        {
            int number;
            Console.Write($"Ange nummer att ta bort (1-{list.Count}): ");

            // Loopar tills användaren matar in ett giltigt heltal som faktiskt finns i listan
            while (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > list.Count)
            {
                Console.Write($"Ogiltigt nummer! Ange ett nummer mellan 1 och {list.Count}: ");
            }
            // Skicka numret direkt till ShoppingList som sköter -1
            list.RemoveAt(number);
        }
    }
    // lade till hantering för choice == 3 för att kunna anropa list.Save().
    // för att spara och bekräfta att listan är sparad.
    else if (choice == 3)
    {
        list.Save();
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta.");
        Console.ReadKey();
    }

    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
    
}

