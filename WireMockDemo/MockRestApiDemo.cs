using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace WireMockDemo
{
	public class MockRestApiDemo
	{
		[Test]
		public void WordSmithTest()
		{
			var url = "http://localhost:8081";
			var settings = new WireMockServerSettings
			{
				Urls = new[] { url },
				StartAdminInterface = true,
				ProxyAndRecordSettings = new ProxyAndRecordSettings
				{
					Url = "http://localhost:8080"
				}
			};
			using var server = WireMockServer.Start(settings);
			server.Given(Request.Create().WithPath("/words/noun").UsingGet()).RespondWith(
				Response.Create().WithBody("{\"word\": \"dummy-noun\"}"));
			server.Given(Request.Create().WithPath("/words/verb").UsingGet()).RespondWith(
				Response.Create().WithBody("{\"word\": \"dummy-verb\"}"));
			server.Given(Request.Create().WithPath("/words/adjective").UsingGet()).RespondWith(
				Response.Create().WithBody("{\"word\": \"dummy-adjective\"}"));
			var driver = new ChromeDriver {Url = url};

			Assert.AreEqual("Dummy-adjective", driver.FindElement(By.CssSelector(".line1 .adjective .word")).Text);
			Assert.AreEqual("dummy-noun", driver.FindElement(By.CssSelector(".line1 .noun .word")).Text);
			Assert.AreEqual("dummy-verb", driver.FindElement(By.CssSelector(".line2 .verb .word")).Text);
			Assert.AreEqual("dummy-adjective", driver.FindElement(By.CssSelector(".line3 .adjective .word")).Text);
			Assert.AreEqual("dummy-noun", driver.FindElement(By.CssSelector(".line3 .noun .word")).Text);

			driver.Quit();
		}
	}
}