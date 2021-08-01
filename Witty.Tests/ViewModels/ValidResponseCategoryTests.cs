using NUnit.Framework;
using Witty.Constants;
using Witty.ViewModels;

namespace Witty.Tests.ViewModels
{
    [TestFixture]
    public class ValidResponseCategoryTests
    {
        private ValidResponseCategory vrc;

        [SetUp]
        public void SetUp()
        {
            vrc = new ValidResponseCategory();
        }

        [Test]
        public void GivenValidResponseCategory_WhenIsValidCalled_ThenReturnTrue()
        {
            Assert.That(vrc.IsValid(Messenger.Analogy), Is.True);
        }

        [Test]
        public void GivenInvalidResponseCategory_WhenIsValidCalled_ThenReturnFalse()
        {
            Assert.That(vrc.IsValid("Invalid_Category"), Is.False);
        }
    }
}
