namespace Labo.Cms.Core.Mvc
{
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;

    using Labo.Common.Exceptions;

    public static class ModuleExecutor
    {
        private static readonly LaboCmsModuleControllerActionInvoker s_ActionInvoker = new LaboCmsModuleControllerActionInvoker();
        
        public static ActionResult InvokeAction(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            string controllerName = controllerContext.RouteData.GetRequiredString("controller");
            string actionName = controllerContext.RouteData.GetRequiredString("action");
            string moduleName = controllerContext.RouteData.GetRequiredString("module");

            IControllerFactory controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = null;

            try
            {
                controller = controllerFactory.CreateController(controllerContext.RequestContext, controllerName);
                
                if (controller == null)
                {
                    throw new CoreLevelException(string.Format(CultureInfo.CurrentCulture, "The module '{0}' controller for path does not found or does not implement IController.", moduleName));
                }

                ActionResult result = s_ActionInvoker.InvokeActionResult(controllerContext, actionName);
                if (result == null)
                {
                    HandleUnknownAction(controller, actionName);
                }

                return result;
            }
            finally 
            {
               controllerFactory.ReleaseController(controller);
            }
        }

        private static void HandleUnknownAction(IController controller, string actionName)
        {
            throw new HttpException(404, string.Format(CultureInfo.CurrentCulture, "Controller_UnknownAction {0} {1}", actionName, controller.GetType().FullName));
        }
    }
}