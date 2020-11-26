using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTestsDemo.SUT
{
	public class Order : IOrder
	{
		private readonly List<IReward> _rewards = new List<IReward>();
		private readonly ICustomer _customer;

		public Order(ICustomer customer)
		{
			_customer = customer;
		}

#pragma warning disable CS1998
		public async Task<ICustomer> GetCustomer()
		{
			return _customer;
		}
#pragma warning restore CS1998

		public decimal Total { get; set; }

		public IReadOnlyCollection<IReward> Rewards => _rewards;

		public void AddReward(decimal discount)
		{
			_rewards.Add(new Reward(discount));
		}
	}
}