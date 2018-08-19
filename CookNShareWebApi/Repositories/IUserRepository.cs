using System.Threading.Tasks;
using CookNShareWebApi.Models;

namespace CookNShareWebApi.Repositories
{
    public interface IUserRepository
    {
         Task<User> GetUserByUsername(string username);
         Task<bool> CreateNewUser(User user);
         Task<bool> UserExists(string username);
    }
}