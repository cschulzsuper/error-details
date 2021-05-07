using System;

namespace Super.Core.ErrorDetails.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class ErrorTypeAttribute : Attribute
    {
        public string Type { get; }

        public ErrorTypeAttribute(string type)
        {
            Type = type;
        }
    }
}
