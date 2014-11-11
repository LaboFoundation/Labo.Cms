namespace Labo.Cms.Modules.Root
{
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.Module;
    using Labo.Cms.Modules.Root.Controllers;

    public sealed class RootModuleRegistration : BaseModuleRegistration
    {
        public override string ModuleName
        {
            get { return "Root"; }
        }

        protected override Assembly ControllerAssembly
        {
            get { return typeof(ContentController).Assembly; }
        }

        protected override void RegisterRoutes(ModuleRouteRegistrationContext context)
        {
            context.RegisterRoute(
                "ContactUs",
                "ContactUs",
                new RouteValueDictionary { { "action", "Index" }, { "controller", "ContactUs" } },
                null,
                1000,
                SessionStateBehavior.Default,
                typeof(ContactUsController),
                new MvcRouteHandler());
        }
    }
}
