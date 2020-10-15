using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string mail { get; set; }

        [Required]
        public string passwordHash { get; set; }
    }
}
