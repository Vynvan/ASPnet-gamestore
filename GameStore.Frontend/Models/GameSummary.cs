using System;

namespace GameStore.Frontend.Models;

public class GameSummary
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required string Genre { get; set; }
   public required decimal Prize { get; set; }
   public required DateOnly ReleaseDate { get; set; }
}
