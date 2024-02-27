using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movieTickets.Models.DTO;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using System.Net;

namespace movieTickets.EndPoints
{
    public static class Locations
    {
        public static void RegisterLocationEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/location", GetAllLocations)
               .WithName("GetLocations").Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/location/{id:int}", GetLocation)
                .WithName("GetLocation").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/location", CreateLocation)
                .WithName("CreateLocation")
                .Accepts<LocationDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/location", UpdateLocation)
                .WithName("UpdateLocation")
                .Accepts<LocationDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/location/{id:int}", DeleteLocation);

        }
        private async static Task<IResult> GetLocation(ILocationRepository _locationRepo, ILogger<Program> _logger, int id)
        {
            Console.WriteLine("Endpoint executed.");
            APIResponse response = new();
            response.Result = await _locationRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }




        // [Authorize]
        private async static Task<IResult> CreateLocation(ILocationRepository _locationRepo, IMapper _mapper,
                 [FromBody] LocationDTO location_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (_locationRepo.GetAsync(location_dto.City).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Location title already Exists");
                return Results.BadRequest(response);
            }

            Location location = _mapper.Map<Location>(location_dto);


            await _locationRepo.CreateAsync(location);
            await _locationRepo.SaveAsync();
            LocationDTO locationDTO = _mapper.Map<LocationDTO>(location);


            response.Result = locationDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
            //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
            //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
        }
        // [Authorize]
        private async static Task<IResult> UpdateLocation(ILocationRepository _locationRepo, IMapper _mapper,
                 [FromBody] LocationDTOupdate location_u_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            await _locationRepo.UpdateAsync(_mapper.Map<Location>(location_u_dto));
            await _locationRepo.SaveAsync();

            response.Result = _mapper.Map<LocationDTO>(await _locationRepo.GetAsync(location_u_dto.LocationId)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        //  [Authorize]
        private async static Task<IResult> DeleteLocation(ILocationRepository _locationRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            Location oneLocation = await _locationRepo.GetAsync(id);
            if (oneLocation != null)
            {
                await _locationRepo.RemoveAsync(oneLocation);
                await _locationRepo.SaveAsync();
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

        private async static Task<IResult> GetAllLocations(ILocationRepository _locationRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Locations");
            response.Result = await _locationRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}

