namespace Labo.Cms.Mvc.UI.Controllers
{
    using System.Web.Mvc;

    using Labo.Cms.Core;
    using Labo.Cms.Core.Mvc;

    public class PageController : Controller
    {
        private readonly IPageContext m_PageContext;

        public PageController(IPageContext pageContext)
        {
            m_PageContext = pageContext;
        }

        public ActionResult Index()
        {
            MvcScreenRenderer mvcScreenRenderer = new MvcScreenRenderer(ControllerContext);
            return mvcScreenRenderer.GetViewResult(m_PageContext.Page);
        }
    }
}
