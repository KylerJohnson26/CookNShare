using System.Threading.Tasks;
using CookNShareWebApi.Models;
using CookNShareWebApi.Repositories;

namespace CookNShareWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _userRepo.GetUserByUsername(username);

            if(user == null)
                return null;
            
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var isCreated = await _userRepo.CreateNewUser(user);

            if(isCreated)
                return user;
            
            return null;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userRepo.UserExists(username);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < ComputedHash.Length; i++)
                    if(ComputedHash[i] != passwordHash[i]) return false;
            }
            return true;
        }
    }
}