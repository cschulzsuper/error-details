using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class TargetSiteAsyncFilter : IErrorDetailFilter
    {
        public async Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next)
        {
            var method = context.TargetSite;

            if (method?.Name == nameof(IAsyncStateMachine.MoveNext) &&
                method.DeclaringType?.IsAssignableTo(typeof(IAsyncStateMachine)) == true)
            {
                var asyncMethod = FindDeclaredMethod(method);
                if (asyncMethod != null)
                {
                    context.Members.Add(asyncMethod);
                    context.Members.Remove(method);
                }
            }

            await next();

        }

        private static MethodBase? FindDeclaredMethod(MemberInfo asyncMethod)
        {
            var generatedType = asyncMethod.DeclaringType;
            if (generatedType == null)
            {
                return null;
            }

            var originalType = generatedType.DeclaringType;
            if (originalType == null)
            {
                return null;
            }

            return originalType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .SingleOrDefault(x => x
                    .GetCustomAttributes<AsyncStateMachineAttribute>()
                    .Any(y => y.StateMachineType == generatedType));

        }
    }
}