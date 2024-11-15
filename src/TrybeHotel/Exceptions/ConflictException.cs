using System.Runtime.Serialization;

namespace TrybeHotel.Exceptions;

    [Serializable]
    internal class ConflictException : Exception
    {
        public ConflictException()
        {
        }

        public ConflictException(string? message) : base(message)
        {
        }

        public ConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }