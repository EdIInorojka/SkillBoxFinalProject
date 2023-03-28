using MyCompany.Domain.Repositories.Abstract;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System;
using MyCompany.Domain.Entities;
using System.Collections.Generic;

namespace MyCompany.DataAPI
{
    public class TextFieldsDataAPI : ITextFieldsRepository
    {
        private HttpClient httpClient { get; set; }

        public TextFieldsDataAPI()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<TextField> GetTextFields()
        {
            string url = @"https://localhost:44347/api/textFields";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<TextField>>(json);
        }

        public TextField GetTextFieldById(Guid id)
        {
            string url = @"https://localhost:44347/api/textField/id";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<TextField>(json);
        }

        public TextField GetTextFieldByCodeWord(string codeword)
        {
            string url = @"https://localhost:44347/api/TextField/codeword";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<TextField>(json);
        }

        public void SaveTextField(TextField entity)
        {
            string url = @"https://localhost:44347/api/textField";
            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
        public void DeleteTextField(Guid id)
        {
            string url = @"https://localhost:44347/api/textField/id";
            var r = httpClient.DeleteAsync(url);
        }
    }
}
