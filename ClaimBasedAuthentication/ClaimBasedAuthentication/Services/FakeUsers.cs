using ClaimBasedAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthentication.Services
{
    public class FakeUsers
    {

        private List<User> users;

        public FakeUsers()
        {
            users = new List<User>
            {
                 new User{ Id=1, UserName="turkayurkmez", Role="admin", Password="12345"   },
                 new User{ Id=2, UserName="ahmetokur", Role="editor", Password="1234"   },
                 new User{ Id=3, UserName="mustafa", Role="user", Password="1234"   }
            };


        }
        public User IsValid(string username, string password)
        {
            return users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}
