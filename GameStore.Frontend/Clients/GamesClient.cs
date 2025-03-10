using System;
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

   public GameSummary[] GetGames() => [.. games];
}
