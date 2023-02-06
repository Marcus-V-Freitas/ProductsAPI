namespace ProductsAPI.Middlewares;

public sealed class ExceptionErrorMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionErrorMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (SqlException)
        {
            await HandleExceptionAsync(context, "Parameter with invalid type.");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, string message)
    {
        await SendResponse(context, message, HttpStatusCode.InternalServerError);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        await SendResponse(context, exception.Message, HttpStatusCode.InternalServerError);
    }

    private Task SendResponse(HttpContext context, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        string result = new
        {
            Message = message,
            StatusCode = (int)statusCode
        }.ToString();

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(result!);
    }
}