using BaseWebApplication.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BaseWebApplication.Configurations.ExceptionsHandler
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        //private readonly IEmailService _emailService;
        private readonly IEmailSender _emailSender;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env, IEmailSender emailSender)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env;
            _emailSender = emailSender;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new { message = ex.Message };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task<Task> HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var statusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            if (statusCode == (int)HttpStatusCode.InternalServerError)
            {
                var user = context.User.Identity.IsAuthenticated ? context.User.Identity.Name.ToString() : "";
                var url = context.Request.GetDisplayUrl();
                var stackTrace = exception.StackTrace == null ? "No exception.StackTrace" : exception.StackTrace.ToString();

                try
                {
                    await _emailSender.SendEmailAsync("leonardo.jrm@gmail.com", "Exception occurred",
                        DateTime.Now + "<br/><br/>" +
                        user + "<br/><br/>" +
                        url + "<br/><br/>" +
                        exception.ToString() + "<br/><br/>" +
                        stackTrace
                    );
                }
                catch (Exception)
                {
                }
            }

            // Clear the current response to avoid any issues with redirection
            context.Response.Clear();

            // Set the status code, you can use 500 (Internal Server Error) for general exceptions
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Redirect to the default MVC error page
            context.Response.Redirect("/Home/Error"); // Update this path based on your error handling setup

            // Return a completed task to end the middleware pipeline
            return Task.CompletedTask;
        }
    }
}
