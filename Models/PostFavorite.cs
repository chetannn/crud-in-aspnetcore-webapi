namespace PostApi.Models
{
    public class PostFavorite
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}