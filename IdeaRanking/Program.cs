using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using IdeaRanking.Data;
using IdeaRanking.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adding Context
var connString = builder.Configuration.GetConnectionString("IdeaRankingContext");
connString = connString.Replace("<username>", builder.Configuration["DB_USERNAME"]);
connString = connString.Replace("<password>", builder.Configuration["DB_PASSWORD"]);

builder.Services.AddDbContext<IdeaRankingContext>(options =>
    options.UseSqlServer(connString));



builder.Services.AddControllers();
// .AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
// });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IIdeasRepository, IdeasRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IEloCalculator, EloCalculator>();
var app = builder.Build();
// Seeding Data
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;

//     SeedData.Initialize(services);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();