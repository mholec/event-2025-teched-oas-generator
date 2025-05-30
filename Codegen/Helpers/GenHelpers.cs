using Microsoft.OpenApi.Models;

namespace Codegen.Helpers;

public static class GenHelpers
{
    public static string ToPascalCase(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        var parts = str.Split(['-', '_'], StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < parts.Length; i++)
        {
            if (i == 0)
                parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
            else
                parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
        }
        return string.Join("", parts);
    }

    public static string ToClrType(OpenApiSchema schema, string innerName = "")
    {
        var clrType = schema
            switch
            {
                {Type:"integer"} => "int",
                {Type:"string", Format:"date"} => "DateTime",
                {Type:"string", Format:"date-time"} => "DateTime",
                {Type:"string", Format:"uuid"} => "Guid",
                {Type:"string"} => "string",
                {Type:"boolean"} => "bool",
                {Type:"number", Format:"decimal"} => "decimal",
                {Type:"number", Format:"float"} => "float",
                {Type:"number"} => "double",
                {Type:"array"} => $"List<{innerName}>",
                _ => "object"
            };

        if (schema.Nullable)
            clrType += "?";

        return clrType;
    }

    public static string ToLocationAnnotation(ParameterLocation? parameterIn)
    {
        return parameterIn switch
        {
            ParameterLocation.Path => "[FromRoute]",
            ParameterLocation.Query => "[FromQuery]",
            ParameterLocation.Header => "[FromHeader]",
            ParameterLocation.Cookie => "[FromCookie]",
            null => ""
        };
    }

    /// <summary>
    /// Vrací název metody dle HTTP protokolu (a Minimal APIs)
    /// Př.: Ok, Created, Accepted
    /// </summary>
    public static string GetReturnMethodName(string statusCode)
    {
        return statusCode
            switch
            {
                "200" => "Ok",
                "201" => "Created",
                "202" => "Accepted",
                "204" => "NoContent",
                _ => "Ok"
            };
    }

    public static string GetRouteWithConstraints(string route, List<OpenApiParameter> parameters)
    {
        foreach (var parameter in parameters)
        {
            var constraint = parameter.Schema
                switch
                {
                    {Type:"integer"} => "int",
                    {Type:"string", Format:"date"} => "datetime",
                    {Type:"string", Format:"date-time"} => "datetime",
                    {Type:"string", Format:"uuid"} => "guid",
                    {Type:"string", Format:"regex", Pattern:"^[a-z0-9]{8}$"} => $"apid",
                    {Type:"boolean"} => "bool",
                    {Type:"number", Format:"decimal"} => "decimal",
                    {Type:"number", Format:"float"} => "float",
                    {Type:"number"} => "double",
                    _ => ""
                };

            if (!string.IsNullOrEmpty(constraint))
            {
                string key = "{" + parameter.Name + "}";
                route = route.Replace(key, "{" + parameter.Name + ":" + constraint + "}");
            }
        }

        return route;
    }
}