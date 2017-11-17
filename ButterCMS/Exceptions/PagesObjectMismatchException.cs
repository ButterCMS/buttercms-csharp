using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButterCMS
{
    public class PagesObjectMismatchException : Exception
    {
        public PagesObjectMismatchException()
        {
        }
        public PagesObjectMismatchException(string message)
        : base(message)
        {
        }
        public PagesObjectMismatchException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
