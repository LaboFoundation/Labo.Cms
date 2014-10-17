namespace Labo.Cms.Core.Routing
{
    using System.Collections.Generic;

    public sealed class DefaultRouteProviderManager : IRouteProviderManager
    {
        private readonly IList<IRouteProvider> m_RouteProviders;

        public DefaultRouteProviderManager()
        {
            m_RouteProviders = new List<IRouteProvider>();
        }

        public void RegisterRouteProvider(IRouteProvider routeProvider)
        {
            m_RouteProviders.Add(routeProvider);
        }

        public void RegisterRoutes(IRouteManager routeManager)
        {
            for (int i = 0; i < m_RouteProviders.Count; i++)
            {
                IRouteProvider routeProvider = m_RouteProviders[i];
                routeProvider.RegisterRoutes(routeManager);
            }
        }
    }
}