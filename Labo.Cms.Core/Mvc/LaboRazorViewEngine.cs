// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboRazorViewEngine.cs" company="Labo">
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
//   Defines the LaboRazorViewEngine type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc
{
    using System.Web.Mvc;

    public sealed class LaboRazorViewEngine : BuildManagerViewEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LaboRazorViewEngine"/> class.
        /// </summary>
        public LaboRazorViewEngine()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboRazorViewEngine"/> class.
        /// </summary>
        /// <param name="viewPageActivator">The view page activator.</param>
        public LaboRazorViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            ViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml" };
            MasterLocationFormats = new[] { "~/Views/{1}/{0}.cshtml" };
            PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml" };
            FileExtensions = new[] { "cshtml" };
        }

        /// <summary>
        /// Creates the specified partial view by using the specified controller context.
        /// </summary>
        /// <returns>
        /// A reference to the partial view.
        /// </returns>
        /// <param name="controllerContext">The controller context.</param><param name="partialPath">The partial path for the new partial view.</param>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, FileExtensions, ViewPageActivator);
        }

        /// <summary>
        /// Creates the specified view by using the controller context, path of the view, and path of the master view.
        /// </summary>
        /// <returns>
        /// A reference to the view.
        /// </returns>
        /// <param name="controllerContext">The controller context.</param><param name="viewPath">The path of the view.</param><param name="masterPath">The path of the master view.</param>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, true, FileExtensions, ViewPageActivator);
        }
    }
}