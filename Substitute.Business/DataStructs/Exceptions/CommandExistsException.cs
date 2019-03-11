using System;

namespace Substitute.Business.DataStructs.Exceptions
{

    [Serializable]
    public class CommandExistsException : Exception
    {
        public CommandExistsException() { }
        public CommandExistsException(string message) : base(message) { }
        public CommandExistsException(string message, Exception inner) : base(message, inner) { }
        protected CommandExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
