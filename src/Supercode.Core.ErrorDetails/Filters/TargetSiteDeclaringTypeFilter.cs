using System;
using System.Linq;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class TargetSiteDeclaringTypeFilter : IErrorDetailFilter
    {
        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            var members = context.Members
                .ToArray();

            foreach (var member in members)
            {
                if (member is not Type &&
                    member.DeclaringType != null)
                {
                    context.Members.Add(member.DeclaringType);
                }
            }

            await next();
        }
    }
}