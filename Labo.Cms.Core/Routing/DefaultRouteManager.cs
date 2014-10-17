namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Web.Routing;

    public sealed class DefaultRouteManager : IRouteManager
    {
        private readonly RouteCollection m_RouteCollection;

        private readonly IPageContextScopeManager m_PageContextScopeManager;

        private readonly RouteInfoCollection m_RouteInfos;

        public DefaultRouteManager(RouteCollection routeCollection, IPageContextScopeManager pageContextScopeManager)
        {
            if (routeCollection == null)
            {
                throw new ArgumentNullException("routeCollection");
            }

            if (pageContextScopeManager == null)
            {
                throw new ArgumentNullException("pageContextScopeManager");
            }

            m_RouteCollection = routeCollection;
            m_PageContextScopeManager = pageContextScopeManager;
            m_RouteCollection.AppendTrailingSlash = true;

            m_RouteInfos = new RouteInfoCollection();
        }

        public void RegisterRoute(RouteInfo routeInfo)
        {
            m_RouteInfos.Add(routeInfo);
        }

        public void InstallRoutes()
        {
            m_RouteInfos.SortByPriority();

            using (m_RouteCollection.GetWriteLock())
            {
                for (int i = 0; i < m_RouteInfos.Count; i++)
                {
                    RouteInfo routeInfo = m_RouteInfos[i];

                    m_RouteCollection.Add(routeInfo.Name, new LaboCmsRoute(routeInfo.Route, m_PageContextScopeManager, routeInfo.SessionState));
                }
            }
        }
    }
}