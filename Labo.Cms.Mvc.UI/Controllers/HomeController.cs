namespace Labo.Cms.Mvc.UI.Controllers
{
    using System.Web.Mvc;

    using Labo.Cms.Core;

    public class HomeController : Controller
    {
        private readonly IPageContext m_PageContext;

        public HomeController(IPageContext pageContext)
        {
            m_PageContext = pageContext;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
