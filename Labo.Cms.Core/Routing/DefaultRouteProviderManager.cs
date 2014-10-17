// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRouteProviderManager.cs" company="Labo">
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
//   Defines the DefaultRouteProviderManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Routing
{
    using System.Collections.Generic;

    /// <summary>
    /// The default route provider manager class. Manager class to register routes using route provider implementations.
    /// </summary>
    public sealed class DefaultRouteProviderManager : IRouteProviderManager
    {
        /// <summary>
        /// The route providers collection.
        /// </summary>
        private readonly IList<IRouteProvider> m_RouteProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRouteProviderManager"/> class.
        /// </summary>
        public DefaultRouteProviderManager()
        {
            m_RouteProviders = new List<IRouteProvider>();
        }

        /// <summary>
        /// Registers the route provider.
        /// </summary>
        /// <param name="routeProvider">The route provider.</param>
        public void RegisterRouteProvider(IRouteProvider routeProvider)
        {
            m_RouteProviders.Add(routeProvider);
        }

        /// <summary>
        /// Registers the routes using route manager.
        /// </summary>
        /// <param name="routeRegistrar">The route registrar.</param>
        public void RegisterRoutes(IRouteRegistrar routeRegistrar)
        {
            for (int i = 0; i < m_RouteProviders.Count; i++)
            {
                IRouteProvider routeProvider = m_RouteProviders[i];
                routeProvider.RegisterRoutes(routeRegistrar);
            }
        }
    }
}