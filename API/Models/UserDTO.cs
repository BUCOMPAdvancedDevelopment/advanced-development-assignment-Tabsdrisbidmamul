using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Extensions;

namespace API.Models
{
    public class UserDTO
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public ProfileImage Image { get; set; }
        public string Role { get; set; }
    }
}