# Shopping list
## Bugs & fixes
* ShoppingList.Load() - string text = File.ReadAllText(path);
    This needs a try/catch, because we can't guarantee that the file exists. If the file doesn't exist, it's created. I also asked Claude to change the working directory for the debugger from "./bin/Debug/net10.0" to "." and he did.
* ShoppingList.Load() - items.Add(new Item(parts[1], int.Parse(parts[0])));
    Check that there are 2 parts and use int.TryParse() to make sure the price is correct before adding them to the new Item.
* ShoppingList.Load() - Item number and Item.Name don't appear on screen after ShoppingList.Print()
    Changed '\n' to Environment.NewLine, so the lines are split on "\r\n" or '\n' depending on what the current system uses as newline.
