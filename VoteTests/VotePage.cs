using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;

namespace VoteTests
{
	internal class VotePage
	{
		private readonly ChromeDriver _driver;

		public VotePage(string url)
		{
			_driver = new ChromeDriver {Url = url};
		}

		public string GetVoterId()
		{
			var voterId = _driver.ExecuteJavaScript<string>("return $.cookie('voter_id')");
			return voterId;
		}

		public void VoteFor(VoteOption option)
		{
			_driver.FindElement(By.Id(option.Id)).Click();
		}

		public void Dispose()
		{
			_driver.Quit();
		}
	}
}