using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTestApiDemo
{
    class CommonApiException : Exception
    {
        public CommonApiException() : base() { }
        public CommonApiException(String message) : base(message) { }
        public CommonApiException(String message, Exception throwable) : base(message, throwable) { }

    }
}
