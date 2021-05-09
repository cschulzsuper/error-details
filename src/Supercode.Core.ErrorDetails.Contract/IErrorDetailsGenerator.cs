using System;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails
{
    public interface IErrorDetailsGenerator
    {
        Task<ErrorDetails> GenerateAsync(Exception exception);
    }
}