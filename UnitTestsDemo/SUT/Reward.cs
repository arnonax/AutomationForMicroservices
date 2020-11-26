namespace UnitTestsDemo.SUT
{
	public class Reward : IReward
	{
		public Reward(in decimal amount)
		{
			Amount = amount;
		}

		public decimal Amount { get; }
	}
}