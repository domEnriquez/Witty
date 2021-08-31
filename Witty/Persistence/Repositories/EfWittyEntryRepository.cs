using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public List<WittyEntry> GetAll()
        {
            return appDbContext.WittyEntries
                .Include(w => w.Responses).ToList();
        }

        public WittyEntry GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException();

            return appDbContext.WittyEntries
                .Include(w => w.Responses)
                .FirstOrDefault(w => w.Id.ToString() == id);
        }

        public WittyEntry GetByQuestion(string question)
        {
            if (string.IsNullOrEmpty(question))
                throw new ArgumentException();

            return appDbContext.WittyEntries
                .Include(w => w.Responses)
                .FirstOrDefault(w => w.Question == question);
        }

        public void Add(WittyEntry wittyEntry)
        {
            appDbContext.Add(wittyEntry);
        }

        public void AddResponses(WittyEntry wittyEntry)
        {
            if (wittyEntry == null)
                throw new ArgumentException();

            WittyEntry oldWe = GetByQuestion(wittyEntry.Question);
            oldWe.Responses.AddRange(wittyEntry.Responses);
        }

        public bool Exists(string question)
        {
            if (GetByQuestion(question) != null)
                return true;
            else
                return false;
        }

        public void DeleteResponse(string wittyEntryId, string responseId)
        {
            if (string.IsNullOrEmpty(wittyEntryId) || string.IsNullOrEmpty(responseId))
                throw new ArgumentException();

            WittyEntry we = GetById(wittyEntryId);
            Response r = we.Responses.FirstOrDefault(re => re.Id.ToString() == responseId);
            we.Responses.Remove(r);
        }

        public List<string> GetMatchingQuestions(string keyword)
        {
            if(string.IsNullOrEmpty(keyword))
                throw new ArgumentException();

            return appDbContext
                .WittyEntries
                .Where(w => w.Question.Contains(keyword))
                .Select(e => e.Question).ToList();
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }
    }
}
