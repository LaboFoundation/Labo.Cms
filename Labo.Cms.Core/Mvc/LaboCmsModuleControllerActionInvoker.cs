namespace Labo.Cms.Core.Mvc
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public sealed class LaboCmsModuleControllerActionInvoker : ControllerActionInvoker
    {
        public ActionResult InvokeActionResult(ControllerContext controllerContext, string actionName)
        {
            ControllerDescriptor controllerDescriptor = GetControllerDescriptor(controllerContext);
            ActionDescriptor actionDescriptor = FindAction(controllerContext, controllerDescriptor, actionName);
            IDictionary<string, object> parameterValues = GetParameterValues(controllerContext, actionDescriptor);
            return InvokeActionMethod(controllerContext, actionDescriptor, parameterValues);
        }
    }
}