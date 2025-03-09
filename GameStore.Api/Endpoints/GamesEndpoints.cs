using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
   private static readonly List<GameSummaryDto> games =
   [
      new GameSummaryDto(1, "Super Mario Bros", "Action", 59.99m, new DateOnly(1985, 9, 13)),
      new GameSummaryDto(2, "The Legend of Zelda", "Adventure", 59.99m, new DateOnly(1986, 2, 21)),
      new GameSummaryDto(3, "Mega Man 2", "Adventure", 59.99m, new DateOnly(1988, 6, 14)),
      new GameSummaryDto(4, "Castlevania", "RPG", 59.99m, new DateOnly(1986, 9, 26)),
   ];

   public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
   {
      var group = app.MapGroup("games");

      group.MapGet("/", async (GameStoreContext dbContext) =>
         await dbContext.Games
         .Include(g => g.Genre)
         .Select(g => g.ToSummaryDto())
         .AsNoTracking()
         .ToListAsync()
      );


      group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
      {
         Game? game = await dbContext.Games.FindAsync(id);
         return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDto());
      }).WithName("GetGameById");


      group.MapPost("/", async (CreateGameDto created, GameStoreContext dbContext) =>
      {
         Game game = created.ToEntity();
         dbContext.Games.Add(game);
         await dbContext.SaveChangesAsync();

         return Results.CreatedAtRoute("GetGameById", new { id = game.Id }, game.ToDetailsDto());
      }).WithParameterValidation();


      group.MapPut("/{id}", async (int id, UpdateGameDto updateDto, GameStoreContext dbContext) =>
      {
         var existing = await dbContext.Games.FindAsync(id);
         if (existing is null)
         {
            return Results.NotFound();
         }

         dbContext.Entry(existing).CurrentValues.SetValues(updateDto.ToEntity(id));
         await dbContext.SaveChangesAsync();
         return Results.NoContent();
      }).WithParameterValidation();


      group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
      {
         await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();
         return Results.NoContent();
      });


      return group;
   }
}
