using System.Collections.Generic;
using System.Linq;
using BuffetDesigner.Domain.Exception;

namespace BuffetDesigner.Domain._Base
{
    public class RuleValidator
    {
        private readonly List<string> _errorMessages;

        private RuleValidator()
        {
            _errorMessages = new List<string>();
        }

        public static RuleValidator New()
        {
            return new RuleValidator();
        }

        public RuleValidator When(bool withError, string errorMessage)
        {
            if (withError) _errorMessages.Add(errorMessage);

            return this;
        }

        public void ThrowExceptionIfExists()
        {            
            if (_errorMessages.Any()) throw new DomainException(_errorMessages);
        }

    }   

}