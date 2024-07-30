using System.Net;
using System.Net.Http.Json;
using EarlyRetirement.Api.UserManagement.Models;
using EarlyRetirement.Infrastructure;
using EarlyRetirement.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EarlyRetirement.IntegrationTests;

public class AuthEndpointTests
{
    private TestWebApplicationFactory<Program> _factory;
    private HttpClient _httpClient;
    [OneTimeSetUp]
    public void Setup()
    {
        _factory = new TestWebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
    }
    
    public static IEnumerable<object[]> InvalidUsers => new List<object[]>
    {
        new object[] { new RegisterViewModel { Email = "vova", Password = "123", ConfirmPassword = "123"}, "Email is invalid"},
        new object[] { new RegisterViewModel { Email = "vova@mail.com", Password = "123", ConfirmPassword = "321"}, "Passwords don't match"}
    };

    [TestCaseSource(nameof(InvalidUsers))]
    public async Task RegisterUserWithValidationProblems_ShouldReturnBadRequestWithMessage(RegisterViewModel user, string validationError)
    {
        var response = await _httpClient.PostAsJsonAsync("/users/register", user);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        
        var problemResult = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        Assert.That(problemResult, Is.Not.Null);
        Assert.That(problemResult.Errors, Has.One.Matches<KeyValuePair<string, string[]>>(error => error.Value.First() == validationError));
    }

    [Test]
    public async Task RegisterUserWithValidUser_ShouldReturnOk()
    {
        //arrange
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<EarlyRetirementDbContext>();
        if (db != null && db.Users.Any())
        {
            db.Users.RemoveRange(db.Users);
            await db.SaveChangesAsync();
        }
        
        
        //act
        var response = await _httpClient.PostAsJsonAsync("/users/register", new RegisterViewModel
        {
            Email = "vova@mail.com",
            Password = "123",
            ConfirmPassword = "123"
        });
        
        //assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
        _factory.Dispose();
    }
}