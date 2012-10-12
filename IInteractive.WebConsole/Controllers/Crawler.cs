using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebConsole
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
                // If the Uri is bad, we are not going to attempt to catch the 
                // UriFormatException, this should be reported to the user and
                // not in a test case.
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
                HttpRequestResult result = BrowserToTest.Get(seed);
                result.Parse();
                HttpRequestResults.Add(result);
            }
            for (int i = 0; i < HttpRequestResults.Count && HttpRequestResults.Count < RecursionLimit; i++)
            {
                if (HttpRequestResults[i].Links != null && GetSetOfCrawlableHosts().Contains(HttpRequestResults[i].ResultUrl.Host))
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
                                var result = BrowserToTest.Get(link.AbsoluteUri);
                                result.Parse();
                                HttpRequestResults.Add(result);
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
                        foreach (var result2 in HttpRequestResults)
                        {
                            if (result2.Equals(link))
                            {
                                link.WasRetrieved = true;
                                break;
                            }
                        }
                    }
                }
            }
        }



        public SortedSet<string> GetSetOfCrawlableHosts()
        {
            return new SortedSet<string>(
                    from seed in Seeds
                    select seed.Host
                    );
        }
    }
}
