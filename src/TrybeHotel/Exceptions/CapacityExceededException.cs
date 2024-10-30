using System.Runtime.Serialization;

namespace TrybeHotel.Exceptions;

[Serializable]
    internal class CapacityExceededException : Exception
    {
        public CapacityExceededException()
        {
        }

        public CapacityExceededException(string? message) : base(message)
        {
        }

        public CapacityExceededException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CapacityExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }