using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
   private readonly List<GameSummary> games =
   [
      new() {
         Id = 1,
         Name = "Street Fighter II",
         Genre = "Fighting",
         Prize = 19.99M,
         ReleaseDate = new DateOnly(1992, 7, 15)
      },
      new() {
         Id = 2,
         Name = "Final Fantasy XIV",
         Genre = "Roleplaying",
         Prize = 59.99M,
         ReleaseDate = new DateOnly(2010, 9, 30)
      },
      new() {
         Id = 3,
         Name = "FIFA 23",
         Genre = "Sports",
         Prize = 69.99M,
         ReleaseDate = new DateOnly(2022, 9, 27)
      }
   ];

   private readonly Genre[] genres = new GenreClient().GetGenres();

   public GameSummary[] GetGames() => [.. games];

   public void AddGame(GameDetails game)
   {
      ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);
      var genre = genres.Single(g => g.Id == int.Parse(game.GenreId));

      var summary = new GameSummary
      {
         Id = games.Count + 1,
         Name = game.Name,
         Genre = genre.Name,
         Prize = game.Prize,
         ReleaseDate = game.ReleaseDate
      };
      games.Add(summary);
   }
}
