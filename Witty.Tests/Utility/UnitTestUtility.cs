using Microsoft.AspNetCore.Mvc;

namespace Witty.Tests.Utility
{
    public class UnitTestUtility
    {
        public static T GetViewResultModel<T>(IActionResult actionResult) where T : class
        {
            var asViewResult = actionResult as ViewResult;

            return asViewResult.Model as T;
        }

        public static T GetActionResultModel<T>(IActionResult actionResult) where T : class
        {
            var asActionResult = actionResult as T;

            return asActionResult;
        }
    }
}
