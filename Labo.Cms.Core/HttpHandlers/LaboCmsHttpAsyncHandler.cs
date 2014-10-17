// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboCmsHttpAsyncHandler.cs" company="Labo">
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
//   Defines the LaboCmsHttpAsyncHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.HttpHandlers
{
    using System;
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// The labo cms async handler class.
    /// </summary>
    internal sealed class LaboCmsHttpAsyncHandler : LaboCmsHttpHandler, IHttpAsyncHandler
    {
        /// <summary>
        /// The HTTP async handler
        /// </summary>
        private readonly IHttpAsyncHandler m_HttpAsyncHandler;
        
        /// <summary>
        /// The page context scope
        /// </summary>
        private IDisposable m_Scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsHttpAsyncHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="httpAsyncHandler">The HTTP async handler.</param>
        /// <param name="pageContextScopeManager">The page context scope manager.</param>
        public LaboCmsHttpAsyncHandler(RequestContext requestContext, IHttpAsyncHandler httpAsyncHandler, IPageContextScopeManager pageContextScopeManager)
            : base(requestContext, httpAsyncHandler, pageContextScopeManager)
        {
            m_HttpAsyncHandler = httpAsyncHandler;
        }

        /// <summary>
        /// Initiates an asynchronous call to the HTTP handler.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult"/> that contains information about the status of the process.
        /// </returns>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param><param name="cb">The <see cref="T:System.AsyncCallback"/> to call when the asynchronous method call is complete. If <paramref name="cb"/> is null, the delegate is not called. </param><param name="extraData">Any extra data needed to process the request. </param>
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            try
            {
                m_Scope = PageContextScopeManager.CreatePageContextScope(RequestContext);

                return m_HttpAsyncHandler.BeginProcessRequest(context, cb, extraData);
            }
            catch
            {
                if (m_Scope != null)
                {
                    m_Scope.Dispose();
                }

                throw;
            }
        }

        /// <summary>
        /// Provides an asynchronous process End method when the process ends.
        /// </summary>
        /// <param name="result">An <see cref="T:System.IAsyncResult"/> that contains information about the status of the process. </param>
        public void EndProcessRequest(IAsyncResult result)
        {
            try
            {
                m_HttpAsyncHandler.EndProcessRequest(result);
            }
            finally
            {
                if (m_Scope != null)
                {
                    m_Scope.Dispose();
                }
            }
        }
    }
}