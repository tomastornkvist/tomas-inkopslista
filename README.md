# Shopping list
## Bugs & fixes
1. ShoppingList.Load() - string text = File.ReadAllText(path);
    This needs a try/catch, because we can't guarantee that the file exists. If the file doesn't exist, it's created. I also asked Claude to change the working directory for the debugger from "./bin/Debug/net10.0" to "." and he did.
2. ShoppingList.Load() - items.Add(new Item(parts[1], int.Parse(parts[0])));
    Use int.TryParse() to make sure the price is an integer.
3. ShoppingList.Load() - items.Add(new Item(parts[1], int.Parse(parts[0])));
    Check that there are 2 parts before adding them to the new Item.
4. ShoppingList.Load() - Item number and Item.Name don't appear on screen after ShoppingList.Print()
    Trim away any whitespace that may occur in any of the parts split out from each line.
    I thought about changing '\n' to Environment.NewLine when splitting the file into lines, but that would miss any other whitespace characters that could potentially create a problem.
5. Program.cs (Menu) - int choice = int.Parse(Console.ReadLine());
    Using int.TryParse() instead of int.Parse() to avoid crashing when the user inputs something other than an integer.
6. Program.cs (Add Item block) - int price = int.Parse(Console.ReadLine());
    Using int.TryParse() instead of int.Parse() to avoid crashing when the user inputs something other than an integer for the price.
7. Program.cs (Add Item block) - string name = Console.ReadLine();
    Checking that name isn't empty before moving on.
8. Program.cs (Remove Item block) - int number = int.Parse(Console.ReadLine());
    Using int.TryParse() to avoid crashing when the user enters an invalid number.
9. Program.cs (Remove Item block) - list.RemoveAt(number);
    Added a try/catch block to handle ArgumentOutOfRangeException.
