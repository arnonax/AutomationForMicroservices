namespace UnitTestsDemo.SUT
{
	public interface IBonusPlan
	{
		int MinimumPoints { get; }
		decimal MinimumAmount { get; }
		decimal DiscountPercent { get; }
	}
}