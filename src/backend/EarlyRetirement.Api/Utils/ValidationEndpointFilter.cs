namespace EarlyRetirement.Api.Utils;

public class ValidationEndpointFilter<T> : IEndpointFilter where T: IValidatable
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<T>(0);
        var problems = model.Validate();
        if (problems.Count > 0)
        {
            return TypedResults.ValidationProblem(problems);
        } 
        return await next(context);
    }
}