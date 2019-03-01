using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostApi.Helpers;
using PostApi.Repository;

namespace PostApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _repo;

    public UsersController(IUserRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(UserParams userParams)
    {
      var users = await _repo.GetUsers(userParams);
      Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

      return Ok(users);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
      var user = await _repo.GetUser(id);

      return Ok(user);
    }
  }
}