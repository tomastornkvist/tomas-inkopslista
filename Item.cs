// One item on the shopping list.
class Item
{
    public string Name { get; }
    public int Price { get; }

    public Item(string name, int price)
    {
        name = name.Trim();

        if (name.Trim().Length <= 0)
        {
            throw new ArgumentException("Empty item name is not allowed", "name");
        }

        if (price < 0)
        {
            throw new ArgumentOutOfRangeException("price", "Item price can not be negative");
        }

        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
