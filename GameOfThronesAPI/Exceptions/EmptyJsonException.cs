using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesAPI.Exceptions
{
    class EmptyJsonException : Exception
    {
        public EmptyJsonException() { }

        public EmptyJsonException(string message) : base(message) { }

        public EmptyJsonException(string message, Exception inner) : base(message,inner) { }
    }
}
