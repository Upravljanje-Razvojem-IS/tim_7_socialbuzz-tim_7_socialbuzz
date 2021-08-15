using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaMicroservice.ServiceCalls
{
    public class ItemForSaleService : IItemForSaleService
    {
        private readonly IConfiguration configuration;

        public ItemForSaleService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<T> GetItemForSaleById<T>(HttpMethod method, Guid itemForSaleId)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:AdMicroservice"] }api/itemsForSale/" + itemForSaleId);
                HttpRequestMessage request = new HttpRequestMessage(method, url);

                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default(T);
                    }

                    return JsonConvert.DeserializeObject<T>(content);
                }
                return default(T);
            }
        }
    }
}
