using OpenQA.Selenium;

namespace SeleniumDemo
{
	public class AuthenticationPage
	{
		private readonly IWebDriver _driver;

		public AuthenticationPage(IWebDriver driver)
		{
			_driver = driver;
		}

		public CreateAccountPanel CreateAccountPanel
		{
			get
			{
				var createAccountForm = _driver.FindElement(By.Id("create-account_form"));
				return new CreateAccountPanel(_driver, createAccountForm);
			}
		}

		public LoginPanel LoginPanel
		{
			get
			{
				var loginForm = _driver.FindElement(By.Id("login_form"));
				return new LoginPanel(loginForm);
			}
		}
	}
}