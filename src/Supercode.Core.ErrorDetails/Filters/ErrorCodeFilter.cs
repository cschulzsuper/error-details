using System;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorCodeFilter : IErrorDetailFilter
    {
        private readonly string _errorCode;

        public ErrorCodeFilter(string errorCode)
        {
            _errorCode = errorCode;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            context.Code ??= _errorCode;
        }
    }
}