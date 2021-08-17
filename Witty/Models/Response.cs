using System;

namespace Witty.Models
{
    public class Response
    {
        public int Id { get; set; }

        public string ResponseCategory { get; set; }
        public string ResponseString { get; set; }

        public int WittyEntryId { get; set; }

        public WittyEntry WittyEntry { get; set; }

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
