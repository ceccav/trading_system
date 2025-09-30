using System.Diagnostics;

namespace App;

class User
{
    public string UserEmail;
    private string _password;

    public User(string email, string password)
    {
        UserEmail = email;
        _password = password;
    }

    /// <summary>
    /// Bool for login function
    /// </summary>
    /// <param name="userEmail"></param>
    /// <param name="password"></param>
    /// <returns>login if user exists</returns>
    public bool TryLogin(string email, string password)
    {
        return email == UserEmail && password == _password;
    }



}