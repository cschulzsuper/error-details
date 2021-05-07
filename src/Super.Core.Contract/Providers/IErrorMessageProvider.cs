using System;

namespace Super.Core.ErrorDetails.Providers
{
    public interface IErrorMessageProvider<in TConstraint>
    {
        FormattableString? GetOrDefaultAsync(TConstraint constraint);
    }
}