using System.Collections.Generic;

namespace Supercode.Core.ErrorDetails.Options
{
    public class ErrorDetailsOptions
    {
        public bool StackTraceProcessing { get; set; } = false;
        public bool InnerErrorsFromInnerExceptions { get; set; } = false;
        public bool UnspecificyErrorMessages { get; set; } = false;
        public IList<ErrorDetailFilterDescriptor> Filters { get; } = new List<ErrorDetailFilterDescriptor>();

    }
}
