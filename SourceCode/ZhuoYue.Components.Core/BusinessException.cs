using System;
using System.Collections.Generic;
using System.Text;

namespace ZhuoYue.Components.Core
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException) : base(message, innerException) { }

    }
}
