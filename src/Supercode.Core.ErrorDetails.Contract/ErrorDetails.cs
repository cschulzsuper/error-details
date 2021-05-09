using System;
using System.Collections.Generic;

namespace Supercode.Core.ErrorDetails
{
    public record ErrorDetails(
        FormattableString? PrincipalMessage,
        string Name,
        string? Type,
        string? Source)
    {
        public ICollection<ErrorMessage> ErrorMessages { get; init; } = new List<ErrorMessage>();
        public ICollection<ErrorDetails> InnerErrors { get; init; } = new List<ErrorDetails>();
    }
}
