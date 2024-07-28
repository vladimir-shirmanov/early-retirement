using EarlyRetirement.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EarlyRetirement.Api.Tests.Utils;

public class ValidationEndpointFilterTests
{
    private ValidationEndpointFilter<TestValidatable> _filter;

    [SetUp]
    public void SetUp()
    {
        _filter = new ValidationEndpointFilter<TestValidatable>();
    }
    
    [Test]
    public async Task InvokeAsync_ShouldValidateObject_ReturnProblemIfInvalid()
    {
        var model = new TestValidatable(false);
        var context = new DefaultEndpointFilterInvocationContext(new DefaultHttpContext(), model);

        var result = await _filter.InvokeAsync(context, _ => new ValueTask<object?>());
        
        Assert.That(result, Is.TypeOf<ValidationProblem>());
    }
    
    [Test]
    public async Task InvokeAsync_ShouldValidateObject_MoveNextIfValid()
    {
        var model = new TestValidatable(true);
        var context = new DefaultEndpointFilterInvocationContext(new DefaultHttpContext(), model);

        var result = await _filter.InvokeAsync(context, _ => new ValueTask<object?>());
        
        Assert.That(result, Is.Not.TypeOf<ValidationProblem>());
    }
}

public class TestValidatable(bool isValid) : IValidatable
{
    private bool _isValid = isValid;

    public Dictionary<string, string[]> Validate()
    {
        return _isValid
            ? new Dictionary<string, string[]>()
            : new Dictionary<string, string[]>
            {
                ["ERROR"] = ["TEST ERROR"]
            };
    }
}