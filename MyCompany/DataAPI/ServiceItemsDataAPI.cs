using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MyCompany.DataAPI
{
    public class ServiceItemsDataAPI : IServiceItemsRepository
    {
        private HttpClient httpClient { get; set; }

        public ServiceItemsDataAPI()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<ServiceItem> GetServiceItems()
        {
            string url = @"https://localhost:44347/api/serviceItems";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<ServiceItem>>(json);
        }

        public ServiceItem GetServiceItemById(Guid id)
        {
            string url = @"https://localhost:44347/api/serviceItems/id";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<ServiceItem>(json);
        }

        public void SaveServiceItem(ServiceItem entity)
        {
            string url = @"https://localhost:44347/api/serviceItems";
            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
        public void DeleteServiceItem(Guid id)
        {
            string url = @"https://localhost:44347/api/serviceItems/id";
            var r = httpClient.DeleteAsync(url);
        }
    }
}
