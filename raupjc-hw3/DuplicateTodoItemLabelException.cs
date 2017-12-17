using System;
using System.Runtime.Serialization;

namespace Task_1
{
    [Serializable]
    internal class DuplicateTodoItemLabelException : Exception
    {
        public DuplicateTodoItemLabelException()
        {
        }

        public DuplicateTodoItemLabelException(string message) : base(message)
        {
        }

        public DuplicateTodoItemLabelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateTodoItemLabelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}