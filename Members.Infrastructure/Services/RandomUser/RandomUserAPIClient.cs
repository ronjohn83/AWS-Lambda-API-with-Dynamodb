using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Members.Infrastructure.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Members.Infrastructure.Services.RandomUser
{
    public class RandomUserApiClient : IRandomUserApiClient
    {
        private readonly HttpClient _client;

        public RandomUserApiClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://randomuser.me");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client = httpClient;
        }

        public async Task<JsonResult> GetData()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/?inc=gender,name,email,phone,cell,location&nat=us");


            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                JsonResult jsonObject = (JsonResult)JsonConvert.DeserializeObject(json, typeof(JsonResult));

                return jsonObject;
            }


            return new JsonResult();
        }
    }
}
