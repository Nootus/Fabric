//-------------------------------------------------------------------------------------------------
// <copyright file="NTException.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Used to raise warnings/validations to the Angular. Also to carry unknown exceptions
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Exception
{
    using System.Collections.Generic;

    public class NTException : System.Exception
    {
        private string message;
        private List<NTError> errors;

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
