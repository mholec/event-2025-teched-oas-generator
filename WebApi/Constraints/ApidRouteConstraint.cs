using System.Text.RegularExpressions;

namespace WebApi.Constraints;

public partial class ApidRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var routeValue = values.FirstOrDefault();
        return ApidRegex().IsMatch(routeValue.Value?.ToString() ?? "");
    }

    [GeneratedRegex("^[a-z0-9]{8}$", RegexOptions.IgnoreCase, "cs-CZ")]
    private static partial Regex ApidRegex();
}