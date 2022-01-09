using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Extensions
{
    public static class ControllerResponse
    {
        public static IActionResult NotFoundOrOk<T>(this ControllerBase controllerBase, T responseValue)
        {
            if (responseValue == null)
            {
                return controllerBase.NotFound();
            }

            return controllerBase.Ok(responseValue);
        }
    }
}
