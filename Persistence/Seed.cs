using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Models;
using Domain.Types;
using Extensions;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {

          if(context.Games.Any()) return;


          var games = new List<Game>
          {
            new Game
            {
              Title = "Astroner",
              PublicId="87c228b4e033651dbc51a265c82498f0_ttrwkz", Url="https://res.cloudinary.com/drmofy8fr/image/upload/v1669636173/logo/cover-art/87c228b4e033651dbc51a265c82498f0_ttrwkz.jpg",
              Description = "Explore and reshape distant worlds! Astroneer is set during the 25th century Intergalactic Age of Discovery, where Astroneers explore the frontiers of outer space, risking their lives in harsh environments to unearth rare discoveries and unlock the mysteries of the universe.",
              Category = CategoryTypes.OpenWorld.GetStringValue(),
              Price = 10.00,
              Stock = 10,
              CreatedAt = DateTime.Now.AddMonths(-2)
            },
            new Game
            {
              Title = "The Legend of Zelda Breath of the Wild",
              PublicId = "BOTW-Share_icon_mhkrqe",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669636503/logo/cover-art/BOTW-Share_icon_mhkrqe.jpg",
              Description = "After a 100-year slumber, Link wakes up alone in a world he no longer remembers. Now the legendary hero must explore a vast and dangerous land and regain his memories before Hyrule is lost forever. Armed only with what he can scavenge, Link sets out to find answers and the resources needed to survive.",
              Category = CategoryTypes.Rpg.GetStringValue(),
              Price = 20.00,
              Stock = 20,
              CreatedAt = DateTime.Now.AddMonths(-4)
            },
            new Game
            {
              Title = "God of War",
              PublicId = "1752327e27a443f4a58de10fafa94ed0_ohe9br",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669637276/logo/cover-art/1752327e27a443f4a58de10fafa94ed0_ohe9br.jpg",
              Description = "His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters. It is in this harsh, unforgiving world that he must fight to survive… and teach his son to do the same.",
              Category = CategoryTypes.Jrpg.GetStringValue(),
              Price = 30.00,
              Stock = 30,
              CreatedAt = DateTime.Now.AddMonths(-2)
            },
            new Game
            {
              Title = "Spider-man remastered",
              PublicId = "spider-man-game-bundle-nvidia-hero-mobile_hib24p",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669638473/logo/cover-art/spider-man-game-bundle-nvidia-hero-mobile_hib24p.jpg",
              Description = "lorem ipsum",
              Category = CategoryTypes.OpenWorld.GetStringValue(),
              Price = 40.00,
              Stock = 40,
              CreatedAt = DateTime.Now.AddMonths(-1)
            },
            new Game
            {
              Title = "Red Dead Redemption 2",
              PublicId = "91l_XW9jctL._AC_SL1500__aakfuu",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669640705/logo/cover-art/91l_XW9jctL._AC_SL1500__aakfuu.jpg",
              Description = "Arthur Morgan and the Van der Linde gang are outlaws on the run. With federal agents and the best bounty hunters in the nation massing on their heels, the gang must rob, steal and fight their way across the rugged heartland of America in order to survive. As deepening internal divisions threaten to tear the gang apart, Arthur must make a choice between his own ideals and loyalty to the gang who raised him.",
              Category = CategoryTypes.Rpg.GetStringValue(),
              Price = 5.00,
              Stock = 100,
              CreatedAt = DateTime.Now.AddMonths(3)
            }
          };

          await context.Games.AddRangeAsync(games);
          await context.SaveChangesAsync();
        }
        
    }
}