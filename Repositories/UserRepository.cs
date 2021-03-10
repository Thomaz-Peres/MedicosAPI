using System.Collections.Generic;
using System.Linq;
using DesafioMedicos.Models;

namespace DesafioMedicos.Repositories
{
    public static class UserRepository
    {
        public static User criarUser(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "Thomaz", Password = "123456", Role = "Manager" });
            users.Add(new User { Id = 2, Username = "Peres", Password = "thomaz", Role = "employee" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
