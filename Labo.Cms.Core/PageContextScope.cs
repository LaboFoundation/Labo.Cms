namespace Labo.Cms.Core
{
    using System.Web;

    internal sealed class PageContextScope : IPageContextScope
    {
        private readonly HttpContextBase m_HttpContext;
        private static readonly object s_PageContextItemKey = new object();

        public IPageContext PageContext
        {
            get
            {
                return m_HttpContext.Items[s_PageContextItemKey] as IPageContext;
            }
        }

        public static IPageContext CurrentPageContext
        {
            get
            {
                return HttpContext.Current == null ? null : HttpContext.Current.Items[s_PageContextItemKey] as IPageContext;
            }
        }

        public PageContextScope(IPageContext pageContext, HttpContextBase httpContext)
        {
            m_HttpContext = httpContext;
            m_HttpContext.Items[s_PageContextItemKey] = pageContext;
        }

        public void Dispose()
        {
            m_HttpContext.Items.Remove(s_PageContextItemKey);
        }
    }
}