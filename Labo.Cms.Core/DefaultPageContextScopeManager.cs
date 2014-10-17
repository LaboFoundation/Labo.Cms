namespace Labo.Cms.Core
{
    using System;
    using System.Globalization;
    using System.Web.Routing;

    using Labo.Cms.Core.Models;
    using Labo.Cms.Core.Services;

    public sealed class DefaultPageContextScopeManager : IPageContextScopeManager
    {
        private readonly ICmsService m_CmsService;

        public DefaultPageContextScopeManager(ICmsService cmsService)
        {
            m_CmsService = cmsService;
        }

        public IPageContextScope CreatePageContextScope(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            string url = Convert.ToString(requestContext.RouteData.Values["PageUrl"], CultureInfo.CurrentCulture);
            Page page = m_CmsService.GetPageBySlug(url);

            return new PageContextScope(new PageContext(page), requestContext.HttpContext);
        }
    }
}