using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Extensions;
using Domain.Types;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User: IdentityUser
    {
        public string DisplayName { get; set; }
        public string Role { get; set; }
        public ProfileImage Image { get; set; } = new ProfileImage
        {
          Id = new Guid(),
          PublicId = "default_etdrb8",
          Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669973955/logo/profile-images/default_etdrb8.jpg"
        };
    }
}