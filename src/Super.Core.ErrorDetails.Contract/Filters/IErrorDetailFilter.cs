using System;
using System.Threading.Tasks;

namespace Super.Core.ErrorDetails.Filters
{
    public interface IErrorDetailFilter
    {
        Task OnProcessingAsync(ErrorDetailContext context, Func<Task> next);
    }
}