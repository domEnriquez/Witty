using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        public void GivenExistingWittyEntryId_WhenGetById_ThenReturnWittyEntry()
        {
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>(); ;
            builder.UseInMemoryDatabase("GetWittyEntryById");
            int expectedId = seedWithOneWittyEntry(builder.Options);

            using(AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                WittyEntry we = efRepo.GetById(expectedId.ToString());

                Assert.That(we.Id, Is.EqualTo(expectedId));
            }
        }

        [Test]
        public void GivenNonExistingWittyEntryId_WhenGetById_ThenReturnNull()
        {
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>(); ;
            builder.UseInMemoryDatabase("GetWittyEntryById");

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);

                Assert.That(efRepo.GetById("10"), Is.Null);
            }
        }

        [Test]
        public void GivenNullOrEmptyQuestion_WhenGetByQuestion_ThenThrowArgumentException()
        {
            Assert.That(() => repo.GetByQuestion(null),
                Throws.TypeOf<ArgumentException>());

            Assert.That(() => repo.GetByQuestion(string.Empty),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GivenExistingQuestion_WhenGetByQuestion_ThenReturnWittyEntry()
        {
            string question = "What?";
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>(); ;
            builder.UseInMemoryDatabase("GetWittyEntryByQuestion");
            seedWithOneWittyEntry(builder.Options, question);

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                WittyEntry we = efRepo.GetByQuestion(question);

                Assert.That(we.Question, Is.EqualTo(question));
            }
        }

        [Test]
        public void GivenNonExistingQuestion_WhenGetByQuestion_ThenReturnNull()
        {
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>(); ;
            builder.UseInMemoryDatabase("GetWittyEntryByQuestion");

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);

                Assert.That(efRepo.GetByQuestion("When?"), Is.Null);
            }
        }

        [Test]
        public void GivenNullWittyEntry_WhenAddResponses_ThenThrowArgumentException()
        {
            Assert.That(() => repo.AddResponses(null),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GivenWittyEntryWithUpdatedResponses_WhenAddResponses_ThenSaveUpdatedResponses()
        {
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>(); ;
            builder.UseInMemoryDatabase("WittyEntryAddResponse");
            int seededId = seedWithOneWittyEntry(builder.Options, "What?");

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                WittyEntry newWe = WittyEntryBuilder
                    .Default()
                    .WithId(seededId)
                    .WithQuestion("What?")
                    .WithResponses(ResponseListBuilder.Default().BuildWithEmptyResponseFields(2))
                    .Build();

                efRepo.AddResponses(newWe);
                efRepo.Save();
            }

            using(AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                WittyEntry we = efRepo.GetById(seededId.ToString());

                Assert.That(we.Responses.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void GivenNullWittyEntry_WhenAdd_ThenThrowArgumentNullException()
        {
            Assert.That(() => repo.Add(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GivenValidWittyEntry_WhenAdd_ThenAddWittyEntryInDatabase()
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

        [Test]
        public void GivenEmptyIds_WhenDeleteResponse_ThenThrowArgumentException()
        {
            Assert.That(() => repo.DeleteResponse(string.Empty, "1"),
                Throws.TypeOf<ArgumentException>());

            Assert.That(() => repo.DeleteResponse("1", string.Empty),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GivenExistingWittyEntryIdAndResponseId_WhenDeleteResponse_ThenDeleteTargetResponse()
        {
            int numOfResponses = 2;
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>(); ;
            builder.UseInMemoryDatabase("WittyEntryDeleteResponse");
            int seededId = seedWithOneWittyEntry(builder.Options, "What?", numOfResponses);

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                WittyEntry we = efRepo.GetById(seededId.ToString());

                efRepo.DeleteResponse(seededId.ToString(), we.Responses[0].Id.ToString());
                efRepo.Save();
            }

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                WittyEntry we = efRepo.GetById(seededId.ToString());

                Assert.That(we.Responses.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void GivenNullOrEmptyKeyword_WhenGetMatchingQuestions_ThenThrowArgumentException()
        {
            Assert.That(() => repo.GetMatchingQuestions(null),
                Throws.TypeOf<ArgumentException>());

            Assert.That(() => repo.GetMatchingQuestions(string.Empty),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GivenQuestionsThatMatchesKeyword_WhenGetMatchingQuestions_ThenReturnQuestions()
        {
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("KeywordHasMatchingQuestions");
            int seededId = seedWithOneWittyEntry(builder.Options, "How are you?");

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                List<string> questions = efRepo.GetMatchingQuestions("How");

                Assert.That(questions, Has.Count.EqualTo(1));
                Assert.That(questions[0], Is.EqualTo("How are you?"));
            }
        }

        [Test]
        public void GivenNoMatchingQuestionsInGivenKeyword_WhenGetMatchingQuestions_ThenReturnEmptyString()
        {
            DbContextOptionsBuilder<AppDbContext> builder =
                new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("KeywordHasNoMatchingQuestions");
            int seededId = seedWithOneWittyEntry(builder.Options, "How are you?");

            using (AppDbContext context = new AppDbContext(builder.Options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(context);
                List<string> questions = efRepo.GetMatchingQuestions("aaa");

                Assert.That(questions, Has.Count.EqualTo(0));
            }
        }

        private int seedWithOneWittyEntry(DbContextOptions<AppDbContext> options)
        {
            using (AppDbContext seedContext = new AppDbContext(options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(seedContext);
                WittyEntry we = new WittyEntry();
                efRepo.Add(we);
                efRepo.Save();

                return we.Id;
            }
        }

        private int seedWithOneWittyEntry(DbContextOptions<AppDbContext> options, 
            string question)
        {
            using (AppDbContext seedContext = new AppDbContext(options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(seedContext);
                WittyEntry we = WittyEntryBuilder
                    .Default()
                    .WithQuestion(question)
                    .Build();

                efRepo.Add(we);
                efRepo.Save();

                return we.Id;
            }
        }

        private int seedWithOneWittyEntry(DbContextOptions<AppDbContext> options,
            string question, int numOfEmptyResponses)
        {
            using (AppDbContext seedContext = new AppDbContext(options))
            {
                EfWittyEntryRepository efRepo = new EfWittyEntryRepository(seedContext);
                WittyEntry we = WittyEntryBuilder
                    .Default()
                    .WithQuestion(question)
                    .WithResponses(ResponseListBuilder
                        .Default()
                        .BuildWithEmptyResponseFields(numOfEmptyResponses))
                    .Build();

                efRepo.Add(we);
                efRepo.Save();

                return we.Id;
            }
        }
    }
}
