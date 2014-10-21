namespace Labo.Cms.Mvc.UI
{
    using System;
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
        /// <summary>
        /// The CMS application
        /// </summary>
        private static ICmsApplication s_CmsApplication;

        /// <summary>
        /// Handles the ApplicationStart event of the Application.
        /// </summary>
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

        /// <summary>
        /// Handles the ApplicationBeginRequest event of the Application.
        /// </summary>
        protected void Application_BeginRequest()
        {
            s_CmsApplication.OnBeginRequest(this);
        }

        /// <summary>
        /// Handles the ApplicationEndRequest event of the Application.
        /// </summary>
        protected void Application_EndRequest()
        {
            s_CmsApplication.OnEndRequest(this);
        }

        /// <summary>
        /// Handles the ApplicationError event of the Application.
        /// </summary>
        protected void Application_Error()
        {
            s_CmsApplication.OnApplicationError(this);
        }

        /// <summary>
        /// Handles the ApplicationEnd event of the Application.
        /// </summary>
        protected void Application_End()
        {
            s_CmsApplication.OnApplicationEnd(this);
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            s_CmsApplication.OnAuthenticateRequest(this);
        }
    }
}