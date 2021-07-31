using Witty.Models;

namespace Witty.Repositories
{
    public interface WittyEntryRepository
    {
        WittyEntry Get(string question);
        void Add(WittyEntry wittyEntry);
    }
}
