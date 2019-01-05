using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using PostApi.Models;
using PostApi.Repository;
using System;

namespace PostApi.Controllers
{
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;

    public AuthController(IAuthRepository repo)
    {
      _repo = repo;
    }

    [Route("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
      if (!string.IsNullOrEmpty(user.Username))
        user.Username = user.Username.ToLower();

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var createdUser = await _repo.Register(user, user.Password);
      return Ok(createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
      var userFromRepo = await _repo.Login(user.Username.ToLower(), user.Password);

      if (userFromRepo == null)
        return Unauthorized();

      //generate token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes("super secret key");

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[] {
                      new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                      new Claim(ClaimTypes.Name, userFromRepo.Username)
                  }),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return Ok(new { tokenString, userFromRepo });
    }
  }
}