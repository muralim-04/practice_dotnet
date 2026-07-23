namespace practice_dotnet.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        // foreign key to post
        public int PostId { get; set; }
        public required UserPost UserPost { get; set; }

        // foreign key to user
        public int UserId { get; set; }
        public required User User { get; set; }

    }
}
