using System;

namespace Witty.Models
{
    public class Response
    {
        public string ResponseCategory { get; set; }
        public string ResponseString { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Response response &&
                   ResponseCategory == response.ResponseCategory &&
                   ResponseString == response.ResponseString;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ResponseCategory, ResponseString);
        }
    }
}
