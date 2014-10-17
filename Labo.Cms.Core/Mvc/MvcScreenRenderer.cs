namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Web.Mvc;

    using Labo.Cms.Core.Models;

    public sealed class MvcScreenRenderer
    {
        private readonly ControllerContext m_ControllerContext;

        public MvcScreenRenderer(ControllerContext controllerContext)
        {
            m_ControllerContext = controllerContext;
        }

        public void Render(Page screen)
        {
            if (screen == null)
            {
                throw new ArgumentNullException("screen");
            }

            Layout layout = screen.Layout;
            string layoutName = layout.Name;

            new LaboMvcViewResult(m_ControllerContext, layoutName).ExecuteResult(m_ControllerContext);
        }
    }
}
