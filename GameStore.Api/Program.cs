using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStoreConnection");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();
app.MapGamesEndpoints();
app.MapGenreEndpoints();
await app.MigrateDbAsync();
app.Run();
