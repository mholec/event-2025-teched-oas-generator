using System.Text;
using Codegen.Dto;
using Codegen.Helpers;
using Microsoft.OpenApi.Models;

namespace Codegen;

public class ApiRoutesGenerator()
{
    private readonly StringBuilder _sb = new();
    public string WriteApiRoutes(string path, KeyValuePair<OperationType, OpenApiOperation> operation)
    {
        string methodName = GenHelpers.ToPascalCase(operation.Value.OperationId);

        // REQUEST
        var requestParameters = ResolveParameters(operation.Value.Parameters).ToList();
        var requestBody = operation.Value.RequestBody;

        var requestBodyType = ResolveRequestType(requestBody, operation.Value.OperationId);
        if (requestBodyType != null)
        {
            requestParameters.Add(new("model", requestBodyType, "[FromBody]"));
            requestParameters.Add(new("validator", $"ContractValidator", "[FromServices]"));
        }
        requestParameters.Add(new("db", $"AppDbContext", "[FromServices]"));

        // RESPONSE
        var successResponse = operation.Value.Responses.FirstOrDefault(x => x.Key.StartsWith("20"));
        string successResultMethod = GenHelpers.GetReturnMethodName(successResponse.Key);

        var responseSchema = successResponse.Value.Content.Values.Select(x => x.Schema).FirstOrDefault();
        ResolveReturnType(responseSchema, operation);



        var methodArguments = string.Join(", ", requestParameters.Select(x=> x.ToString()));
        string route = GenHelpers.GetRouteWithConstraints(path, operation.Value.Parameters.Where(x=> x.In == ParameterLocation.Path).ToList());

        _sb.AppendLine($"using FluentValidation;");
        _sb.AppendLine($"using Microsoft.AspNetCore.Mvc;");
        _sb.AppendLine($"using WebApi.Contracts;");
        _sb.AppendLine($"using WebApi.Data;");
        _sb.AppendLine($"using WebApi.DemoErrorHandling;");
        _sb.AppendLine();
        _sb.AppendLine($"namespace WebApi.Endpoints;");
        _sb.AppendLine();
        _sb.AppendLine($"public static class {methodName}Api");
        _sb.AppendLine("{");
        _sb.AppendLine($"    ///");
        _sb.AppendLine($"    /// {operation.Value.Summary}");
        _sb.AppendLine($"    ///");
        _sb.AppendLine($"    public static RouteHandlerBuilder {methodName}(this IEndpointRouteBuilder api)");
        _sb.AppendLine("    {");
        _sb.AppendLine($"        return api.Map{operation.Key.ToString()}(\"{route}\", async ({methodArguments}) =>");
        _sb.AppendLine("        {");

        if (requestBodyType != null)
        {
            _sb.AppendLine( $"            validator.EnsureValid(model);");
            _sb.AppendLine();
        }

        _sb.AppendLine($"            return Results.{successResultMethod}();");
        _sb.AppendLine("        });");
        _sb.AppendLine("    }");
        _sb.AppendLine("}");


        string result = _sb.ToString();
        Directory.CreateDirectory(Path.Combine(Program.BasePath, "Endpoints"));
        string filePath = Path.Combine(Program.BasePath, "Endpoints", $"{GenHelpers.ToPascalCase(operation.Value.OperationId)}Api.cs");
        File.WriteAllText(filePath, result);

        return methodName;
    }

    private IEnumerable<Member> ResolveParameters(IList<OpenApiParameter> parameters)
    {
        foreach (var parameter in parameters)
        {
            yield return new (parameter.Name, GenHelpers.ToClrType(parameter.Schema), GenHelpers.ToLocationAnnotation(parameter.In ?? null));
        }
    }

    private void ResolveReturnType(OpenApiSchema schema, KeyValuePair<OperationType, OpenApiOperation> operation)
    {
        if (schema?.Type != "object")
        {
            return;
        }

        string className = $"{GenHelpers.ToPascalCase(operation.Value.OperationId)}Result";

        var contractGen = new ContractGenerator(schema, className);
        contractGen.Generate();
    }

    private string ResolveRequestType(OpenApiRequestBody schema, string operationId)
    {
        if (schema != null)
        {
            string className = $"{GenHelpers.ToPascalCase(operationId)}Request";

            var bodySchema = schema.Content.Values.Select(x => x.Schema).FirstOrDefault();
            var contractGen = new ContractGenerator(bodySchema, className);
            contractGen.Generate();

            var validatorGen = new ValidatorGenerator(bodySchema, className);
            validatorGen.Generate();

            return className;
        }

        return null;
    }
}