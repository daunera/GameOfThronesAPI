using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesAPI.Exceptions
{
    class RedirectMainException : Exception
    {
        public RedirectMainException() { }

        public RedirectMainException(string message) : base(message) { }

        public RedirectMainException(string message, Exception inner) : base(message, inner) { }
    }
}
