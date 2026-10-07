// One item on the shopping list.
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
        // Krav 1: Tomt namn kastar ArgumentException
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Namnet på varan kan inte vara tomt.");
        }

        // Krav 2: Negativt pris kastar ArgumentOutOfRangeException
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Priset kan inte vara negativt.");
        }
        
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
