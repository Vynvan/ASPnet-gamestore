namespace GameStore.Frontend.Models;

public class GameSummary
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required string Genre { get; set; }
   public decimal Prize { get; set; }
   public DateOnly ReleaseDate { get; set; }
}
