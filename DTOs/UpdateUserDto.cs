using System.ComponentModel.DataAnnotations;

namespace practice_dotnet.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string UserName { get; set; }
    }
}
