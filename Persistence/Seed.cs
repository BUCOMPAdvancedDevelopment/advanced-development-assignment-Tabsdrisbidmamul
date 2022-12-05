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
                Email = "baal@email.com",
                Role = Roles.Admin.GetStringValue(),
                Image = new ProfileImage 
                {
                  PublicId = "la7hiqw8s8asomhrbvul",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1670101467/logo/profile-images/la7hiqw8s8asomhrbvul.jpg"
                }
              },
              new User
              {
                DisplayName = "Morax",
                UserName = "morax",
                Email = "morax@email.com",
                Role = Roles.User.GetStringValue()
              },
              new User
              {
                DisplayName = "Beur",
                UserName = "beur",
                Email = "beur@email.com",
                Role = Roles.User.GetStringValue()
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
              CoverArt = new List<CoverArt> 
              {
                new CoverArt
                {
                  PublicId="Astroneer-Hero-Asset-1920x1080_qwg7fw",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669723081/logo/cover-art/Astroneer-Hero-Asset-1920x1080_qwg7fw.jpg",
                  IsBoxArt = false
                },
                new CoverArt
                {
                 PublicId = "547082-astroneer-windows-apps-front-cover_utrr2t",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1670233064/logo/cover-art/547082-astroneer-windows-apps-front-cover_utrr2t.jpg",
                  IsBoxArt = true
                }

              },
              Description = "Explore and reshape distant worlds! Astroneer is set during the 25th century Intergalactic Age of Discovery, where Astroneers explore the frontiers of outer space, risking their lives in harsh environments to unearth rare discoveries and unlock the mysteries of the universe.",
              Category = new List<string>
              {
                CategoryTypes.OpenWorld.GetStringValue(),
                CategoryTypes.Action.GetStringValue(),
                CategoryTypes.Multiplayer.GetStringValue(),
                CategoryTypes.Coop.GetStringValue(),
                CategoryTypes.FirstPerson.GetStringValue(),
                CategoryTypes.SciFi.GetStringValue(),
              },
              Price = 10.00,
              Stock = 10,
              CreatedAt = DateTime.Now.AddMonths(-2),
              YoutubeLink = "0KXQZG7riEs"
            },
            new Game
            {
              Title = "The Legend of Zelda Breath of the Wild",
              CoverArt = new List<CoverArt>
              {
                new CoverArt
                {
                  PublicId = "16.9_TheLegendofZeldaBreatheoftheWild_fqhydw",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722961/logo/cover-art/16.9_TheLegendofZeldaBreatheoftheWild_fqhydw.jpg",
                  IsBoxArt = false
                },
                new CoverArt 
                {
                  PublicId = "384053-the-legend-of-zelda-breath-of-the-wild-limited-edition-nintendo-switch-front-cover_lppw8a",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1670234068/logo/cover-art/384053-the-legend-of-zelda-breath-of-the-wild-limited-edition-nintendo-switch-front-cover_lppw8a.jpg",
                  IsBoxArt = true
                }
              },
              Description = "After a 100-year slumber, Link wakes up alone in a world he no longer remembers. Now the legendary hero must explore a vast and dangerous land and regain his memories before Hyrule is lost forever. Armed only with what he can scavenge, Link sets out to find answers and the resources needed to survive.",
              Category = new List<string>
              {
                CategoryTypes.Rpg.GetStringValue(),
                CategoryTypes.Action.GetStringValue(),
                CategoryTypes.Adventure.GetStringValue(),
                CategoryTypes.SinglePlayer.GetStringValue(),
                CategoryTypes.OpenWorld.GetStringValue(),
              },
              Price = 20.00,
              Stock = 20,
              CreatedAt = DateTime.Now.AddMonths(-4),
              YoutubeLink = "zw47_q9wbBE"
            },
            new Game
            {
              Title = "God of War Ragnarok",
              CoverArt = new List<CoverArt>
              {
                new CoverArt
                {
                  PublicId = "E-37dPaVcBYyRWH_qcoyxt",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722882/logo/cover-art/E-37dPaVcBYyRWH_qcoyxt.jpg",
                  IsBoxArt = false
                },
                new CoverArt
                {
                  PublicId = "r90tvpqmjpb91_c7cguy",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1670234178/logo/cover-art/r90tvpqmjpb91_c7cguy.jpg",
                  IsBoxArt = true
                }
              },
              Description = "His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters. It is in this harsh, unforgiving world that he must fight to survive… and teach his son to do the same.",
              Category = new List<string>
              {
                CategoryTypes.Rpg.GetStringValue(),
                CategoryTypes.SinglePlayer.GetStringValue(),
                CategoryTypes.Action.GetStringValue(),
                CategoryTypes.Adventure.GetStringValue(),
                CategoryTypes.OpenWorld.GetStringValue(),

              },
              Price = 30.00,
              Stock = 30,
              CreatedAt = DateTime.Now.AddMonths(-2),
              YoutubeLink = "EE-4GvjKcfs"
            },
            new Game
            {
              Title = "Spider-man remastered",
              CoverArt = new List<CoverArt>
              {
                new CoverArt
                {
                  PublicId = "marvel-s-spider-man-remastered-ps5-playstation-5-game-playstation-store-europe-cover_mqemx4",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722807/logo/cover-art/marvel-s-spider-man-remastered-ps5-playstation-5-game-playstation-store-europe-cover_mqemx4.jpg",
                  IsBoxArt = false
                },
                new CoverArt {
                  PublicId = "marvel-s-spider-man-remastered-ps5-playstation-5-game-playstation-store-europe-cover_mqemx4",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722807/logo/cover-art/marvel-s-spider-man-remastered-ps5-playstation-5-game-playstation-store-europe-cover_mqemx4.jpg",
                  IsBoxArt = true
                },
              },
              Description = "Developed by Insomniac Games in collaboration with Marvel, and optimized for PC by Nixxes Software, Marvel's Spider-Man Remastered on PC introduces an experienced Peter Parker who’s fighting big crime and iconic villains in Marvel’s New York. At the same time, he’s struggling to balance his chaotic personal life and career while the fate of Marvel’s New York rests upon his shoulders.",
              Category = new List<string>
              {
                CategoryTypes.OpenWorld.GetStringValue(),
                CategoryTypes.Action.GetStringValue(),
                CategoryTypes.Adventure.GetStringValue(),
                CategoryTypes.Rpg.GetStringValue(),
              },
              Price = 40.00,
              Stock = 40,
              CreatedAt = DateTime.Now.AddMonths(-1),
              YoutubeLink = "Tsf5Wjb1uAM"
            },
            new Game
            {
              Title = "Red Dead Redemption 2",
              CoverArt = new List<CoverArt>
              {
                new CoverArt
                {
                  PublicId = "red-dead-redemption-2-game-poster-2018_a2dobmyUmZqaraWkpJRmbmdlrWZlbWU_lrq9ls",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1669722607/logo/cover-art/red-dead-redemption-2-game-poster-2018_a2dobmyUmZqaraWkpJRmbmdlrWZlbWU_lrq9ls.jpg",
                  IsBoxArt = false
                },
                new CoverArt
                {
                  PublicId = "red_dead_redemption_2_cover_art_1_h2axxs",
                  Url = "https://res.cloudinary.com/drmofy8fr/image/upload/v1670232895/logo/cover-art/red_dead_redemption_2_cover_art_1_h2axxs.jpg",
                  IsBoxArt = true
                }
              },
              Description = "Arthur Morgan and the Van der Linde gang are outlaws on the run. With federal agents and the best bounty hunters in the nation massing on their heels, the gang must rob, steal and fight their way across the rugged heartland of America in order to survive. As deepening internal divisions threaten to tear the gang apart, Arthur must make a choice between his own ideals and loyalty to the gang who raised him.",
              Category = new List<string>
              {
                CategoryTypes.Rpg.GetStringValue(),
                CategoryTypes.Action.GetStringValue(),
                CategoryTypes.Adventure.GetStringValue(),
                CategoryTypes.SinglePlayer.GetStringValue(),
              },
              Price = 5.00,
              Stock = 100,
              CreatedAt = DateTime.Now.AddMonths(3),
              YoutubeLink = "eaW0tYpxyp0"
            }
          };

          await context.Games.AddRangeAsync(games);
          await context.SaveChangesAsync();
        }
        
    }
}