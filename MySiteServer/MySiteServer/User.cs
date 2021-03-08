using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace MySiteServer
{
    public class User
    {
        public int UserId { get; set; }
        public string L0gin { get; set; }
        public string Passwrd { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public string UserCart { get; set; }
        public void ShowUser()
        {
            Console.WriteLine($"UserName : {UserName}");
            Console.WriteLine($"Password : {Passwrd}");
        }
    }
}
