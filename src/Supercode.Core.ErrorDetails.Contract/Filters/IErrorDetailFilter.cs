using System;
using System.Threading.Tasks;

namespace Supercode.Core.ErrorDetails.Filters
{
    public interface IErrorDetailFilter
    {
        Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next);
    }
}