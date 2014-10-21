namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web.Mvc;

    using Labo.Cms.Core.Models;

    public sealed class ContainerViewRenderer
    {
        private readonly ControllerContext m_ControllerContext;
        private readonly ViewDataDictionary m_ViewData;
        private readonly TempDataDictionary m_TempData;

        public ContainerViewRenderer(ControllerContext controllerContext, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            m_ControllerContext = controllerContext;
            m_ViewData = viewData;
            m_TempData = tempData;
        }

        public string Render(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            string viewName = container.Name;

            using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture))
            {
                RazorView view = new RazorView(m_ControllerContext, string.Format(CultureInfo.InvariantCulture, "~/Views/Containers/{0}.cshtml", viewName), string.Empty, false, null);
                ViewContext viewContext = new ViewContext(m_ControllerContext, view, new ViewDataDictionary(container), m_TempData, sw);
                view.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}