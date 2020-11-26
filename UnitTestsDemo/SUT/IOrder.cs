using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTestsDemo.SUT
{
	public interface IOrder
	{
		Task<ICustomer> GetCustomer();
		decimal Total { get; }
		IReadOnlyCollection<IReward> Rewards { get; }
	}
}