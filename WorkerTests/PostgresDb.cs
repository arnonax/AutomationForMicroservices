using Common;

namespace WorkerTests
{
	internal class PostgresDb : PostgresDbBase
	{
		public PostgresDb(string connectionString) : base(connectionString)
		{
		}

		public string GetVoteByVoterId(string voterId)
		{
			using var command = CreateCommand("select vote from votes where id=@voterId");
			command.Parameters.AddWithValue("voterId", voterId);
			return command.ExecuteScalar() as string;
		}
	}
}