using EarlyRetirement.Api.UserManagement.Models;
using EarlyRetirement.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EarlyRetirement.Api.UserManagement;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthApi(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("/register", Register)
            .AddEndpointFilter(async (context, next) =>
            {
                var model = context.GetArgument<RegisterViewModel>(0);
                var problems = model.Validate();
                if (problems.Count > 0)
                {
                    return TypedResults.ValidationProblem(problems);
                } 
                return await next(context);
            });
        return groupBuilder;
    }

    public static async Task<Ok> Register(
        [FromBody] RegisterViewModel user, [FromServices] IUserManager manager)
    {
        await manager.RegisterUserAsync(user.ToRegisterDto());
        return TypedResults.Ok();
    }
}