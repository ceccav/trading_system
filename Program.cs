using System.ComponentModel;
using App;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Buffers;
using System.Runtime.InteropServices;
List<User> users = new List<User>(); //creates list for Users
users.Add(new User("demo@user.com", "demo"));
List<Item> items = new List<Item>(); //creates list to store all items
items.Add(new Item("demo@ex.com", "Demo Item", "Just a test"));
List<Trade> trades = new List<Trade>(); //creates list to store trades

//keeps track of current logged in user, null = nobody logged in
User? active_user = null;
int nextTradeId = 1; //trade counter

//controls main loop
bool running = true;

//main menu
while (running)
{
    //if no user is logged in, shows login menu
    if (active_user == null)
    {
        try { Console.Clear(); } catch { }

        Console.WriteLine("WELCOME TO THE TRADINGAPP");
        Console.WriteLine("-----------------");
        Console.WriteLine("Choose an option");
        Console.WriteLine("-----------------");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Create an account");
        Console.WriteLine("3. Quit");

        string userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1":
                LogInUser(); //calls the method to log in
                Debug.Assert(active_user != null); //LogInUser should set an active user.
                break;
            case "2":
                CreateUser(); //calls for method to create a new user
                break;
            case "3":
                running = false; //exit program
                break;
        }
    }
    else
    {
        //if a user is logged in, shows the user menu
        try { Console.Clear(); } catch { }

        Console.WriteLine("Welcome to your tradingspace");
        Console.WriteLine("----------------------------");
        Console.WriteLine("1. Add an item");
        Console.WriteLine("2. See list of items available on the trade market");
        Console.WriteLine("3. Start trading"); //not connected yet
        Console.WriteLine("4. Logout");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddItem(); //add a new item
                break;
            case "2":
                SeeAvailableItems(); //show all items in the market
                break;
            case "3":
                TradeMenu();
                break;
            case "4":
                active_user = null; //log out
                break;
        }

    }

}

/*Method to login user
Asks the user for email and password
searches the users list for a match
if correct, sets active_user. Otherwise shows error message*/
void LogInUser()
{
    try { Console.Clear(); } catch { }
    Console.WriteLine("Enter your email: ");
    string email = Console.ReadLine(); //user input stored in string email

    try { Console.Clear(); } catch { }

    Console.WriteLine("Enter your password");
    string _password = Console.ReadLine(); //user input stored in strin _password

    try { Console.Clear(); } catch { }

    //looks through all users and tries to log in
    foreach (User user in users)
    {
        if (user.TryLogin(email, _password)) //searches the list user for matching credentials
        {
            active_user = user; //if valid user, logs in
            return;
        }
    }

    //if no match was found
    Console.WriteLine("Invalid username or password, try again.");
}


/*Method to create a new user, 
Creates a new user account.
asks the user for their email and to choose a password
Adds the user in the list of users

left to add is checks for duplicates or empty input*/
void CreateUser()
{
    try { Console.Clear(); } catch { }

    Console.WriteLine("Enter your email");
    string newAccountEmail = Console.ReadLine();

    try { Console.Clear(); } catch { }

    Console.WriteLine("Choose your password");
    string newAccountPassword = Console.ReadLine();

    users.Add(new User(newAccountEmail, newAccountPassword));



}


/*
Adds a new item
Lets the logged in user add a new item
asks for name, description and then stores it in the item list
*/
void AddItem()
{

    try { Console.Clear(); } catch { }

    //make sure someone is logged in
    Debug.Assert(active_user != null, "no active user");

    int CountBefore = items.Count; //store count before adding for later check

    Console.WriteLine("Name of item: ");
    string? nameItem = Console.ReadLine();

    try { Console.Clear(); } catch { }

    Console.WriteLine("Description: ");
    string? descriptionItem = Console.ReadLine();


    //create new item, replace null input with empty string
    var newItem = new Item(active_user!.UserEmail, nameItem ?? "", descriptionItem ?? "");

    items.Add(newItem); //add to list

    Console.WriteLine($"Added: {newItem.ID} | {newItem.ItemToTrade}");
    //Console.WriteLine($"Trace count AFFTER = {items.Count}");

    Console.WriteLine("Press ENTER to return..");
    Console.ReadLine();

    //number of items should have increased by 1
    Debug.Assert(items.Count == CountBefore + 1);

}

/*
See all available items
shows all items in the market
if none exists, tells the user and returns
*/
void SeeAvailableItems()
{
    try { Console.Clear(); } catch { }

    if (items.Count == 0)
    {
        Console.WriteLine("No items available for trading");
        Console.WriteLine("Press any key to return");
        Console.ReadLine();
        return;
    }

    Console.WriteLine("All items currently listed on the trade market");
    Console.WriteLine("----------------------------------------------");

    foreach (Item item in items)
    {
        Console.WriteLine($"{item.ID} | {item.OwnerEmail} | {item.ItemToTrade} | {item.Description} ");
    }

    Console.WriteLine("PRESS ENTER TO RETURN TO MENU");
    Console.ReadLine();

}

void TradeMenu()
{
    // vad ska ske i en trade? Min användarmail, item jag ska skicka loop leta item. de som du vill sätta upp som offer, foreach, if offeritem inpu == item.itemto trade. 
    // Kunna se andras --> välja en item, make an offer, spotta ut en lista av mina items, input readline --> tillbaka
    // byta owner, byta status throw an exception. 

    bool runningTrade = true;

    while (runningTrade)
    {
        try { Console.Clear(); } catch { }

        Console.WriteLine("T R A D E  M A R K E T");
        Console.WriteLine("----------------------");
        Console.WriteLine("1. Trade");
        Console.WriteLine("2. See trade requests");
        Console.WriteLine("3. See completed requests");
        Console.WriteLine("4. See your items");
        Console.WriteLine("5. Go back to menu");

        string tradeChoice = Console.ReadLine();

        switch (tradeChoice)
        {
            case "1":
                Trade();
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
                return;
        }
    }

    void Trade()
    {
        try { Console.Clear(); } catch { }
        Debug.Assert(active_user != null, "no active user");

        List<Item> others = new List<Item>(); //list to hold the items of other users

        int i;

        for (i = 0; i < items.Count; i++)
        {
            if (items[i].OwnerEmail != active_user!.UserEmail) //for all the items of other users
            {
                others.Add(items[i]); //add to list others
            }
        }
        if (others.Count == 0)
        {
            Console.WriteLine("No items available on the trademarket"); // if there is no items from other users, message is shown
            Console.ReadLine();
            return;
        }

        //for loop to display others items
        Console.WriteLine("Pick the item you want to begin trading for");
        for (i = 0; i < others.Count; i++)
        {
            Console.WriteLine((i + 1) + ". [" + others[i].ID + "]" + others[i].ItemToTrade + " | " + others[i].Description + " | owner: " + others[i].OwnerEmail);
        }

    }
}