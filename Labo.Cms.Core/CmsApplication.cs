// --------------------------------------------------------------------------------------------------------------------
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
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;

    using Labo.Cms.Core.Module;
    using Labo.Cms.Core.Mvc.EmbeddedViews;
    using Labo.Cms.Core.Routing;
    using Labo.Cms.Core.Services;
    using Labo.Cms.Core.Utils;
    using Labo.Common.Ioc;
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

        private readonly IEmbeddedViewResolver m_EmbeddedViewResolver;

        private readonly IIocContainer m_IocContainer;

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
        /// <param name="embeddedViewResolver">The embedded view resolver.</param>
        /// <param name="iocContainer">The ioc container.</param>
        public CmsApplication(ICmsService cmsService, IRouteManager routeManager, IRouteProviderManager routeProviderManager, IEmbeddedViewResolver embeddedViewResolver, IIocContainer iocContainer)
        {
            m_CmsService = cmsService;
            m_RouteManager = routeManager;
            m_RouteProviderManager = routeProviderManager;
            m_EmbeddedViewResolver = embeddedViewResolver;
            m_IocContainer = iocContainer;
        }

        /// <summary>
        /// Initializes the cms application.
        /// </summary>
        public void Initialize()
        {
            RegisterRouteProviders();

            RegisterModules();

            m_RouteManager.InstallRoutes();

            EmbeddedViewVirtualPathProvider embeddedProvider = new EmbeddedViewVirtualPathProvider(m_EmbeddedViewResolver.GetEmbeddedViews());
            HostingEnvironment.RegisterVirtualPathProvider(embeddedProvider);

            MvcHandler.DisableMvcResponseHeader = true;
        }

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        /// <param name="application">The application.</param>
        public void OnApplicationStart(HttpApplication application)
        {
        }

        /// <summary>
        /// Called when the application stops.
        /// </summary>
        /// <param name="application">The application.</param>
        public void OnApplicationEnd(HttpApplication application)
        {
        }

        /// <summary>
        /// Called when the application throws unhandled error.
        /// </summary>
        /// <param name="application">The application.</param>
        public void OnApplicationError(HttpApplication application)
        {
        }

        /// <summary>
        /// Called when the application begins an http request.
        /// </summary>
        /// <param name="application">The host application.</param>
        public void OnBeginRequest(HttpApplication application)
        {
        }

        /// <summary>
        /// Called when the application ends an http request.
        /// </summary>
        /// <param name="application">The application.</param>
        public void OnEndRequest(HttpApplication application)
        {
        }

        /// <summary>
        /// Called when the application authenticates an http request.
        /// </summary>
        /// <param name="application">The application.</param>
        public void OnAuthenticateRequest(HttpApplication application)
        {
        }

        /// <summary>
        /// Method to restarts the host application domain.
        /// </summary>
        public void RestartApplicationHost()
        {
        }

        /// <summary>
        /// Registers the route providers.
        /// </summary>
        private void RegisterRouteProviders()
        {
            AssemblyUtils.FindClassesOfType(typeof(IRouteProvider), AppDomain.CurrentDomain.GetAssemblies())
                .ForEach(x => m_RouteProviderManager.RegisterRouteProvider((IRouteProvider)DynamicMethodHelper.EmitConstructorInvoker(x)()));
        }

        /// <summary>
        /// Registers the modules.
        /// </summary>
        private void RegisterModules()
        {
            AssemblyUtils.FindClassesOfType(typeof(IModuleRegistration), AppDomain.CurrentDomain.GetAssemblies())
                .ForEach(
                    x =>
                    {
                        IModuleRegistration moduleRegistration = (IModuleRegistration)Activator.CreateInstance(x);
                        moduleRegistration.RegisterModule(m_RouteManager, m_IocContainer);
                    });
        }
    }
}
