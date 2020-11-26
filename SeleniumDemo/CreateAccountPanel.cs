using System;
using OpenQA.Selenium;

namespace SeleniumDemo
{
	public class CreateAccountPanel
	{
		private readonly IWebDriver _driver;
		private readonly IWebElement _createAccountForm;

		public CreateAccountPanel(IWebDriver driver, IWebElement createAccountForm)
		{
			_driver = driver;
			_createAccountForm = createAccountForm;
		}

		public string EmailAddress
		{
			get => throw new NotImplementedException();
			set => _createAccountForm.FindElement(By.Id("email_create")).SendKeys(value);
		}

		public AccountCreationPage CreateAccount()
		{
			_createAccountForm.FindElement(By.Id("SubmitCreate")).Click();
			return new AccountCreationPage(_driver);
		}
	}
}