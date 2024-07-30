using EarlyRetirement.Api.UserManagement.Models;
using EarlyRetirement.Api.Utils;
using EarlyRetirement.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EarlyRetirement.Api.UserManagement;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthApi(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("/register", Register)
            .AddEndpointFilter<ValidationEndpointFilter<RegisterViewModel>>();
        return groupBuilder;
    }

    public static async Task<Ok> Register(
        [FromBody] RegisterViewModel user, [FromServices] IUserManager manager)
    {
        await manager.RegisterUserAsync(user.ToRegisterDto());
        return TypedResults.Ok();
    }
}