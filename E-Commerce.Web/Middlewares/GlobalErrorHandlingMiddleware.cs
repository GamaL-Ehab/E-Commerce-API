using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Shared.ErrorModels;

namespace E_Commerce.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == 404) // Routing Middleware
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = $"Endpoint {context.Request.Path} was not found!"
                    };

                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                //1. Set Status Code of Response.
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };

                //2. Set Content Type of Response.
                context.Response.ContentType = "application/json";

                //3. Set Body of Response.
                var response = new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                };

                //Return Response
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
