using System.Threading.Tasks;

namespace UnitTestsDemo.SUT
{
	public class ClubRewardsProcessor
	{
		public async Task<IOrder> Evaluate(IOrder baseOrder)
		{
			var customer = await baseOrder.GetCustomer();
			var bonusPlan = await customer.GetBonusPlan();
			if (customer.BonusPoints < bonusPlan.MinimumPoints
			    || baseOrder.Total < bonusPlan.MinimumAmount)
				return baseOrder;

			var discount = baseOrder.Total * bonusPlan.DiscountPercent / 100;
			var newOrder = new Order(customer)
			{
				Total = baseOrder.Total - discount
			};
			newOrder.AddReward(discount);
			return newOrder;
		}
	}
}