using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PostApi.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; }

    [NotMapped]
    public string Password { get; set; }

    [JsonIgnore]
    public byte[] PasswordHash { get; set; }

    [JsonIgnore]
    public byte[] PasswordSalt { get; set; }
    public string Gender { get; set; }
    public DateTime Created { get; set; }

    public List<Photo> Photos { get; set; }

    public ICollection<PostFavorite> PostFavorites { get; set; }
    public User()
    {
      Created = DateTime.Now;
      Photos = new List<Photo>();
    }
  }
}