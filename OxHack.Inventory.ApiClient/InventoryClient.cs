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
using System.Net.Http.Headers;

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
			List<string> result = null;
			try
			{
				using (var client = new HttpClient())
				{
					var response = await InventoryClient.GetWithTimeoutAsync(this.CategoriesResource, client);

					InventoryClient.ThrowExceptionOnError(response);

					var content = await response.Content.ReadAsStringAsync();

					result = JsonConvert.DeserializeObject<List<string>>(content);
				}
			}
			catch (TaskCanceledException e)
			{
				InventoryClient.ThrowTimeoutException(e);
			}

			return result;
		}

		public async Task<IEnumerable<Item>> GetItemsInCategoryAsync(string category)
		{
			List<Item> result = null;
			try
			{
				using (var client = new HttpClient())
				{
					var resource = new Uri(this.ItemsResource, $"?category={ WebUtility.UrlEncode(category) }");

					var response = await InventoryClient.GetWithTimeoutAsync(resource, client);

					InventoryClient.ThrowExceptionOnError(response);

					var content = await response.Content.ReadAsStringAsync();

					result = JsonConvert.DeserializeObject<List<Item>>(content);
				}
			}
			catch (TaskCanceledException e)
			{
				InventoryClient.ThrowTimeoutException(e);
			}

			return result;
		}

		public async Task<Item> GetItemByIdAsync(Guid id)
		{
			Item result = null;
			try
			{
				using (var client = new HttpClient())
				{
					var resource = new Uri(this.ItemsResource, $"{ id }");

					var response = await InventoryClient.GetWithTimeoutAsync(resource, client);

					InventoryClient.ThrowExceptionOnError(response);

					var content = await response.Content.ReadAsStringAsync();

					result = JsonConvert.DeserializeObject<Item>(content);
				}
			}
			catch (TaskCanceledException e)
			{
				InventoryClient.ThrowTimeoutException(e);
			}

			return result;
		}

		public async Task SaveItemAsync(Item update)
		{
			try
			{
				using (var client = new HttpClient())
				{
                    var resource = new Uri(this.ItemsResource, $"{ update.Id }");

                    var content = new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json");
                    content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("domain-model", "UpdateItemCommand"));

                    var response = await InventoryClient.PutWithTimeoutAsync(resource, content, client);

					InventoryClient.ThrowExceptionOnError(response);
				}
			}
			catch (TaskCanceledException e)
			{
				InventoryClient.ThrowTimeoutException(e);
			}
        }

        public Task AddPhotos(Guid itemId, string concurrencyId, Uri added)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePhotos(Guid itemId, string concurrencyId, Uri removed)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var resource = new Uri(this.ItemsResource, $"{ itemId }");

                    var payload = new
                    {
                        Id = itemId.ToString(),
                        ConcurrencyId = concurrencyId,
                        Photo = removed
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("domain-model", "RemovePhotoCommand"));

                    var response = await InventoryClient.PutWithTimeoutAsync(resource, content, client);

                    InventoryClient.ThrowExceptionOnError(response);
                }
            }
            catch (TaskCanceledException e)
            {
                InventoryClient.ThrowTimeoutException(e);
            }
        }

        private static async Task<HttpResponseMessage> GetWithTimeoutAsync(Uri resource, HttpClient client)
		{
			var token = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
			return await client.GetAsync(resource, token);
		}

		private static async Task<HttpResponseMessage> PutWithTimeoutAsync(Uri resource, HttpContent content, HttpClient client)
		{
			var token = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
			return await client.PutAsync(resource, content, token);
		}

		private static void ThrowExceptionOnError(HttpResponseMessage response)
		{
			if (((int)response.StatusCode / 100) != ((int)HttpStatusCode.OK / 100))
			{
				throw new HttpRequestException($"HTTP {response.StatusCode}. {response.ToString()}");
			}
		}

		private static void ThrowTimeoutException(TaskCanceledException e)
		{
			throw new InvalidOperationException("You need to be connected to the hackspace's network for this app to work.  If you aren't, then that's the problem.", e);
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
