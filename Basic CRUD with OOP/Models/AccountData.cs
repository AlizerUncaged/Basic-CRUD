using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_CRUD_with_OOP.Models
{
    public class AccountData
    {
        public string Email { get; set; }
        public byte? Age { get; set; }

        public AccountData() : this(null)
        {

        }

        public AccountData(string email) : this(email, null)
        {

        }

        public AccountData(string email, byte? age)
        {
            Email = email;
            Age = age;
        }
    }
}
