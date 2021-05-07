using System;

namespace Super.Core.ErrorDetails.Options
{
    public record ErrorDetailFilterDescriptor(Type Type, params object[] Arguments);
}
