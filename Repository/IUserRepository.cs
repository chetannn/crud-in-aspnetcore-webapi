using PostApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PostApi.Repository
{
    public interface IUserRepository
    {
         Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> SaveAll();
    }
}