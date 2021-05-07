using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace SharedClasses.Exceptions
{
    [Serializable]
    public class SharedException : Exception
    {
        [IgnoreDataMember]
        public new FormattableString Message { get; }

        public SharedException()
        {
            Message = FormattableStringFactory.Create(string.Empty);
        }

        public SharedException(FormattableString message)
            : base(message.ToString())
        {
            Message = message;
        }

        public SharedException(FormattableString message, Exception inner)
            : base(message.ToString(), inner)
        {
            Message = message;
        }

        protected SharedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Message = FormattableStringFactory.Create(string.Empty);
        }
    }
}
