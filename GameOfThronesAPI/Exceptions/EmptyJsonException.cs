using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesAPI.Exceptions
{

    /// <summary>
    /// This exception throwed, when a returning json is empty like that: "[]"
    /// </summary>
    class EmptyJsonException : Exception
    {
        public EmptyJsonException() { }

        public EmptyJsonException(string message) : base(message) { }

        public EmptyJsonException(string message, Exception inner) : base(message,inner) { }
    }
}
