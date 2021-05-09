using System.Collections.Generic;
using Supercode.Core.ErrorDetails.Filters;

namespace Supercode.Core.ErrorDetails
{
    public interface IErrorDetailFilterProvider
    {
        IEnumerable<IErrorDetailFilter> GetAll();
    }
}