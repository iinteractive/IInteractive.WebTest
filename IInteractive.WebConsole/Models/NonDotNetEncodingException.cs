using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.MarkupValidator
{
    public class NonDotNetEncodingException : Exception
    {
        public NonDotNetEncodingException(string CharsetToUse)
        {
            this.CharsetToUse = CharsetToUse;
        }

        public string CharsetToUse;
    }
}
