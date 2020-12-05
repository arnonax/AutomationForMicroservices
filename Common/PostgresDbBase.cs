using System;
using Npgsql;

namespace Common
{
	public class PostgresDbBase : IDisposable
	{
		protected readonly NpgsqlConnection Connection;

		public PostgresDbBase(string connectionString)
		{
			Connection = new NpgsqlConnection(connectionString);
			Connection.Open();
		}

		public void Dispose()
		{
			Connection?.Dispose();
		}

		public static string BuildConnectionString()
		{
			var csb = new NpgsqlConnectionStringBuilder
			{
				Host = "localhost", Port = 5432, Username = "postgres", Password = "postgres"
			};
			return csb.ToString();
		}

		protected NpgsqlCommand CreateCommand(string cmdText)
		{
			return new NpgsqlCommand(cmdText, Connection);
		}
	}
}