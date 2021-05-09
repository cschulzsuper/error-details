using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Supercode.Core.ErrorDetails.Providers;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorTypeFromServiceFilter<TService> : IErrorDetailFilter
        where TService : class, IErrorTypeProvider<MemberInfo>
    {
        private readonly TService _errorTypeProvider;

        public ErrorTypeFromServiceFilter(TService errorTypeProvider)
        {
            _errorTypeProvider = errorTypeProvider;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {

            await next();

            if (context.Type == null)
            {
                var errorType = context.Members
                    .Select(member => _errorTypeProvider.GetOrDefaultAsync(member))
                    .FirstOrDefault();

                if (errorType != null)
                {
                    context.Type = errorType;
                }
            }
        }
    }
}