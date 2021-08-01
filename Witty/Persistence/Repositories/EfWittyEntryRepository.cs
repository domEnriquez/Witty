using Microsoft.EntityFrameworkCore;
using System.Linq;
using Witty.Models;
using Witty.Repositories;

namespace Witty.Persistence.Repositories
{
    public class EfWittyEntryRepository : WittyEntryRepository
    {
        private readonly AppDbContext appDbContext;

        public EfWittyEntryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public WittyEntry Get(string question)
        {
            return appDbContext.WittyEntries
                .Include(w => w.Responses)
                .FirstOrDefault(w => w.Question == question);
        }

        public void Add(WittyEntry wittyEntry)
        {
            appDbContext.Add(wittyEntry);
            appDbContext.SaveChanges();
        }

        public void AddResponses(WittyEntry wittyEntry)
        {
            WittyEntry oldWe = Get(wittyEntry.Question);
            oldWe.Responses.AddRange(wittyEntry.Responses);
            appDbContext.SaveChanges();
        }

        public bool Exists(string question)
        {
            if (Get(question) != null)
                return true;
            else
                return false;
        }
    }
}
