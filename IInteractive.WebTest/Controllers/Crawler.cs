using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    public class Crawler
    {
        public static String UNIQUE_SEEDS_ERROR_MESSAGE = "The list of seeds may not contain any duplicate values.";

        public List<HttpRequestResult> HttpRequestResults;
        public SortedSet<Uri> Seeds { get; set; }
        public Browser BrowserToTest { get; private set; }
        public int RecursionLimit { get; set; }

        public Crawler(List<String> Seeds, Browser BrowserToTest, int RecursionLimit)
        {
            this.Seeds = new SortedSet<Uri>();
            foreach (String seed in Seeds.Distinct().ToList())
            {
                this.Seeds.Add(new Uri(seed));
            }

            if (Seeds.Count != this.Seeds.Count)
                throw new ArgumentException(UNIQUE_SEEDS_ERROR_MESSAGE, "Seeds");

            this.BrowserToTest = BrowserToTest;
            this.RecursionLimit = RecursionLimit;
        }

        public void Crawl()
        {
            this.HttpRequestResults = new List<HttpRequestResult>();
            foreach (Uri seed in Seeds)
            {
                HttpRequestResults.Add(BrowserToTest.Get(seed));
            }
            for (int i = 0; i < HttpRequestResults.Count && HttpRequestResults.Count < RecursionLimit; i++)
            {
                if (HttpRequestResults[i].Links != null && GetSetOfCrawlableHosts().Contains(HttpRequestResults[i].RequestUrl.Host))
                {
                    foreach (Link link in HttpRequestResults[i].Links)
                    {
                        if (link.Error == null)
                        {
                            bool alreadyRequested = (from result in HttpRequestResults
                                                     where result.Equals(link)
                                                     select result).Count() != 0;
                            if (!alreadyRequested && HttpRequestResults.Count < RecursionLimit)
                            {
                                HttpRequestResults.Add(BrowserToTest.Get(link.AbsoluteUri));
                            }
                        }
                    }
                }
            }

            foreach (var result in HttpRequestResults)
            {
                if (result.Links != null)
                {
                    foreach (var link in result.Links)
                    {
                        if (result.Equals(link))
                        {
                            link.WasRetrieved = true;
                        }
                    }
                }
            }
        }



        private SortedSet<string> GetSetOfCrawlableHosts()
        {
            return new SortedSet<string>(
                    from seed in Seeds
                    select seed.Host
                    );
        }
    }
}
