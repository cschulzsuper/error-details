using System;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class InnerExceptionsFilter : IErrorDetailFilter
    {
        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            if (context.Exception is AggregateException aggregateException)
            {
                var flattenExceptions = aggregateException.Flatten().InnerExceptions;
                foreach (var flattenException in flattenExceptions)
                {
                    context.InnerExceptions.Add(flattenException);
                }
            }

            await next();

        }
    }
}