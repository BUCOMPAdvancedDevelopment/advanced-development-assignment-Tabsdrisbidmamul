using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ReviewDTO
    {
      public string GameId { get; set; }
      public string Username { get; set; }
      public string Review { get; set; }
      public string Rating { get; set; }
    }

    public class ReviewObj
    {
      public string Username { get; set; }
      public string Review { get; set; }
      public int Rating { get; set; }
    }
}