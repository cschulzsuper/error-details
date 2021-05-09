using System;

namespace Supercode.Core.ErrorDetails.Options
{
    public record ErrorDetailFilterDescriptor(Type Type, params object[] Arguments);
}
