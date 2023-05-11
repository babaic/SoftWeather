using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static VM.Weather.WeatherVM;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly string _apiKey;
        private WeatherBLL weather;

        public HomeController()
        {
            _apiKey = ConfigurationManager.AppSettings["OpenWeatherAPIKey"];
            weather = new WeatherBLL(_apiKey);
        }

        public async Task<ActionResult> Index(bool isSaved = false)
        {
            try
            {
                WeatherModel weatherModel = await weather.GetWeather();
                ViewBag.isSaved = isSaved;
                return View(weatherModel);
            }
            catch (Exception e)
            {
                return Content("Error " + e.Message);
            }
            
        }

        public async Task<ActionResult> SaveWeather(WeatherModel newData)
        {
            try
            {
                WeatherModel weatherModel = await weather.GetWeather();
                weatherModel.Main.Temp = newData.Main.Temp;
                weatherModel.Main.Humidity = newData.Main.Humidity;
                weatherModel.Main.Pressure = newData.Main.Pressure;
                weatherModel.Wind.Speed = newData.Wind.Speed;

                var jsonString = JsonConvert.SerializeObject(weatherModel);

                weather.SaveJsonToFile(jsonString);

                return RedirectToAction("Index", new { isSaved = true });
            }
            catch (Exception e)
            {
                return Content("Error " + e.Message);
            }
            
        }
    }
}