using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient(HttpClient httpClient)
{

   public async Task<GameSummary[]> GetGamesAsync() => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];


   public async Task AddGameAsync(GameDetails game)
   {
      var response = await httpClient.PostAsJsonAsync("games", game);
      if (!response.IsSuccessStatusCode)
      {
         var error = await response.Content.ReadAsStringAsync();
         throw new InvalidOperationException($"Failed to add game: {response.StatusCode} - {error}");
      }
   }


   public async Task DeleteGameAsync(int id) => await httpClient.DeleteAsync($"games/{id}");


   public async Task<GameDetails> GetGameAsync(int id) => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}")
      ?? throw new InvalidOperationException($"Game with id {id} not found");


   public async Task UpdateGameAsync(GameDetails game) => await httpClient.PutAsJsonAsync($"games/{game.Id}", game);

}
