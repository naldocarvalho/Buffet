using System;
using BuffetDesigner.Domain.Exception;
using Xunit;

namespace BuffetDesigner.DomainTest._Util
{
    public static class AssertExtension
    {
        public static void WithMessage(this DomainException exception, string message)
        {
            if (exception.ErrorMessages.Contains(message))
                Assert.True(true);
            else
                Assert.True(false, $"Estava esperando a mensagem: '{message}' e recebemos '{exception.ErrorMessages.ToString()}");
        }
        
    }
}