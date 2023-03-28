
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Net.Http;
using System.Text;
using MyCompanyTelegramBot.Domain.Entities;

namespace MyCompanyTelegramBot.DataAPI
{
    public class QuestionsDataAPI
    {
        private HttpClient httpClient { get; set; }

        public QuestionsDataAPI()
        {
            httpClient = new HttpClient();
        }
        public IEnumerable<Question> GetQuestions()
        {
            string url = @"https://localhost:44347/api/questions";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<Question>>(json);
        }

        public Question GetQuestionById(Guid id)
        {
            string url = @"https://localhost:44347/api/question/id";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<Question>(json);
        }
        public void SaveQuestion(Question entity)
        {
            string url = @"https://localhost:44347/api/question";
            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
        public void DeleteQuestion(Guid id)
        {
            string url = @"https://localhost:44347/api/question/id";
            var r = httpClient.DeleteAsync(url);
        }
    }
}
