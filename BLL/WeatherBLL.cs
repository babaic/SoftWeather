using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static VM.Weather.WeatherVM;

namespace BLL
{
    public class WeatherBLL
    {
        private readonly string _apiKey;

        public WeatherBLL(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<WeatherModel> GetWeather()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.openweathermap11111111.org/data/2.5/weather?q=sarajevo&units=metric&appid=" + _apiKey);
            
            string responseBody = await response.Content.ReadAsStringAsync();

            WeatherModel weather = JsonConvert.DeserializeObject<WeatherModel>(responseBody);
            return weather;
        }

        public void SaveJsonToFile(string jsonString)
        {
            var fileLocation = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            var filePath = $"{fileLocation}\\JSONfile.json";
            File.WriteAllText(filePath, jsonString);

        }

    }
}
