using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movieTickets.Models.DTO;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using System.Net;

namespace movieTickets.EndPoints
{
    public static class Times
    {
        public static void RegisterTimeEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/time", GetAllTimes)
               .WithName("GetTimes").Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/time/{id:int}", GetTime)
                .WithName("GetTime").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/time", CreateTime)
                .WithName("CreateTime")
                .Accepts<TimeDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/time", UpdateTime)
                .WithName("UpdateTime")
                .Accepts<TimeDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/time/{id:int}", DeleteTime);

        }
        private async static Task<IResult> GetTime(ITimeRepository _timeRepo, ILogger<Program> _logger, int id)
        {
            Console.WriteLine("Endpoint executed.");
            APIResponse response = new();
            response.Result = await _timeRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }




        // [Authorize]
        private async static Task<IResult> CreateTime(ITimeRepository _timeRepo, IMapper _mapper,
                 [FromBody] TimeDTO time_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (_timeRepo.GetAsync(time_dto.AvalibaleTime).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Time Name already Exists");
                return Results.BadRequest(response);
            }

            Time time = _mapper.Map<Time>(time_dto);


            await _timeRepo.CreateAsync(time);
            await _timeRepo.SaveAsync();
            TimeDTO timeDTO = _mapper.Map<TimeDTO>(time);


            response.Result = timeDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
            //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
            //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
        }
        // [Authorize]
        private async static Task<IResult> UpdateTime(ITimeRepository _timeRepo, IMapper _mapper,
                 [FromBody] TimeDTOupdate time_u_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            await _timeRepo.UpdateAsync(_mapper.Map<Time>(time_u_dto));
            await _timeRepo.SaveAsync();

            response.Result = _mapper.Map<TimeDTO>(await _timeRepo.GetAsync(time_u_dto.TimeId)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        //  [Authorize]
        private async static Task<IResult> DeleteTime(ITimeRepository _timeRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            Time oneTime = await _timeRepo.GetAsync(id);
            if (oneTime != null)
            {
                await _timeRepo.RemoveAsync(oneTime);
                await _timeRepo.SaveAsync();
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

        private async static Task<IResult> GetAllTimes(ITimeRepository _timeRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Times");
            response.Result = await _timeRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}

