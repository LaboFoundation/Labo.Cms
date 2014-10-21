// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICmsApplication.cs" company="Labo">
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
//   Defines the ICmsApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core
{
    using System.Web;

    using Labo.Cms.Core.Routing;
    using Labo.Cms.Core.Services;

    /// <summary>
    /// The cms application interface.
    /// </summary>
    public interface ICmsApplication
    {
        /// <summary>
        /// Gets the route manager.
        /// </summary>
        /// <value>
        /// The route manager.
        /// </value>
        IRouteManager RouteManager { get; }

        /// <summary>
        /// Gets the route provider manager.
        /// </summary>
        /// <value>
        /// The route provider manager.
        /// </value>
        IRouteProviderManager RouteProviderManager { get; }

        /// <summary>
        /// Gets the CMS service.
        /// </summary>
        /// <value>
        /// The CMS service.
        /// </value>
        ICmsService CmsService { get; }

        /// <summary>
        /// Initializes the cms application.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        /// <param name="application">The application.</param>
        void OnApplicationStart(HttpApplication application);

        /// <summary>
        /// Called when the application stops.
        /// </summary>
        /// <param name="application">The application.</param>
        void OnApplicationEnd(HttpApplication application);

        /// <summary>
        /// Called when the application throws unhandled error.
        /// </summary>
        /// <param name="application">The application.</param>
        void OnApplicationError(HttpApplication application);

        /// <summary>
        /// Called when the application begins an http request.
        /// </summary>
        /// <param name="application">The host application.</param>
        void OnBeginRequest(HttpApplication application);

        /// <summary>
        /// Called when the application ends an http request.
        /// </summary>
        /// <param name="application">The application.</param>
        void OnEndRequest(HttpApplication application);

        /// <summary>
        /// Called when the application authenticates an http request.
        /// </summary>
        /// <param name="application">The application.</param>
        void OnAuthenticateRequest(HttpApplication application);

        /// <summary>
        /// Method to restarts the host application domain.
        /// </summary>
        void RestartApplicationHost();
    }
}