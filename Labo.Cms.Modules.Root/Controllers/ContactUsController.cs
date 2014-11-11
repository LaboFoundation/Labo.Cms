namespace Labo.Cms.Modules.Root.Controllers
{
    using System.Web.Mvc;

    public class ContactUsController : Controller
    {
        public ActionResult Index()
        {
            return View("Labo.Cms.Modules.Root.Views.ContactUs.Index");
        }
    }
}
