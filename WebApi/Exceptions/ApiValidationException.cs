
using WebApi.DemoErrorHandling.Contracts;

namespace WebApi.DemoErrorHandling.Exceptions;

public class ApiValidationException : Exception
{
    public List<ValidationError> Errors { get; }
    
    public ApiValidationException()
    {
    }

    public ApiValidationException(ValidationError error)
    {
        Errors = [error];
    }

    public ApiValidationException(List<ValidationError> errors)
    {
        Errors.AddRange(errors);
    }
}