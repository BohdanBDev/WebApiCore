using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.Exceptions
{
    public class PostException : Exception
    {
        public PostException()
        {
        }

        public PostException(string message)
            : base(message)
        {
        }

        public PostException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
