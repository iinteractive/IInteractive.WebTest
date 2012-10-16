using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IInteractive.WebConsole.Models
{
    public class UriComparable : Uri, IComparable, IComparable<UriComparable>
    {
        public UriComparable(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

        public UriComparable(string uri) : base(uri) { }
        public UriComparable(string uri, bool dontEscape) : base(uri, dontEscape) { }
        public UriComparable(string uriString, UriKind uriKind) : base(uriString, uriKind) { }
        public UriComparable(Uri baseUri, string relativeUri) : base(baseUri, relativeUri) { }
        public UriComparable(Uri baseUri, string relativeUri, bool dontEscape) : base(baseUri, relativeUri, dontEscape) { }
        public UriComparable(Uri baseUri, Uri relativeUri) : base(baseUri, relativeUri) { }

        public int CompareTo(UriComparable other)
        {
            return string.Compare(AbsoluteUri, other.AbsoluteUri);
        }

        public int CompareTo(object obj)
        {
            if (obj is UriComparable) return CompareTo((UriComparable) obj);
            if (obj is Uri) return string.Compare(AbsoluteUri, ((Uri) obj).AbsoluteUri);
            
            throw new ArgumentException("Compared to invalid type!  Must be Uri or UriComparable");
        }
    }
}
