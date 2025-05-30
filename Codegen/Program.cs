using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Codegen;

public class Program
{
    public const string BasePath = "/Users/mholec/EDU/Pr\u030cedna\u0301s\u030cky/2025-05 TechEd Na\u0301vrh a vy\u0301voj REST API s OAS/_C6_generator/WebApi/Generated";
    static async Task Main(string[] args)
    {
        var specification = await ReadOpenApiSpecification();
        var tags = specification.Tags.Select(x => x.Name).ToList();

        string[] restrictedTags = ["Auth", "Extra"];

        StringBuilder sb = new();

        sb.AppendLine("using WebApi.Endpoints;");
        sb.AppendLine();
        sb.AppendLine("namespace WebApi;");
        sb.AppendLine();
        sb.AppendLine("public static class ApiRoutes");
        sb.AppendLine("{");
        sb.AppendLine("    public static IEndpointRouteBuilder RegisterApiRoutes(this IEndpointRouteBuilder api)");
        sb.AppendLine("    {");

        foreach (var tag in tags.Where(t => !restrictedTags.Contains(t)))
        {
            foreach (var path in specification.Paths.OrderBy(x=> x.Key))
            {
                var operations = path.Value.Operations
                    .Where(x => x.Value.Tags.Select(y => y.Name).Contains(tag))
                    .OrderBy(x=> x.Key).ToList();

                foreach (var operation in operations)
                {
                    ApiRoutesGenerator gen = new();
                    string methodName = gen.WriteApiRoutes(path.Key, operation);

                    sb.AppendLine($"        api.{methodName}();");
                }
            }
        }

        sb.AppendLine();
        sb.AppendLine("        return api;");

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();

        string result = sb.ToString();
        Directory.CreateDirectory(Path.Combine(BasePath, ""));
        string filePath = Path.Combine(BasePath, "", $"ApiRoutes.cs");
        await File.WriteAllTextAsync(filePath, result);
    }

    private static async Task<OpenApiDocument> ReadOpenApiSpecification()
    {
        var oasFilePath = Path.Combine(Directory.GetCurrentDirectory(), "workshopy.openapi.json");
        await using var stream = new FileStream(oasFilePath, FileMode.Open, FileAccess.Read);
        OpenApiDocument doc = new OpenApiStreamReader().Read(stream, out _);

        return doc;
    }
}