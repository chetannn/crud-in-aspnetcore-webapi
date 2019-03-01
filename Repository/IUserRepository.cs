using PostApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using PostApi.Helpers;

namespace PostApi.Repository
{
    public interface IUserRepository
    {
         Task<User> GetUser(int id);
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<bool> SaveAll();
    }
}