using System;

namespace DotnetCoreSample.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base()
        {

        }
        public BusinessException(string message) : base(message)
        {

        }
    }
}
