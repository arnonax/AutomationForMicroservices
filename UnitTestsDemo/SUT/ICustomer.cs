namespace UnitTestsDemo.SUT
{
	public interface ICustomer
	{
		IBonusPlan GetBonusPlan();
		int BonusPoints { get; }
	}
}