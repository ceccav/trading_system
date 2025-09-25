using System.ComponentModel;
using App;
using System.Diagnostics;
List<User> users = new List<User>();
users.Add(new User("admin", "admin"));


User? active_user = null;

bool running = true;

while (running)
{
    if (active_user == null)
    {
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
                LogInUser();
                Debug.Assert(active_user == null); //LogInUser should set an active user.
                break;
            case "2":
                CreateUser();
                break;
            case "3":
                running = false;
                break;
        }
    }

}

/*Method to login user with existing account, 
takes the user input and checks if it matches an active user
if it does, logs in*/
void LogInUser()
{
    Console.WriteLine("Enter your email: ");
    string email = Console.ReadLine(); //user input stored in string email

    Console.Clear();

    Console.WriteLine("Enter your password");
    string _password = Console.ReadLine(); //user input stored in strin _password

    Console.Clear();

    foreach (User user in users)
    {
        if (user.TryLogin(email, _password)) //searches the list user for matching credentials
        {
            active_user = user; //if valid user, logs in
            return;
        }
    }
    Console.WriteLine("Invalid username or password, try again.");
}


/*Method to create a new user, asks the user for their email and to choose a password
Adds the user in the list of users*/
void CreateUser()
{
    Console.WriteLine("Enter your email");
    string newAccountEmail = Console.ReadLine();

    Console.Clear();

    Console.WriteLine("Choose your password");
    string newAccountPassword = Console.ReadLine();

    users.Add(new User(newAccountEmail, newAccountPassword));



}