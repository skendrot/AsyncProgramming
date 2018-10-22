using AsyncDemos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncDemos.ViewModels
{
    class CatClient
    {
        HttpClient _client = new HttpClient { BaseAddress = new Uri("https://api.thecatapi.com/") };

        public async Task<IEnumerable<Cat>> GetCatsAsync(int count)
        {
            // Do NOT return on the same SynchronizationContext
            var response = await _client
                .GetAsync($"api/images/get?format=json&results_per_page={count}")
                .ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var cats = await GetJsonResponse<IEnumerable<Cat>>(response)
                    .ConfigureAwait(false);
                if (cats != null)
                {
                    return cats;
                }
            }
            return Enumerable.Empty<Cat>();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var response = await _client
                .GetAsync("api/categories/list?format=json")
                .ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var categories = await GetJsonResponse<IEnumerable<Category>>(response)
                    .ConfigureAwait(false);
                if (categories != null)
                {
                    return categories;
                }
            }
            return Enumerable.Empty<Category>();
        }

        private static async Task<T> GetJsonResponse<T>(HttpResponseMessage response)
        {
            using (var content = await response.Content.ReadAsStreamAsync()
                .ConfigureAwait(false))
            {
                using (var textReader = new StreamReader(content))
                {
                    using (var reader = new JsonTextReader(textReader))
                    {
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<T>(reader);
                    }
                }
            }
        }

    }

}
