using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Super.Core.ErrorDetails.Filters
{
    public class ErrorMessageFilter : IErrorDetailFilter
    {
        private readonly string _errorMessage;

        public ErrorMessageFilter(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            context.Message ??= FormattableStringFactory.Create(_errorMessage);
        }
    }
}