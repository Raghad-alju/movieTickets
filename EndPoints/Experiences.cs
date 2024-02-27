using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movieTickets.Models.DTO;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using System.Net;

namespace movieTickets.EndPoints
{
    public static class Experiences
    {
        public static void RegisterExperienceEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/experience", GetAllExperiences)
               .WithName("GetExperiences").Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/experience/{id:int}", GetExperience)
                .WithName("GetExperience").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/experience", CreateExperience)
                .WithName("CreateExperience")
                .Accepts<ExperienceDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/experience", UpdateExperience)
                .WithName("UpdateExperience")
                .Accepts<ExperienceDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/experience/{id:int}", DeleteExperience);

        }
        private async static Task<IResult> GetExperience(IExperienceRepository _expRepo, ILogger<Program> _logger, int id)
        {
            Console.WriteLine("Endpoint executed.");
            APIResponse response = new();
            response.Result = await _expRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }




        // [Authorize]
        private async static Task<IResult> CreateExperience(IExperienceRepository _expRepo, IMapper _mapper,
                 [FromBody] ExperienceDTO exp_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (_expRepo.GetAsync(exp_dto.Name).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Experience already Exists");
                return Results.BadRequest(response);
            }

            Experience experience = _mapper.Map<Experience>(exp_dto);


            await _expRepo.CreateAsync(experience);
            await _expRepo.SaveAsync();
            ExperienceDTO experienceDTO = _mapper.Map<ExperienceDTO>(experience);


            response.Result = experienceDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
            //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
            //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
        }
        // [Authorize]
        private async static Task<IResult> UpdateExperience(IExperienceRepository _expRepo, IMapper _mapper,
                 [FromBody] ExperienceDTOupdate exp_u_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            await _expRepo.UpdateAsync(_mapper.Map<Experience>(exp_u_dto));
            await _expRepo.SaveAsync();

            response.Result = _mapper.Map<ExperienceDTO>(await _expRepo.GetAsync(exp_u_dto.ExperienceId)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        //  [Authorize]
        private async static Task<IResult> DeleteExperience(IExperienceRepository _expRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            Experience oneExp = await _expRepo.GetAsync(id);
            if (oneExp != null)
            {
                await _expRepo.RemoveAsync(oneExp);
                await _expRepo.SaveAsync();
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

        private async static Task<IResult> GetAllExperiences(IExperienceRepository _expRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Experiences");
            response.Result = await _expRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}

