using Newtonsoft.Json;
using OxHack.Inventory.ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OxHack.Inventory.ApiClient
{
    [Obsolete("TODO: Extract interface.")]
    public class InventoryClient
    {
        public InventoryClient(Uri apiUri)
        {
            this.CategoriesResource = new Uri(apiUri, "categories/");
            this.ItemsResource = new Uri(apiUri, "items/");
            this.PhotosResource = new Uri(apiUri, "photos/");
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

		public async Task CreateItemAsync(Item model)
		{
			try
			{
				using (var client = new HttpClient())
				{
					var resource = new Uri(this.ItemsResource, $"{ model.Id }");

					var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
					content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("domain-model", "CreateItemCommand"));

					var response = await InventoryClient.PutWithTimeoutAsync(resource, content, client);

					InventoryClient.ThrowExceptionOnError(response);
				}
			}
			catch (TaskCanceledException e)
			{
				InventoryClient.ThrowTimeoutException(e);
			}
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

        public async Task<Uri> UploadPhoto(byte[] photoData)
        {
            Uri result = null;

            try
            {
                using (var client = new HttpClient())
                {
                    var resource = this.PhotosResource;

                    var content = new ByteArrayContent(photoData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    var response = await PostWithLongTimeoutAsync(resource, content, client);

                    InventoryClient.ThrowExceptionOnError(response);

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var uri = JsonConvert.DeserializeObject<String>(responseContent);
                    result = new Uri(uri);
                }
            }
            catch (TaskCanceledException e)
            {
                InventoryClient.ThrowTimeoutException(e);
            }

            return result;
        }

        public async Task AddPhotoToItem(Guid itemId, string concurrencyId, byte[] photoData)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var resource = new Uri(this.ItemsResource, $"{ itemId }/photos");

                    var content = new ByteArrayContent(photoData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    content.Headers.Add("ConcurrencyId", concurrencyId);

					var response = await PostWithLongTimeoutAsync(resource, content, client);

                    InventoryClient.ThrowExceptionOnError(response);
                }
            }
            catch (TaskCanceledException e)
            {
                InventoryClient.ThrowTimeoutException(e);
            }
        }

        public async Task RemovePhotoFromItem(Guid itemId, string concurrencyId, string removed)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var resource = new Uri(this.ItemsResource, $"{ itemId }/photos/{ removed }");

                    client.DefaultRequestHeaders.Add("ConcurrencyId", concurrencyId);

                    var response = await DeleteWithTimeoutAsync(resource, client);

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
            return await client.GetAsync(resource, GetCancellationToken());
        }

        private static async Task<HttpResponseMessage> PutWithTimeoutAsync(Uri resource, HttpContent content, HttpClient client)
        {
            return await client.PutAsync(resource, content, GetCancellationToken());
        }

        private static async Task<HttpResponseMessage> PostWithLongTimeoutAsync(Uri resource, HttpContent content, HttpClient client)
        {
            return await client.PostAsync(resource, content, GetLongCancellationToken());
        }

        private static async Task<HttpResponseMessage> DeleteWithTimeoutAsync(Uri resource, HttpClient client)
        {
            return await client.DeleteAsync(resource, GetCancellationToken());
        }

        private static CancellationToken GetCancellationToken()
            => new CancellationTokenSource(TimeSpan.FromSeconds(305)).Token;

		private static CancellationToken GetLongCancellationToken()
			=> new CancellationTokenSource(TimeSpan.FromSeconds(180)).Token;

		private static void ThrowExceptionOnError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
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

        private Uri PhotosResource
        {
            get;
        }
    }
}
