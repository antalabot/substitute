using System;

namespace Substitute.Business.DataStructs.Exceptions
{
    [Serializable]
    public class CommandNotExistsException : Exception
    {
        public CommandNotExistsException() { }
        public CommandNotExistsException(string message) : base(message) { }
        public CommandNotExistsException(string message, Exception inner) : base(message, inner) { }
        protected CommandNotExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
