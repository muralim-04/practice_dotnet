namespace practice_dotnet.Entities
{
    public class PostLike
    {
        public int PostId { get; set; }
        public required UserPost UserPost { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
    }
}
