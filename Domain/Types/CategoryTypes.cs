using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Extensions;

namespace Domain.Types
{
    public enum CategoryTypes: int
    {
        [StringValue("MMO")]
        MMO = 0,
        [StringValue("Action")]
        Action = 1,
        [StringValue("RPG")]
        Rpg = 2,
        [StringValue("JRPG")]
        Jrpg = 3,
        [StringValue("Open World")]
        OpenWorld = 4,
        [StringValue("Multiplayer")]
        Multiplayer = 5,
        [StringValue("First Person")]
        FirstPerson = 6,
        [StringValue("Sci-Fi")]
        SciFi = 7,
        [StringValue("Co-Op")]
        Coop = 8,
        [StringValue("Adventure")]
        Adventure = 9,
        [StringValue("Single Player")]
        SinglePlayer = 10

    }
}