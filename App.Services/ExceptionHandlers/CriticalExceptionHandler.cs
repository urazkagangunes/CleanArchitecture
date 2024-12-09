using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.Services.ExceptionHandlers;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //business logic
        if(exception is CriticalException)
        {
            Console.WriteLine("Sended the message already about the error.");
        }

        return ValueTask.FromResult(false);
    }
}
