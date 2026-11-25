using Project12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project12.Services
{
    public class DBMokup
    {
        private List<AppUser> _users = new List<AppUser>();

        public DBMokup()
        {
            _users.Add(new AppUser { FirstName = "Admin", LastName = "User", UserEmail = "admin@mail.com", UserPassword = "admin", Mobile = "0500000000" });
            _users.Add(new AppUser { FirstName = "User", LastName = "One", UserEmail = "user1@mail.com", UserPassword = "pass1", Mobile = "0501111111" });
            _users.Add(new AppUser { FirstName = "User", LastName = "Two", UserEmail = "user2@mail.com", UserPassword = "pass2", Mobile = "0502222222" });
        }

        public bool IsExist(string uEmail, string uPass)
        {
            return _users.Any(u => u.UserEmail == uEmail && u.UserPassword == uPass);
        }

        public bool IsEmailExist(string uEmail)
        {
            return _users.Any(u => u.UserEmail == uEmail);
        }

        public void AddUser(AppUser user)
        {
            _users.Add(user);
        }
    }
}
