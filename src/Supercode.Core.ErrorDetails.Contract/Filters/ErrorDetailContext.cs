using System;
using System.Collections.Generic;
using System.Reflection;

namespace Supercode.Core.ErrorDetails.Filters
{
    public class ErrorDetailContext
    {
        public Exception? Exception { get; init; } = null!;
        public MethodBase? TargetSite { get; init; } = null!;
        public ICollection<MemberInfo> Members { get; init; } = new HashSet<MemberInfo>();
        public ICollection<Exception> InnerExceptions { get; init; } = new HashSet<Exception>();
        public FormattableString? Message { get; set; }
        public string? Code { get; set; }
    }
}
