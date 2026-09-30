# Shopping list
## Bugs & fixes
1. ShoppingList.Load() - string text = File.ReadAllText(path);
    This needs a try/catch, because we can't guarantee that the file exists. If the file doesn't exist, it's created. I also asked Claude to change the working directory for the debugger from ".\bin\Debug\net10.0" to "." and he added Properties\launchSettings.json, that sets "workingDirectory" to "$(ProjectDir)", so I can manage items.txt in the project folder instead of deep in a debug folder.
2. ShoppingList.Load() - items.Add(new Item(parts[1], int.Parse(parts[0])));
    Use int.TryParse() to make sure the price is an integer.
3. ShoppingList.Load() - items.Add(new Item(parts[1], int.Parse(parts[0])));
    Check that there are 2 parts before adding them to the new Item.
4. ShoppingList.Load()/Save() - Item number and Item.Name don't appear on screen after ShoppingList.Print()
    Using Environment.NewLine for line breaks, to follow the system standard and to make sure it's the same when saving and loading the file.
5. ShoppingList.Save() - File.WriteAllText(), Empty catch
    Since I would have to cheat and remove or change the access rights to the working directory while the application is running to trigger an exception for File.WriteAllText(), I didn't bother to trigger it. Instead I asked Claude for the most common exceptions from the call.
6. Program.cs (Menu) - int choice = int.Parse(Console.ReadLine());
    Using int.TryParse() instead of int.Parse() to avoid crashing when the user inputs something other than an integer.
7. Program.cs (Add Item block) - int price = int.Parse(Console.ReadLine());
    Using int.TryParse() instead of int.Parse() to avoid crashing when the user inputs something other than an integer for the price.
8. Program.cs (Add Item block) - string name = Console.ReadLine();
    Checking that name isn't empty before moving on.
9. Program.cs (Remove Item block) - int number = int.Parse(Console.ReadLine());
    Using int.TryParse() to avoid crashing when the user enters an invalid number.
10. Program.cs (Remove Item block) - list.RemoveAt(number);
    Added a try/catch block to handle ArgumentOutOfRangeException.
11. ShoppingList.Total() - The total sum wasn't calculated correctly.
    Starting the for-loop at 0 instead of 1, since arrays and lists use 0-based index.
