namespace Labo.Cms.Core
{
    using Labo.Cms.Core.Models;

    public sealed class PageContext : IPageContext
    {
        private readonly Page m_Page;

        public Page Page
        {
            get
            {
                return m_Page;
            }
        }

        public PageContext(Page page)
        {
            m_Page = page;
        }
    }
}
