using System.ComponentModel.DataAnnotations;

namespace practice_dotnet.DTOs
{
    public class UserReqDto
    {
        [Required]
        [StringLength(40, MinimumLength =4)]
        public string? Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*\d).+$")]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
