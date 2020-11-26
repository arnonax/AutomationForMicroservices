using System;
using System.Text;
using NUnit.Framework;

namespace SeleniumDemo
{
	public class Tests
	{
		[Test]
		public void RegisterUserCanLogin()
		{
			using var myStore = new MyStoreApp();
			var randomSuffix = CreateRandomLetterString(5);
			var email = $"dummyUsername{randomSuffix}@dummy.com";
			var firstName = "Arnon" + randomSuffix;
			var lastName = "Axelrod" + randomSuffix;
			var password = "12345";

			myStore.RegisterNewUser(email, firstName, lastName, password);
			myStore.SignOut();

			myStore.Login(email, password);
			var loggedInUserFullName = myStore.GetLoggedInUserFullName();
			
			Assert.AreEqual($"{firstName} {lastName}", loggedInUserFullName, "Logged in user full name");
		}

		private static string CreateRandomLetterString(int length)
		{
			var random = new Random();
			var sb = new StringBuilder();
			for (var i = 0; i < length; i++)
			{
				sb.Append((char) random.Next('a', 'z'));
			}

			return sb.ToString();
		}
	}
}