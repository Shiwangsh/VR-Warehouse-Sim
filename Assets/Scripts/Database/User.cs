using System;
using System.Collections.Generic;

public class User
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> SessionIds { get; set; } // List of sessions the user participated in

    public User(string userId, string name, string email)
    {
        UserId = userId;
        Name = name;
        Email = email;
        SessionIds = new List<string>();
    }
}
