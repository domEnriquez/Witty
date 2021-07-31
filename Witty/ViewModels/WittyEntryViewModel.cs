using System.Collections.Generic;
using System.Linq;
using Witty.Models;

namespace Witty.ViewModels
{
    public class WittyEntryViewModel
    {
        public string Question { get; set; }
        public List<Response> Responses { get; set; }

        public IEnumerable<IGrouping<string, Response>> ResponsesGroupedByCategory()
        {
            return Responses.GroupBy(r => r.ResponseCategory);
        }

    }
}
