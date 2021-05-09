using System;

namespace Supercode.Core.ErrorDetails
{
    public record ErrorMessage(
        FormattableString Message,
        string? Type,
        string? Source);
}