using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Xunit;
using Moq;
using Application.Games;
using Persistence;
using Domain;
using Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Extensions;

namespace Application.Tests
{
  public class TestGamesWithMediator
  {
    [Fact]
    public async void ListGamesQuery_GetAllGamesFromDatabase()
    {
      // Arrage
      var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "shogun")
      .Options;

      List<Game> gamesList = new List<Game>
      {
        new Game
        {
          Id = new Guid(),
          Title = "Game 1",
          Image = "default.png",
          Description = "Lorem ipsum",
          Category = CategoryTypes.MMO.GetStringValue()
        },

        new Game
        {
          Title = "Game 2",
          Image = "default.png",
          Description = "lorem ipsum",
          Category = CategoryTypes.Rpg.GetStringValue(),
          Price = 20.00,
          Stock = 20,
          CreatedAt = DateTime.Now.AddMonths(-4)
        },

        new Game
        {
          Title = "Game 3",
          Image = "default.png",
          Description = "lorem ipsum",
          Category = CategoryTypes.Jrpg.GetStringValue(),
          Price = 30.00,
          Stock = 30,
          CreatedAt = DateTime.Now.AddMonths(-2)
        }
      };

      // insert seed data into db
      using(var context = new DataContext(options)) {
        context.AddRange(gamesList);

        context.SaveChanges();
      }
      

      using(var context = new DataContext(options)) 
      {
        var mediator = new Mock<IMediator>();

        List.Query query = new List.Query();
        List.Handler handler = new List.Handler(context);

        // Act
        List<Game> games = await handler.Handle(query, new System.Threading.CancellationToken());

        Assert.Collection(games, 
          item1 => Assert.Equal("Game 1", item1.Title),
          item2 => Assert.Equal("Game 2", item2.Title),
          item3 => Assert.Equal("Game 3", item3.Title)
        );
      }

    }
  }
}