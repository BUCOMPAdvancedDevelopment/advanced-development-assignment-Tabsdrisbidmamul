using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Xunit;
using Moq;
using Application.Games;
using Persistence;
using Domain;
using Domain.Types;
using Microsoft.EntityFrameworkCore;
using Extensions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Tests
{

  public class TestGamesWithMediator: IDisposable
  {
    DbContextOptions<DataContext>? _options;
    List<Game>? _gamesList = null;
    public TestGamesWithMediator()
    {
      _options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "shogun")
      .Options;

      List<Game> _gamesList = new List<Game>
      {
        new Game
        {
          Id = "A".AsGuid(),
          Title = "Game 1",
          Image = "default.png",
          Description = "lorem ipsum",
          Category = CategoryTypes.MMO.GetStringValue(),
          Price = 10.00,
          Stock = 10,
          CreatedAt = DateTime.Now.AddMonths(-2)
        },

        new Game
        {
          Id = "B".AsGuid(),
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
          Id = "C".AsGuid(),
          Title = "Game 3",
          Image = "default.png",
          Description = "lorem ipsum",
          Category = CategoryTypes.Jrpg.GetStringValue(),
          Price = 30.00,
          Stock = 30,
          CreatedAt = DateTime.Now.AddMonths(-2)
        }
      };
    }


    public void Dispose()
    {
      _options = null;
      _gamesList = null;
    }

    /// <summary>
    /// Test Mediator to see if a single game is being retrievd
    /// </summary>
    [Fact]
    public async void SingleGamesQuery_GetASingleGameFromDatabase()
    {
      // Arrage
      using(var context = new DataContext(_options)) {
        if(_gamesList == null) return;

        context.AddRange(_gamesList);
        context.Games.Add(new Game
        {
          Id = "D".AsGuid(),
          Title = "Game 4",
          Image = "default.png",
          Description = "lorem ipsum",
          Category = CategoryTypes.OpenWorld.GetStringValue(),
          Price = 40.00,
          Stock = 40,
          CreatedAt = DateTime.Now.AddMonths(-1)

        });
        context.SaveChanges();
      }

      // create a fresh instance of the Db context
      using(var context = new DataContext(_options)) 
      {
        var mediator = new Mock<IMediator>();
        var logger = new Mock<ILogger<Game>>();

        Application.Games.Single.Query query = 
          new Application.Games.Single.Query{ Id = "D".AsGuid() };

        Application.Games.Single.Handler handler = new Application.Games.Single.Handler(context, logger.Object);

        // Act
        Game game = await handler.Handle(query, new System.Threading.CancellationToken());

        Assert.Equal(_gamesList.Last().Id, game.Id);
      }      
    }


    /// <summary>
    /// Test Mediator to see if all Games are retrieved from an in memory database
    /// </summary>
    [Fact]
    public async void ListGamesQuery_GetAllGamesFromDatabase()
    {
      // Arrage
      using(var context = new DataContext(_options)) {
        if(_gamesList == null) return;

        context.AddRange(_gamesList);
        context.SaveChanges();
      }
      
      // create a fresh instance of the Db context
      using(var context = new DataContext(_options)) 
      {
        var mediator = new Mock<IMediator>();
        var logger = new Mock<ILogger<List>>();

        List.Query query = new List.Query();
        List.Handler handler = new List.Handler(context, logger.Object);

        // Act
        List<Game> games = await handler.Handle(query, new System.Threading.CancellationToken());

        Assert.Collection(games, 
          item1 => Assert.Equal("Game 1", item1.Title),
          item2 => Assert.Equal("Game 2", item2.Title),
          item3 => Assert.Equal("Game 3", item3.Title)
        );
      }

    }

    /// <summary>
    /// Will run multiple times for different seed values adding new games to the db and will check if the added game is the same in the db
    /// </summary>
    /// <param name="seed">String value to seed the GUIDs to have testable Ids to compare against on</param>
    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C")]
    public async void CreateGamesCommand_AddASingleGameToDatabase(string seed)
    {
      // create a fresh instance of the Db context
      using(var context = new DataContext(_options)) 
      {
        var mediator = new Mock<IMediator>();
        var logger = new Mock<ILogger<Unit>>();

        Create.Command command = new Create.Command
        {
          Game = new Game 
          {
            Id = seed.AsGuid(),
            Title = "Game 1",
            Image = "default.png",
            Description = "lorem ipsum",
            Category = CategoryTypes.MMO.GetStringValue(),
            Price = 10.00,
            Stock = 10,
            CreatedAt = DateTime.Now.AddMonths(-2)
          }
        };

        Create.Handler handler = new Create.Handler(context, logger.Object);

        // Act
        Unit gameWasCreated = 
          await handler.Handle(command, new System.Threading.CancellationToken());

        Assert.Equal(seed.AsGuid(), context.Games.First(x => x.Id == seed.AsGuid()).Id);
      }
    }

    /// <summary>
    /// Test Mediator to see if items can be editted via the seeded games in the database
    /// </summary>
    /// <param name="seed">Seeded string for repeatbale GUIDS</param>
    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C")]
    public async void EditGamesCommand_UpdateASingleGameInDatabase(string seed)
    {
      using(var context = new DataContext(_options)) {
        if(_gamesList == null) return;

        context.AddRange(_gamesList);
        context.SaveChanges();
      }
      
      // create a fresh instance of the Db context
      using(var context = new DataContext(_options)) 
      {
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var logger = new Mock<ILogger<Unit>>();

        Edit.Command command = new Edit.Command
        {
          Game = new Game
          {
            Id = seed.AsGuid(),
            Title = "Game 3 Updated",
            Image = "default.png",
            Description = "lorem ipsum",
            Category = CategoryTypes.Jrpg.GetStringValue(),
            Price = 30.00,
            Stock = 30,
            CreatedAt = DateTime.Now.AddMonths(-2)
          }
        };

        Edit.Handler handler = new Edit.Handler(context, mapper.Object, logger.Object);

        // Act
        await handler.Handle(command, new System.Threading.CancellationToken());

        var updatedGame = await context.Games.FindAsync(seed.AsGuid());

        if(updatedGame == null) return;

        Assert.Equal("Game 3 Updated", updatedGame.Title);
      }
    }

    /// <summary>
    /// Test Mediator to see if items are being deleted
    /// </summary>
    [Fact]
    public async void DeleteGamesCommand_DeleteAGameFromDatabase()
    {
      using(var context = new DataContext(_options)) {
        if(_gamesList == null) return;

        context.AddRange(_gamesList);
        context.SaveChanges();
      }

      // create a fresh instance of the Db context
      using(var context = new DataContext(_options)) 
      {
        var mediator = new Mock<IMediator>();
        var logger = new Mock<ILogger<Unit>>();

        Delete.Command command = new Delete.Command
        {
          Id = "A".AsGuid()
        };

        Delete.Handler handler = new Delete.Handler(context, logger.Object);

        // Act
        await handler.Handle(command, new System.Threading.CancellationToken());


        Assert.Equal(2, context.Games.Count());
      }

    }
  }
}