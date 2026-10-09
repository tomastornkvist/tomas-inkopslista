// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;
    public int budget { get; private set; }

    // Throws BudgetTooLowException.
    public ShoppingList(string path, int budget)
    {
        if (budget < 50)
            throw new BudgetTooLowException("Den måste vara minst 50 kr.");

        this.path = path;
        this.budget = budget;
    }

    // Returns true if the item is added to the list and
    // false if there's no room for it in the budget.
    public bool Add(Item item)
    {
        if (budget > 0 && Total() + item.Price > budget)
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
        Console.WriteLine($"Budget: {budget} kr");

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    // public void Save()
    public void Save()
    {
        try
        {
            using StreamWriter sw = new(path);

            // Save the budget to the file. Reverse the name and amount, so
            // it won't be seen as an item if it's not supported when read.
            sw.WriteLine($"budget;{budget}");

            foreach (Item item in items)
            {
                sw.WriteLine($"{item.Price};{item.Name}");
            }
        }

        // Catch specific exceptions, not the base class Exception.
        catch (IOException)
        {
            Console.WriteLine($"Ett filsystemfel orsakade att {path} inte kunde sparas.");
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
        // Using try/catch to handle when items.txt doesn't exist.
        try
        {
            using StreamReader sr = new(path);

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(';');

                // We need at least price and name.
                if (parts.Length >= 2)
                {
                    // Load the budget from the file.
                    if (parts[0].ToLower() == "budget")
                    {
                        int tmp;
                        if (int.TryParse(parts[1], out tmp))
                        {
                            if (tmp < 50)
                                throw new BudgetTooLowException(
                                    $"Den måste vara minst 50 kr. Ändra den på första raden i {path}.");
                            budget = tmp;
                        }
                        else
                            budget = 0;

                        continue;
                    }

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
        catch (IOException)
        {
            // Catch all other exceptions from reading the file.
            Console.WriteLine($"Ett filsystemfel orsakade att {path} inte kunde läsas.");
        }
    }
}
