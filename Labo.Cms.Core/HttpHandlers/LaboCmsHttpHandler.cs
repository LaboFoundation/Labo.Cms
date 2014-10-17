// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboCmsHttpHandler.cs" company="Labo">
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
//   Defines the LaboCmsHttpHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.HttpHandlers
{
    using System.Web;
    using System.Web.Routing;
    using System.Web.SessionState;

    /// <summary>
    /// The labo cms http handler.
    /// </summary>
    internal class LaboCmsHttpHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// The request context
        /// </summary>
        protected readonly RequestContext RequestContext;
        
        /// <summary>
        /// The page context scope manager
        /// </summary>
        protected readonly IPageContextScopeManager PageContextScopeManager;

        /// <summary>
        /// The HTTP handler
        /// </summary>
        private readonly IHttpHandler m_HttpHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="httpHandler">The HTTP handler.</param>
        /// <param name="pageContextScopeManager">The page context scope manager.</param>
        public LaboCmsHttpHandler(RequestContext requestContext, IHttpHandler httpHandler, IPageContextScopeManager pageContextScopeManager)
        {
            RequestContext = requestContext;
            PageContextScopeManager = pageContextScopeManager;
            m_HttpHandler = httpHandler;
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
        public void ProcessRequest(HttpContext context)
        {
            using (PageContextScopeManager.CreatePageContextScope(RequestContext))
            {
                m_HttpHandler.ProcessRequest(context);
            }
        }
    }
}