// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PageContextScope.cs" company="Labo">
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
//   Defines the PageContextScope type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core
{
    using System.Web;

    /// <summary>
    /// The page context scope class.
    /// </summary>
    internal sealed class PageContextScope : IPageContextScope
    {
        /// <summary>
        /// The page context item key
        /// </summary>
        private static readonly object s_PageContextItemKey = new object();

        /// <summary>
        /// The HTTP context
        /// </summary>
        private readonly HttpContextBase m_HttpContext;

        /// <summary>
        /// Gets the current page context.
        /// </summary>
        /// <value>
        /// The current page context.
        /// </value>
        public static IPageContext CurrentPageContext
        {
            get
            {
                return HttpContext.Current == null ? null : HttpContext.Current.Items[s_PageContextItemKey] as IPageContext;
            }
        }

        /// <summary>
        /// Gets the page context.
        /// </summary>
        /// <value>
        /// The page context.
        /// </value>
        public IPageContext PageContext
        {
            get
            {
                return m_HttpContext.Items[s_PageContextItemKey] as IPageContext;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageContextScope"/> class.
        /// </summary>
        /// <param name="pageContext">The page context.</param>
        /// <param name="httpContext">The HTTP context.</param>
        public PageContextScope(IPageContext pageContext, HttpContextBase httpContext)
        {
            m_HttpContext = httpContext;
            m_HttpContext.Items[s_PageContextItemKey] = pageContext;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            m_HttpContext.Items.Remove(s_PageContextItemKey);
        }
    }
}