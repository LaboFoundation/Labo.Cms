// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseModuleRegistration.cs" company="Labo">
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
//   Defines the BaseModuleRegistration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Module
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Web.Mvc;

    using Labo.Cms.Core.Routing;
    using Labo.Common.Ioc;
    using Labo.Common.Utils;

    public abstract class BaseModuleRegistration : IModuleRegistration
    {
        public abstract string ModuleName { get; }

        protected abstract Assembly ControllerAssembly { get; }

        public void RegisterModule(IRouteRegistrar routeRegistrar, IIocContainerRegistrar iocContainerRegistrar)
        {
            RegisterControllers(iocContainerRegistrar);

            RegisterRoutes(new ModuleRouteRegistrationContext(ModuleName, routeRegistrar));
        }

        private void RegisterControllers(IIocContainerRegistrar iocContainerRegistrar)
        {
            Utils.AssemblyUtils.FindClassesOfType(typeof(IController), new[] { ControllerAssembly })
                .ForEach(x => iocContainerRegistrar.RegisterInstanceNamed(typeof(IController), x, string.Format(CultureInfo.InvariantCulture, "{0}-{1}", ModuleName, GetControllerName(x.Name))));
        }

        private static string GetControllerName(string controllerTypeName)
        {
            return controllerTypeName.Length > 10 ? string.Concat(StringUtils.Left(controllerTypeName, controllerTypeName.Length - 10), StringUtils.Right(controllerTypeName, 10).Replace("Controller", string.Empty)) : controllerTypeName;
        }

        protected abstract void RegisterRoutes(ModuleRouteRegistrationContext context);
    }
}