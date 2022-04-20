using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly Context _context;

        public UserRepo(Context context)
        {
            this._context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByIdAsnc(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x=>x.id == id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
