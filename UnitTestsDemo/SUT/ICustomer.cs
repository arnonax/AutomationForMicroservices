using System.Threading.Tasks;

namespace UnitTestsDemo.SUT
{
	public interface ICustomer
	{
		Task<IBonusPlan> GetBonusPlan();
		int BonusPoints { get; }
	}
}