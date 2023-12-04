using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TARpe21ShopVaitmaa.Core.Dto.OpenWeatherDto;
using TARpe21ShopVaitmaa.Core.Dto.WeatherDto;
using TARpe21ShopVaitmaa.Core.ServiceInterface;

namespace Tarpe21ShopVaitmaa.ApplicationServices.Services
{
    public class WeatherForecastsServices : IWeatherForecastsServices
    {
        public async Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto)
        {
            string apikey = "HtXsGFLFAcnYRG10m695VGkgKCAbEbZd";
            var url = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/127964?apiKey=HtXsGFLFAcnYRG10m695VGkgKCAbEbZd5metric=true";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                WeatherRootDto weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherRootDto>(json);

                weatherInfo.Headline.EffectiveDate = dto.EffectiveDate;
                weatherInfo.Headline.EffectiveEpochDate = dto.EffectiveEpochDate;
                weatherInfo.Headline.Severity = dto.Severity;
                weatherInfo.Headline.Text = dto.Text;
                weatherInfo.Headline.Category = dto.Category;
                weatherInfo.Headline.EndDate = dto.EndDate;
                weatherInfo.Headline.EndEpochDate = dto.EndEpochDate;
                weatherInfo.Headline.MobileLink = dto.MobileLink;
                weatherInfo.Headline.Link = dto.Link;

                weatherInfo.DailyForecasts[0].Temperature.Minimum.Value = dto.TempMinValue;
                weatherInfo.DailyForecasts[0].Temperature.Minimum.Unit = dto.TempMinUnit;
                weatherInfo.DailyForecasts[0].Temperature.Minimum.UnitType = dto.TempMinUnitType;

                weatherInfo.DailyForecasts[0].Temperature.Maximum.Value = dto.TempMaxValue;
                weatherInfo.DailyForecasts[0].Temperature.Maximum.Unit = dto.TempMaxUnit;
                weatherInfo.DailyForecasts[0].Temperature.Maximum.UnitType = dto.TempMaxUnitType;

                weatherInfo.DailyForecasts[0].Day.Icon = dto.DayIcon;
                weatherInfo.DailyForecasts[0].Day.IconPhrase = dto.DayIconPhrase;
                weatherInfo.DailyForecasts[0].Day.HasPrecipitation = dto.DayHasPrecipitation;
                weatherInfo.DailyForecasts[0].Day.PrecipitationType = dto.DayPrecipitationType;
                weatherInfo.DailyForecasts[0].Day.PrecipitationIntensity = dto.DayPrecipitationIntensity;

                weatherInfo.DailyForecasts[0].Night.Icon = dto.NightIcon;
                weatherInfo.DailyForecasts[0].Night.IconPhrase = dto.NightIconPhrase;
                weatherInfo.DailyForecasts[0].Night.HasPrecipitation = dto.NightHasPrecipitation;
                weatherInfo.DailyForecasts[0].Night.PrecipitationType = dto.NightPrecipitationType;
                weatherInfo.DailyForecasts[0].Night.PrecipitationIntensity = dto.NightPrecipitationIntensity;

            }
            return dto;
        }

        public async Task<OpenWeatherResultDto> OpenWeatherDetail(OpenWeatherResultDto dto)
        {
            string openapikey = "7ba820d416182f9e45f4e39c95d4696b";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.City}&units=metric&APPID={openapikey}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                OpenWeatherRootDto weatherResult = (new JavaScriptSerializer()).Deserialize<OpenWeatherRootDto>(json);

                dto.City = weatherResult.Name;
                dto.Timezone = weatherResult.Timezone;
                dto.Name = weatherResult.Name;
                dto.Lon = weatherResult.Lon;
                dto.Lat = weatherResult.Lat;
                dto.Temperature = Math.Round(weatherResult.Main.Temp);
                dto.Feels_like = Math.Round(weatherResult.Main.FeelsLike);
                dto.Humidity = weatherResult.Main.Humidity;
                dto.Pressure = weatherResult.Main.Pressure;
                dto.Speed = weatherResult.Wind.Speed;
                dto.Description = weatherResult.Weather[0].Description;

            }
            return dto;
        }



    }
}
