using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebTest
{
    /// <summary>
    /// Intended to be used for parsing link elements out of an HTML page.
    /// </summary>
    public class StyleSheet : Link
    {
        public StyleSheet(Uri Root, string Path)
            : base(Root, Path)
        {
        }
    }
}
