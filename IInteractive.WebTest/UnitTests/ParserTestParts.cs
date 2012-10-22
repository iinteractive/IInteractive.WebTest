using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IInteractive.WebTest
{
    [TestClass]
    public class ParserTestParts
    {
        public static Uri ROOT = new Uri("http://www.iinteractive.com/");

        public static string[] WHITE_SPACES = new string[] {
            " ",
            "\n\t",
            "\r\n "
        };

        public static string[] WHITE_SPACES_2 = new string[] {
            "",
            " ",
            "\n\t",
            "\r\n "
        };

        public static string[] TEXT = new string[] {
            "",
            "text",
            "\"text\'"
        };

        public static string[] OTHER_ATTRIBUTES = new string[] {
            "target=\"_blank\"",
            "target='_blank'"
        };

        public static string[] OTHER_SCRIPT_ATTRIBUTES = new string[] {
            "language=\"text\\javascript\"",
            "language='text\\javascript'"
        };

        public static string[] OTHER_STYLESHEET_ATTRIBUTES = new string[] {
            "rel=\"stylesheet\"",
            "rel='stylesheet'"
        };

        public static string[] URIS = new string[] {
            "htTp://webcrawlertest",
            "httPs://webcrawlertest", 
            "/AbsoluteUrlTests/CaseA/Seed.aspx", 
            "Linked.htm",
            "",
            "maiLTo:fake@iinteractive.com",
            "javaScript:document.location.href='http://www.iinteractive.com/';",
            "javaScript:document.location.href=\"http://www.iinteractive.com/\";",
            "tEl:+1-201-555-0111"
        };

        public static string[] FORMAT_EXCEPTION_URIS = new string[] {
            "http://{}[]!@#$%^&*()_+~`"
        };

        public static string[] HREF_TEMPLATES = new string[] {
            "href=\"{0}\"",
            "hrEf=\"{0}\"",
            "hrEf='{0}'"
        };

        public static string[] SRC_TEMPLATES = new string[] {
            "src=\"{0}\"",
            "srC=\"{0}\"",
            "sRc='{0}'"
        };

        public static string[] A_TEMPLATES = new string[] {
            "<a{0}>{1}</a>",
            "<ab{0}>{1}</ab>",
            "<!--<a{0}>{1}</a>-->"
        };

        public static string[] SCRIPT_TEMPLATES = new string[] {
            "<script{0}>{1}</script>",
            "<scriptb{0}>{1}</scriptb>",
            "<!--<script{0}>{1}</script>-->"
        };

        public static string[] IMAGE_TEMPLATES = new string[] {
            "<img{0}>",
            "<img{0}/>",
            "<imgb{0}>",
            "<!--<imgb{0}>-->"
        };

        public static string[] STYLESHEET_TEMPLATES = new string[] {
            "<link{0}>",
            "<link{0}/>",
            "<linkb{0}>",
            "<!--<linkb{0}>-->"
        };

        public static string[] URL_TEMPLATES = new string[]
        {
            "urL{0}({1}'{2}'{3})",
            "uRl{0}({1}\"{2}\"{3})"
        };

        public static string[] CSS_COMMENT_TEMPLATES = new string[]
        {
            "/*{0}*/"
        };
    }
}
