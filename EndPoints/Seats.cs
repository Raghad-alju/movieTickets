using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movieTickets.Models.DTO;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using FluentValidation;

using System.Net;

namespace movieTickets.EndPoints
{
    public static class Seats
    {
        public static void RegisterSeatEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/seat", GetAllSeats)
               .WithName("GetSeats").Produces<APIResponse>(200)
                .RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/seat/{id:int}", GetSeat)
                .WithName("GetSeat").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/seat", CreateSeat)
                .WithName("CreateSeat")
                .Accepts<SeatDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/seat", UpdateSeat)
                .WithName("UpdateSeat")
                .Accepts<SeatDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/seat/{id:int}", DeleteSeat);

        }
        private async static Task<IResult> GetSeat(ISeatRepository _seatRepo, ILogger<Program> _logger, int id)
        {
            Console.WriteLine("Endpoint executed.");
            APIResponse response = new();
            response.Result = await _seatRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }




        // [Authorize]
        private async static Task<IResult> CreateSeat(ISeatRepository _seatRepo, IMapper _mapper,
                 [FromBody] SeatDTO seat_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (_seatRepo.GetAsync(seat_dto.SeatNumber).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Seat Name already Exists");
                return Results.BadRequest(response);
            }

            Seat seat = _mapper.Map<Seat>(seat_dto);


            await _seatRepo.CreateAsync(seat);
            await _seatRepo.SaveAsync();
            SeatDTO seatDTO = _mapper.Map<SeatDTO>(seat);


            response.Result = seatDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
            //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
            //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
        }
        // [Authorize]
        private async static Task<IResult> UpdateSeat(ISeatRepository _seatRepo, IMapper _mapper,
                 [FromBody] SeatDTOupdate seat_u_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            await _seatRepo.UpdateAsync(_mapper.Map<Seat>(seat_u_dto));
            await _seatRepo.SaveAsync();

            response.Result = _mapper.Map<SeatDTO>(await _seatRepo.GetAsync(seat_u_dto.SeatId)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        //  [Authorize]
        private async static Task<IResult> DeleteSeat(ISeatRepository _seatRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            Seat oneSeat = await _seatRepo.GetAsync(id);
            if (oneSeat != null)
            {
                await _seatRepo.RemoveAsync(oneSeat);
                await _seatRepo.SaveAsync();
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

        private async static Task<IResult> GetAllSeats(ISeatRepository _seatRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Seats");
            response.Result = await _seatRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
