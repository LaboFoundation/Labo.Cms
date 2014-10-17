namespace Labo.Cms.Mvc.UI
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Labo.Cms.Core;
    using Labo.Cms.Core.Routing;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        private static ICmsApplication s_CmsApplication;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            s_CmsApplication = CmsApplicationFactory.CreateApplication(RouteTable.Routes, null, Assembly.GetExecutingAssembly());
            s_CmsApplication.RouteManager.RegisterRoute(
                new RouteInfo
                    {
                        Name = "page",
                        Priority = 1000,
                        SessionState = SessionStateBehavior.Disabled,
                        Route =
                            new Route(
                            "{*PageUrl}",
                            new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } },
                            new MvcRouteHandler())
                    });
            s_CmsApplication.Initialize();
        }
    }
}