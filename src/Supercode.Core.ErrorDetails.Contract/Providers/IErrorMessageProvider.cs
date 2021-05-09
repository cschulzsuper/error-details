using System;

namespace Supercode.Core.ErrorDetails.Providers
{
    public interface IErrorMessageProvider<in TConstraint>
    {
        FormattableString? GetOrDefaultAsync(TConstraint constraint);
    }
}