using Witty.Models;

namespace Witty.Repositories
{
    public interface WittyEntryRepository
    {
        WittyEntry Get(string question);
        void Add(WittyEntry wittyEntry);
        void AddResponses(WittyEntry wittyEntry);
        bool Exists(string question);
    }
}
