using System;
using Newtonsoft.Json;
using Npgsql;
using NUnit.Framework;
using StackExchange.Redis;

namespace WorkerTests
{
	public class Tests
	{
		// Run "docker-compose -f docker-compose-workertests.yml up -V" before running the test

		private readonly RedisClient _redis = new RedisClient(6379);
		private readonly PostgresDb _postgresDb = new PostgresDb(BuildConnectionString());

		private static string BuildConnectionString()
		{
			var csb = new NpgsqlConnectionStringBuilder
			{
				Host = "localhost", Port = 5432, Username = "postgres", Password = "postgres"
			};
			return csb.ToString();
		}

		[TearDown]
		public void TearDown()
		{
			_redis.Dispose();
			_postgresDb.Dispose();
		}

		[Test]
		public void AddNewVote()
		{
			var vote = new {voter_id = Guid.NewGuid().ToString(), vote = "dummyVote"};
			_redis.Push(vote);
			Wait.Until(() => _postgresDb.GetVoteByVoterId(vote.voter_id) == vote.vote, 10.Seconds(), "Vote should be written to Postgres");
		}

		[Test]
		public void UpdateVote()
		{
			var vote = new { voter_id = Guid.NewGuid().ToString(), vote = "dummyVote" };
			_redis.Push(vote);
			Wait.Until(() => _postgresDb.GetVoteByVoterId(vote.voter_id) == vote.vote, 10.Seconds(), "Vote should be written to Postgres");
		}

		internal class RedisClient
		{
			private readonly ConnectionMultiplexer _client;

			public RedisClient(int port)
			{
				_client = ConnectionMultiplexer.Connect($"localhost:{port}");
			}

			public void Dispose()
			{
				_client.Close();
			}

			public void Push(object vote)
			{
				var value = JsonConvert.SerializeObject(vote);
				_client.GetDatabase().ListLeftPush("votes", value);
			}
		}
	}
}