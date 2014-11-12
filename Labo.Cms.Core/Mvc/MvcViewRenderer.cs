// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MvcViewRenderer.cs" company="Labo">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 Bora Akgun
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of
//   this software and associated documentation files (the "Software"), to deal in
//   the Software without restriction, including without limitation the rights to
//   use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//   the Software, and to permit persons to whom the Software is furnished to do so,
//   subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//   FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//   COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//   CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the MvcViewRenderer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Globalization;
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

        public string Render(View view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture))
            {
                string viewName = view.Name;

                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(m_ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(m_ControllerContext, viewResult.View, new ViewDataDictionary(view), m_TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}