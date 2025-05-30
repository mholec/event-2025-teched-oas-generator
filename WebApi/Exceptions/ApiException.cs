namespace WebApi.DemoErrorHandling.Exceptions;

public class ApiException(string message) : Exception(message)
{
}