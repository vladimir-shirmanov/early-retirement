namespace EarlyRetirement.Api.Utils;

public interface IValidatable
{
    Dictionary<string, string[]> Validate();
}