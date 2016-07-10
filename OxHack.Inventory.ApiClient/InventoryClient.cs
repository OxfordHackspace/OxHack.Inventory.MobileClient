using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OxHack.Inventory.ApiClient.Models;

namespace OxHack.Inventory.ApiClient
{
	[Obsolete("TODO: Extract interface.")]
	public class InventoryClient
	{
		public InventoryClient(Uri apiUri)
		{
			this.CategoriesResource = new Uri(apiUri, "categories/");
			this.ItemsResource = new Uri(apiUri, "items/");
		}

		public async Task<IReadOnlyCollection<string>> GetAllCategoriesAsync()
		{
            try
            {
                using (var client = new HttpClient())
				{
					var response = await InventoryClient.GetAsyncWithTimeout(this.CategoriesResource, client);

					InventoryClient.ThrowExceptionOnError(response);

					var content = await response.Content.ReadAsStringAsync();

					var categories = JsonConvert.DeserializeObject<List<string>>(content);

					return categories;
				}
			}
            catch (TaskCanceledException e)
            {
                throw new InvalidOperationException("You need to be connected to the hackspace's network for this app to work.  If you aren't, then that's the problem.", e);
            }
		}

		public async Task<IEnumerable<Item>> GetItemsInCategoryAsync(string category)
		{
			using (var client = new HttpClient())
			{
				var resource = new Uri(this.ItemsResource, $"?category={ WebUtility.UrlEncode(category) }");

				var response = await InventoryClient.GetAsyncWithTimeout(resource, client);

				InventoryClient.ThrowExceptionOnError(response);

				var content = await response.Content.ReadAsStringAsync();

				var items = JsonConvert.DeserializeObject<List<Item>>(content);

				return items;
			}
		}

		public async Task<Item> GetItemByIdAsync(Guid id)
		{
			using (var client = new HttpClient())
			{
				var resource = new Uri(this.ItemsResource, $"{ id }");

				var response = await InventoryClient.GetAsyncWithTimeout(resource, client);

				InventoryClient.ThrowExceptionOnError(response);

				var content = await response.Content.ReadAsStringAsync();

				var item = JsonConvert.DeserializeObject<Item>(content);

				return item;
			}
		}

		private static Task<HttpResponseMessage> GetAsyncWithTimeout(Uri resource, HttpClient client)
		{
			var token = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
			return client.GetAsync(resource, token);
		}

		private static void ThrowExceptionOnError(HttpResponseMessage response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new HttpRequestException($"HTTP {response.StatusCode}. {response.ToString()}");
			}
		}

		private Uri CategoriesResource
		{
			get;
		}

		private Uri ItemsResource
		{
			get;
		}
	}
}
