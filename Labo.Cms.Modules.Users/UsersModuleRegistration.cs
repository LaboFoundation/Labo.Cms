namespace Labo.Cms.Modules.Users
{
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core.Module;
    using Labo.Cms.Modules.Users.Controllers;

    public sealed class RootModuleRegistration : BaseModuleRegistration
    {
        public override string ModuleName
        {
            get { return "Users"; }
        }

        protected override Assembly ControllerAssembly
        {
            get { return typeof(UsersController).Assembly; }
        }

        protected override void RegisterRoutes(ModuleRouteRegistrationContext context)
        {
            context.RegisterRoute(
                "Login",
                "Login",
                new RouteValueDictionary { { "action", "Login" }, { "controller", "Users" } },
                null,
                1000,
                SessionStateBehavior.Default,
                typeof(UsersController),
                new MvcRouteHandler());

            context.RegisterRoute(
               "Logout",
               "Logout",
               new RouteValueDictionary { { "action", "Logout" }, { "controller", "Users" } },
               null,
               1000,
               SessionStateBehavior.Default,
               typeof(UsersController),
               new MvcRouteHandler());
        }
    }
}
