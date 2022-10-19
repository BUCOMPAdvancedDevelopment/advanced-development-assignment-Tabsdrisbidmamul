using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Types;

namespace Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt {get;set;}

    }
}