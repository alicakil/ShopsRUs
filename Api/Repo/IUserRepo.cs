using Api.Models;

namespace Api.Repo
{
    public interface IUserRepo
    {
        public Task<List<User>> GetUsersAsync();
        public Task<User> GetByIdAsnc(int id);
        public Task<User> CreateUserAsync(User user);
    }
}
