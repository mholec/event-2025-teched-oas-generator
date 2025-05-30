using System.Text;
using Codegen.Helpers;
using Microsoft.OpenApi.Models;

namespace Codegen;

public class ValidatorGenerator(OpenApiSchema schema, string name)
{
    private readonly StringBuilder _sb = new();
    public void Generate()
    {
        _sb.AppendLine("using System;");
        _sb.AppendLine("using System.Collections.Generic;");
        _sb.AppendLine("using System.Linq;");
        _sb.AppendLine("using WebApi.Contracts;");
        _sb.AppendLine("using FluentValidation;");
        _sb.AppendLine();
        _sb.AppendLine($"namespace WebApi.Validators;");
        _sb.AppendLine();
        _sb.AppendLine($"public class {name}Validator : AbstractValidator<{name}>");
        _sb.AppendLine( "{");
        _sb.AppendLine($"    public {name}Validator()");
        _sb.AppendLine("    {");

        foreach (var prop in schema.Properties)
        {
            string validationRules = BuildValidationRules(prop.Value);

            _sb.AppendLine($"        RuleFor(x => x.{GenHelpers.ToPascalCase(prop.Key)}){validationRules};");
        }

        _sb.AppendLine("    }");
        _sb.AppendLine("}");


        string result = _sb.ToString();
        Directory.CreateDirectory(Path.Combine(Program.BasePath, "Validators"));
        string filePath = Path.Combine(Program.BasePath, "Validators", $"{name}Validator.cs");
        File.WriteAllText(filePath, result);
    }

    private string BuildValidationRules(OpenApiSchema propValue)
    {
        StringBuilder validations = new();
        if (propValue.Minimum != null)
        {
            validations.Append(".GreaterThan(" + propValue.Minimum + ")");
        }

        if (propValue.Maximum != null)
        {
            validations.Append(".LessThan(" + propValue.Maximum + ")");
        }

        if (propValue.MaxLength != null)
        {
            validations.Append(".MaximumLength(" + propValue.MaxLength + ")");
        }

        if (propValue.MinLength != null)
        {
            validations.Append(".MinimumLength(" + propValue.MinLength + ")");
        }

        if (propValue.Pattern != null)
        {
            validations.Append(".Matches(\"" + propValue.Pattern + "\")");
        }

        return validations.ToString();
    }
}