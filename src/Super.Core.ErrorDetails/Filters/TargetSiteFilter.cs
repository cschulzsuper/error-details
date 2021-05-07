using System;
using System.Threading.Tasks;

namespace Super.Core.ErrorDetails.Filters
{
    public class TargetSiteFilter : IErrorDetailFilter
    {
        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            if (context.TargetSite != null)
            {
                context.Members.Add(context.TargetSite);
            }

            await next();
        }
    }
}