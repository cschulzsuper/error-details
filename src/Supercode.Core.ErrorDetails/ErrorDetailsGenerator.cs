using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Supercode.Core.ErrorDetails.Filters;
using Supercode.Core.ErrorDetails.Options;

namespace Supercode.Core.ErrorDetails
{
    public class ErrorDetailsGenerator : IErrorDetailsGenerator
    {
        private readonly IErrorDetailFilterProvider _errorDetailFilterProvider;
        private readonly IOptions<ErrorDetailsOptions> _errorDetailsOptions;

        public ErrorDetailsGenerator(
            IErrorDetailFilterProvider errorDetailFilterProvider,
            IOptions<ErrorDetailsOptions> errorDetailsOptions)
        {
            _errorDetailFilterProvider = errorDetailFilterProvider;
            _errorDetailsOptions = errorDetailsOptions;
        }

        public async Task<ErrorDetails> GenerateAsync(Exception exception)
        {
            var errorDetailContext = new ErrorDetailContext
            {
                Exception = exception,
                TargetSite = exception.TargetSite
            };

            await ProcessAsync(errorDetailContext);

            var errorDetails = new ErrorDetails(
                errorDetailContext.Message,
                errorDetailContext.Exception.GetType().Name,
                errorDetailContext.Type,
                GetMemberName(errorDetailContext.TargetSite));

            if (_errorDetailsOptions.Value.InnerErrorsFromInnerExceptions)
            {
                foreach (var innerException in errorDetailContext.InnerExceptions)
                {
                    var innerError = await GenerateAsync(innerException);
                    errorDetails.InnerErrors.Add(innerError);
                }
            }

            if (_errorDetailsOptions.Value.StackTraceProcessing)
            {
                var stackTrace = new StackTrace(exception, false);
                var stackFrames = stackTrace.GetFrames();

                foreach (var stackFrame in stackFrames)
                {
                    var stackFrameErrorDetail = new ErrorDetailContext
                    {
                        TargetSite = stackFrame.GetMethod()
                    };

                    await ProcessAsync(stackFrameErrorDetail);

                    if (stackFrameErrorDetail.Message != null)
                    {
                        var errorMessageValue = stackFrameErrorDetail.Message;
                        var errorMessageType = stackFrameErrorDetail.Type;
                        var errorMessageFirstMember = stackFrameErrorDetail.Members
                            .FirstOrDefault(x => x.DeclaringType?.IsInterface == false);

                        var errorMessage = new ErrorMessage(
                            errorMessageValue,
                            errorMessageType,
                            GetMemberName(errorMessageFirstMember));

                        errorDetails.ErrorMessages.Add(errorMessage);
                    }
                }
            }

            return errorDetails;
        }

        private static string? GetMemberName(MemberInfo? memberInfo)
        {
            if (memberInfo == null)
            {
                return null;
            }

            if (memberInfo.DeclaringType == null)
            {
                return memberInfo.Name;
            }

            return $"{memberInfo.DeclaringType.Name}.{memberInfo.Name}";
        }

        private async Task ProcessAsync(ErrorDetailContext context)
        {
            var errorDetailContext = context;
            var errorFilters = _errorDetailFilterProvider.GetAll()
                .Reverse()
                .ToArray();

            Func<Task> errorFilterTask = () => Task.CompletedTask;

            foreach (var errorFilter in errorFilters)
            {
                errorFilterTask = ExecuteFilterAsync(
                    errorFilter,
                    errorDetailContext,
                    errorFilterTask);
            }

            await errorFilterTask();
        }

        private static Func<Task> ExecuteFilterAsync(IErrorDetailFilter filter, ErrorDetailContext context, Func<Task> nextFilterTask)
        {
            return () => filter.OnProcessingAsync(context, nextFilterTask);
        }
    }
}
