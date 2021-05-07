using System;
using System.Threading.Tasks;

namespace Super.Core.ErrorDetails
{
    public interface IErrorDetailsGenerator
    {
        Task<ErrorDetails> GenerateAsync(Exception exception);
    }
}