using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Extensions;

namespace Domain.Types
{
    public enum CategoryTypes: int
    {
        [StringValue("mmo")]
        MMO = 0,
        [StringValue("action")]
        Action = 1,
        [StringValue("rpg")]
        Rpg = 2,
        [StringValue("jrpg")]
        Jrpg = 3,
        [StringValue("OpenWorld")]
        OpenWorld = 4

    }
}