using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostApi.Models;
using PostApi.Repository;

namespace PostApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController :ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        { 
            if(!string.IsNullOrEmpty(user.Username))
                user.Username = user.Username.ToLower();

                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var createdUser = await _repo.Register(user, user.Password);
                return Ok(createdUser);
        }
    }
}