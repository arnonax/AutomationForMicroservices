using System;
using System.Text.RegularExpressions;
using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Worker;

namespace ResultsTests
{
	public class ResultsTests
	{
		// Run "docker-compose -f docker-compose-resulttests.yml up -V" before running the test

		private readonly ResultsPage _resultsPage = new ResultsPage("http://localhost:5051");
		private readonly PostgresDB _postgresDB = new PostgresDB(PostgresDbBase.BuildConnectionString());

		[OneTimeTearDown]
		public void TearDown()
		{
			_resultsPage.Dispose();
		}

		[Test]
		public void UIDisplaysCorrectResults()
		{
			ClearAllResults();
			AddResults(VoteOption.Dog, 3);
			AddResults(VoteOption.Cat, 2);
			Wait.Until(() => _resultsPage.GetVotes() == 5, 2.Seconds(), $"Expected 5 votes, but had {_resultsPage.GetVotes()}");

			Assert.AreEqual("60.0%", _resultsPage.GetDogsResult(), "Dogs result");
			Assert.AreEqual("40.0%", _resultsPage.GetCatsResult(), "Cats result");
		}

		private void ClearAllResults()
		{
			_postgresDB.ClearAllVotes();
		}

		private void AddResults(VoteOption option, int count)
		{
			for (int i = 0; i < count; i++)
			{
				_postgresDB.AddVote(Guid.NewGuid().ToString(), option.Id);
			}
		}

		[Test]
		public void NoResults()
		{

		}
	}

	internal class PostgresDB : PostgresDbBase
	{
		public PostgresDB(string connectionString) : base(connectionString)
		{
			Program.InitVotesDB(Connection);
		}

		public void AddVote(string voterId, string vote)
		{
			Program.UpdateVote(Connection, voterId, vote);
		}

		public void ClearAllVotes()
		{
			CreateCommand("delete from votes").ExecuteNonQuery();
		}
	}

	internal class ResultsPage
	{
		private readonly IWebDriver _driver;

		public ResultsPage(string url)
		{
			_driver = new ChromeDriver {Url = url};
		}

		public int GetVotes()
		{
			var resultText = _driver.GetElementIfExists(By.Id("result"))?.Text;
			if (resultText == null)
				return 0;

			Assert.IsTrue(Regex.IsMatch(resultText, "\\d+ votes"), $"Result text '{resultText} is not in the expected format");
			var number = int.Parse(resultText.Split(' ')[0]);
			return number;
		}

		public void Dispose()
		{
			_driver.Quit();
		}

		public string GetDogsResult()
		{
			return _driver.GetElementIfExists(By.CssSelector(".dogs .stat")).Text;
		}

		public string GetCatsResult()
		{
			return _driver.GetElementIfExists(By.CssSelector(".cats .stat")).Text;
		}
	}
}