using Supercode.Core.ErrorDetails.Filters;
using System.Collections.Generic;

namespace Supercode.Core.ErrorDetails
{
    public interface IErrorDetailFilterProvider
    {
        IEnumerable<IErrorDetailFilter> GetAll();
    }
}