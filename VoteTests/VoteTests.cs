using NUnit.Framework;

namespace VoteTests
{
	public class VoteTests
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
}