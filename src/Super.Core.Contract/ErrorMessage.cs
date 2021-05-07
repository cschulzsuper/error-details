using System;

namespace Super.Core.ErrorDetails
{
    public record ErrorMessage(
        FormattableString Message,
        string? Type,
        string? Source);
}