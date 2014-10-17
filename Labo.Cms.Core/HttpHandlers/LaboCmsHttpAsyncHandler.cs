namespace Labo.Cms.Core.HttpHandlers
{
    using System;
    using System.Web;
    using System.Web.Routing;

    internal sealed class LaboCmsHttpAsyncHandler : LaboCmsHttpHandler, IHttpAsyncHandler
    {
        private readonly IHttpAsyncHandler m_HttpAsyncHandler;
        private IDisposable m_Scope;

        public LaboCmsHttpAsyncHandler(RequestContext requestContext, IHttpAsyncHandler httpAsyncHandler, IPageContextScopeManager pageContextScopeManager)
            : base(requestContext, httpAsyncHandler, pageContextScopeManager)
        {
            m_HttpAsyncHandler = httpAsyncHandler;
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            try
            {
                m_Scope = PageContextScopeManager.CreatePageContextScope(RequestContext);

                return m_HttpAsyncHandler.BeginProcessRequest(context, cb, extraData);
            }
            catch
            {
                if (m_Scope != null)
                {
                    m_Scope.Dispose();
                }

                throw;
            }
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            try
            {
                m_HttpAsyncHandler.EndProcessRequest(result);
            }
            finally
            {
                if (m_Scope != null)
                {
                    m_Scope.Dispose();
                }
            }
        }
    }
}