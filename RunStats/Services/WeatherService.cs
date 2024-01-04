using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RunStats.Models;
using System.Net.Http;
using static RunStats.Services.LocationService;
using static RunStats.Utils.WeatherDataParser;
using RunStats.Utils;

namespace RunStats.Services
{
    public class WeatherService
    {
        public async Task<Weather?> GetWeatherAsync(string lat, string lng, DateTime targetDate)
        {
            var selectedDate = targetDate.ToString("yyyy-MM-dd");
            DateTime yesterday = DateTime.Now.AddDays(-1);

            var archiveWeatherURL = $"https://archive-api.open-meteo.com/v1/archive?latitude={lat}&longitude={lng}&start_date={selectedDate}&end_date={selectedDate}&hourly=temperature_2m,relative_humidity_2m,cloud_cover,wind_speed_10m&timezone=auto";
            var forecastWeatherURL = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&hourly=temperature_2m,relative_humidity_2m,cloud_cover,wind_speed_10m&start_date={selectedDate}&end_date={selectedDate}";

            var url = (targetDate > yesterday) ? forecastWeatherURL : archiveWeatherURL;

            using (HttpClient httpClient = new HttpClient())
            try
            {
                LocationData location = LocationService.getAddress(lat, lng);
                HttpResponseMessage response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();

                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(result);

                HourlyData parsedData = WeatherDataParser.parseWeatherData(weatherData, targetDate);

                return new Weather
                {
                    Temperature = parsedData.Temperature,
                    Date = DateTime.Now,
                    Location = $"{location.address.city ?? location.display_name}",
                    Moisture = parsedData.Moisture,
                    Clouds = parsedData.Clouds,
                    WindSpeed = parsedData.WindSpeed,
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
                return null;
            }
        }
    }
}
