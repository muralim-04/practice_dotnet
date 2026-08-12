using practice_dotnet.Entities;

namespace practice_dotnet.DTOs
{
    public class UserProfileDto
    {
        public required string UserName { get; set; }
        public required string Bio { get; set; } = string.Empty;
        public required string? AvatarUrl { get; set; }
        public required string Email { get; set; }

    }
}
