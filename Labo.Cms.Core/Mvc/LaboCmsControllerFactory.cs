namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Labo.Common.Ioc;

    public sealed class LaboCmsControllerFactory : DefaultControllerFactory
    {
        private readonly IIocContainerResolver m_IocContainerResolver;

        public LaboCmsControllerFactory(IIocContainerResolver iocContainerResolver)
        {
            m_IocContainerResolver = iocContainerResolver;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            IController controller = m_IocContainerResolver.GetInstanceOptionalByName<IController>(controllerName);
            return controller ?? base.CreateController(requestContext, controllerName);
        }

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