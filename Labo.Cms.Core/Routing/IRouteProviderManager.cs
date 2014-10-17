namespace Labo.Cms.Core.Routing
{
    public interface IRouteProviderManager
    {
        void RegisterRouteProvider(IRouteProvider routeProvider);

        void RegisterRoutes(IRouteManager routeManager);
    }
}