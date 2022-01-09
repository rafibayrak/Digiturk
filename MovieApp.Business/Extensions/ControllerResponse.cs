using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Business.Extensions
{
    public static class ControllerResponse
    {
        public static IActionResult NotFoundOrOk<T>(this ControllerBase controllerBase, T responseValue, string notFoundMessage = "")
        {
            if (responseValue == null)
            {
                return controllerBase.NotFound(notFoundMessage);
            }

            return controllerBase.Ok(responseValue);
        }
    }
}
