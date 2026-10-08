ShoppingList list = new ShoppingList("items.txt", 100);
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

    // Use int.TryParse() to avoid a crash when user
    // enters something other than an integer.
    int choice;
    int.TryParse(Console.ReadLine(), out choice);

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        Console.Write("Pris: ");

        // Use int.TryParse() to avoid a crash when user
        // enters something other than an integer.
        int price;
        if (int.TryParse(Console.ReadLine(), out price))
        {
            try
            {
                if (!list.Add(new Item(name, price)))
                {
                    Console.WriteLine("Varan lades inte till, eftersom inköpslistan då hade övskridit budgeten.");
                }
            }

            // Catch the ArgumentOutOfRangeException first, because
            // otherwise ArgumentException would catch both of them
            // if the compiler had allowed it.
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Varans pris kan inte vara negativt.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Varan måste ha ett namn.");
            }
        }
        else
        {
            Console.WriteLine("Var vänlig och ange ett heltal som pris på varan.");
        }
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");

        // Use int.TryParse() to avoid a crash when user
        // enters something other than an integer.
        int number;
        if (int.TryParse(Console.ReadLine(), out number))
        {
            // Make sure there's no crash when the user enters the wrong number.
            try
            {
                list.RemoveAt(number);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Varan finns inte i listan.");
            }
        }
        else
        {
            Console.WriteLine("Var vänlig och ange ett giltigt nummer.");
        }
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
