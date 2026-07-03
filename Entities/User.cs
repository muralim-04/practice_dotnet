using System.ComponentModel.DataAnnotations;

namespace practice_dotnet.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        ICollection<Note>? Notes { get; set; }
    }
}
