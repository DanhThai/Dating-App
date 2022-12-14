using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data.Entity;

namespace DatingApp.API.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        void InsertNewUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool IsSaveChanges();
    }
}