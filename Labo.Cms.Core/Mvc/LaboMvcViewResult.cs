namespace Labo.Cms.Core.Mvc
{
    using System.Globalization;
    using System.Web.Mvc;

    internal sealed class LaboMvcViewResult : ViewResult
    {
        private readonly IViewEngine m_ViewEngine;
         
        public LaboMvcViewResult(ControllerContext controllerContext, string templateName)
        {
            View = new RazorView(controllerContext, string.Format(CultureInfo.InvariantCulture, "~/Views/Layouts/{0}.cshtml", templateName), string.Empty, false, null);
            m_ViewEngine = new RazorViewEngine();
        }

        protected override ViewEngineResult FindView(ControllerContext context)
        {
            return new ViewEngineResult(View, m_ViewEngine);
        }
    }
}
