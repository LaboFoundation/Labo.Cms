// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleRouteRegistrationContext.cs" company="Labo">
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
//   Defines the ModuleRouteRegistrationContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.Cms.Core.Module
{
    using System;
    using System.Globalization;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.Routing;

    public sealed class ModuleRouteRegistrationContext
    {
        private readonly string m_ModuleName;

        private readonly IRouteRegistrar m_RouteRegistrar;

        public ModuleRouteRegistrationContext(string moduleName, IRouteRegistrar routeRegistrar)
        {
            m_ModuleName = moduleName;
            m_RouteRegistrar = routeRegistrar;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public void RegisterRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, int priority, SessionStateBehavior sessionState, Type controllerType, IRouteHandler routeHandler)
        {
            if (controllerType == null)
            {
                throw new ArgumentNullException("controllerType");
            }

            if (defaults == null)
            {
                defaults = new RouteValueDictionary();
            }

            defaults["module"] = m_ModuleName;

            string routeName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", m_ModuleName, name);

            m_RouteRegistrar.RegisterRoute(
                new RouteInfo
                    {
                        Name = routeName,
                        Priority = priority,
                        Route =
                            new LaboCmsModuleRoute(
                            m_ModuleName,
                            url,
                            defaults,
                            constraints,
                            new RouteValueDictionary { { "Namespaces", controllerType.Namespace } },
                            routeHandler),
                        SessionState = sessionState
                    });
        }
    }
}