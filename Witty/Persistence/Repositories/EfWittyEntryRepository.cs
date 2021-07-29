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

        public void Add(WittyEntry wittyEntry)
        {
            appDbContext.Add(wittyEntry);
            appDbContext.SaveChanges();
        }
    }
}
