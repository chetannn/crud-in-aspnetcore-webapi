using Microsoft.AspNetCore.Mvc;
using PostApi.Data;
using PostApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PostsController : ControllerBase
  {
    private readonly AppDbContext _context;
    public PostsController(AppDbContext context)
    {
      _context = context;
    }

    [HttpPost]
    public ActionResult<Post> Post([FromBody] Post post)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      _context.Posts.Add(post);
      _context.SaveChanges();

      return CreatedAtRoute(nameof(GetPost), new { controller = "Posts", id = post.Id }, post);

    }
    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetPosts(int page = 1, int pageSize = 5)
    {
      var posts = _context.Posts;
      
      var entries =  posts.Skip((page- 1) * pageSize).Take(pageSize).ToList();
      if(posts == null) return NotFound();
      return entries;
    }

    [HttpGet("{id}", Name = "GetPost")]
    public ActionResult<Post> GetPost(int id)
    {
      var post = _context.Posts.FirstOrDefault(p => p.Id == id);
      if (post == null) return NotFound();
      return post;
    }

    [HttpPut("{id}")]
    public ActionResult<Post> PutPost([FromBody] Post post, int id)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var postFromDb = _context.Posts.FirstOrDefault(p => p.Id == id);
      if (postFromDb == null)
        return NotFound();

      postFromDb.Title = post.Title;
      postFromDb.Content = post.Content;

      _context.Posts.Update(postFromDb);
      _context.SaveChanges();
      return NoContent();

    }

    [HttpDelete("{id}")]
    public ActionResult<Post> DeletePost(int id)
    {
      var post = _context.Posts.FirstOrDefault(p => p.Id == id);
      if (post == null) return NotFound();

      _context.Posts.Remove(post);
      _context.SaveChanges();

      return post;
    }

  }
}