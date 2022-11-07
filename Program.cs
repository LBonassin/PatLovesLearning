using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace PatLovesLearning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MakeCall();
        }

        public static async void MakeCall()
        {
            var characters = new List<Character>();

            using var client = new HttpClient();

            string url = "https://swapi.dev/api/people/";

            var result = Task.Run(()=> client.GetStringAsync(url)).Result;
            
            var response = JsonConvert.DeserializeObject<Response>(result);

            characters.AddRange(response.results);

            while(response.next != null)
            {
                url = response.next;

                result = Task.Run(() => client.GetStringAsync(url)).Result;

                response = JsonConvert.DeserializeObject<Response>(result);

                characters.AddRange(response.results);
            }

            var exist = characters.Any(x => x.name == "Luke Skywalker") ? "yes" : "no";

            Console.WriteLine($"Is Luke Skywalker in this list? {exist}");
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }

    //Copy an example of the response and in VS, go to Edit > Paste Special > Paste JSON as classes
    public class Response
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public Character[] results { get; set; }
    }

    public class Character
    {
        public string name { get; set; }
        public string height { get; set; }
        public string mass { get; set; }
        public string hair_color { get; set; }
        public string skin_color { get; set; }
        public string eye_color { get; set; }
        public string birth_year { get; set; }
        public string gender { get; set; }
        public string homeworld { get; set; }
        public string[] films { get; set; }
        public string[] species { get; set; }
        public string[] vehicles { get; set; }
        public string[] starships { get; set; }
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string url { get; set; }
    }
}
