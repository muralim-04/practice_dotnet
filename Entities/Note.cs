namespace practice_dotnet.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public required User User { get; set; }
    }
}
