using OpenQA.Selenium;

namespace SeleniumDemo
{
	public class Toolbar
	{
		private readonly IWebDriver _driver;
		private readonly IWebElement _nav;

		public Toolbar(IWebDriver driver)
		{
			_driver = driver;
			_nav = _driver.FindElement(By.ClassName("nav"));
		}

		public string LoggedInUserFullName => _nav.FindElement(By.ClassName("account")).Text;

		public AuthenticationPage SignIn()
		{
			_nav.FindElement(By.ClassName("login")).Click();
			return new AuthenticationPage(_driver);
		}

		public void SignOut()
		{
			_nav.FindElement(By.ClassName("logout")).Click();
		}
	}
}