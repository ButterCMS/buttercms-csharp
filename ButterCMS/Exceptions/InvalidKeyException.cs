using System;

namespace ButterCMS
{
    public class InvalidKeyException : Exception
    {
        public InvalidKeyException()
        {
        }
        public InvalidKeyException(string message)
        : base(message)
        {
        }
        public InvalidKeyException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
