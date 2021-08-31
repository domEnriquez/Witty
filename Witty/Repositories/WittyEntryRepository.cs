using System.Collections.Generic;
using Witty.Models;

namespace Witty.Repositories
{
    public interface WittyEntryRepository
    {
        List<WittyEntry> GetAll();
        WittyEntry GetById(string id);
        WittyEntry GetByQuestion(string question);   
        void Add(WittyEntry wittyEntry);
        void AddResponses(WittyEntry wittyEntry);
        bool Exists(string question);
        void DeleteResponse(string wittyEntryId, string responseId);
        public List<string> GetMatchingQuestions(string keyword);
        void Save();
    }
}
