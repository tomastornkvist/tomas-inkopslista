// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;
    public int budget { get; private set; }

    public ShoppingList(string path, int budget)
    {
        this.path = path;
        this.budget = budget;
    }

    // Returns true if the item is added to the list and
    // false if there's no room for it in the budget.
    public bool Add(Item item)
    {
        if (Total() + item.Price > budget)
            return false;

        items.Add(item);
        return true;
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        // Starting loop at 0 to begin with the first item in the list.
        for (int i = 0; i < items.Count; i++)
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
            // Use Environment.NewLine for line breaks, both here
            // and in Load(), to follow the system standard.
            File.WriteAllText(path, string.Join(Environment.NewLine, lines) + Environment.NewLine);
        }

        // Catch specific exceptions, not just the universal Exception.
        catch (IOException)
        {
            Console.WriteLine("Ett filsystemfel orsakade att filen inte kunde sparas.");
            return;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Det saknas rättigheter för att spara filen.");
            return;
        }

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
    public void Load()
    {
        string text;

        // Added try/catch to handle when items.txt doesn't exist.
        // The catch (Exception e) helped me identify the problem
        // with the empty line at the end of items.txt.
        try
        {
            text = File.ReadAllText(path);

            // Use Environment.NewLine for line breaks, both here
            // and in Save(), to follow the system standard.
            string[] lines = text.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                // We need at least price and name.
                if (parts.Length >= 2)
                {
                    // What if the price is in a wrong format.
                    int price;
                    if (int.TryParse(parts[0], out price))
                    {
                        items.Add(new Item(parts[1], price));
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            // Catch when items.txt doesn't exist.
            Console.WriteLine("Inköpslistan finns inte. Försöker skapa den.");
            Save();
        }
    }
}
