// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboCmsRoute.cs" company="Labo">
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
//   Defines the LaboCmsRoute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using System.Web.SessionState;

    /// <summary>
    /// The labo cms route class.
    /// </summary>
    public sealed class LaboCmsRoute : RouteBase
    {
        /// <summary>
        /// Gets the route base instance
        /// </summary>
        internal RouteBase Route
        {
            get
            {
                return m_Route;
            }
        }

        /// <summary>
        /// Gets the page context scope manager
        /// </summary>
        internal IPageContextScopeManager PageContextScopeManager
        {
            get
            {
                return m_PageContextScopeManager;
            }
        }

        /// <summary>
        /// Gets the session state behavior
        /// </summary>
        internal SessionStateBehavior SessionStateBehavior
        {
            get
            {
                return m_SessionStateBehavior;
            }
        }

        /// <summary>
        /// The route base instance
        /// </summary>
        private readonly RouteBase m_Route;

        /// <summary>
        /// The page context scope manager
        /// </summary>
        private readonly IPageContextScopeManager m_PageContextScopeManager;

        /// <summary>
        /// The session state behavior
        /// </summary>
        private readonly SessionStateBehavior m_SessionStateBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsRoute"/> class.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="pageContextScopeManager">The page context scope manager.</param>
        /// <param name="sessionStateBehavior">The session state behavior.</param>
        public LaboCmsRoute(RouteBase route, IPageContextScopeManager pageContextScopeManager, SessionStateBehavior sessionStateBehavior)
        {
            m_Route = route;
            m_PageContextScopeManager = pageContextScopeManager;
            m_SessionStateBehavior = sessionStateBehavior;
        }

        /// <summary>
        /// When overridden in a derived class, returns route information about the request.
        /// </summary>
        /// <returns>
        /// An object that contains the values from the route definition if the route matches the current request, or null if the route does not match the request.
        /// </returns>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            RouteData routeData = m_Route.GetRouteData(httpContext);
            if (routeData == null)
            {
                return null;
            }

            httpContext.SetSessionStateBehavior(m_SessionStateBehavior);

            routeData.RouteHandler = new LaboCmsRouteHandler(routeData.RouteHandler, m_PageContextScopeManager, SessionStateBehavior);

            return routeData;
        }

        /// <summary>
        /// When overridden in a derived class, checks whether the route matches the specified values, and if so, generates a URL and retrieves information about the route.
        /// </summary>
        /// <returns>
        /// An object that contains the generated URL and information about the route, or null if the route does not match <paramref name="values"/>.
        /// </returns>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param><param name="values">An object that contains the parameters for a route.</param>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            RequestContext effectiveRequestContext = requestContext;
            VirtualPathData virtualPath = m_Route.GetVirtualPath(effectiveRequestContext, values);
            return virtualPath;
        }
    }
}