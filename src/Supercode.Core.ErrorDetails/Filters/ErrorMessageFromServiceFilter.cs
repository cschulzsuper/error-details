using Supercode.Core.ErrorDetails.Providers;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorMessageFromServiceFilter<TService> : IErrorDetailFilter
        where TService : IErrorMessageProvider<MemberInfo>
    {
        private readonly TService _errorMessageProvider;

        public ErrorMessageFromServiceFilter(TService errorMessageProvider)
        {
            _errorMessageProvider = errorMessageProvider;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            if (context.Message == null)
            {
                var errorMessage = context.Members
                    .Select(member => _errorMessageProvider.GetOrDefaultAsync(member))
                    .FirstOrDefault();

                if (errorMessage != null)
                {
                    context.Message = errorMessage;
                }
            }
        }
    }
}