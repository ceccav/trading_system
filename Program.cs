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
                Debug.Assert(active_user != null, "no active user");//LogInUser should set an active user.
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
        Console.WriteLine("2. See your items");
        Console.WriteLine("3. Start trading"); //not connected yet
        Console.WriteLine("4. Logout");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddItem(); //add a new item
                break;
            case "2":
                ShowMyItems(); //show all items in the market
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
    Item newItem = new Item(active_user!.UserEmail, nameItem ?? "", descriptionItem ?? "");

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

void ShowMyItems()
{
    Console.WriteLine("---- YOUR ITEMS ----");
    Console.WriteLine("--------------------\n");

    foreach (Item item in items)
    {
        if (item.OwnerEmail == active_user.UserEmail)
        {
            Console.WriteLine($"[{item.ID}]  {item.ItemToTrade} | {item.Description}");
        }
    }

    Console.WriteLine("---------------------");
    Console.WriteLine("Press ENTER to continue..");
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
        Console.WriteLine("4. Go back to menu");

        string tradeChoice = Console.ReadLine();

        switch (tradeChoice)
        {
            case "1":
                TradeItems();
                break;
            case "2":
                TradeRequests();
                break;
            case "3":
                break;
            case "4":
                return;
        }
    }

    void TradeItems()
    {
        try { Console.Clear(); } catch { }
        Debug.Assert(active_user != null, "no active user");

        List<Item> others = new List<Item>(); //list to hold the items of other users
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].OwnerEmail != active_user!.UserEmail) //for all the items of other users
            {
                others.Add(items[i]); //add to list others
            }
        }
        if (others.Count == 0) //if the other users have no items yet, message is shown
        {
            Console.WriteLine("No items available on the trademarket");
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
            return;
        }

        //for loop to display others items
        Console.WriteLine("Pick the item you want to begin trading for");
        Console.WriteLine("-------------------------------------------");
        for (int i = 0; i < others.Count; i++)
        {
            Console.WriteLine((i + 1) + ". [" + others[i].ID + "]" + others[i].ItemToTrade + " | " + others[i].Description + " | owner: " + others[i].OwnerEmail);
        }

        string inputTarget = Console.ReadLine();
        int targetIdx;
        if (!int.TryParse(inputTarget, out targetIdx) || targetIdx < 1 || targetIdx > others.Count) //takes the string input makes it into int targetIdx and checks if the number is under 1 or bigger than amount of items in other users list. Displays error message
        {
            Console.WriteLine("Not a valid choice");
            Console.WriteLine("Press ENTER to continue..");
            Console.ReadLine();
            return;
        }
        Item target = others[targetIdx - 1];

        // list my items 
        List<Item> mine = new List<Item>(); //list for only my items
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].OwnerEmail == active_user!.UserEmail)
            {
                mine.Add(items[i]);
            }
        }
        if (mine.Count == 0)
        {
            Console.WriteLine("you have no items to offer");
            Console.ReadLine();
            return;
        }

        //pick one or more of active users items to trade with
        Console.WriteLine("Pick one of your items to offer.");
        Console.WriteLine("Type the number and press \n");
        for (int i = 0; i < mine.Count; i++)
        {
            Console.WriteLine((i + 1) + ". [" + mine[i].ID + "] " + mine[i].ItemToTrade + " | " + mine[i].Description);
        }

        // offeredIds


        Console.Write("Number: ");
        string s = Console.ReadLine();
        int pick;
        if (!int.TryParse(s, out pick) || pick < 1 || pick > mine.Count)
        {
            Console.WriteLine("Not a valid number.");
            Console.WriteLine("Press ENTER to continue..");
            return;
        }

        string offeredId = mine[pick - 1].ID;

        //send request with one offerId
        List<string> offeredIds = new List<string>();
        offeredIds.Add(offeredId);

        bool ok = Trade.tryMakeTrade(items, trades, active_user!.UserEmail, target.ID, offeredIds, nextTradeId);

        if (ok)
        {
            nextTradeId++;
            Console.WriteLine("Request sent to: " + target.OwnerEmail);
            Console.WriteLine(offeredId);
        }
        else
        {
            Console.WriteLine("Couldn't create a request");

        }

        Console.WriteLine("Press ENTER to continue..");
        Console.ReadLine();



    }
    void TradeRequests()
    {

    }

}