using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Models;
using Domain.Types;
using Extensions;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<User> userManager)
        {

          if(!userManager.Users.Any()) 
          {
            var users = new List<User>
            {
              new User
              {
                DisplayName = "Baal",
                UserName = "baal",
                Email = "baal@email.com"
              },
              new User
              {
                DisplayName = "Morax",
                UserName = "morax",
                Email = "morax@email.com"
              },
              new User
              {
                DisplayName = "Beur",
                UserName = "beur",
                Email = "beur@email.com"
              },
            };

            foreach(var user in users) 
            {
              await userManager.CreateAsync(user, "W8R$M*Tz689^"); 
            }

          }

          if(context.Games.Any()) return;


          var games = new List<Game>
          {
            new Game
            {
              Title = "Astroner",
              PublicId="Astroneer-Hero-Asset-1920x1080_qwg7fw",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669723081/logo/cover-art/Astroneer-Hero-Asset-1920x1080_qwg7fw.jpg",
              Description = "Explore and reshape distant worlds! Astroneer is set during the 25th century Intergalactic Age of Discovery, where Astroneers explore the frontiers of outer space, risking their lives in harsh environments to unearth rare discoveries and unlock the mysteries of the universe.",
              Category = CategoryTypes.OpenWorld.GetStringValue(),
              Price = 10.00,
              Stock = 10,
              CreatedAt = DateTime.Now.AddMonths(-2)
            },
            new Game
            {
              Title = "The Legend of Zelda Breath of the Wild",
              PublicId = "16.9_TheLegendofZeldaBreatheoftheWild_fqhydw",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722961/logo/cover-art/16.9_TheLegendofZeldaBreatheoftheWild_fqhydw.jpg",
              Description = "After a 100-year slumber, Link wakes up alone in a world he no longer remembers. Now the legendary hero must explore a vast and dangerous land and regain his memories before Hyrule is lost forever. Armed only with what he can scavenge, Link sets out to find answers and the resources needed to survive.",
              Category = CategoryTypes.Rpg.GetStringValue(),
              Price = 20.00,
              Stock = 20,
              CreatedAt = DateTime.Now.AddMonths(-4)
            },
            new Game
            {
              Title = "God of War Ragnarok",
              PublicId = "E-37dPaVcBYyRWH_qcoyxt",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722882/logo/cover-art/E-37dPaVcBYyRWH_qcoyxt.jpg",
              Description = "His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters. It is in this harsh, unforgiving world that he must fight to survive… and teach his son to do the same.",
              Category = CategoryTypes.Jrpg.GetStringValue(),
              Price = 30.00,
              Stock = 30,
              CreatedAt = DateTime.Now.AddMonths(-2)
            },
            new Game
            {
              Title = "Spider-man remastered",
              PublicId = "marvel-s-spider-man-remastered-ps5-playstation-5-game-playstation-store-europe-cover_mqemx4",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722807/logo/cover-art/marvel-s-spider-man-remastered-ps5-playstation-5-game-playstation-store-europe-cover_mqemx4.jpg",
              Description = "lorem ipsum",
              Category = CategoryTypes.OpenWorld.GetStringValue(),
              Price = 40.00,
              Stock = 40,
              CreatedAt = DateTime.Now.AddMonths(-1)
            },
            new Game
            {
              Title = "Red Dead Redemption 2",
              PublicId = "red-dead-redemption-2-game-poster-2018_a2dobmyUmZqaraWkpJRmbmdlrWZlbWU_lrq9ls",
              Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722607/logo/cover-art/red-dead-redemption-2-game-poster-2018_a2dobmyUmZqaraWkpJRmbmdlrWZlbWU_lrq9ls.jpg",
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