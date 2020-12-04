using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using StackExchange.Redis;

namespace VoteTests
{
	public class Tests
	{
		// Run "docker-compose -f docker-compose-votetests.yml up -V" before running the test

		private readonly VotePage _votePage = new VotePage("http://localhost:5050");
		private readonly RedisClient _redis = new RedisClient(6379);

		[TearDown]
		public void TearDown()
		{
			_votePage.Dispose();
			_redis.Dispose();
		}

		[Test]
		public void UserVoteIsRegistered()
		{
			var voterId = _votePage.GetVoterId();
			_votePage.VoteFor(VoteOption.Dog);
			var vote = _redis.GetVoteByVoterId(voterId);
			Assert.AreEqual(VoteOption.Dog.Id, vote);
		}
	}

	internal class RedisClient
	{
		private readonly ConnectionMultiplexer _client;

		public RedisClient(int port)
		{
			_client = ConnectionMultiplexer.Connect($"localhost:{port}");
		}

		public string GetVoteByVoterId(string voterId)
		{
			RedisValue value;
			do
			{
				value = _client.GetDatabase().ListLeftPop("votes");
			} while (value.IsNull);

			while(JObject.Parse(value.ToString())["voter_id"].ToString() != voterId)
			{
				value = _client.GetDatabase().ListLeftPop("votes");
			}

			return JObject.Parse(value.ToString())["vote"].ToString();
		}

		public void Dispose()
		{
			_client.Close();
		}
	}

	public sealed class VoteOption
	{
		public string Id { get; }

		private VoteOption(string voteId)
		{
			Id = voteId;
		}

		public static readonly VoteOption Cat = new VoteOption("a");
		public static readonly VoteOption Dog = new VoteOption("b");
	}

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