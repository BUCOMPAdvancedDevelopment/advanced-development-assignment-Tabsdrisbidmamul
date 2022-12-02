using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public class ProfileImage
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
    }
}