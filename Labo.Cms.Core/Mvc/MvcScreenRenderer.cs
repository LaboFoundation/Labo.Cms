// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MvcScreenRenderer.cs" company="Labo">
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
//   Defines the MvcScreenRenderer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
