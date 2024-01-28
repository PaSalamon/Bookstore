using Bookstore.Models;

namespace Bookstore.DbServices
{
    public class UserService : IUserService
    {
        private readonly AppDbContext db;

        public UserService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<UserModel> RegisterUser(UserModel user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> GetUserByEmailAndPassword(string email, string password)
        {
            var propUser = new UserModel();
            var allEntries = db.Set<UserModel>().ToList();
            foreach (var entry in allEntries)
            {
                if (entry.Email == email && entry.Password == password) 
                {
                    return entry;
                }
            }
            return propUser;
        }

    }
}
