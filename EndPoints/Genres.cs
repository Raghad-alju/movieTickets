using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movieTickets.Models.DTO;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using System.Net;

namespace movieTickets.EndPoints
{
    public static class Genres
    {
        public static void RegisterGenreEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/genre", GetAllGenres)
               .WithName("GetGenres").Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/genre/{id:int}", GetGenre)
                .WithName("GetGenre").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/genre", CreateGenre)
                .WithName("CreateGenre")
                .Accepts<GenreDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/genre", UpdateGenre)
                .WithName("UpdateGenre")
                .Accepts<GenreDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/genre/{id:int}", DeleteGenre);

        }
        private async static Task<IResult> GetGenre(IGenreRepository _genreRepo, ILogger<Program> _logger, int id)
        {
            Console.WriteLine("Endpoint executed.");
            APIResponse response = new();
            response.Result = await _genreRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }




        // [Authorize]
        private async static Task<IResult> CreateGenre(IGenreRepository _genreRepo, IMapper _mapper,
                 [FromBody] GenreDTO genre_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (_genreRepo.GetAsync(genre_dto.GenreName).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Genre title already Exists");
                return Results.BadRequest(response);
            }

            Genre genre = _mapper.Map<Genre>(genre_dto);


            await _genreRepo.CreateAsync(genre);
            await _genreRepo.SaveAsync();
            GenreDTO genreDTO = _mapper.Map<GenreDTO>(genre);


            response.Result = genreDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
            //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
            //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
        }
        // [Authorize]
        private async static Task<IResult> UpdateGenre(IGenreRepository _genreRepo, IMapper _mapper,
                 [FromBody] GenreDTOupdate genre_u_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            await _genreRepo.UpdateAsync(_mapper.Map<Genre>(genre_u_dto));
            await _genreRepo.SaveAsync();

            response.Result = _mapper.Map<GenreDTO>(await _genreRepo.GetAsync(genre_u_dto.GenreId)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        //  [Authorize]
        private async static Task<IResult> DeleteGenre(IGenreRepository _genreRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            Genre oneGenre = await _genreRepo.GetAsync(id);
            if (oneGenre != null)
            {
                await _genreRepo.RemoveAsync(oneGenre);
                await _genreRepo.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invalid Id");
                return Results.BadRequest(response);
            }
        }

        private async static Task<IResult> GetAllGenres(IGenreRepository _genreRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Genres");
            response.Result = await _genreRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}

