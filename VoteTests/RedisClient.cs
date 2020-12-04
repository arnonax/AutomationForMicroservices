using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace VoteTests
{
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

			var definition = new {vote = "", voter_id = ""};
			while(JsonConvert.DeserializeAnonymousType(value, definition).voter_id != voterId)
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
}