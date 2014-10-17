// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboCmsRouteHandler.cs" company="Labo">
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
//   Defines the LaboCmsRouteHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.HttpHandlers;

    /// <summary>
    /// The labo cms route handler class.
    /// </summary>
    internal sealed class LaboCmsRouteHandler : IRouteHandler
    {
        /// <summary>
        /// Gets the route handler
        /// </summary>
        internal IRouteHandler RouteHandler
        {
            get
            {
                return m_RouteHandler;
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
        /// The route handler
        /// </summary>
        private readonly IRouteHandler m_RouteHandler;

        /// <summary>
        /// The page context scope manager
        /// </summary>
        private readonly IPageContextScopeManager m_PageContextScopeManager;

        /// <summary>
        /// The session state behavior
        /// </summary>
        private readonly SessionStateBehavior m_SessionStateBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsRouteHandler"/> class.
        /// </summary>
        /// <param name="routeHandler">The route handler.</param>
        /// <param name="pageContextScopeManager">The page context scope manager.</param>
        /// <param name="sessionStateBehavior">The session state behavior.</param>
        public LaboCmsRouteHandler(IRouteHandler routeHandler, IPageContextScopeManager pageContextScopeManager, SessionStateBehavior sessionStateBehavior)
        {
            m_RouteHandler = routeHandler;
            m_PageContextScopeManager = pageContextScopeManager;
            m_SessionStateBehavior = sessionStateBehavior;
        }

        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <returns>
        /// An object that processes the request.
        /// </returns>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            IHttpHandler httpHandler = m_RouteHandler.GetHttpHandler(requestContext);
            requestContext.HttpContext.SetSessionStateBehavior(SessionStateBehavior);

            IHttpAsyncHandler asyncHttpHandler = httpHandler as IHttpAsyncHandler;
            return asyncHttpHandler == null
                       ? new LaboCmsHttpHandler(requestContext, httpHandler, PageContextScopeManager)
                       : new LaboCmsHttpAsyncHandler(requestContext, asyncHttpHandler, PageContextScopeManager);
        }
    }
}