using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException()
        {

        }

        public ClientException(string message)
            : base(message)
        {

        }
    }
}
