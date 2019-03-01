using System.Collections.Generic;
using System.Threading.Tasks;
using PostApi.Data;
using PostApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PostApi.Helpers;

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

    public async Task<PagedList<User>> GetUsers(UserParams userParams)
    {
      var users = _context.Users.Include(p => p.Photos);
      return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
    }

    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}