﻿using System;
using System.Collections.Generic;



namespace RunStats.Utils
{

    public class WeatherDataParser
    {
        public class WeatherData
        {
            public HourlyUnits hourly_units { get; set; }
            public Hourly hourly { get; set; }
        }

        public class HourlyUnits
        {
            public string time { get; set; }
            public string temperature_2m { get; set; }
            public string relative_humidity_2m { get; set; }
            public string cloud_cover { get; set; }
            public string wind_speed_10m { get; set; }
        }

        public class Hourly
        {
            public List<DateTime> time { get; set; }
            public List<double?> temperature_2m { get; set; }
            public List<double?> relative_humidity_2m { get; set; }
            public List<double?> cloud_cover { get; set; }
            public List<double?> wind_speed_10m { get; set; }
        }
        public class HourlyData
        {
            public double Temperature { get; set; }
            public int Moisture { get; set; }
            public int Clouds { get; set; }
            public int WindSpeed { get; set; }
        }
        public static HourlyData parseWeatherData(WeatherData weatherData, DateTime targetDateTime)
        {
            DateTime timeRoundedToNearestHour = RoundToNearestHour.round(targetDateTime);

            int index = weatherData.hourly.time.FindIndex(time => time == timeRoundedToNearestHour);

            if (index == -1)
            {
                // if API does not include data for the selected time, just return some fake data
                // TODO - this is bad design, but works for now
                Console.WriteLine("No data for provided time");
                return new HourlyData
                {
                    Temperature = 0,
                    Moisture = 0,
                    Clouds = 0,
                    WindSpeed = 0
                };
            }

            HourlyData result = new HourlyData
            {
                Temperature = Convert.ToDouble(weatherData.hourly.temperature_2m[index]),
                Moisture = Convert.ToInt32(weatherData.hourly.relative_humidity_2m[index]),
                Clouds = Convert.ToInt32(weatherData.hourly.cloud_cover[index]),
                WindSpeed = Convert.ToInt32(weatherData.hourly.wind_speed_10m[index])
            };

            return result;
        }
    }
}
