using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using PostApi.Models;
using PostApi.Repository;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PostApi.Controllers
{

  [Authorize]
  [Route("api/users/{userId}/photos")]
  [ApiController]
  public class PhotosController : ControllerBase
  {
    private readonly IHostingEnvironment _host;
    private readonly IUserRepository _userRepository;
    private readonly IPhotoRepository _photoRepository;
    public PhotosController(IHostingEnvironment host, IUserRepository userRepository, IPhotoRepository photoRepository)
    {
      _photoRepository = photoRepository;
      _userRepository = userRepository;
      _host = host;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(int userId, IFormFile file)
    {

      var user = await _userRepository.GetUser(userId);

      if (user == null) return NotFound("User Not found");
 
      var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (currentUserId != user.Id)
        return Unauthorized();

      if (file == null) return BadRequest("Null file");
      if (file.Length == 0) return BadRequest("Empty file");

      var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
      if (!Directory.Exists(uploadsFolderPath))
        Directory.CreateDirectory(uploadsFolderPath);

      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
      var filePath = Path.Combine(uploadsFolderPath, fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      var photo = new Photo { FileName = fileName };
      await _photoRepository.AddPhotoToUserAsync(user.Id, photo);

      if (await _photoRepository.SaveAll())
      {
        return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photo);
      }

      return BadRequest("Could not add the photo");
    }

    [HttpGet("{id}", Name = "GetPhoto")]
    public async Task<IActionResult> GetPhoto(int id)
    {
      var photoFromRepo = await _photoRepository.GetPhotoAsync(id);

      return Ok(photoFromRepo);
    }
  }

}