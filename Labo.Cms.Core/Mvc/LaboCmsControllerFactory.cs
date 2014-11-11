// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboCmsControllerFactory.cs" company="Labo">
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
//   The labo cms controller factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Labo.Common.Ioc;

    /// <summary>
    /// The labo cms controller factory.
    /// </summary>
    public sealed class LaboCmsControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// The ioc container resolver
        /// </summary>
        private readonly IIocContainerResolver m_IocContainerResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaboCmsControllerFactory"/> class.
        /// </summary>
        /// <param name="iocContainerResolver">The ioc container resolver.</param>
        public LaboCmsControllerFactory(IIocContainerResolver iocContainerResolver)
        {
            m_IocContainerResolver = iocContainerResolver;
        }

        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <returns>
        /// The controller.
        /// </returns>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param><param name="controllerName">The name of the controller.</param><exception cref="T:System.ArgumentNullException">The <paramref name="requestContext"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The <paramref name="controllerName"/> parameter is null or empty.</exception>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            string moduleName = Convert.ToString(requestContext.RouteData.Values["module"], CultureInfo.InvariantCulture);
            IController controller = m_IocContainerResolver.GetInstanceOptionalByName<IController>(string.IsNullOrWhiteSpace(moduleName) ? controllerName : string.Format(CultureInfo.InvariantCulture, "{0}-{1}", moduleName, controllerName));
            return controller ?? base.CreateController(requestContext, controllerName);
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller to release.</param>
        public override void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}