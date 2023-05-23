using static System.Console;
using static System.Environment;
using OpenAI_API;
namespace OpenAIApp {
    class Program {

        public static string question = "Where are you from?";
        public static string answer = "";

        public static void SetQuestion(string q){
            question = q;
        }
        public static string GetQuestion(){
            return question;
        }
        public static async Task OpenAIMain(string[] args) {
            // get an api key.
            string? apiKey = Environment.GetEnvironmentVariable("OPEN_API_KEY");
            // create an api object.
            OpenAIAPI api = new(apiKey);
            answer = await api.Completions.GetCompletion(question);
            //WriteLine(result.Trim());
        }

        public static string GetResult(){
            return answer;
        }
    }
}