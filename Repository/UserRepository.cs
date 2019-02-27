using System.Collections.Generic;
using System.Threading.Tasks;
using PostApi.Data;
using PostApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostApi.Repository
{
  public class UserRepository : IUserRepository
  {
      private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
    public async Task<User> GetUser(int id)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
      return user;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
       var users = await  _context.Users.ToListAsync();
       return users;
    }

    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}