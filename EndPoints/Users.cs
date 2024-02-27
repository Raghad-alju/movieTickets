using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Models.DTO.AuthDTO;
using movieTickets.Repository.IRepository;
using movieTickets.Services;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace movieTickets.EndPoints
{
    public static class Users
    {
        

        public static void ConfigureUsersEndpoints(this WebApplication app)
        {

            app.MapPost("/api/login", Login).WithName("Login").Accepts<UserDTO>("application/json")
                .Produces<APIResponse>(200).Produces(400);
            app.MapPost("/api/register", Register).WithName("Register").Accepts<UserDTO>("application/json")
                .Produces<APIResponse>(200).Produces(400);
           
        }
        
        public static User user = new User();

        private static TokenService service = new TokenService();
        private async static Task<IResult> Register(
            [FromBody] UserDTO request)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            string passwordHash= BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;

            response.Result = user;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);

        }
        private async static Task<IResult> Login(
           [FromBody] UserDTO request )
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if(user.Username != request.Username) { 
            return Results.BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Results.BadRequest("Wrong password.");
            }
            var token = service.GenerateToken(user);

            response.Result = token;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);

        }

      
        /* private async static Task<IResult> Login(IAuthRepository _authRepo,
            [FromBody] LoginRequestDTO model)
         {
             APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
             var loginResponse = await _authRepo.Login(model);
             if (loginResponse == null)
             {
                 response.ErrorMessages.Add("Username or password is incorrect");
                 return Results.BadRequest(response);
             }
             response.Result = loginResponse;
             response.IsSuccess = true;
             response.StatusCode = HttpStatusCode.OK;
             return Results.Ok(response);

         }*/
    }
}

