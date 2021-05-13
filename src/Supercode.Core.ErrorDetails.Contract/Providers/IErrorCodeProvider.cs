namespace Supercode.Core.ErrorDetails.Providers
{
    public interface IErrorCodeProvider<in TConstraint>
    {
        string? GetOrDefaultAsync(TConstraint constraint);
    }
}