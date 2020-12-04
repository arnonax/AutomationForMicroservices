using System;
using Npgsql;
using NpgsqlTypes;

namespace WorkerTests
{
	internal class PostgresDb : IDisposable
	{
		private readonly NpgsqlConnection _connection;

		public PostgresDb(string connectionString)
		{
			_connection = new NpgsqlConnection(connectionString);
			_connection.Open();
		}

		public string GetVoteByVoterId(string voterId)
		{
			using var command = new NpgsqlCommand("select vote from votes where id=@voterId", _connection);
			command.Parameters.Add("voterId", NpgsqlDbType.Varchar).Value = voterId;
			return command.ExecuteScalar() as string;
		}

		public void Dispose()
		{
			_connection?.Dispose();
		}
	}
}