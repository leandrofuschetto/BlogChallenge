using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogChallenge.Domain.Exceptions
{
    public class DataBaseContextException : Exception
    {
        public string Code { get; private set; } = "DATABASE_GENERAL_EXCEPTION";

        public DataBaseContextException(string message) : base(message) { }

        public DataBaseContextException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
