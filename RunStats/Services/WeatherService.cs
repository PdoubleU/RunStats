using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RunStats.Models;
using System.Net.Http;
using static RunStats.Services.LocationService;

namespace RunStats.Services
{
    public class WeatherService
    {
        public async Task<Weather?> GetWeatherAsync(string lat, string lng)
        {
            var archiveWeatherURL = $"https://archive-api.open-meteo.com/v1/archive?latitude={lat}&longitude={lng}&start_date=2023-12-25&end_date=2023-12-25&hourly=temperature_2m,relative_humidity_2m,cloud_cover,wind_speed_10m&timezone=auto";
            var currentWeatherURL = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&hourly=temperature_2m,relative_humidity_2m,cloud_cover,wind_speed_10m&start_date=2023-12-26&end_date=2023-12-26";



            using (HttpClient httpClient = new HttpClient())
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(archiveWeatherURL);

                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();


                Console.WriteLine(result);

                LocationData location = LocationService.getAddress(lat, lng);

                Console.WriteLine(location.ToString());

                return new Weather
                {
                    Temperature = 10.4,
                    Date = DateTime.Now,
                    Location = $"{location.address.city ?? location.display_name}",
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
