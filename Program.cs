using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.EndPoints;
using FluentValidation;
using movieTickets.Repository;
using movieTickets.Repository.IRepository;
using movieTickets;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using movieTickets.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
*/

builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<ITheaterRepository, TheaterRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITimeRepository, TimeRepository>();
//builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAutoMapper(typeof(MappingConfig));


builder.Services.AddDbContext<DataContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();



var app = builder.Build();
var config = builder.Configuration.GetValue("AppSettings","Token");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureUsersEndpoints();

app.RegisterMovieEndpoints();
app.RegisterExperienceEndpoints();
app.RegisterTheaterEndpoints();
app.RegisterGenreEndpoints();
app.RegisterLocationEndpoints();
app.RegisterSeatEndpoints();
app.RegisterTicketEndpoints();
app.RegisterTimeEndpoints();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
