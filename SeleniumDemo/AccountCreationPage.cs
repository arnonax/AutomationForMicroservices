using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumDemo
{
	public class AccountCreationPage
	{
		private readonly IWebDriver _driver;

		public AccountCreationPage(IWebDriver driver)
		{
			_driver = driver;
			WaitForFormToAppear();
		}

		private void WaitForFormToAppear()
		{
			var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
			wait.Until(ExpectedConditions.ElementIsVisible(By.Id("account-creation_form")));
		}

		public string FirstName
		{
			get => throw new NotImplementedException();
			set => FillText("customer_firstname", value);
		}

		private void FillText(string elementId, string value)
		{
			_driver.FindElement(By.Id(elementId)).SendKeys(value);
		}

		public string LastName
		{
			get => throw new NotImplementedException();
			set => FillText("customer_lastname", value);
		}

		public string Password
		{
			get => throw new NotImplementedException();
			set => FillText("passwd", value);
		}

		public string Address
		{
			get => throw new NotImplementedException();
			set => FillText("address1", value);
		}

		public string City
		{
			get => throw new NotImplementedException();
			set => FillText("city", value);
		}

		public string State
		{
			get => throw new NotImplementedException();
			set => new SelectElement(_driver.FindElement(By.Id("id_state"))).SelectByText(value);
		}

		public string ZipOrPostalCode
		{
			get => throw new NotImplementedException();
			set => FillText("postcode", value);
		}

		public string MobilePhone
		{
			get => throw new NotImplementedException();
			set => FillText("phone_mobile", value);
		}

		public void Register()
		{
			_driver.FindElement(By.Id("submitAccount")).Click();
		}
	}
}