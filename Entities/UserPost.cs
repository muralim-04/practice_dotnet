namespace practice_dotnet.Entities
{
    public class UserPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        // navigation properties
        public int UserId { get; set; }
        public required User User { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostLike>? Likes { get; set; }
    }
}
