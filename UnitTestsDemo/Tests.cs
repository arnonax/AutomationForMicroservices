using System.Linq;
using Moq;
using NUnit.Framework;
using UnitTestsDemo.SUT;

namespace UnitTestsDemo
{
	public class Tests
	{
		[Test]
		public void CustomerBonusPointsGivesDiscountForOrdersAboveCertainAmount()
		{
			// Arrange
			const int minPoints = 100;
			const decimal minAmount = 1000;
			const decimal discountPercent = 15;

			const int actualPoints = 105;
			const decimal actualAmount = 2000;
			const decimal expectedDiscount = actualAmount * discountPercent / 100;
			const decimal expectedTotal = actualAmount - expectedDiscount;

			var bonusPlan = new Mock<IBonusPlan>();
			bonusPlan.Setup(o => o.MinimumPoints).Returns(minPoints);
			bonusPlan.Setup(o => o.MinimumAmount).Returns(minAmount);
			bonusPlan.Setup(o => o.DiscountPercent).Returns(discountPercent);

			var customer = new Mock<ICustomer>();
			customer.Setup(o => o.GetBonusPlan()).Returns(bonusPlan.Object);
			customer.Setup(o => o.BonusPoints).Returns(actualPoints);


			var baseOrder = new Mock<IOrder>();
			baseOrder.Setup(o => o.GetCustomer()).Returns(customer.Object);
			baseOrder.Setup(o => o.Total).Returns(actualAmount);

			var clubRewardsProcessor = new ClubRewardsProcessor(); // SUT
			
			// Act
			var updatedOrder = clubRewardsProcessor.Evaluate(baseOrder.Object);

			// Assert
			Assert.AreEqual(expectedTotal, updatedOrder.Total, "Total");
			Assert.AreEqual(1, updatedOrder.Rewards.Count, "Number of rewards");
			var actualDiscount = updatedOrder.Rewards.Single().Amount;
			Assert.AreEqual(expectedDiscount, actualDiscount, "Reward amount");
		}
	}
}