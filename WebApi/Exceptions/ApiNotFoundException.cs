namespace WebApi.DemoErrorHandling.Exceptions;

public class ApiNotFoundException(string message = "Resource Not Found") : Exception(message)
{
}