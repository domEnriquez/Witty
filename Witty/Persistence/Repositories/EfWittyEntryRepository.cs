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
    }
}
