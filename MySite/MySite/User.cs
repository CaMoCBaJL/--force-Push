using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite
{
    public class User
    {
        public User()
        {
            UserName = "Nine";
            Password = "Nine!";
        }
        public string UserName { get; set; }
        public string Password { get; set; }

        public void ShowUser()
        {
            Console.WriteLine($"UserName : {UserName}");
            Console.WriteLine($"Password : {Password}");
        }
    }
}
