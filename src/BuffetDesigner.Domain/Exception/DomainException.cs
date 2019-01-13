using System;
using System.Collections.Generic;

namespace BuffetDesigner.Domain.Exception
{
    public class DomainException : ArgumentException
    {
        public List<string> ErrorMessages { get; set; }

        public DomainException(List<string> errorMessagesReceived)
        {
            ErrorMessages = errorMessagesReceived;
        }
    }
}