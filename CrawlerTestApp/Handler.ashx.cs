using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;

namespace CrawlerTestApp
{
    /// <summary>
    /// Summary description for Handler2
    /// </summary>
    public class Handler : IHttpHandler
    {
        public static string Prefix = "File-";
        public static string Template = "<!DOCTYPE html><html><head><title></title></head><body><p>{0}</p>{1}<p>{2}</p></body>";
        public static TestParameters CurrentParameters { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            WriteOutput(context.Request.Path, GetTestParameters(context), context.Response.Output);
            context.Response.StatusCode = 200;
            context.Response.Flush();
        }

        public void WriteOutput(string Path, TestParameters parameters, TextWriter writer)
        {
            string[] data = GetData(parameters);
            string links = GetLinks(ParsePosition(Path), parameters);

            writer.Write(Template, data[0], links, data[1]);
        }

        public string GetLinks(int[] positions, TestParameters parameters)
        {
            string links = "";
            if (positions.Count() <= parameters.Depth)
            {
                for (int i = 0; i < parameters.FanOut; i++)
                {
                    links += "<a href=\"" + GetChildPositionLink(positions, i) + "\"> Link </a><br />";
                }
            }
            return links;
        }

        public int[] ParsePosition(string Path)
        {
            string filePath = Path.Substring(("/PerformanceTests/" + Prefix).Length);
            string[] positions = filePath.Split('-');
            int[] output = new int[positions.Length];
            int ctr = 0;
            foreach (string position in positions)
            {
                output[ctr] = Int32.Parse(position);
                ctr++;
            }
            return output;
        }

        public string GetChildPositionLink(int[] position, int child)
        {
            string output = "/PerformanceTests/" + Prefix;
            for (int i = 0; i < position.Count(); i++)
            {
                output += position[i];
                output += "-";
            }
            output += child;

            return output;
        }

        public string[] GetData(TestParameters parameters)
        {
            string[] output = new string[2];
            int break1 = parameters.Rand.Next(parameters.Data);
            output[0] = new string('A', break1);
            output[1] = new string('B', parameters.Data - break1);
            return output;
        }

        public TestParameters GetTestParameters(HttpContext context)
        {
            var parameters = context.Request.Params;
            string[] fanOuts = parameters.GetValues("FanOut");
            string[] depths = parameters.GetValues("Depth");
            string[] datas = parameters.GetValues("Data");
            string[] seeds = parameters.GetValues("Seed");
            string fanOut = fanOuts != null ? fanOuts[0] : null;
            string depth = depths != null ? depths[0] : null;
            string data = datas != null ? datas[0] : null;
            string seed = seeds != null ? seeds[0] : null; 

            if (!string.IsNullOrEmpty(fanOut) && !string.IsNullOrEmpty(depth) && !string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(seed))
            {
                try
                {
                    TestParameters attemptedNewParameters
                        = new TestParameters(
                                Int32.Parse(fanOut)
                                , Int32.Parse(depth)
                                , Int32.Parse(data)
                                , Int32.Parse(seed)
                        );
                    CurrentParameters = attemptedNewParameters;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            
            return CurrentParameters;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class TestParameters
    {
        public int FanOut { get; set; }
        public int Depth { get; set; }
        public int Data { get; set; }
        public Random Rand { get; set; }

        public TestParameters(int FanOut, int Depth, int Data, int Seed)
        {
            this.FanOut = FanOut;
            this.Depth = Depth;
            this.Data = Data;
            this.Rand = new Random(Seed);
        }
    }
}