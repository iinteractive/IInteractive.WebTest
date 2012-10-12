using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebConsole
{
    /// <summary>
    /// Intended to be used for parsing script elements out of an HTML page.
    /// </summary>
    public class JavaScript : Link
    {
        public JavaScript(Uri Root, string Path)
            : base(Root, Path)
        {
        }

        public string Source { get; set; }
    }
}
