using System.Diagnostics;

namespace App;

class User : IUser
{
    public string Username;
    private string _password;

    public User(string username, string password)
    {
        Username = username;
        _password = password;
    }

    /// <summary>
    /// Bool for login function
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>login if user exists</returns>
    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }

}