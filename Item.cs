//name
//description
namespace App;

public class Item
{
    private static int counter = 1;
    public string OwnerEmail = "";

    public string ItemToTrade = "";

    public string Description;

    public string ID;

    public Item(string ownerEmail, string itemToTrade, string description)
    {
        OwnerEmail = ownerEmail;
        ItemToTrade = itemToTrade;
        Description = description;
        ID = counter.ToString("D4");
        counter++;
    }
}