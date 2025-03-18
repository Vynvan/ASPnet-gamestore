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
      Genre genre = GetGenreById(game.GenreId);

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


   public void DeleteGame(int id)
   {
      GameSummary? game = GetGameById(id);
      games.Remove(game);
   }


   public GameDetails GetGame(int id)
   {
      GameSummary? game = GetGameById(id);

      var genre = genres.Single(g => string.Equals(g.Name, game.Genre, StringComparison.OrdinalIgnoreCase));
      return new GameDetails
      {
         Id = game.Id,
         Name = game.Name,
         GenreId = genre.Id.ToString(),
         Prize = game.Prize,
         ReleaseDate = game.ReleaseDate
      };
   }


   public void UpdateGame(GameDetails game)
   {
      GameSummary? existingGame = GetGameById(game.Id);
      Genre genre = GetGenreById(game.GenreId);

      existingGame.Name = game.Name;
      existingGame.Genre = genre.Name;
      existingGame.Prize = game.Prize;
      existingGame.ReleaseDate = game.ReleaseDate;
   }


   private GameSummary GetGameById(int id)
   {
      GameSummary? game = games.Find(g => g.Id == id);
      ArgumentNullException.ThrowIfNull(game);
      return game;
   }


    private Genre GetGenreById(string? id)
   {
      ArgumentException.ThrowIfNullOrWhiteSpace(id);
      var genre = genres.Single(g => g.Id == int.Parse(id));
      return genre;
   }

}
