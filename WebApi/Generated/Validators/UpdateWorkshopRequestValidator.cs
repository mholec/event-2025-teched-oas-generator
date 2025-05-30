using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Contracts;
using FluentValidation;

namespace WebApi.Validators;

public class UpdateWorkshopRequestValidator : AbstractValidator<UpdateWorkshopRequest>
{
    public UpdateWorkshopRequestValidator()
    {
        RuleFor(x => x.Title).MaximumLength(300);
        RuleFor(x => x.StartDate);
        RuleFor(x => x.Capacity).GreaterThan(0).LessThan(100);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
