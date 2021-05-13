using Supercode.Core.ErrorDetails.Providers;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorCodeFromServiceFilter<TService> : IErrorDetailFilter
        where TService : class, IErrorCodeProvider<MemberInfo>
    {
        private readonly TService _errorCodeProvider;

        public ErrorCodeFromServiceFilter(TService errorCodeProvider)
        {
            _errorCodeProvider = errorCodeProvider;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {

            await next();

            if (context.Code == null)
            {
                var errorCode = context.Members
                    .Select(member => _errorCodeProvider.GetOrDefaultAsync(member))
                    .FirstOrDefault();

                if (errorCode != null)
                {
                    context.Code = errorCode;
                }
            }
        }
    }
}