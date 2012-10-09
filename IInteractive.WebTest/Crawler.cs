using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    public class Crawler
    {
        public static String UNIQUE_SEEDS_ERROR_MESSAGE = "The list of seeds may not contain any duplicate values.";

        public SortedSet<WebPage> Seeds { get; private set; }
        public Browser BrowserToTest { get; private set; }
        public SortedSet<WebPage> Pages { get; private set; }

        public Crawler(List<String> Seeds, Browser BrowserToTest)
        {
            this.Seeds = new SortedSet<WebPage>();
            foreach (String seed in Seeds.Distinct().ToList())
            {
                this.Seeds.Add(new WebPage(new Uri(seed), BrowserToTest));
            }

            if (Seeds.Count != this.Seeds.Count)
                throw new ArgumentException(UNIQUE_SEEDS_ERROR_MESSAGE, "Seeds");

            this.BrowserToTest = BrowserToTest;
        }

        /// <summary>
        /// What are the possible problems that could result?
        /// 1. The link parsed from the a element is not an Html type.
        /// </summary>
        public bool Crawl()
        {
            List<WebPage> pages = new List<WebPage>();
            foreach (WebPage seed in Seeds)
            {
                pages.Add(seed);
            }

            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].Get();
                if (pages[i].Error == null && GetSetOfCrawlableHosts().Contains(pages[i].RequestUrl.Host))
                {
                    foreach (HyperLink link in pages[i].Links)
                    {
                        WebPage potentialAdd = new WebPage(link.AbsoluteUri, pages[i].Browser);
                        if (!pages.Contains(potentialAdd))
                        {
                            pages.Add(potentialAdd);
                        }
                    }
                }
            }

            Pages = new SortedSet<WebPage>(pages);

            return pages.Count == Pages.Count;
        }

        private SortedSet<string> GetSetOfCrawlableHosts()
        {
            return new SortedSet<string>(
                    from seed in Seeds 
                    select seed.RequestUrl.Host
                    );
        }
    }
}
