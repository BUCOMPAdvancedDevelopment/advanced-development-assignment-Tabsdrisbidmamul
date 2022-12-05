using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ReviewDTO
    {
      public string GameId { get; set; }
      public ReviewMapping Review { get; set; }
    }

    public class ReviewMapping
    {
      public string Username { get; set; }
      public string Review { get; set; }
    }
}