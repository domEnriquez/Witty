using System.Collections.Generic;

namespace Witty.Models
{
    public class WittyEntry
    {
        public WittyEntry()
        {
            Responses = new List<Response>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public List<Response> Responses { get; set; }
    }
}
