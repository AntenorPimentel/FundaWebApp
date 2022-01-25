using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace Funda.Controllers
{
    public class APIControllerBase : Controller
    {
        protected readonly ILogger _logger = Log.Logger;

        protected void LogError(Exception ex, string customErrorMessage)
        {
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            _logger.Error(ex, "{CustomErrorMessage}, Controller: {ControllerName}, Action: {ActionName}", customErrorMessage, controllerName, actionName);
        }
    }
}