using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.MarkupValidator
{
    public class MarkupValidationResponse
    {
        public MarkupValidationResponse()
        {
        }

        public Uri Uri { get; set; }
        public Uri CheckedBy { get; set; }
        public string DocType { get; set; }
        public string CharSet { get; set; }
        public bool Validity { get; set; }
        public Errors Errors { get; set; }
        public Warnings Warnings { get; set; }
    }

    public class Errors
    {
        public Errors()
        {
        }

        public int ErrorCount { 
            get {
                return ErrorList.Count;
            }
        }
        public List<Error> ErrorList { get; set; }
    }

    public class Warnings
    {
        public Warnings()
        {
        }

        public int WarningCount
        {
            get
            {
                return WarningList.Count;
            }
        }
        public List<Warning> WarningList { get; set; }
    }

    public class Problem
    {
        public Problem()
        {
        }

        public Problem(int Line, int Col, string Message)
        {
            this.Line = Line;
            this.Col = Col;
            this.Message = Message;
        }

        public int Line { get; set; }
        public int Col { get; set; }
        public string Message { get; set; }
    }

    public class Error : Problem
    {
        public Error()
        {
        }

        public Error(int Line, int Col, string Message, int Messageid, string Explanation, string Source)
            : base(Line, Col, Message)
        {
            this.Messageid = Messageid;
            this.Explanation = Explanation;
            this.Source = Source;
        }

        public int Messageid { get; set; }
        public string Explanation { get; set; }
        public string Source { get; set; }
    }

    public class Warning : Problem
    {
        public Warning()
        {
        }

        public Warning(int Line, int Col, string Message)
            : base(Line, Col, Message)
        {
        }
    }
}
