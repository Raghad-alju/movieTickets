using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movieTickets.Models.DTO;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using System.Net;

namespace movieTickets.EndPoints
{
    public static class Theaters
    {
        public static void RegisterTheaterEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/theater", GetAllTheaters)
               .WithName("GetTheaters").Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/theater/{id:int}", GetTheater)
                .WithName("GetTheater").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/theater", CreateTheater)
                .WithName("CreateTheater")
                .Accepts<TheaterDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/theater", UpdateTheater)
                .WithName("UpdateTheater")
                .Accepts<TheaterDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/theater/{id:int}", DeleteTheater);

        }
        private async static Task<IResult> GetTheater(ITheaterRepository _theaterRepo, ILogger<Program> _logger, int id)
        {
            Console.WriteLine("Endpoint executed.");
            APIResponse response = new();
            response.Result = await _theaterRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }




        // [Authorize]
        private async static Task<IResult> CreateTheater(ITheaterRepository _theaterRepo, IMapper _mapper,
                 [FromBody] TheaterDTO theater_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (_theaterRepo.GetAsync(theater_dto.TheaterName).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Theater Name already Exists");
                return Results.BadRequest(response);
            }

            Theater theater = _mapper.Map<Theater>(theater_dto);


            await _theaterRepo.CreateAsync(theater);
            await _theaterRepo.SaveAsync();
            TheaterDTO theaterDTO = _mapper.Map<TheaterDTO>(theater);


            response.Result = theaterDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
            //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
            //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
        }
        // [Authorize]
        private async static Task<IResult> UpdateTheater(ITheaterRepository _theaterRepo, IMapper _mapper,
                 [FromBody] TheaterDTOupdate theater_u_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            await _theaterRepo.UpdateAsync(_mapper.Map<Theater>(theater_u_dto));
            await _theaterRepo.SaveAsync();

            response.Result = _mapper.Map<TheaterDTO>(await _theaterRepo.GetAsync(theater_u_dto.TheaterId)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        //  [Authorize]
        private async static Task<IResult> DeleteTheater(ITheaterRepository _theaterRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            Theater oneTheater = await _theaterRepo.GetAsync(id);
            if (oneTheater != null)
            {
                await _theaterRepo.RemoveAsync(oneTheater);
                await _theaterRepo.SaveAsync();
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

        private async static Task<IResult> GetAllTheaters(ITheaterRepository _theaterRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Theaters");
            response.Result = await _theaterRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
