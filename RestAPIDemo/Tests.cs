using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RestAPIDemo
{
	public class Tests
	{
		[Test]
		public async Task CreateAndGetCats()
		{
			using var client = new HttpClient {BaseAddress = new Uri("http://PF16UHJZ:3000") };
			
			var postCatRequest = new {name = "Mitzi", age = 3, type = "Persian"};
			var postResponse = await client.SendPostAsync("/cat", postCatRequest);
			string id = postResponse.data._id;

			var getCatResponse = (await client.SendGetAsync($"/cat/{id}")).data;
			
			Assert.AreEqual(postCatRequest.name, (string)getCatResponse.name, "name");
			Assert.AreEqual(postCatRequest.age, (int)getCatResponse.age, "age");
			Assert.AreEqual(postCatRequest.type, (string)getCatResponse.type, "type");
		}
	}
}