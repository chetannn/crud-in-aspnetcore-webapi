using System.Collections.Generic;

namespace PostApi.Models
{
  public class Post 
  {
    public Post()
    {
        Comments = new List<Comment>();
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Comment> Comments { get; set; }

    public ICollection<PostFavorite> PostFavorites { get; set; }
  }
}