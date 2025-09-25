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
                break;
            case "2":
                CreateUser();
                break;
            case "3":
                active_user = null;
                break;
        }
    }
    Debug.Assert(active_user == null);
}

void CreateUser()
{
    Console.WriteLine("Enter your email: ");
    string email = Console.ReadLine();

    Console.WriteLine("Enter your password");
    string _password = Console.ReadLine();
}