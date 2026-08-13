namespace practice_dotnet.DTOs
{
    public class PostResDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string UserName { get; set; }
    }
}
