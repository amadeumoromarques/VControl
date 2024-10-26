using Microsoft.AspNetCore.Mvc;
using NetCore.AutoRegisterDi;

namespace AI.Infra
{
    public static class Extensions
    {
        public static void UseErrorReturnExceptionHandler(this IApplicationBuilder app, bool showDetailedError)
        {
            app.UseExceptionHandler(delegate (IApplicationBuilder builder)
            {
                builder.Run(ErrorReturnExceptionHandler.ExceptionHandlerDelegate(showDetailedError));
            });
        }

        public static IMvcBuilder AddODataErrorReturn(this IMvcBuilder mvcBuilder, bool showDetailedError)
        {
            return mvcBuilder.AddMvcOptions(delegate (MvcOptions options)
            {
                options.Filters.Add(new Error404FilterHandler());
                options.OutputFormatters.Insert(0, new ErrorReturnODataOutputFormatter(showDetailedError));
            });
        }
    }
}