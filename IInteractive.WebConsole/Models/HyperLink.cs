﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebConsole
{
    /// <summary>
    /// Intended to be used for parsing a elements out of an HTML page.
    /// </summary>
    public class HyperLink : Link, IEquatable<HyperLink>
    {
        public HyperLink(Uri Root, string Path)
            : base(Root, Path)
        {
        }

        public string Text { get; set; }

        public bool Equals(HyperLink link)
        {
            return ((Link)this).Equals((Link)link);
        }
    }
}
