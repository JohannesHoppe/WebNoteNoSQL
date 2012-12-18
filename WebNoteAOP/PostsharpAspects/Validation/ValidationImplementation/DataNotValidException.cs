namespace PostsharpAspects.Validation.ValidationImplementation
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DataNotValidException : Exception
    {
        public DataNotValidException()
        {
        }

        public DataNotValidException(string message) : base(message)
        {
        }

        public DataNotValidException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DataNotValidException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
