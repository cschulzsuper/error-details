using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Super.Core.ErrorDetails.Filters
{
    public class ErrorTypeFromAnnotationFilter<TAttribute> : IErrorDetailFilter
        where TAttribute : Attribute
    {
        private readonly Func<TAttribute, string> _selector;

        public ErrorTypeFromAnnotationFilter(Func<TAttribute, string> selector)
        {
            _selector = selector;
        }

        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            await next();

            if (context.Type == null)
            {
                var attribute = context.Members
                    .Select(x => x.GetCustomAttributes<TAttribute>())
                    .Where(x => x != null)
                    .SelectMany(x => x)
                    .FirstOrDefault();

                if (attribute != null)
                {
                    context.Type = _selector(attribute);
                }
            }
        }
    }
}