using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_CRUD_with_OOP.Models
{
    public class Account : IModel
    {
        public string XmlName => "Accounts.xml";

        public string Username { get; set; }

        public List<Post> Posts { get; set; } // One is to many relationship.

        public AccountData AccountData { get; set; } // One is to one relationship.

        public Account() : this("Anonymous")
        {

        }

        public Account(string username) : this(username, null, null)
        {

        }

        public Account(string username, string email, byte? age)
        {
            Posts = new List<Post>();
            Username = username;
        }

        public static Account AskAccount()
        {
            Console.Write("[?] Username: ");
            var username = Console.ReadLine();
            Console.Write("[?] Email: ");
            var email = Console.ReadLine();
            Console.Write("[?] Age: ");
            byte age = 0x0;
            byte.TryParse(Console.ReadLine(), out age);
            return new Account(username, email, age == 0x0 ? null : (byte?)age);
        }
    }
}
