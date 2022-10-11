using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

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
              Title = "Game 1",
              Image = "default.png",
              Description = "lorem ipsum",
              Category = "action",
              Price = 10.00,
              Stock = 10,
              CreatedAt = DateTime.Now.AddMonths(-2)
            },
            new Game
            {
              Title = "Game 2",
              Image = "default.png",
              Description = "lorem ipsum",
              Category = "rpg",
              Price = 20.00,
              Stock = 20,
              CreatedAt = DateTime.Now.AddMonths(-4)
            },
            new Game
            {
              Title = "Game 3",
              Image = "default.png",
              Description = "lorem ipsum",
              Category = "jrpg",
              Price = 30.00,
              Stock = 30,
              CreatedAt = DateTime.Now.AddMonths(-2)
            },
            new Game
            {
              Title = "Game 4",
              Image = "default.png",
              Description = "lorem ipsum",
              Category = "open-world",
              Price = 40.00,
              Stock = 40,
              CreatedAt = DateTime.Now.AddMonths(-1)
            },
            new Game
            {
              Title = "Game 5",
              Image = "default.png",
              Description = "lorem ipsum",
              Category = "mmo",
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