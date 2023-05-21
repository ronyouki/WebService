using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;


namespace ConsoleApplication1
{
    public class StWeather
    {
        public float temperature { get; set; }
        public float windspeed { get; set; }

        public float winddirection { get; set; }
        public float weathercode { get; set; }

        public int is_day{get;set;}

        public DateTime time;
    }
    public class WeatherForecast
    {
        public StWeather current_weather { get; set; }
    }
    class ProgramWeather
    {
        const string NO_VALUE = "---";

        static HttpClient client = new HttpClient();
        static string baseURL = "https://api.open-meteo.com/v1/forecast?latitude=35.6785&longitude=139.6823&current_weather=true&timezone=Asia%2FTokyo";

        public static string GetResult(){
            return result;
        }
        static string weatherCodeFunc(int weatherCode){

            if(weatherCode == 0) return "快晴";  // 0 : Clear Sky
            if(weatherCode == 1) return "晴れ";  // 1 : Mainly Clear
            if(weatherCode == 2) return "一部曇";  // 2 : Partly Cloudy
            if(weatherCode == 3) return "曇り";  // 3 : Overcast
            if(weatherCode <= 49) return "霧";  // 45, 48 : Fog And Depositing Rime Fog
            if(weatherCode <= 59) return "霧雨";  // 51, 53, 55 : Drizzle Light, Moderate And Dense Intensity ・ 56, 57 : Freezing Drizzle Light And Dense Intensity
            if(weatherCode <= 69) return "雨";  // 61, 63, 65 : Rain Slight, Moderate And Heavy Intensity ・66, 67 : Freezing Rain Light And Heavy Intensity
            if(weatherCode <= 79) return "雪";  // 71, 73, 75 : Snow Fall Slight, Moderate And Heavy Intensity ・ 77 : Snow Grains
            if(weatherCode <= 84) return "俄か雨";  // 80, 81, 82 : Rain Showers Slight, Moderate And Violent
            if(weatherCode <= 94) return "雪・雹";  // 85, 86 : Snow Showers Slight And Heavy
            if(weatherCode <= 99) return "雷雨";  // 95 : Thunderstorm Slight Or Moderate ・ 96, 99 : Thunderstorm With Slight And Heavy Hail
            return "不明";
        }
        public static void Run(string[] args)
        {
            GetWeatherText().GetAwaiter().GetResult();
            //Console.WriteLine(GetWeatherText());
            //Console.ReadKey();
        }
        static WeatherForecast weather;
        static async Task<WeatherForecast> GetWeatherAsync(string path)
        {
            String response = await client.GetStringAsync(path);
            weather = JsonSerializer.Deserialize<WeatherForecast>(response);
            Console.WriteLine(response);
            Console.WriteLine(weather);
            return weather;
        }

        static string result = "";
        static bool init = false;
        static async Task GetWeatherText()
        {
            if(!init){
                client.BaseAddress = new Uri(baseURL);
                init=true;
            }
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            WeatherForecast weatherForecast = await GetWeatherAsync(baseURL);

            Console.WriteLine(weatherForecast?.current_weather.temperature);
            Console.WriteLine(weatherForecast?.current_weather.windspeed);
            Console.WriteLine(weatherForecast?.current_weather.winddirection);
            Console.WriteLine(weatherCodeFunc((int)weatherForecast?.current_weather.weathercode));
            Console.WriteLine(weatherForecast?.current_weather.is_day);
            Console.WriteLine(weatherForecast?.current_weather.time);

            result = string.Format("東京の天気は{0}です。気温は{1}℃です。",
                    weatherCodeFunc((int)weatherForecast?.current_weather.weathercode),
                    weatherForecast?.current_weather.temperature
                    );
        }
    }
}