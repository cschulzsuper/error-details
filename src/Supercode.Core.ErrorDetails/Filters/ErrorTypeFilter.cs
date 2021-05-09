using System;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorTypeFilter : IErrorDetailFilter
    {
        private readonly string _errorType;

        public ErrorTypeFilter(string errorType)
        {
            _errorType = errorType;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            context.Type ??= _errorType;
        }
    }
}