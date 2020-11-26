using System;
using OpenQA.Selenium;

namespace SeleniumDemo
{
	public class LoginPanel
	{
		private readonly IWebElement _formElement;

		public LoginPanel(IWebElement formElement)
		{
			_formElement = formElement;
		}

		public string EmailAddress
		{
			get => throw new NotImplementedException();
			set => _formElement.FindElement(By.Id("email")).SendKeys(value);
		}

		public string Password
		{
			get => throw new NotImplementedException();
			set => _formElement.FindElement(By.Id("passwd")).SendKeys(value);
		}

		public void SignIn()
		{
			_formElement.FindElement(By.Id("SubmitLogin")).Click();
		}
	}
}