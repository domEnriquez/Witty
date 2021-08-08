using System.Collections.Generic;
using System.Linq;
using Witty.Models;

namespace Witty.ViewModels
{
    public class WittyEntryViewModel
    {
        public WittyEntryViewModel()
        {
            WittyEntry = new WittyEntry();
        }

        public WittyEntry WittyEntry { get; set; }

        public IEnumerable<IGrouping<string, Response>> ResponsesGroupedByCategory()
        {
            return WittyEntry.Responses.GroupBy(r => r.ResponseCategory);
        }

    }
}
