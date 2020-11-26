namespace SeleniumDemo
{
	public class AccountBuilder
	{
		private string _firstName = "dummyFirstName";
		private string _lastName = "dummyLastName";
		private string _password = "dummyPassword";

		public AccountBuilder WithFirstName(string firstName)
		{
			_firstName = firstName;
			return this;
		}

		public AccountBuilder WithLastName(string lastName)
		{
			_lastName = lastName;
			return this;
		}

		public void Fill(AccountCreationPage accountCreationPage)
		{
			accountCreationPage.FirstName = _firstName;
			accountCreationPage.LastName = _lastName;
			accountCreationPage.Password = _password;
			accountCreationPage.Address = "dummy address";
			accountCreationPage.City = "dummy city";
			accountCreationPage.State = "Alabama";
			accountCreationPage.ZipOrPostalCode = "12345";
			accountCreationPage.MobilePhone = "12345678";
		}

		public AccountBuilder WithPassword(string password)
		{
			_password = password;
			return this;
		}
	}
}