using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movieTickets.Models.DTO;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using System.Net;

namespace movieTickets.EndPoints
{
    public static class Tickets
    {
        public static void RegisterTicketEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/ticket", GetAllTickets)
               .WithName("GetTickets").Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly") ;

            app.MapGet("/api/ticket/{id:int}", GetTicket)
                .WithName("GetTicket").Produces<APIResponse>(200)
                ;

            app.MapPost("/api/ticket", CreateTicket)
                .WithName("CreateTicket")
                .Accepts<TicketDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400)
                ;

            app.MapPut("/api/ticket", UpdateTicket)
                .WithName("UpdateTicket")
                .Accepts<TicketDTOupdate>("application/json")
                .Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/ticket/{id:int}", DeleteTicket);

        }
        private async static Task<IResult> GetTicket(ITicketRepository _ticketRepo, ILogger<Program> _logger, int id)
        {
            Console.WriteLine("Endpoint executed.");
            APIResponse response = new();
            response.Result = await _ticketRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }




        // [Authorize]
        private async static Task<IResult> CreateTicket(ITicketRepository _ticketRepo, IMapper _mapper,
                 [FromBody] TicketDTO ticket_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (_ticketRepo.GetAsync(ticket_dto.TicketNumber).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Ticket Name already Exists");
                return Results.BadRequest(response);
            }

            Ticket ticket = _mapper.Map<Ticket>(ticket_dto);


            await _ticketRepo.CreateAsync(ticket);
            await _ticketRepo.SaveAsync();
            TicketDTO ticketDTO = _mapper.Map<TicketDTO>(ticket);


            response.Result = ticketDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
            //return Results.CreatedAtRoute("GetCoupon",new { id=coupon.Id }, couponDTO);
            //return Results.Created($"/api/coupon/{coupon.Id}",coupon);
        }
        // [Authorize]
        private async static Task<IResult> UpdateTicket(ITicketRepository _ticketRepo, IMapper _mapper,
                 [FromBody] TicketDTOupdate ticket_u_dto)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            await _ticketRepo.UpdateAsync(_mapper.Map<Ticket>(ticket_u_dto));
            await _ticketRepo.SaveAsync();

            response.Result = _mapper.Map<TicketDTO>(await _ticketRepo.GetAsync(ticket_u_dto.TicketId)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        //  [Authorize]
        private async static Task<IResult> DeleteTicket(ITicketRepository _ticketRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


            Ticket oneTicket = await _ticketRepo.GetAsync(id);
            if (oneTicket != null)
            {
                await _ticketRepo.RemoveAsync(oneTicket);
                await _ticketRepo.SaveAsync();
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

        private async static Task<IResult> GetAllTickets(ITicketRepository _ticketRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Tickets");
            response.Result = await _ticketRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}

