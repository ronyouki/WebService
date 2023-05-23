using static System.Console;
using static System.Environment;
using OpenAI_API;
namespace OpenAIApp {
    class Program {

        public static string answer = "";
        public static async Task OpenAIMain(string[] args) {
            // get an api key.
            string? apiKey = Environment.GetEnvironmentVariable("OPEN_API_KEY");
            Console.WriteLine(apiKey);
            // create an api object.
            OpenAIAPI api = new(apiKey);
            answer = await api.Completions.GetCompletion("where are you from?");
            //WriteLine(result.Trim());
        }

        public static string GetResult(){
            return answer;
        }
    }
}