using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace NapovedaPomoc {
    public class Problem {

        static string [] args = [];
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
                ctx.ProblemDetails.Extensions["customMessage"] = "Něco se pokazilo";
            };
        });

        //standardizované chyby ve formátu RFC 7807
        builder.Services.AddProblemDetails(options => {
            options.CustomizeProblemDetails = ctx => {
                //vlatní rozšíření
                ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
                ctx.ProblemDetails.Extensions["Informace"] = "Něco se pokazilo";
            };
        });

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        var app = builder.Build();


        app.MapGet("/", () => "Hello World!");

        //app.UseExceptionHandler(); // přesměruje chyby do ProblemDetails
        app.UseExceptionHandler();   // pro 500 a jiné chyby
        //může být použito jednoduše
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseStatusCodePages();    // pro 404, 401, 403 atd. 

        // Přidání vlastního middleware pro zpracování výjimek
        //app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }

    public class GlobalExceptionHandler: IExceptionHandler {

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

                _logger.LogError(exception, exception.Message);
                var response = new ErrorResponse
                {
                    Message = exception.Message,
                };

                switch (exception)
                {
                    case BadHttpRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.Title = exception.GetType().Name;
                        break;

                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        response.Title = "Neautorizovaný přístup";
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response.Title = "Internal Server Error";
                        break;
                }

                httpContext.Response.StatusCode = response.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

                return true;
        }
        
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
