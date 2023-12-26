using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RunStats.Models;
using System.Net.Http;

namespace RunStats.Services
{
    public class WeatherService
    {
        public async Task<Weather?> GetWeatherAsync()
        {
            var archiveWeatherURL = "https://archive-api.open-meteo.com/v1/archive?latitude=52.52&longitude=13.41&start_date=2023-12-25&end_date=2023-12-25&hourly=temperature_2m,relative_humidity_2m,cloud_cover,wind_speed_10m&timezone=auto";
            var currentWeatherURL = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,relative_humidity_2m,cloud_cover,wind_speed_10m&start_date=2023-12-26&end_date=2023-12-26";


            using (HttpClient httpClient = new HttpClient())
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(archiveWeatherURL);

                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();


                Console.WriteLine(result);

                return new Weather
                {
                    Temperature = 10.4,
                    Date = DateTime.Now,
                    Location = "Test location",
                    Moisture = 66,
                    Clouds = 25,
                    WindSpeed = 15
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Wystąpił błąd HTTP: {ex.Message}");
                return null;
            }
        }
    }
}
