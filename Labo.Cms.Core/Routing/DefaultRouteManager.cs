// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRouteManager.cs" company="Labo">
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
//   Defines the DefaultRouteManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Web.Routing;

    /// <summary>
    /// The default route manager class.
    /// </summary>
    public sealed class DefaultRouteManager : IRouteManager
    {
        /// <summary>
        /// Gets the route info collection
        /// </summary>
        internal RouteInfoCollection RouteInfos
        {
            get
            {
                return m_RouteInfos;
            }
        }

        /// <summary>
        /// The route collection
        /// </summary>
        private readonly RouteCollection m_RouteCollection;

        /// <summary>
        /// The page context scope manager
        /// </summary>
        private readonly IPageContextScopeManager m_PageContextScopeManager;

        /// <summary>
        /// The route info collection
        /// </summary>
        private readonly RouteInfoCollection m_RouteInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRouteManager"/> class.
        /// </summary>
        /// <param name="routeCollection">The route collection.</param>
        /// <param name="pageContextScopeManager">The page context scope manager.</param>
        /// <exception cref="System.ArgumentNullException">
        /// routeCollection
        /// or
        /// pageContextScopeManager
        /// </exception>
        public DefaultRouteManager(RouteCollection routeCollection, IPageContextScopeManager pageContextScopeManager)
        {
            if (routeCollection == null)
            {
                throw new ArgumentNullException("routeCollection");
            }

            if (pageContextScopeManager == null)
            {
                throw new ArgumentNullException("pageContextScopeManager");
            }

            m_RouteCollection = routeCollection;
            m_PageContextScopeManager = pageContextScopeManager;
            m_RouteCollection.AppendTrailingSlash = true;
            m_RouteCollection.LowercaseUrls = true;

            m_RouteInfos = new RouteInfoCollection();
        }

        /// <summary>
        /// Registers the route.
        /// </summary>
        /// <param name="routeInfo">The route info.</param>
        public void RegisterRoute(RouteInfo routeInfo)
        {
            m_RouteInfos.Add(routeInfo);
        }

        /// <summary>
        /// Installs all the routes registered.
        /// </summary>
        public void InstallRoutes()
        {
            m_RouteInfos.SortByPriority();

            using (m_RouteCollection.GetWriteLock())
            {
                for (int i = 0; i < RouteInfos.Count; i++)
                {
                    RouteInfo routeInfo = RouteInfos[i];

                    m_RouteCollection.Add(routeInfo.Name, new LaboCmsRoute(routeInfo.Route, m_PageContextScopeManager, routeInfo.SessionState));
                }
            }
        }
    }
}