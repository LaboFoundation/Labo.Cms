namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using System.Web.SessionState;

    public sealed class LaboCmsRoute : RouteBase
    {
        private readonly RouteBase m_Route;

        private readonly IPageContextScopeManager m_PageContextScopeManager;

        private readonly SessionStateBehavior m_SessionStateBehavior;

        public LaboCmsRoute(RouteBase route, IPageContextScopeManager pageContextScopeManager, SessionStateBehavior sessionStateBehavior)
        {
            m_Route = route;
            m_PageContextScopeManager = pageContextScopeManager;
            m_SessionStateBehavior = sessionStateBehavior;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            RouteData routeData = m_Route.GetRouteData(httpContext);
            if (routeData == null)
            {
                return null;
            }

            httpContext.SetSessionStateBehavior(m_SessionStateBehavior);

            routeData.RouteHandler = new LaboCmsRouteHandler(routeData.RouteHandler, m_PageContextScopeManager, m_SessionStateBehavior);

            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            RequestContext effectiveRequestContext = requestContext;
            VirtualPathData virtualPath = m_Route.GetVirtualPath(effectiveRequestContext, values);
            if (virtualPath == null)
            {
                return null;
            }

            return virtualPath;
        }
    }
}