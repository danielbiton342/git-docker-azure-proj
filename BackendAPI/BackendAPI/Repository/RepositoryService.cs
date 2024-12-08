using BackendAPI.DAL;
using BackendAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Repository
{
    public class RepositoryService
    {
        private readonly MyDBContext _myDbContext;

        public RepositoryService(MyDBContext myDBContext)
        {
            _myDbContext = myDBContext;
        }


        public async Task AddUser(User user)
        {
            await _myDbContext.Users.AddAsync(user);
            await _myDbContext.SaveChangesAsync();
        }


        public async Task<User?> IsUserExist(string username)
        {
            return await _myDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
