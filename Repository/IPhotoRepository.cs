using PostApi.Data;
using PostApi.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostApi.Repository
{
  public interface IPhotoRepository
  {
    Task<Photo> AddPhotoToUserAsync(int userId, Photo photo);
    Task<Photo> GetPhotoAsync(int id);
    Task<bool> SaveAll();
  }

  public class PhotoRepository : IPhotoRepository
  {
    private readonly AppDbContext _context;
    public PhotoRepository(AppDbContext context)
    {
      _context = context;

    }
    public async Task<Photo> AddPhotoToUserAsync(int userId, Photo photo)
    {
      var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

      user.Photos.Add(photo);
      return photo;
    }

    public async Task<Photo> GetPhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        return photo;
    }

    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}