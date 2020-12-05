using System.Linq;
using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WireMock.Matchers.Request;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace WireMockDemo
{
	public class MockRestApiDemo2
	{
		private const string Url = "http://localhost:8081";
		private WireMockServer _server;
		private ChromeDriver _driver;

		[SetUp]
		public void SetUp()
		{
			var settings = new WireMockServerSettings
			{
				Urls = new[] { Url },
				StartAdminInterface = true,
				ProxyAndRecordSettings = new ProxyAndRecordSettings
				{
					Url = "http://localhost:8080"
				}
			};
			_server = WireMockServer.Start(settings);
			_driver = new ChromeDriver();
		}

		[TearDown]
		public void TearDown()
		{
			_driver.Dispose();
			_server.Dispose();
		}

		[Test]
		public void WordSmithTest()
		{
			When(RequestIsSentTo("/words/noun")).RespondWith(WordResponse("dummy-noun"));
			When(RequestIsSentTo("/words/verb")).RespondWith(WordResponse("dummy-verb"));
			When(RequestIsSentTo("/words/adjective")).RespondWith(WordResponse("dummy-adjective"));

			_driver.Url = Url;
			Assert.AreEqual("Dummy-adjective", _driver.FindElement(By.CssSelector(".line1 .adjective .word")).Text);
			Assert.AreEqual("dummy-noun", _driver.FindElement(By.CssSelector(".line1 .noun .word")).Text);
			Assert.AreEqual("dummy-verb", _driver.FindElement(By.CssSelector(".line2 .verb .word")).Text);
			Assert.AreEqual("dummy-adjective", _driver.FindElement(By.CssSelector(".line3 .adjective .word")).Text);
			Assert.AreEqual("dummy-noun", _driver.FindElement(By.CssSelector(".line3 .noun .word")).Text);
		}

		[Test]
		public void WordSmithTest2()
		{
			var nounIsRequested = RequestIsSentTo("/words/noun");
			var verbIsRequested = RequestIsSentTo("/words/verb");
			var adjectiveIsRequested = RequestIsSentTo("/words/adjective");

			When(nounIsRequested).RespondWith(WordResponse("dummy-noun"));
			When(verbIsRequested).RespondWith(WordResponse("dummy-verb"));
			When(adjectiveIsRequested).RespondWith(WordResponse("dummy-adjective"));

			_driver.Url = Url;
			Wait.Until(() => "Dummy-adjective" == _driver.FindElement(By.CssSelector(".line1 .adjective .word")).Text, 2.Seconds(), "First word");
			Assert.AreEqual("dummy-noun", _driver.FindElement(By.CssSelector(".line1 .noun .word")).Text);
			Assert.AreEqual("dummy-verb", _driver.FindElement(By.CssSelector(".line2 .verb .word")).Text);
			Assert.AreEqual("dummy-adjective", _driver.FindElement(By.CssSelector(".line3 .adjective .word")).Text);
			Assert.AreEqual("dummy-noun", _driver.FindElement(By.CssSelector(".line3 .noun .word")).Text);

			Assert.AreEqual(2, _server.FindLogEntries(nounIsRequested).Count(), "nouns");
			Assert.AreEqual(1, _server.FindLogEntries(verbIsRequested).Count(), "verbs");
			Assert.AreEqual(2, _server.FindLogEntries(adjectiveIsRequested).Count(), "adjectives");
		}

		private IRespondWithAProvider When(IRequestMatcher requestMatcher)
		{
			return _server.Given(requestMatcher);
		}

		private static IResponseBuilder WordResponse(string word)
		{
			return Response.Create().WithBody($"{{\"word\": \"{word}\"}}");
		}

		private static IRequestBuilder RequestIsSentTo(string path)
		{
			return Request.Create().WithPath(path).UsingGet();
		}
	}
}