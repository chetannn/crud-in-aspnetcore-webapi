using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PostApi.Data;
using PostApi.Models;

namespace PostApi.Controllers
{
  [ApiController]
  [Route("api/[controller]/{postId}")]
  public class CommentsController : ControllerBase
  {
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
      _context = context;
    }

    public ActionResult<IEnumerable<Comment>> GetComments()
    {
      return _context.Comments.ToList();
    }

    [HttpPost]
    public ActionResult<Comment> CreateComment(int postId, [FromBody] Comment comment)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

      if (post == null)
        return BadRequest("Post Not found");

      post.Comments.Add(comment);
      _context.SaveChanges();

      return CreatedAtRoute("GetComment", new { controller = "Comments", postId = postId, id = comment.Id }, comment);
    }

    [HttpGet("{id}", Name = "GetComment")]
    public ActionResult<Comment> GetComment(int postId,int id)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
        if(post == null)
         return BadRequest();

      var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
      if (comment == null)
        return NotFound();

      return comment;
    }
  }
}