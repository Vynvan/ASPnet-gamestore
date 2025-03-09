using GameStore.Api.Data;
using GameStore.Api.Entities;
using GameStore.Api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{
   public static RouteGroupBuilder MapGenreEndpoints(this WebApplication app)
   {
      var group = app.MapGroup("genres");

      group.MapGet("/", async (GameStoreContext dbContext) =>
         await dbContext.Genres
         .Select(g => g.ToDto())
         .AsNoTracking()
         .ToListAsync());


      group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
      {
         Genre? genre = await dbContext.Genres.FindAsync(id);
         return genre is null ? Results.NotFound() : Results.Ok(genre.ToDto());
      }).WithName("GetGenreById");

      return group;
   }
}
