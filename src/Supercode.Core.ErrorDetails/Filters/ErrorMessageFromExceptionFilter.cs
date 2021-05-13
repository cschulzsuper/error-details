using System;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorMessageFromExceptionFilter<TException> : IErrorDetailFilter
        where TException : Exception
    {
        private readonly Func<TException, FormattableString> _selector;

        public ErrorMessageFromExceptionFilter(Func<TException, FormattableString> selector)
        {
            _selector = selector;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            if (context.Message == null &&
                context.Exception is TException typedException)
            {
                context.Message = _selector(typedException);
            }
        }
    }
}