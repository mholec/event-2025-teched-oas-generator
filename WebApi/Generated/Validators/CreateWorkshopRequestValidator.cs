using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Contracts;
using FluentValidation;

namespace WebApi.Validators;

public class CreateWorkshopRequestValidator : AbstractValidator<CreateWorkshopRequest>
{
    public CreateWorkshopRequestValidator()
    {
        RuleFor(x => x.Slug).MaximumLength(300);
        RuleFor(x => x.Title).MaximumLength(300);
        RuleFor(x => x.StartDate);
        RuleFor(x => x.Capacity).GreaterThan(0).LessThan(100);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
