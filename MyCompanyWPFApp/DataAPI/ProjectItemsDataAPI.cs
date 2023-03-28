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
    public class ProjectItemsDataAPI
    {
        private HttpClient httpClient { get; set; }

        public ProjectItemsDataAPI()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<ProjectItem> GetProjectItems()
        {
            string url = @"https://localhost:44347/api/projectItems";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<ProjectItem>>(json);
        }

        public ProjectItem GetProjectItemById(Guid id)
        {
            string url = @"https://localhost:44347/api/projectItems/id";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<ProjectItem>(json);
        }

        public void SaveProjectItem(ProjectItem entity)
        {
            string url = @"https://localhost:44347/api/projectItems";
            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
        public void DeleteProjectItem(Guid id)
        {
            string url = @"https://localhost:44347/api/projectItems/id";
            var r = httpClient.DeleteAsync(url);
        }
    }
}
