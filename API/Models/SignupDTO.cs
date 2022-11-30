using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SignupDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{12,30}$", ErrorMessage = "Password must be complex, it must contain at least one number, one uppercase letter, one lowercase letter, one non-alphanumeric character and be at least 12 characters long")]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}