using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Super.Core.ErrorDetails.Filters
{
    public class ErrorMessageFromAnnotationFilter<TAttribute> : IErrorDetailFilter
        where TAttribute : Attribute
    {
        private readonly Func<TAttribute, FormattableString> _selector;

        public ErrorMessageFromAnnotationFilter(Func<TAttribute, FormattableString> selector)
        {
            _selector = selector;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            if (context.Message == null)
            {
                var attribute = context.Members
                    .Select(x => x.GetCustomAttributes<TAttribute>())
                    .Where(x => x != null)
                    .SelectMany(x => x)
                    .FirstOrDefault();

                if (attribute != null)
                {
                    context.Message = _selector(attribute);
                }
            }
        }
    }
}