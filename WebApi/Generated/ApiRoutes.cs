using WebApi.Endpoints;

namespace WebApi;

public static class ApiRoutes
{
    public static IEndpointRouteBuilder RegisterApiRoutes(this IEndpointRouteBuilder api)
    {
        api.GetWorkshops();
        api.CreateWorkshop();
        api.GetWorkshop();
        api.UpdateWorkshop();
        api.DeleteWorkshop();
        api.GetRegistrations();
        api.DeleteRegistration();

        return api;
    }
}

