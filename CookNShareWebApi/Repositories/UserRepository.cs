using System.Threading.Tasks;
using CookNShareWebApi.Data;
using CookNShareWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CookNShareWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetUserByUserId(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<bool> CreateNewUser(User user)
        {
            await _context.Users.AddAsync(user);
            var numChanges =  await _context.SaveChangesAsync();
            return numChanges > 0;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }
    }
}