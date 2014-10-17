namespace Labo.Cms.Core
{
    using Labo.Cms.Core.Routing;
    using Labo.Cms.Core.Services;

    public sealed class CmsApplication : ICmsApplication
    {
        private readonly ICmsService m_CmsService;

        private readonly IRouteManager m_RouteManager;

        private readonly IRouteProviderManager m_RouteProviderManager;

        public IRouteManager RouteManager
        {
            get
            {
                return m_RouteManager;
            }
        }

        public IRouteProviderManager RouteProviderManager
        {
            get
            {
                return m_RouteProviderManager;
            }
        }

        public ICmsService CmsService
        {
            get
            {
                return m_CmsService;
            }
        }

        public CmsApplication(ICmsService cmsService, IRouteManager routeManager, IRouteProviderManager routeProviderManager)
        {
            m_CmsService = cmsService;
            m_RouteManager = routeManager;
            m_RouteProviderManager = routeProviderManager;
        }

        public void Initialize()
        {
            m_RouteManager.InstallRoutes();
        }
    }
}
