using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;

namespace SeleniumDemo
{
	public class MyStoreApp : IDisposable
	{
		private readonly IWebDriver _driver = new ChromeDriver();

		public MyStoreApp()
		{
			_driver.Url = "http://automationpractice.com/";
		}

		public void RegisterNewUser(string email, string firstName, string lastName, string password)
		{
			var authenticationPage = Toolbar.SignIn();
			var createAccountPanel = authenticationPage.CreateAccountPanel;
			createAccountPanel.EmailAddress = email;
			var accountCreationPage = createAccountPanel.CreateAccount();
			var accountBuilder = new AccountBuilder()
				.WithFirstName(firstName)
				.WithLastName(lastName)
				.WithPassword(password);
			accountBuilder.Fill(accountCreationPage);
			accountCreationPage.Register();
		}

		public Toolbar Toolbar => new Toolbar(_driver);

		public void SignOut()
		{
			Toolbar.SignOut();
		}

		public void Login(string email, string password)
		{
			var authenticationPage = Toolbar.SignIn();
			var loginPanel = authenticationPage.LoginPanel;
			loginPanel.EmailAddress = email;
			loginPanel.Password = password;
			loginPanel.SignIn();
		}

		public string GetLoggedInUserFullName()
		{
			return Toolbar.LoggedInUserFullName;
		}

		public void Dispose()
		{
			_driver.TakeScreenshot().SaveAsFile("Screenshot.png");
			_driver.Quit();
		}
	}
}