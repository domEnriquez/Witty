using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using Witty.Models;
using Witty.Persistence;
using Witty.Persistence.Repositories;
using Witty.Tests.Builders;

namespace Witty.Tests.Persistence.Repositories
{
    [TestFixture]
    public class EfWittyEntryRepositoryTests
    {
        private EfWittyEntryRepository repo;
        DbContextOptionsBuilder<AppDbContext> defaultBuilder;

        [SetUp]
        public void SetUp()
        {
            defaultBuilder = new DbContextOptionsBuilder<AppDbContext>();
            defaultBuilder.UseInMemoryDatabase("InvalidInputTests");
            repo = new EfWittyEntryRepository(
                new AppDbContext(defaultBuilder.Options));
        }

        [Test]
        public void GivenNullOrEmptyId_WhenGetById_ThenThrowArgumentException()
        {
            Assert.That(() => repo.GetById(null),
                Throws.TypeOf<ArgumentException>());

            Assert.That(() => repo.GetById(string.Empty),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GivenNullWittyEntry_WhenAdd_ThenThrowArgumentNullException()
        {
            Assert.That(() => repo.Add(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GivenValidWittyEntry_WhenAdd_ThenStoreWittyEntryInDatabase()
        {
            WittyEntry we = WittyEntryBuilder
                .Default()
                .Build();

            DbContextOptionsBuilder<AppDbContext> builder = 
                new DbContextOptionsBuilder<AppDbContext>(); ;
            builder.UseInMemoryDatabase("AddSingleWittyEntry");

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                efRepo.Add(we);
                Assert.That(context.Entry(we).State, Is.EqualTo(EntityState.Added));
            }
        }
    }
}
