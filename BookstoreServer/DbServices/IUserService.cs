using Bookstore.Models;

namespace Bookstore.DbServices
{
    public interface IUserService
    {
        Task<UserModel> RegisterUser(UserModel user);

        Task<UserModel?> GetUserByEmailAndPassword(string email, string password);

    }
}
