// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboMvcViewResult.cs" company="Labo">
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
//   Defines the LaboMvcViewResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc
{
    using System.Globalization;
    using System.Web.Mvc;

    using Labo.Cms.Core.Models;

    /// <summary>
    /// The labo mvc view result class.
    /// </summary>
    internal sealed class LaboMvcViewResult : ViewResult
    {
        /// <summary>
        /// The view engine
        /// </summary>
        private readonly IViewEngine m_ViewEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboMvcViewResult"/> class.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="screen">The screen model.</param>
        /// <param name="templateName">Name of the template.</param>
        public LaboMvcViewResult(ControllerContext controllerContext, Page screen, string templateName)
        {
            View = new RazorView(controllerContext, string.Format(CultureInfo.InvariantCulture, "~/Views/Layouts/{0}.cshtml", templateName), string.Empty, false, null);
            ViewData.Model = screen;
            m_ViewEngine = new LaboRazorViewEngine();
        }

        /// <summary>
        /// Searches the registered view engines and returns the object that is used to render the view.
        /// </summary>
        /// <returns>
        /// The object that is used to render the view.
        /// </returns>
        /// <param name="context">The controller context.</param><exception cref="T:System.InvalidOperationException">An error occurred while the method was searching for the view.</exception>
        protected override ViewEngineResult FindView(ControllerContext context)
        {
            return new ViewEngineResult(View, m_ViewEngine);
        }
    }
}
