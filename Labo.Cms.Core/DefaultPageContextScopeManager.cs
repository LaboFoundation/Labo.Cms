// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultPageContextScopeManager.cs" company="Labo">
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
//   Defines the DefaultPageContextScopeManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core
{
    using System;
    using System.Globalization;
    using System.Web.Routing;

    using Labo.Cms.Core.Models;
    using Labo.Cms.Core.Services;

    /// <summary>
    /// The default page context scope manager.
    /// </summary>
    public sealed class DefaultPageContextScopeManager : IPageContextScopeManager
    {
        /// <summary>
        /// The CMS service
        /// </summary>
        private readonly ICmsService m_CmsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPageContextScopeManager"/> class.
        /// </summary>
        /// <param name="cmsService">The CMS service.</param>
        public DefaultPageContextScopeManager(ICmsService cmsService)
        {
            m_CmsService = cmsService;
        }

        /// <summary>
        /// Creates new page context scope.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The page context scope.</returns>
        public IPageContextScope CreatePageContextScope(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            string url = Convert.ToString(requestContext.RouteData.Values["PageUrl"], CultureInfo.CurrentCulture);
            Page page = m_CmsService.GetPageBySlug(url);

            return new PageContextScope(new PageContext(page), requestContext.HttpContext);
        }
    }
}