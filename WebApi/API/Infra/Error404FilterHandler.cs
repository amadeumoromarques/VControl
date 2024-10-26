using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AI.Infra
{
    internal class Error404FilterHandler : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is ODataController && context.Result is StatusCodeResult && (context.Result as StatusCodeResult).StatusCode == 404)
            {
                context.Result = new JsonResult(null)
                {
                    StatusCode = 204
                };
            }
            else
            {
                base.OnResultExecuting(context);
            }
        }
    }
}