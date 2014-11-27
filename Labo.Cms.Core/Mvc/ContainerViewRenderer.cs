namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web.Mvc;

    using Labo.Cms.Core.Models;

    public static class ContainerViewRenderer
    {
        public static string Render(ControllerContext controllerContext, View view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            Container container = view.Container;
            if (container == null)
            {
                throw new NullReferenceException("view.Container");
            }

            string viewName = container.Name;

            using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture))
            {
                RazorView razorView = new RazorView(controllerContext, string.Format(CultureInfo.InvariantCulture, "~/Views/Containers/{0}.cshtml", viewName), string.Empty, false, null);
                ViewContext viewContext = new ViewContext(controllerContext, razorView, new ViewDataDictionary(view), new TempDataDictionary(), sw);
                razorView.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}