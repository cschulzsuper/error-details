using System;

namespace Supercode.Core.ErrorDetails.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class ErrorCodeAttribute : Attribute
    {
        public string Type { get; }

        public ErrorCodeAttribute(string type)
        {
            Type = type;
        }
    }
}
