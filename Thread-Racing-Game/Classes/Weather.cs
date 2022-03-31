using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Racing_Game.Classes
{
    public class Weather
    {
        private String condition;
        private String locationName;
        private String[] cities = {"Emmen", "Kyiv", "Krakow", "Berlin", "Minsk", "Varnek", "Bodo", "Amsterdam", "Oslo"};

        public Weather()
        {
            getWeather("Emmen");
        }

        public string Condition
        {
            get => condition;
            set => condition = value;
        }
        public string LocationName
        {
            get => locationName;
            set => locationName = value;
        }

        public void getRandomWeather()
        {
            Random rnd = new Random();
            int index = rnd.Next(cities.Length);
            getWeather(cities[index]);
        }

        private void getWeather(String location)
        {
            string url_api = null;

            url_api = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=0f8fb90d8b26b2347d7b2c845966884d";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url_api);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                Root weatherResponse = JsonConvert.DeserializeObject<Root>(response);

                Condition = weatherResponse.Weather[0].Description;
                LocationName = location;

                httpWebResponse.Close();
            }
        }
    }

    public class Coord
    {
        [JsonProperty("lon")]
        public double Lon
        {
            get;
            set;
        }

        [JsonProperty("lat")]
        public double Lat
        {
            get;
            set;
        }
    }

    public class WeatherInfo
    {
        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("main")]
        public string Main
        {
            get;
            set;
        }

        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("icon")]
        public string Icon
        {
            get;
            set;
        }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temp
        {
            get;
            set;
        }

        [JsonProperty("feels_like")]
        public double FeelsLike
        {
            get;
            set;
        }

        [JsonProperty("temp_min")]
        public double TempMin
        {
            get;
            set;
        }

        [JsonProperty("temp_max")]
        public double TempMax
        {
            get;
            set;
        }

        [JsonProperty("pressure")]
        public int Pressure
        {
            get;
            set;
        }

        [JsonProperty("humidity")]
        public int Humidity
        {
            get;
            set;
        }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed
        {
            get;
            set;
        }

        [JsonProperty("deg")]
        public int Deg
        {
            get;
            set;
        }

        [JsonProperty("gust")]
        public double Gust
        {
            get;
            set;
        }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public int All
        {
            get;
            set;
        }
    }

    public class Sys
    {
        [JsonProperty("type")]
        public int Type
        {
            get;
            set;
        }

        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("country")]
        public string Country
        {
            get;
            set;
        }

        [JsonProperty("sunrise")]
        public int Sunrise
        {
            get;
            set;
        }

        [JsonProperty("sunset")]
        public int Sunset
        {
            get;
            set;
        }
    }

    public class Root
    {
        [JsonProperty("coord")]
        public Coord Coord
        {
            get;
            set;
        }

        [JsonProperty("weather")]
        public List<WeatherInfo> Weather
        {
            get;
            set;
        }

        [JsonProperty("base")]
        public string Base
        {
            get;
            set;
        }

        [JsonProperty("main")]
        public Main Main
        {
            get;
            set;
        }

        [JsonProperty("visibility")]
        public int Visibility
        {
            get;
            set;
        }

        [JsonProperty("wind")]
        public Wind Wind
        {
            get;
            set;
        }

        [JsonProperty("clouds")]
        public Clouds Clouds
        {
            get;
            set;
        }

        [JsonProperty("dt")]
        public int Dt
        {
            get;
            set;
        }

        [JsonProperty("sys")]
        public Sys Sys
        {
            get;
            set;
        }

        [JsonProperty("timezone")]
        public int Timezone
        {
            get;
            set;
        }

        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("cod")]
        public int Cod
        {
            get;
            set;
        }
    }

}
