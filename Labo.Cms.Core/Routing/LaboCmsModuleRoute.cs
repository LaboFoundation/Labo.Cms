// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboCmsModuleRoute.cs" company="Labo">
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
//   Defines the LaboCmsModuleRoute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Web.Routing;

    /// <summary>
    /// Labo cms module route class.
    /// </summary>
    public sealed class LaboCmsModuleRoute : Route
    {
        /// <summary>
        /// The module route key.
        /// </summary>
        private const string MODULE_ROUTE_KEY = "module";

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsModuleRoute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="url">The URL.</param>
        /// <param name="routeHandler">The route handler.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public LaboCmsModuleRoute(string moduleName, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            ModuleName = moduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsModuleRoute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="routeHandler">The route handler.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public LaboCmsModuleRoute(string moduleName, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            ModuleName = moduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsModuleRoute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="constraints">The constraints.</param>
        /// <param name="routeHandler">The route handler.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public LaboCmsModuleRoute(string moduleName, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            ModuleName = moduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsModuleRoute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="constraints">The constraints.</param>
        /// <param name="dataTokens">The data tokens.</param>
        /// <param name="routeHandler">The route handler.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public LaboCmsModuleRoute(string moduleName, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            ModuleName = moduleName;
        }

        /// <summary>
        /// Returns information about the URL that is associated with the route.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        /// <returns>
        /// An object that contains information about the URL that is associated with the route.
        /// </returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            requestContext.RouteData.Values[MODULE_ROUTE_KEY] = ModuleName;
            return base.GetVirtualPath(requestContext, values);
        }
    }
}