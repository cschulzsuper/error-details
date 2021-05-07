using System;
using System.Runtime.CompilerServices;

namespace Super.Core.ErrorDetails.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class ErrorMessageAttribute : Attribute
    {
        public FormattableString Message { get; }

        public ErrorMessageAttribute(string message)
        {
            Message = FormattableStringFactory.Create(message);
        }
    }
}
