

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.API.Data.Entity;

namespace DatingApp.API.Data.Seed
{
    public static class Seed
    {
        public static void SeedUser(DataContext context){
            if (context.AppUsers.Any()) return;
            
            var usersText = System.IO.File.ReadAllText("Data/Seed/user_data.json");
            var users = JsonSerializer.Deserialize<List<User>>(usersText);
            foreach(var user in users){
                using var hmac= new HMACSHA512();
                user.PasswordHash =hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
                user.PasswordSalt = hmac.Key;
                user.CreatedAt = DateTime.Now;

                context.AppUsers.Add(user);
            }
            context.SaveChanges();
        }
    }
}