using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<string> GetCategories()
        {
            return ResponsesGroupedByCategory().Select(r => r.Key).ToList();
        }

        public string StringifyCategories()
        {
            List<string> categories = GetCategories();
            StringBuilder catBuilder = new StringBuilder();
            catBuilder.Append("all,");

            foreach(string category in categories)
                catBuilder.Append(category).Append(",");

            return catBuilder.ToString();
        }
    }
}
