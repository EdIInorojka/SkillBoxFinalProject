using MyCompanyWPFApp.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MyCompanyWPFApp.DataAPI
{
    public class BlogItemsDataAPI
    {
        private HttpClient httpClient { get; set; }

        public BlogItemsDataAPI()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<BlogItem> GetBlogItems()
        {
            string url = @"https://localhost:44347/api/blogItems";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<BlogItem>>(json);
        }

        public BlogItem GetBlogItemById(Guid id)
        {
            string url = @"https://localhost:44347/api/blogItems/id";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<BlogItem>(json);
        }

        public void SaveBlogItem(BlogItem entity)
        {
            string url = @"https://localhost:44347/api/blogItems";
            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
        public void DeleteBlogItem(Guid id)
        {
            string url = @"https://localhost:44347/api/blogItems/id";
            var r = httpClient.DeleteAsync(url);
        }

    }
}
