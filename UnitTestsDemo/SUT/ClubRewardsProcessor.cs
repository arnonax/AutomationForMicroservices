namespace UnitTestsDemo.SUT
{
	public class ClubRewardsProcessor
	{
		public IOrder Evaluate(IOrder baseOrder)
		{
			var bonusPlan = baseOrder.GetCustomer().GetBonusPlan();
			var customer = baseOrder.GetCustomer();
			if (customer.BonusPoints < bonusPlan.MinimumPoints
			    || baseOrder.Total < bonusPlan.MinimumAmount)
				return baseOrder;

			var discount = baseOrder.Total * bonusPlan.DiscountPercent / 100;
			var newOrder = new Order
			{
				Total = baseOrder.Total - discount
			};
			newOrder.AddReward(discount);
			return newOrder;
		}
	}
}