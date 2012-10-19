using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebConsole
{
    /// <summary>
    /// Intended to be used for parsing img elements out of an HTML page.
    /// </summary>
    public class Image : Link
    {
        public Image(Uri Root, string Path)
            : base(Root, Path)
        {
        }
    }
}
