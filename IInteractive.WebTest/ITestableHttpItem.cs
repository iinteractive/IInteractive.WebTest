using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    public interface ITestableHttpItem
    {
        bool Validate(out IEnumerable<HttpValidationError> errors);
    }
}
