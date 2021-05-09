using System;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class InnerExceptionFilter : IErrorDetailFilter
    {
        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            var exception = context.Exception;

            if (exception?.InnerException != null)
            {
                context.InnerExceptions.Add(exception.InnerException);
            }

            await next();
        }
    }
}