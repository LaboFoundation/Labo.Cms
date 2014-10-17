namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    using Labo.Cms.Core.Models;

    public sealed class MvcViewRenderer
    {
        private readonly ControllerContext m_ControllerContext;
        private readonly ViewDataDictionary m_ViewData;
        private readonly TempDataDictionary m_TempData;

        public MvcViewRenderer(ControllerContext controllerContext, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            m_ControllerContext = controllerContext;
            m_ViewData = viewData;
            m_TempData = tempData;
        }

        public void Render(View view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            string viewName = view.Name;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(m_ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(m_ControllerContext, viewResult.View, m_ViewData, m_TempData, sw);
                viewResult.View.Render(viewContext, sw);

                sw.GetStringBuilder().ToString();
            }
        }
    }
}