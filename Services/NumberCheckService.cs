using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using webApiClass.Iservice;
using webApiClass.model;

namespace webApiClass.Services
{
    public class NumberCheckService : INumberCheckService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NumberCheckService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Root> GetCountryNumber(string number)
        {
            var verify = _httpClientFactory.CreateClient("Number-check");
            try
            {
                var data = await verify.GetAsync($"number_verification/validate?number={number}");

                if (data.IsSuccessStatusCode)
                {
                    var data2 = await data.Content.ReadAsStringAsync();
                    var details = JsonConvert.DeserializeObject<Root>(data2);
                    return details;
                } else
                {
                    return new Root();
                }
            } catch (Exception ex) {

                throw;
            }
        }
    }
}
