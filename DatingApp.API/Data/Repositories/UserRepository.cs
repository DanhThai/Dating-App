

using DatingApp.API.Data.Entity;

namespace DatingApp.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            this._context = context;
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            _context.AppUsers.Remove(user);
        }

        public User GetUserById(int id)
        {
            return _context.AppUsers.FirstOrDefault(user =>user.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.AppUsers.FirstOrDefault(user =>user.Username == username);
        }

        public List<User> GetUsers()
        {
            return _context.AppUsers.ToList();
        }

        public void InsertNewUser(User user)
        {
            user.CreatedAt= DateTime.Now;
            _context.AppUsers.Add(user);
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges()>0;
        }

        public void UpdateUser(User user)
        {
            _context.AppUsers.Update(user);
        }
    }
}