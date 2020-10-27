using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {            
            this.Redirect($"/?{actionName}").ExecuteResult(this.ControllerContext);
        }

    }
}