using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Types;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User: IdentityUser
    {
        public string DisplayName { get; set; }
        public string Role { get; set; }
    }
}