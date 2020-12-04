namespace VoteTests
{
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
}