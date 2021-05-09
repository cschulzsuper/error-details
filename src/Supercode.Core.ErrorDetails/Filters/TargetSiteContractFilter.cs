using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class TargetSiteContractFilter : IErrorDetailFilter
    {
        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            var members = context.Members
                .ToArray();

            foreach (var member in members)
            {
                var method = member as MethodBase;
                if (method == null ||
                    method.IsSpecialName ||
                    method is not MethodInfo ||
                    !method.IsPublic)
                {
                    continue;
                }

                method = FindInterfaceMethod(method);
                if (method != null)
                {
                    context.Members.Add(method);
                }
            }

            await next();
        }

        private static MethodBase? FindInterfaceMethod(MethodBase method)
        {
            return method.DeclaringType?
                .GetInterfaces()
                .Select(x => x.GetMethod(method.Name, method.GetParameters().Select(y => y.ParameterType).ToArray()))
                .FirstOrDefault(x => x != null);
        }
    }
}