using System.Collections.Generic;
using Super.Core.ErrorDetails.Filters;

namespace Super.Core.ErrorDetails
{
    public interface IErrorDetailFilterProvider
    {
        IEnumerable<IErrorDetailFilter> GetAll();
    }
}