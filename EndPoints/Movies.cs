using movieTickets.data_context;
using System.Net;
using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using movieTickets.Models.DTO;

namespace movieTickets.EndPoints
{
    public static class Movies
    {
        public static void RegisterMovieEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/movies", GetAllMovies)
               .WithName("GetMovies").Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/movie/{id:int}", GetMovie)
                .WithName("GetMovie").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/movie", CreateMovie)
                .WithName("CreateMovie")
                .Accepts<MovieDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/movie", UpdateMovie)
                .WithName("UpdateMovie")
                .Accepts<MovieDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/movie/{id:int}", DeleteMovie);

        }
            private async static Task<IResult> GetMovie(IMovieRepository _movieRepo, ILogger<Program> _logger, int id)
            {
                Console.WriteLine("Endpoint executed.");
                APIResponse response = new();
                response.Result = await _movieRepo.GetAsync(id);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
            }




            // [Authorize]
            private async static Task<IResult> CreateMovie(IMovieRepository _movieRepo, IMapper _mapper,
                     [FromBody] MovieDTO movie_dto)
            {
                APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

                if (_movieRepo.GetAsync(movie_dto.Title).GetAwaiter().GetResult() != null)
                {
                    response.ErrorMessages.Add("Movie title already Exists");
                    return Results.BadRequest(response);
                }

                Movie movie = _mapper.Map<Movie>(movie_dto);


                await _movieRepo.CreateAsync(movie);
                await _movieRepo.SaveAsync();
                MovieDTO movieDTO = _mapper.Map<MovieDTO>(movie);


                response.Result = movieDTO;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                return Results.Ok(response);
                //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
                //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
            }
            // [Authorize]
            private async static Task<IResult> UpdateMovie(IMovieRepository _movieRepo, IMapper _mapper,
                     [FromBody] MovieDTOupdate movie_u_dto)
            {
                APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


                await _movieRepo.UpdateAsync(_mapper.Map<Movie>(movie_u_dto));
                await _movieRepo.SaveAsync();

                response.Result = _mapper.Map<MovieDTO>(await _movieRepo.GetAsync(movie_u_dto.MovieId)); ;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
            }
            //  [Authorize]
            private async static Task<IResult> DeleteMovie(IMovieRepository _movieRepo, int id)
            {
                APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


                Movie oneMovie = await _movieRepo.GetAsync(id);
                if (oneMovie != null)
                {
                    await _movieRepo.RemoveAsync(oneMovie);
                    await _movieRepo.SaveAsync();
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

            private async static Task<IResult> GetAllMovies(IMovieRepository _movieRepo, ILogger<Program> _logger)
            {
                APIResponse response = new();
                _logger.Log(LogLevel.Information, "Getting all Movies");
                response.Result = await _movieRepo.GetAllAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
            }
        }
    }

