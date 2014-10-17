namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.HttpHandlers;

    internal sealed class LaboCmsRouteHandler : IRouteHandler
    {
        private readonly IRouteHandler m_RouteHandler;

        private readonly IPageContextScopeManager m_PageContextScopeManager;

        private readonly SessionStateBehavior m_SessionStateBehavior;

        public LaboCmsRouteHandler(IRouteHandler routeHandler, IPageContextScopeManager pageContextScopeManager, SessionStateBehavior sessionStateBehavior)
        {
            m_RouteHandler = routeHandler;
            m_PageContextScopeManager = pageContextScopeManager;
            m_SessionStateBehavior = sessionStateBehavior;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            IHttpHandler httpHandler = m_RouteHandler.GetHttpHandler(requestContext);
            requestContext.HttpContext.SetSessionStateBehavior(m_SessionStateBehavior);

            IHttpAsyncHandler asyncHttpHandler = httpHandler as IHttpAsyncHandler;
            return asyncHttpHandler == null
                       ? new LaboCmsHttpHandler(requestContext, httpHandler, m_PageContextScopeManager)
                       : new LaboCmsHttpAsyncHandler(requestContext, asyncHttpHandler, m_PageContextScopeManager);
        }
    }
}