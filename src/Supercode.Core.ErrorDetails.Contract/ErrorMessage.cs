using System;

namespace Supercode.Core.ErrorDetails
{
    public record ErrorMessage(
        FormattableString? Message,
        string? Code,
        string? Source);
}