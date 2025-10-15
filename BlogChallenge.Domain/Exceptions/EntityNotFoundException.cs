using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogChallenge.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string Code { get; set; }
        public EntityNotFoundException(string message, string ErrorCode)
            : base(message)
        {
            this.Code = ErrorCode;
        }
    }
}
