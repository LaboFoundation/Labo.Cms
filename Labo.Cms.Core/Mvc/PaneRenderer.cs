namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.WebPages;

    using Labo.Cms.Core.Models;

    public static class PaneRenderer
    {
        public static void Render(WebPageBase page, ControllerContext controllerContext, ViewDataDictionary viewData, TempDataDictionary tempData, IList<Pane> panes, string paneName)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            if (panes == null)
            {
                throw new ArgumentNullException("panes");
            }

            Pane pane = panes.SingleOrDefault(x => x.Name == paneName);
            IList<View> views = pane.Views;
            for (int i = 0; i < views.Count; i++)
            {
                View view = views[i];

                page.WriteLiteral(view.Container != null
                           ? ContainerViewRenderer.Render(controllerContext, view)
                           : new MvcViewRenderer(controllerContext, viewData, tempData).Render(view));
            }
        }
    }
}