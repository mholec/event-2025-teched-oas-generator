using FluentValidation;
using FluentValidation.Results;
using WebApi.DemoErrorHandling.Contracts;
using WebApi.DemoErrorHandling.Exceptions;

namespace WebApi.DemoErrorHandling;

public class ContractValidator(IServiceProvider serviceProvider)
{
    public void EnsureValid<T>(T? contract)
    {
        if (contract is null)
        {
            throw new ApiEmptyBodyException();
        }

        var validators = serviceProvider.GetServices<IValidator<T>>().ToList();
        if (!validators.Any())
        {
            return;
        }

        var failures = new List<ValidationFailure>();
        foreach (var validator in validators)
        {
            var result = validator.Validate(contract);
            if (!result.IsValid)
            {
                failures.AddRange(result.Errors);
            }
        }

        if (failures.Any())
        {
            throw new ApiValidationException(failures.Select(x => new ValidationError(x.ErrorMessage, x.PropertyName)).ToList());
        }
    }
}