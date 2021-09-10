using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Witty.Controllers.Api;
using Witty.Models;
using Witty.Repositories;
using Witty.Tests.Builders;
using Witty.Tests.Utility;

namespace Witty.Tests.Controllers.Api
{
    [TestFixture]
    public class WittyEntryControllerTests
    {
        WittyEntryController controller;
        Mock<WittyEntryRepository> mockRepo;

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<WittyEntryRepository>();
            controller = new WittyEntryController(mockRepo.Object);
        }


        [Test]
        public void WhenDelete_ThenCallDeleteResponseInRepo()
        {
            controller.Delete("1", "2");

            mockRepo.Verify(r => r.DeleteResponse(It.IsAny<string>(), It.IsAny<string>()));
        }
        
        [Test]
        public void WhenSuccessfulDelete_ThenReturnOkResult()
        {
            StatusCodeResult statusCode = UnitTestUtility
                .GetActionResultModel<StatusCodeResult>(controller.Delete(It.IsAny<string>(), It.IsAny<string>()));

            Assert.That(statusCode, Is.TypeOf<OkResult>());
        }

        [Test]
        public void WhenUnsuccesfulDelete_ThenReturn500InternalServerError()
        {
            mockRepo
                .Setup(r => r.DeleteResponse(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            ObjectResult objRes = UnitTestUtility
                .GetActionResultModel<ObjectResult>(controller.Delete(It.IsAny<string>(), It.IsAny<string>()));

            Assert.That(objRes.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]

        public void WhenSuccessfulSearchQuestion_ThenReturnOkResultWithListOfQuestions()
        {
            List<string> questions = new List<string> { "How are you?", "What are you?" };
            mockRepo
                .Setup(r => r.GetMatchingQuestions(It.IsAny<string>()))
                .Returns(questions);

            OkObjectResult statusCode = UnitTestUtility
                .GetActionResultModel<OkObjectResult>(controller.SearchQuestion(It.IsAny<string>()));

            Assert.That(statusCode.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(statusCode.Value, Has.Count.EqualTo(2));
            Assert.That(statusCode.Value, Is.EquivalentTo(questions));
        }

        [Test]
        public void WhenUnsuccessfulSearchQuestion_ThenReturn500InternalServerError()
        {
            mockRepo
                .Setup(r => r.GetMatchingQuestions(It.IsAny<string>()))
                .Throws(new Exception());

            ObjectResult objRes = UnitTestUtility
                .GetActionResultModel<ObjectResult>(controller.SearchQuestion(It.IsAny<string>()));

            Assert.That(objRes.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public void WhenSuccessfulGetRandomWittyEntry_ThenReturnOkResultWithQuestionAndResponse()
        {
            mockRepo
                .Setup(r => r.GetAll())
                .Returns(new List<WittyEntry>
                {
                    WittyEntryBuilder.Default().Simple().Build()
                });


            OkObjectResult statusCode = UnitTestUtility
                .GetActionResultModel<OkObjectResult>(controller.GetRandomWittyEntry());


            Assert.That(statusCode.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(statusCode.Value, Has.Property("Question").EqualTo("What?"));
            Assert.That(statusCode.Value, Has.Property("Response").EqualTo("Yes"));
        }

        [Test]
        public void WhenUnsuccessfulGetRandomWittyEntry_ThenReturn500InternalServerError()
        {
            mockRepo
                .Setup(r => r.GetAll())
                .Throws(new Exception());

            ObjectResult objRes = UnitTestUtility
                .GetActionResultModel<ObjectResult>(controller.GetRandomWittyEntry());

            Assert.That(objRes.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
