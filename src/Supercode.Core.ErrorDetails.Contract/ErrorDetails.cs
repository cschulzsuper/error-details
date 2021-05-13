using System;
using System.Collections.Generic;

namespace Supercode.Core.ErrorDetails
{
    public record ErrorDetails(
        string Name,
        FormattableString? PrincipalMessage,
        string? Code,
        string? Source)
    {
        public ICollection<ErrorMessage> SecondaryMessages { get; init; } = new List<ErrorMessage>();
        public ICollection<ErrorDetails> InnerErrors { get; init; } = new List<ErrorDetails>();
    }
}
