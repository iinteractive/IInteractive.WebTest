using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebConsole
{
    public class CssUrl : Link, IEquatable<CssUrl>
    {
        public CssUrl(Uri Root, string Path)
            : base(Root, Path)
        {
        }

        public bool Equals(CssUrl link)
        {
            return ((Link)this).Equals((Link)link);
        }
    }
}
