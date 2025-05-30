namespace WebApi.DemoErrorHandling.Contracts;

public class ValidationError(string message, string property = "")
{
    public string Property { get; set; } = property;
    public string Message { get; set; } = message;
}