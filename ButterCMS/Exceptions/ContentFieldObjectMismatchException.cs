using System;

namespace ButterCMS
{
    public class ContentFieldObjectMismatchException : Exception
    {
        public ContentFieldObjectMismatchException()
        {
        }
        public ContentFieldObjectMismatchException(string message)
        : base(message)
        {
        }
        public ContentFieldObjectMismatchException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
