// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleExecutor.cs" company="Labo">
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
//   Defines the ModuleExecutor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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