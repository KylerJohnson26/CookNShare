using System.Threading.Tasks;
using CookNShareWebApi.Models;

namespace CookNShareWebApi.Services
{
    public interface IAuthService
    {
         Task<User> Login(string username, string password);
         Task<User> Register(User user, string password);
         Task<bool> UserExists(string username);
    }
}