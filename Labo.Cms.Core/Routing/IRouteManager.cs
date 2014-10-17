namespace Labo.Cms.Core.Routing
{
    public interface IRouteManager
    {
        void RegisterRoute(RouteInfo routeInfo);

        void InstallRoutes();
    }
}
