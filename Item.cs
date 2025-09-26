//name
//description
namespace App;

public class Item
{
    private static int counter = 1; //counter to give each item an uniqe ID
    public string OwnerEmail = ""; // string to store ownerEmail

    public string ItemToTrade = ""; //string to hold the itam to trade

    public string Description; //string to hold the description of the item

    public string ID; //string for id


    /// <summary>
    /// Constructor to create an item object with wet owner, item to trade, description and unique ID
    /// </summary>
    /// <param name="ownerEmail">email to the owner of the item</param>
    /// <param name="itemToTrade">the item the user wants to trade</param>
    /// <param name="description">description of the item</param>
    public Item(string ownerEmail, string itemToTrade, string description)
    {
        OwnerEmail = ownerEmail; //sets owner email
        ItemToTrade = itemToTrade; //sets item to trade
        Description = description; //sets a description
        ID = counter.ToString("D4"); //creates a unique id for example 0001, 0002
        counter++; // for each created object the counter adds one to the count.
    }
}