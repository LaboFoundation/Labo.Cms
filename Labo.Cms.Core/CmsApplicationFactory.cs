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

    public static class CmsApplicationFactory
    {
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

            FindClassesOfType(typeof(IController), new[] { Assembly.GetExecutingAssembly() }.Union(controllerAssemblies))
                .ForEach(x => iocContainer.RegisterInstanceNamed(typeof(IController), x, GetControllerName(x.Name)));

            if (registrations != null)
            {
                registrations(iocContainer);
            }

            ControllerBuilder.Current.SetControllerFactory(new LaboCmsControllerFactory(iocContainer));

            return iocContainer.GetInstance<ICmsApplication>();
        }

        private static string GetControllerName(string controllerTypeName)
        {
            return controllerTypeName.Length > 10 ? string.Concat(StringUtils.Left(controllerTypeName, controllerTypeName.Length - 10), StringUtils.Right(controllerTypeName, 10).Replace("Controller", string.Empty)) : controllerTypeName;
        }

        private static IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            List<Type> result = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    Type type = types[i];
                    if (assignTypeFrom.IsAssignableFrom(type) || (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(type, assignTypeFrom)))
                    {
                        if (!type.IsInterface)
                        {
                            if (onlyConcreteClasses)
                            {
                                if (type.IsClass && !type.IsAbstract)
                                {
                                    result.Add(type);
                                }
                            }
                            else
                            {
                                result.Add(type);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            Type genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            Type[] implementedInterfaces = type.FindInterfaces((objType, objCriteria) => true, null);
            for (int i = 0; i < implementedInterfaces.Length; i++)
            {
                Type implementedInterface = implementedInterfaces[i];
                if (!implementedInterface.IsGenericType)
                {
                    continue;
                }

                return genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }

            return false;
        }
    }
}