using System.Collections.Generic;

namespace Nootus.Fabric.Mobile.Exception
{
    public class NTException : System.Exception
    {
        private readonly string message;
        private readonly List<NTError> errors;

        public NTException()
        {
        }

        public NTException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public NTException(string message)
        {
            this.message = message;
        }

        public NTException(string message, List<NTError> errors)
            : this(message)
        {
            this.errors = errors;
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }

        public List<NTError> Errors
        {
            get
            {
                return this.errors;
            }
        }
    }
}