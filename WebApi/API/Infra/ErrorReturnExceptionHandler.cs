using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace AI.Infra
{
    internal class ErrorReturnExceptionHandler
    {
        public static RequestDelegate ExceptionHandlerDelegate(bool showDetailedError)
        {
            return async delegate (HttpContext context)
            {
                IExceptionHandlerPathFeature exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                ErrorReturn errorReturn = new ErrorReturn("An error has occured");
                IntegraException ex = null;
                if (exceptionHandlerPathFeature.Error != null)
                {
                    ex = FindIntegraException(exceptionHandlerPathFeature.Error);
                }

                if (ex != null)
                {
                    statusCode = HttpStatusCode.BadRequest;
                    errorReturn = new ErrorReturn(ex);
                }
                else if (showDetailedError && exceptionHandlerPathFeature != null)
                {
                    GetAllExceptions(errorReturn.ValidationFields, exceptionHandlerPathFeature.Error);
                    errorReturn.ValidationFields = errorReturn.ValidationFields.OrderBy((KeyValuePair<string, List<string>> x) => x.Key).ToDictionary((KeyValuePair<string, List<string>> x) => x.Key, (KeyValuePair<string, List<string>> x) => x.Value);
                }

                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("access-control-expose-headers", "Application-Error");
                string text = JsonConvert.SerializeObject(errorReturn);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(text);
            };
        }

        internal static IntegraException FindIntegraException(Exception e)
        {
            if (e is IntegraException)
            {
                return e as IntegraException;
            }

            if (e is AggregateException)
            {
                foreach (Exception innerException in (e as AggregateException).InnerExceptions)
                {
                    IntegraException ex = FindIntegraException(innerException);
                    if (ex != null)
                    {
                        return ex;
                    }
                }
            }

            return null;
        }

        internal static void GetAllExceptions(Dictionary<string, List<string>> dictionary, Exception error, int level = 0)
        {
            dictionary.Add($"Message - {level}", new List<string> { error.Message });
            DbUpdateException ex = error as DbUpdateException;
            if (ex != null)
            {
                dictionary.Add($"Entries - {level}", ex.Entries.Select((EntityEntry x) => x.ToString()).ToList());
            }

            dictionary.Add($"Stacktrace - {level}", new List<string> { error.StackTrace });
            if (error.InnerException != null)
            {
                GetAllExceptions(dictionary, error.InnerException, ++level);
            }
        }
    }
}