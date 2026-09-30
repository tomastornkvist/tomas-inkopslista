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
    Console.Write("Välj: ");

    // Use int.TryParse() to avoid a crash when user enters something other than an integer.
    int choice;
    int.TryParse(Console.ReadLine(), out choice);

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        // Make sure the new item has a name.
        name = name.Trim();
        if (name.Length == 0)
        {
            Console.WriteLine("Please enter a valid name for the item.");
            continue;
        }

        Console.Write("Pris: ");

        // Use int.TryParse() to avoid a crash when user enters something other than an integer.
        int price;
        if (int.TryParse(Console.ReadLine(), out price))
        {
            list.Add(new Item(name, price));
        }
        else
        {
            Console.WriteLine("Please enter a valid integer number for the price of the item.");
        }
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        int number = int.Parse(Console.ReadLine());
        list.RemoveAt(number);
    }
    else if (choice == 3)
    {
        list.Save();
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
