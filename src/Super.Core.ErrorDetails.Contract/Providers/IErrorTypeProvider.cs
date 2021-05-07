namespace Super.Core.ErrorDetails.Providers
{
    public interface IErrorTypeProvider<in TConstraint>
    {
        string? GetOrDefaultAsync(TConstraint constraint);
    }
}