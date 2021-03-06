using Supercode.Core.ErrorDetails.Filters;

namespace Supercode.Core.ErrorDetails.Options
{
    public static class ErrorDetailsOptionsExtensions
    {

        public static ErrorDetailsOptions EnableStackTraceIteration(this ErrorDetailsOptions options, bool value = true)
        {
            options.StackTraceProcessing = value;
            return options;
        }

        public static ErrorDetailsOptions EnableInnerErrorsFromInnerExceptions(this ErrorDetailsOptions options, bool value = true)
        {
            options.InnerErrorsFromInnerExceptions = value;
            return options;
        }

        public static ErrorDetailsOptions EnableUnspecificyErrorMessages(this ErrorDetailsOptions options, bool value = true)
        {
            options.UnspecificyErrorMessages = value;
            return options;
        }

        public static ErrorDetailsOptions ErrorDetailFilter<TFilter>(this ErrorDetailsOptions options)
            where TFilter : class, IErrorDetailFilter
        {
            options.Filters.Add(new ErrorDetailFilterDescriptor(typeof(TFilter)));
            return options;
        }
    }
}