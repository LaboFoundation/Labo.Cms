namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Web.Mvc;

    using Labo.Cms.Core.Models;

    public static class WebViewPageExtensions
    {
        public static void RenderPane(this WebViewPage<Page> page, string paneName)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            if (paneName == null)
            {
                throw new ArgumentNullException("paneName");
            }

            PaneRenderer.Render(page, page.ViewContext.Controller.ControllerContext, page.ViewData, page.TempData, page.Model.Layout.Panes, paneName);
        }
    }
}
