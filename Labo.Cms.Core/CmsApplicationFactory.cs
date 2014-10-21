// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CmsApplicationFactory.cs" company="Labo">
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
//   Defines the CmsApplicationFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Labo.Cms.Core.Mvc;
    using Labo.Cms.Core.Routing;
    using Labo.Cms.Core.Services;
    using Labo.Common.Ioc;
    using Labo.Common.Utils;

    /// <summary>
    /// The cms application factory class.
    /// </summary>
    public static class CmsApplicationFactory
    {
        /// <summary>
        /// Creates the application.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="registrations">The registrations.</param>
        /// <param name="controllerAssemblies">The controller assemblies.</param>
        /// <returns>The cms application instance.</returns>
        public static ICmsApplication CreateApplication(RouteCollection routes, Action<IIocContainerRegistrar> registrations = null, params Assembly[] controllerAssemblies)
        {
            IIocContainer iocContainer = new Common.Ioc.Container.IocContainer();

            iocContainer.RegisterSingleInstance<ICmsService, CmsService>();
            iocContainer.RegisterSingleInstance<IRouteManager, DefaultRouteManager>();
            iocContainer.RegisterSingleInstance<IRouteProviderManager, DefaultRouteProviderManager>();
            iocContainer.RegisterSingleInstance<IPageContextScopeManager, DefaultPageContextScopeManager>();
            iocContainer.RegisterSingleInstance<ICmsApplication, CmsApplication>();
            iocContainer.RegisterSingleInstance(x => routes);
            iocContainer.RegisterSingleInstance(x => PageContextScope.CurrentPageContext);

            RegisterControllers(controllerAssemblies, iocContainer);

            if (registrations != null)
            {
                registrations(iocContainer);
            }

            ControllerBuilder.Current.SetControllerFactory(new LaboCmsControllerFactory(iocContainer));

            return iocContainer.GetInstance<ICmsApplication>();
        }

        /// <summary>
        /// Registers the controllers.
        /// </summary>
        /// <param name="controllerAssemblies">The controller assemblies.</param>
        /// <param name="iocContainerRegistrar">The ioc container registrar.</param>
        private static void RegisterControllers(IEnumerable<Assembly> controllerAssemblies, IIocContainerRegistrar iocContainerRegistrar)
        {
            Utils.AssemblyUtils.FindClassesOfType(
                typeof(IController),
                new[] { Assembly.GetExecutingAssembly() }.Union(controllerAssemblies))
                .ForEach(x => iocContainerRegistrar.RegisterInstanceNamed(typeof(IController), x, GetControllerName(x.Name)));
        }

        /// <summary>
        /// Gets the name of the controller by removing the "Controller" word.
        /// </summary>
        /// <param name="controllerTypeName">Name of the controller type.</param>
        /// <returns>The controller name.</returns>
        private static string GetControllerName(string controllerTypeName)
        {
            return controllerTypeName.Length > 10 ? string.Concat(StringUtils.Left(controllerTypeName, controllerTypeName.Length - 10), StringUtils.Right(controllerTypeName, 10).Replace("Controller", string.Empty)) : controllerTypeName;
        }
    }
}