using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanApp.API.ExceptionHandler;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //business logic
        if (exception is CriticalException)
        {
            Console.WriteLine("Sended the message already about the error.");
        }

        return ValueTask.FromResult(false);
    }
}
