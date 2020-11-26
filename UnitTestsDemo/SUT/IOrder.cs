using System.Collections.Generic;

namespace UnitTestsDemo.SUT
{
	public interface IOrder
	{
		ICustomer GetCustomer();
		decimal Total { get; }
		IReadOnlyCollection<IReward> Rewards { get; }
	}
}