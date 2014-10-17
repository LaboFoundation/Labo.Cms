namespace Labo.Cms.Core.HttpHandlers
{
    using System.Web;
    using System.Web.Routing;
    using System.Web.SessionState;

    internal class LaboCmsHttpHandler : IHttpHandler, IRequiresSessionState
    {
        protected readonly RequestContext RequestContext;

        private readonly IHttpHandler m_HttpHandler;

        protected readonly IPageContextScopeManager PageContextScopeManager;

        public LaboCmsHttpHandler(RequestContext requestContext, IHttpHandler httpHandler, IPageContextScopeManager pageContextScopeManager)
        {
            RequestContext = requestContext;
            PageContextScopeManager = pageContextScopeManager;
            m_HttpHandler = httpHandler;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            using (PageContextScopeManager.CreatePageContextScope(RequestContext))
            {
                m_HttpHandler.ProcessRequest(context);
            }
        }
    }
}