using System.Linq;
using OpenQA.Selenium;

namespace ResultsTests
{
	public static class SeleniumExtensions
	{
		public static IWebElement GetElementIfExists(this ISearchContext searchContext, By locator)
		{
			var matchingElements = searchContext.FindElements(locator);
			return matchingElements.FirstOrDefault();
		}
	}
}
