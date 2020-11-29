using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace RestAPIDemo
{
	[SuppressMessage("ReSharper", "MethodHasAsyncOverload")]
	static class HttpExtensions
	{
		public static async Task<dynamic> SendPostAsync(this HttpClient client, string path, object request)
		{
			var content = JsonConvert.SerializeObject(request);
			var response = await client.PostAsync(path, new StringContent(content, Encoding.UTF8, "application/json"));
			return await DeserializeResponse(response);
		}

		public static async Task<dynamic> SendGetAsync(this HttpClient client, string path)
		{
			var response = await client.GetAsync(path);
			return await DeserializeResponse(response);
		}

		private static async Task<dynamic> DeserializeResponse(HttpResponseMessage response)
		{
			Assert.IsTrue(response.IsSuccessStatusCode, "Successful response");
			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject(responseBody);
		}
	}
}
