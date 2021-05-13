using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorCodeFromAnnotationFilter<TAttribute> : IErrorDetailFilter
        where TAttribute : Attribute
    {
        private readonly Func<TAttribute, string> _selector;

        public ErrorCodeFromAnnotationFilter(Func<TAttribute, string> selector)
        {
            _selector = selector;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            if (context.Code == null)
            {
                var attribute = context.Members
                    .Select(x => x.GetCustomAttributes<TAttribute>())
                    .Where(x => x != null)
                    .SelectMany(x => x)
                    .FirstOrDefault();

                if (attribute != null)
                {
                    context.Code = _selector(attribute);
                }
            }
        }
    }
}