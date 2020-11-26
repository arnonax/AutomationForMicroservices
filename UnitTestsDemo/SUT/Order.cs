using System;
using System.Collections.Generic;

namespace UnitTestsDemo.SUT
{
	public class Order : IOrder
	{
		private readonly List<IReward> _rewards = new List<IReward>();

		public ICustomer GetCustomer()
		{
			throw new NotImplementedException();
		}

		public decimal Total { get; set; }

		public IReadOnlyCollection<IReward> Rewards => _rewards;

		public void AddReward(decimal discount)
		{
			_rewards.Add(new Reward(discount));
		}
	}
}