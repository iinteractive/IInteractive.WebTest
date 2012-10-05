using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    public class LinkChecker
    {
        public LinkChecker(Uri root)
        {
            Root = root;
        }

        public Uri Root { get; private set; }
        public List<WebPage> Pages { get; private set; } 

        public void Crawl()
        {
            
        }
    }
}
