﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CmsApplication.cs" company="Labo">
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
//   Defines the CmsApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core
{
    using System;

    using Labo.Cms.Core.Routing;
    using Labo.Cms.Core.Services;
    using Labo.Cms.Core.Utils;
    using Labo.Common.Reflection;

    /// <summary>
    /// The cms application class.
    /// </summary>
    public sealed class CmsApplication : ICmsApplication
    {
        /// <summary>
        /// The CMS service
        /// </summary>
        private readonly ICmsService m_CmsService;

        /// <summary>
        /// The route manager
        /// </summary>
        private readonly IRouteManager m_RouteManager;

        /// <summary>
        /// The route provider manager
        /// </summary>
        private readonly IRouteProviderManager m_RouteProviderManager;

        /// <summary>
        /// Gets the route manager.
        /// </summary>
        /// <value>
        /// The route manager.
        /// </value>
        public IRouteManager RouteManager
        {
            get
            {
                return m_RouteManager;
            }
        }

        /// <summary>
        /// Gets the route provider manager.
        /// </summary>
        /// <value>
        /// The route provider manager.
        /// </value>
        public IRouteProviderManager RouteProviderManager
        {
            get
            {
                return m_RouteProviderManager;
            }
        }

        /// <summary>
        /// Gets the CMS service.
        /// </summary>
        /// <value>
        /// The CMS service.
        /// </value>
        public ICmsService CmsService
        {
            get
            {
                return m_CmsService;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CmsApplication"/> class.
        /// </summary>
        /// <param name="cmsService">The CMS service.</param>
        /// <param name="routeManager">The route manager.</param>
        /// <param name="routeProviderManager">The route provider manager.</param>
        public CmsApplication(ICmsService cmsService, IRouteManager routeManager, IRouteProviderManager routeProviderManager)
        {
            m_CmsService = cmsService;
            m_RouteManager = routeManager;
            m_RouteProviderManager = routeProviderManager;
        }

        /// <summary>
        /// Initializes the cms application.
        /// </summary>
        public void Initialize()
        {
            RegisterRouteProviders();

            m_RouteManager.InstallRoutes();
        }

        /// <summary>
        /// Registers the route providers.
        /// </summary>
        private void RegisterRouteProviders()
        {
            AssemblyUtils.FindClassesOfType(typeof(IRouteProvider), AppDomain.CurrentDomain.GetAssemblies())
                .ForEach(x => m_RouteProviderManager.RegisterRouteProvider((IRouteProvider)DynamicMethodHelper.EmitConstructorInvoker(x)()));
        }
    }
}
