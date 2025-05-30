using System.Text;
using Codegen.Helpers;
using Microsoft.OpenApi.Models;

namespace Codegen;

public class ContractGenerator(OpenApiSchema schema, string name)
{
    private readonly StringBuilder _sb = new();
    public void Generate()
    {
        _sb.AppendLine("using System;");
        _sb.AppendLine("using System.Collections.Generic;");
        _sb.AppendLine("using System.Linq;");
        _sb.AppendLine("using System.Threading.Tasks;");
        _sb.AppendLine();
        _sb.AppendLine($"namespace WebApi.Contracts");
        _sb.AppendLine("{");
        _sb.AppendLine($"    public partial class {name}");
        _sb.AppendLine( "    {");

        foreach (var prop in schema.Properties)
        {
            // array item
            // TODO: toto není správně, ten innertype se musí dělat reurzivně jinak
            string innerName = "object";
            if (prop.Value.Items != null && prop.Key == "items")
            {
                innerName = name + "Item";
                var cg2 = new ContractGenerator(prop.Value.Items, innerName);
                cg2.Generate();
            }

            string clrType = GenHelpers.ToClrType(prop.Value, innerName);

            _sb.AppendLine($"        public {clrType} {GenHelpers.ToPascalCase(prop.Key)} {{ get; set; }}");
        }

        _sb.AppendLine("    }");
        _sb.AppendLine("}");

        string result = _sb.ToString();
        Directory.CreateDirectory(Path.Combine(Program.BasePath, "Contracts"));
        string filePath = Path.Combine(Program.BasePath, "Contracts", $"{name}.cs");
        File.WriteAllText(filePath, result);
    }
}