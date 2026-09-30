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

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
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
        string text;

        // Added try/catch to handle when items.txt doesn't exist.
        // The catch (Exception e) helped me identify the problem
        // with the empty line at the end of items.txt.
        try
        {
            text = File.ReadAllText(path);
            string[] lines = text.Split('\n');

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                // Empty lines won't work.
                if (parts.Length >= 2)
                {
                    // What if the price is in a wrong format.
                    int price;
                    if (int.TryParse(parts[0], out price))
                        items.Add(new Item(parts[1], price));
                }
            }
        }
        catch (FileNotFoundException)
        {
            // Catch when items.txt doesn't exist.
            Console.WriteLine("Inköpslistan finns inte. Försöker skapa den.");
            Save();
        }
        catch (Exception e)
        {
            // Catch unknown exceptions, so they can be reported and handled.
            Console.WriteLine($"Ett oväntat fel har inträffat vid läsning av inköpslistan: {e.Message}");
        }
    }
}
