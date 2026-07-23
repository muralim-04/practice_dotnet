using System.ComponentModel.DataAnnotations;

namespace practice_dotnet.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public string Email { get; set; } = string.Empty;

        //navigation properties
        public ICollection<UserPost>? UserPosts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostLike>? Likes { get; set; }
    }
}
